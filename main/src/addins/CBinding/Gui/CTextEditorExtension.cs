//
// CTextEditorExtension.cs
//
// Authors:
//   Marcos David Marin Amador <MarcosMarin@gmail.com>
//
// Copyright (C) 2007 Marcos David Marin Amador
//
//
// This source code is licenced under The MIT License:
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Ide.CodeCompletion;
using MonoDevelop.Components.Commands;

using CBinding.Parser;
using Mono.TextEditor;

namespace CBinding
{
	public class CTextEditorExtension : CompletionTextEditorExtension
	{
		// Allowed chars to be next to an identifier
		private static char[] allowedChars = new char[] {
			'.', ':', ' ', '\t', '=', '*', '+', '-', '/', '%', ',', '&',
			'|', '^', '{', '}', '[', ']', '(', ')', '\n', '!', '?', '<', '>'
		};
		
		// Allowed Chars to be next to an identifier excluding ':' (to get the full name in '::' completion).
		private static char[] allowedCharsMinusColon = new char[] {
			'.', ' ', '\t', '=', '*', '+', '-', '/', '%', ',', '&', '|',
			'^', '{', '}', '[', ']', '(', ')', '\n', '!', '?', '<', '>'
		};
		
		/// <summary>
		/// A delegate for getting completion data 
		/// </summary>
		private delegate CompletionDataList GetMembersForExtension (CTextEditorExtension self, string completionExtension, string completionText);
		
		/// <summary>
		/// An associative array containing each completion-triggering extension 
		/// and its respective callback
		/// </summary>
		private static KeyValuePair<string, GetMembersForExtension>[] completionExtensions = new KeyValuePair<string, GetMembersForExtension>[] {
			new KeyValuePair<string, GetMembersForExtension>("::", GetItemMembers),
			new KeyValuePair<string, GetMembersForExtension>("->", GetInstanceMembers),
			new KeyValuePair<string, GetMembersForExtension>(".", GetInstanceMembers)
		};
		
		static bool IsOpenBrace (char c)
		{
			return c == '(' || c == '{' || c == '<' || c == '[';
		}
		static bool IsCloseBrace (char c)
		{
			return c == ')' || c == '}' || c == '>' || c == ']';
		}
		
		static bool IsBrace (char c)
		{
			return IsOpenBrace  (c) || IsCloseBrace (c);
		}
		
		static int SearchMatchingBracket (TextEditorData editor, int offset, char openBracket, char closingBracket, int direction)
		{
			bool isInString       = false;
			bool isInChar         = false;	
			bool isInBlockComment = false;
			int depth = -1;
			while (offset >= 0 && offset < editor.Length) {
				char ch = editor.GetCharAt (offset);
				switch (ch) {
					case '/':
						if (isInBlockComment) 
							isInBlockComment = editor.GetCharAt (offset + direction) != '*';
						if (!isInString && !isInChar && offset - direction < editor.Length) 
							isInBlockComment = offset > 0 && editor.GetCharAt (offset - direction) == '*';
						break;
					case '"':
						if (!isInChar && !isInBlockComment) 
							isInString = !isInString;
						break;
					case '\'':
						if (!isInString && !isInBlockComment) 
							isInChar = !isInChar;
						break;
					default :
						if (ch == closingBracket) {
							if (!(isInString || isInChar || isInBlockComment)) 
								--depth;
						} else if (ch == openBracket) {
							if (!(isInString || isInChar || isInBlockComment)) {
								++depth;
								if (depth == 0) 
									return offset;
							}
						}
						break;
				}
				offset += direction;
			}
			return -1;
		}
		
		static int GetClosingBraceForLine (TextEditorData editor, LineSegment line, out int openingLine)
		{
			int offset = SearchMatchingBracket (editor, line.Offset, '{', '}', -1);
			if (offset == -1) {
				openingLine = -1;
				return -1;
			}
				
			openingLine = editor.Document.OffsetToLineNumber (offset);
			return offset;
		}
		
		public override bool KeyPress (Gdk.Key key, char keyChar, Gdk.ModifierType modifier)
		{
			var line = Editor.Document.GetLine (Editor.Caret.Line);
			int lineCursorIndex = Editor.Caret.Column;
			string lineText = Editor.GetLineText (Editor.Caret.Line);
			// Smart Indentation
			if (TextEditorProperties.IndentStyle == IndentStyle.Smart)
			{
				if (keyChar == '}') {
					// Only indent if the brace is preceeded by whitespace.
					if(AllWhiteSpace(lineText.Substring(0, lineCursorIndex))) {
						int braceOpeningLine;
						if(GetClosingBraceForLine(Editor, line, out braceOpeningLine) >= 0)
						{
							Editor.Replace (line.Offset, line.EditableLength, GetIndent(Editor, braceOpeningLine, 0) + "}" + lineText.Substring(lineCursorIndex));
							Editor.Document.CommitLineUpdate (line);
							return false;
						}
					}
				} else {
					switch(key)
					{
						case Gdk.Key.Return:
							// Calculate additional indentation, if any.
							char finalChar = '\0';
							char nextChar = '\0';
							string indent = String.Empty;
							if (!String.IsNullOrEmpty (Editor.SelectedText)) {
								int cursorPos = Editor.SelectionRange.Offset;
							
								Editor.DeleteSelectedText ();
								Editor.Caret.Offset = cursorPos;
								
								lineText = Editor.GetLineText (Editor.Caret.Line);
								lineCursorIndex = Editor.Caret.Column;
	//							System.Console.WriteLine(TextEditorData.Caret.Offset);
							}
							if(lineText.Length > 0)
							{
								if(lineCursorIndex > 0)
									finalChar = lineText[Math.Min(lineCursorIndex, lineText.Length) - 1];
								
								if(lineCursorIndex < lineText.Length)
									nextChar = lineText[lineCursorIndex];
	
								if(finalChar == '{')
									indent = TextEditorProperties.IndentString;
							}
	
							// If the next character is an closing brace, indent it appropriately.
							if(IsBrace(nextChar) && !IsOpenBrace(nextChar))
							{
								int openingLine;
								if(GetClosingBraceForLine (Editor, line, out openingLine) >= 0)
								{
									Editor.InsertAtCaret (Editor.EolMarker + GetIndent(Editor, openingLine, 0));
									return false;
								}
							}
						
							// Default indentation method
							Editor.InsertAtCaret (Editor.EolMarker + indent + GetIndent(Editor, Editor.Document.OffsetToLineNumber (line.Offset), lineCursorIndex));
							
							return false;
						
					}
				}
			}
			
			return base.KeyPress (key, keyChar, modifier);
		}
		
		public override ICompletionDataList HandleCodeCompletion (
		    CodeCompletionContext completionContext, char completionChar)
		{
			string lineText = Editor.GetLineText (completionContext.TriggerLine).TrimEnd();
			
			// If the line ends with a matched extension, invoke its handler
			foreach (KeyValuePair<string, GetMembersForExtension> pair in completionExtensions) {
				if (lineText.EndsWith(pair.Key)) {
					lineText = lineText.Substring (0, lineText.Length - pair.Key.Length);
					
					int nameStart = lineText.LastIndexOfAny (allowedCharsMinusColon) + 1;
					string itemName = lineText.Substring (nameStart).Trim ();
					
					if (string.IsNullOrEmpty (itemName))
						return null;
					
					return pair.Value (this, pair.Key, itemName);
				}
			}
			
			return null;
		}
		
		public override ICompletionDataList CodeCompletionCommand (
		    CodeCompletionContext completionContext)
		{
			int pos = completionContext.TriggerOffset;
			string lineText = Editor.GetLineText (Editor.Caret.Line).Trim();
			
			foreach (KeyValuePair<string, GetMembersForExtension> pair in completionExtensions) {
				if(lineText.EndsWith(pair.Key)) {
					return HandleCodeCompletion (completionContext, Editor.GetCharAt (pos));
				}
			}

			return GlobalComplete ();
		}
		
		/// <summary>
		/// Gets contained members for a namespace or class
		/// </summary>
		/// <param name="self">
		/// The current CTextEditorExtension
		/// <see cref="CTextEditorExtension"/>
		/// </param>
		/// <param name="completionExtension">
		/// The extension that triggered the completion 
		/// (e.g. "::")
		/// <see cref="System.String"/>
		/// </param>
		/// <param name="completionText">
		/// The identifier that triggered the completion
		/// (e.g. "Foo::")
		/// <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// Completion data for the namespace or class
		/// <see cref="CompletionDataList"/>
		/// </returns>
		private static CompletionDataList GetItemMembers (CTextEditorExtension self, string completionExtension, string completionText) {
			return self.GetMembersOfItem (completionText);
		}
		
		/// <summary>
		/// Gets contained members for an instance
		/// </summary>
		/// <param name="self">
		/// The current CTextEditorExtension
		/// <see cref="CTextEditorExtension"/>
		/// </param>
		/// <param name="completionExtension">
		/// The extension that triggered the completion 
		/// (e.g. "->")
		/// <see cref="System.String"/>
		/// </param>
		/// <param name="completionText">
		/// The identifier that triggered the completion
		/// (e.g. "blah->")
		/// <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// Completion data for the instance
		/// <see cref="CompletionDataList"/>
		/// </returns>
		private static CompletionDataList GetInstanceMembers (CTextEditorExtension self, string completionExtension, string completionText) {
			return self.GetMembersOfInstance (completionText, ("->" == completionExtension));
		}
		
		private CompletionDataList GetMembersOfItem (string itemFullName)
		{
			CProject project = Document.Project as CProject;
			
			if (project == null)
				return null;
				
			ProjectInformation info = ProjectInformationManager.Instance.Get (project);
			CompletionDataList list = new CompletionDataList ();
			
			LanguageItem container = null;
			
			string currentFileName = Document.FileName;
			bool in_project = false;
				
			foreach (LanguageItem li in info.Containers ()) {
				if (itemFullName == li.FullName) {
					container = li;
					in_project = true;
				}
			}
			
			if (!in_project && info.IncludedFiles.ContainsKey (currentFileName)) {
				foreach (CBinding.Parser.FileInformation fi in info.IncludedFiles[currentFileName]) {
					foreach (LanguageItem li in fi.Containers ()) {
						if (itemFullName == li.FullName)
							container = li;
					}
				}
			}
			
			if (container == null)
				return null;
			
			if (in_project) {
				AddItemsWithParent (list, info.AllItems (), container);
			} else {
				foreach (CBinding.Parser.FileInformation fi in info.IncludedFiles[currentFileName]) {
					AddItemsWithParent (list, fi.AllItems (), container);
				}
			}
			
			return list;
		}
		
		/// <summary>
		/// Adds completion data for children to a list
		/// </summary>
		/// <param name="list">
		/// The list to which completion data will be added
		/// <see cref="CompletionDataList"/>
		/// </param>
		/// <param name="items">
		/// A list of items to search
		/// <see cref="IEnumerable"/>
		/// </param>
		/// <param name="parent">
		/// The parent that will be matched
		/// </param>
		public static void AddItemsWithParent(CompletionDataList list, IEnumerable<LanguageItem> items, LanguageItem parent) {
			foreach (LanguageItem li in items) {
				if (li.Parent != null && li.Parent.Equals (parent))
					list.Add (new CompletionData (li));
			}
		}

		
		/// <summary>
		/// Gets completion data for a given instance
		/// </summary>
		/// <param name="instanceName">
		/// The identifier of the instance
		/// <see cref="System.String"/>
		/// </param>
		/// <param name="isPointer">
		/// Whether the instance in question is a pointer
		/// <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// Completion data for the instance
		/// <see cref="CompletionDataList"/>
		/// </returns>
		private CompletionDataList GetMembersOfInstance (string instanceName, bool isPointer)
		{
			CProject project = Document.Project as CProject;
			
			if (project == null)
				return null;
				
			ProjectInformation info = ProjectInformationManager.Instance.Get (project);
			CompletionDataList list = new CompletionDataList ();
			
			string container = null;
			
			string currentFileName = Document.FileName;
			bool in_project = false;
			
			// Find the typename of the instance
			foreach (Member li in info.Members ) {
				if (instanceName == li.Name && li.IsPointer == isPointer) {
					container = li.InstanceType;
					in_project = true;
					break;
				}
			}
			
			// Search included files
			if (!in_project && info.IncludedFiles.ContainsKey (currentFileName)) {
				foreach (CBinding.Parser.FileInformation fi in info.IncludedFiles[currentFileName]) {
					foreach (Member li in fi.Members) {
						if (instanceName == li.Name && li.IsPointer == isPointer) {
							container = li.InstanceType;
							break;
						}
					}
				}
			}
			
			if (null == container) {
				// Search locals
				foreach (Local li in info.Locals ) {
					if (instanceName == li.Name && li.IsPointer == isPointer && currentFileName == li.File) {
						container = li.InstanceType;
						in_project = true;
						break;
					}
				}
			}
			
			// Not found
			if (container == null)
				return null;
			
			// Get the LanguageItem corresponding to the typename 
			// and populate completion data accordingly
			if (in_project) {
				AddMembersWithParent (list, info.InstanceMembers (), container);
			} else {
				foreach (CBinding.Parser.FileInformation fi in info.IncludedFiles[currentFileName]) {
					AddMembersWithParent (list, fi.InstanceMembers (), container);
				}
			}
			
			return list;
		}
		
		/// <summary>
		/// Adds completion data for children to a list
		/// </summary>
		/// <param name="list">
		/// The list to which completion data will be added
		/// <see cref="CompletionDataList"/>
		/// </param>
		/// <param name="items">
		/// A list of items to search
		/// <see cref="IEnumerable"/>
		/// </param>
		/// <param name="parentName">
		/// The name of the parent that will be matched
		/// <see cref="System.String"/>
		/// </param>
		public static void AddMembersWithParent(CompletionDataList list, IEnumerable<LanguageItem> items, string parentName) {
				foreach (LanguageItem li in items) {
					if (li.Parent != null && li.Parent.Name.EndsWith (parentName))
						list.Add (new CompletionData (li));
				}
		}

		private ICompletionDataList GlobalComplete ()
		{
			CProject project = Document.Project as CProject;
			
			if (project == null)
				return null;
			
			ProjectInformation info = ProjectInformationManager.Instance.Get (project);
			
			CompletionDataList list = new CompletionDataList ();
			
			foreach (LanguageItem li in info.Containers ())
				if (li.Parent == null)
					list.Add (new CompletionData (li));
			
			foreach (Function f in info.Functions)
				if (f.Parent == null)
					list.Add (new CompletionData (f));

			foreach (Enumerator e in info.Enumerators)
				list.Add (new CompletionData (e));
			
			foreach (Macro m in info.Macros)
				list.Add (new CompletionData (m));
			
			string currentFileName = Document.FileName;
			
			if (info.IncludedFiles.ContainsKey (currentFileName)) {
				foreach (CBinding.Parser.FileInformation fi in info.IncludedFiles[currentFileName]) {
					foreach (LanguageItem li in fi.Containers ())
						if (li.Parent == null)
							list.Add (new CompletionData (li));
					
					foreach (Function f in fi.Functions)
						if (f.Parent == null)
							list.Add (new CompletionData (f));

					foreach (Enumerator e in fi.Enumerators)
						list.Add (new CompletionData (e));
					
					foreach (Macro m in fi.Macros)
						list.Add (new CompletionData (m));
				}
			}
			
			return list;
		}
		
		public override  IParameterDataProvider HandleParameterCompletion (
		    CodeCompletionContext completionContext, char completionChar)
		{
			if (completionChar != '(')
				return null;
			
			CProject project = Document.Project as CProject;
			
			if (project == null)
				return null;
			
			ProjectInformation info = ProjectInformationManager.Instance.Get (project);
			string lineText = Editor.GetLineText (Editor.Caret.Line).TrimEnd ();
			
			int nameStart = lineText.LastIndexOfAny (allowedChars);

			nameStart++;
			
			string functionName = lineText.Substring (nameStart).Trim ();
			
			if (string.IsNullOrEmpty (functionName))
				return null;
			
			return new ParameterDataProvider (Document, info, functionName);
		}
		
		private bool AllWhiteSpace (string lineText)
		{
			// We will almost definately need a faster method than this
			foreach (char c in lineText)
				if (!char.IsWhiteSpace (c))
					return false;
			
			return true;
		}
		
		// Snatched from DefaultFormattingStrategy
		private string GetIndent (TextEditorData d, int lineNumber, int terminateIndex)
		{
			string lineText = d.GetLineText (lineNumber);
			if(terminateIndex > 0)
				lineText = terminateIndex < lineText.Length ? lineText.Substring(0, terminateIndex) : lineText;
			
			StringBuilder whitespaces = new StringBuilder ();
			
			foreach (char ch in lineText) {
				if (!char.IsWhiteSpace (ch))
					break;
				whitespaces.Append (ch);
			}
			
			return whitespaces.ToString ();
		}
		
		[CommandHandler (MonoDevelop.DesignerSupport.Commands.SwitchBetweenRelatedFiles)]
		protected void Run ()
		{
			var cp = this.Document.Project as CProject;
			if (cp != null) {
				string match = cp.MatchingFile (this.Document.FileName);
				if (match != null)
					MonoDevelop.Ide.IdeApp.Workbench.OpenDocument (match, true);
			}
		}
		
		[CommandUpdateHandler (MonoDevelop.DesignerSupport.Commands.SwitchBetweenRelatedFiles)]
		protected void Update (CommandInfo info)
		{
			var cp = this.Document.Project as CProject;
			info.Visible = info.Visible = cp != null && cp.MatchingFile (this.Document.FileName) != null;
		}
	}
}

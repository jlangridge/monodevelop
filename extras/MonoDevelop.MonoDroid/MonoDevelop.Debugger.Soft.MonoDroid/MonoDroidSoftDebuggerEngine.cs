// 
// MonoDroidSoftDebuggerEngine.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2010 Novell, Inc. (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using MonoDevelop.Debugger;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.MonoDroid;
using Mono.Debugging.Client;
using MonoDevelop.Debugger.Soft;
using System.Net;

namespace MonoDevelop.Debugger.Soft.MonoDroid
{
	public class MonoDroidSoftDebuggerEngine: IDebuggerEngine
	{
		
		public bool CanDebugCommand (ExecutionCommand command)
		{
			var cmd = command as MonoDroidExecutionCommand;
			return cmd != null && cmd.DebugMode;
		}
		
		public DebuggerStartInfo CreateDebuggerStartInfo (ExecutionCommand command)
		{
			var cmd = (MonoDroidExecutionCommand) command;
			
			//var startInfo = new MonoDroidDebuggerStartInfo (MonoDroidSettings.GetDebuggerHostIP (false), cmd);
			var startInfo = new MonoDroidDebuggerStartInfo (IPAddress.Loopback, cmd);
			startInfo.SetUserAssemblies (cmd.UserAssemblyPaths);
			return startInfo;
		}

		public DebuggerSession CreateSession ()
		{
			return new MonoDroidDebuggerSession ();
		}
		
		public ProcessInfo[] GetAttachableProcesses ()
		{
			return new ProcessInfo[0];
		}
	}
}

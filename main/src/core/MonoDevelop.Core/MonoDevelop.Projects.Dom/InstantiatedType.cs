//
// InstantiatedType.cs
//
// Author:
//   Mike Krüger <mkrueger@novell.com>
//
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
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
using System.Linq;
using System.Collections.Generic;

namespace MonoDevelop.Projects.Dom
{
	public class InstantiatedType : DomType, IInstantiatedType
	{
		IList<IReturnType> genericParameters = null;
		static readonly IList<IReturnType> emptyList = new IReturnType[0];

		public override TypeKind Kind {
			get {
				return TypeKind.GenericInstantiation;
			}
		}

		public IList<IReturnType> GenericParameters {
			get {
				return genericParameters ?? emptyList;
			}
			set {
				genericParameters = value;
			}
		}
		
		public IType UninstantiatedType {
			get;
			set;
		}
		
		public override string HelpUrl {
			get {
				IType type = UninstantiatedType;
				if (type != null)
					return type.HelpUrl;
				return base.HelpUrl;
			}
		}
		
		public InstantiatedType()
		{
		}
		
		public InstantiatedType (IType uninstantiatedType, IEnumerable<IReturnType> genericParameters)
		{
			this.UninstantiatedType = uninstantiatedType;
			this.GenericParameters  = new List<IReturnType> (genericParameters);
		}
		
		public override string ToString ()
		{
			return string.Format ("[InstantiatedType: UninstantiatedType={1}, GenericParameters={0}]", string.Join (";", new List<string> (genericParameters.Select (gp => gp.ToString ())).ToArray ()), UninstantiatedType);
		}
	}
}
﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Dom;
using Rhino.Mocks;

namespace PackageManagement.Tests.Helpers
{
	public class FieldHelper
	{
		public IField Field;
		public ProjectContentHelper ProjectContentHelper = new ProjectContentHelper();
		
		/// <summary>
		/// Field name should include the class prefix (e.g. "Class1.MyField")
		/// </summary>
		public void CreateField(string fullyQualifiedName)
		{
			Field = MockRepository.GenerateMock<IField, IEntity>();
			Field.Stub(f => f.ProjectContent).Return(ProjectContentHelper.FakeProjectContent);
			Field.Stub(f => f.FullyQualifiedName).Return(fullyQualifiedName);
		}
		
		public void CreatePublicField(string fullyQualifiedName)
		{
			CreateField(fullyQualifiedName);
			Field.Stub(f => f.IsPublic).Return(true);
		}
		
		public void CreatePrivateField(string fullyQualifiedName)
		{
			CreateField(fullyQualifiedName);
			Field.Stub(f => f.IsPublic).Return(false);
			Field.Stub(f => f.IsPrivate).Return(true);
		}
	}
}

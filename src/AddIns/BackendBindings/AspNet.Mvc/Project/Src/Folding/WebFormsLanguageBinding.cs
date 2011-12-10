﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor;

namespace ICSharpCode.AspNet.Mvc.Folding
{
	public class WebFormsLanguageBinding : DefaultLanguageBinding
	{
		ITextEditorWithParseInformationFoldingFactory textEditorFactory;
		IWebFormsFoldGeneratorFactory foldGeneratorFactory;
		IFoldGenerator foldGenerator;
		
		public WebFormsLanguageBinding()
			: this(
				new TextEditorWithParseInformationFoldingFactory(),
				new WebFormsFoldGeneratorFactory())
		{
		}
		
		public WebFormsLanguageBinding(
			ITextEditorWithParseInformationFoldingFactory textEditorFactory,
			IWebFormsFoldGeneratorFactory foldGeneratorFactory)
		{
			this.textEditorFactory = textEditorFactory;
			this.foldGeneratorFactory = foldGeneratorFactory;
		}
		
		public override IFormattingStrategy FormattingStrategy {
			get { return new DefaultFormattingStrategy(); }
		}
		
		public override LanguageProperties Properties {
			get { return LanguageProperties.None; }
		}
		
		public override void Attach(ITextEditor editor)
		{
			Attach(textEditorFactory.CreateTextEditor(editor));
		}
		
		void Attach(ITextEditorWithParseInformationFolding editor)
		{
			foldGenerator = foldGeneratorFactory.CreateFoldGenerator(editor);
		}
		
		public override void Detach()
		{
			foldGenerator.Dispose();
		}
	}
}

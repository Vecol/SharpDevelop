// <file>
//     <copyright see="prj:///doc/copyright.txt">2002-2005 AlphaSierraPapa</copyright>
//     <license see="prj:///doc/license.txt">GNU General Public License</license>
//     <owner name="none" email=""/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Windows.Forms;

using ICSharpCode.SharpDevelop.Internal.ExternalTool;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.SharpDevelop.Gui.OptionPanels
{
	public class ReferencePaths : AbstractProjectOptionPanel
	{
		public override void LoadPanelContents()
		{
			InitializeHelper();
			
			StringListEditor editor = new StringListEditor();
			editor.BrowseForDirectory = true;
			editor.ListCaption = StringParser.Parse("&${res:Dialog.ProjectOptions.ReferencePaths}:");
			editor.TitleText = StringParser.Parse("&${res:Dialog.ExportProjectToHtml.FolderLabel}");
			editor.AddButtonText = StringParser.Parse("${res:Dialog.ProjectOptions.ReferencePaths.AddPath}");
			editor.ListChanged += delegate { IsDirty = true; };
			helper.AddBinding("ReferencePath", new SemicolonSeparatedStringListBinding(editor));
			this.Controls.Add(editor);
		}
		
		public class SemicolonSeparatedStringListBinding : ConfigurationGuiBinding
		{
			StringListEditor editor;
			
			public SemicolonSeparatedStringListBinding(StringListEditor editor)
			{
				this.editor = editor;
			}
			
			public override void Load()
			{
				string[] values = Get("").Split(';');
				if (values.Length == 1 && values[0].Length == 0) {
					editor.LoadList(new string[0]);
				} else {
					editor.LoadList(values);
				}
			}
			
			public override bool Save()
			{
				Set(string.Join(";", editor.GetList()));
				return true;
			}
		}
	}
}

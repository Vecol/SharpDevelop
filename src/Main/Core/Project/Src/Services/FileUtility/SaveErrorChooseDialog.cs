﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ICSharpCode.Core
{
	/// <summary>
	///     Add summary description for SaveErrorChooseDialog
	/// </summary>
	public class SaveErrorChooseDialog : System.Windows.Forms.Form 
	{
		Button  retryButton;
		Button  ignoreButton;
		Label   descriptionLabel;
		TextBox descriptionTextBox;
		Button  exceptionButton;
		Button  chooseLocationButton;
		
		string    displayMessage;
		Exception exceptionGot;
		
		public SaveErrorChooseDialog(string fileName, string message, string dialogName, Exception exceptionGot, bool chooseLocationEnabled)
		{
			this.Text = StringParser.Parse(dialogName);
			//  Must be called for initialization
			this.InitializeComponents(chooseLocationEnabled);
			RightToLeftConverter.ConvertRecursive(this);
			
			displayMessage = StringParser.Parse(message, new string[,] {
				{"FileName", fileName},
				{"Path",     Path.GetDirectoryName(fileName)},
				{"FileNameWithoutPath", Path.GetFileName(fileName)},
				{"Exception", exceptionGot.GetType().FullName},
			});
			
			descriptionTextBox.Lines = StringParser.Parse(this.displayMessage).Split('\n');
			
			this.exceptionGot = exceptionGot;
		}
		
		void ShowException(object sender, EventArgs e)
		{
			MessageService.ShowMessage(exceptionGot.ToString(), StringParser.Parse("${res:ICSharpCode.Core.Services.ErrorDialogs.ExceptionGotDescription}"));
		}
		
		/// <summary>
		///     This method was autogenerated - do not change the contents manually
		/// </summary>
		private void InitializeComponents(bool chooseLocationEnabled) 
		{
			//
			//  Set up generated class SaveErrorChooseDialog
			// 
			this.ClientSize = new Size(508, 320);
			this.SuspendLayout();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveErrorChooseDialog";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterScreen;

			// 
			//  Set up member descriptionLabel
			// 
			this.descriptionLabel = new Label();
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Location = new Point(8, 8);
			this.descriptionLabel.Size = new Size(584, 24);
			this.descriptionLabel.TabIndex = 3;
			this.descriptionLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top 
						| (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
			this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.descriptionLabel.Text = StringParser.Parse("${res:ICSharpCode.Core.Services.ErrorDialogs.DescriptionLabel}");
			this.Controls.Add(descriptionLabel);
			
			// 
			//  Set up member descriptionTextBox
			// 
			this.descriptionTextBox = new TextBox();
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Size = new Size(584, 237);
			this.descriptionTextBox.Location = new Point(8, 40);
			this.descriptionTextBox.TabIndex = 2;
			this.descriptionTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top 
						| (System.Windows.Forms.AnchorStyles.Bottom 
						| (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.ReadOnly = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.Controls.Add(descriptionTextBox);
			
			// 
			//  Set up member retryButton
			// 
			this.retryButton = new Button();
			this.retryButton.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.retryButton.Name = "retryButton";
			this.retryButton.TabIndex = 5;
			this.retryButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.retryButton.Text = StringParser.Parse("${res:Global.RetryButtonText}");
			this.retryButton.Size = new Size(110, 27);
			this.retryButton.Location = new Point(28, 285);
			this.Controls.Add(retryButton);
			
			// 
			//  Set up member ignoreButton
			// 
			this.ignoreButton = new Button();
			this.ignoreButton.Name = "ignoreButton";
			this.ignoreButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.ignoreButton.TabIndex = 4;
			this.ignoreButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ignoreButton.Text = StringParser.Parse("${res:Global.IgnoreButtonText}");
			this.ignoreButton.Size = new Size(110, 27);
			this.ignoreButton.Location = new Point(146, 285);
			this.Controls.Add(ignoreButton);
			
			
			// 
			//  Set up member exceptionButton
			// 
			this.exceptionButton = new Button();
			this.exceptionButton.TabIndex = 1;
			this.exceptionButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.exceptionButton.Name = "exceptionButton";
			this.exceptionButton.Text = ResourceService.GetString("ICSharpCode.Core.Services.ErrorDialogs.ShowExceptionButton");
			this.exceptionButton.Size = new Size(110, 27);
			this.exceptionButton.Location = new Point(382, 285);
			this.exceptionButton.Click += new EventHandler(ShowException);
			this.Controls.Add(exceptionButton);
			
			if (chooseLocationEnabled) {
				// 
				//  Set up member chooseLocationButton
				// 
				this.chooseLocationButton = new Button();
				this.chooseLocationButton.Name = "chooseLocationButton";
				this.chooseLocationButton.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.chooseLocationButton.TabIndex = 0;
				this.chooseLocationButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
				this.chooseLocationButton.Text = ResourceService.GetString("Global.ChooseLocationButtonText");
				this.chooseLocationButton.Size = new Size(110, 27);
				this.chooseLocationButton.Location = new Point(264, 285);
			}
			
			this.Controls.Add(chooseLocationButton);
			
			this.ResumeLayout(false);
			this.Size = new Size(526, 262);
		}
	}
}

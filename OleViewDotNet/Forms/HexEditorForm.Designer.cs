﻿namespace OleViewDotNet.Forms
{
    partial class HexEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnOK;
            System.Windows.Forms.Button btnCancel;
            this.hexEditorControl = new OleViewDotNet.Forms.HexEditorControl();
            btnOK = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hexEditorControl
            // 
            this.hexEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditorControl.Bytes = new byte[0];
            this.hexEditorControl.Location = new System.Drawing.Point(-2, 1);
            this.hexEditorControl.Name = "hexEditorControl";
            this.hexEditorControl.ReadOnly = false;
            this.hexEditorControl.Size = new System.Drawing.Size(801, 411);
            this.hexEditorControl.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Location = new System.Drawing.Point(297, 418);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(428, 418);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // HexEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnOK);
            this.Controls.Add(this.hexEditorControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HexEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Hex Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private HexEditorControl hexEditorControl;
    }
}
namespace EmbedImage
{
    partial class MyImages
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemLoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabelEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripLabelTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuEmbedMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemGetMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabelHelp = new System.Windows.Forms.ToolStripLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelFile,
            this.toolStripLabelEdit,
            this.toolStripLabelTools,
            this.toolStripLabelHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelFile
            // 
            this.toolStripLabelFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLoadImage});
            this.toolStripLabelFile.Name = "toolStripLabelFile";
            this.toolStripLabelFile.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabelFile.Text = "File";
            // 
            // toolStripMenuItemLoadImage
            // 
            this.toolStripMenuItemLoadImage.Name = "toolStripMenuItemLoadImage";
            this.toolStripMenuItemLoadImage.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemLoadImage.Text = "Load Image";
            this.toolStripMenuItemLoadImage.Click += new System.EventHandler(this.toolStripMenuItemLoadImage_Click);
            // 
            // toolStripLabelEdit
            // 
            this.toolStripLabelEdit.Name = "toolStripLabelEdit";
            this.toolStripLabelEdit.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabelEdit.Text = "Edit";
            // 
            // toolStripLabelTools
            // 
            this.toolStripLabelTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuEmbedMessage,
            this.toolStripMenuItemGetMessage});
            this.toolStripLabelTools.Name = "toolStripLabelTools";
            this.toolStripLabelTools.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabelTools.Text = "Tools";
            // 
            // toolStripMenuEmbedMessage
            // 
            this.toolStripMenuEmbedMessage.Name = "toolStripMenuEmbedMessage";
            this.toolStripMenuEmbedMessage.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuEmbedMessage.Text = "Embed Message";
            this.toolStripMenuEmbedMessage.Click += new System.EventHandler(this.toolStripMenuEmbedMessage_Click);
            // 
            // toolStripMenuItemGetMessage
            // 
            this.toolStripMenuItemGetMessage.Name = "toolStripMenuItemGetMessage";
            this.toolStripMenuItemGetMessage.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemGetMessage.Text = "Get Message";
            this.toolStripMenuItemGetMessage.Click += new System.EventHandler(this.toolStripMenuItemGetMessage_Click);
            // 
            // toolStripLabelHelp
            // 
            this.toolStripLabelHelp.Name = "toolStripLabelHelp";
            this.toolStripLabelHelp.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabelHelp.Text = "Help";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Location = new System.Drawing.Point(228, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 2;
            // 
            // MyImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 583);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MyImages";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyImages_Paint);
            //this.Resize += new System.EventHandler(this.MyImages_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabelFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadImage;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabelEdit;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabelTools;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuEmbedMessage;
        private System.Windows.Forms.ToolStripLabel toolStripLabelHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGetMessage;
        private System.Windows.Forms.TextBox textBox1;

    }
}


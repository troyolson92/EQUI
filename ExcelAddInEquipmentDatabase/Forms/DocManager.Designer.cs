namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class DocManager
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
            this.btn_find = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tb_InFile = new System.Windows.Forms.TextBox();
            this.tb_inIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_find
            // 
            this.btn_find.Location = new System.Drawing.Point(422, 12);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(75, 23);
            this.btn_find.TabIndex = 0;
            this.btn_find.Text = "Find";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 90);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(485, 342);
            this.listBox1.TabIndex = 1;
            // 
            // tb_InFile
            // 
            this.tb_InFile.Location = new System.Drawing.Point(84, 18);
            this.tb_InFile.Name = "tb_InFile";
            this.tb_InFile.Size = new System.Drawing.Size(100, 20);
            this.tb_InFile.TabIndex = 2;
            this.tb_InFile.Text = "HSe";
            // 
            // tb_inIndex
            // 
            this.tb_inIndex.Location = new System.Drawing.Point(84, 49);
            this.tb_inIndex.Name = "tb_inIndex";
            this.tb_inIndex.Size = new System.Drawing.Size(100, 20);
            this.tb_inIndex.TabIndex = 3;
            this.tb_inIndex.Text = "Instructions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FindInFiles:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "FindInIndex:";
            // 
            // DocManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_inIndex);
            this.Controls.Add(this.tb_InFile);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_find);
            this.Name = "DocManager";
            this.Text = "DocManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_find;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox tb_InFile;
        private System.Windows.Forms.TextBox tb_inIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
namespace EQUIToolsLib
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
            this.tb_inIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_result = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.cb_path = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_find
            // 
            this.btn_find.Location = new System.Drawing.Point(563, 53);
            this.btn_find.Margin = new System.Windows.Forms.Padding(4);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(100, 28);
            this.btn_find.TabIndex = 0;
            this.btn_find.Text = "Find";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // tb_inIndex
            // 
            this.tb_inIndex.Location = new System.Drawing.Point(145, 61);
            this.tb_inIndex.Margin = new System.Windows.Forms.Padding(4);
            this.tb_inIndex.Name = "tb_inIndex";
            this.tb_inIndex.Size = new System.Drawing.Size(222, 22);
            this.tb_inIndex.TabIndex = 3;
            this.tb_inIndex.Text = "Gripp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "PathnameContains:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "IndexContains:";
            // 
            // lv_result
            // 
            this.lv_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_result.FullRowSelect = true;
            this.lv_result.Location = new System.Drawing.Point(20, 100);
            this.lv_result.MultiSelect = false;
            this.lv_result.Name = "lv_result";
            this.lv_result.Size = new System.Drawing.Size(643, 432);
            this.lv_result.TabIndex = 6;
            this.lv_result.UseCompatibleStateImageBehavior = false;
            this.lv_result.View = System.Windows.Forms.View.Details;
            this.lv_result.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_result_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "ProcessInfo:";
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(482, 28);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(46, 17);
            this.lbl_info.TabIndex = 8;
            this.lbl_info.Text = "label4";
            // 
            // cb_path
            // 
            this.cb_path.FormattingEnabled = true;
            this.cb_path.Location = new System.Drawing.Point(145, 23);
            this.cb_path.Name = "cb_path";
            this.cb_path.Size = new System.Drawing.Size(222, 24);
            this.cb_path.TabIndex = 9;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // DocManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 544);
            this.Controls.Add(this.cb_path);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lv_result);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_inIndex);
            this.Controls.Add(this.btn_find);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocManager";
            this.Text = "DocManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_find;
        private System.Windows.Forms.TextBox tb_inIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_result;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.ComboBox cb_path;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
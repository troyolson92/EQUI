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
            this.tb_InFile = new System.Windows.Forms.TextBox();
            this.tb_inIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_result = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btn_find
            // 
            this.btn_find.Location = new System.Drawing.Point(563, 15);
            this.btn_find.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(100, 28);
            this.btn_find.TabIndex = 0;
            this.btn_find.Text = "Find";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_InFile
            // 
            this.tb_InFile.Location = new System.Drawing.Point(112, 22);
            this.tb_InFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_InFile.Name = "tb_InFile";
            this.tb_InFile.Size = new System.Drawing.Size(132, 22);
            this.tb_InFile.TabIndex = 2;
            this.tb_InFile.Text = "HSe";
            // 
            // tb_inIndex
            // 
            this.tb_inIndex.Location = new System.Drawing.Point(112, 60);
            this.tb_inIndex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_inIndex.Name = "tb_inIndex";
            this.tb_inIndex.Size = new System.Drawing.Size(132, 22);
            this.tb_inIndex.TabIndex = 3;
            this.tb_inIndex.Text = "SpotL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "FindInFiles:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "FindInIndex:";
            // 
            // lv_result
            // 
            this.lv_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_result.Location = new System.Drawing.Point(20, 100);
            this.lv_result.Name = "lv_result";
            this.lv_result.Size = new System.Drawing.Size(643, 432);
            this.lv_result.TabIndex = 6;
            this.lv_result.UseCompatibleStateImageBehavior = false;
            this.lv_result.View = System.Windows.Forms.View.Details;
            // 
            // DocManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 544);
            this.Controls.Add(this.lv_result);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_inIndex);
            this.Controls.Add(this.tb_InFile);
            this.Controls.Add(this.btn_find);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DocManager";
            this.Text = "DocManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_find;
        private System.Windows.Forms.TextBox tb_InFile;
        private System.Windows.Forms.TextBox tb_inIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_result;
    }
}
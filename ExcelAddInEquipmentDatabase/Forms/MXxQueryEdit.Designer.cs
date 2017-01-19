namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class MXxQueryEdit
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
            this.rtb_Query = new System.Windows.Forms.RichTextBox();
            this.tb_QueryName = new System.Windows.Forms.TextBox();
            this.tb_QueryDiscription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_Query
            // 
            this.rtb_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Query.Location = new System.Drawing.Point(12, 62);
            this.rtb_Query.Name = "rtb_Query";
            this.rtb_Query.Size = new System.Drawing.Size(448, 403);
            this.rtb_Query.TabIndex = 0;
            this.rtb_Query.Text = "";
            // 
            // tb_QueryName
            // 
            this.tb_QueryName.Location = new System.Drawing.Point(82, 13);
            this.tb_QueryName.Name = "tb_QueryName";
            this.tb_QueryName.Size = new System.Drawing.Size(142, 20);
            this.tb_QueryName.TabIndex = 1;
            // 
            // tb_QueryDiscription
            // 
            this.tb_QueryDiscription.Location = new System.Drawing.Point(82, 39);
            this.tb_QueryDiscription.Name = "tb_QueryDiscription";
            this.tb_QueryDiscription.Size = new System.Drawing.Size(353, 20);
            this.tb_QueryDiscription.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Queryname:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Discription:";
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(385, 472);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_delete.Location = new System.Drawing.Point(12, 472);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // MXxQueryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 507);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_QueryDiscription);
            this.Controls.Add(this.tb_QueryName);
            this.Controls.Add(this.rtb_Query);
            this.MinimumSize = new System.Drawing.Size(488, 545);
            this.Name = "MXxQueryEdit";
            this.Text = "MXxQueryEdit";
            this.Load += new System.EventHandler(this.MXxQueryEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Query;
        private System.Windows.Forms.TextBox tb_QueryName;
        private System.Windows.Forms.TextBox tb_QueryDiscription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_delete;
    }
}
namespace ThucHanh5
{
    partial class FormDataListKhachHang
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
            this.lstKhachHang = new System.Windows.Forms.ListBox();
            this.btnXemTT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstKhachHang
            // 
            this.lstKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstKhachHang.FormattingEnabled = true;
            this.lstKhachHang.ItemHeight = 20;
            this.lstKhachHang.Location = new System.Drawing.Point(157, 51);
            this.lstKhachHang.Name = "lstKhachHang";
            this.lstKhachHang.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstKhachHang.Size = new System.Drawing.Size(483, 184);
            this.lstKhachHang.TabIndex = 1;
            this.lstKhachHang.SelectedIndexChanged += new System.EventHandler(this.lstKhachHang_SelectedIndexChanged);
            // 
            // btnXemTT
            // 
            this.btnXemTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTT.Location = new System.Drawing.Point(469, 324);
            this.btnXemTT.Name = "btnXemTT";
            this.btnXemTT.Size = new System.Drawing.Size(231, 43);
            this.btnXemTT.TabIndex = 2;
            this.btnXemTT.Text = "Xem thông tin";
            this.btnXemTT.UseVisualStyleBackColor = true;
            this.btnXemTT.Click += new System.EventHandler(this.btnXemTT_Click);
            // 
            // FormDataListKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXemTT);
            this.Controls.Add(this.lstKhachHang);
            this.Name = "FormDataListKhachHang";
            this.Text = "FormDataListKhachHang";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstKhachHang;
        private System.Windows.Forms.Button btnXemTT;
    }
}
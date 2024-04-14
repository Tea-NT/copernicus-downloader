
namespace copernicus_download
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtAuth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLyer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLatLng = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.txtDeltaRange = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnInsertDb = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.txtQuadKey = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Authorization";
            // 
            // txtAuth
            // 
            this.txtAuth.Location = new System.Drawing.Point(15, 29);
            this.txtAuth.Multiline = true;
            this.txtAuth.Name = "txtAuth";
            this.txtAuth.Size = new System.Drawing.Size(688, 306);
            this.txtAuth.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bbox";
            // 
            // txtBbox
            // 
            this.txtBbox.Location = new System.Drawing.Point(12, 445);
            this.txtBbox.Name = "txtBbox";
            this.txtBbox.Size = new System.Drawing.Size(691, 21);
            this.txtBbox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(709, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "download";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLyer
            // 
            this.txtLyer.Location = new System.Drawing.Point(12, 406);
            this.txtLyer.Name = "txtLyer";
            this.txtLyer.Size = new System.Drawing.Size(124, 21);
            this.txtLyer.TabIndex = 6;
            this.txtLyer.Text = "14";
            this.txtLyer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLyer_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 391);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lyer";
            // 
            // txtLatLng
            // 
            this.txtLatLng.Location = new System.Drawing.Point(12, 367);
            this.txtLatLng.Name = "txtLatLng";
            this.txtLatLng.Size = new System.Drawing.Size(691, 21);
            this.txtLatLng.TabIndex = 8;
            this.txtLatLng.Text = "39.91546,116.39186";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "lat_lng";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(628, 406);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 9;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // txtDeltaRange
            // 
            this.txtDeltaRange.Location = new System.Drawing.Point(161, 406);
            this.txtDeltaRange.Name = "txtDeltaRange";
            this.txtDeltaRange.Size = new System.Drawing.Size(124, 21);
            this.txtDeltaRange.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Delta Range";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(709, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "download all";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(709, 144);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "download test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnInsertDb
            // 
            this.btnInsertDb.Location = new System.Drawing.Point(709, 203);
            this.btnInsertDb.Name = "btnInsertDb";
            this.btnInsertDb.Size = new System.Drawing.Size(114, 23);
            this.btnInsertDb.TabIndex = 14;
            this.btnInsertDb.Text = "Insert database";
            this.btnInsertDb.UseVisualStyleBackColor = true;
            this.btnInsertDb.Click += new System.EventHandler(this.button4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(709, 312);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Select database";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(709, 232);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Insert Z/X/Y";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(709, 261);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(114, 21);
            this.txtPos.TabIndex = 17;
            this.txtPos.Text = "2,0,0";
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(709, 341);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(128, 128);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 18;
            this.picBox.TabStop = false;
            // 
            // txtQuadKey
            // 
            this.txtQuadKey.Location = new System.Drawing.Point(709, 288);
            this.txtQuadKey.Name = "txtQuadKey";
            this.txtQuadKey.Size = new System.Drawing.Size(114, 21);
            this.txtQuadKey.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 493);
            this.Controls.Add(this.txtQuadKey);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.txtPos);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnInsertDb);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDeltaRange);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtLatLng);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLyer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAuth);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLyer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLatLng;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox txtDeltaRange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnInsertDb;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtPos;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.TextBox txtQuadKey;
    }
}


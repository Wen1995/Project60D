﻿namespace xls2lua
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_inputPath = new System.Windows.Forms.TextBox();
            this.m_inputOutPath = new System.Windows.Forms.TextBox();
            this.m_btnBrowse1 = new System.Windows.Forms.Button();
            this.m_btnBrowse2 = new System.Windows.Forms.Button();
            this.m_btnCovert = new System.Windows.Forms.Button();
            this.m_txStatus = new System.Windows.Forms.Label();
            this.m_btnToCs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "xls路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "lua输出路径:";
            // 
            // m_inputPath
            // 
            this.m_inputPath.Location = new System.Drawing.Point(96, 21);
            this.m_inputPath.Name = "m_inputPath";
            this.m_inputPath.Size = new System.Drawing.Size(433, 21);
            this.m_inputPath.TabIndex = 2;
            // 
            // m_inputOutPath
            // 
            this.m_inputOutPath.Location = new System.Drawing.Point(95, 63);
            this.m_inputOutPath.Name = "m_inputOutPath";
            this.m_inputOutPath.Size = new System.Drawing.Size(433, 21);
            this.m_inputOutPath.TabIndex = 3;
            // 
            // m_btnBrowse1
            // 
            this.m_btnBrowse1.Location = new System.Drawing.Point(544, 12);
            this.m_btnBrowse1.Name = "m_btnBrowse1";
            this.m_btnBrowse1.Size = new System.Drawing.Size(46, 29);
            this.m_btnBrowse1.TabIndex = 4;
            this.m_btnBrowse1.Text = "...";
            this.m_btnBrowse1.UseVisualStyleBackColor = true;
            this.m_btnBrowse1.Click += new System.EventHandler(this.m_btnBrowse1_Click);
            // 
            // m_btnBrowse2
            // 
            this.m_btnBrowse2.Location = new System.Drawing.Point(544, 59);
            this.m_btnBrowse2.Name = "m_btnBrowse2";
            this.m_btnBrowse2.Size = new System.Drawing.Size(46, 29);
            this.m_btnBrowse2.TabIndex = 5;
            this.m_btnBrowse2.Text = "...";
            this.m_btnBrowse2.UseVisualStyleBackColor = true;
            this.m_btnBrowse2.Click += new System.EventHandler(this.m_btnBrowse2_Click);
            // 
            // m_btnCovert
            // 
            this.m_btnCovert.Location = new System.Drawing.Point(499, 109);
            this.m_btnCovert.Name = "m_btnCovert";
            this.m_btnCovert.Size = new System.Drawing.Size(91, 29);
            this.m_btnCovert.TabIndex = 6;
            this.m_btnCovert.Text = "to lua";
            this.m_btnCovert.UseVisualStyleBackColor = true;
            this.m_btnCovert.Click += new System.EventHandler(this.m_btnCovert_Click);
            // 
            // m_txStatus
            // 
            this.m_txStatus.AutoSize = true;
            this.m_txStatus.Location = new System.Drawing.Point(21, 126);
            this.m_txStatus.Name = "m_txStatus";
            this.m_txStatus.Size = new System.Drawing.Size(0, 12);
            this.m_txStatus.TabIndex = 7;
            // 
            // m_btnToCs
            // 
            this.m_btnToCs.Location = new System.Drawing.Point(499, 150);
            this.m_btnToCs.Name = "m_btnToCs";
            this.m_btnToCs.Size = new System.Drawing.Size(91, 29);
            this.m_btnToCs.TabIndex = 8;
            this.m_btnToCs.Text = "to cs";
            this.m_btnToCs.UseVisualStyleBackColor = true;
            this.m_btnToCs.Click += new System.EventHandler(this.m_btnToCs_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 191);
            this.Controls.Add(this.m_btnToCs);
            this.Controls.Add(this.m_txStatus);
            this.Controls.Add(this.m_btnCovert);
            this.Controls.Add(this.m_btnBrowse2);
            this.Controls.Add(this.m_btnBrowse1);
            this.Controls.Add(this.m_inputOutPath);
            this.Controls.Add(this.m_inputPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "xls2lua";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_inputPath;
        private System.Windows.Forms.TextBox m_inputOutPath;
        private System.Windows.Forms.Button m_btnBrowse1;
        private System.Windows.Forms.Button m_btnBrowse2;
        private System.Windows.Forms.Button m_btnCovert;
        private System.Windows.Forms.Label m_txStatus;
        private System.Windows.Forms.Button m_btnToCs;
    }
}


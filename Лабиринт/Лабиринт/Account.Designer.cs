﻿namespace Лабиринт
{
    partial class Account
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
            this.label1 = new System.Windows.Forms.Label();
            this.smenit_accaunt = new System.Windows.Forms.Button();
            this.vihod = new System.Windows.Forms.Button();
            this.generirovat = new System.Windows.Forms.Button();
            this.izmenit_parametry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя аккаунта";
            // 
            // smenit_accaunt
            // 
            this.smenit_accaunt.Location = new System.Drawing.Point(362, 12);
            this.smenit_accaunt.Name = "smenit_accaunt";
            this.smenit_accaunt.Size = new System.Drawing.Size(110, 23);
            this.smenit_accaunt.TabIndex = 1;
            this.smenit_accaunt.Text = "Сменить Аккаунт";
            this.smenit_accaunt.UseVisualStyleBackColor = true;
            this.smenit_accaunt.Click += new System.EventHandler(this.button1_Click);
            // 
            // vihod
            // 
            this.vihod.Location = new System.Drawing.Point(362, 326);
            this.vihod.Name = "vihod";
            this.vihod.Size = new System.Drawing.Size(110, 23);
            this.vihod.TabIndex = 2;
            this.vihod.Text = "Выход";
            this.vihod.UseVisualStyleBackColor = true;
            this.vihod.Click += new System.EventHandler(this.button2_Click);
            // 
            // generirovat
            // 
            this.generirovat.Location = new System.Drawing.Point(150, 120);
            this.generirovat.Name = "generirovat";
            this.generirovat.Size = new System.Drawing.Size(200, 80);
            this.generirovat.TabIndex = 3;
            this.generirovat.Text = "Генерировать";
            this.generirovat.UseVisualStyleBackColor = true;
            this.generirovat.Click += new System.EventHandler(this.button3_Click);
            // 
            // izmenit_parametry
            // 
            this.izmenit_parametry.Location = new System.Drawing.Point(12, 326);
            this.izmenit_parametry.Name = "izmenit_parametry";
            this.izmenit_parametry.Size = new System.Drawing.Size(128, 23);
            this.izmenit_parametry.TabIndex = 4;
            this.izmenit_parametry.Text = "Изменить параметры";
            this.izmenit_parametry.UseVisualStyleBackColor = true;
            this.izmenit_parametry.Click += new System.EventHandler(this.button4_Click);
            // 
            // Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.izmenit_parametry);
            this.Controls.Add(this.generirovat);
            this.Controls.Add(this.vihod);
            this.Controls.Add(this.smenit_accaunt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Account";
            this.Text = "Account";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Account_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button smenit_accaunt;
        private System.Windows.Forms.Button vihod;
        private System.Windows.Forms.Button generirovat;
        private System.Windows.Forms.Button izmenit_parametry;
    }
}
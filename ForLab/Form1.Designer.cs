﻿
namespace ForLab
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.butModifyDoc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butModifyDoc
            // 
            this.butModifyDoc.Location = new System.Drawing.Point(177, 121);
            this.butModifyDoc.Name = "butModifyDoc";
            this.butModifyDoc.Size = new System.Drawing.Size(128, 45);
            this.butModifyDoc.TabIndex = 0;
            this.butModifyDoc.Text = "Выбрать файл";
            this.butModifyDoc.UseVisualStyleBackColor = true;
            this.butModifyDoc.Click += new System.EventHandler(this.butModifyDoc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Программа дополняет свидетельство уникальным номером и главным мтерологом";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Программа свыдаст ошибку если файл делается второй раз и он остался открытым!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 208);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butModifyDoc);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butModifyDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


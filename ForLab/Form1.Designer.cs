
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
            this.cboTypeWork = new System.Windows.Forms.ComboBox();
            this.chkIsElectronic = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // butModifyDoc
            // 
            this.butModifyDoc.Location = new System.Drawing.Point(272, 91);
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
            // cboTypeWork
            // 
            this.cboTypeWork.Enabled = false;
            this.cboTypeWork.FormattingEnabled = true;
            this.cboTypeWork.Items.AddRange(new object[] {
            "Первичная",
            "Периодическая"});
            this.cboTypeWork.Location = new System.Drawing.Point(15, 114);
            this.cboTypeWork.Name = "cboTypeWork";
            this.cboTypeWork.Size = new System.Drawing.Size(166, 21);
            this.cboTypeWork.TabIndex = 2;
            this.cboTypeWork.Text = "Вид поверки";
            // 
            // chkIsElectronic
            // 
            this.chkIsElectronic.AutoSize = true;
            this.chkIsElectronic.Location = new System.Drawing.Point(15, 91);
            this.chkIsElectronic.Name = "chkIsElectronic";
            this.chkIsElectronic.Size = new System.Drawing.Size(171, 17);
            this.chkIsElectronic.TabIndex = 3;
            this.chkIsElectronic.Text = "Электронное свидетельство";
            this.chkIsElectronic.UseVisualStyleBackColor = true;
            this.chkIsElectronic.CheckedChanged += new System.EventHandler(this.chkIsElectronic_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 161);
            this.Controls.Add(this.chkIsElectronic);
            this.Controls.Add(this.cboTypeWork);
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
        private System.Windows.Forms.ComboBox cboTypeWork;
        private System.Windows.Forms.CheckBox chkIsElectronic;
    }
}


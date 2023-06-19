
namespace ForLab
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.butModifyDoc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDateSymbol = new System.Windows.Forms.ComboBox();
            this.chkIsElectronic = new System.Windows.Forms.CheckBox();
            this.chkIsMulty = new System.Windows.Forms.CheckBox();
            this.cboNameHead = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboHeadPosition = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butAddHead = new System.Windows.Forms.Button();
            this.butDelHead = new System.Windows.Forms.Button();
            this.butAddPosition = new System.Windows.Forms.Button();
            this.butDelPosition = new System.Windows.Forms.Button();
            this.butAddDate = new System.Windows.Forms.Button();
            this.butDelDate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCertificateLog = new System.Windows.Forms.Label();
            this.butSetcertificateLog = new System.Windows.Forms.Button();
            this.butInsertInCertificateLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNumCertificate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // butModifyDoc
            // 
            this.butModifyDoc.Location = new System.Drawing.Point(150, 272);
            this.butModifyDoc.Name = "butModifyDoc";
            this.butModifyDoc.Size = new System.Drawing.Size(128, 44);
            this.butModifyDoc.TabIndex = 0;
            this.butModifyDoc.Text = "Выбрать файл";
            this.butModifyDoc.UseVisualStyleBackColor = true;
            this.butModifyDoc.Click += new System.EventHandler(this.butModifyDoc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(426, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // cboDateSymbol
            // 
            this.cboDateSymbol.Enabled = false;
            this.cboDateSymbol.FormattingEnabled = true;
            this.cboDateSymbol.Location = new System.Drawing.Point(6, 227);
            this.cboDateSymbol.Name = "cboDateSymbol";
            this.cboDateSymbol.Size = new System.Drawing.Size(166, 21);
            this.cboDateSymbol.TabIndex = 2;
            this.cboDateSymbol.Text = "Год поверки";
            // 
            // chkIsElectronic
            // 
            this.chkIsElectronic.AutoSize = true;
            this.chkIsElectronic.Location = new System.Drawing.Point(6, 204);
            this.chkIsElectronic.Name = "chkIsElectronic";
            this.chkIsElectronic.Size = new System.Drawing.Size(171, 17);
            this.chkIsElectronic.TabIndex = 3;
            this.chkIsElectronic.Text = "Электронное свидетельство";
            this.chkIsElectronic.UseVisualStyleBackColor = true;
            this.chkIsElectronic.CheckedChanged += new System.EventHandler(this.chkIsElectronic_CheckedChanged);
            // 
            // chkIsMulty
            // 
            this.chkIsMulty.AutoSize = true;
            this.chkIsMulty.Location = new System.Drawing.Point(240, 197);
            this.chkIsMulty.Name = "chkIsMulty";
            this.chkIsMulty.Size = new System.Drawing.Size(191, 30);
            this.chkIsMulty.TabIndex = 4;
            this.chkIsMulty.Text = "Электронное свидетельство \r\nдля всех свидетельств в папке. ";
            this.chkIsMulty.UseVisualStyleBackColor = true;
            this.chkIsMulty.CheckedChanged += new System.EventHandler(this.chkIsMulty_CheckedChanged);
            // 
            // cboNameHead
            // 
            this.cboNameHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNameHead.FormattingEnabled = true;
            this.cboNameHead.Location = new System.Drawing.Point(9, 113);
            this.cboNameHead.Name = "cboNameHead";
            this.cboNameHead.Size = new System.Drawing.Size(358, 21);
            this.cboNameHead.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ФИО руководителя";
            // 
            // cboHeadPosition
            // 
            this.cboHeadPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHeadPosition.FormattingEnabled = true;
            this.cboHeadPosition.Location = new System.Drawing.Point(6, 159);
            this.cboHeadPosition.Name = "cboHeadPosition";
            this.cboHeadPosition.Size = new System.Drawing.Size(361, 21);
            this.cboHeadPosition.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Должность руководителя";
            // 
            // butAddHead
            // 
            this.butAddHead.Location = new System.Drawing.Point(373, 113);
            this.butAddHead.Name = "butAddHead";
            this.butAddHead.Size = new System.Drawing.Size(24, 23);
            this.butAddHead.TabIndex = 7;
            this.butAddHead.Text = "+";
            this.butAddHead.UseVisualStyleBackColor = true;
            this.butAddHead.Click += new System.EventHandler(this.butAddHead_Click);
            // 
            // butDelHead
            // 
            this.butDelHead.Location = new System.Drawing.Point(398, 113);
            this.butDelHead.Name = "butDelHead";
            this.butDelHead.Size = new System.Drawing.Size(24, 23);
            this.butDelHead.TabIndex = 7;
            this.butDelHead.Text = "-";
            this.butDelHead.UseVisualStyleBackColor = true;
            this.butDelHead.Click += new System.EventHandler(this.butDelHead_Click);
            // 
            // butAddPosition
            // 
            this.butAddPosition.Location = new System.Drawing.Point(371, 159);
            this.butAddPosition.Name = "butAddPosition";
            this.butAddPosition.Size = new System.Drawing.Size(24, 23);
            this.butAddPosition.TabIndex = 7;
            this.butAddPosition.Text = "+";
            this.butAddPosition.UseVisualStyleBackColor = true;
            this.butAddPosition.Click += new System.EventHandler(this.butAddPosition_Click);
            // 
            // butDelPosition
            // 
            this.butDelPosition.Location = new System.Drawing.Point(396, 159);
            this.butDelPosition.Name = "butDelPosition";
            this.butDelPosition.Size = new System.Drawing.Size(24, 23);
            this.butDelPosition.TabIndex = 7;
            this.butDelPosition.Text = "-";
            this.butDelPosition.UseVisualStyleBackColor = true;
            this.butDelPosition.Click += new System.EventHandler(this.butDelPosition_Click);
            // 
            // butAddDate
            // 
            this.butAddDate.Location = new System.Drawing.Point(176, 227);
            this.butAddDate.Name = "butAddDate";
            this.butAddDate.Size = new System.Drawing.Size(24, 23);
            this.butAddDate.TabIndex = 7;
            this.butAddDate.Text = "+";
            this.butAddDate.UseVisualStyleBackColor = true;
            this.butAddDate.Click += new System.EventHandler(this.butAddDate_Click);
            // 
            // butDelDate
            // 
            this.butDelDate.Location = new System.Drawing.Point(201, 227);
            this.butDelDate.Name = "butDelDate";
            this.butDelDate.Size = new System.Drawing.Size(24, 23);
            this.butDelDate.TabIndex = 7;
            this.butDelDate.Text = "-";
            this.butDelDate.UseVisualStyleBackColor = true;
            this.butDelDate.Click += new System.EventHandler(this.butDelDate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Регистрация эл. свидетельств в журнале";
            // 
            // lblCertificateLog
            // 
            this.lblCertificateLog.AutoSize = true;
            this.lblCertificateLog.Location = new System.Drawing.Point(7, 43);
            this.lblCertificateLog.Name = "lblCertificateLog";
            this.lblCertificateLog.Size = new System.Drawing.Size(103, 13);
            this.lblCertificateLog.TabIndex = 9;
            this.lblCertificateLog.Text = "Журнал не выбран";
            // 
            // butSetcertificateLog
            // 
            this.butSetcertificateLog.Location = new System.Drawing.Point(9, 67);
            this.butSetcertificateLog.Name = "butSetcertificateLog";
            this.butSetcertificateLog.Size = new System.Drawing.Size(102, 32);
            this.butSetcertificateLog.TabIndex = 10;
            this.butSetcertificateLog.Text = "Выбрать журнал";
            this.butSetcertificateLog.UseVisualStyleBackColor = true;
            this.butSetcertificateLog.Click += new System.EventHandler(this.butSetcertificateLog_Click);
            // 
            // butInsertInCertificateLog
            // 
            this.butInsertInCertificateLog.Location = new System.Drawing.Point(299, 119);
            this.butInsertInCertificateLog.Name = "butInsertInCertificateLog";
            this.butInsertInCertificateLog.Size = new System.Drawing.Size(102, 32);
            this.butInsertInCertificateLog.TabIndex = 10;
            this.butInsertInCertificateLog.Text = "Внести в журнал";
            this.butInsertInCertificateLog.UseVisualStyleBackColor = true;
            this.butInsertInCertificateLog.Click += new System.EventHandler(this.butInsertInCertificateLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.butModifyDoc);
            this.groupBox1.Controls.Add(this.cboDateSymbol);
            this.groupBox1.Controls.Add(this.chkIsElectronic);
            this.groupBox1.Controls.Add(this.chkIsMulty);
            this.groupBox1.Controls.Add(this.butDelDate);
            this.groupBox1.Controls.Add(this.cboNameHead);
            this.groupBox1.Controls.Add(this.butAddDate);
            this.groupBox1.Controls.Add(this.cboHeadPosition);
            this.groupBox1.Controls.Add(this.butDelPosition);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.butAddPosition);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.butDelHead);
            this.groupBox1.Controls.Add(this.butAddHead);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 323);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNumCertificate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblCertificateLog);
            this.groupBox2.Controls.Add(this.butInsertInCertificateLog);
            this.groupBox2.Controls.Add(this.butSetcertificateLog);
            this.groupBox2.Location = new System.Drawing.Point(491, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 319);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // txtNumCertificate
            // 
            this.txtNumCertificate.Location = new System.Drawing.Point(6, 131);
            this.txtNumCertificate.Name = "txtNumCertificate";
            this.txtNumCertificate.Size = new System.Drawing.Size(284, 20);
            this.txtNumCertificate.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Номер свидетельства";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 349);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Модификатор свидетельств";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butModifyDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDateSymbol;
        private System.Windows.Forms.CheckBox chkIsElectronic;
        private System.Windows.Forms.CheckBox chkIsMulty;
        private System.Windows.Forms.ComboBox cboNameHead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboHeadPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butAddHead;
        private System.Windows.Forms.Button butDelHead;
        private System.Windows.Forms.Button butAddPosition;
        private System.Windows.Forms.Button butDelPosition;
        private System.Windows.Forms.Button butAddDate;
        private System.Windows.Forms.Button butDelDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCertificateLog;
        private System.Windows.Forms.Button butSetcertificateLog;
        private System.Windows.Forms.Button butInsertInCertificateLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumCertificate;
    }
}


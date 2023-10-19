namespace StoreProject
{
    partial class Worker_Record
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
            this.components = new System.ComponentModel.Container();
            this.sTORE2DataSet = new StoreProject.STORE2DataSet();
            this.sTORE2DataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.addUser = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.textSurn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboPos = new System.Windows.Forms.ComboBox();
            this.comboDep = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSsn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sTORE2DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTORE2DataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sTORE2DataSet
            // 
            this.sTORE2DataSet.DataSetName = "STORE2DataSet";
            this.sTORE2DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sTORE2DataSetBindingSource
            // 
            this.sTORE2DataSetBindingSource.DataSource = this.sTORE2DataSet;
            this.sTORE2DataSetBindingSource.Position = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dataGridView1.Location = new System.Drawing.Point(12, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(865, 581);
            this.dataGridView1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(12, 640);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ana Menü";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addUser
            // 
            this.addUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addUser.Location = new System.Drawing.Point(1011, 426);
            this.addUser.Name = "addUser";
            this.addUser.Size = new System.Drawing.Size(114, 30);
            this.addUser.TabIndex = 5;
            this.addUser.Text = "Ekle";
            this.addUser.UseVisualStyleBackColor = true;
            this.addUser.Click += new System.EventHandler(this.button2_Click);
            // 
            // textName
            // 
            this.textName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textName.Location = new System.Drawing.Point(1011, 215);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(140, 22);
            this.textName.TabIndex = 0;
            // 
            // textSurn
            // 
            this.textSurn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textSurn.Location = new System.Drawing.Point(1011, 255);
            this.textSurn.Name = "textSurn";
            this.textSurn.Size = new System.Drawing.Size(140, 22);
            this.textSurn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(907, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Adı:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(907, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Soyadı:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(907, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Pozisyon:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(918, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Yeni Çalışan Ekle";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(784, 640);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "Sil";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboPos
            // 
            this.comboPos.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPos.FormattingEnabled = true;
            this.comboPos.Items.AddRange(new object[] {
            "General Manager",
            "Manager",
            "Supervisor",
            "Worker"});
            this.comboPos.Location = new System.Drawing.Point(1011, 297);
            this.comboPos.Name = "comboPos";
            this.comboPos.Size = new System.Drawing.Size(140, 24);
            this.comboPos.TabIndex = 2;
            // 
            // comboDep
            // 
            this.comboDep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDep.FormattingEnabled = true;
            this.comboDep.Items.AddRange(new object[] {
            "1 (General Management)",
            "2 (IT Management)",
            "3 (Purchase Management)",
            "4 (Human Resources)",
            "5 (Accounting Management)",
            "6 (X Management)",
            "7 (Y Management)"});
            this.comboDep.Location = new System.Drawing.Point(1011, 338);
            this.comboDep.Name = "comboDep";
            this.comboDep.Size = new System.Drawing.Size(140, 24);
            this.comboDep.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(907, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Department:";
            // 
            // textSsn
            // 
            this.textSsn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textSsn.Location = new System.Drawing.Point(1011, 383);
            this.textSsn.Name = "textSsn";
            this.textSsn.Size = new System.Drawing.Size(140, 22);
            this.textSsn.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(907, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Üst Sicil No:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(13, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 18);
            this.label9.TabIndex = 29;
            this.label9.Text = "Çalışan Bilgileri";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.Location = new System.Drawing.Point(675, 640);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 35);
            this.button3.TabIndex = 30;
            this.button3.Text = "Güncelle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Worker_Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(178)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1204, 703);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textSsn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboDep);
            this.Controls.Add(this.comboPos);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textSurn);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.addUser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Worker_Record";
            this.Text = "Worker_Record";
            this.Load += new System.EventHandler(this.Worker_Record_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sTORE2DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTORE2DataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource sTORE2DataSetBindingSource;
        private STORE2DataSet sTORE2DataSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addUser;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textSurn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboPos;
        private System.Windows.Forms.ComboBox comboDep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textSsn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
    }
}
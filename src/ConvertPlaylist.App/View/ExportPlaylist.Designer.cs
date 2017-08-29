namespace ConvertPlaylist.App.View
{
    partial class ExportPlaylist
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
            this.cbxPlaylist = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewPlaylist = new System.Windows.Forms.TextBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ltvAddItens = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pBarPlaylist = new System.Windows.Forms.ProgressBar();
            this.lblAtual = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecione uma playlist";
            // 
            // cbxPlaylist
            // 
            this.cbxPlaylist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxPlaylist.Location = new System.Drawing.Point(130, 10);
            this.cbxPlaylist.Name = "cbxPlaylist";
            this.cbxPlaylist.Size = new System.Drawing.Size(333, 21);
            this.cbxPlaylist.TabIndex = 1;
            this.cbxPlaylist.SelectedIndexChanged += new System.EventHandler(this.cbxPlaylist_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome da playliost Nova";
            // 
            // txtNewPlaylist
            // 
            this.txtNewPlaylist.Location = new System.Drawing.Point(130, 44);
            this.txtNewPlaylist.Name = "txtNewPlaylist";
            this.txtNewPlaylist.Size = new System.Drawing.Size(333, 20);
            this.txtNewPlaylist.TabIndex = 3;
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(8, 73);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(76, 25);
            this.btnExportar.TabIndex = 4;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ltvAddItens);
            this.groupBox1.Location = new System.Drawing.Point(13, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 205);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Músicas";
            // 
            // ltvAddItens
            // 
            this.ltvAddItens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.ltvAddItens.FullRowSelect = true;
            this.ltvAddItens.Location = new System.Drawing.Point(15, 19);
            this.ltvAddItens.Name = "ltvAddItens";
            this.ltvAddItens.Size = new System.Drawing.Size(429, 180);
            this.ltvAddItens.TabIndex = 27;
            this.ltvAddItens.UseCompatibleStateImageBehavior = false;
            this.ltvAddItens.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nome";
            this.columnHeader2.Width = 308;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Adicionado";
            this.columnHeader3.Width = 117;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pBarPlaylist);
            this.groupBox2.Controls.Add(this.lblAtual);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(16, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 38);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estado";
            // 
            // pBarPlaylist
            // 
            this.pBarPlaylist.Location = new System.Drawing.Point(141, 16);
            this.pBarPlaylist.MarqueeAnimationSpeed = 1;
            this.pBarPlaylist.Maximum = 0;
            this.pBarPlaylist.Name = "pBarPlaylist";
            this.pBarPlaylist.Size = new System.Drawing.Size(300, 12);
            this.pBarPlaylist.Step = 1;
            this.pBarPlaylist.TabIndex = 5;
            // 
            // lblAtual
            // 
            this.lblAtual.AutoSize = true;
            this.lblAtual.Location = new System.Drawing.Point(106, 16);
            this.lblAtual.Name = "lblAtual";
            this.lblAtual.Size = new System.Drawing.Size(0, 13);
            this.lblAtual.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Atual:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(41, 16);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 13);
            this.lblTotal.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total:";
            // 
            // ExportPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 366);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.txtNewPlaylist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxPlaylist);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExportPlaylist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar Playlist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportPlaylist_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxPlaylist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewPlaylist;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ltvAddItens;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblAtual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pBarPlaylist;
    }
}
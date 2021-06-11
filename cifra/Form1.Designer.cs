namespace Cifra
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbNomeArquivo = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tbNumeroSemiTons = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Abrir Arquivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Selecione um arquivo";
            // 
            // tbNomeArquivo
            // 
            this.tbNomeArquivo.Enabled = false;
            this.tbNomeArquivo.Location = new System.Drawing.Point(12, 42);
            this.tbNomeArquivo.Name = "tbNomeArquivo";
            this.tbNomeArquivo.Size = new System.Drawing.Size(695, 20);
            this.tbNomeArquivo.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(295, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Subir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // tbNumeroSemiTons
            // 
            this.tbNumeroSemiTons.Location = new System.Drawing.Point(189, 97);
            this.tbNumeroSemiTons.Name = "tbNumeroSemiTons";
            this.tbNumeroSemiTons.Size = new System.Drawing.Size(100, 20);
            this.tbNumeroSemiTons.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbNumeroSemiTons);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbNomeArquivo);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox tbNomeArquivo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbNumeroSemiTons;
    }
}


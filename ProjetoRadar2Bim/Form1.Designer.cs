/*
 * Form1.Designer.cs
 * Gerado manualmente para o Projeto Radar 2 Bimestre
 * Monte esse arquivo no Visual Studio junto com Form1.cs
 *
 * INSTRUCOES PARA MONTAR NO VISUAL STUDIO:
 * 1. Crie um novo projeto Windows Forms App (.NET Framework) em C#
 * 2. Substitua o conteudo de Form1.cs pelo arquivo Form1.cs fornecido
 * 3. Substitua o conteudo de Form1.Designer.cs por este arquivo
 * 4. Compile e execute (F5)
 */

namespace ProjetoRadar2Bim
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Controles da interface
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTecla;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label lblInstrucoes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            lblTecla = new Label();
            lblStatus = new Label();
            btnLimpar = new Button();
            lblInstrucoes = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.branco;
            pictureBox1.Location = new Point(28, 29);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(476, 469);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            // 
            // lblTecla
            // 
            lblTecla.BackColor = Color.Black;
            lblTecla.BorderStyle = BorderStyle.FixedSingle;
            lblTecla.Font = new Font("Courier New", 8F, FontStyle.Bold);
            lblTecla.ForeColor = Color.LimeGreen;
            lblTecla.Location = new Point(519, 237);
            lblTecla.Margin = new Padding(4, 0, 4, 0);
            lblTecla.Name = "lblTecla";
            lblTecla.Size = new Size(268, 34);
            lblTecla.TabIndex = 2;
            lblTecla.Text = "Modo: [1] Vermelho Tracejado";
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Black;
            lblStatus.BorderStyle = BorderStyle.FixedSingle;
            lblStatus.Font = new Font("Courier New", 8F);
            lblStatus.ForeColor = Color.Cyan;
            lblStatus.Location = new Point(519, 286);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(268, 57);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Clique no radar para definir o Ponto A.";
            // 
            // btnLimpar
            // 
            btnLimpar.BackColor = Color.DarkRed;
            btnLimpar.FlatStyle = FlatStyle.Flat;
            btnLimpar.Font = new Font("Courier New", 9F, FontStyle.Bold);
            btnLimpar.ForeColor = Color.White;
            btnLimpar.Location = new Point(519, 363);
            btnLimpar.Margin = new Padding(4, 3, 4, 3);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(268, 40);
            btnLimpar.TabIndex = 4;
            btnLimpar.Text = "LIMPAR FRENTES";
            btnLimpar.UseVisualStyleBackColor = false;
            btnLimpar.Click += btnLimpar_Click;
            // 
            // lblInstrucoes
            // 
            lblInstrucoes.BackColor = Color.Black;
            lblInstrucoes.BorderStyle = BorderStyle.FixedSingle;
            lblInstrucoes.Font = new Font("Courier New", 9F);
            lblInstrucoes.ForeColor = Color.DarkGreen;
            lblInstrucoes.Location = new Point(519, 14);
            lblInstrucoes.Margin = new Padding(4, 0, 4, 0);
            lblInstrucoes.Name = "lblInstrucoes";
            lblInstrucoes.Size = new Size(268, 207);
            lblInstrucoes.TabIndex = 1;
            lblInstrucoes.Text = resources.GetString("lblInstrucoes.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(811, 513);
            Controls.Add(pictureBox1);
            Controls.Add(lblInstrucoes);
            Controls.Add(lblTecla);
            Controls.Add(lblStatus);
            Controls.Add(btnLimpar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Radar Meteorologico Estilizado - ICG 2 Bimestre";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
    }
}
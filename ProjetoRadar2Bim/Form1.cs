using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoRadar2Bim
{
    public partial class Form1 : Form
    {
        // ---------------------------------------------------------
        // Variaveis do feixe rotativo (slide do professor)
        // ---------------------------------------------------------
        int raio = 180;
        int xc = 0;   // sera calculado no centro do pictureBox
        int yc = 0;   // sera calculado no centro do pictureBox
        int x = 0;
        int y = 0;
        int angle = 0;

        // Controla se a cor do feixe e Verde ou Preto (alterna a cada 360 graus)
        bool feixeVerde = true;

        // ---------------------------------------------------------
        // Captura de pontos pelo mouse (dois cliques -> desenha reta)
        // ---------------------------------------------------------
        int clickCount = 0;
        int px1 = 0, py1 = 0;   // primeiro ponto
        int px2 = 0, py2 = 0;   // segundo ponto

        // ---------------------------------------------------------
        // Lista de frentes de vento ja desenhadas
        // Cada entrada guarda: x1,y1,x2,y2,tecla (1 ou 2)
        // ---------------------------------------------------------
        struct FrenteDeVento
        {
            public int x1, y1, x2, y2;
            public int tipo; // 1 = Vermelha Tracejada | 2 = Azul Traco-ponto
        }
        List<FrenteDeVento> frentes = new List<FrenteDeVento>();

        // ---------------------------------------------------------
        // Tecla pressionada no momento do clique
        // ---------------------------------------------------------
        int teclaPressionada = 1; // padrao: Acao A

        // Timer para animar o feixe
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        // ---------------------------------------------------------
        // Construtor
        // ---------------------------------------------------------
        public Form1()
        {
            InitializeComponent();

            // Centro do pictureBox sera definido no Load
            this.KeyPreview = true; // Form captura teclas antes dos controles

            // Configura o Timer (intervalo em ms)
            timer1.Interval = 20;    // ~50 fps
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        // ==========================================================
        // PRIMITIVAS ENSINADAS PELO PROFESSOR
        // ==========================================================

        // Cria cor RGB
        public Color criaCor(int r, int g, int b)
        {
            Color cor = Color.FromArgb(r, g, b);
            return cor;
        }

        // Cria caneta com cor e espessura
        public Pen criaCaneta(int r, int g, int b, int espessura)
        {
            Pen caneta = new Pen(criaCor(r, g, b), espessura);
            return caneta;
        }

        // Pinta um unico ponto (primitiva do professor)
        public void pintaPonto(Graphics g, Pen caneta, int px, int py)
        {
            g.DrawLine(caneta, px, py, px + 1, py);
        }

        // ==========================================================
        // GRADIENTE LINEAR DO FEIXE (requisito 3 do projeto)
        // Interpola cor entre Verde (centro) e Preto (ponta)
        // ==========================================================
        public Color interpolaCor(Color corA, Color corB, float t)
        {
            int r = (int)(corA.R + (corB.R - corA.R) * t);
            int g = (int)(corA.G + (corB.G - corA.G) * t);
            int b = (int)(corA.B + (corB.B - corA.B) * t);
            return Color.FromArgb(r, g, b);
        }

        // ==========================================================
        // FEIXE ROTATIVO com gradiente ponto-a-ponto
        // Matematica de rotacao do slide do professor:
        //   Xc = x + raio * cos(angle * 3.15 / 180)
        //   Yc = y + raio * sin(angle * 3.15 / 180)
        // ==========================================================
        public void desenhafeixe(Graphics g)
        {
            // Cor solida por volta: Verde ou Preto, alterna a cada 360 graus
            Color cor = feixeVerde ? criaCor(0, 255, 0) : criaCor(0, 0, 0);
            Pen canetaFeixe = new Pen(cor, 4);

            for (int d = 0; d <= raio; d++)
            {
                // Formula de rotacao do professor
                int px = x + (int)(d * Math.Cos(angle * 3.15 / 180));
                int py = y + (int)(d * Math.Sin(angle * 3.15 / 180));

                pintaPonto(g, canetaFeixe, px, py);
            }

            canetaFeixe.Dispose();
        }

        // ==========================================================
        // RETA TRACEJADA - Acao A: Vermelha, espessura 2 (slide 64/65)
        // ==========================================================
        public void retaTracejada(Graphics g, int x0, int y0, int x1, int y1)
        {
            // Padrao Dash: {5, 2} -> traco de 5, espaco de 2
            float[] padrao = { 5, 2 };
            Pen caneta = criaCaneta(255, 0, 0, 2); // Vermelho, espessura 2
            caneta.DashPattern = padrao;
            g.DrawLine(caneta, x0, y0, x1, y1);
            caneta.Dispose();
        }

        // ==========================================================
        // RETA TRACO-PONTO - Acao B: Azul, espessura 3 (slide 66)
        // ==========================================================
        public void retaTracoPonto(Graphics g, int x0, int y0, int x1, int y1)
        {
            // Padrao DashDot: {5, 2, 1, 2} -> traco, espaco, ponto, espaco
            float[] padrao = { 5, 2, 1, 2 };
            Pen caneta = criaCaneta(0, 0, 255, 3); // Azul, espessura 3
            caneta.DashPattern = padrao;
            g.DrawLine(caneta, x0, y0, x1, y1);
            caneta.Dispose();
        }

        // ==========================================================
        // CIRCULO ponto a ponto usando a formula do professor
        // ==========================================================
        public void desenhaCirculo(Graphics g, Pen caneta, int cx, int cy, int r)
        {
            for (int a = 0; a < 360; a++)
            {
                int px = cx + (int)(r * Math.Cos(a * 3.15 / 180));
                int py = cy + (int)(r * Math.Sin(a * 3.15 / 180));
                pintaPonto(g, caneta, px, py);
            }
        }

        // ==========================================================
        // CIRCULOS DO RADAR (decoracao da interface)
        // Usando formula do professor ponto a ponto (DrawEllipse removido)
        // ==========================================================
        public void desenhaRadar(Graphics g)
        {
            Pen canetaCirculo = criaCaneta(0, 0, 0, 4); // espessura 3 para as circunferencias
            Pen canetaCruz    = criaCaneta(0, 0, 0, 2); // espessura 1 para a cruz

            // 4 circulos concentricos
            int[] raios = { 50, 90, 130, 180 };
            foreach (int r in raios)
            {
                desenhaCirculo(g, canetaCirculo, x, y, r);
            }

            // Cruz central (eixos horizontal e vertical)
            g.DrawLine(canetaCruz, x - raio, y, x + raio, y);
            g.DrawLine(canetaCruz, x, y - raio, x, y + raio);

            canetaCirculo.Dispose();
            canetaCruz.Dispose();
        }

        // ==========================================================
        // FRENTES DE VENTO ja armazenadas na lista
        // ==========================================================
        public void desenhaFrentes(Graphics g)
        {
            foreach (FrenteDeVento f in frentes)
            {
                if (f.tipo == 1)
                    retaTracejada(g, f.x1, f.y1, f.x2, f.y2);
                else
                    retaTracoPonto(g, f.x1, f.y1, f.x2, f.y2);
            }
        }

        // ==========================================================
        // EVENTO PAINT do pictureBox1 (requisito 6 do projeto)
        // ==========================================================
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // 1) Desenha os circulos e eixos do radar
            desenhaRadar(e.Graphics);

            // 2) Desenha as frentes de vento ja registradas
            desenhaFrentes(e.Graphics);

            // 3) Desenha o feixe rotativo com gradiente
            desenhafeixe(e.Graphics);
        }
 
        // timer tick avanca o angulo e invalida o pictureBox
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            angle++;

            // A cada 360 graus completos, alterna a cor do feixe
            if (angle >= 360)
            {
                angle = 0;
                feixeVerde = !feixeVerde; // alterna Verde <-> Preto
            }

            // Forca repintura do pictureBox (Invalidate do slide 58)
            pictureBox1.Invalidate();
        }

        // captura tecla 1 ou 2
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                teclaPressionada = 1;
                lblTecla.Text = "Modo: [1] Vermelho Tracejado";
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                teclaPressionada = 2;
                lblTecla.Text = "Modo: [2] Azul Traco-ponto";
            }
        }

        // ==========================================================
        // CAPTURA DO MOUSE no pictureBox (MouseEventArgs - slide 76/77)
        // Dois cliques definem o segmento de reta
        // ==========================================================
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Captura coordenadas do mouse 
            int mx = e.X;
            int my = e.Y;

            if (clickCount == 0)
            {
                // Primeiro ponto
                px1 = mx;
                py1 = my;
                clickCount = 1;
                lblStatus.Text = "Ponto A definido. Clique no Ponto B.";
            }
            else
            {
                // Segundo ponto -> cria a frente de vento
                px2 = mx;
                py2 = my;
                clickCount = 0;

                FrenteDeVento nova;
                nova.x1 = px1;
                nova.y1 = py1;
                nova.x2 = px2;
                nova.y2 = py2;
                nova.tipo = teclaPressionada;

                frentes.Add(nova);

                lblStatus.Text = "Frente de vento adicionada!";

                // forca repintura
                pictureBox1.Invalidate();
            }
        }

        // load
        private void Form1_Load(object sender, EventArgs e)
        {
            // Centro do pictureBox
            x = pictureBox1.Width / 2;
            y = pictureBox1.Height / 2;

            lblTecla.Text = "Modo: [1] Vermelho Tracejado";
            lblStatus.Text = "Clique no radar para definir o Ponto A.";
        }

        // bot�o de limpar
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            frentes.Clear();
            clickCount = 0;
            lblStatus.Text = "Frentes limpas. Clique no Ponto A.";
            pictureBox1.Invalidate();
        }
    }
}
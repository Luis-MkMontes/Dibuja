using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dibujo1
{
    // Montes Ojeda Luis Antonio 20211814

    struct Punto
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Punto(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public partial class Form1 : Form
    {
        List<Figura> figuras = new List<Figura>();
        private string estado = "dibujando";
        private string estadoFigura = "Rectangulo";

        private Punto p1_actual;
        private Figura figuraSeleccionada;

        private Figura Selecciona(int x, int y)
        {
            for (int i = figuras.Count - 1; i >= 0; i--)
            {
                if (figuras[i].EstaDentro(x, y))
                {
                    return figuras[i];
                }
            }
            return null;
        }   

        private void DibujaFiguras()
        {
            foreach (var figura in figuras)
            {
                figura.Dibuja(this);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (estado == "dibujando")
            {
                estado = "moviendo";
                label1.Text = String.Format($"x:{e.X}   y:{e.Y}");
                p1_actual = new Punto(e.X, e.Y);
            }
            else if (estado == "seleccionando")
            {
                figuraSeleccionada = Selecciona(e.X, e.Y);

                if (figuraSeleccionada != null)
                {
                    label1.Text = String.Format($"x:{figuraSeleccionada.punto1.X}   y:{figuraSeleccionada.punto1.Y}");
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (estado == "moviendo")
            {
                estado = "dibujando";
                label1.Text = String.Format($"x:{e.X}   y:{e.Y}");

                if (estadoFigura == "Rectangulo")
                {       
                    if (e.Button == MouseButtons.Left)
                    {
                        Rectangulo r = new Rectangulo(p1_actual, new Punto(e.X, e.Y));
                        r.Dibuja(this);
                        figuras.Add(r);
                    }
                }
                else if (estadoFigura == "Elipse")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Elipse elipse = new Elipse(p1_actual, new Punto(e.X, e.Y));
                        elipse.Dibuja(this);
                        figuras.Add(elipse);
                    }
                }
                else if (estadoFigura == "Linea")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Linea linea = new Linea(p1_actual, new Punto(e.X, e.Y));
                        linea.Dibuja(this);
                        figuras.Add(linea);
                    }
                }
                else if (estadoFigura == "Circulo")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        CaminoCircular circulo = new CaminoCircular(p1_actual, new Punto(e.X, e.Y));
                        circulo.Dibuja(this);
                        figuras.Add(circulo);
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (estado == "dibujando")
            {
                label1.Text = String.Format($"x:{e.X}   y:{e.Y}");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.DibujaFiguras();
        }

        private void dibujarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estado = "dibujando";
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estado = "seleccionando";
        }

        private void ordenarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figuras.Sort();
            figuras.Reverse();
            DibujaFiguras();
        }

        private void rojoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Red;
                this.Refresh();
            }
        }

        private void azulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Blue;
                this.Refresh();
            }
        }

        private void verdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Green;
                this.Refresh();
            }
        }

        private void naranjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Orange;
                this.Refresh();
            }
        }

        private void blancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.White;
                this.Refresh();
            }
        }

        private void rectanguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadoFigura = "Rectangulo";
        }

        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadoFigura = "Elipse";
        }

        private void lineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadoFigura = "Linea";
        }
        private void caminoCircularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadoFigura = "Circulo";
        }

        private void borrarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figuras.Clear();
            Refresh();
        }

        private void borrarUltimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = figuras.Count - 1;

            if (i >= 0)
            {
                figuras.RemoveAt(i);
                Refresh();
            }
        }

        private void borrarSeleccionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuras.Remove(figuraSeleccionada);
                Refresh();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Entidades;

namespace Final._2021.WinFormsApp
{
    ///Agregar manejo de excepciones en TODOS los lugares críticos!!!
   

    public partial class FrmPrincipal : Form
    {
        protected Task hilo;
        private List<Auto> lista;
        public FrmPrincipal()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        ///
        /// Punto 10 - Iniciar hilo
        ///
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Huallpa Wilson";
            MessageBox.Show(this.Text);
            this.hilo = Task.Run(() => ActualizarListadoAutosBD(sender));
            ///Se inicia el hilo

        }

        ///
        /// Punto 3 - FrmListado
        /// 
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListado frm = new FrmListado();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show(this);
        }

        ///
        /// Punto 9
        ///
        private void verLogAutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Abrir el archivo de auto";
                openFileDialog.InitialDirectory = "Documentos";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.DefaultExt = ".log";
                openFileDialog.FileName = "autos";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    if(ManejadoraTexto.Leer(filePath, out fileContent))
                    {
                        this.txtAutosLog.Text = fileContent;
                    }
                    else
                    {
                        MessageBox.Show("No se encontro el directorio");
                    }
                }
            }
        }

        ///PARA ACTUALIZAR LISTADO DESDE BD EN HILO
        ///NOTA: propiedades BackColor (fondo) y ForeColor (fuente)
        ///colores: 
        ///System.Drawing.Color.Black (negro)
        ///System.Drawing.Color.White (blanco)
        public void ActualizarListadoAutosBD(object param)
        {
            bool bandera = true;
            while (true)
            {

                Thread.Sleep(1500);
                this.HarcodearListBox(bandera);
                bandera = !bandera;
            }
        }

        private void HarcodearListBox(bool bandera)
        {
            if (this.lstAutos.InvokeRequired)
            {
                this.lstAutos.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.lista = Entidades.ADO.ObtenerTodos();
                    this.lstAutos.DataSource = this.lista;
                    if (bandera)
                    {
                        this.lstAutos.BackColor = System.Drawing.Color.Black;
                        this.lstAutos.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        this.lstAutos.BackColor = System.Drawing.Color.White;
                        this.lstAutos.ForeColor = System.Drawing.Color.Black;
                    }
                    
                });
            }
            else
            {
                this.lista = Entidades.ADO.ObtenerTodos();
                this.lstAutos.DataSource = this.lista;
                if (bandera)
                {
                    this.lstAutos.BackColor = System.Drawing.Color.Black;
                    this.lstAutos.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    this.lstAutos.BackColor = System.Drawing.Color.White;
                    this.lstAutos.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
    }
}

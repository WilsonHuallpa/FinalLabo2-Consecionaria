using Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Final._2021.WinFormsApp
{
    public delegate void EventHandler(object sender, EventArgs e);
    public partial class FrmListado : Form
    {
        List<Entidades.Auto> lista;
        public event EventHandler colorExit;
        public FrmListado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.colorExit += Manejador_colorExistente;
        }

        ///
        /// Punto 3 - Obtener y mostrar todos los autos de la BD
        ///
        private void FrmListado_Load(object sender, EventArgs e)
        {
            this.HarcodearListBox();
        }

        ///
        /// Punto 4 - Agregar un nuevo auto a la BD. Utilizar FrmAuto
        ///
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAuto frm = new FrmAuto();
            frm.StartPosition = FormStartPosition.CenterScreen;

            try
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.HarcodearListBox();
                }
                else
                {
                    MessageBox.Show("Nos veremos en el Infiernosss!!");
                }
            }
            catch (PatenteExisteException ex)
            {
                if(colorExit != null)
                {
                    colorExit.Invoke(this, new EventArgs());
                }
            }
            catch (Exception we)
            {
                MessageBox.Show(we.Message);
            }

            ///
            /// Punto 8-a - Capturar excepción si está repetido.
            ///
        }

        ///
        /// Punto 5 - Modificar auto seleccionado en la BD. Reutilizar FrmAuto
        ///
        private void btnModificar_Click(object sender, EventArgs e)
        {
            FrmAuto frm = new FrmAuto();
            Auto autoEliminar = (Auto)lstListado.SelectedItem;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.loadingAuto(autoEliminar, "Modificar");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.HarcodearListBox();
                MessageBox.Show("Gracias!!! Vuelva prontosss");
            }
            else
            {
                MessageBox.Show("Nos veremos en el Infiernosss!!");
            }
        }

        ///
        /// Punto 6 - Eliminar auto seleccionado de la BD. Reutilizar FrmAuto.
        ///
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            FrmAuto frm = new FrmAuto();
            Auto autoEliminar = (Auto)lstListado.SelectedItem;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.loadingAuto(autoEliminar, "Eliminar");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.HarcodearListBox();
                MessageBox.Show("Gracias!!! Vuelva prontosss");
            }
            else
            {
                MessageBox.Show("Nos veremos en el Infiernosss!!");
            }
        }

        ///
        /// Punto 8-b - Capturar evento ColorExiste y escribir en log
        ///
        private void Manejador_colorExistente(object sender, EventArgs e)
        {
            bool todoOK = false;
            //Reemplazar por la llamada al método de clase ManejadoraTexto.EscribirArchivo
            ADO.ColorExistente += ManejadoraTexto.EscribirArchivo;

            List<Auto> list = ADO.ObtenerTodos("rojo");

            //todoOK = ManejadoraTexto.EscribirArchivo(list);
            MessageBox.Show("Color repetido!!!");

            if (todoOK)
            {
                MessageBox.Show("Se escribió correctamente!!!");
            }
            else
            {
                MessageBox.Show("No se pudo escribir!!!");
            }
        }

        private void HarcodearListBox()
        {
            this.lista = Entidades.ADO.ObtenerTodos();
            this.lstListado.DataSource = this.lista;
        }
    }
}

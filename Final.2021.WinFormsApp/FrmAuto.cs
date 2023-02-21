using System;
using System.Windows.Forms;
using Entidades;

namespace Final._2021.WinFormsApp
{
    public partial class FrmAuto : Form
    {
        private Entidades.Auto auto;
        private Entidades.ADO aDO;
        private string addUpdateDelete = "Agregar";
        public Entidades.Auto AutoDelFormulario
        {
            get { return this.auto; }
        }

        public FrmAuto()
        {
            InitializeComponent();
        }
        /// Crar una instancia de tipo Auto
        /// Establecer como valor del atributo auto
        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            //Aceptar tiene que manejar modificacion de datos, eliminacion y agregar. 
            string modelo = this.txtModelo.Text;
            string marca = this.txtMarca.Text;
            string patente = this.txtPatente.Text;
            string color = this.txtColor.Text;
            int kms;
            kms = (int.TryParse(this.txtKms.Text, out kms)) ? int.Parse(this.txtKms.Text) : 0;
            try
            {
                aDO = new ADO();
                auto = new Auto(color, marca, modelo, kms, patente);
                switch (addUpdateDelete)
                {
                    case "Agregar":
                        aDO.Agregar(auto);
                        MessageBox.Show($"Se agrego correctamente la Patente {patente}");
                        break;
                    case "Modificar":
                        aDO.Modificar(auto);
                        MessageBox.Show($"Se Modifico correctamente la patente {patente}");
                        break;
                    case "Eliminar":
                        aDO.Eliminar(auto);
                        MessageBox.Show("Eliminar");
                        break;

                }
            } catch(PatenteExisteException ex)
            {
                MessageBox.Show($"Error Aceptar - {ex.Message}");
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void loadingAuto(Auto autoModificado, string estado)
        {
            auto = autoModificado;
            this.txtModelo.Text = auto.Modelo;
            this.txtMarca.Text = auto.Marca;
            this.txtPatente.Text = auto.Patente;
            this.txtColor.Text = auto.Color;
            this.txtKms.Text = auto.Kms.ToString();
            this.addUpdateDelete = estado;
        }
    }
}

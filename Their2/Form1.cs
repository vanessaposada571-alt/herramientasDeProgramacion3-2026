using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Their2
{
    public partial class Form1 : Form
    {
        BLLUsuario objetoCN = new BLLUsuario();
        private int id = 0;
        private bool Editar = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void ViewAllUsuario()
        {

            BLLUsuario objeto = new BLLUsuario();
            dataGridView1.DataSource = objeto.View();
        }

        private void ClearControls()
        {
            txtContrasena.Clear();
            txtIntentos.Clear();
            txtNivelS.Clear();
            txtFechaS.Clear();
            txtUsuario.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Editar == false)
            {
                try
                {
                    //Validación de controles

                    if (txtUsuario.Text == "")
                    {
                        MessageBox.Show("Falta Ingresar el Usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsuario.Focus();
                        return;
                    }
                    if (txtContrasena.Text == "")
                    {
                        MessageBox.Show("Falta Ingresar la Contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtContrasena.Focus();
                        return;
                    }
                    if (txtIntentos.Text == "")
                    {
                        MessageBox.Show("Falta Ingresar el Nro de Intentos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIntentos.Focus();
                        return;
                    }
                    if (txtNivelS.Text == "")
                    {
                        MessageBox.Show("Falta Ingresar el Nivel de Seguridad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNivelS.Focus();
                        return;
                    }
                    if (txtFechaS.Text == "")
                    {
                        MessageBox.Show("Falta Ingresar la Fecha de Registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFechaS.Focus();
                        return;
                    }

                    objetoCN.Create(txtUsuario.Text, txtContrasena.Text, Convert.ToInt32(txtIntentos.Text), Convert.ToDouble(txtNivelS.Text), Convert.ToDateTime(txtFechaS.Text));
                    MessageBox.Show("Se guardo correctamente");
                    ViewAllUsuario();
                    ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos, se encontro el siguiente error : " + ex);
                }
            }

            if (Editar == true)
            {

                try
                {
                    objetoCN.Update(txtUsuario.Text, txtContrasena.Text, Convert.ToInt32(txtIntentos.Text), Convert.ToDouble(txtNivelS.Text), Convert.ToDateTime(txtFechaS.Text), id);
                    MessageBox.Show("Registro actualizado correctamente");
                    ViewAllUsuario();
                    ClearControls();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos, se encontro el siguiente error : " + ex);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                txtUsuario.Text = dataGridView1.CurrentRow.Cells["usuario"].Value.ToString();
                txtContrasena.Text = dataGridView1.CurrentRow.Cells["contrasena"].Value.ToString();
                txtIntentos.Text = dataGridView1.CurrentRow.Cells["intentos"].Value.ToString();
                txtNivelS.Text = dataGridView1.CurrentRow.Cells["nivelSeg"].Value.ToString();
                txtFechaS.Text = dataGridView1.CurrentRow.Cells["fechaReg"].Value.ToString();
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            }
            else
                MessageBox.Show("Debe seleccionar un resgistro en el DataGridView");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                objetoCN.Delete(id);
                MessageBox.Show("Registro eliminado correctamente");
                ViewAllUsuario();
            }
            else
                MessageBox.Show("Debe seleccionar un resgistro en el DataGridView");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ViewAllUsuario();
        }
    }
}


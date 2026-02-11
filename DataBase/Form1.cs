using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(txtPersonID.Text);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=miBaseDeDatos;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Person WHERE PersonID = @PersonID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable resultadoTable = new DataTable();
                            adapter.Fill(resultadoTable);

                            dgvPersons.DataSource = resultadoTable;

                            if (resultadoTable.Rows.Count > 0)
                            {
                                txtLastName.Text = resultadoTable.Rows[0]["LastName"].ToString();
                                txtFirstName.Text = resultadoTable.Rows[0]["FirstName"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro.");
                            }


                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al buscar datos: " + ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int PersonID;

            string LastName, FirstName;

            PersonID = Convert.ToInt32(txtPersonID.Text);
            LastName = txtLastName.Text;
            FirstName = txtFirstName.Text;

            SqlConnection connection;
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=miBaseDeDatos;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;";
            string query = "SELECT * FROM Person";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand("insert into Person ( PersonID, LastName, FirstName) VALUES (@PersonID, @LastName, @FirstName)", connection);
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);

                cmd.ExecuteNonQuery();

                // SqlDataAdapter es útil para llenar DataSets o DataTables
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable PersonsTable = new DataTable();
                    adapter.Fill(PersonsTable);

                    // Asignar el DataTable como origen de datos del DataGridView
                    dgvPersons.DataSource = PersonsTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPersonID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

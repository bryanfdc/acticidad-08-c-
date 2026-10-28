using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace acticidad_08_c_
{
    public partial class Form1 : Form
    {
        // Conexión predeterminada a la base de datos de Laragon
        private string cadenaConexion = "Server=localhost;Port=3306;Database=programacionavanzada;Uid=root;Pwd=M4rciano15;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que los campos de texto no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, ingrese al menos el nombre y apellido.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seleccionar género (máximo 6 caracteres para no sobrepasar el VARCHAR(6) de la BD)
            string genero = "Otro";
            if (rbMasculino.Checked)
            {
                genero = "Hombre";
            }
            else if (rbFemenino.Checked)
            {
                genero = "Mujer";
            }

            // Consulta SQL para insertar en la tabla usuarios
            string sql = "INSERT INTO registros (Nombres, Apellidos, Telefono, Estatura, Edad, Genero) " +
                         "VALUES (@nombre, @apellidos, @telefono, @estatura, @edad, @genero)";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                    {
                        // Pasar los valores de las cajas de texto a la consulta SQL
                        comando.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        comando.Parameters.AddWithValue("@apellidos", txtApellido.Text.Trim());
                        comando.Parameters.AddWithValue("@telefono", Convert.ToInt64(txtTelefono.Text.Trim()));
                        comando.Parameters.AddWithValue("@estatura", float.Parse(txtEstatura.Text.Trim()));
                        comando.Parameters.AddWithValue("@edad", Convert.ToInt32(txtEdad.Text.Trim()));
                        comando.Parameters.AddWithValue("@genero", genero);

                        // Ejecutar la inserción en MySQL
                        comando.ExecuteNonQuery();

                        MessageBox.Show("¡Registro guardado exitosamente en la base de datos!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpiar los campos al finalizar
                        btnLimpiar_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEdad.Clear();
            txtEstatura.Clear();
            rbMasculino.Checked = false;
            rbFemenino.Checked = false;
            rbOtro.Checked = false;
            txtNombre.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

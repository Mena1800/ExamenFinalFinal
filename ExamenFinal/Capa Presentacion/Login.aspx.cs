using System;
using System.Web.UI;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;

namespace ExamenFinal.Presentacion
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string usuario = Request.Form["username"];
            string contraseña = Request.Form["password"];
            string connectionString = ConfigurationManager.ConnectionStrings["examenfinal"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM UsuariosLogin WHERE Usuario = @Usuario AND Contraseña = @Contraseña";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);


                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        Session["Usuario"] = usuario;
                        Response.Redirect("Menuprincipal.aspx");
                    }
                    else
                    {
                        lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    }


                }
            }
        }

    }
}
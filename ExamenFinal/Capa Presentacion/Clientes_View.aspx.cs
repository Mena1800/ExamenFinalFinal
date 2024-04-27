using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal.Capa_Presentacion
{
    public partial class Clientes_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();

        }
        protected void btnGuardarclient_Click(object sender, EventArgs e)
        {
            string nombre = Request.Form["txtnombrecli"];
            string email = Request.Form["txtemailcli"];
            string telefono = Request.Form["txttelefonocli"];

            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexionn = new SqlConnection(s);
            conexionn.Open();
            SqlCommand comandoo = new SqlCommand("INSERT INTO Clientes VALUES('" + nombre + "' " + email + "'" + telefono + ")", conexionn);
            comandoo.ExecuteNonQuery();
            conexionn.Close();
            LlenarGrid();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@Nombre, @Email, @Telefono)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Email", email);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.ExecuteNonQuery();
            }

            LlenarGrid();

        }




        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM JUGADOR"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();//refrescar la tabla
                        }

                    }


                }


            }


        }

        protected void Bdeleteclient_Click(object sender, EventArgs e)
        {
            string IDCliente = Request.Form["idBorrar"];
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            SqlCommand comando = new SqlCommand("DELETE Agentes  WHERE ID ='" + IDCliente + "'", conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            LlenarGrid();

        }

        protected void Bmodifclient_Click(object sender, EventArgs e)
        {
            string IDClient = Request.Form["idModificar"];
            string Nomclient = Request.Form["nombreModificar"];
            string emailclient = Request.Form["emailModificar"];
            string telefclient = Request.Form["telefonoModificar"];
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Agentes SET NOMBRE ='" + Nomclient + "',Email='" + emailclient + "',Telefono= '" + telefclient + "' WHERE ID = '" + IDClient + "'", conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            LlenarGrid();
        }


    }


}
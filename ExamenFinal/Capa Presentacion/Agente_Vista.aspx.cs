using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal.Capa_Presentacion
{
    public partial class Agente_Vista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = Request.Form["txtnombre"];

            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexionn = new SqlConnection(s);
            conexionn.Open();
            SqlCommand comandoo = new SqlCommand("INSERT INTO Agentes VALUES('" + nombre + "')", conexionn);
            comandoo.ExecuteNonQuery();
            conexionn.Close();
            LlenarGrid();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "INSERT INTO Agentes (Nombre) VALUES (@agente_nombre)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@agente_nombre", nombre);


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

        protected void Bdelete_Click(object sender, EventArgs e)
        {
            string IDAgent = Request.Form["txtIDAgent"];
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            SqlCommand comando = new SqlCommand("DELETE from Agentes  WHERE ID ='" + IDAgent + "'", conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            LlenarGrid();

        }

        protected void Bmodif_Click(object sender, EventArgs e)
        {
            string IDAgentnu = Request.Form["txtIDAgentnu"];
            string Nomagente = Request.Form["txtNomagente"];


            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            SqlCommand comando = new SqlCommand("UPDATE Agentes SET NOMBRE ='" + Nomagente + "' WHERE ID = '" + IDAgentnu + "'", conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            LlenarGrid();
        }


    }
}
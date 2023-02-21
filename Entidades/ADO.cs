using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Entidades
{
    public delegate bool ColorExistenteDelegado(List<Auto> lista);
    public class ADO
    {
        static SqlConnection connection;
        static SqlCommand command;      
        static SqlDataReader reader;
        public static event ColorExistenteDelegado ColorExistente;
        static ADO()
        {
            connection = new SqlConnection(@"Data Source = DESKTOP-JOPMB0N;
                                Database = concesionaria;
                                Trusted_Connection = True;");

            command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            
        }
        public bool Agregar(Auto car)
        {
            try
            {
                Auto auxCar;
                if(this.existePatente(car.Patente, out auxCar))
                {
                    throw new PatenteExisteException($"Patente ya existe {auxCar.ToString()}");
                }
                else
                {
                  
                    string sql = "INSERT INTO autos VALUES(@marca, @modelo, @kms, @color, @patente)";

                    command.Parameters.Add(new SqlParameter("@marca", car.Marca));
                    command.Parameters.Add(new SqlParameter("@modelo", car.Modelo));
                    command.Parameters.Add(new SqlParameter("@kms", car.Kms));
                    command.Parameters.Add(new SqlParameter("@color", car.Color));
                    command.Parameters.Add(new SqlParameter("@patente", car.Patente));
                    return EjecutarNonQuery(sql);
                  
                }
            }
            catch (PatenteExisteException ex) 
            {
                throw new PatenteExisteException($"Error en Agregando - {ex.Message}" );
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool Eliminar(Auto car)
        {
            string sql = "Delete autos where patente = @patente";

            command.Parameters.Add(new SqlParameter("@patente", car.Patente));

            return EjecutarNonQuery(sql);
        }
        public bool Modificar(Auto car)
        {
            string sql = "Update autos Set marca = @marca, modelo = @modelo," +
                "kms = @kms, color = @color, patente = @patente where patente = @patente";

            command.Parameters.Add(new SqlParameter("@marca", car.Marca));
            command.Parameters.Add(new SqlParameter("@modelo", car.Modelo));
            command.Parameters.Add(new SqlParameter("@kms", car.Kms));
            command.Parameters.Add(new SqlParameter("@color", car.Color));
            command.Parameters.Add(new SqlParameter("@patente", car.Patente));
            return EjecutarNonQuery(sql);
        }
        public static List<Auto> ObtenerTodos()
        {
            List<Auto> list = null;
            try
            {
                list = new List<Auto>();
                command.CommandText = "SELECT * FROM autos";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Auto(reader["color"].ToString(),
                                      reader["marca"].ToString(),
                                      reader["modelo"].ToString(),
                                      int.Parse(reader["kms"].ToString()),
                                      reader["patente"].ToString()));
                }
                return list;
            }
            catch (Exception)
            {
                throw new Exception("Error de conexión a la base de datos");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public static List<Auto> ObtenerTodos(string colorAuto)
        {
            List<Auto> list = null;
            try
            {
                list = new List<Auto>();
                command.CommandText = "SELECT * FROM autos where color = @color";
                command.Parameters.Add(new SqlParameter("@color", colorAuto));
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Auto(reader["color"].ToString(),
                                      reader["marca"].ToString(),
                                      reader["modelo"].ToString(),
                                      int.Parse(reader["kms"].ToString()),
                                      reader["patente"].ToString()));
                }
                return list;
            }
            catch (Exception)
            {
                throw new Exception("Error de conexión a la base de datos");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private bool existePatente(string patenteAuto, out Auto auto)
        {
            auto = null;
            List<Auto> lista = ObtenerTodos();

            foreach (Auto item in lista)
            {
                if (item.Patente == patenteAuto) 
                {
                    auto=item;
                    return true;
                }
            }
            return false;
        }
        private static bool EjecutarNonQuery(string sql)
        {
            bool todoOk = false;
            try
            {
                command.CommandText = sql;

                connection.Open();

                command.ExecuteNonQuery();

                todoOk = true;
            }
            catch (Exception ex)
            {
                todoOk = false;
                throw ex;
            }
            finally
            {
                command.Parameters.Clear();
                ADO.connection.Close();
            }
            return todoOk;
        }
        private bool existeColor(string color)
        {
            List<Auto> lista = ObtenerTodos();

            foreach (Auto item in lista)
            {
                if (item.Color == color)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

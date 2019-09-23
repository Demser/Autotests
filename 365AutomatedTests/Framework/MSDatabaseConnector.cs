using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace _365AutomatedTests.Framework
{
    using System.Data;
    using System.Threading;
    using static Config;
    public class MSDatabaseConnector
    {
        /// <summary>
        /// This is for connecting to the *some* database 
        /// </summary>
        private static string ConnectionString = $"Data Source={MSDbHost};User id={MSDbLogin};Password={MSDbPassword};Persist Security Info=True;Initial Catalog=";

        private SqlConnection conn;


        /// <summary>
        /// By default, DatabaseConnector connects to the LW08v6 database
        /// </summary>
        public MSDatabaseConnector() : this(MSDbLW)
        {
        }
        /// <summary>
        /// Initialize the DatabaseConnector with selected database and null command
        /// </summary>
        /// <param name="DbName">There should be a string identifies the database name taken from the Config class, e.g. Config.DbUsers</param>
        public MSDatabaseConnector(string DbName)
        {
            conn = new SqlConnection(ConnectionString + DbName);
        }

        public void DisposeConnection()
        {
            conn.Close();
        }
        public List<string> QueryExecutor(string Command)
        {
            List<string> result = new List<string>();
            conn.Open();
            var cmd = new SqlCommand(Command, conn);
            var dSet = new DataSet();
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dSet);
                dt = dSet.Tables[0];
                foreach (DataRow a in dt.Rows)
                {
                    StringBuilder row = new StringBuilder();
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        row.Append(a[i].ToString() + " ");
                    result.Add(row.ToString());
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return result;
        }
        public DataTable QueryExecutorTable(string Command)
        {
            List<string> result = new List<string>();
            conn.Open();
            var cmd = new SqlCommand(Command, conn);
            var dSet = new DataSet();
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dSet);
                dt = dSet.Tables[0];
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return dt;
        }
        public string QueryExecutorScalar(string Command)
        {
            conn.Open();
            var cmd = new SqlCommand(Command, conn);
            try
            {
                return cmd.ExecuteScalar().ToString();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
        public int NonQueryExecutor(string Command)
        {
            conn.Open();
            var cmd = new SqlCommand(Command, conn);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
    
  }

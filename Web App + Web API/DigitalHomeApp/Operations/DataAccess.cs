using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DigitalHome
{
    public static class DataAccess
    {
        public static SqlConnection OpenDBConnection(string connString)
        {
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();
            return sqlConn;
        }


        public static SqlConnection OpenDBConnection(SqlConnection sqlConn, string connString)
        {
            sqlConn = OpenDBConnection(connString);
            return sqlConn;
        }

        public static void CloseConnection(SqlConnection sqlConn)
        {
            sqlConn.Close();
            sqlConn.Dispose();
        }

        public static DataTable GetData(string connString, string SQlQuery)
        {

            SqlCommand sqlComm = new SqlCommand();

            try
            {
                DataTable _dtable = new DataTable();

                sqlComm.CommandText = SQlQuery;
                sqlComm.CommandType = CommandType.Text;
                sqlComm.Connection = OpenDBConnection(connString);


                SqlDataAdapter sqlAdap = new SqlDataAdapter();
                sqlAdap.SelectCommand = sqlComm;
                sqlAdap.Fill(_dtable);
                return _dtable;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlComm.Connection);
            }


        }

        public static DataTable GetData(string connString, string ProcedureName, SqlParameter[] inputParam)
        {
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                DataTable _dtable = new DataTable("Appliance");

                sqlComm.CommandText = ProcedureName;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Connection = OpenDBConnection(connString);

                for (int i = 0; i < inputParam.Count(); i++)
                {
                    sqlComm.Parameters.Add(inputParam[i]);
                }


                // SqlDataReader rdr = sqlComm.ExecuteReader();

                SqlDataAdapter sqlAdap = new SqlDataAdapter();
                sqlAdap.SelectCommand = sqlComm;
                sqlAdap.Fill(_dtable);
                return _dtable;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlComm.Connection);
            }



        }

        public static int ExecuteProcedure(string connString, string SqlQuery)
        {
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.CommandText = SqlQuery;
                sqlComm.CommandType = CommandType.Text;
                sqlComm.Connection = OpenDBConnection(connString);

                return sqlComm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlComm.Connection);
            }


        }

        private static int ExecuteProcedure(SqlConnection sqlConn, string ProcedureName, SqlParameter[] inputParam)
        {
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.CommandText = ProcedureName;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Connection = sqlConn;

                for (int i = 0; i < inputParam.Count(); i++)
                {
                    sqlComm.Parameters.Add(inputParam[i]);
                }

                return sqlComm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlComm.Connection);
            }


        }

        public static int ExecuteProcedure(string connString, string ProcedureName, SqlParameter[] inputParam)
        {
            return ExecuteProcedure(OpenDBConnection(connString), ProcedureName, inputParam);
        }
    }
}
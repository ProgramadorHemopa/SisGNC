using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class MySqlDo
    {
        public MySqlDo()
        {
        }
    

        public DataTable Consulta
        (
            string pQuery,
            string pConnectionString
        )
        {
            DataTable lTable = new DataTable();
            DataSet lDataSet = new DataSet();
            //OracleConnection lConnection = new OracleConnection(pConnectionString);
            MySqlConnection lConnectionMySql = new MySqlConnection(pConnectionString);

            try
            {
                //lConnection.Open();
                lConnectionMySql.Open();

                //OracleDataAdapter lDataAdapter = new OracleDataAdapter(pQuery, pConnectionString);
                MySqlDataAdapter lDataAdapterMySql = new MySqlDataAdapter(pQuery, pConnectionString);
                
                //lDataAdapter.Fill(lDataSet);
                lDataAdapterMySql.Fill(lDataSet);

                //lTable = lDataSet.Tables[0];
                lTable = lDataSet.Tables[0];

                return lTable;
            }
            finally
            {
                //lConnection.Close();
                lConnectionMySql.Close();
            }
        }

        public void Exclui(string aCommand, string pConnectionString)
        {
            //OracleConnection lConnection = new OracleConnection(pConnectionString);
            MySqlConnection lConnection = new MySqlConnection(pConnectionString);
            try
            {
                //OracleCommand cmd = new OracleCommand();
                MySqlCommand cmd = new MySqlCommand();
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = aCommand;
                cmd.Connection = lConnection;

                lConnection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                lConnection.Close();
            }

        }
     
    }
}

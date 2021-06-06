﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
namespace APIs.DBUtility
{


    public class DBHelper
    {
        //加入参数，引入 "sever=8.140.12.78;database=orcl;uid=system;pwd=123456;" 
        public string ConnectionString { get;} =
        //"User ID=system;Password=123456;Data Source=orcl;)";
        "User ID=system;Password=123456;Data Source=(DESCRIPTION =(ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 8.140.12.78)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
        public DataTable ExecuteTable(string cmdText, params OracleParameter[] oraParameters) {
           
            using OracleConnection conn = new OracleConnection(ConnectionString); 
            conn.Open();
            OracleCommand cmd = new OracleCommand(cmdText, conn);           
            cmd.Parameters.AddRange(oraParameters);
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(cmd);           
            DataSet ds = new DataSet();
            oracleDataAdapter.Fill(ds);
            return ds.Tables[0];
            //using SqlConnection conn = new SqlConnection(ConnectionString);
            //sqlDataAdapter.Fill(ds);
            //SqlCommand cmd = new SqlCommand(cmdText, conn);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        }

        public int ExecuteNonQuery(string cmdText, params OracleParameter[] oraParameters)
        {
            using OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(cmdText, conn);
            cmd.Parameters.AddRange(oraParameters);
            // cmd.ExecuteNonQuery();
            return cmd.ExecuteNonQuery();
        }
        public int ExecuteCount(string tableName)
        {
            int res;
            string query = "SELECT COUNT(ID) COUNT FROM " + tableName;
            DataTable ds = ExecuteTable(query);
            res = int.Parse(ds.Rows[0]["COUNT"].ToString());
            return res;
        }
    }
}
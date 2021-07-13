using System;
using System.Data;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

namespace WebApiTpl.Services
{
    public class MySqlHelper
    {
            string connectionString = string.Empty;
        public MySqlHelper(string _connectionString)
        {
            connectionString = _connectionString;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="commandText">要运行的sql语句</param>
        ///// <param name="cmdType">运行的命令类型</param>
        ///// <param name="para">参数列表，没有参数设置为null</param>
        ///// <returns>受影响的行数</returns>
        //public int ExecuteNonQuery(string commandText, CommandType cmdType, params MySqlParameter[] para)
        //{
        //    using (MySqlConnection con = new MySqlConnection(connectionString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = commandText;
        //        cmd.CommandType = cmdType;
        //        cmd.Connection = con;
        //        if (para != null && para.Length > 0)
        //        {
        //            cmd.Parameters.AddRange(para);
        //        }
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="commandText">要运行的sql语句</param>
        ///// <param name="cmdType">运行的命令类型</param>
        ///// <param name="para">参数列表，没有参数设置为null</param>
        ///// <returns>受影响的行数</returns>
        //public async Task<int> ExecuteNonQueryAsync(string commandText, CommandType cmdType, params MySqlParameter[] para)
        //{
        //    await using MySqlConnection con = new MySqlConnection(connectionString);
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = commandText;
        //    cmd.CommandType = cmdType;
        //    cmd.Connection = con;
        //    if (para != null && para.Length > 0)
        //    {
        //        cmd.Parameters.AddRange(para);
        //    }
        //    con.Open();
        //    return await cmd.ExecuteNonQueryAsync();
        //}
        ///// <summary>
        ///// 执行带参数的sql语句，返回DataSet
        ///// </summary>
        ///// <param name="commandText">要运行的sql语句</param>
        ///// <param name="cmdType">运行的命令类型(Text为sql语句,StoredProcedure为存储过程)</param>
        ///// <param name="para">参数列表，没有参数设置为null</param>
        ///// <returns>数据集</returns>
        //public DataSet GetDataSet(string commandText, CommandType cmdType, params MySqlParameter[] para)
        //{
        //    using (MySqlConnection con = new MySqlConnection(connectionString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = commandText;
        //        //设置Cmd对象的命令类型
        //        cmd.CommandType = cmdType;
        //        cmd.CommandTimeout = 300;
        //        //如果参数不为空，添加参数列表
        //        if (para != null && para.Length > 0)
        //            cmd.Parameters.AddRange(para);
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        return ds;
        //    }
        //}
        ///// <summary>
        ///// 查询结果集中的第一行的第一列的数据
        ///// </summary>
        ///// <param name="commandText">要运行的sql语句</param>
        ///// <param name="cmdType">运行的命令类型</param>
        ///// <param name="para">参数列表，没有参数设置为null</param>
        ///// <returns>返回结果集中的第一行的第一列</returns>
        //public object ExecuteScalar(string commandText, CommandType cmdType, params MySqlParameter[] para)
        //{
        //    using (MySqlConnection con = new MySqlConnection(connectionString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = commandText;
        //        cmd.CommandType = cmdType;
        //        cmd.Connection = con;
        //        if (para != null && para.Length > 0)
        //        {
        //            cmd.Parameters.AddRange(para);
        //        }
        //        con.Open();
        //        return cmd.ExecuteScalar();
        //    }
        //}



        ///// <summary>
        ///// 执行sql，带有事务
        ///// </summary>
        ///// <param name="commandText">要运行的sql语句</param>
        ///// <param name="cmdType">运行的命令类型</param>
        ///// <param name="para">参数列表，没有参数设置为null</param>
        ///// <returns>受影响的行数</returns>
        //public int ExecuteCommandWidthTran(string commandText, CommandType cmdType, params MySqlParameter[] para)
        //{
        //    using (MySqlConnection con = new MySqlConnection(connectionString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = commandText;
        //        cmd.CommandType = cmdType;
        //        cmd.Connection = con;

        //        if (para != null && para.Length > 0)
        //        {
        //            cmd.Parameters.AddRange(para);
        //        }

        //        con.Open();
        //        MySqlTransaction t = con.BeginTransaction();
        //        cmd.Transaction = t;
        //        try
        //        {
        //            int Result = cmd.ExecuteNonQuery();
        //            //提交事务
        //            t.Commit();
        //            return Result;
        //        }
        //        catch (Exception ex)
        //        {
        //            //回滚事务
        //            Console.WriteLine(ex);
        //            t.Rollback();
        //            return 0;
        //        }

        //    }
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;

namespace CoreClass
{
	/// <summary>
	/// 数据库连接基类
	/// </summary>
	public class SqlProc_ACCESS
	{
		protected string m_connectionstring = null;
		protected OleDbConnection m_sqlConn = null;
		protected OleDbCommand m_SqlCmd = null;
		protected string m_sql = null;

		/// <summary>
		/// 打开数据库连接
		/// </summary>
		protected void SqlProcOpen()
		{
			m_sqlConn = new OleDbConnection(m_connectionstring);
			m_sqlConn.Open();
		}
		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		protected void SqlProcClose()
		{
			try
			{
				m_SqlCmd.Cancel();
				m_SqlCmd.Dispose();
			}
			catch { }
			try
			{
				m_sqlConn.Dispose();
				m_sqlConn.Close();
			}
			catch { }
		}
		/// <summary>
		/// 执行T-sql语句,返回执行结果
		/// </summary>
		/// <param name="sql">T-sql语句</param>
		protected bool Execute()
		{
			try
			{
				m_SqlCmd = new OleDbCommand(m_sql, m_sqlConn);
				int rows = m_SqlCmd.ExecuteNonQuery();
				return Convert.ToBoolean(rows);
			}
			catch (System.Data.SqlClient.SqlException E)
			{
				throw new Exception(E.Message);
			}
		}
		/// <summary>
		/// 执行T-sql语句,返回执数据集
		/// </summary>
		/// <param name="sql">T-sql语句</param>
		/// <param name="ds">ref 返回数据集</param>
		protected bool Execute(DataSet ds)
		{
			bool b = false;
			try
			{
				m_SqlCmd = new OleDbCommand(m_sql, m_sqlConn);
				new OleDbDataAdapter(m_SqlCmd).Fill(ds, "ds");
				b = true;
			}
			catch (System.Data.SqlClient.SqlException E)
			{
				throw new Exception(E.Message);
			}
			return b;
		}
		

	}
}

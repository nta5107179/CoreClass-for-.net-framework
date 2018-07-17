using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.SQLite;

namespace CoreClass
{
	/// <summary>
	/// 数据库连接基类
	/// </summary>
	public class SqlProc_SQLITE
	{
		protected string m_connectionstring = null;
		protected SQLiteConnection m_sqlConn = null;
		protected SQLiteCommand m_SQLiteCmd = null;
		protected string m_sql = null;

		/// <summary>
		/// 打开数据库连接
		/// </summary>
		protected void SQLiteProcOpen()
		{
			m_sqlConn = new SQLiteConnection(m_connectionstring);
			m_sqlConn.Open();
		}
		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		protected void SQLiteProcClose()
		{
			try
			{
				m_SQLiteCmd.Cancel();
				m_SQLiteCmd.Dispose();
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
		protected bool Execute()
		{
			try
			{
				m_SQLiteCmd = new SQLiteCommand(m_sql, m_sqlConn);
				int rows = m_SQLiteCmd.ExecuteNonQuery();
				return Convert.ToBoolean(rows);
			}
			catch (System.Data.SQLite.SQLiteException E)
			{
				throw new Exception(E.Message);
			}
		}
        /// <summary>
        /// 执行T-sql语句,返回执行结果
        /// </summary>
        /// <param name="identity">返回新插入的列编号</param>
        protected bool Execute(ref long identity)
        {
            try
            {
                m_SQLiteCmd = new SQLiteCommand(m_sql, m_sqlConn);
                identity = (long)m_SQLiteCmd.ExecuteScalar();
                return Convert.ToBoolean(identity);
            }
            catch (System.Data.SQLite.SQLiteException E)
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
				m_SQLiteCmd = new SQLiteCommand(m_sql, m_sqlConn);
				new SQLiteDataAdapter(m_SQLiteCmd).Fill(ds, "ds");
				b = true;
			}
			catch (System.Data.SQLite.SQLiteException E)
			{
				throw new Exception(E.Message);
			}
			return b;
		}
	}
}

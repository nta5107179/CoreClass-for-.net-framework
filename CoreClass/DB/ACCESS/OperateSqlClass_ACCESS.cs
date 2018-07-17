using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

namespace CoreClass
{
	/// <summary>
	/// T-Sql语句 存储过程执行类
	/// </summary>
	public class OperateSqlClass_ACCESS : SqlProc_ACCESS
	{
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		public void Open()
		{
			m_connectionstring = ConnectionStrings.GetConnectString1;
			SqlProcOpen();
		}
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		public void Open(string connectionstring)
		{
			m_connectionstring = connectionstring;
			SqlProcOpen();
		}
		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		public void Close()
		{
			SqlProcClose();
		}
		/// <summary>
		/// 数据库操作-添加行
		/// </summary>
		/// <param name="sql">T-Sql语句</param>
		/// <returns></returns>
		public bool Insert(string sql)
		{
			bool b = false;
			m_sql = sql;
			b = Execute();
			return b;
		}
		/// <summary>
		/// 数据库操作-删除行
		/// </summary>
		/// <param name="sql">T-Sql语句</param>
		/// <returns></returns>
		public bool Delete(string sql)
		{
			bool b = false;
			m_sql = sql;
			b = Execute();
			return b;
		}
		/// <summary>
		/// 数据库操作-修改行
		/// </summary>
		/// <param name="sql">T-Sql语句</param>
		/// <returns></returns>
		public bool Update(string sql)
		{
			bool b = false;
			m_sql = sql;
			b = Execute();
			return b;
		}
		/// <summary>
		/// 数据库操作-查询数据
		/// </summary>
		/// <param name="sql">执行语句</param>
		/// <returns>dataset数据集</returns>
		public DataSet Select(string sql)
		{
			DataSet ds = new DataSet();
			m_sql = sql;
			Execute(ds);
			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				return ds;
			}
			return null;
		}

	}
}

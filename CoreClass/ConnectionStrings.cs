using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CoreClass
{
	/// <summary>
	///ConnectionStrings 连接字串类
	/// </summary>
	public static class ConnectionStrings
	{
		readonly static string sKey = "xiao0xiong.mao_";

		/// <summary>
		/// 得到连接字
		/// </summary>
		public static string GetConnectString1
        {
            //get { return DESDecryption(ConfigurationManager.ConnectionStrings["ApplicationServices1"].ToString()); }
            /*
            <connectionStrings>
                //MSSQL
	            <add name="ApplicationServices1" connectionString="server=192.168.1.2;database=ZLSystem;uid=sa;pwd=sa.sa1" providerName="System.Data.SqlClient"/>
	            //MYSQL
	            <add name="ApplicationServices1" connectionString="server=127.0.0.1;database=tj;uid=root;pwd=;charset=utf8;" providerName="System.Data.SqlClient"/>
	            //ACCESS 文件放在App_Data里
	            <add name="ApplicationServices1" connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\db.mdb;" />
	            //SQLite 文件放在App_Data里
	            <add name="ApplicationServices1" connectionString="Pooling=true;FailIfMissing=false;Data Source=|DataDirectory|\db.db;" />
            </connectionStrings>
            */
            get { return ConfigurationManager.ConnectionStrings["ApplicationServices1"].ToString(); }
		}
		/// <summary>
		/// DES加密
		/// </summary>
		/// <param name="Text">内容</param>
		/// <returns></returns>
		public static string DESEncryption(string Text)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			byte[] inputByteArray = Encoding.Default.GetBytes(Text);
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			StringBuilder ret = new StringBuilder();
			foreach (byte b in ms.ToArray())
			{
				ret.AppendFormat("{0:X2}", b);
			}
			return ret.ToString();
		}

		/// <summary>
		/// DES解密
		/// </summary>
		/// <param name="Text">内容</param>
		/// <returns></returns>
		public static string DESDecryption(string Text)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();

			int len = Text.Length / 2;
			byte[] inputByteArray = new byte[len];
			for (int x = 0; x < len; x++)
			{
				int i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
				inputByteArray[x] = (byte)i;
			}
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			return Encoding.Default.GetString(ms.ToArray());
		}
	}
}
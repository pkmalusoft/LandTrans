using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
 
namespace LTMSV2.Models
{
    public class pgFun
    {
        public pgFun()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public static Dictionary<string, string> ExecuteSQL
               (string Query,
               Dictionary<string, string> InputParametersListWithValue,
               Dictionary<string, string> OutputParametersListWithValue,
               bool IsStoreProcedure = true)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Query;
                    if (IsStoreProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    foreach (KeyValuePair<string, string> para in InputParametersListWithValue)
                    {
                        cmd.Parameters.AddWithValue(para.Key, para.Value);
                    }
                    foreach (KeyValuePair<string, string> para in OutputParametersListWithValue)
                    {
                        cmd.Parameters.Add(para.Key, SqlDbType.NVarChar, 1000);
                        cmd.Parameters[para.Key].Direction = ParameterDirection.Output;
                    }
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    Dictionary<string, string> res = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> para in OutputParametersListWithValue)
                    {
                        res.Add(para.Key, cmd.Parameters[para.Key].Value.ToString());
                    }

                    return res;
                }
            }

        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static Dictionary<string, string> InsertData(string Query, Dictionary<string, string> InputParametersListWithValue, Dictionary<string, string> OutputParametersListWithValue, bool IsStoreProcedure = true)
        {

            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Query;
                    if (IsStoreProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    foreach (KeyValuePair<string, string> para in InputParametersListWithValue)
                    {
                        cmd.Parameters.AddWithValue(para.Key, para.Value);
                    }
                    foreach (KeyValuePair<string, string> para in OutputParametersListWithValue)
                    {
                        cmd.Parameters.Add(para.Key, SqlDbType.NVarChar, 1000);
                        cmd.Parameters[para.Key].Direction = ParameterDirection.Output;
                    }
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    Dictionary<string, string> res = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> para in OutputParametersListWithValue)
                    {
                        res.Add(para.Key, cmd.Parameters[para.Key].Value.ToString());
                    }

                    return res;
                }
            }

        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            System.Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName].ToString(), null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static List<T> GetData<T>(string Query, Dictionary<string, string> InputParametersListWithValue = null, bool IsStoreProcedure = true)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Query;
                    if (IsStoreProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    if (InputParametersListWithValue != null)
                    {
                        foreach (KeyValuePair<string, string> para in InputParametersListWithValue)
                        {
                            cmd.Parameters.AddWithValue(para.Key, para.Value);
                        }
                    }
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {

                            sda.Fill(dt);
                        }
                    }
                    List<T> data = new List<T>();
                    foreach (DataRow row in dt.Rows)
                    {
                        T item = GetItem<T>(row);
                        T obj = Activator.CreateInstance<T>();

                        foreach (DataColumn column in dt.Columns)
                        {
                            foreach (PropertyInfo pro in typeof(T).GetProperties())
                            {
                                if (pro.Name == column.ColumnName)
                                    pro.SetValue(obj, row[column.ColumnName].ToString(), null);
                                else
                                    continue;
                            }
                        }
                        data.Add(item);
                    }
                    return data;
                }
            }
        }
        public static string GetSingalData(string query, string ParaName, string parameterWithValue, string para2, string para2val)
        {
            /// <summary>
            /// Summary get single data from DB
            /// </summary>
            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue(ParaName, parameterWithValue);
                    cmd.Parameters.AddWithValue(para2, para2val);
                    cmd.Connection = con;
                    con.Open();
                    string res = cmd.ExecuteScalar().ToString();
                    con.Close();
                    return res;

                }
            }
        }
        public static string GetSingalData(string query, string ParaName, string parameterWithValue, string para2, string para2val, string Outname, bool IsSP = true)
        {
            /// <summary>
            /// Summary get single data from DB
            /// </summary>
            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    if (IsSP)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    cmd.Parameters.AddWithValue(ParaName, parameterWithValue);
                    cmd.Parameters.AddWithValue(para2, para2val);
                    cmd.Parameters.Add(Outname, SqlDbType.NVarChar, 300);
                    cmd.Parameters[Outname].Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string res = cmd.Parameters[Outname].Value.ToString();
                    con.Close();
                    return res;

                }
            }
        }
        public static string GetSingalData(string query, string ParaName, string parameterWithValue)
        {
            /// <summary>
            /// Summary get single data from DB
            /// </summary>
            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue(ParaName, parameterWithValue);
                    cmd.Connection = con;
                    con.Open();
                    string res = cmd.ExecuteScalar().ToString();
                    con.Close();
                    return res;

                }
            }
        }
        public static string GetIPAddress()
        {
            HttpContext context = HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static string GetSingalData(string query)
        {
            /// <summary>
            /// Summary get single data from DB
            /// </summary>
            string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    con.Open();
                    string res = cmd.ExecuteScalar().ToString();
                    con.Close();
                    return res;

                }
            }
        }
        public static Random _random = new Random();
        public static string GenerateRandomNo()
        {
            return _random.Next(10000, 99999).ToString("D5");
        }      
        public static async Task<string> SendMail(string Subject, string Message, string Name, string Email, string filepath = "", string SenderAccount = "SMTPMail", List<string> list = null)
        {
            try
            {
                string mid = SenderAccount + "ID";
                string mpass = SenderAccount + "Pass";
                MailMessage Msg = new MailMessage(ConfigurationManager.AppSettings[mid].ToString(), Email);
                Msg.From = new MailAddress(ConfigurationManager.AppSettings[mid].ToString(), "");
                if (list != null)
                {
                    foreach (var x in list)
                    {
                        //  Msg.To.Add(x);
                        MailAddress bcc = new MailAddress(x);
                        Msg.Bcc.Add(bcc);
                    }
                }
                Msg.Subject = Subject;
                Msg.Body = Message;
                Msg.IsBodyHtml = true;
                if (filepath != "noFile")
                {
                    Attachment at = new Attachment(HttpContext.Current.Server.MapPath(filepath));
                    Msg.Attachments.Add(at);
                }
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[mid].ToString(), ConfigurationManager.AppSettings[mpass].ToString());
                var res = Task.Run(() =>
                {
                    client.Send(Msg);
                });
                return "1";
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }   

    }
}
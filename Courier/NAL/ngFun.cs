using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace LTMSV2.NAL
{
    public class ngFun
    {
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

        public static string GetGUID32bit()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsValidEmail(string YourEmail)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(YourEmail, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string SendMail(string Subject, string Message, string Name, string Email, string filepath)
        {
            try
            {
                MailMessage Msg = new MailMessage(ConfigurationManager.AppSettings["SMTPMailID"].ToString(), Email);
                Msg.Subject = Subject;
                Msg.Body = Message;
                Msg.IsBodyHtml = true;
                if (filepath != "noFile")
                {
                    // Attachment at = new Attachment(Server.MapPath(filepath));
                    // Msg.Attachments.Add(at);
                }

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPMailID"].ToString(), ConfigurationManager.AppSettings["SMTPMailPass"].ToString());
                client.Send(Msg);
                return "1";

            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public static int GetRowCount(string query, string Mode, string Value, string ParaName, string parameterWithValue)
        {
            /// <summary>
            /// Summary get single data from DB
            /// </summary>
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Mode, Value);
                    cmd.Parameters.AddWithValue(ParaName, parameterWithValue);
                    cmd.Connection = con;
                    con.Open();
                    string res = cmd.ExecuteScalar().ToString();
                    con.Close();
                    int tr = 0;
                    int.TryParse(res, out tr);
                    return tr;
                }
            }
        }
    }
}
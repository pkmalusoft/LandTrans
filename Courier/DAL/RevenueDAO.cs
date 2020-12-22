using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using LTMSV2.Models;
namespace LTMSV2.DAL
{
    public class RevenueDAO
    {
        public static List<RevenueUpdateMasterVM> GetRevenueUpdateList(string ConsignmentNote,DateTime FromDate,DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetRevenueUpdateList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar);
            cmd.Parameters["@FromDate"].Value = FromDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar);
            cmd.Parameters["@ToDate"].Value = FromDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ConsignmentNo", SqlDbType.VarChar);
            cmd.Parameters["@ConsignmentNo"].Value = ConsignmentNote;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<RevenueUpdateMasterVM> objList = new List<RevenueUpdateMasterVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RevenueUpdateMasterVM obj = new RevenueUpdateMasterVM();
                    obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.ConsignmentNo = ds.Tables[0].Rows[i]["ConsignmentNo"].ToString();
                    obj.ConsignmentDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ConsignmentDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.Currency = ds.Tables[0].Rows[i]["CurrencyName"].ToString();
                    obj.PaymentType = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["PartyName"].ToString();
                    obj.InvoicedTo = ds.Tables[0].Rows[i]["CustomerName"].ToString();
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }                    
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
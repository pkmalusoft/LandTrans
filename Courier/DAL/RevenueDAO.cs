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
            cmd.Parameters["@ToDate"].Value = ToDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ConsignmentNo", SqlDbType.VarChar);
            if (ConsignmentNote == null)
                ConsignmentNote = "";
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
                    obj.InvoiceTo  = ds.Tables[0].Rows[i]["InvoiceTo"].ToString();
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["CreditAccountHead"].ToString();
                    obj.CustomerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
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

        public static List<RevenueUpdateDetailVM> GetRevenueUpdateDetail(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetRevenueUpdateDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = ID;                        
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<RevenueUpdateDetailVM> objList = new List<RevenueUpdateDetailVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RevenueUpdateDetailVM obj = new RevenueUpdateDetailVM();
                    obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.MasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["MasterID"].ToString());
                    obj.CurrencyId= CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CurrencyId"].ToString());
                    obj.ExchangeRate = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ExchangeRate"].ToString());
                    obj.RevenueCostMasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RevenueCostMasterID"].ToString());
                    obj.RevenueCost = ds.Tables[0].Rows[i]["RevenueComponent"].ToString();
                    obj.CustomerId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CustomerId"].ToString());
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    obj.AcHeadDebitId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadDebitId"].ToString());
                    obj.AcHeadCreditId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadCreditId"].ToString());
                    obj.PaymentModeId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["PaymentModeId"].ToString());
                    obj.InvoiceTo  = ds.Tables[0].Rows[i]["InvoiceTo"].ToString();
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["CreditAccountHead"].ToString();                    
                    obj.Currency = ds.Tables[0].Rows[i]["CurrencyName"].ToString();                    
                    obj.CustomerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
                    obj.PaymentType = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static List<CostUpdateMasterVM> GetCostUpdateList(DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCostUpdateList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar);
            cmd.Parameters["@FromDate"].Value = FromDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar);
            cmd.Parameters["@ToDate"].Value = ToDate.ToString("MM/dd/yyyy");
                        
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<CostUpdateMasterVM> objList = new List<CostUpdateMasterVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CostUpdateMasterVM obj = new CostUpdateMasterVM();
                    obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.TDNo = ds.Tables[0].Rows[i]["ReceiptNo"].ToString();
                    obj.TDDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TDDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.Currency = ds.Tables[0].Rows[i]["CurrencyName"].ToString();                    
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["CreditAccountHead"].ToString();
                    obj.InvoicedTo = ds.Tables[0].Rows[i]["SupplierName"].ToString();
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
        public static List<CostUpdateDetailVM> GetCostUpdateDetail(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCostUpdateDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = ID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<CostUpdateDetailVM> objList = new List<CostUpdateDetailVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CostUpdateDetailVM obj = new CostUpdateDetailVM();
                    obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.MasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["MasterID"].ToString());
                    obj.CurrencyId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CurrencyId"].ToString());
                    obj.ExchangeRate = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ExchangeRate"].ToString());
                    obj.RevenueCostMasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RevenueCostMasterID"].ToString());
                    obj.RevenueCost = ds.Tables[0].Rows[i]["RevenueComponent"].ToString();
                    obj.SupplierId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["SupplierId"].ToString());
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    obj.AcHeadDebitId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadDebitId"].ToString());
                    obj.AcHeadCreditId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadCreditId"].ToString());
                    //obj. = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["CreditAccountHead"].ToString();
                    obj.Currency = ds.Tables[0].Rows[i]["CurrencyName"].ToString();
                    obj.InvoicedTo = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
﻿using System;
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

        public static List<RevenueUpdateDetailVM> GetMandatoryRevenueUpdateDetail(int InScanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetInScanMandatoryRevenue";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InScanId", SqlDbType.Int);
            cmd.Parameters["@InScanId"].Value = InScanId;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<RevenueUpdateDetailVM> objList = new List<RevenueUpdateDetailVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RevenueUpdateDetailVM obj = new RevenueUpdateDetailVM();
                    obj.ID = 0;
                    obj.MasterID = 0;
                    obj.CurrencyId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CurrencyId"].ToString());
                    obj.ExchangeRate = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ExchangeRate"].ToString());
                    obj.RevenueCostMasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RevenueCostMasterID"].ToString());
                    obj.RevenueCost = ds.Tables[0].Rows[i]["RevenueComponent"].ToString();
                    obj.CustomerId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CustomerId"].ToString());
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    obj.AcHeadDebitId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadDebitId"].ToString());
                    obj.AcHeadCreditId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadCreditId"].ToString());
                    obj.PaymentModeId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["PaymentModeId"].ToString());
                    obj.InvoiceTo = ds.Tables[0].Rows[i]["InvoiceTo"].ToString();
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
                    obj.TaxPercent = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxPercent"].ToString());
                    obj.TaxAmount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxAmount"].ToString());
                    obj.TotalCharge = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TotalCharge"].ToString());
                    obj.AcHeadDebitId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadDebitId"].ToString());
                    obj.AcHeadCreditId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadCreditId"].ToString());
                    obj.PaymentModeId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["PaymentModeId"].ToString());
                    obj.InvoiceTo  = ds.Tables[0].Rows[i]["InvoiceTo"].ToString();
                    obj.DebitAccountName = ds.Tables[0].Rows[i]["DebitAccountHead"].ToString();
                    obj.CreditAccountName = ds.Tables[0].Rows[i]["CreditAccountHead"].ToString();                    
                    obj.Currency = ds.Tables[0].Rows[i]["CurrencyName"].ToString();                    
                    obj.CustomerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
                    obj.PaymentType = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                    obj.InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"].ToString();
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
                    //obj.InvoicedTo = ds.Tables[0].Rows[i]["SupplierName"].ToString();
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


        public static List<TruckAssignVM> GetTruckDetailConsignments(string TDHNo, string ConsignmentNote, DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetTruckAssignList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar);
            cmd.Parameters["@FromDate"].Value = FromDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar);
            cmd.Parameters["@ToDate"].Value = ToDate.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@ConsignmentNo", SqlDbType.VarChar);
            if (ConsignmentNote == null)
                ConsignmentNote = "";
            cmd.Parameters["@ConsignmentNo"].Value = ConsignmentNote;

            cmd.Parameters.Add("@TDHNo", SqlDbType.VarChar);
            if (TDHNo == null)
                TDHNo = "";
            cmd.Parameters["@TDHNo"].Value = TDHNo;
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<TruckAssignVM> objList = new List<TruckAssignVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TruckAssignVM obj = new TruckAssignVM();
                    obj.TruckDetailId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["TruckDetailId"].ToString());
                    obj.ReceiptNo = ds.Tables[0].Rows[i]["ReceiptNo"].ToString();
                    obj.ConsignmentNo= ds.Tables[0].Rows[i]["ConsignmentNo"].ToString();
                    obj.TDDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TDDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.RouteName = ds.Tables[0].Rows[i]["RouteName"].ToString();
                    obj.VechileRegistrationNo = ds.Tables[0].Rows[i]["RegNo"].ToString();
                    obj.Rent= Convert.ToDecimal(ds.Tables[0].Rows[i]["Rent"].ToString());
                    obj.OtherCharges = Convert.ToDecimal(ds.Tables[0].Rows[i]["OtherCharge"].ToString());
                    obj.TotalCharge = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalCharge"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static List<CustomerInvoiceDetailVM> GenerateInvoice(DateTime FromDate,DateTime ToDate, int CustomerId,int FYearId,int InvoiceId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GenerateInvoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@FYearId", FYearId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<CustomerInvoiceDetailVM> objList = new List<CustomerInvoiceDetailVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CustomerInvoiceDetailVM obj = new CustomerInvoiceDetailVM();
                    obj.CustomerInvoiceDetailID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CustomerInvoiceDetailID"].ToString());
                    obj.CustomerInvoiceID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CustomerInvoiceID"].ToString());
                    obj.InScanID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["InScanId"].ToString());
                    obj.ConsignmentNo = ds.Tables[0].Rows[i]["ConsignmentNo"].ToString();
                    obj.AWBDateTime =Convert.ToDateTime(ds.Tables[0].Rows[i]["AWBDateTime"].ToString());                    
                    obj.FreightCharge = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["FreightCharge"].ToString());
                    obj.DocCharge = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["DocCharge"].ToString());
                    obj.CustomsCharge = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["CustomsCharge"].ToString());
                    obj.OtherCharge = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["OtherCharge"].ToString());
                    obj.TotalCharges = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TotalCharges"].ToString());
                    obj.ConsigneeName = ds.Tables[0].Rows[i]["Consignee"].ToString();
                    obj.ConsigneeCountryName = ds.Tables[0].Rows[i]["ConsigneeCountryName"].ToString();
                    obj.Origin = ds.Tables[0].Rows[i]["Consignor"].ToString();
                    obj.ConsigneeName= ds.Tables[0].Rows[i]["Consignee"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;

        }
    }
}
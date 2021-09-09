using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LTMSV2.Models;

namespace LTMSV2.DAL
{
    public class ReceiptDAO
    {
        //CustomerInvoiceDetailForReceipt
        public static List<ReceiptVM> GetCustomerReceipts(int FYearId, DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetAllRecieptsDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@FYearId", FYearId);

            //cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            //cmd.Parameters["@AcJournalDetailID"].Value = AcJournalDetailID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<ReceiptVM> objList = new List<ReceiptVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ReceiptVM obj = new ReceiptVM();
                    obj.RecPayID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RecPayID"].ToString());
                    obj.RecPayDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.DocumentNo = ds.Tables[0].Rows[i]["DocumentNo"].ToString();
                    obj.PartyName = ds.Tables[0].Rows[i]["PartyName"].ToString();
                    
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    obj.Currency = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Currency"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static List<ReceiptVM> GetCustomerReceiptsByDate(DateTime FromDate, DateTime ToDate, int FyearID, string ReceiptNo)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetAllRecieptsDetailsByDate";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Todate", Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@FyearId", FyearID);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            cmd.Parameters.AddWithValue("@ReceiptNo", ReceiptNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<ReceiptVM> objList = new List<ReceiptVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ReceiptVM obj = new ReceiptVM();
                    obj.RecPayID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RecPayID"].ToString());
                    obj.RecPayDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.DocumentNo = ds.Tables[0].Rows[i]["DocumentNo"].ToString();
                    obj.PaymentMode = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                    obj.BankName = ds.Tables[0].Rows[i]["BankName"].ToString();
                    obj.PartyName = ds.Tables[0].Rows[i]["PartyName"].ToString();
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    obj.Currency = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Currency"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static List<ReceiptVM> GetSupplierPaymentsByDate(DateTime FromDate, DateTime ToDate, int FyearID, string ReceiptNo)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetAllPaymentDetailsByDate";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Todate", Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@FyearId", FyearID);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            cmd.Parameters.AddWithValue("@ReceiptNo", ReceiptNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<ReceiptVM> objList = new List<ReceiptVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ReceiptVM obj = new ReceiptVM();
                    obj.RecPayID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RecPayID"].ToString());
                    obj.RecPayDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.DocumentNo = ds.Tables[0].Rows[i]["DocumentNo"].ToString();
                    obj.PaymentMode = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                    obj.BankName = ds.Tables[0].Rows[i]["BankName"].ToString();
                    obj.PartyName = ds.Tables[0].Rows[i]["PartyName"].ToString();
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    obj.Currency = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Currency"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static List<CustomerInvoiceDetailForReceipt> GetCustomerInvoiceDetailsForReciept(int CustomerID,string FromDate,string ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerInvoiceDetailsForReciept";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters["@CustomerID"].Value = CustomerID;
            cmd.Parameters["@FromDate"].Value =Convert.ToDateTime(FromDate).Date;
            cmd.Parameters["@ToDate"].Value = Convert.ToDateTime(ToDate).Date;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<CustomerInvoiceDetailForReceipt> objList = new List<CustomerInvoiceDetailForReceipt>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CustomerInvoiceDetailForReceipt obj = new CustomerInvoiceDetailForReceipt();
                    obj.InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"].ToString();
                    obj.CustomerInvoiceID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["InvoiceNo"].ToString());                
                    obj.InvoiceDate = CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    obj.CurrencyName = ds.Tables[0].Rows[i]["CurrencyName"].ToString();                    
                    
                    if (ds.Tables[0].Rows[i]["AmountToBeReceived"] == DBNull.Value)
                    {
                        obj.AmountToBeReceived = 0;
                    }
                    else
                    {
                        obj.AmountToBeReceived = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["AmountToBeReceived"].ToString());
                    }
                    
                    if (ds.Tables[0].Rows[i]["AmtPaidTillDate"] == DBNull.Value)
                    {
                        obj.AmtPaidTillDate = 0;
                    }
                    else
                    {
                        obj.AmtPaidTillDate = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["AmtPaidTillDate"].ToString());
                    }

                    if (ds.Tables[0].Rows[i]["Balance"] == DBNull.Value)
                    {
                        obj.Balance = 0;
                    }
                    else
                    {
                        obj.Balance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Balance"].ToString());
                    }

                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }


                    if (ds.Tables[0].Rows[i]["Advance"] == DBNull.Value)
                    {
                        obj.Advance = 0;
                    }
                    else
                    {
                        obj.Advance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Advance"].ToString());
                    }
                    


                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static int AddCustomerRecieptPayment(CustomerRcieptVM RecPy, string UserID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertRecPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayDate", RecPy.RecPayDate);
            cmd.Parameters.AddWithValue("@DocumentNo", RecPy.DocumentNo);
            cmd.Parameters.AddWithValue("@CustomerID", RecPy.CustomerID);
            //cmd.Parameters.AddWithValue("@SupplierID", RecPy.SupplierID);
            cmd.Parameters.AddWithValue("@BusinessCentreID", RecPy.BusinessCentreID);
            cmd.Parameters.AddWithValue("@BankName", RecPy.BankName);
            cmd.Parameters.AddWithValue("@ChequeNo", RecPy.ChequeNo);
            cmd.Parameters.AddWithValue("@ChequeDate", RecPy.ChequeDate);
            cmd.Parameters.AddWithValue("@Remarks", RecPy.Remarks);
            cmd.Parameters.AddWithValue("@AcJournalID", RecPy.AcJournalID);
            cmd.Parameters.AddWithValue("@StatusRec", RecPy.StatusRec);
            cmd.Parameters.AddWithValue("@StatusEntry", RecPy.StatusEntry);
            cmd.Parameters.AddWithValue("@StatusOrigin", RecPy.StatusOrigin);
            cmd.Parameters.AddWithValue("@FYearID", RecPy.FYearID);
            cmd.Parameters.AddWithValue("@AcCompanyID", RecPy.AcCompanyID);
            cmd.Parameters.AddWithValue("@EXRate", RecPy.EXRate);
            cmd.Parameters.AddWithValue("@FMoney", RecPy.FMoney);
            cmd.Parameters.AddWithValue("@UserID", RecPy.UserID);
            cmd.Parameters.AddWithValue("@EntryTime", CommanFunctions.GetCurrentDateTime());
            cmd.Parameters.AddWithValue("@AcOPInvoiceDetailID", RecPy.AcOPInvoiceDetailID);
            if (RecPy.PaymentRef==null)
                cmd.Parameters.AddWithValue("@PaymentRef", "");
            else
                cmd.Parameters.AddWithValue("@PaymentRef", RecPy.PaymentRef);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count>0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }
        public static int AddSupplierRecieptPayment(CustomerRcieptVM RecPy, string UserID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertSupplierRecPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayDate", RecPy.RecPayDate);
            cmd.Parameters.AddWithValue("@DocumentNo", RecPy.DocumentNo);
            //cmd.Parameters.AddWithValue("@CustomerID", RecPy.CustomerID);
            cmd.Parameters.AddWithValue("@SupplierID", RecPy.SupplierID);
            cmd.Parameters.AddWithValue("@BusinessCentreID", RecPy.BusinessCentreID);
            cmd.Parameters.AddWithValue("@BankName", RecPy.BankName);
            cmd.Parameters.AddWithValue("@ChequeNo", RecPy.ChequeNo);
            cmd.Parameters.AddWithValue("@ChequeDate", RecPy.ChequeDate);
            cmd.Parameters.AddWithValue("@Remarks", RecPy.Remarks);
            cmd.Parameters.AddWithValue("@AcJournalID", RecPy.AcJournalID);
            cmd.Parameters.AddWithValue("@StatusRec", RecPy.StatusRec);
            cmd.Parameters.AddWithValue("@StatusEntry", RecPy.StatusEntry);
            cmd.Parameters.AddWithValue("@StatusOrigin", RecPy.StatusOrigin);
            cmd.Parameters.AddWithValue("@FYearID", RecPy.FYearID);
            cmd.Parameters.AddWithValue("@AcCompanyID", RecPy.AcCompanyID);
            cmd.Parameters.AddWithValue("@EXRate", RecPy.EXRate);
            cmd.Parameters.AddWithValue("@FMoney", RecPy.FMoney);
            cmd.Parameters.AddWithValue("@TruckDetailId", RecPy.TruckDetailId);
            cmd.Parameters.AddWithValue("@UserID", RecPy.UserID);
            cmd.Parameters.AddWithValue("@UpdateDate", CommanFunctions.GetCurrentDateTime().ToString("MM/dd/yyyy HH:mm"));
            
            cmd.Parameters.AddWithValue("@AcOPInvoiceDetailId", RecPy.AcOPInvoiceDetailID);
            if (RecPy.PaymentRef == null)
                RecPy.PaymentRef = "";
            cmd.Parameters.AddWithValue("@PaymentRef", RecPy.PaymentRef);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }
        public static List<CustomerReceivable> SPGetAllLocalCurrencyCustRecievable(int FinancialyearId)
        {
            List<CustomerReceivable> crecs = new List<CustomerReceivable>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertRecPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcFinancialYearID", FinancialyearId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CustomerReceivable crec = new CustomerReceivable();
                    crec.InvoiceId = Convert.ToInt32(ds.Tables[0].Rows[i]["InvoiceId"].ToString());
                    crec.Receivable = Convert.ToDecimal(ds.Tables[0].Rows[i]["Receivable"].ToString());
                    crecs.Add(crec);
                }
            }
            else
            {
                
            }

            return crecs;
        }

        public static decimal SP_GetCustomerAdvance(int CustomerId,int RecPayId,int FyearId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerAdvance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@FYearId", FyearId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return  Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }

        public static decimal SP_GetCustomerInvoiceReceived(int CustomerId,int InvoiceId, int RecPayId,int CreditNoteId, string Type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerInvoiceReceived";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);             
            cmd.Parameters.AddWithValue("@Type", Type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }

        public static decimal SP_GetSupplierAdvance(int SupplierId, int RecPayId,int FyearId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierAdvance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@FYearId", FyearId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }

        public static decimal SP_GetSupplierInvoicePaid(int SupplierId, int InvoiceId, int RecPayId, int DebitNoteId, string Type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierInvoicePaid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@DebitNoteId", DebitNoteId);            
            cmd.Parameters.AddWithValue("@Type", Type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }
        //public static CustomerRcieptVM GetRecPayByRecpayID(int RecpayID)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    CustomerRcieptVM cust = new CustomerRcieptVM();
        //    cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
        //    cmd.CommandText = "SP_GetCustomerRecieptByRecPayID";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters["@RecPayID"].Value = RecpayID;
        //    if (RecpayID <= 0)
        //        return new CustomerRcieptVM();
        //    var query = Context1.SP_GetCustomerRecieptByRecPayID(RecpayID);

        //    if (query != null)
        //    {
        //        var item = query.FirstOrDefault();
        //        cust.RecPayDate = item.RecPayDate;
        //        cust.DocumentNo = item.DocumentNo;
        //        cust.CustomerID = item.CustomerID;

        //        var cashOrBankID = (from t in Context1.AcHeads where t.AcHead1 == item.BankName select t.AcHeadID).FirstOrDefault();
        //        cust.CashBank = (cashOrBankID).ToString();
        //        cust.ChequeBank = (cashOrBankID).ToString();
        //        cust.ChequeNo = item.ChequeNo;
        //        cust.ChequeDate = item.ChequeDate;
        //        cust.Remarks = item.Remarks;
        //        cust.EXRate = item.EXRate;
        //        cust.FMoney = item.FMoney;
        //        cust.RecPayID = item.RecPayID;
        //        cust.SupplierID = item.SupplierID;
        //        cust.AcJournalID = item.AcJournalID;
        //        cust.StatusEntry = item.StatusEntry;

        //        var a = (from t in Context1.RecPayDetails where t.RecPayID == RecpayID select t.CurrencyID).FirstOrDefault();
        //        cust.CurrencyId = Convert.ToInt32(a.HasValue ? a.Value : 0);



        //    }

        //    else
        //    {
        //        return new CustomerRcieptVM();
        //    }

        //    return cust;
        //}

        public static string SP_GetMaxPVID()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetMaxPVID";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }
        public static int InsertRecpayDetailsForCust(int RecPayID, int InvoiceID, int JInvoiceID, decimal Amount, string Remarks, string StatusInvoice, bool StatusAdvance, string statusReceip, string InvDate, string InvNo, int CurrencyID, int invoiceStatus, decimal AdjustmentAmount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertRecPayDetailsForCustomer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecPayID);
            cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@StatusInvoice",StatusInvoice);
            cmd.Parameters.AddWithValue("@StatusAdvance", StatusAdvance);
            cmd.Parameters.AddWithValue("@statusReceipt", statusReceip);
            cmd.Parameters.AddWithValue("@InvDate", InvDate);
            cmd.Parameters.AddWithValue("@InvNo", InvNo);
            cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);            
            cmd.Parameters.AddWithValue("@invoiceStatus", invoiceStatus);
            cmd.Parameters.AddWithValue("@AdjustmentAmount", AdjustmentAmount);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

                                   
        }
        public static int InsertRecpayDetailsForSupplier(int RecPayID, int InvoiceID, int JInvoiceID, decimal Amount, string Remarks, string StatusInvoice, bool StatusAdvance, string statusReceip, string InvDate, string InvNo, int CurrencyID, int invoiceStatus, int JobID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertRecPayDetailsForSupplier";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecPayID);
            cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@StatusInvoice", StatusInvoice);
            cmd.Parameters.AddWithValue("@StatusAdvance", StatusAdvance);
            cmd.Parameters.AddWithValue("@statusReceipt", statusReceip);
            cmd.Parameters.AddWithValue("@InvDate", InvDate);
            cmd.Parameters.AddWithValue("@InvNo", InvNo);
            cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
            cmd.Parameters.AddWithValue("@invoiceStatus", invoiceStatus);
            //cmd.Parameters.AddWithValue("@InScanId", JobID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }

        public static void InsertJournalOfCustomer(int RecpayID, int fyearId)
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertJournalEntryForRecPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecpayID);
            cmd.Parameters.AddWithValue("@AcFinnancialYearId",fyearId);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            
            //Context1.SP_InsertJournalEntryForRecPay(RecpayID, fyaerId);
        }

        public static List<CustomerTradeReceiptVM> SP_GetCustomerInvoicePending(int CustomerId, int InvoiceId, int RecPayId, int CreditNoteId, string Type)
        {
            List<CustomerTradeReceiptVM> list = new List<CustomerTradeReceiptVM>();
            CustomerTradeReceiptVM item = new CustomerTradeReceiptVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(HttpContext.Current.Session["fyearid"].ToString());
            int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerInvoicePending";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@FYearId", yearid);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drrow = ds.Tables[0].Rows[i];
                    item = new CustomerTradeReceiptVM();
                    item.SalesInvoiceID = Convert.ToInt32(drrow["InvoiceID"].ToString());
                    item.InvoiceNo = drrow["InvoiceNo"].ToString();
                    item.InvoiceType = drrow["TransType"].ToString();
                    item.DateTime = Convert.ToDateTime(drrow["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                    item.InvoiceAmount = Convert.ToDecimal(drrow["InvoiceAmount"].ToString());
                    item.AmountReceived = Convert.ToDecimal(drrow["ReceivedAmount"].ToString());
                    item.Balance = Convert.ToDecimal(drrow["Balance"].ToString());

                    list.Add(item);

                }
            }

            return list;

        }

        public static List<CustomerTradeReceiptVM> SP_GetCustomerReceiptPending(int CustomerId, int InvoiceId, int RecPayId, int CreditNoteId, string Type)
        {
            List<CustomerTradeReceiptVM> list = new List<CustomerTradeReceiptVM>();
            CustomerTradeReceiptVM item = new CustomerTradeReceiptVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(HttpContext.Current.Session["fyearid"].ToString());
            int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerReceiptPending";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@FYearId", yearid);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drrow = ds.Tables[0].Rows[i];
                    item = new CustomerTradeReceiptVM();
                    item.SalesInvoiceID = Convert.ToInt32(drrow["InvoiceID"].ToString());
                    item.InvoiceNo = drrow["InvoiceNo"].ToString();
                    item.DateTime = Convert.ToDateTime(drrow["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                    item.InvoiceType = drrow["TransType"].ToString();
                    item.InvoiceAmount = Convert.ToDecimal(drrow["InvoiceAmount"].ToString());
                    item.AmountReceived = Convert.ToDecimal(drrow["ReceivedAmount"].ToString());
                    item.Balance = Convert.ToDecimal(drrow["Balance"].ToString());

                    list.Add(item);

                }
            }

            return list;

        }

        public static List<CustomerTradeReceiptVM> SP_GetSupplierInvoicePending(int SupplierId, int InvoiceId, int RecPayId, int DebitNoteId, string Type)
        {
            List<CustomerTradeReceiptVM> list = new List<CustomerTradeReceiptVM>();
            CustomerTradeReceiptVM item = new CustomerTradeReceiptVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(HttpContext.Current.Session["fyearid"].ToString());
            int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierInvoicePending";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@DebitNoteId", DebitNoteId);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@FYearId", yearid);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drrow = ds.Tables[0].Rows[i];
                    item = new CustomerTradeReceiptVM();
                    item.SalesInvoiceID = Convert.ToInt32(drrow["InvoiceID"].ToString());
                    item.InvoiceNo = drrow["InvoiceNo"].ToString();
                    item.InvoiceType = drrow["TransType"].ToString();
                    item.DateTime = Convert.ToDateTime(drrow["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                    item.InvoiceAmount = Convert.ToDecimal(drrow["InvoiceAmount"].ToString());
                    item.AmountReceived = Convert.ToDecimal(drrow["ReceivedAmount"].ToString());
                    item.Balance = Convert.ToDecimal(drrow["Balance"].ToString());

                    list.Add(item);

                }
            }

            return list;

        }

        public static List<CustomerTradeReceiptVM> SP_GetSupplierReceiptPending(int SupplierID, int InvoiceId, int RecPayId, int CreditNoteId, string Type)
        {
            List<CustomerTradeReceiptVM> list = new List<CustomerTradeReceiptVM>();
            CustomerTradeReceiptVM item = new CustomerTradeReceiptVM();
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(HttpContext.Current.Session["fyearid"].ToString());
            int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierPaymentPending";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierID);
            cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);
            cmd.Parameters.AddWithValue("@DebitNoteId", CreditNoteId);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@FYearId", yearid);
            cmd.Parameters.AddWithValue("@BranchId", branchid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drrow = ds.Tables[0].Rows[i];
                    item = new CustomerTradeReceiptVM();
                    item.SalesInvoiceID = Convert.ToInt32(drrow["InvoiceID"].ToString());
                    item.InvoiceNo = drrow["InvoiceNo"].ToString();
                    item.InvoiceType = drrow["TransType"].ToString();
                    item.DateTime = Convert.ToDateTime(drrow["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                    item.InvoiceAmount = Convert.ToDecimal(drrow["InvoiceAmount"].ToString());
                    item.AmountReceived = Convert.ToDecimal(drrow["ReceivedAmount"].ToString());
                    item.Balance = Convert.ToDecimal(drrow["Balance"].ToString());

                    list.Add(item);

                }
            }

            return list;

        }
        public static List<OpeningInvoiceVM> GetCustomerOpeningReceipts(int CustomerId, int BranchId, int FYearId)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerOpeningCredit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@FYearId", FYearId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<OpeningInvoiceVM> objList = new List<OpeningInvoiceVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    OpeningInvoiceVM obj = new OpeningInvoiceVM();
                    obj.ACOPInvoiceDetailId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ACOPInvoiceDetailId"].ToString());
                    obj.CustomerId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CustomerId"].ToString());
                    //obj.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.RefNo = ds.Tables[0].Rows[i]["RefNo"].ToString();

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
        public static List<OpeningInvoiceVM> GetSupplierOpeningPayments(int SupplierId, int BranchId, int FYearId)
        {
            int branchid = Convert.ToInt32(HttpContext.Current.Session["CurrentBranchID"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierOpeningDebit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@FYearId", FYearId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<OpeningInvoiceVM> objList = new List<OpeningInvoiceVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    OpeningInvoiceVM obj = new OpeningInvoiceVM();
                    obj.ACOPInvoiceDetailId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ACOPInvoiceDetailId"].ToString());
                    obj.SupplierId = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["SupplierId"].ToString());
                    //obj.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.RefNo = ds.Tables[0].Rows[i]["RefNo"].ToString();

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


        public static void InsertJournalOfSupplier(int RecpayID, int fyearId)
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_InsertJournalEntryForSupplierRecPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecpayID);
            cmd.Parameters.AddWithValue("@AcFinnancialYearId", fyearId);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            //Context1.SP_InsertJournalEntryForRecPay(RecpayID, fyaerId);
        }
        public static void ReSaveSupplierCode()
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_ReSaveSupplierCode";
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            //Context1.SP_InsertJournalEntryForRecPay(RecpayID, fyaerId);
        }

        public static void ReSaveCustomerCode()
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_ReSaveCustomerCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();            
        }

        public static string GetMaxCustomerCode(string CustomerName)
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);            
            cmd.CommandText = "SP_GetMaxCustomerCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerName",CustomerName);
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);            
            if (ds.Tables[0].Rows.Count > 0)
            {
                string cutomercode = ds.Tables[0].Rows[0][0].ToString();

                return cutomercode;
            }
            else
            {
                return "";
            }
        }
        public static void ReSaveEmployeeCode()
        {
            //SP_InsertJournalEntryForRecPay
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_ReSaveEmployeeCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            //Context1.SP_InsertJournalEntryForRecPay(RecpayID, fyaerId);
        }
        public static DataTable DeleteCustomerReceipt(int RecPayID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteCustomerReciepts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecPayID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        //SP_DeleteCODCustomerReciepts

        public static DataTable DeleteCOdCustomerReceipt(int RecPayID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteCODCustomerReciepts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecPayID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static DataTable DeleteAccountHead(int AcheadId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteAcHead";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcHeadId", AcheadId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static DataTable DeleteDriver(int DriverId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteDriver";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DriverId", DriverId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static DataTable DeleteCustomer(int CustomerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteCustomer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static DataTable DeleteInvoice(int InvoiceId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteCustomerInvoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerInvoiceId", InvoiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }

        public static DataTable DeleteInscan(int InscanId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteInscan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InScanId", InscanId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        
        //Delete  TruckDetails
        public static DataTable DeleteTruckDetail(int TruckDetailId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteTruckDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TruckDetailId", TruckDetailId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        //SP_DeleteSupplierPayments
        public static DataTable DeleteSupplierPayments(int RecPayID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteSupplierPayments";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecPayID", RecPayID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static List<ReceiptAllocationDetailVM> GetAWBAllocation(List<ReceiptAllocationDetailVM> list, int InvoiceId, decimal Amount, int RecpayId)
        {
            try
            {
                if (list == null)
                    list = new List<ReceiptAllocationDetailVM>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "SP_GetInvoiceAWBAllocation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                cmd.Parameters.AddWithValue("@ReceivedAmount", Amount);
                cmd.Parameters.AddWithValue("@RecPayId", RecpayId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow drrow = ds.Tables[0].Rows[i];
                        ReceiptAllocationDetailVM item = new ReceiptAllocationDetailVM();
                        item.ID = Convert.ToInt32(drrow["ID"].ToString());
                        item.CustomerInvoiceId = Convert.ToInt32(drrow["CustomerInvoiceId"].ToString());
                        item.CustomerInvoiceDetailID = Convert.ToInt32(drrow["CustomerInvoiceDetailID"].ToString());
                        item.InScanID = Convert.ToInt32(drrow["InScanId"].ToString());
                        item.RecPayID = Convert.ToInt32(drrow["RecPayID"].ToString());
                        item.RecPayDetailID = Convert.ToInt32(drrow["RecPayDetailID"].ToString());
                        item.CustomerInvoiceDetailID = Convert.ToInt32(drrow["CustomerInvoiceDetailID"].ToString());
                        item.AWBNo = drrow["AWBNo"].ToString();
                        item.AWBDate = Convert.ToDateTime(drrow["AWBDate"].ToString()).ToString("dd-MM-yyyy");
                        item.TotalAmount = Convert.ToDecimal(drrow["TotalAmount"].ToString());
                        item.ReceivedAmount = Convert.ToDecimal(drrow["ReceivedAmount"].ToString());
                        item.PendingAmount = Convert.ToDecimal(drrow["PendingAmount"].ToString());
                        item.AllocatedAmount = Convert.ToDecimal(drrow["AllocatedAmount"].ToString());
                        item.Allocated = Convert.ToBoolean(drrow["Allocated"].ToString());

                        list.Add(item);

                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        #region "customerInvoice"
        public static string GetCustomerInvoiceTotal(DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCustomerInvoiceTotal";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }


        }
        #endregion
        #region "supplierInvoice"
        public static string GetSupplierInvoiceTotal(DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetSupplierInvoiceTotal";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }


        }
        public static DataTable DeleteSupplierInvoice(int InvoiceId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteSupplierInvoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierInvoiceId", InvoiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        public static string SP_GetMaxSINo(int TypeId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetMaxSupplierInvoiceNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region "Cod REceipt"
        public static string SP_GetMaxCODID()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetMaxCODID";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //int query = Context1.SP_InsertRecPay(RecPy.RecPayDate, RecPy.DocumentNo, RecPy.CustomerID, RecPy.SupplierID, RecPy.BusinessCentreID, RecPy.BankName, RecPy.ChequeNo, RecPy.ChequeDate, RecPy.Remarks, RecPy.AcJournalID, RecPy.StatusRec, RecPy.StatusEntry, RecPy.StatusOrigin, RecPy.FYearID, RecPy.AcCompanyID, RecPy.EXRate, RecPy.FMoney, Convert.ToInt32(UserID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }
        public static List<ReceiptVM> GetCODReceipts(int FYearId, DateTime FromDate, DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetAllCODRecieptsDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@FYearId", FYearId);

            //cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            //cmd.Parameters["@AcJournalDetailID"].Value = AcJournalDetailID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<ReceiptVM> objList = new List<ReceiptVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ReceiptVM obj = new ReceiptVM();
                    obj.RecPayID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["RecPayID"].ToString());
                    obj.RecPayDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["RecPayDate"].ToString()); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.DocumentNo = ds.Tables[0].Rows[i]["DocumentNo"].ToString();
                    obj.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    obj.Currency = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Currency"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }


        public static List<CustomerTradeReceiptVM> GetCODPending(int FYearId,int RecPayId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetCODPending";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@FromDate", FromDate.ToString("MM/dd/yyyy"));
            //cmd.Parameters.AddWithValue("@ToDate", ToDate.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@FYearId", FYearId);
            cmd.Parameters.AddWithValue("@RecPayId", RecPayId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            
            var custreceipt = new List<CustomerTradeReceiptVM>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CustomerTradeReceiptVM obj = new CustomerTradeReceiptVM();
                    obj.InScanID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["InScanID"].ToString());
                    obj.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransactionDate"].ToString()).ToString("dd-MM-yyyy"); // CommanFunctions.ParseDate(ds.Tables[0].Rows[i]["RecPayDate"].ToString());
                    obj.ConsignmentNo = ds.Tables[0].Rows[i]["ConsignmentNo"].ToString();
                    obj.InvoiceAmount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["InvoiceAmount"].ToString());
                    obj.AmountReceived = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ReceivedAmount"].ToString());
                    obj.Balance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["PendingAmount"].ToString());
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    custreceipt.Add(obj);
                }
            }
            return custreceipt;
        }
        #endregion

        #region "REvenueUpdate"
        public static DataTable DeleteRevenueUpdate(int InvoiceId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_DeleteRevenueUpdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RevenueUpdateMasterId", InvoiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }


        }
        #endregion
    }
}
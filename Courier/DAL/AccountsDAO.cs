using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using LTMSV2.Models;
using System.Configuration;

namespace LTMSV2.DAL
{
    public class AccountsDAO
    {

        public static int InsertOrUpdateAcBankDetails(AcBankDetail ObjectAcBankDetail, int isupdate)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            string strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            cmd.Connection = new SqlConnection(strConnString);
            cmd.CommandText = "SP_InsertOrUpdateAcBankDetails";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AcBankDetailID", SqlDbType.Int);
            cmd.Parameters["@AcBankDetailID"].Value = ObjectAcBankDetail.AcBankDetailID;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = ObjectAcBankDetail.AcJournalID;

            cmd.Parameters.Add("@BankName", SqlDbType.NVarChar);
            cmd.Parameters["@BankName"].Value = ObjectAcBankDetail.BankName;

            cmd.Parameters.Add("@ChequeNo", SqlDbType.NVarChar);
            cmd.Parameters["@ChequeNo"].Value = ObjectAcBankDetail.ChequeNo;

            if (ObjectAcBankDetail.ChequeDate != null)
            {
                cmd.Parameters.Add("@ChequeDate", SqlDbType.DateTime);
                cmd.Parameters["@ChequeDate"].Value = ObjectAcBankDetail.ChequeDate;
            }
            cmd.Parameters.Add("@PartyName", SqlDbType.NVarChar);
            cmd.Parameters["@PartyName"].Value = ObjectAcBankDetail.PartyName;

            cmd.Parameters.Add("@StatusTrans", SqlDbType.NVarChar);
            cmd.Parameters["@StatusTrans"].Value = ObjectAcBankDetail.StatusTrans;

            cmd.Parameters.Add("@IsUpdate", SqlDbType.Int);
            cmd.Parameters["@IsUpdate"].Value = isupdate;
            if (ObjectAcBankDetail.StatusReconciled != null)
            {
                cmd.Parameters.Add("@StatusReconciled", SqlDbType.Bit);
                cmd.Parameters["@StatusReconciled"].Value = ObjectAcBankDetail.StatusReconciled;
            }
            if (ObjectAcBankDetail.ValueDate != null)
            {
                cmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);
                cmd.Parameters["@ValueDate"].Value = ObjectAcBankDetail.ValueDate;
            }
            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public static List<AcJournalDetailsVM> GetAcJournalDetails(int AcJournalID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SELECT ah.AcHead,aj.Amount, aj.Remarks FROM AcJournalDetail as aj INNER JOIN AcHead as ah on aj.AcHeadID=ah.AcHeadID WHERE aj.AcJournalID = @AcJournalID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = AcJournalID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcJournalDetailsVM> objList = new List<AcJournalDetailsVM>();
            AcJournalDetailsVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcJournalDetailsVM();
                    obj.AcHead = ds.Tables[0].Rows[i]["AcHead"].ToString();
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    obj.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static int UpdateAcJournalDetail(AcJournalDetail ObjectAcJournalDetail)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "UPDATE AcJournalDetail SET AcJournalID=@AcJournalID,AcHeadID=@AcHeadID,AnalysisHeadID=@AnalysisHeadID,Amount=@Amount,Remarks=@Remarks,BranchID=@BranchID,AmountIncludingTax=@AmountIncludingTax,SupplierId=@SupplierId,TaxPercent=@TaxPercent,TaxAmount=@TaxAmount WHERE AcJournalDetailID = @AcJournalDetailID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcJournalDetailID"].Value = ObjectAcJournalDetail.AcJournalDetailID;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = ObjectAcJournalDetail.AcJournalID;

            cmd.Parameters.Add("@AcHeadID", SqlDbType.Int);
            cmd.Parameters["@AcHeadID"].Value = ObjectAcJournalDetail.AcHeadID;

            cmd.Parameters.Add("@AnalysisHeadID", SqlDbType.Int);
            cmd.Parameters["@AnalysisHeadID"].Value = ObjectAcJournalDetail.AnalysisHeadID;

            cmd.Parameters.Add("@Amount", SqlDbType.Money);
            cmd.Parameters["@Amount"].Value = ObjectAcJournalDetail.Amount;

            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar);
            cmd.Parameters["@Remarks"].Value = ObjectAcJournalDetail.Remarks;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = ObjectAcJournalDetail.BranchID;

            cmd.Parameters.Add("@AmountIncludingTax", SqlDbType.Bit);
            cmd.Parameters["@AmountIncludingTax"].Value = ObjectAcJournalDetail.AmountIncludingTax;

            cmd.Parameters.Add("@SupplierId", SqlDbType.Int);
            cmd.Parameters["@SupplierId"].Value = ObjectAcJournalDetail.SupplierId;

            cmd.Parameters.Add("@TaxPercent", SqlDbType.Money);
            cmd.Parameters["@TaxPercent"].Value = ObjectAcJournalDetail.TaxPercent;

            cmd.Parameters.Add("@TaxAmount", SqlDbType.Money);
            cmd.Parameters["@TaxAmount"].Value = ObjectAcJournalDetail.TaxAmount;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            return iReturn;
        }
        public static int DeleteAcJournalConsignments(AcJournalDetail ObjectAcJournalDetail)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "Delete from AcJournalConsignment where  AcJournalID=@AcJournalID and AcJournalDetailID = @AcJournalDetailID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcJournalDetailID"].Value = ObjectAcJournalDetail.AcJournalDetailID;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = ObjectAcJournalDetail.AcJournalID;                                    

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            return iReturn;
        }
        public static int DeleteAcJournalDetail(int AcJournalDetailID)
        {
            int iReturn = 0;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd2.CommandText = "DELETE FROM AcAnalysisHeadAllocation WHERE AcjournalDetailID = @AcjournalDetailID";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("@AcjournalDetailID", SqlDbType.Int);
            cmd2.Parameters["@AcjournalDetailID"].Value = AcJournalDetailID;
            try
            {
                cmd2.Connection.Open();
                iReturn = cmd2.ExecuteNonQuery();
                cmd2.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd3.CommandText = "DELETE FROM AcJournalConsignment WHERE AcJournalDetailID = @AcJournalDetailID";
            cmd3.CommandType = CommandType.Text;

            cmd3.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd3.Parameters["@AcJournalDetailID"].Value = AcJournalDetailID;

            try
            {
                cmd3.Connection.Open();
                iReturn = cmd3.ExecuteNonQuery();
                cmd3.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "DELETE FROM AcJournalDetail WHERE AcJournalDetailID = @AcJournalDetailID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcJournalDetailID"].Value = AcJournalDetailID;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            return iReturn;
        }
        public static int InsertAcJournalDetail(AcJournalDetail ObjectAcJournalDetail)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "INSERT INTO AcJournalDetail(AcJournalDetailID,AcJournalID,AcHeadID,Amount,Remarks,BranchID,AmountIncludingTax,SupplierId,TaxPercent,TaxAmount) VALUES(@AcJournalDetailID,@AcJournalID,@AcHeadID,@Amount,@Remarks,@BranchID,@AmountIncludingTax,@SupplierId,@TaxPercent,@TaxAmount)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcJournalDetailID"].Value = ObjectAcJournalDetail.AcJournalDetailID;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = ObjectAcJournalDetail.AcJournalID;

            cmd.Parameters.Add("@AcHeadID", SqlDbType.Int);
            cmd.Parameters["@AcHeadID"].Value = ObjectAcJournalDetail.AcHeadID;

            //   cmd.Parameters.Add("@AnalysisHeadID", SqlDbType.Int);
            //    cmd.Parameters["@AnalysisHeadID"].Value = ObjectAcJournalDetail.AnalysisHeadID;

            cmd.Parameters.Add("@Amount", SqlDbType.Money);
            cmd.Parameters["@Amount"].Value = ObjectAcJournalDetail.Amount;

            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar);
            cmd.Parameters["@Remarks"].Value = ObjectAcJournalDetail.Remarks;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = ObjectAcJournalDetail.BranchID;

            cmd.Parameters.Add("@AmountIncludingTax", SqlDbType.Bit);
            cmd.Parameters["@AmountIncludingTax"].Value = ObjectAcJournalDetail.AmountIncludingTax;
            
            cmd.Parameters.Add("@SupplierId", SqlDbType.Int);
            cmd.Parameters["@SupplierId"].Value = ObjectAcJournalDetail.SupplierId;

            cmd.Parameters.Add("@TaxPercent", SqlDbType.Money);
            cmd.Parameters["@TaxPercent"].Value = ObjectAcJournalDetail.TaxPercent;

            cmd.Parameters.Add("@TaxAmount", SqlDbType.Money);
            cmd.Parameters["@TaxAmount"].Value = ObjectAcJournalDetail.TaxAmount;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            return iReturn;
        }

        public static int UpdateAcAnalysisHeadAllocation(AcAnalysisHeadAllocation ObjectAcAnalysisHeadAllocation)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "UPDATE AcAnalysisHeadAllocation SET AcjournalDetailID=@AcjournalDetailID,AnalysisHeadID=@AnalysisHeadID,Amount=@Amount WHERE AcAnalysisHeadAllocationID=@AcAnalysisHeadAllocationID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcAnalysisHeadAllocationID", SqlDbType.Int);
            cmd.Parameters["@AcAnalysisHeadAllocationID"].Value = ObjectAcAnalysisHeadAllocation.AcAnalysisHeadAllocationID;

            cmd.Parameters.Add("@AcjournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcjournalDetailID"].Value = ObjectAcAnalysisHeadAllocation.AcjournalDetailID;

            cmd.Parameters.Add("@AnalysisHeadID", SqlDbType.Int);
            cmd.Parameters["@AnalysisHeadID"].Value = ObjectAcAnalysisHeadAllocation.AnalysisHeadID;

            cmd.Parameters.Add("@Amount", SqlDbType.Money);
            cmd.Parameters["@Amount"].Value = ObjectAcAnalysisHeadAllocation.Amount;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public static int InsertAcAnalysisHeadAllocation(AcAnalysisHeadAllocation ObjectAcAnalysisHeadAllocation)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "INSERT INTO AcAnalysisHeadAllocation(AcjournalDetailID,AnalysisHeadID,Amount) VALUES(@AcjournalDetailID,@AnalysisHeadID,@Amount)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcjournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcjournalDetailID"].Value = ObjectAcAnalysisHeadAllocation.AcjournalDetailID;

            cmd.Parameters.Add("@AnalysisHeadID", SqlDbType.Int);
            cmd.Parameters["@AnalysisHeadID"].Value = ObjectAcAnalysisHeadAllocation.AnalysisHeadID;

            cmd.Parameters.Add("@Amount", SqlDbType.Money);
            cmd.Parameters["@Amount"].Value = ObjectAcAnalysisHeadAllocation.Amount;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public static int DeleteAcAnalysisHeadAllocation(int AcAnalysisHeadAllocationID)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "DELETE FROM AcAnalysisHeadAllocation WHERE AcAnalysisHeadAllocationID=@AcAnalysisHeadAllocationID";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@AcAnalysisHeadAllocationID", SqlDbType.Int);
            cmd.Parameters["@AcAnalysisHeadAllocationID"].Value = AcAnalysisHeadAllocationID;

            try
            {
                cmd.Connection.Open();
                iReturn = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public static List<AcAnalysisHeadAllocationVM> GetAcJDetailsExpenseAllocation(int AcJournalDetailID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "GetAcJDetailsExpenseAllocation";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AcJournalDetailID", SqlDbType.Int);
            cmd.Parameters["@AcJournalDetailID"].Value = AcJournalDetailID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcAnalysisHeadAllocationVM> objList = new List<AcAnalysisHeadAllocationVM>();
            AcAnalysisHeadAllocationVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcAnalysisHeadAllocationVM();
                    obj.AcAnalysisHeadAllocationID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcAnalysisHeadAllocationID"].ToString());
                    obj.AcjournalDetailID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcjournalDetailID"].ToString());
                    obj.AnalysisHeadID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AnalysisHeadID"].ToString());
                    if (ds.Tables[0].Rows[i]["Amount"] == DBNull.Value)
                    {
                        obj.Amount = 0;
                    }
                    else
                    {
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    obj.AnalysisHead = ds.Tables[0].Rows[i]["AnalysisHead"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static List<AcHeadSelectAllVM> GetAcHeadSelectAll(int BranchID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "AcHeadSelectAll";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = BranchID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcHeadSelectAllVM> objList = new List<AcHeadSelectAllVM>();
            AcHeadSelectAllVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcHeadSelectAllVM();
                    obj.AcHeadID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadID"].ToString());
                    obj.AcHeadKey = ds.Tables[0].Rows[i]["AcHeadKey"].ToString();
                    obj.AcHead = ds.Tables[0].Rows[i]["AcHead"].ToString();
                    obj.AcGroupID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcGroupID"].ToString());
                    obj.ParentID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ParentID"].ToString());
                    obj.HeadOrder = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["HeadOrder"].ToString());
                    if (ds.Tables[0].Rows[i]["StatusHide"] == DBNull.Value)
                    {
                        obj.StatusHide = false;
                    }
                    else
                    {
                        obj.StatusHide = Convert.ToBoolean(ds.Tables[0].Rows[i]["StatusHide"].ToString());
                    }
                    obj.UserID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["UserID"].ToString());
                    obj.Prefix = ds.Tables[0].Rows[i]["Prefix"].ToString();
                    obj.AcGroup = ds.Tables[0].Rows[i]["AcGroup"].ToString();
                    obj.AccountType = ds.Tables[0].Rows[i]["AccountType"].ToString();
                    if (ds.Tables[0].Rows[i]["TaxApplicable"] == DBNull.Value)
                    {
                        obj.TaxApplicable = false;

                    }
                    else
                    {                        
                        obj.TaxApplicable = Convert.ToBoolean(ds.Tables[0].Rows[i]["TaxApplicable"].ToString());
                        if (obj.TaxApplicable==true)
                        {
                            obj.TaxPercent = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxPercent"].ToString());
                        }
                        else
                        {
                            obj.TaxPercent = 0;
                        }
                        
                    }


                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static List<AcHeadSelectAllVM> GetAcVoucherType(int BranchID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "Select distinct VoucherType from AcJOurnalMaster Where isnull(Vouchertype,'')<>''";
            cmd.CommandType = CommandType.Text;
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcHeadSelectAllVM> objList = new List<AcHeadSelectAllVM>();
            AcHeadSelectAllVM obj = new AcHeadSelectAllVM();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcHeadSelectAllVM();
                    obj.AcHead = ds.Tables[0].Rows[i]["VoucherType"].ToString();                                       
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static List<AcJournalDetailVM> AcJournalDetailSelectByAcJournalID(int AcJournalID, string PaymentType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "AcJournalDetailSelectByAcJournalID";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AcJournalID", SqlDbType.Int);
            cmd.Parameters["@AcJournalID"].Value = AcJournalID;

            cmd.Parameters.Add("@PaymentType", SqlDbType.VarChar);
            cmd.Parameters["@PaymentType"].Value = PaymentType;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcJournalDetailVM> objList = new List<AcJournalDetailVM>();
            AcJournalDetailVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcJournalDetailVM();
                    obj.AcJournalDetID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcJournalDetailID"].ToString());
                    obj.AcHeadID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadID"].ToString());
                    obj.TaxPercent = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxPercent"].ToString());
                    obj.TaxAmount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxAmount"].ToString());
                                        
                    if (ds.Tables[0].Rows[i]["AmountIncludingTax"]==null)
                    {
                        obj.AmountIncludingTax = false;                        
                    }
                    else
                    {
                        obj.AmountIncludingTax = Convert.ToBoolean(ds.Tables[0].Rows[i]["AmountIncludingTax"].ToString());
                    }

                    if (obj.AmountIncludingTax ==true && obj.TaxAmount>0)
                    {
                        obj.Amt = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString())+ obj.TaxAmount;
                    }
                    else
                    {
                        obj.Amt = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    }
                    
                    obj.Rem = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    obj.AcHead = ds.Tables[0].Rows[i]["AcHead"].ToString();
                    obj.SupplierID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupplierId"].ToString());
                    obj.SupplierName = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                    objList.Add(obj);
                }
            }
            return objList;
        }

        //index jv voucher book
        public static List<AcJournalMasterVM> AcJournalMasterSelect(int FYearId, int BranchID,DateTime FromDate,DateTime ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "AcJournalMasterSelectAllJVNew";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FYearID", SqlDbType.Int);
            cmd.Parameters["@FYearID"].Value = FYearId;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = BranchID;

            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar);
            cmd.Parameters["@FromDate"].Value = FromDate.ToString("MM/dd/yyyy");

            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar);
            cmd.Parameters["@ToDate"].Value = ToDate.ToString("MM/dd/yyyy");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcJournalMasterVM> objList = new List<AcJournalMasterVM>();
            AcJournalMasterVM obj;
            
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcJournalMasterVM();
                    obj.AcJournalID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcJournalID"].ToString());
                    obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.VoucherNo = ds.Tables[0].Rows[i]["VoucherNo"].ToString();
                    obj.TransDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"].ToString());
                    obj.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    obj.TransactionNo = ds.Tables[0].Rows[i]["TransactionNo"].ToString();
                    obj.Reference= ds.Tables[0].Rows[i]["Reference"].ToString();
                    obj.VoucherType= ds.Tables[0].Rows[i]["VoucherType"].ToString();
                    obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["JournalAmount"].ToString());
                    objList.Add(obj);
                }
            }
            return objList;
        }

        
        //Indexacbook page
        public static List<AcJournalMasterVM> AcJournalMasterSelectAll(int FYearId, int BranchID, DateTime FromDate, DateTime ToDate,string VoucherType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "AcJournalMasterAll";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FYearID", SqlDbType.Int);
            cmd.Parameters["@FYearID"].Value = FYearId;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = BranchID;

            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar);
            cmd.Parameters["@FromDate"].Value = FromDate.ToString("MM/dd/yyyy");

            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar);
            cmd.Parameters["@ToDate"].Value = ToDate.ToString("MM/dd/yyyy");

            cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar);
            cmd.Parameters["@VoucherType"].Value = VoucherType;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcJournalMasterVM> objList = new List<AcJournalMasterVM>();
            AcJournalMasterVM obj;

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcJournalMasterVM();
                    obj.AcJournalID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcJournalID"].ToString());
                    //obj.ID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.VoucherNo = ds.Tables[0].Rows[i]["VoucherNo"].ToString();
                    obj.TransDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"].ToString());
                    obj.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    //obj.TransactionNo = ds.Tables[0].Rows[i]["TransactionNo"].ToString();
                    obj.Reference = ds.Tables[0].Rows[i]["Reference"].ToString();
                    obj.Amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                    obj.VoucherType = ds.Tables[0].Rows[i]["VoucherType"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static List<AcHeadSelectAllVM> GetAcHeadSelectAllByCategory(int BranchID,string Category)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "AcHeadSelectAllByCategory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BranchID", SqlDbType.Int);
            cmd.Parameters["@BranchID"].Value = BranchID;

            cmd.Parameters.Add("@Category", SqlDbType.VarChar);
            cmd.Parameters["@Category"].Value = Category;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcHeadSelectAllVM> objList = new List<AcHeadSelectAllVM>();
            AcHeadSelectAllVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcHeadSelectAllVM();
                    obj.AcHeadID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcHeadID"].ToString());
                    obj.AcHeadKey = ds.Tables[0].Rows[i]["AcHeadKey"].ToString();
                    obj.AcHead = ds.Tables[0].Rows[i]["AcHead"].ToString();
                    obj.AcGroupID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcGroupID"].ToString());
                    obj.ParentID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["ParentID"].ToString());
                    obj.HeadOrder = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["HeadOrder"].ToString());
                    if (ds.Tables[0].Rows[i]["StatusHide"] == DBNull.Value)
                    {
                        obj.StatusHide = false;
                    }
                    else
                    {
                        obj.StatusHide = Convert.ToBoolean(ds.Tables[0].Rows[i]["StatusHide"].ToString());
                    }
                    obj.UserID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["UserID"].ToString());
                    obj.Prefix = ds.Tables[0].Rows[i]["Prefix"].ToString();
                    obj.AcGroup = ds.Tables[0].Rows[i]["AcGroup"].ToString();
                    obj.AccountType = ds.Tables[0].Rows[i]["AccountType"].ToString();
                    if (ds.Tables[0].Rows[i]["TaxApplicable"] == DBNull.Value)
                    {
                        obj.TaxApplicable = false;

                    }
                    else
                    {
                        obj.TaxApplicable = Convert.ToBoolean(ds.Tables[0].Rows[i]["TaxApplicable"].ToString());
                        if (obj.TaxApplicable == true)
                        {
                            obj.TaxPercent = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["TaxPercent"].ToString());
                        }
                        else
                        {
                            obj.TaxPercent = 0;
                        }

                    }


                    objList.Add(obj);
                }
            }
            return objList;
        }

        //Account Master Opening Posting
        public  static string AccountOpeningPosting(int fyearid,int branchid)
        {
            try
            {
                //string json = "";
                string strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strConnString))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SP_AcOpeningMasterPosting " + fyearid.ToString() + "," + branchid;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";

        }

        //InvoiceOpeningPosting
        public static string InvoiceOpeningPosting(int MasterId, int fyearid, int branchid)
        {
            try
            {
                //string json = "";
                string strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strConnString))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SP_AcInvoiceOpeningPosting " + MasterId + "," +  fyearid.ToString() + "," + branchid;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";

        }

        public static DateTime CheckParamDate(DateTime EntryDate, int yearid)
        {
            DateTime pFromDate = Convert.ToDateTime(EntryDate);
            StatusModel obj = new StatusModel();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "SP_CheckDateValiate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntryDate", pFromDate.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@FYearId", yearid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    obj.Status = ds.Tables[0].Rows[0][0].ToString();
                    obj.Message = ds.Tables[0].Rows[0][1].ToString();
                    obj.ValidDate = Convert.ToDateTime(ds.Tables[0].Rows[0][2].ToString()).ToString("dd-MM-yyyy");
                }
            }
            catch (Exception ex)
            {
                obj.Status = "Failed";
                obj.Message = ex.Message;

            }

            if (obj.Status != "OK")
            {
                return Convert.ToDateTime(obj.ValidDate);
            }
            else
            {
                return EntryDate;
            }
        }


        public static List<AcInvoiceOpeningVM> GetInvoiceOpening(int FYearId,string Type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetInvoiceOpening";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FYearId", FYearId);
            cmd.Parameters.AddWithValue("@Type", Type);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<AcInvoiceOpeningVM> objList = new List<AcInvoiceOpeningVM>();
            AcInvoiceOpeningVM obj;
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AcInvoiceOpeningVM();
                    obj.AcOPInvoiceMasterID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["AcOPInvoiceMasterID"].ToString());
                    obj.PartyID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["PartyId"].ToString());
                    obj.PartyName = ds.Tables[0].Rows[i]["PartName"].ToString();
                    obj.PartyType = ds.Tables[0].Rows[i]["PartType"].ToString();
                    obj.Debit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Debit"].ToString());
                    obj.Credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());
                    obj.StatusSDSC = ds.Tables[0].Rows[i]["StatusSDSC"].ToString();
                    
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static StatusModel CheckDateValidate(string EntryDate, int FyearId)
        {
            DateTime pFromDate = Convert.ToDateTime(EntryDate);
            StatusModel obj = new StatusModel();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "SP_CheckDateValiate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntryDate", pFromDate.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@FYearId", FyearId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    obj.Status = ds.Tables[0].Rows[0][0].ToString();
                    obj.Message = ds.Tables[0].Rows[0][1].ToString();
                    obj.ValidDate = Convert.ToDateTime(ds.Tables[0].Rows[0][2].ToString()).ToString("dd-MM-yyyy");
                }
            }
            catch (Exception ex)
            {
                obj.Status = "Failed";
                obj.Message = ex.Message;

            }
            return obj;
        }

        //ac journal master
        public static string GetMaxVoucherNo(string VoucherType, int yearid)
        {
            string voucherno="";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "GetMaxVoucherNo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherType", VoucherType);
                cmd.Parameters.AddWithValue("@FYearId", yearid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    voucherno= ds.Tables[0].Rows[0][0].ToString();                    
                }
            }
            catch (Exception ex)
            {
                return voucherno;
            }

            return voucherno;
        }
        public static string GetMaxCreditNoteNo(int yearid)
        {
            string voucherno = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "GetMaxCreditNoteNo";
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@FYearId", yearid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    voucherno = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return voucherno;
            }

            return voucherno;
        }

        public static string GetMaxDebiteNoteNo(int yearid)
        {
            string voucherno = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "GetMaxDebitNoteNo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FYearId", yearid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    voucherno = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return voucherno;
            }

            return voucherno;
        }

        public static string DeleteDebiteNote(int id)
        {
            string voucherno = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "DeleteDebitNoteNo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    voucherno = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return voucherno;
            }

            return voucherno;
        }

        public static string DeleteCreditNote(int id)
        {
            string voucherno = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
                cmd.CommandText = "DeleteCreditNote";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    voucherno = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return voucherno;
            }

            return voucherno;
        }

        public static string GetMaxVoucherNo(int FYearId, int BranchId)
        {

            SqlCommand cmd = new SqlCommand();
            string strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            cmd.Connection = new SqlConnection(strConnString);
            cmd.CommandText = "SP_GetMaxVoucherNo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FyearId", SqlDbType.Int);
            cmd.Parameters["@FyearId"].Value = FYearId;

            cmd.Parameters.AddWithValue("@VoucherType", "");

            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            string voucherno = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        voucherno = ds.Tables[0].Rows[0][0].ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                return "";
            }
            return voucherno;
        }

        #region YearEndPRocess
        public static YearEndProcessSearch GetYearEndProcess(int YearId, int BranchId, int ProcessStatus)
        {
            YearEndProcessSearch vm = new YearEndProcessSearch();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_AccYearEndProcess";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@YearID", SqlDbType.Int);
            cmd.Parameters["@YearID"].Value = YearId;
            cmd.Parameters.Add("@BranchId", SqlDbType.Int);
            cmd.Parameters["@BranchId"].Value = BranchId;
            cmd.Parameters.AddWithValue("@Process", ProcessStatus);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<YearEndProcessIncomeExpense> objList = new List<YearEndProcessIncomeExpense>();
            List<YearEndProcessPL> objList1 = new List<YearEndProcessPL>();
            if (ProcessStatus == 0)
            {

                YearEndProcessIncomeExpense obj;
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new YearEndProcessIncomeExpense();
                        obj.AcHeadId = Convert.ToInt32(ds.Tables[0].Rows[i]["AcHeadId"].ToString());
                        obj.AcHeadName = ds.Tables[0].Rows[i]["AcHeadName"].ToString();
                        obj.ClosingBalance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ClosingBalance"].ToString());
                        objList.Add(obj);
                    }
                }
                YearEndProcessPL obj1;
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        obj1 = new YearEndProcessPL();
                        obj1.AcHeadId = Convert.ToInt32(ds.Tables[1].Rows[i]["AcHeadId"].ToString());
                        obj1.VoucherNo = ds.Tables[1].Rows[i]["VoucherNo"].ToString();
                        obj1.AcHeadName = ds.Tables[1].Rows[i]["AcHeadName"].ToString();
                        obj1.Amount = CommanFunctions.ParseDecimal(ds.Tables[1].Rows[i]["Amount"].ToString());
                        obj1.updatestatus = Convert.ToBoolean(ds.Tables[1].Rows[i]["updatedstatus"].ToString());
                        objList1.Add(obj1);
                    }
                }
                vm.PLDetails = objList1;
                vm.IncomeExpDetails = objList;
            }
            else
            {
                vm.PLDetails = objList1;
                vm.IncomeExpDetails = objList;

            }
            return vm;
        }


        public static List<YearEndProcessAccounts> GetYearEndAccountOpening(int Userid, int YearId, int BranchId, int process)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_AcYearEndAccountOpeningProcess";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@YearID", SqlDbType.Int);
            cmd.Parameters["@YearID"].Value = YearId;
            cmd.Parameters.Add("@BranchId", SqlDbType.Int);
            cmd.Parameters["@BranchId"].Value = BranchId;
            cmd.Parameters.AddWithValue("@UserId", Userid);
            cmd.Parameters.AddWithValue("@Process", process);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<YearEndProcessAccounts> objList = new List<YearEndProcessAccounts>();
            YearEndProcessAccounts obj;
            if (process == 0)
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new YearEndProcessAccounts();
                        obj.Particulars = ds.Tables[0].Rows[i]["AcHeadName"].ToString();
                        obj.OpeningBalance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());
                        obj.Transactions = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Transactions"].ToString());
                        obj.ClosingBalance = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["ClosingBalance"].ToString());
                        obj.NextYearOpening = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["NextYearOpening"].ToString());

                        objList.Add(obj);
                    }
                }
            }
            return objList;
        }


        public static YearEndProcessSearch GetYearEndProcessCustomerInv(string CustomerType, int YearId, int BranchId, int ProcessStatus)
        {
            YearEndProcessSearch vm = new YearEndProcessSearch();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetYearEndCustomerStatement";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("CustomerType", CustomerType);
            cmd.Parameters.Add("@FYearId", SqlDbType.Int);
            cmd.Parameters["@FYearID"].Value = YearId;
            cmd.Parameters.Add("@BranchId", SqlDbType.Int);
            cmd.Parameters["@BranchId"].Value = BranchId;
            cmd.Parameters.AddWithValue("@Process", ProcessStatus);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<YearEndProcessCustomer> objList = new List<YearEndProcessCustomer>();
            if (ProcessStatus == 0)
            {
                YearEndProcessCustomer obj;
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new YearEndProcessCustomer();
                        obj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"].ToString());
                        obj.CustomerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
                        obj.InvoiceNo = ds.Tables[0].Rows[i]["CustomerInvoiceNo"].ToString();
                        obj.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                        objList.Add(obj);
                    }
                }

                vm.CustomerInvDetails = objList;
            }
            return vm;
        }

        public static YearEndProcessSearch GetYearEndProcessSupplierInv(int SupplierTypeId, int YearId, int BranchId, int ProcessStatus)
        {
            YearEndProcessSearch vm = new YearEndProcessSearch();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_GetYearEndSupplierStatement";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@SupplierTypeId", 3); 
            cmd.Parameters.Add("@FYearId", SqlDbType.Int);
            cmd.Parameters["@FYearID"].Value = YearId;
            cmd.Parameters.Add("@BranchId", SqlDbType.Int);
            cmd.Parameters["@BranchId"].Value = BranchId;
            cmd.Parameters.AddWithValue("@Process", ProcessStatus);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<YearEndProcessSupplier> objList = new List<YearEndProcessSupplier>();
            if (ProcessStatus == 0)
            {
                YearEndProcessSupplier obj;
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new YearEndProcessSupplier();
                        obj.SupplierId = Convert.ToInt32(ds.Tables[0].Rows[i]["SupplierId"].ToString());
                        obj.SupplierName = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                        obj.InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"].ToString();
                        obj.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"].ToString()).ToString("dd-MM-yyyy");
                        obj.Amount = CommanFunctions.ParseDecimal(ds.Tables[0].Rows[i]["Amount"].ToString());
                        objList.Add(obj);
                    }
                }

                vm.SupplierInvDetails = objList;
            }
            return vm;
        }
        #endregion
    }
}
    



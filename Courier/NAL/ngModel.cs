using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTMSV2.NAL
{
    public class ngModel
    {

        [System.Web.Script.Services.ScriptMethod()]
        public static List<CountryList> GetDDLCountry()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CountryList> list = new List<CountryList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.CountryMaster order by CountryName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CountryList
                        {
                            Value = sdr["CountryID"].ToString(),
                            Text = sdr["CountryName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new CountryList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }




        [System.Web.Script.Services.ScriptMethod()]
        public static List<CityList> GetDDLCity(int CID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CityList> list = new List<CityList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from [dbo].[CityMaster] where CountryId=@CID order by City", con);
                cmd.Parameters.AddWithValue("CID", CID);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CityList
                        {
                            Value = sdr["CityID"].ToString(),
                            Text = sdr["City"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }
            catch (Exception p)
            {
                list.Add(new CityList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }

        }



        [System.Web.Script.Services.ScriptMethod()]
        public static List<LocationList> GetDDLLocation(int LID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<LocationList> list = new List<LocationList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from [dbo].[LocationMaster] where CityID=@LID order by Location", con);
                cmd.Parameters.AddWithValue("LID", LID);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new LocationList
                        {
                            Value = sdr["LocationID"].ToString(),
                            Text = sdr["Location"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }
            catch (Exception p)
            {
                list.Add(new LocationList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }

        }

        [System.Web.Script.Services.ScriptMethod()]
        public static List<DesignationList> GetDDLDesignation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<DesignationList> list = new List<DesignationList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Designation order by Designation", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new DesignationList
                        {
                            Value = sdr["DesignationID"].ToString(),
                            Text = sdr["Designation"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new DesignationList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        public static List<CurrencyList> GetDDLCurrency()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CurrencyList> list = new List<CurrencyList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.CurrencyMaster order by CurrencyName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CurrencyList
                        {
                            Value = sdr["CurrencyID"].ToString(),
                            Text = sdr["CurrencyName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new CurrencyList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }



        [System.Web.Script.Services.ScriptMethod()]
        public static List<CustomerMasterList> GetDDLCustomerMaster()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CustomerMasterList> list = new List<CustomerMasterList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.CustomerMaster order by CustomerName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CustomerMasterList
                        {
                            Value = sdr["CustomerID"].ToString(),
                            Text = sdr["CustomerName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new CustomerMasterList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        public static List<CustomerMasterList> GetDDLCustomerMasterBYTrade()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CustomerMasterList> list = new List<CustomerMasterList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct DocumentName,DocumentID from ImpExpDocumentMaster where LEN(DocumentName)>0 order by DocumentName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CustomerMasterList
                        {
                            Value = sdr["DocumentID"].ToString(),
                            Text = sdr["DocumentName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new CustomerMasterList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        public static List<BranchMasterList> GetDDLBranchMaster()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<BranchMasterList> list = new List<BranchMasterList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.BranchMaster order by BranchName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new BranchMasterList
                        {
                            Value = sdr["BranchID"].ToString(),
                            Text = sdr["BranchName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new BranchMasterList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }



        [System.Web.Script.Services.ScriptMethod()]
        public static List<CityList> CityMasterSelectDepot()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<CityList> list = new List<CityList>();
            try
            {
                //SqlCommand cmd = new SqlCommand("select CityID, City from CityMaster where DepotID IS NULL order by City", con);
                SqlCommand cmd = new SqlCommand("Select ID, Depot From tbldepot", con);
                
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new CityList
                        {
                            Value = sdr["ID"].ToString(),
                            Text = sdr["Depot"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new CityList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        public static List<PortList> GetDDLPort()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<PortList> list = new List<PortList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select PortID,PortName from PortMaster order by PortName", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new PortList
                        {
                            Value = sdr["PortID"].ToString(),
                            Text = sdr["PortName"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new PortList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        public static List<ZoneCategoryList> GetDDLZoneCategory()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            List<ZoneCategoryList> list = new List<ZoneCategoryList>();
            try
            {
                SqlCommand cmd = new SqlCommand("select cm.*,case when cm.StatusBaseCategory = 'True' Then 'Yes' Else 'No' End as StatusBaseCategoryDisp from ZoneCategory cm order by ZoneCategory", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new ZoneCategoryList
                        {
                            Value = sdr["ZoneCategoryID"].ToString(),
                            Text = sdr["ZoneCategory"].ToString()
                        });
                    }
                }
                sdr.Close();
                con.Close();
                con.Dispose();
                return list;
            }

            catch (Exception p)
            {
                list.Add(new ZoneCategoryList { Value = "0", Text = "Not Loaded Please Reload page" });
                return list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

    }

    public class ZoneCategoryList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class PortList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class CurrencyList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }



    public class DesignationList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }


    public class LocationList
    {
        public string ID { get; set; }
        public string LID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }

    }


    public class CityList
    {
        public string ID { get; set; }
        public string CID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }

    }

    public class CountryList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class CustomerMasterList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }


    public class BranchMasterList
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }



    public class tblCountry
    {
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string StatusCountry { get; set; }
        public string StatusBaseCountry { get; set; }
        public string CountryCode { get; set; }
        public string TelephoneCode { get; set; }

        public string ManageInsertCountry(tblCountry o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CountryID", o.CountryID);
                input.Add("CountryName", o.CountryName ?? "");
                input.Add("CountryCode", o.CountryCode ?? "");
                input.Add("TelephoneCode", o.TelephoneCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CountryMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateCountry(tblCountry o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CountryID", o.CountryID);
                input.Add("CountryName", o.CountryName ?? "");
                input.Add("CountryCode", o.CountryCode ?? "");
                input.Add("TelephoneCode", o.TelephoneCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CountryMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteCountry(tblCountry o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CountryID", o.CountryID);
                input.Add("CountryName", o.CountryName ?? "");
                input.Add("CountryCode", o.CountryCode ?? "");
                input.Add("TelephoneCode", o.TelephoneCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CountryMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }




        public List<tblCountry> GetCountry(tblCountry o)
        {
            List<tblCountry> Result = new List<tblCountry>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CountryMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCountry j = new tblCountry();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();
                        j.StatusCountry = sdr["StatusCountry"].ToString();
                        j.StatusBaseCountry = sdr["StatusBaseCountry"].ToString();
                        j.CountryCode = sdr["CountryCode"].ToString();
                        j.TelephoneCode = sdr["TelephoneCode"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }


    public class tblAcCategory
    {
        public string AcCategoryID { get; set; }
        public string AcCategory { get; set; }

        public string ManageInsertAcCategory(tblAcCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AcCategoryID", o.AcCategoryID);
                input.Add("AcCategory", o.AcCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AcCategoryInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateAcCategory(tblAcCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AcCategoryID", o.AcCategoryID);
                input.Add("AcCategory", o.AcCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AcCategoryUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteAcCategory(tblAcCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AcCategoryID", o.AcCategoryID);
                input.Add("AcCategory", o.AcCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AcCategoryDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblAcCategory> GetAcCategory(tblAcCategory o)
        {
            List<tblAcCategory> Result = new List<tblAcCategory>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[AcCategorySelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblAcCategory j = new tblAcCategory();
                        j.AcCategoryID = sdr["AcCategoryID"].ToString();
                        j.AcCategory = sdr["AcCategory"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }

    }


    public class tblDesignation
    {
        public string DesignationID { get; set; }
        public string Designation { get; set; }
        public string StatusDesignation { get; set; }

        public string ManageInsertDesignation(tblDesignation o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DesignationID", o.DesignationID);
                input.Add("Designation", o.Designation ?? "");
                input.Add("StatusDesignation", o.StatusDesignation ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DesignationInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateDesignation(tblDesignation o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DesignationID", o.DesignationID);
                input.Add("Designation", o.Designation ?? "");
                input.Add("StatusDesignation", o.StatusDesignation ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DesignationUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteDesignation(tblDesignation o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DesignationID", o.DesignationID);
                input.Add("Designation", o.Designation ?? "");
                input.Add("StatusDesignation", o.StatusDesignation ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DesignationDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblDesignation> GetDesignation(tblDesignation o)
        {
            List<tblDesignation> Result = new List<tblDesignation>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[DesignationSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblDesignation j = new tblDesignation();
                        j.DesignationID = sdr["DesignationID"].ToString();
                        j.Designation = sdr["Designation"].ToString();
                        j.StatusDesignation = sdr["StatusDesignation"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblItemMaster
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string HsCode { get; set; }


        public string ManageInsertItemMaster(tblItemMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();


                input.Add("ItemName", o.ItemName ?? "");
                input.Add("Description", o.Description ?? "");
                input.Add("HsCode", o.HsCode ?? "");
                output.Add("ItemID", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ItemMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["ItemID"], out LastID);
                if (isdone)
                {
                    Res = res["ItemID"];
                }
                else
                {
                    Res = res["ItemID"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateItemMaster(tblItemMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("ItemID", o.ItemID);
                input.Add("ItemName", o.ItemName ?? "");
                input.Add("Description", o.Description ?? "");
                input.Add("HsCode", o.HsCode ?? "");
                //output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ItemMasterUpdate]", input, output, true);
                int LastID = 0;
                //bool isdone = int.TryParse(res["Result"], out LastID);
                //if (isdone)
                //{
                //    Res = res["Result"];
                //}
                //else
                //{
                //    Res = res["Result"];
                //}
                return o.ItemID;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteItemMaster(tblItemMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("@ItemIDs", o.ItemID);
                //input.Add("ItemName", o.ItemName ?? "");
                //input.Add("Description", o.Description ?? "");
                //input.Add("HsCode", o.HsCode ?? "");
                //output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ItemMasterDelete]", input, output, true);
                int LastID = 0;
                //bool isdone = int.TryParse(res["Result"], out LastID);
                //if (isdone)
                //{
                //    Res = res["Result"];
                //}
                //else
                //{
                //    Res = res["Result"];
                //}
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblItemMaster> GetItemMaster(tblItemMaster o)
        {
            
            List<tblItemMaster> Result = new List<tblItemMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblItemMaster j = new tblItemMaster();
                        j.ItemID = sdr["ItemID"].ToString();
                        j.ItemName = sdr["ItemName"].ToString();
                        j.Description = sdr["Description"].ToString();
                        j.HsCode = sdr["HsCode"].ToString();

                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblPackages
    {
        public string PackageID { get; set; }
        public string PackageType { get; set; }
        public string PackageDescription { get; set; }


        public string ManageInsertPackages(tblPackages o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                
                input.Add("PackageType", o.PackageType ?? "");
                input.Add("PackageDescription", o.PackageDescription ?? "");
                output.Add("PackageID", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[InsertPackages]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["PackageID"], out LastID);
                if (isdone)
                {
                    Res = res["PackageID"];
                }
                else
                {
                    Res = res["PackageID"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdatePackages(tblPackages o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("PackageID", o.PackageID);
                input.Add("PackageType", o.PackageType ?? "");
                input.Add("PackageDescription", o.PackageDescription ?? "");
               /// output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[UpdatePackages]", input, output, true);
                int LastID = 0;
                //bool isdone = int.TryParse(res["Result"], out LastID);
                //if (isdone)
                //{
                //    Res = res["Result"];
                //}
                //else
                //{
                //    Res = res["Result"];
                //}
                return o.PackageID;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeletePackages(tblPackages o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("@PackageIDs", o.PackageID);
                //input.Add("PackageType", o.PackageType ?? "");
                //input.Add("PackageDescription", o.PackageDescription ?? "");
                //output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DeletePackages]", input, output, true);
                //int LastID = 0;
                //bool isdone = int.TryParse(res["Result"], out LastID);
                //if (isdone)
                //{
                //    Res = res["Result"];
                //}
                //else
                //{
                //    Res = res["Result"];
                //}
                return o.PackageID;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblPackages> GetPackages(tblPackages o)
        {
            List<tblPackages> Result = new List<tblPackages>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetPackages]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblPackages j = new tblPackages();
                        j.PackageID = sdr["PackageID"].ToString();
                        j.PackageType = sdr["PackageType"].ToString();
                        j.PackageDescription = sdr["PackageName"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }


    public class tblPortMaster
    {
        public string PortID { get; set; }
        public string PortName { get; set; }
        public string PortCode { get; set; }


        public string ManageInsertPortMaster(tblPortMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("PortID", o.PortID);
                input.Add("PortName", o.PortName ?? "");
                input.Add("PortCode", o.PortCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[PortMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdatePortMaster(tblPortMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("PortID", o.PortID);
                input.Add("PortName", o.PortName ?? "");
                input.Add("PortCode", o.PortCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[PortMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeletePortMaster(tblPortMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("PortID", o.PortID);
                input.Add("PortName", o.PortName ?? "");
                input.Add("PortCode", o.PortCode ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[PortMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblPortMaster> GetPortMaster(tblPortMaster o)
        {
            List<tblPortMaster> Result = new List<tblPortMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[PortMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblPortMaster j = new tblPortMaster();
                        j.PortID = sdr["PortID"].ToString();
                        j.PortName = sdr["PortName"].ToString();
                        j.PortCode = sdr["PortCode"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblCourierStatus
    {
        public string CourierStatusID { get; set; }
        public string CourierStatus { get; set; }
        public string StatusCourier { get; set; }


        public string ManageInsertCourierStatus(tblCourierStatus o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CourierStatusID", o.CourierStatusID);
                input.Add("CourierStatus", o.CourierStatus ?? "");
                input.Add("StatusCourier", o.StatusCourier ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CourierStatusInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateCourierStatus(tblCourierStatus o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CourierStatusID", o.CourierStatusID);
                input.Add("CourierStatus", o.CourierStatus ?? "");
                input.Add("StatusCourier", o.StatusCourier ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CourierStatusUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteCourierStatus(tblCourierStatus o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CourierStatusID", o.CourierStatusID);
                input.Add("CourierStatus", o.CourierStatus ?? "");
                input.Add("StatusCourier", o.StatusCourier ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CourierStatusDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblCourierStatus> GetCourierStatus(tblCourierStatus o)
        {
            List<tblCourierStatus> Result = new List<tblCourierStatus>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CourierStatusSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCourierStatus j = new tblCourierStatus();
                        j.CourierStatusID = sdr["CourierStatusID"].ToString();
                        j.CourierStatus = sdr["CourierStatus"].ToString();
                        j.StatusCourier = sdr["StatusCourier"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
        public List<tblCourierStatus> GetCourierStatusHold(tblCourierStatus o)
        {
            List<tblCourierStatus> Result = new List<tblCourierStatus>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CourierStatusSelectHold]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCourierStatus j = new tblCourierStatus();
                        j.CourierStatusID = sdr["CourierStatusID"].ToString();
                        j.CourierStatus = sdr["CourierStatus"].ToString();
                        j.StatusCourier = sdr["StatusCourier"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblDocumentSetup
    {
        public string DocumentSetupID { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }

        public string CustomerName { get; set; }
        public string BranchName { get; set; }

        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNo { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssuePlace { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }


        public string ManageInsertDocumentSetup(tblDocumentSetup o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentSetupID", o.DocumentSetupID);
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                input.Add("DocumentType", o.DocumentType ?? "");
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("DocumentNo", o.DocumentNo ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");
                input.Add("IssuePlace", o.IssuePlace ?? "");
                input.Add("Value", o.Value ?? "");
                input.Add("Remarks", o.Remarks ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DocumentSetupInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateDocumentSetup(tblDocumentSetup o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentSetupID", o.DocumentSetupID);
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                input.Add("DocumentType", o.DocumentType ?? "");
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("DocumentNo", o.DocumentNo ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");
                input.Add("IssuePlace", o.IssuePlace ?? "");
                input.Add("Value", o.Value ?? "");
                input.Add("Remarks", o.Remarks ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DocumentSetupUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteDocumentSetup(tblDocumentSetup o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentSetupID", o.DocumentSetupID);
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                input.Add("DocumentType", o.DocumentType ?? "");
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("DocumentNo", o.DocumentNo ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");
                input.Add("IssuePlace", o.IssuePlace ?? "");
                input.Add("Value", o.Value ?? "");
                input.Add("Remarks", o.Remarks ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[DocumentSetupDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblDocumentSetup> GetDocumentSetup(tblDocumentSetup o)
        {
            List<tblDocumentSetup> Result = new List<tblDocumentSetup>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[DocumentSetupSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblDocumentSetup j = new tblDocumentSetup();
                        j.DocumentSetupID = sdr["DocumentSetupID"].ToString();
                        j.CustomerID = sdr["CustomerID"].ToString();
                        j.BranchID = sdr["BranchID"].ToString();

                        j.CustomerName = sdr["CustomerName"].ToString();
                        j.BranchName = sdr["BranchName"].ToString();

                        j.DocumentType = sdr["DocumentType"].ToString();
                        j.DocumentName = sdr["DocumentName"].ToString();
                        j.DocumentNo = sdr["DocumentNo"].ToString();
                        j.IssueDate = Convert.ToDateTime(sdr["IssueDate"]).ToString("yyyy-MM-dd");
                        j.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"]).ToString("yyyy-MM-dd");
                        j.IssuePlace = sdr["IssuePlace"].ToString();
                        j.Value = sdr["Value"].ToString();
                        j.Remarks = sdr["Remarks"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblVehicleType
    {
        public string VehicleTypeID { get; set; }
        public string VehicleType { get; set; }

        public string ManageInsertVehicleType(tblVehicleType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                //input.Add("VehicleTypeID", o.VehicleTypeID);
                input.Add("VehicleType", o.VehicleType ?? "");
                output.Add("VehicleTypeID", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[VehicleTypeInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["VehicleTypeID"], out LastID);
                if (isdone)
                {
                    Res = res["VehicleTypeID"];
                }
                else
                {
                    Res = res["VehicleTypeID"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageUpdateVehicleType(tblVehicleType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("VehicleTypeID", o.VehicleTypeID);
                input.Add("VehicleType", o.VehicleType ?? "");
                //output.Add("VehicleTypeID", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[VehicleTypeUpdate]", input, output, true);
                //int LastID = 0;
                //bool isdone = int.TryParse(res["VehicleTypeID"], out LastID);
                //if (isdone)
                //{
                //    Res = res["VehicleTypeID"];
                //}
                //else
                //{
                //    Res = res["VehicleTypeID"];
                //}
                return o.VehicleTypeID;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public string ManageDeleteVehicleType(tblVehicleType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("VehicleTypeID", o.VehicleTypeID);
                input.Add("VehicleType", o.VehicleType ?? "");
                //output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[VehicleTypeDelete]", input, output, true);
                //int LastID = 0;
                //bool isdone = int.TryParse(res["Result"], out LastID);
                //if (isdone)
                //{
                //    Res = res["Result"];
                //}
                //else
                //{
                //    Res = res["Result"];
                //}
                return o.VehicleTypeID;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }

        public List<tblVehicleType> GetVehicleType(tblVehicleType o)
        {
            List<tblVehicleType> Result = new List<tblVehicleType>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[VehicleTypeSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblVehicleType j = new tblVehicleType();
                        j.VehicleTypeID = sdr["VehicleTypeID"].ToString();
                        j.VehicleType = sdr["VehicleType"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }


    public class tblZoneCategory
    {
        public string ZoneCategoryID { get; set; }
        public string ZoneCategory { get; set; }
        public string StatusBaseCategory { get; set; }


        public string ManageInsertZoneCategory(tblZoneCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("ZoneCategoryID", o.ZoneCategoryID);
                input.Add("ZoneCategory", o.ZoneCategory ?? "");
                input.Add("StatusBaseCategory", o.StatusBaseCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ZoneCategoryInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateZoneCategory(tblZoneCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("ZoneCategoryID", o.ZoneCategoryID);
                input.Add("ZoneCategory", o.ZoneCategory ?? "");
                input.Add("StatusBaseCategory", o.StatusBaseCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ZoneCategoryUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteZoneCategory(tblZoneCategory o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("ZoneCategoryID", o.ZoneCategoryID);
                input.Add("ZoneCategory", o.ZoneCategory ?? "");
                input.Add("StatusBaseCategory", o.StatusBaseCategory ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ZoneCategoryDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblZoneCategory> GetZoneCategory(tblZoneCategory o)
        {
            List<tblZoneCategory> Result = new List<tblZoneCategory>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ZoneCategorySelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblZoneCategory j = new tblZoneCategory();
                        j.ZoneCategoryID = sdr["ZoneCategoryID"].ToString();
                        j.ZoneCategory = sdr["ZoneCategory"].ToString();
                        j.StatusBaseCategory = sdr["StatusBaseCategory"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblRevenueCostMaster
    {
        public string RCID { get; set; }
        public string RCCode { get; set; }
        public string RevenueComponent { get; set; }
        public string RevenueRate { get; set; }
        public string RevenueMandatory { get; set; }
        public string RevenueAcHeadID { get; set; }
        public string CostComponent { get; set; }
        public string CostRate { get; set; }
        public string CostMandatory { get; set; }
        public string CostAcHeadID { get; set; }
        public string AcCompanyId { get; set; }
        public string BranchID { get; set; }


        public string ManageInsertRevenueCostMaster(tblRevenueCostMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("RCID", o.RCID);
                input.Add("RCCode", o.RCCode ?? "");
                input.Add("RevenueComponent", o.RevenueComponent ?? "");
                input.Add("RevenueRate", o.RevenueRate ?? "");
                input.Add("RevenueMandatory", o.RevenueMandatory ?? "");
                input.Add("RevenueAcHeadID", o.RevenueAcHeadID ?? "");
                input.Add("CostComponent", o.CostComponent ?? "");
                input.Add("CostRate", o.CostRate ?? "");
                input.Add("CostMandatory", o.CostMandatory ?? "");
                input.Add("CostAcHeadID", o.CostAcHeadID ?? "");
                input.Add("AcCompanyId", o.AcCompanyId ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[RevenueCostMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateRevenueCostMaster(tblRevenueCostMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("RCID", o.RCID);
                input.Add("RCCode", o.RCCode ?? "");
                input.Add("RevenueComponent", o.RevenueComponent ?? "");
                input.Add("RevenueRate", o.RevenueRate ?? "");
                input.Add("RevenueMandatory", o.RevenueMandatory ?? "");
                input.Add("RevenueAcHeadID", o.RevenueAcHeadID ?? "");
                input.Add("CostComponent", o.CostComponent ?? "");
                input.Add("CostRate", o.CostRate ?? "");
                input.Add("CostMandatory", o.CostMandatory ?? "");
                input.Add("CostAcHeadID", o.CostAcHeadID ?? "");
                input.Add("AcCompanyId", o.AcCompanyId ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[RevenueCostMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteRevenueCostMaster(tblRevenueCostMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("RCID", o.RCID);
                input.Add("RCCode", o.RCCode ?? "");
                input.Add("RevenueComponent", o.RevenueComponent ?? "");
                input.Add("RevenueRate", o.RevenueRate ?? "");
                input.Add("RevenueMandatory", o.RevenueMandatory ?? "");
                input.Add("RevenueAcHeadID", o.RevenueAcHeadID ?? "");
                input.Add("CostComponent", o.CostComponent ?? "");
                input.Add("CostRate", o.CostRate ?? "");
                input.Add("CostMandatory", o.CostMandatory ?? "");
                input.Add("CostAcHeadID", o.CostAcHeadID ?? "");
                input.Add("AcCompanyId", o.AcCompanyId ?? "");
                input.Add("BranchID", o.BranchID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[RevenueCostMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblRevenueCostMaster> GetRevenueCostMaster(tblRevenueCostMaster o)
        {
            List<tblRevenueCostMaster> Result = new List<tblRevenueCostMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[RevenueCostMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblRevenueCostMaster j = new tblRevenueCostMaster();
                        j.RCID = sdr["RCID"].ToString();
                        j.RCCode = sdr["RCCode"].ToString();
                        j.RevenueComponent = sdr["RevenueComponent"].ToString();
                        j.RevenueRate = sdr["RevenueRate"].ToString();
                        j.RevenueMandatory = sdr["RevenueMandatory"].ToString();
                        j.RevenueAcHeadID = sdr["RevenueAcHeadID"].ToString();
                        j.CostComponent = sdr["CostComponent"].ToString();
                        j.CostRate = sdr["CostRate"].ToString();
                        j.CostMandatory = sdr["CostMandatory"].ToString();
                        j.CostAcHeadID = sdr["CostAcHeadID"].ToString();
                        j.AcCompanyId = sdr["AcCompanyId"].ToString();
                        j.BranchID = sdr["BranchID"].ToString();

                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblLocationMaster
    {

        public string LocationID { get; set; }
        public string Location { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }



        public string ManageInsertLocationMaster(tblLocationMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("LocationID", o.LocationID);
                input.Add("Location", o.Location ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("Area", o.Area ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[LocationMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateLocationMaster(tblLocationMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("LocationID", o.LocationID);
                input.Add("Location", o.Location ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("Area", o.Area ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[LocationMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteLocationMaster(tblLocationMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();
                input.Add("LocationID", o.LocationID);
                input.Add("Location", o.Location ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("Area", o.Area ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[LocationMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblLocationMaster> GetLocationMaster(tblLocationMaster o)
        {
            List<tblLocationMaster> Result = new List<tblLocationMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LocationMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        tblLocationMaster j = new tblLocationMaster();
                        j.LocationID = sdr["LocationID"].ToString();
                        j.Location = sdr["Location"].ToString();
                        j.CityID = sdr["CityID"].ToString();
                        j.City = sdr["City"].ToString();
                        j.Area = sdr["Area"].ToString();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();
                        Result.Add(j);

                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }




    public class tblCityMaster
    {

        public string CityID { get; set; }
        public string City { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string AreaCode { get; set; }
        public string PinCode { get; set; }
        public string MobileCode { get; set; }
        public string DepotID { get; set; }
        public string Depot { get; set; }



        public string ManageInsertCityMaster(tblCityMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CityID", o.CityID);
                input.Add("CityName", o.City ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("AreaCode", o.AreaCode ?? "");
                input.Add("PinCode", o.PinCode ?? "");
                input.Add("MobileCode", o.MobileCode ?? "");
                input.Add("DepotID", o.DepotID ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CityMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateCityMaster(tblCityMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CityID", o.CityID);
                input.Add("CityName", o.City ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("AreaCode", o.AreaCode ?? "");
                input.Add("PinCode", o.PinCode ?? "");
                input.Add("MobileCode", o.MobileCode ?? "");
                input.Add("DepotID", o.DepotID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CityMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteCityMaster(tblCityMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CityID", o.CityID);
                input.Add("CityName", o.City ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("AreaCode", o.AreaCode ?? "");
                input.Add("PinCode", o.PinCode ?? "");
                input.Add("MobileCode", o.MobileCode ?? "");
                input.Add("DepotID", o.DepotID ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CityMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblCityMaster> GetCityMaster(tblCityMaster o)
        {
            List<tblCityMaster> Result = new List<tblCityMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CityMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCityMaster j = new tblCityMaster();
                        j.CityID = sdr["CityID"].ToString();
                        j.City = sdr["City"].ToString();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();
                        j.AreaCode = sdr["AreaCode"].ToString();
                        j.PinCode = sdr["PinCode"].ToString();
                        j.MobileCode = sdr["MobileCode"].ToString();
                        j.DepotID = sdr["DepotID"].ToString();
                        j.Depot = sdr["Depot"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }




    public class tblCurrencyMaster
    {

        public string CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }
        public string NoOfDecimals { get; set; }
        public string MonetaryUnit { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string StatusBaseCurrency { get; set; }
        public string StatusBaseCurrencyDisp { get; set; }



        public string ManageInsertCurrencyMaster(tblCurrencyMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CurrencyID", o.CurrencyID);
                input.Add("CurrencyName", o.CurrencyName ?? "");
                input.Add("Symbol", o.Symbol ?? "");
                input.Add("NoOfDecimals", o.NoOfDecimals ?? "");
                input.Add("MonetaryUnit", o.MonetaryUnit ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("StatusBaseCurrency", o.StatusBaseCurrency ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CurrencyMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateCurrencyMaster(tblCurrencyMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CurrencyID", o.CurrencyID);
                input.Add("CurrencyName", o.CurrencyName ?? "");
                input.Add("Symbol", o.Symbol ?? "");
                input.Add("NoOfDecimals", o.NoOfDecimals ?? "");
                input.Add("MonetaryUnit", o.MonetaryUnit ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("StatusBaseCurrency", o.StatusBaseCurrency ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CurrencyMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteCurrencyMaster(tblCurrencyMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CurrencyID", o.CurrencyID);
                input.Add("CurrencyName", o.CurrencyName ?? "");
                input.Add("Symbol", o.Symbol ?? "");
                input.Add("NoOfDecimals", o.NoOfDecimals ?? "");
                input.Add("MonetaryUnit", o.MonetaryUnit ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("StatusBaseCurrency", o.StatusBaseCurrency ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CurrencyMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblCurrencyMaster> GetCurrencyMaster(tblCurrencyMaster o)
        {
            List<tblCurrencyMaster> Result = new List<tblCurrencyMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CurrencyMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCurrencyMaster j = new tblCurrencyMaster();
                        j.CurrencyID = sdr["CurrencyID"].ToString();
                        j.CurrencyName = sdr["CurrencyName"].ToString();
                        j.Symbol = sdr["Symbol"].ToString();
                        j.NoOfDecimals = sdr["NoOfDecimals"].ToString();
                        j.MonetaryUnit = sdr["MonetaryUnit"].ToString();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();

                        j.StatusBaseCurrency = sdr["StatusBaseCurrency"].ToString();
                        j.StatusBaseCurrencyDisp = sdr["StatusBaseCurrencyDisp"].ToString();

                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }


    public class tblBranchMaster
    {
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public string LocationID { get; set; }
        public string KeyPerson { get; set; }
        public string DesignationID { get; set; }
        public string Phone { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PhoneNo3 { get; set; }
        public string PhoneNo4 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        public string BranchPrefix { get; set; }
        public string CurrencyID { get; set; }
        public string AcCompanyID { get; set; }
        public string StatusAssociate { get; set; }


        public string CountryName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string CurrencyName { get; set; }


        public string ManageInsertBranchMaster(tblBranchMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("BranchID", o.BranchID);
                input.Add("BranchName", o.BranchName ?? "");
                input.Add("Address1", o.Address1 ?? "");
                input.Add("Address2", o.Address2 ?? "");
                input.Add("Address3", o.Address3 ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("KeyPerson", o.KeyPerson ?? "");
                input.Add("DesignationID", o.DesignationID ?? "");
                input.Add("Phone", o.Phone ?? "");
                input.Add("PhoneNo1", o.PhoneNo1 ?? "");
                input.Add("PhoneNo2", o.PhoneNo2 ?? "");
                input.Add("PhoneNo3", o.PhoneNo3 ?? "");
                input.Add("PhoneNo4", o.PhoneNo4 ?? "");
                input.Add("MobileNo1", o.MobileNo1 ?? "");
                input.Add("MobileNo2", o.MobileNo2 ?? "");
                input.Add("EMail", o.EMail ?? "");
                input.Add("Website", o.Website ?? "");
                input.Add("BranchPrefix", o.BranchPrefix ?? "");
                input.Add("CurrencyID", o.CurrencyID ?? "");
                input.Add("AcCompanyID", o.AcCompanyID ?? "");
                input.Add("StatusAssociate", o.StatusAssociate ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[BranchMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateBranchMaster(tblBranchMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("BranchID", o.BranchID);
                input.Add("BranchName", o.BranchName ?? "");
                input.Add("Address1", o.Address1 ?? "");
                input.Add("Address2", o.Address2 ?? "");
                input.Add("Address3", o.Address3 ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("KeyPerson", o.KeyPerson ?? "");
                input.Add("DesignationID", o.DesignationID ?? "");
                input.Add("Phone", o.Phone ?? "");
                input.Add("PhoneNo1", o.PhoneNo1 ?? "");
                input.Add("PhoneNo2", o.PhoneNo2 ?? "");
                input.Add("PhoneNo3", o.PhoneNo3 ?? "");
                input.Add("PhoneNo4", o.PhoneNo4 ?? "");
                input.Add("MobileNo1", o.MobileNo1 ?? "");
                input.Add("MobileNo2", o.MobileNo2 ?? "");
                input.Add("EMail", o.EMail ?? "");
                input.Add("Website", o.Website ?? "");
                input.Add("BranchPrefix", o.BranchPrefix ?? "");
                input.Add("CurrencyID", o.CurrencyID ?? "");
                input.Add("AcCompanyID", o.AcCompanyID ?? "");
                input.Add("StatusAssociate", o.StatusAssociate ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[BranchMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteBranchMaster(tblBranchMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("BranchID", o.BranchID);
                input.Add("BranchName", o.BranchName ?? "");
                input.Add("Address1", o.Address1 ?? "");
                input.Add("Address2", o.Address2 ?? "");
                input.Add("Address3", o.Address3 ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("KeyPerson", o.KeyPerson ?? "");
                input.Add("DesignationID", o.DesignationID ?? "");
                input.Add("Phone", o.Phone ?? "");
                input.Add("PhoneNo1", o.PhoneNo1 ?? "");
                input.Add("PhoneNo2", o.PhoneNo2 ?? "");
                input.Add("PhoneNo3", o.PhoneNo3 ?? "");
                input.Add("PhoneNo4", o.PhoneNo4 ?? "");
                input.Add("MobileNo1", o.MobileNo1 ?? "");
                input.Add("MobileNo2", o.MobileNo2 ?? "");
                input.Add("EMail", o.EMail ?? "");
                input.Add("Website", o.Website ?? "");
                input.Add("BranchPrefix", o.BranchPrefix ?? "");
                input.Add("CurrencyID", o.CurrencyID ?? "");
                input.Add("AcCompanyID", o.AcCompanyID ?? "");
                input.Add("StatusAssociate", o.StatusAssociate ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[BranchMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblBranchMaster> GetBranchMaster(tblBranchMaster o)
        {
            List<tblBranchMaster> Result = new List<tblBranchMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[BranchMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblBranchMaster j = new tblBranchMaster();
                        j.BranchID = sdr["BranchID"].ToString();
                        j.BranchName = sdr["BranchName"].ToString();
                        j.Address1 = sdr["Address1"].ToString();
                        j.Address2 = sdr["Address2"].ToString();
                        j.Address3 = sdr["Address3"].ToString();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CityID = sdr["CityID"].ToString();
                        j.LocationID = sdr["LocationID"].ToString();
                        j.KeyPerson = sdr["KeyPerson"].ToString();
                        j.DesignationID = sdr["DesignationID"].ToString();
                        j.Phone = sdr["Phone"].ToString();
                        j.PhoneNo1 = sdr["PhoneNo1"].ToString();
                        j.PhoneNo2 = sdr["PhoneNo2"].ToString();
                        j.PhoneNo3 = sdr["PhoneNo3"].ToString();
                        j.PhoneNo4 = sdr["PhoneNo4"].ToString();
                        j.MobileNo1 = sdr["MobileNo1"].ToString();
                        j.MobileNo2 = sdr["MobileNo2"].ToString();
                        j.EMail = sdr["EMail"].ToString();
                        j.Website = sdr["Website"].ToString();
                        j.BranchPrefix = sdr["BranchPrefix"].ToString();
                        j.CurrencyID = sdr["CurrencyID"].ToString();
                        j.AcCompanyID = sdr["AcCompanyID"].ToString();
                        j.StatusAssociate = sdr["StatusAssociate"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();
                        j.City = sdr["City"].ToString();
                        j.Location = sdr["Location"].ToString();
                        j.Designation = sdr["Designation"].ToString();
                        j.CurrencyName = sdr["CurrencyName"].ToString();

                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }
            }
        }
    }



    public class tblAreaMaster
    {

        public string AreaID { get; set; }
        public string LocationID { get; set; }
        public string Location { get; set; }
        public string AreaName { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }


        public string ManageInsertAreaMaster(tblAreaMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AreaID", o.AreaID);
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("AreaName", o.AreaName ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AreaMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateAreaMaster(tblAreaMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AreaID", o.AreaID);
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("AreaName", o.AreaName ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AreaMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteAreaMaster(tblAreaMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("AreaID", o.AreaID);
                input.Add("LocationID", o.LocationID ?? "");
                input.Add("AreaName", o.AreaName ?? "");
                input.Add("CountryID", o.CountryID ?? "");
                input.Add("CityID", o.CityID ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[AreaMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblAreaMaster> GetAreaMaster(tblAreaMaster o)
        {
            List<tblAreaMaster> Result = new List<tblAreaMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[AreaMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblAreaMaster j = new tblAreaMaster();
                        j.AreaID = sdr["AreaID"].ToString();
                        j.LocationID = sdr["LocationID"].ToString();
                        j.Location = sdr["Location"].ToString();
                        j.AreaName = sdr["AreaName"].ToString();
                        j.CountryID = sdr["CountryID"].ToString();
                        j.CountryName = sdr["CountryName"].ToString();
                        j.CityID = sdr["CityID"].ToString();
                        j.City = sdr["City"].ToString();
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



    public class tblImpExpDocumentMaster
    {

        public string DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string IMPEXPCode { get; set; }
        public string PortID { get; set; }
        public string CustomerID { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string CustomerName { get; set; }
        public string PortName { get; set; }


        public string ManageInsertImpExpDocumentMaster(tblImpExpDocumentMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentID", o.DocumentID);
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("IMPEXPCode", o.IMPEXPCode ?? "");
                input.Add("PortID", o.PortID ?? "");
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ImpExpDocumentMasterInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateImpExpDocumentMaster(tblImpExpDocumentMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentID", o.DocumentID);
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("IMPEXPCode", o.IMPEXPCode ?? "");
                input.Add("PortID", o.PortID ?? "");
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");

                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ImpExpDocumentMasterUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteImpExpDocumentMaster(tblImpExpDocumentMaster o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("DocumentID", o.DocumentID);
                input.Add("DocumentName", o.DocumentName ?? "");
                input.Add("IMPEXPCode", o.IMPEXPCode ?? "");
                input.Add("PortID", o.PortID ?? "");
                input.Add("CustomerID", o.CustomerID ?? "");
                input.Add("IssueDate", o.IssueDate ?? "");
                input.Add("ExpiryDate", o.ExpiryDate ?? "");

                output.Add("Result", "");
                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[ImpExpDocumentMasterDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblImpExpDocumentMaster> GetImpExpDocumentMaster(tblImpExpDocumentMaster o)
        {
            List<tblImpExpDocumentMaster> Result = new List<tblImpExpDocumentMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ImpExpDocumentMasterSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblImpExpDocumentMaster j = new tblImpExpDocumentMaster();

                        j.DocumentID = sdr["DocumentID"].ToString();
                        j.DocumentName = sdr["DocumentName"].ToString();
                        j.IMPEXPCode = sdr["IMPEXPCode"].ToString();
                        j.PortID = sdr["PortID"].ToString();
                        j.PortName = sdr["PortName"].ToString();
                        j.CustomerID = sdr["CustomerID"].ToString();
                        j.CustomerName = sdr["CustomerName"].ToString();
                        j.IssueDate = Convert.ToDateTime(sdr["IssueDate"]).ToString("yyyy-MM-dd"); 
                        j.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"]).ToString("yyyy-MM-dd");  
                        Result.Add(j);
                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }


    public class tblCustomerRateType
    {

        public string CustomerRateTypeID { get; set; }
        public string CustomerRateType { get; set; }
        public string ZoneCategoryID { get; set; }
        public string ZoneCategory { get; set; }
        public string StatusDefault { get; set; }


        public string ManageInsertCustomerRateType(tblCustomerRateType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CustomerRateTypeID", o.CustomerRateTypeID);
                input.Add("CustomerRateType", o.CustomerRateType ?? "");
                input.Add("ZoneCategoryID", o.ZoneCategoryID ?? "");
                input.Add("StatusDefault", o.StatusDefault ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CustomerRateTypeInsert]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageUpdateCustomerRateType(tblCustomerRateType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CustomerRateTypeID", o.CustomerRateTypeID);
                input.Add("CustomerRateType", o.CustomerRateType ?? "");
                input.Add("ZoneCategoryID", o.ZoneCategoryID ?? "");
                input.Add("StatusDefault", o.StatusDefault ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CustomerRateTypeUpdate]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public string ManageDeleteCustomerRateType(tblCustomerRateType o)
        {
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>();
                Dictionary<string, string> output = new Dictionary<string, string>();

                input.Add("CustomerRateTypeID", o.CustomerRateTypeID);
                input.Add("CustomerRateType", o.CustomerRateType ?? "");
                input.Add("ZoneCategoryID", o.ZoneCategoryID ?? "");
                input.Add("StatusDefault", o.StatusDefault ?? "");
                output.Add("Result", "");

                Dictionary<string, string> res = ngFun.ExecuteSQL("[dbo].[CustomerRateTypeDelete]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }


        public List<tblCustomerRateType> GetCustomerRateType(tblCustomerRateType o)
        {
            List<tblCustomerRateType> Result = new List<tblCustomerRateType>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CustomerRateTypeSelectAll]", con))
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        tblCustomerRateType j = new tblCustomerRateType();
                        j.CustomerRateTypeID = sdr["CustomerRateTypeID"].ToString();
                        j.CustomerRateType = sdr["CustomerRateType"].ToString();
                        j.ZoneCategoryID = sdr["ZoneCategoryID"].ToString();
                        j.StatusDefault = sdr["StatusDefault"].ToString();
                        j.ZoneCategory = sdr["ZoneCategory"].ToString();
                        Result.Add(j);

                    }
                    sdr.Close();
                    con.Close();
                    return Result;
                }

            }
        }
    }



























}
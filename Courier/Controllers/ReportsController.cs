﻿using LTMSV2.DAL;
using LTMSV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class ReportsController :Controller
    {
        Entities1 db = new Entities1();
        #region "Empost"
        public ActionResult EmposFeeReport()
        {
            ViewBag.ReportName = "Empost Fee Analysis Report";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("EmpostFee_"))
                {
                    Session["ReportOutput"] = null;
                }
            }
            return View();
        }
        public ActionResult ReportFrame()
        {
            if (Session["ReportOutput"] != null)
                ViewBag.ReportOutput = Session["ReportOutput"].ToString();
            else
            {
                string reportpath = AccountsReportsDAO.GenerateDefaultReport();
                ViewBag.ReportOutput = reportpath; // "~/Reports/DefaultReport.pdf";
            }
            return PartialView();
        }
        public ActionResult PrintSearch()
        {
            AccountsReportParam reportparam = SessionDataModel.GetAccountsParam();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime pFromDate;
            DateTime pToDate;

            if (reportparam == null)
            {
                pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                pToDate = CommanFunctions.GetLastDayofMonth().Date;
                reportparam = new AccountsReportParam();
                reportparam.FromDate = pFromDate;
                reportparam.ToDate = pToDate;
                reportparam.AcHeadId = 0;
                reportparam.AcHeadName = "";
                reportparam.Output = "PDF";
            }
            else
            {
                if (reportparam.FromDate.Date.ToString() == "01-01-0001 00:00:00")
                {
                    pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                    reportparam.FromDate = pFromDate;
                    reportparam.Output = "PDF";
                }

            }
                        
            SessionDataModel.SetAccountsParam(reportparam);

            return View(reportparam);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintSearch([Bind(Include = "FromDate,ToDate,Output")] AccountsReportParam picker)
        {
            AccountsReportParam model = new AccountsReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate,
                Output = picker.Output
            };


            ViewBag.Token = model;
            SessionDataModel.SetAccountsParam(model);
            
            AccountsReportsDAO.GenerateEmposFeeReport();
            return RedirectToAction("EmposFeeReport", "Reports");


        }

        #endregion

        #region "ConsignmentRegister"
        public ActionResult AWBRegister()
        {
            ViewBag.ReportName = "Consignment Note Register";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("ConsignmentRegister_"))
                {
                    Session["ReportOutput"] = null;
                }
            }
           
            return View();
        }


        public ActionResult AWBReportParam()
        {
            AWBReportParam reportparam = SessionDataModel.GetAWBReportParam();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime pFromDate;
            DateTime pToDate;

            if (reportparam == null)
            {
                pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                pToDate = CommanFunctions.GetLastDayofMonth().Date;
                reportparam = new AWBReportParam();
                reportparam.FromDate = pFromDate;
                reportparam.ToDate = pToDate;
                reportparam.ParcelTypeId ="1,2";
                reportparam.PaymentModeId= 0;
                //reportparam.MovementId = "1,2,3,4";
                reportparam.Output = "PDF";
                reportparam.SortBy = "Date Wise";
                reportparam.ReportType = "Date";
                reportparam.RevenueUpdated = "True";
            }
            else
            {
                if (reportparam.FromDate.Date.ToString() == "01-01-0001 00:00:00")
                {
                    pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                    reportparam.FromDate = pFromDate;
                    reportparam.Output = "PDF";
                }
                else
                {

                }

            }

            SessionDataModel.SetAWBReportParam(reportparam);
            ViewBag.PaymentMode = db.tblPaymentModes.ToList();
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            //lsttype.Add(new VoucherTypeVM { TypeName = "All" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Consignee" });

            ViewBag.InvoiceTo = lsttype;
            //ViewBag.Movement = db.CourierMovements.ToList();
            return View(reportparam);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AWBReportParam([Bind(Include = "FromDate,ToDate,PaymentModeId,SelectedValues,MovementId,ParcelTypeId,Output,ReportType,SortBy,InvoicedTo,RevenueUpdated")] AWBReportParam picker)
        {
            AWBReportParam model = new AWBReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate,
                PaymentModeId=picker.PaymentModeId,
                ParcelTypeId=picker.ParcelTypeId,
                MovementId =picker.MovementId,
                Output = picker.Output,
                ReportType=picker.ReportType,
                SortBy =picker.SortBy,
                InvoicedTo=picker.InvoicedTo,
                RevenueUpdated =picker.RevenueUpdated
                

            };
            model.MovementId = "";
            if (picker.SelectedValues != null)
            {
                foreach (var item in picker.SelectedValues)
                {
                    if (model.ParcelTypeId == "" || model.ParcelTypeId==null)
                    {
                        model.ParcelTypeId = item.ToString();
                    }
                    else
                    {
                        model.ParcelTypeId = model.ParcelTypeId + "," + item.ToString();
                    }

                }
            }

            ViewBag.Token = model;
            SessionDataModel.SetAWBReportParam(model);

            AccountsReportsDAO.GenerateConsignmentRegister();
            return RedirectToAction("AWBRegister", "Reports");


        }


        #endregion

        #region "TaxRegister"
        public ActionResult TaxRegister()
        {
            ViewBag.ReportName = "Tax Register";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("TaxRegister_"))
                {
                    Session["ReportOutput"] = null;
                }
            }

            return View();
        }


        public ActionResult TaxReportParam()
        {
            TaxReportParam reportparam = SessionDataModel.GetTaxReportParam();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime pFromDate;
            DateTime pToDate;

            if (reportparam == null)
            {
                pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                pToDate = CommanFunctions.GetLastDayofMonth().Date;
                reportparam = new TaxReportParam();
                reportparam.FromDate = pFromDate;
                reportparam.ToDate = pToDate;
                reportparam.Output = "PDF";
                reportparam.SortBy = "Date Wise";
                reportparam.ReportType = "Date";
            }
            else
            {
                if (reportparam.FromDate.Date.ToString() == "01-01-0001 00:00:00")
                {
                    pFromDate = CommanFunctions.GetFirstDayofMonth().Date; //.AddDays(-1);
                    reportparam.FromDate = pFromDate;
                    reportparam.Output = "PDF";
                }
                else
                {

                }

            }

            SessionDataModel.SetTaxReportParam(reportparam);
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            lsttype.Add(new VoucherTypeVM { TypeName = "All" });
            var typeitems = (from c in db.AcJournalMasters select new VoucherTypeVM { TypeName = c.VoucherType }).Distinct().ToList();
            foreach (VoucherTypeVM Item in typeitems)
            {
                lsttype.Add(Item);
            }
            
            ViewBag.VoucherTypes = lsttype;
            return View(reportparam);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaxReportParam([Bind(Include = "FromDate,ToDate,TransactionType,Output,ReportType,SortBy")] TaxReportParam picker)
        {
            TaxReportParam model = new TaxReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate,
                TransactionType= picker.TransactionType,                
                Output = picker.Output,
                ReportType = picker.ReportType,
                SortBy = picker.SortBy

            };            

            ViewBag.Token = model;
            SessionDataModel.SetTaxReportParam(model);

            AccountsReportsDAO.GenerateTaxRegister();
            return RedirectToAction("TaxRegister", "Reports");


        }


        #endregion

        #region "CustomerLedger"
        public ActionResult CustomerLedger()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerLedgerReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerLedgerParam(model);

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Customer Ledger";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerLedger"))
                {
                    Session["ReportOutput"] = null;
                }
                //else if (!currentreport.Contains("CustomerOutStanding") && model.ReportType == "OutStanding")
                //{
                //    Session["ReportOutput"] = null;
                //}
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerLedger(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = "PDF",
                ReportType = "Ledger",
                AsonDate=picker.AsonDate
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
           
            
                AccountsReportsDAO.GenerateCustomerLedgerDetailReport();
           
            
            return RedirectToAction("CustomerLedger", "Reports");


        }
        #endregion

        #region "CustomerStatement"
        public ActionResult CustomerStatement()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerStatementReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            if (model.AsonDate.ToString() == "01-01-0001 00:00:00")
            {
                model.AsonDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerStatementParam(model);

            
            model.AsonDate = AccountsDAO.CheckParamDate(model.AsonDate, yearid).Date;
            //model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Customer Statement";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerStatement"))
                {
                    Session["ReportOutput"] = null;
                }
                
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerStatement(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),                                
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType,
                AsonDate = picker.AsonDate
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerStatementParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();           
                        
            AccountsReportsDAO.GenerateCustomerStatementReport();            

            return RedirectToAction("CustomerStatement", "Reports");


        }
        #endregion
        #region "CustomerOustanding"
        public ActionResult CustomerOutstanding()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerLedgerReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "OutStanding"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerLedgerParam(model);

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Customer Outstanding";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
               if (!currentreport.Contains("CustomerOutStanding") && model.ReportType=="OutStanding")
                {                    
                    Session["ReportOutput"] = null;
                }
                else if (!currentreport.Contains("AWBOutStanding") && model.ReportType == "AWBUnAllocated")
                {
                    Session["ReportOutput"] = null;
                }
                else if (!currentreport.Contains("AWBUnInvoiced") && model.ReportType == "AWBOutStanding")
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerOutstanding(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = "OutStanding"
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (model.ReportType == "Ledger")
            {
                //AccountsReportsDAO.GenerateCustomerLedgerReport();
                AccountsReportsDAO.GenerateCustomerLedgerDetailReport();
            }
            else if (model.ReportType == "OutStanding")
            {
                AccountsReportsDAO.GenerateCustomerOutStandingReport();
            }
            else if (model.ReportType == "AWBUnAllocated")
            {
                AccountsReportsDAO.GenerateAWBOutStandingReport();
            }            
            else if (model.ReportType == "AWBOutStanding")
            {
                AccountsReportsDAO.GenerateAWBUnInvoiced();
            }

            return RedirectToAction("CustomerOutstanding", "Reports");


        }
        #endregion

        #region "ConsignmentUnAllocated"
        public ActionResult ConsignmentUnAllocate()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerLedgerReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "OutStanding"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerLedgerParam(model);

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Consignment Allocation Pending";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerOutStanding") && model.ReportType == "OutStanding")
                {
                    Session["ReportOutput"] = null;
                }
                else if (!currentreport.Contains("AWBOutStanding") && model.ReportType == "AWBUnAllocated")
                {
                    Session["ReportOutput"] = null;
                }
                else if (!currentreport.Contains("AWBUnInvoiced") && model.ReportType == "AWBOutStanding")
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult ConsignmentUnAllocate(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (model.ReportType == "Ledger")
            {
                //AccountsReportsDAO.GenerateCustomerLedgerReport();
                AccountsReportsDAO.GenerateCustomerLedgerDetailReport();
            }
            else if (model.ReportType == "OutStanding")
            {
                AccountsReportsDAO.GenerateCustomerOutStandingReport();
            }
            else if (model.ReportType == "AWBUnAllocated")
            {
                AccountsReportsDAO.GenerateAWBOutStandingReport();
            }
            else if (model.ReportType == "AWBOutStanding")
            {
                AccountsReportsDAO.GenerateAWBUnInvoiced();
            }

            return RedirectToAction("ConsignmentUnAllocate", "Reports");


        }
        #endregion
        public ActionResult CustomerProfitAnalysis()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerLedgerReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "OutStanding"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerLedgerParam(model);

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Customer Profit Analysis";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerProfit"))
                {
                    Session["ReportOutput"] = null;
                }                
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerProfitAnalysis(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
           
            AccountsReportsDAO.GenerateCustomerProfit();
           
            return RedirectToAction("CustomerProfitAnalysis", "Reports");


        }
        #region supplierledger
        public ActionResult SupplierLedger()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            var supplierMasterTypes = (from d in db.SupplierTypes select d).ToList();
            ViewBag.SupplierType = supplierMasterTypes;
            SupplierLedgerReportParam model = SessionDataModel.GetSupplierLedgerReportParam();
            if (model == null)
            {
                model = new SupplierLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    SupplierTypeId = 1,
                    SupplierId = 0,
                    SupplierName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }
            if (model.AsonDate.ToString() == "01-01-0001 00:00:00")
            {
                model.AsonDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            SessionDataModel.SetSupplierLedgerParam(model);
            ViewBag.ReportName = "Supplier Ledger";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("SupplierLedger") )
                {
                    Session["ReportOutput"] = null;
                }                
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult SupplierLedger(SupplierLedgerReportParam picker)
        {

            SupplierLedgerReportParam model = new SupplierLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate,
                AsonDate=picker.AsonDate,
                SupplierId = picker.SupplierId,
                SupplierName = picker.SupplierName,
                SupplierTypeId=picker.SupplierTypeId,
                Output = picker.Output,
                ReportType = "Ledger"
            };

            ViewBag.Token = model;
            SessionDataModel.SetSupplierLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (model.ReportType == "Ledger")
            {
                //AccountsReportsDAO.GenerateCustomerLedgerReport();
                AccountsReportsDAO.GenerateSupplierLedgerDetailReport();
            }
            else if (model.ReportType == "Statement")
            {
                AccountsReportsDAO.GenerateSupplierStatementDetailReport();
            }            

            return RedirectToAction("SupplierLedger", "Reports");


        }
        #endregion

        #region supplierStatement
        public ActionResult SupplierStatement()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            var supplierMasterTypes = (from d in db.SupplierTypes select d).ToList();
            ViewBag.SupplierType = supplierMasterTypes;
            SupplierLedgerReportParam model = SessionDataModel.GetSupplierLedgerReportParam();
            if (model == null)
            {
                model = new SupplierLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    SupplierTypeId = 1,
                    SupplierId = 0,
                    SupplierName = "",
                    Output = "PDF",
                    ReportType = "Statement"
                };
            }
            if (model.AsonDate.ToString() == "01-01-0001 00:00:00")
            {
                model.AsonDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
           

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            SessionDataModel.SetSupplierLedgerParam(model);
            ViewBag.ReportName = "Supplier Statement";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("SupplierStatement"))
                {
                    Session["ReportOutput"] = null;
                }                
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult SupplierStatement(SupplierLedgerReportParam picker)
        {
            SupplierLedgerReportParam model = SessionDataModel.GetSupplierLedgerReportParam();        
            model = new SupplierLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                AsonDate = picker.AsonDate,
                SupplierId = picker.SupplierId,
                SupplierName = picker.SupplierName,
                SupplierTypeId = picker.SupplierTypeId,
                Output = picker.Output,
                ReportType = "Statement"
            };

            ViewBag.Token = model;
            SessionDataModel.SetSupplierLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
           
           
                AccountsReportsDAO.GenerateSupplierStatementDetailReport();
           

            return RedirectToAction("SupplierStatement", "Reports");


        }
        #endregion
        public ActionResult ManifestReport(int id=0)
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            ManifestReportParam model = SessionDataModel.GetManifestReportParam();
            if (model == null)
            {
                model = new ManifestReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    Output = "PDF",
                    TDNo = "",
                    TDID = 0,
                    ReportType="Detail"
                };                
            }

           
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }

            if (id > 0)
            {

                model.TDID = id;
                var td = db.TruckDetails.Find(model.TDID);//
                model.TDNo = td.ReceiptNo + "-" + td.RegNo + "-" + td.DriverName;
                ViewBag.ReportOption = "1";
                ViewBag.ReportName = "Manifest Report - " + td.ReceiptNo;
                SessionDataModel.SetManifestReportParam(model);
                AccountsReportsDAO.GenerateManifestReport();

                return View(model);
            }
            else
            {
                ViewBag.ReportOption = "0";
                SessionDataModel.SetManifestReportParam(model);

                model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
                model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

                ViewBag.ReportName = "Manifest Report";
                if (Session["ReportOutput"] != null)
                {
                    string currentreport = Session["ReportOutput"].ToString();
                    if (!currentreport.Contains("TripManifestReport"))
                    {
                        Session["ReportOutput"] = null;
                    }
                }

            }
            return View(model);

        }

        [HttpPost]
        public ActionResult ManifestReport(ManifestReportParam picker)
        {

            ManifestReportParam model = new ManifestReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                Output = picker.Output,
                TDID = picker.TDID,
                TDNo = picker.TDNo,
                ReportType=picker.ReportType
            };


            SessionDataModel.SetManifestReportParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            if (picker.ReportType=="Summary")
            {
                AccountsReportsDAO.GenerateManifestSummaryReport();
            }
            else
            {
                AccountsReportsDAO.GenerateManifestReport();
            }

            
            //if (id > 0)
            //{
            //    model.TDID = id;
            //    var td = db.TruckDetails.Find(model.TDID);//
            //    model.TDNo = td.ReceiptNo + "-" + td.RegNo + "-" + td.DriverName;
            //    ViewBag.ReportOption = "1";
            //}
            //else
            //{
            //    ViewBag.ReportOption = "0";
            //}
            return View(model);
            //return RedirectToAction("ManifestReport", "Accounts");


        }

        #region "CustomerAging"
        public ActionResult CustomerAging()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = SessionDataModel.GetCustomerLedgerReportParam();
            if (model == null)
            {
                model = new CustomerLedgerReportParam                {
                    
                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Summary"
                };
            }
            if (model.AsonDate.ToString() == "01-01-0001 00:00:00")
            {
                model.AsonDate = CommanFunctions.GetLastDayofMonth().Date;
            }

            SessionDataModel.SetCustomerLedgerParam(model);

            model.AsonDate = AccountsDAO.CheckParamDate(model.AsonDate, yearid).Date;            
            ViewBag.ReportName = "Customer Aging Report";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerAging"))
                {
                    Session["ReportOutput"] = null;
                }                
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerAging(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {               
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType,
                AsonDate = picker.AsonDate
            };

            ViewBag.Token = model;
            SessionDataModel.SetCustomerLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            AccountsReportsDAO.GenerateCustomerAgingReport();

            return RedirectToAction("CustomerAging", "Reports");


        }
        #endregion


        #region "SupplierAging"
        public ActionResult SupplierAging()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            var supplierMasterTypes = (from d in db.SupplierTypes select d).ToList();
            ViewBag.SupplierType = supplierMasterTypes;

            SupplierLedgerReportParam model = SessionDataModel.GetSupplierLedgerReportParam();
            if (model == null)
            {
                model = new SupplierLedgerReportParam
                {

                    AsonDate = CommanFunctions.GetLastDayofMonth().Date, //.AddDays(-1);,
                    SupplierId = 0,
                    SupplierName = "",
                    SupplierTypeId=1,
                    Output = "PDF",
                    ReportType = "Summary"
                };
            }
            if (model.AsonDate.ToString() == "01-01-0001 00:00:00")
            {
                model.AsonDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            model.AsonDate = AccountsDAO.CheckParamDate(model.AsonDate, yearid).Date;
            SessionDataModel.SetSupplierLedgerParam(model);
                        
            ViewBag.ReportName = "Supplier Aging Report";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("SupplierAging"))
                {
                    Session["ReportOutput"] = null;
                }
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult SupplierAging(SupplierLedgerReportParam picker)
        {

            SupplierLedgerReportParam model = new SupplierLedgerReportParam
            {
                SupplierId = picker.SupplierId,
                SupplierName = picker.SupplierName,
                SupplierTypeId=picker.SupplierTypeId,
                Output = picker.Output,
                ReportType = picker.ReportType,
                AsonDate = picker.AsonDate
            };

            ViewBag.Token = model;
            SessionDataModel.SetSupplierLedgerParam(model);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            AccountsReportsDAO.GenerateSupplierAgingReport();

            return RedirectToAction("SupplierAging", "Reports");


        }
        #endregion

        public ActionResult ConsignmentTimeLine()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            AWBTimeLineReportParam model = (AWBTimeLineReportParam)Session["AWBTimeLineParam"];
            if (model == null)
            {
                model = new AWBTimeLineReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    Output = "PDF"                    
                };
            }


            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }

            ViewBag.ReportName = "Consignment TimeLine Report ";
            Session["AWBTimeLineParam"] = model;
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("AWBTimeLine"))
                {
                    Session["ReportOutput"] = null;
                }
                
            }
            return View(model);                               

        }

        [HttpPost]
        public ActionResult ConsignmentTimeLine(AWBTimeLineReportParam picker)
        {

        AWBTimeLineReportParam model = new AWBTimeLineReportParam
        {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                Output = picker.Output
                
            };


         Session["AWBTimeLineParam"] = model;
         Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            AccountsReportsDAO.GenerateAWBTimeLineReport();

            //return View(model);
            return RedirectToAction("ConsignmentTimeLine", "Reports");


        }

        #region "SalesReport"
        public ActionResult SalesReport()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            ViewBag.Employee = db.EmployeeMasters.ToList();
            SalesReportParam model =(SalesReportParam)Session["SalesReportParam"];
            if (model == null)
            {
                model = new SalesReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    EmployeeID = 0,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
           
            Session["SalesReportParam"]=model;
                      
            //model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Sales Report";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("SalesReport"))
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult SalesReport(SalesReportParam picker)
        {

            SalesReportParam model = new SalesReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                EmployeeID = picker.EmployeeID,
                ReportType = picker.ReportType                
            };

            ViewBag.Token = model;
            Session["SalesReportParam"] = model; 
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            AccountsReportsDAO.GenerateSalesReport();

            return RedirectToAction("SalesReport", "Reports");


        }
        #endregion

        //#region "VoucherPrint"
        //public ActionResult VoucherPrint(string type,int id = 0)
        //{
        //    int yearid = Convert.ToInt32(Session["fyearid"].ToString());

        //    if (id > 0  && type!="")
        //    {                               

        //        ViewBag.ReportOption = "Receipt Voucher";
        //        ViewBag.ReportName = "Receipt Voucher";


        //        AccountsReportsDAO.GenerateCustomerReceiptPrint(id);           
        //    }

        //    return View();

        //}

        //[HttpPost]
        //public ActionResult ManifestReport(ManifestReportParam picker)
        //{

        //    ManifestReportParam model = new ManifestReportParam
        //    {
        //        FromDate = picker.FromDate,
        //        ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
        //        Output = picker.Output,
        //        TDID = picker.TDID,
        //        TDNo = picker.TDNo
        //    };


        //    SessionDataModel.SetManifestReportParam(model);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();


        //    AccountsReportsDAO.GenerateManifestReport();
        //    //if (id > 0)
        //    //{
        //    //    model.TDID = id;
        //    //    var td = db.TruckDetails.Find(model.TDID);//
        //    //    model.TDNo = td.ReceiptNo + "-" + td.RegNo + "-" + td.DriverName;
        //    //    ViewBag.ReportOption = "1";
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.ReportOption = "0";
        //    //}
        //    return View(model);
        //    //return RedirectToAction("ManifestReport", "Accounts");


        //}

        //#endregion

        #region "CustomerInvoiceRegister"
        public ActionResult CustomerInvoiceRegister()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            CustomerLedgerReportParam model = (CustomerLedgerReportParam)Session["CustomerInvoiceRegisterParam"];

            if (model == null)
            {
                model = new CustomerLedgerReportParam
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date,
                    AsonDate = CommanFunctions.GetFirstDayofMonth().Date, //.AddDays(-1);,
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }
            if (model.FromDate.ToString() == "01-01-0001 00:00:00")
            {
                model.FromDate = CommanFunctions.GetFirstDayofMonth().Date;
            }

            if (model.ToDate.ToString() == "01-01-0001 00:00:00")
            {
                model.ToDate = CommanFunctions.GetLastDayofMonth().Date;
            }
            SessionDataModel.SetCustomerLedgerParam(model);

            model.FromDate = AccountsDAO.CheckParamDate(model.FromDate, yearid).Date;
            model.ToDate = AccountsDAO.CheckParamDate(model.ToDate, yearid).Date;

            ViewBag.ReportName = "Customer Invoice Register";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerInvoiceRegister"))
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerInvoiceRegister(CustomerLedgerReportParam picker)
        {

            CustomerLedgerReportParam model = new CustomerLedgerReportParam
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType,
                AsonDate = picker.AsonDate
            };

            ViewBag.Token = model;
            Session["CustomerInvoiceRegisterParam"] = model;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            AccountsReportsDAO.GenerateCustomerInvoiceRegisterReport();

            return RedirectToAction("CustomerInvoiceRegister", "Reports");


        }
        #endregion

        #region "CustomerInvoiceOpeningRegister"
        public ActionResult CustomerInvoiceOpeningRegister()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            AcInvoiceOpeningParam model = (AcInvoiceOpeningParam)Session["CustomerInvoiceOpeningRegisterParam"];

            if (model == null)
            {
                model = new AcInvoiceOpeningParam
                {
                    CustomerId = 0,
                    CustomerName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }

            ViewBag.ReportName = "Customer Invoice Opening Register";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("CustomerInvoiceOpeningRegister"))
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult CustomerInvoiceOpeningRegister(AcInvoiceOpeningParam picker)
        {

            AcInvoiceOpeningParam model = new AcInvoiceOpeningParam
            {
                CustomerId = picker.CustomerId,
                CustomerName = picker.CustomerName,
                Output = picker.Output,
                ReportType = picker.ReportType
            };

            ViewBag.Token = model;
            Session["CustomerInvoiceOpeningRegisterParam"] = model;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            AccountsReportsDAO.GenerateCustomerOpeningRegisterReport();

            return RedirectToAction("CustomerInvoiceOpeningRegister", "Reports");


        }
        #endregion

        #region "SupplierInvoiceOpeningRegister"
        public ActionResult SupplierInvoiceOpeningRegister()
        {
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());
            var supplierMasterTypes = (from d in db.SupplierTypes select d).ToList();
            ViewBag.SupplierType = supplierMasterTypes;

            AcInvoiceOpeningParam model = (AcInvoiceOpeningParam)Session["SupplierInvoiceOpeningRegisterParam"];

            if (model == null)
            {
                model = new AcInvoiceOpeningParam
                {
                    SupplierTypeId = 0,
                    SupplierId = 0,
                    SupplierName = "",
                    Output = "PDF",
                    ReportType = "Ledger"
                };
            }

            ViewBag.ReportName = "Supplier Invoice Opening Register";
            if (Session["ReportOutput"] != null)
            {
                string currentreport = Session["ReportOutput"].ToString();
                if (!currentreport.Contains("SupplierInvoiceOpeningRegister"))
                {
                    Session["ReportOutput"] = null;
                }

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult SupplierInvoiceOpeningRegister(AcInvoiceOpeningParam picker)
        {

            AcInvoiceOpeningParam model = new AcInvoiceOpeningParam
            {
                SupplierId = picker.SupplierId,
                SupplierName = picker.SupplierName,
                Output = picker.Output,
                ReportType = picker.ReportType,
                SupplierTypeId=picker.SupplierTypeId
            };

            ViewBag.Token = model;
            Session["SupplierInvoiceOpeningRegisterParam"] = model;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            AccountsReportsDAO.GenerateSupplierOpeningRegisterReport();

            return RedirectToAction("SupplierInvoiceOpeningRegister", "Reports");


        }
        #endregion
    }
}
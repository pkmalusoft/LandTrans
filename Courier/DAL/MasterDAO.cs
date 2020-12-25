using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using LTMSV2.Models;

namespace LTMSV2.DAL
{
    public class MasterDAO
    {
      
        public static List<LocationVM> GetLocation(string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CommanFunctions.GetConnectionString);
            cmd.CommandText = "SP_QryGetLocation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@term", term);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<LocationVM> objList = new List<LocationVM>();

            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LocationVM obj = new LocationVM();
                    obj.LocationID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["LocationID"].ToString());
                    obj.CityID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CityID"].ToString());
                    obj.CountryID = CommanFunctions.ParseInt(ds.Tables[0].Rows[i]["CountryID"].ToString());                    
                    obj.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    obj.CityName = ds.Tables[0].Rows[i]["City"].ToString();
                    obj.CountryName = ds.Tables[0].Rows[i]["CountryName"].ToString();
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
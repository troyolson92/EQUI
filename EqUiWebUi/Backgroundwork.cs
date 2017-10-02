using System.Data;
using EQUICommunictionLib;
using System;
using System.Linq;

namespace EqUiWebUi
{
    public static class DataBuffer
    {
        public static DataTable Tipstatus { get; set; }
        public static DateTime TipstatusLastDt { get; set; }
    }

	public class Backgroundwork
	{
		public void UpdateTipstatus()
		{
			GadataComm gadataComm = new GadataComm();
			DataTable dt = gadataComm.RunQueryGadata(
                @"SELECT TOP(50)[controller_name]
						  ,[Date Time]
						  ,[Dress_Num]
						  ,[Weld_Counter]
						  ,[ESTremainingspotsFixed]
						  ,[ESTremainingspotsMove]
					  FROM[GADATA].[NGAC].[TipwearLast]
					  Order by [Date Time] DESC");
			if (dt.Rows.Count != 0 )
			{
                
                DataBuffer.Tipstatus = dt;

                DateTime maxDate = dt.AsEnumerable()
                            .Select(r => r.Field<DateTime>("Date Time"))
                            .Max();

                DataBuffer.TipstatusLastDt = maxDate;

            }
			else
			{
				// send message that something failed s
			}
		}


	}
}
using System.Data;
using EQUICommunictionLib;

namespace EqUiWebUi
{
    public static class DataBuffer
    {
        public static DataTable Tipstatus { get; set; }
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
			}
			else
			{
				// send message that something failed s
			}
		}


	}
}
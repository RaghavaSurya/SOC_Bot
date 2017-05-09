using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace SEVONEBot.Models
{
	public class DataEmulator
	{
		public string GetData()
		{
			string str = null;
			using (SEVONEBot.Models.Sev1BotModel.Sev1DataBaseEntities emt = new Sev1BotModel.Sev1DataBaseEntities())
			{
				List<Sev1BotModel.Sev1WorkersData> workersList= emt.Sev1WorkersData.ToList();
				str  = workersList.Where(t => t.Id == 1).First().Owner;				
			}
			return str;
		}
	}
}
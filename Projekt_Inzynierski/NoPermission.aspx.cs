using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Inzynierski
{
	public partial class NoPermission : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Page_LoadComplete(object sender, EventArgs e)
		{
			Response.AddHeader("REFRESH", "3; URL = /Default.aspx");
		}
	}
}
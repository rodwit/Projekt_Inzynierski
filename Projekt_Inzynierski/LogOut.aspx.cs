using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Inzynierski
{
	public partial class LogOut : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Page_LoadComplete(object sender, EventArgs e)
		{
			HttpContext.Current.Session.Clear();
			HttpContext.Current.Session.Abandon();
			HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
			Response.AddHeader("REFRESH", "1; URL = Default.aspx");
		}
	}
}
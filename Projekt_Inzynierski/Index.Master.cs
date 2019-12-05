using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Inzynierski
{
	public partial class Index : System.Web.UI.MasterPage
	{
		private readonly Dictionary<string, string> _pages = new Dictionary<string, string>()
		{
			{ "/Default.aspx","Strona Główna" },
			{ "/LogIn.aspx","Logowanie" },
			{ "/Invoices.aspx","Faktury" },
			{ "/AddInvoice.aspx","Wystaw fakturę" },
			{ "/AddProduct.aspx","Dodaj produkt" },
			{ "/LogOut.aspx","Wylogowywanie..." },
			{ "/Product.aspx","Produkty" },
			{ "/AddContractor.aspx","Dodaj kontrahenta" },
			{ "/EditContractor.aspx","Edytuj kontrahenta" },
			{ "/Contractors.aspx","Kontrahenci" },
			{ "/NoPermission.aspx","Brak uprawnień" }
		};
		protected void Page_Load(object sender, EventArgs e)
		{
			// change page title
			string page = this.Page.Request.FilePath;
			foreach(string ele in _pages.Keys)
			{
				if (ele == page)
				{
					module_name.InnerHtml = _pages[ele];
					break;
				}
			}

			if((page != "/NoPermission.aspx") && (page != "/LogIn.aspx") && (page != "/LogOut.aspx") && (page != "/Default.aspx") && !checkPermission())
				Response.Redirect("/NoPermission.aspx");
		}


		private bool checkPermission()
		{
			if (Session["User"] != null)
				return true;
			return false;
		}
	}
}
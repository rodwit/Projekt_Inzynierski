using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Inzynierski
{
	public partial class Product : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void GridViewProducts_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "DeleteProduct")
				deleteProduct(Convert.ToInt32(e.CommandArgument));
		}

		private void deleteProduct(int index)
		{
			if (!BaseConnection.openConnection())
				return;

			string query = "delete from Products where id=" + GridViewProducts.DataKeys[index].Value.ToString();

			BaseConnection.execCommand(query);

			BaseConnection.closeConnection();

			Response.Redirect(Request.RawUrl);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projekt_Inzynierski
{
	public partial class AddProduct : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void ButtonAddProduct_Click(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
				return;

			string command = "select * from Products where Name='" + TextBoxName.Text + "' and Id_user=" + Session["User"];
			if(BaseConnection.execScalar(command) != null)
			{
				BaseConnection.closeConnection();
				string title = "Błąd";
				string body = "Taki produkt już istnieje!";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);

				return;
			}

			command = "insert into Products values (" + Session["User"] + ",'" + TextBoxName.Text +"',"+ DropDownListUnit.SelectedValue+" )";

			if((int)BaseConnection.execCommand(command) == 1)
			{
				LabelError.Text = "Produkt dodany";
				LabelError.CssClass = "text-success";
				LabelError.Visible = true;
			}
			else
			{
				LabelError.Text = "Nie udało się dodać do bazy";
				LabelError.CssClass = "text-danger";
				LabelError.Visible = true;
			}


			BaseConnection.closeConnection();

			Response.Redirect("Product.aspx");
		}
	}
}
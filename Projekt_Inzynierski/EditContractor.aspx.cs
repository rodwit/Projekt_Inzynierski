using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Projekt_Inzynierski
{
	public partial class EditContractor : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string id = Request.QueryString["id"];

			if (String.IsNullOrEmpty(id))
				Response.Redirect("Default.aspx");

			if (IsPostBack)
				return;

			if (!BaseConnection.openConnection())
				return;

			string query = "select Name,Post_town,Post_code, City, Street, NIP, REGON, PESEL from Contractors where Id = " + id;
			SqlDataReader reader = BaseConnection.execReader(query);

			

			while (reader.Read())
			{
				TextBoxName.Text = reader.GetString(0);
				TextBoxPostTown.Text = reader.GetString(1);
				TextBoxPostCode.Text = reader.GetString(2);
				TextBoxCity.Text = reader.GetString(3);
				TextBoxStreet.Text = reader.GetString(4);
				if (!reader.IsDBNull(5))
					TextBoxNIP.Text = reader.GetString(5);
				if (!reader.IsDBNull(6))
					TextBoxREGON.Text = reader.GetString(6);
			}

			BaseConnection.closeConnection();
		}

		protected void ButtonCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("Contractors.aspx");
		}

		protected void ButtonSave_Click(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
				return;
			
			string id = Request.QueryString["id"];

			Dictionary<string, string> pair = new Dictionary<string, string>();
			pair.Add("@userId", id);
			pair.Add("@name", TextBoxName.Text);
			pair.Add("@postTown", TextBoxPostTown.Text);
			pair.Add("@postCode", TextBoxPostCode.Text);
			pair.Add("@city", TextBoxCity.Text);
			pair.Add("@street", TextBoxStreet.Text);
			if (!String.IsNullOrEmpty(TextBoxNIP.Text))
				pair.Add("@nip", TextBoxNIP.Text);
			else
				pair.Add("@nip", "null");
			if (!String.IsNullOrEmpty(TextBoxREGON.Text))
				pair.Add("@regon", TextBoxREGON.Text);
			else
				pair.Add("@regon", "null");
			if (!String.IsNullOrEmpty(TextBoxPESEL.Text))
				pair.Add("@pesel", TextBoxPESEL.Text);
			else
				pair.Add("@pesel", "null");

			BaseConnection.execProcedure("EditContractor", pair);

			BaseConnection.closeConnection();

			Response.Redirect("Contractors.aspx");
		}
	}
}
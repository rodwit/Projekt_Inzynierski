using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Projekt_Inzynierski
{
	public partial class Contractors : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void GridViewContractors_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "DeleteContractor")
				deleteContractor(Convert.ToInt32(e.CommandArgument));
			else if (e.CommandName == "EditContractor")
				editContractor(Convert.ToInt32(e.CommandArgument));
		}

		private void deleteContractor(int index)
		{
			if (!BaseConnection.openConnection())
				return;

			string query = "delete from Contractors where id=" + GridViewContractors.DataKeys[GridViewContractors.SelectedIndex].Value.ToString();

			BaseConnection.execCommand(query);

			BaseConnection.closeConnection();

			Response.Redirect(Request.RawUrl);
		}

		private void editContractor(int index)
		{
			Response.Redirect("EditContractor.aspx?id=" + GridViewContractors.DataKeys[index].Value);
		}
		protected void GridViewContractors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
				return;

			string query = "select Name,Post_town,Post_code, City, Street, NIP, REGON, PESEL from Contractors where Id = " + GridViewContractors.DataKeys[GridViewContractors.SelectedIndex].Value;
			SqlDataReader reader = BaseConnection.execReader(query);
			while(reader.Read())
			{
				LabelName.Text = LabelTitle.Text= reader.GetString(0);
				LabelPostTown.Text = reader.GetString(1);
				LabelPostCode.Text = reader.GetString(2);
				LabelCity.Text = reader.GetString(3);
				LabelStreet.Text = reader.GetString(4);
				if (!reader.IsDBNull(5))
					LabelNIP.Text = reader.GetString(5);
				if (!reader.IsDBNull(6))
					LabelREGON.Text = reader.GetString(6);
			}

			BaseConnection.closeConnection();


			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showInfo();", true);

		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace Projekt_Inzynierski
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session["User"] != null)
				loadCompanyDetail();
		}

		private void loadCompanyDetail()
		{
			if (!BaseConnection.openConnection())
				return;

			List<string> list = new List<string>();

			string query = "SELECT [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [Bank], [Bank_account] FROM [User_Detail] WHERE ([Id_user] =" + Session["User"]+ ")";
			SqlDataReader reader = BaseConnection.execReader(query);
			while (reader.Read())
			{
				LabelName.Text = reader.GetString(0);
				LabelPostTown.Text = reader.GetString(1);
				LabelPostCode.Text = reader.GetString(2);
				LabelCity.Text = reader.GetString(3);
				LabelStreet.Text = reader.GetString(4);
				if (!reader.IsDBNull(5))
					LabelNIP.Text = reader.GetString(5);
				if (!reader.IsDBNull(6))
					LabelREGON.Text = reader.GetString(6);
				LabelBank.Text = reader.GetString(7);
				LabelBankAccount.Text = reader.GetString(8);
				/*for (int i = 0; i < reader.FieldCount; ++i)
				{
					System.Diagnostics.Debug.WriteLine(reader.GetString(i));
				}*/
			}

			BaseConnection.closeConnection();


		}
	}
}
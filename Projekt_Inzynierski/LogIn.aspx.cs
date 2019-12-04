using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Projekt_Inzynierski
{
	public partial class LogIn : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void ButtonLogIn_Click(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
			{
				string title = "Błąd";
				string body = "Błąd serwera. Proszę wrócić później.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);

				return;
			}

			Dictionary<string, string> para = new Dictionary<string, string>();
			para.Add("@e_mail", TextBoxAddres.Text);
			para.Add("@password", TextBoxPassword.Text);

			SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.NVarChar, 50);

			BaseConnection.execProcedure("LogInUser", para, result);

			BaseConnection.closeConnection();

			System.Diagnostics.Debug.WriteLine((string)result.Value);

			if ((string)result.Value == "null")
			{
				System.Diagnostics.Debug.WriteLine("brak w bazie");
				string title = "Błąd logowania";
				string body = "Podano błędny adres e-mail lub hasło.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);
				return;
			}
			

			Session["User"] = (string)result.Value;

			BaseConnection.closeConnection();

			Response.Redirect("/Default.aspx");

		}
	}
}
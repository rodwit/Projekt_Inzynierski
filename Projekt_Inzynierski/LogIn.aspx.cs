using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Projekt_Inzynierski
{
    public partial class LogIn : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e){}

		protected void ButtonLogIn_Click(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
			{
				string title = "Błąd";
				string body = "Błąd serwera. Proszę wrócić później.";
				ClientScript.RegisterStartupScript(
                    GetType(), 
                    "Popup", 
                    "showModalError('" + title + "', '" + body + "');", 
                    true
                );
				return;
			}
            var para = new Dictionary<string, string>
            {
                { 
                    "@e_mail", 
                    TextBoxAddres.Text 
                },
                { 
                    "@password", 
                    TextBoxPassword.Text 
                }
            };
            var result = new SqlParameter("@result", SqlDbType.NVarChar, 50);
			BaseConnection.execProcedure("LogInUser", para, result);
			BaseConnection.closeConnection();
			Debug.WriteLine((string)result.Value);
			if ((string)result.Value == "null")
			{
				System.Diagnostics.Debug.WriteLine("brak w bazie");
				string title = "Błąd logowania";
				string body = "Podano błędny adres e-mail lub hasło.";
				ClientScript.RegisterStartupScript(
                    GetType(), 
                    "Popup", 
                    "showModalError('" + title + "', '" + body + "');", 
                    true
                );
				return;
			}
			Session["User"] = (string)result.Value;
			BaseConnection.closeConnection();
			Response.Redirect("/Default.aspx");
		}
	}
}
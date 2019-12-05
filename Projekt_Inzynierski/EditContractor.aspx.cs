using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using DataLayerLibrary;
using GusLibrary;
using MfLibrary;

namespace Projekt_Inzynierski
{
    public partial class EditContractor : System.Web.UI.Page
	{
        private Context db = new Context();
        protected void Page_Load(object sender, EventArgs e)
		{
			string id = Request.QueryString["id"];
			if (string.IsNullOrEmpty(id))
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
			if (!string.IsNullOrEmpty(TextBoxNIP.Text))
				pair.Add("@nip", TextBoxNIP.Text);
			else
				pair.Add("@nip", "null");
			if (!string.IsNullOrEmpty(TextBoxREGON.Text))
				pair.Add("@regon", TextBoxREGON.Text);
			else
				pair.Add("@regon", "null");
			if (!string.IsNullOrEmpty(TextBoxPESEL.Text))
				pair.Add("@pesel", TextBoxPESEL.Text);
			else
				pair.Add("@pesel", "null");

			BaseConnection.execProcedure("EditContractor", pair);

			BaseConnection.closeConnection();

			Response.Redirect("Contractors.aspx");
		}


		protected void ButtonUpdate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TextBoxNIP.Text))
				return;
			var gus = db.GusDomain.FirstOrDefault(o => o.Nip == TextBoxNIP.Text && o.AddedDate == DateTime.Today);
			if (gus == null)
			{
				gus = GusApiHelper.DataSearchSubjects(TextBoxNIP.Text);
				db.GusDomain.Add(gus);
				db.SaveChanges();
			}
			var mf = db.MfDomain.FirstOrDefault(o => o.Nip == TextBoxNIP.Text && o.AddedDate == DateTime.Today);
			if (mf == null)
			{
				mf = MfApiHelper.SearchNip(TextBoxNIP.Text);
				db.MfDomain.Add(mf);
				db.SaveChanges();
			}
			if ((mf == null) || (gus == null))
			{
				string title = "Błąd";
				string body = "Nie znaleziono kontrahenta.";
				ClientScript.RegisterStartupScript(
					GetType(),
					"Popup", "showModalError('" + title + "', '" + body + "');",
					true
				);
				return;
			}

			TextBoxName.Text = gus.Nazwa;
			TextBoxPESEL.Text = mf.Pesel;
			TextBoxCity.Text = gus.Miejscowosc;
			TextBoxPostCode.Text = gus.KodPocztowy;
			TextBoxNIP.Text = gus.Nip;

			string nr = (string.IsNullOrEmpty(gus.NrNieruchomosci) ? gus.NrLokalu : gus.NrNieruchomosci);
			TextBoxStreet.Text = string.IsNullOrEmpty(gus.Ulica) ? nr : (gus.Ulica + " " + nr);
			TextBoxPostTown.Text = gus.MiejscowoscPoczty;

			TextBoxName.Text = TextBoxName.Text;
			TextBoxStreet.Text = TextBoxStreet.Text;
			TextBoxCity.Text = TextBoxCity.Text;
			TextBoxPostCode.Text = TextBoxPostCode.Text;
			TextBoxPostTown.Text = TextBoxPostTown.Text;
			TextBoxNIP.Text = TextBoxNIP.Text;
			TextBoxPESEL.Text = TextBoxPESEL.Text;
		}
	}
}
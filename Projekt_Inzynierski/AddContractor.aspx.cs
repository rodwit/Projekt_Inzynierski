using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MfLibrary;
using GusLibrary;
using DataLayerLibrary;

namespace Projekt_Inzynierski
{
	public partial class AddContractor : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void ButtonSave_Click(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
				return;

			string command = "select * from (select Name from Contractors inner join Users on Contractors.Id_User = Users.id where Users.id =" 
				+ Session["User"] +") as tabelka where tabelka.Name = '"+TextBoxName.Text+"'; ";

			// check if already added
			if (BaseConnection.execScalar(command) != null)
			{
				BaseConnection.closeConnection();
				string title = "Błąd";
				string body = "Kontrahent o takiej nazwie już isnieje!";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);
				return;
			}

			Dictionary<string, string> pair = new Dictionary<string, string>();
			pair.Add("@userId", Session["User"].ToString());
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

			BaseConnection.execProcedure("AddContractor", pair);

			BaseConnection.closeConnection();

			Response.Redirect("Contractors.aspx");
		}

		protected void ButtonSearch_Click(object sender, EventArgs e)
		{
			//Ministerstwo Finansów
			var mf = MfApiHelper.SearchNip(TextBoxSearchByNIP.Text);
			if(mf == null)
			{
				string title = "Błąd";
				string body = "Nie znaleziono kontrahenta.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);
				return;
			}

			LabelMF_AccountNumbers.Text =  mf.AccountNumbersAsString;
			LabelMF_KRS.Text =  mf.Krs;
			LabelMF_Name.Text =  mf.Name;
			LabelMF_NIP.Text = mf.Nip;
			LabelMF_PESEL.Text =  mf.Pesel;
			LabelMF_RegistrationDenialBasis.Text= mf.RegistrationDenialBasis;
			LabelMF_RegistrationDenialDate.Text= mf.RegistrationDenialDate;
			LabelMF_RegistrationLegalDate.Text = mf.RegistrationLegalDate;
			LabelMF_REGON.Text= mf.Regon;
			LabelMF_RemovalBasis.Text = mf.RemovalBasis;
			LabelMF_RemovalDate.Text = mf.RemovalDate;
			LabelMF_ResidenceAddress.Text =  mf.ResidenceAddress;
			LabelMF_RestorationBasis.Text = mf.RestorationBasis;
			LabelMF_RestorationDate.Text = mf.RestorationDate;
			LabelMF_StatusVAT.Text =  mf.StatusVat;
			LabelMF_WorkingAddress.Text =  mf.WorkingAddress;


			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showInfo();", true);
		}
	}
}
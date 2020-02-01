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
		public Context db = new Context();
        public void Page_Load(object sender, EventArgs e)
		{

		}

		public void ButtonSave_Click(object sender, EventArgs e)
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
			pair.Add("@regon", "null");
			if (!String.IsNullOrEmpty(TextBoxPESEL.Text))
				pair.Add("@pesel", TextBoxPESEL.Text);
			else
				pair.Add("@pesel", "null");

			BaseConnection.execProcedure("AddContractor", pair);

			BaseConnection.closeConnection();

			Response.Redirect("Contractors.aspx");
		}

		public void ButtonSearch_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(TextBoxSearchByNIP.Text))
				return;
            var gus = db.GusDomain.FirstOrDefault(o => o.Nip == TextBoxSearchByNIP.Text && o.AddedDate == DateTime.Today);
            if (gus == null)
            {
                try
                {
                    gus = GusApiHelper.DataSearchSubjects(TextBoxSearchByNIP.Text);
                    if (!String.IsNullOrEmpty(gus.Nazwa))
                    {
                        db.GusDomain.Add(gus);
                        db.SaveChanges();
                    }
                    else
                        gus = null;
                }
                catch(Exception)
                {
                    gus = null;
                }
                
            }
            var mf = db.MfDomain.FirstOrDefault(o => o.Nip == TextBoxSearchByNIP.Text && o.AddedDate == DateTime.Today);
            if (mf == null)
            {
                
                try
                {
                    mf = MfApiHelper.SearchNip(TextBoxSearchByNIP.Text);
                    if(mf != null)
                    {
                        db.MfDomain.Add(mf);
                        db.SaveChanges();
                    }
                    
                }
                catch(Exception)
                {
                    mf = null;
                }
                
            }	
			if( (mf == null) || (gus == null) )
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

			// MF
			LabelMF_AccountNumbers.Text =  mf.AccountNumbersAsString.Replace(";", "<br />");
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

			// GUS

			LabelGUS_NIP.Text = gus.Nip;
			LabelGUS_REGON.Text = gus.Regon;
			LabelGUS_StatusNIP.Text = gus.StatusNip;
			LabelGUS_ApartmentNr.Text = gus.NrLokalu;
			LabelGUS_City.Text = gus.Miejscowosc;
			LabelGUS_Commune.Text = gus.Gmina;
			LabelGUS_District.Text = gus.Powiat;
			LabelGUS_EndDate.Text = gus.DataZakonczeniaDzialalnosci;
			LabelGUS_HouseNr.Text = gus.NrNieruchomosci;
			LabelGUS_Name.Text = gus.Nazwa;
			LabelGUS_PostCity.Text = gus.MiejscowoscPoczty;
			LabelGUS_PostCode.Text = gus.KodPocztowy;
			LabelGUS_Province.Text = gus.Wojewodztwo;
			LabelGUS_Street.Text = gus.Ulica;
			LabelGUS_Type.Text = gus.Typ;


			LabelName.Text = gus.Nazwa;
			LabelPESEL.Text = mf.Pesel;
			LabelCity.Text = gus.Miejscowosc;
			LabelPostCode.Text = gus.KodPocztowy;
			LabelNIP.Text = gus.Nip;

			string nr = (String.IsNullOrEmpty(gus.NrNieruchomosci) ? gus.NrLokalu : gus.NrNieruchomosci);
			LabelStreet.Text = String.IsNullOrEmpty(gus.Ulica) ? nr : (gus.Ulica + " " + nr);
			LabelPostTown.Text = gus.MiejscowoscPoczty;



			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showInfo();", true);
		}

		public void ButtonOK_ServerClick(object sender, EventArgs e)
		{
			TextBoxName.Text = LabelName.Text;
			TextBoxStreet.Text = LabelStreet.Text;
			TextBoxCity.Text = LabelCity.Text;
			TextBoxPostCode.Text = LabelPostCode.Text;
			TextBoxPostTown.Text = LabelPostTown.Text;
			TextBoxNIP.Text = LabelNIP.Text;
			TextBoxPESEL.Text = LabelPESEL.Text;

			ClientScript.RegisterStartupScript(this.GetType(), "Function", "ifSwitchButton();", true);
		}
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


using MfLibrary;
using GusLibrary;

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

			if (String.IsNullOrEmpty(LabelNIP.Text))
				ButtonDetails.Enabled = false;
			else
				ButtonDetails.Enabled = true;

			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showInfo();", true);

		}

		protected void ButtonDetails_Click(object sender, EventArgs e)
		{
			var mf = MfApiHelper.SearchNip(LabelNIP.Text);
			var gus = GusApiHelper.DataSearchSubjects(LabelNIP.Text);
			if ((mf == null) || (gus == null))
			{
				string title = "Błąd";
				string body = "Nie znaleziono kontrahenta.";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModalError('" + title + "', '" + body + "');", true);
				return;
			}

			// MF
			LabelMF_AccountNumbers.Text = mf.AccountNumbersAsString.Replace(";", "<br />");
			LabelMF_KRS.Text = mf.Krs;
			LabelMF_Name.Text = mf.Name;
			LabelMF_NIP.Text = mf.Nip;
			LabelMF_PESEL.Text = mf.Pesel;
			LabelMF_RegistrationDenialBasis.Text = mf.RegistrationDenialBasis;
			LabelMF_RegistrationDenialDate.Text = mf.RegistrationDenialDate;
			LabelMF_RegistrationLegalDate.Text = mf.RegistrationLegalDate;
			LabelMF_REGON.Text = mf.Regon;
			LabelMF_RemovalBasis.Text = mf.RemovalBasis;
			LabelMF_RemovalDate.Text = mf.RemovalDate;
			LabelMF_ResidenceAddress.Text = mf.ResidenceAddress;
			LabelMF_RestorationBasis.Text = mf.RestorationBasis;
			LabelMF_RestorationDate.Text = mf.RestorationDate;
			LabelMF_StatusVAT.Text = mf.StatusVat;
			LabelMF_WorkingAddress.Text = mf.WorkingAddress;

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


			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showDetails();", true);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

namespace Projekt_Inzynierski
{
	public partial class Invoices : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected string SetInvoiceNumber(object issueDate, object number)
		{
			DateTime dateTime = DateTime.ParseExact(issueDate.ToString(), "yyyy-MM-dd HH:mm:ss", null);
			//DateTime dateTime = DateTime.ParseExact(issueDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", null);
			int num = (int)number;

			string result = num.ToString("00") + "/" + dateTime.Month.ToString("00") + "/" + dateTime.Year.ToString();

			//string result = issueDate.ToString();

			return result;
		}

		protected decimal SetProductBrutto(object  nettoValue, object vatValue)
		{
			decimal netto = (decimal)nettoValue;
			decimal vat = (decimal)vatValue;
			vat += 1;

			return Math.Round(netto * vat,2);

		}

		protected void GridViewInvoices_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!BaseConnection.openConnection())
				return;

			string query = "select main.Name, reciever.Name, Issue_date, Due_date, Payment_Method.Name from Invoices " +
				"inner join Contractors as main on main.Id = Id_contractor left join Contractors as reciever on reciever.Id = Id_receiver " +
				"inner join Payment_Method on Id_payment = Payment_Method.Id where Invoices.Id =" +GridViewInvoices.DataKeys[GridViewInvoices.SelectedIndex].Value.ToString();
			SqlDataReader reader = BaseConnection.execReader(query);
			while (reader.Read())
			{
				LabelContractor.Text = reader.GetString(0);
				if (!reader.IsDBNull(1))
					LabelReciever.Text = reader.GetString(1);
				LabelIssueDate.Text = reader.GetDateTime(2).ToShortDateString();
				LabelDueDate.Text = reader.GetDateTime(3).ToShortDateString();
				LabelPayment.Text = reader.GetString(4);
			}

			BaseConnection.closeConnection();

			LabelTitle.Text = (GridViewInvoices.Rows[GridViewInvoices.SelectedIndex].Cells[2].Controls[1] as Label).Text;

			SqlDataSourceProducts.SelectCommand = "select Products.Name,Units.Name, Tax_rates.Name,Tax_rates.Value, Amount, Price from Invoice_Products " +
				"inner join Tax_rates on Id_tax = Tax_rates.Id inner join Products on Id_product = Products.Id " +
				"inner join Units on Products.Id_unit = Units.Id where Id_invoice=" + GridViewInvoices.DataKeys[GridViewInvoices.SelectedIndex].Value.ToString();
			ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showInfo();", true);
		}

		private void deleteInvoice(int index)
		{
			string idInvoice =   GridViewInvoices.DataKeys[index].Value.ToString();

			if (!BaseConnection.openConnection())
				return;

			string q1 = "delete from Invoice_Products where Id_invoice=" + idInvoice;
			string q2 = "delete from Invoices where id=" + idInvoice;
			string query = q1 + "; " + q2;

			BaseConnection.execCommand(query);


			BaseConnection.closeConnection();

			Response.Redirect(Request.RawUrl);
		}

		private void printInvoice(int index)
		{
			string idInvoice = GridViewInvoices.DataKeys[index].Value.ToString();
			string nr = (GridViewInvoices.Rows[index].Cells[2].Controls[1] as Label).Text;


			GenerateInvoice generateInvoice = new GenerateInvoice(Convert.ToInt32(Session["User"]), Convert.ToInt32( idInvoice),nr);
			var pdfFile = generateInvoice.generateFile();

			Response.ContentType = "application/html";
			Response.AppendHeader("content-disposition",
					"attachment; filename=Faktura " + nr+".pdf");
			Response.TransmitFile(pdfFile);
			Response.End();

			File.Delete(pdfFile);

		}

		protected void GridViewInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "DeleteInvoice")
				deleteInvoice(Convert.ToInt32(e.CommandArgument));
			else if (e.CommandName == "PrintInvoice")
				printInvoice(Convert.ToInt32(e.CommandArgument));
		}
	}
}
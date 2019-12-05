using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.IO;
using IronPdf;

namespace Projekt_Inzynierski
{
	public class GenerateInvoice
	{
		private int _idInvoice;
		private int _idContractor;
		private int _idUser;
		private string _nr;
		private string _payment;
		private HtmlTextWriter _writer;
		private StringWriter _stringWriter;
        
		private struct ProductRow
		{
			public string name;
			public string unit;
			public int amount;
			public string taxRate;
			public decimal taxRateValue;
			public decimal price;
		}
		public GenerateInvoice(int idUser, int invoiceId, string nr)
		{
			_stringWriter = new StringWriter();
			_writer = new HtmlTextWriter(_stringWriter);

		
			_idInvoice = invoiceId;
			_nr = nr;
			_idUser = idUser;
			if (!BaseConnection.openConnection())
				return;

			string query = "select Id_contractor from Invoices where Id =" + invoiceId;

			_idContractor = (int)BaseConnection.execScalar(query);

			BaseConnection.closeConnection();

		}
		private void header()
		{
			_writer.AddAttribute(HtmlTextWriterAttribute.Href, "https://stackpath.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.css");
			_writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
			_writer.RenderBeginTag(HtmlTextWriterTag.Link);
			_writer.RenderEndTag();
		}

		public void head()
		{
			string issueDate = "", dueDate = "", type = "Oryginał";

			if (!BaseConnection.openConnection())
				return;

			string query = "select Issue_date, Due_date , Payment_Method.Name from Invoices " +
				"inner join Payment_Method on Invoices.Id_payment = Payment_Method.Id where Invoices.Id=" + _idInvoice;

			type = "Oryginał";
			SqlDataReader reader = BaseConnection.execReader(query);
			while (reader.Read())
			{
				issueDate = reader.GetDateTime(0).ToShortDateString();
				dueDate = reader.GetDateTime(1).ToShortDateString();
				_payment = reader.GetString(2);
			}


			BaseConnection.closeConnection();



			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "row");
			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "h-25");
			_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div row h-25
			{
				_writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-6");
				_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div col-6
				{
					_writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 50%; height: 90%");
					//_writer.AddAttribute(HtmlTextWriterAttribute.Class, "p-2");
					//_writer.AddAttribute(HtmlTextWriterAttribute.Src, "file:\\" + HttpContext.Current.Server.MapPath( "Images/Logo.jpg"));
					_writer.AddAttribute(HtmlTextWriterAttribute.Src, IronPdf.Util.ImageToDataUri(System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("Images/Logo.jpg"))));
					_writer.RenderBeginTag(HtmlTextWriterTag.Img); // img logo
					_writer.RenderEndTag();
				}
				_writer.RenderEndTag(); // close  div col-6

				_writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-6");
				_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div col-6
				{
					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "table");
					_writer.RenderBeginTag(HtmlTextWriterTag.Table); // open table
					{
						_writer.RenderBeginTag(HtmlTextWriterTag.Tbody); // open tbody
						{
							_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
							{
								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   //Invoice
								_writer.Write("Faktura");
								_writer.RenderEndTag();

								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Invoice nr
								_writer.Write(_nr);
								_writer.RenderEndTag();
							}
							_writer.RenderEndTag(); // close tr
							_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
							{
								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   //Issue date
								_writer.Write("Data wystawienia:");
								_writer.RenderEndTag();

								_writer.RenderBeginTag(HtmlTextWriterTag.Td);
								_writer.Write(issueDate);
								_writer.RenderEndTag();
							}
							_writer.RenderEndTag(); // close tr
							_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
							{
								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   //Due date
								_writer.Write("Termin zapłaty:");
								_writer.RenderEndTag();

								_writer.RenderBeginTag(HtmlTextWriterTag.Td);
								_writer.Write(dueDate);
								_writer.RenderEndTag();
							}
							_writer.RenderEndTag(); // close tr
							_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
							{
								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   //Payment
								_writer.Write("Metoda płatności:");
								_writer.RenderEndTag();

								_writer.RenderBeginTag(HtmlTextWriterTag.Td);
								_writer.Write(_payment);
								_writer.RenderEndTag();
							}
							_writer.RenderEndTag(); // close tr
							_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
							{
								_writer.RenderBeginTag(HtmlTextWriterTag.Td);   //Payment
								_writer.Write("Typ dokumentu:");
								_writer.RenderEndTag();

								_writer.RenderBeginTag(HtmlTextWriterTag.Td);
								_writer.Write(type);
								_writer.RenderEndTag();
							}
							_writer.RenderEndTag(); // close tr
						}
						_writer.RenderEndTag(); //close tbody
					}
					_writer.RenderEndTag(); //close table
				}
				_writer.RenderEndTag(); // close  div col-6
			}
			_writer.RenderEndTag(); // close  div row h-25

			_writer.AddAttribute(HtmlTextWriterAttribute.Style, "color: black; height: 2px; background-color:black;"); //hr
			_writer.RenderBeginTag(HtmlTextWriterTag.Hr);
			_writer.RenderEndTag();
			_writer.RenderBeginTag(HtmlTextWriterTag.Br); //br
			_writer.RenderEndTag();
		}

		private void persons()
		{
			if (!BaseConnection.openConnection())
				return;

			string query = "select * from User_Detail where Id_user =" + _idUser;

			int id;
			string name = "", postTown = "", postCode = "", city = "", street = "", nip = "", regon = "", bank = "", bankAccount = "";
			SqlDataReader reader = BaseConnection.execReader(query);
			while (reader.Read())
			{
				id = reader.GetInt32(0);
				name = reader.GetString(1);
				postTown = reader.GetString(2);
				postCode = reader.GetString(3);
				city = reader.GetString(4);
				street = reader.GetString(5);
				if(!reader.IsDBNull(6))
					nip = reader.GetString(6);
				if (!reader.IsDBNull(7))
					regon = reader.GetString(7);
				if (!reader.IsDBNull(8))
					bank = reader.GetString(8);
				if (!reader.IsDBNull(9))
					bankAccount = reader.GetString(9);
			}
			reader.Close();
			BaseConnection.closeConnection();




			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "row h-auto align-items-center");
			_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div row h-auto align-items-center
			{
				_writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-6");
				_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div col-6 seller
				{
					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // title
					_writer.Write("Sprzedawca:");
					_writer.RenderEndTag();

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // name
					_writer.Write(name);
					_writer.RenderEndTag();

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // address
					_writer.Write(city + " " + street);
					_writer.RenderEndTag();

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // post
					_writer.Write(postCode + " " + postTown);
					_writer.RenderEndTag();

					if(nip != "")
					{
						_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
						_writer.RenderBeginTag(HtmlTextWriterTag.Div); // nip
						_writer.Write("NIP: "+nip);
						_writer.RenderEndTag();
					}

					if ( (_payment=="Przelew") && (bank != "") && (bankAccount!=""))
					{
						_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
						_writer.RenderBeginTag(HtmlTextWriterTag.Div); // bank
						_writer.Write(bank + " " + bankAccount);
						_writer.RenderEndTag();
					}
				}
				_writer.RenderEndTag(); // close div col-6 seller

				if (!BaseConnection.openConnection())
					return;

				query = "select Contractors.* from Contractors inner join Invoices on Contractors.Id=Invoices.Id_contractor where Invoices.Id =" + _idInvoice;
				string pesel = "";
				reader = BaseConnection.execReader(query);
				while (reader.Read())
				{
					id = reader.GetInt32(0);
					id = reader.GetInt32(1);
					name = reader.GetString(2);
					postTown = reader.GetString(3);
					postCode = reader.GetString(4);
					city = reader.GetString(5);
					street = reader.GetString(6);
					if (!reader.IsDBNull(7))
						nip = reader.GetString(7);
					if (!reader.IsDBNull(8))
						regon = reader.GetString(8);
					if (!reader.IsDBNull(9))
						pesel = reader.GetString(9);
				}

				BaseConnection.closeConnection();


				_writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-6");
				_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open div col-6 buyer
				{
					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // title
					_writer.Write("Kupujący:");
					_writer.RenderEndTag();

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // name
					_writer.Write(name);
                    _writer.RenderEndTag();                    

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // address
					_writer.Write(city + " " + street);
					_writer.RenderEndTag();

					_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
					_writer.RenderBeginTag(HtmlTextWriterTag.Div); // post
					_writer.Write(postCode + " " + postTown);
					_writer.RenderEndTag();

					if (nip != "")
					{
						_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
						_writer.RenderBeginTag(HtmlTextWriterTag.Div); // nip
						_writer.Write("NIP: " + nip);
						_writer.RenderEndTag();
					}

					if (pesel != "")
					{
						_writer.AddAttribute(HtmlTextWriterAttribute.Class, "mt-2");
						_writer.RenderBeginTag(HtmlTextWriterTag.Div); // pesel
						_writer.Write("PESEL: " + pesel);
						_writer.RenderEndTag();
					}
				}
				_writer.RenderEndTag(); // close div col-6 buyer
			}
			_writer.RenderEndTag(); // close div row h-auto align-items-center

			_writer.AddAttribute(HtmlTextWriterAttribute.Style, "color: black; height: 2px; background-color:black;"); //hr
			_writer.RenderBeginTag(HtmlTextWriterTag.Hr);
			_writer.RenderEndTag();
			_writer.RenderBeginTag(HtmlTextWriterTag.Br); //br
			_writer.RenderEndTag();

		}

		private void products()
		{
			if (!BaseConnection.openConnection())
				return;


			string query = "select Products.Name, Units.Name, Invoice_Products.Amount, Tax_rates.Name, Tax_rates.Value, Invoice_Products.Price " +
				"from Invoice_Products inner join Invoices on Invoice_Products.Id_invoice = Invoices.Id " +
				"inner join Products on Invoice_Products.Id_product = Products.Id " +
				"inner join Units on Units.Id = Products.Id_unit inner join Tax_rates on Tax_rates.Id = Invoice_Products.Id_tax " +
				"where Invoices.Id = " + _idInvoice;


			decimal sum = 0;
			List<ProductRow> productsList = new List<ProductRow>();
			SqlDataReader reader = BaseConnection.execReader(query);
			while (reader.Read())
			{
				ProductRow productRow = new ProductRow();
				productRow.name = reader.GetString(0);
				productRow.unit = reader.GetString(1);
				productRow.amount = reader.GetInt32(2);
				productRow.taxRate = reader.GetString(3);
				productRow.taxRateValue = reader.GetDecimal(4);
				productRow.price = reader.GetDecimal(5);
				productsList.Add(productRow);
			}


			BaseConnection.closeConnection();


			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "table");
			_writer.RenderBeginTag(HtmlTextWriterTag.Table); // open table
			{
				_writer.AddAttribute(HtmlTextWriterAttribute.Class, "thead-dark");
				_writer.RenderBeginTag(HtmlTextWriterTag.Thead); // open thead
				{
					_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
					{
						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Product
						_writer.Write("Produkt");
						_writer.RenderEndTag();

						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Unit
						_writer.Write("Jedn.");
						_writer.RenderEndTag();

						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Amount
						_writer.Write("Ilość");
						_writer.RenderEndTag();

						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Vat
						_writer.Write("Stawka");
						_writer.RenderEndTag();

						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Price netto
						_writer.Write("Cena netto");
						_writer.RenderEndTag();

						_writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
						_writer.RenderBeginTag(HtmlTextWriterTag.Th);   //Price brutto
						_writer.Write("Cena brutto");
						_writer.RenderEndTag();
					}
					_writer.RenderEndTag(); // close tr

				}
				_writer.RenderEndTag(); //close thead

				_writer.RenderBeginTag(HtmlTextWriterTag.Tbody); // open tbody
				{

					foreach (var ele in productsList)
					{
						_writer.RenderBeginTag(HtmlTextWriterTag.Tr); //open tr
						{
							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Product
							_writer.Write(ele.name);
							_writer.RenderEndTag();

							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Unit
							_writer.Write(ele.unit);
							_writer.RenderEndTag();

							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Amount
							_writer.Write(ele.amount);
							_writer.RenderEndTag();

							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // VAT
							_writer.Write(ele.taxRate);
							_writer.RenderEndTag();

							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Price netto
							_writer.Write(ele.price.ToString("0.##"));
							_writer.RenderEndTag();

							decimal brutto = Math.Round(ele.price * (ele.taxRateValue + 1), 2);
							sum += (brutto*ele.amount);
							_writer.RenderBeginTag(HtmlTextWriterTag.Td);   // Price brutto
							_writer.Write(brutto);
							_writer.RenderEndTag();
						}
						_writer.RenderEndTag(); // close tr
					}


				}
				_writer.RenderEndTag(); //close tbody
			}
			_writer.RenderEndTag(); //close table

			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "text-right w-100 font-weight-bold bg-light");
			_writer.RenderBeginTag(HtmlTextWriterTag.H3); //open h3
			_writer.Write("Razem do zapłaty (brutto): " + sum + " zł");
			_writer.RenderEndTag(); //close h3

		}

		public string generateFile()
		{

			_writer.AddAttribute(HtmlTextWriterAttribute.Class, "container");
			_writer.RenderBeginTag(HtmlTextWriterTag.Div); // open container

			header();
			head();
			persons();
			products();

			_writer.RenderEndTag(); // close container


			//string fileName = HttpContext.Current.Server.MapPath("~/newFile.html");
			string fileName = Guid.NewGuid().ToString() + "_" + _idUser + "_" + _nr.Replace('/', '-');
			string htmlFile = HttpContext.Current.Server.MapPath("~/"+fileName + ".html");
			File.WriteAllText(htmlFile, _stringWriter.ToString());

			HtmlToPdf render = new IronPdf.HtmlToPdf();
			render.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
			PdfDocument invoicePDF = render.RenderHTMLFileAsPdf(htmlFile);

			string pdfFile = HttpContext.Current.Server.MapPath("~/" + fileName + ".pdf");

			invoicePDF.SaveAs(pdfFile);

			File.Delete(htmlFile);

			return pdfFile;
		}
	}


}
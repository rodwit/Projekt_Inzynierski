using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;

namespace Projekt_Inzynierski
{
	public partial class AddInvoice : System.Web.UI.Page
	{
		[Serializable]
		private class RowData
		{
			private int _idProduct;
			private string _ilosc;
			private int _idVAT;
			private string _priceBrutto;
			private string _priceNetto;

			public int IdProduct { get => _idProduct; set => _idProduct = value; }
			public string Ilosc { get => _ilosc; set => _ilosc = value; }
			public int IdVAT { get => _idVAT; set => _idVAT = value; }
			public string PriceBrutto { get => _priceBrutto; set => _priceBrutto = value; }
			public string PriceNetto { get => _priceNetto; set => _priceNetto = value; }
		}

		List<RowData> _productList = new List<RowData>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				if (ViewState["ProductList"] != null)
				{
					restoreTable();
					/*TableRow row = createRow();
					TableProducts.Rows.Add(row);*/
				}
			}

			/*if (TableProducts.Rows.Count <= 1)
			{
				TableRow row = createRow();
				TableProducts.Rows.Add(row);
			}*/

			TextBoxIssueDate.Attributes.Add("readonly", "readonly");
			TextBoxDueDate.Attributes.Add("readonly", "readonly");

			if (TextBoxIssueDate.Text == String.Empty)
				TextBoxIssueDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

			if (TextBoxDueDate.Text == String.Empty)
				TextBoxDueDate.Text = DateTime.Now.ToString("dd-MM-yyyy");


		}

		protected void ButtonAddRow_Click(object sender, EventArgs e)
		{
			saveRow();
			TableRow row = createRow();
			RowData rowData = new RowData();
			_productList.Add(rowData);
			TableProducts.Rows.Add(row);
			

			ViewState["ProductList"] = _productList;
		}

		protected void deleteProduct(object sender, EventArgs e)
		{
			Button button = (sender as Button);
			int index = Convert.ToInt32(button.ID.Substring(button.ID.LastIndexOf('_') + 1));
			//TableProducts.Rows.RemoveAt(index+1);
			_productList.RemoveAt(index);

			int rows = TableProducts.Rows.Count;
			for (int i = 1; i < rows; ++i)
				TableProducts.Rows.RemoveAt(1);


			ViewState["ProductList"] = _productList;

			restoreTable();
		}

		private TableRow createRow()
		{
			TableRow row = new TableRow();
			for (int i = 0; i < 6; ++i)
				row.Cells.Add(new TableCell());

			int index = TableProducts.Rows.Count-1;
			//PRODUCT
			DropDownList dropDownList = new DropDownList();
			dropDownList.ID = "Product" + index;
			dropDownList.DataSourceID = "SqlDataSourceProducts";
			dropDownList.DataTextField = "Name";
			dropDownList.DataValueField = "Id";
			dropDownList.SelectedIndex = 0;
			row.Cells[0].Controls.Add(dropDownList);

			//AMOUNT
			TextBox textBox = new TextBox();
			textBox.ID = "Amount" + index;
			textBox.TextMode = TextBoxMode.Number;
			textBox.Attributes.Add("min", "1");
			textBox.Attributes.Add("max", "99999");
			textBox.Attributes.Add("step", "1");
			textBox.Attributes.Add("required", "required");
			row.Cells[1].Controls.Add(textBox);

			//TAX_RATE
			dropDownList = new DropDownList();
			dropDownList.ID = "TaxRate" + index;
			dropDownList.DataSourceID = "SqlDataSourceTaxRate";
			dropDownList.DataTextField = "Name";
			dropDownList.DataValueField = "Value";
			dropDownList.CssClass = "tax";
			dropDownList.SelectedIndex = 0;
			row.Cells[2].Controls.Add(dropDownList);

			//NETTO
			textBox = new TextBox();
			textBox.ID = "Netto" + index;
			textBox.TextMode = TextBoxMode.Number;
			textBox.CssClass = "netto";
			textBox.Attributes.Add("step", "0.01");
			textBox.Attributes.Add("lang","en-US");
			textBox.Attributes.Add("onchange", "this.value = this.value.replace(/,/g, '.')");
			textBox.Attributes.Add("required", "required");
			row.Cells[3].Controls.Add(textBox);

			//BRUTTO
			textBox = new TextBox();
			textBox.ID = "Brutto" + index;
			textBox.TextMode = TextBoxMode.Number;
			textBox.CssClass = "brutto";
			textBox.Attributes.Add("step", "0.01");
			textBox.Attributes.Add("lang", "en-US");
			textBox.Attributes.Add("onchange", "this.value = this.value.replace(/,/g, '.')");
			textBox.Attributes.Add("required", "required");
			row.Cells[4].Controls.Add(textBox);

			//DELETE BUTTON
			Button button = new Button();
			button.ID = "DeleteProduct_" + index;
			button.Text = "Usuń";
			button.Click += new EventHandler(deleteProduct);
			button.UseSubmitBehavior = false;
			button.Attributes.Add("runat", "server");
			button.CssClass = "btn btn-danger";
			row.Cells[5].Controls.Add(button);


			return row;
		}

		private void restoreTable()
		{
			_productList = ViewState["ProductList"] as List<RowData>;
			foreach (RowData ele in _productList)
			{
				TableRow row = createRow();
				(row.Cells[0].Controls[0] as DropDownList).SelectedIndex = ele.IdProduct;
				(row.Cells[1].Controls[0] as TextBox).Text = ele.Ilosc;
				(row.Cells[2].Controls[0] as DropDownList).SelectedIndex = ele.IdVAT;
				(row.Cells[3].Controls[0] as TextBox).Text = ele.PriceNetto;
				(row.Cells[4].Controls[0] as TextBox).Text = ele.PriceBrutto;
				TableProducts.Rows.Add(row);
			}
		}

		private void saveRow()
		{
			if (TableProducts.Rows.Count <= 1)
				return;
			if (!String.IsNullOrEmpty(_productList[_productList.Count - 1].PriceBrutto))
				return;

			TableRow row = TableProducts.Rows[TableProducts.Rows.Count-1];
			RowData rowData = new RowData();
			rowData.IdProduct = (row.Cells[0].Controls[0] as DropDownList).SelectedIndex;

			if ((row.Cells[1].Controls[0] as TextBox).Text != String.Empty)
				rowData.Ilosc = (row.Cells[1].Controls[0] as TextBox).Text;

			rowData.IdVAT = (row.Cells[2].Controls[0] as DropDownList).SelectedIndex;

			if ((row.Cells[3].Controls[0] as TextBox).Text != String.Empty)
				rowData.PriceNetto = (row.Cells[3].Controls[0] as TextBox).Text;

			if ((row.Cells[4].Controls[0] as TextBox).Text != String.Empty)
				rowData.PriceBrutto = (row.Cells[4].Controls[0] as TextBox).Text;
			_productList[_productList.Count - 1] = rowData;

		}

		private void saveRowSaveInvoice()
		{
			if (TableProducts.Rows.Count <= 1)
				return;
			if (!String.IsNullOrEmpty(_productList[_productList.Count - 1].PriceBrutto))
				return;


			_productList.Clear();

			for (int i = 0; i < TableProducts.Rows.Count-1; ++i)
			{
				TableRow row = TableProducts.Rows[i+1];
				RowData rowData = new RowData();
				rowData.IdProduct = Convert.ToInt32((row.Cells[0].Controls[0] as DropDownList).SelectedValue);

				if ((row.Cells[1].Controls[0] as TextBox).Text != String.Empty)
					rowData.Ilosc = (row.Cells[1].Controls[0] as TextBox).Text;

				rowData.IdVAT = (row.Cells[2].Controls[0] as DropDownList).SelectedIndex + 1;

				if ((row.Cells[3].Controls[0] as TextBox).Text != String.Empty)
					rowData.PriceNetto = (row.Cells[3].Controls[0] as TextBox).Text;

				if ((row.Cells[4].Controls[0] as TextBox).Text != String.Empty)
					rowData.PriceBrutto = (row.Cells[4].Controls[0] as TextBox).Text;

				_productList.Add(rowData);
			}
			

		}

		protected void ButtonOtherReceiver_Click(object sender, EventArgs e)
		{
			collapseOther.Visible = !collapseOther.Visible;
			TextBoxOther.Text = "";
		}

		protected void ButtonSave_Click(object sender, EventArgs e)
		{
			saveRowSaveInvoice(); // get products
			DateTime issueDate = DateTime.ParseExact(TextBoxIssueDate.Text,"dd-MM-yyyy",null);
			DateTime dueDate = DateTime.ParseExact(TextBoxDueDate.Text, "dd-MM-yyyy",null);
			string payment = DropDownListPaymentMethod.SelectedValue;
			string contractor = DropDownListContractor.SelectedValue;


			if (!BaseConnection.openConnection())
				return;

			Dictionary<string, string> data = new Dictionary<string, string>();
			data.Add("@userId", Session["User"].ToString());
			data.Add("@contractorId", contractor);
			data.Add("@recieverId", "-1");
			data.Add("@issueDate", issueDate.ToShortDateString());
			data.Add("@dueDate", dueDate.ToShortDateString());
			data.Add("@paymentId", payment);

			SqlParameter idInvoice = new SqlParameter("@result", System.Data.SqlDbType.Int);

			BaseConnection.execProcedure("AddInvoice", data, idInvoice);
			
			foreach(var ele in _productList)
			{
				string query = "insert into Invoice_Products values (" + (int)idInvoice.Value+","+ele.IdProduct+","+ele.IdVAT+","+ele.Ilosc+","+ele.PriceNetto+")";
				System.Diagnostics.Debug.WriteLine(query);
				BaseConnection.execCommand(query);				
			}

			BaseConnection.closeConnection();

			Response.Redirect("/Invoices.aspx");

		}
	}
}
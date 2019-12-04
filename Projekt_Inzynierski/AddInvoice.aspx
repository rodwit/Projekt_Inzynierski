<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="AddInvoice.aspx.cs" Inherits="Projekt_Inzynierski.AddInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="Scripts/DatePicker/datepicker.css" rel="stylesheet" />
	<script src="Scripts/DatePicker/bootstrap-datepicker.js"></script>
	<script src="Scripts/DatePicker/bootstrap-datepicker.pl.js"></script>
    
    <link href="Content/bootstrap-select.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-select.min.js"></script>
    <script src="Scripts/i18n/defaults-pl_PL.min.js"></script>

	<script src="Scripts/Round/Round.js"></script>
	<script src="Scripts/VAT/vat.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	

	<div class="col h-100">
		<div class="d-flex flex-row">
			<div class="p-2">
				<label>Kontrahent </label>
			</div>
			<div class="p-2">
                <asp:DropDownList ID="DropDownListContractor" CssClass="selectpicker" data-live-search="true" runat="server" DataSourceID="SqlDataSourceContractors" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
			    <asp:SqlDataSource ID="SqlDataSourceContractors" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Id], [Name] FROM [Contractors] WHERE ([Id_User] = @Id_User)">
                    <SelectParameters>
                        <asp:SessionParameter Name="Id_User" SessionField="User" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
			</div>
			<div class="p-2">
				<asp:Button ID="ButtonAddContractor" CssClass="btn btn-primary" runat="server" Text="Dodaj" Visible="false"/></div>
			<div class="p-2">
				<asp:Button ID="ButtonOtherReceiver" CssClass="btn btn-primary" runat="server" Text="Inny odbiorca" OnClick="ButtonOtherReceiver_Click" Visible="false"/></div>
		</div>
		<div class="d-flex flex-row" runat="server" id="collapseOther" visible="false">
			<div class="p-2">
				<label>Odbiorca </label>
			</div>
			<div class="p-2">
				<asp:TextBox ID="TextBoxOther" runat="server"></asp:TextBox></div>
			<div class="p-2">
				<asp:Button ID="ButtonAddReceiver" CssClass="btn btn-primary" runat="server" Text="Dodaj" /></div>
		</div>
		<div class="d-flex flex-row align-items-center">
			<div class="p-2">
				<label>Metoda płatności </label>
			</div>
			<asp:DropDownList ID="DropDownListPaymentMethod" CssClass="selectpicker" runat="server" DataSourceID="SQL_Project" DataTextField="Name" DataValueField="Id">
			</asp:DropDownList>
			<asp:SqlDataSource ID="SQL_Project" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Id], [Name] FROM [Payment_Method]"></asp:SqlDataSource>
		</div>
		<div class="d-flex flex-row align-items-center">
			<div class="p-2">
				<label>Data wystawienia </label>
			</div>
			<asp:TextBox ID="TextBoxIssueDate" runat="server"></asp:TextBox>
		</div>
		<div class="d-flex flex-row align-items-center">
			<div class="p-2">
				<label>Data płatności</label></div>
			<asp:TextBox ID="TextBoxDueDate" runat="server"></asp:TextBox>
		</div>
		<div class="d-flex flex-row align-items-center">
			<div class="p-2">
				<label>Zapłacono </label>
			</div>
			<asp:CheckBox ID="CheckBoxPaid" CssClass="custom-checkbox" runat="server" />
		</div>

		<h2>Produkty</h2>
		<hr />

		<div class="d-flex flex-row align-items-center">
			<asp:Table ID="TableProducts" runat="server" CssClass="table table-striped text-center">
				<asp:TableHeaderRow runat="server">
					<asp:TableHeaderCell>Produkt</asp:TableHeaderCell>
					<asp:TableHeaderCell>Ilość</asp:TableHeaderCell>
					<asp:TableHeaderCell>Stawka VAT</asp:TableHeaderCell>
					<asp:TableHeaderCell>Cena netto</asp:TableHeaderCell>
					<asp:TableHeaderCell>Cena brutto</asp:TableHeaderCell>
					<asp:TableHeaderCell></asp:TableHeaderCell>
				</asp:TableHeaderRow>
			</asp:Table>
			<asp:SqlDataSource ID="SqlDataSourceTaxRate" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Name], [Value] FROM [Tax_rates]"></asp:SqlDataSource>
			<asp:SqlDataSource ID="SqlDataSourceProducts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Name], [Id] FROM [Products] WHERE ([Id_user] = @Id_user)">
				<SelectParameters>
					<asp:SessionParameter Name="Id_user" SessionField="User" Type="Int32" />
				</SelectParameters>
			</asp:SqlDataSource>
		</div>

		<div class="d-flex flex-row align-items-center">
			<asp:Button ID="ButtonAddRow" runat="server" Text="Dodaj wiersz" CssClass="btn btn-primary" OnClick="ButtonAddRow_Click" />
		</div>
        <hr />
        <div class="d-flex flex-row">
			<div class="p-2"><asp:Button ID="ButtonSave" runat="server" Text="Zapisz" CssClass="btn btn-primary" OnClick="ButtonSave_Click" /></div>
			<div class="p-2"><a id="ButtonCancel" class="btn btn-danger" href="Invoices.aspx">Anuluj</a></div>
		</div>


	</div>



    <script type="text/javascript">
        $(function () {
            $('[id*=TextBoxIssueDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                autoclose: true,
                todayBtn: 'linked',
                format: "dd-mm-yyyy",
                language: "pl"
            });
        });

        $(function () {
            $('[id*=TextBoxDueDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                autoclose: true,
                todayBtn: 'linked',
                format: "dd-mm-yyyy",
                language: "pl"
            });
        });

        $(function () {
            $('.netto').change(VAT);
            $('.brutto').change(VAT);

            $('.tax').change(VAT);
        });
	</script>
</asp:Content>

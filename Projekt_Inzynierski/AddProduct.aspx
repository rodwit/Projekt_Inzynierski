<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Projekt_Inzynierski.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap-select.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-select.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col h-100">
		<div class="d-flex flex-row">
			<div class="p-2">
				<label>Nazwa produktu</label>
			</div>
			<div class=" p-2 form-group">
				<asp:TextBox runat="server" CssClass="form-control" ID="TextBoxName" required="required"></asp:TextBox>
			</div>
			<div class=" p-2 form-group">
				<asp:DropDownList ID="DropDownListUnit" runat="server" CssClass="custom-checkbox selectpicker" DataSourceID="SqlDataSourceUnits" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSourceUnits" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Id], [Name] FROM [Units]"></asp:SqlDataSource>
			</div>
		</div>
		<div class="d-flex flex-row align-items-center">
			<div class="p-2">
				<asp:Button ID="ButtonAddProduct" runat="server" CssClass="btn btn-primary" Text="Dodaj" OnClick="ButtonAddProduct_Click" />
			</div>
			<div class="p-2">
				<asp:Label ID="LabelError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
			</div>
		</div>
	</div>

</asp:Content>

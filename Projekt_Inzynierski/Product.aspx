<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Projekt_Inzynierski.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col h-100">
        <div class="row p-3">
			 <a class="btn btn-primary" href="AddProduct.aspx">Dodaj produkt</a>
		</div>
        <br />
		<asp:GridView ID="GridViewProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" DataSourceID="SqlDataSourceProjects" OnRowCommand="GridViewProducts_RowCommand" DataKeyNames="id">
			<Columns>
				<asp:BoundField DataField="id" HeaderText="id" SortExpression="id" InsertVisible="False" ReadOnly="True" Visible="False" />
				<asp:BoundField DataField="Name" HeaderText="Nazwa" SortExpression="Name" />
				<asp:BoundField DataField="Name1" HeaderText="Jednostka" SortExpression="Name1" />
				<asp:ButtonField ButtonType="Button" CommandName="DeleteProduct" Text="Usuń">
				<ControlStyle CssClass="btn btn-danger" />
				</asp:ButtonField>
			</Columns>
			<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
			<RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="select Products.id, Products.Name, Units.Name from Products inner join Units on Products.Id_unit = Units.Id where Id_user = @User;">
			<SelectParameters>
				<asp:SessionParameter Name="User" SessionField="User" />
			</SelectParameters>
		</asp:SqlDataSource>
	</div>
</asp:Content>

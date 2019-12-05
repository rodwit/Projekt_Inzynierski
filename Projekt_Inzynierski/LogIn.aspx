<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Projekt_Inzynierski.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col h-100">
        <div class="form-group">
            <label>Adres E-mail:</label>
            <asp:TextBox runat="server" CssClass="form-control" id="TextBoxAddres" TextMode="Email" required="required" ></asp:TextBox>
		</div>
		<div class="form-group">
			<label>Hasło:</label>
			<asp:TextBox runat="server" CssClass="form-control" id="TextBoxPassword" TextMode="Password" required="required" ></asp:TextBox>
		</div>
		<asp:Button ID="ButtonLogIn" CssClass="btn btn-primary" runat="server" Text="Zaloguj" OnClick="ButtonLogIn_Click"></asp:Button>
	</div>
	<script></script>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Projekt_Inzynierski.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="CSS/Default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="col h-100">
		<%if (Session["User"] != null)
			{ %>
		<h3>Dane firmy</h3>
		<div class="table table-bordered p-4">
			<div class="col-6">
				<fieldset class="row">
					<legend>Nazwa</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelName" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>Miasto</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelCity" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>Ulica</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelStreet" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>Poczta</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelPostTown" runat="server"></asp:Label>
							<asp:Label ID="Label2" runat="server" Text="  "></asp:Label>
							<asp:Label ID="LabelPostCode" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>NIP</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelNIP" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>REGON</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelREGON" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>
				<fieldset class="row">
					<legend>Konto bankowe</legend>
					<div class="panel panel-default">
						<div class="panel-body">
							<asp:Label ID="LabelBank" runat="server"></asp:Label>
							<asp:Label ID="LabelBankAccount" runat="server"></asp:Label>
						</div>
					</div>
				</fieldset>

				<div class="clearfix"></div>
			</div>

		</div>
		<%}
			else
			{ %>
		    <h3>System do uproszczonego zarządzania zasobami przedsiębiorstwa</h3>
            <br />
            <asp:Label ID="LabelTest" runat="server" Text="Label"></asp:Label>
		<%} %>
	</div>
</asp:Content>

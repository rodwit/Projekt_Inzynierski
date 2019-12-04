<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Contractors.aspx.cs" Inherits="Projekt_Inzynierski.Contractors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="col h-100">
		<div class="row p-3">
			 <a class="btn btn-primary" href="AddContractor.aspx">Dodaj kontrahenta</a>
		</div>
		<br/>
		<asp:GridView ID="GridViewContractors" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Id" DataSourceID="SqlDataSourceContractors" OnRowCommand="GridViewContractors_RowCommand" OnSelectedIndexChanged="GridViewContractors_SelectedIndexChanged">
			<Columns>
				<asp:BoundField DataField="Id" HeaderText="id" SortExpression="id" InsertVisible="False" ReadOnly="True" Visible="false"/>
				<asp:CommandField ShowSelectButton="True" SelectText="Pokaż" />
				<asp:BoundField DataField="Name" HeaderText="Nazwa" SortExpression="Name" />
				<asp:BoundField DataField="NIP" HeaderText="NIP" SortExpression="NIP" />
				<asp:BoundField DataField="REGON" HeaderText="REGON" SortExpression="REGON" />
				<asp:BoundField DataField="PESEL" HeaderText="PESEL" SortExpression="PESEL" />
				<asp:ButtonField Text="Edytuj"  CommandName="EditContractor" ButtonType="Button">
					<ControlStyle CssClass="btn btn-success" />
				</asp:ButtonField>
				<asp:ButtonField Text="Usuń" CommandName="DeleteContractor" ButtonType="Button">
					<ControlStyle CssClass="btn btn-danger" />
				</asp:ButtonField>
			</Columns>
			<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
			<RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSourceContractors" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT [Id], [Name], [NIP], [REGON], [PESEL] FROM [Contractors] WHERE ([Id_User] = @Id_User)">
			<SelectParameters>
				<asp:SessionParameter Name="Id_User" SessionField="User" Type="Int32" />
			</SelectParameters>
		</asp:SqlDataSource>
	</div>

	<!-- modal info -->
	<div class="modal fade" id="modalInfo" role="dialog">
		<div class="modal-dialog mw-100 w-50">

			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header">
					<h4>Dane kontrahenta <asp:Label ID="LabelTitle" runat="server" Text="Label"></asp:Label></h4>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
				<div class="modal-body">
					<h6>Dane podstawowe</h6>
					<div class=" row w-100 table-bordered p-4 bg-light">
						<div class="col-6">
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Nazwa:</div>
								<asp:Label ID="LabelName" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">PESEL:</div>
								<asp:Label ID="LabelPESEL" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Miasto:</div>
								<asp:Label ID="LabelCity" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Kod pocztowy:</div>
								<asp:Label ID="LabelPostCode" runat="server" Text="Label"></asp:Label>
							</div>
						</div>
						<div class="col-6">
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">NIP:</div>
								<asp:Label ID="LabelNIP" runat="server"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">REGON:</div>
								<asp:Label ID="LabelREGON" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Ulica:</div>
								<asp:Label ID="LabelStreet" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Poczta</div>
								<asp:Label ID="LabelPostTown" runat="server" Text="Label"></asp:Label>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-danger" data-dismiss="modal">Zamknij</button>
				</div>
			</div>

		</div>
	</div>

	<script type="text/javascript">
		function showInfo()
		{
			$('#modalInfo').modal('show');
		}
	</script>

</asp:Content>

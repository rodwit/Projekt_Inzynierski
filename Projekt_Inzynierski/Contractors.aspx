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
                    <div class="col-6"><asp:Button ID="ButtonDetails" runat="server" Text="Szczegóły"  CssClass="btn btn-primary" OnClick="ButtonDetails_Click"/></div>
                    <div class="col-6"><button type="button" class="btn btn-danger float-right" data-dismiss="modal">Zamknij</button></div>        
				</div>
			</div>

		</div>
	</div>


    <!-- modal info -->
	<div class="modal fade" id="modalDetails" role="dialog">
		<div class="modal-dialog mw-100 w-50">

			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header">
					<h4>Szczegółowe dane kontrahenta</h4>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
                <!-- Historia -->
				<div class="modal-body">                    
                    <asp:DropDownList 
                        OnSelectedIndexChanged="HistoryDropDownList_SelectedIndexChanged"
                        AutoPostBack="True"
                        ID="HistoryDropDownList" 
                        runat="server" 
                        DataSourceID="SelectHistory" 
                        DataTextField="AddedDate" 
                        DataValueField="Id"
                        CssClass="selectpicker float-right ml-2" 
                        DataTextFormatString="{0:yyyy-MM-dd}" />
                    <asp:Label 
                        ID="HistoryLabel" 
                        runat="server" 
                        Text="Historia wpisów:" 
                        CssClass="float-right" />
                    <asp:Button 
                        ID="ActalizeButton" 
                        runat="server"
                        CssClass="btn btn-primary mb-2"
                        Text="Aktualizuj"
                        OnClick="ActalizeButton_Click" />
                    <h6>Główny Urząd Statystyczny</h6> <!-- GUS -->
                    <div class="row w-100 table-bordered p-4 bg-light">
                        <div class="col">
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">REGON:</div>
                                <asp:Label ID="LabelGUS_REGON" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">NIP:</div>
                                <asp:Label ID="LabelGUS_NIP" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Status NIP:</div>
                                <asp:Label ID="LabelGUS_StatusNIP" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Nazwa:</div>
                                <asp:Label ID="LabelGUS_Name" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Województwo:</div>
                                <asp:Label ID="LabelGUS_Province" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Powiat:</div>
                                <asp:Label ID="LabelGUS_District" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Gmina:</div>
                                <asp:Label ID="LabelGUS_Commune" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Kod pocztowy:</div>
                                <asp:Label ID="LabelGUS_PostCode" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Poczta:</div>
                                <asp:Label ID="LabelGUS_PostCity" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Miejscowość:</div>
                                <asp:Label ID="LabelGUS_City" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Ulica:</div>
                                <asp:Label ID="LabelGUS_Street" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Nr. nieruchomości:</div>
                                <asp:Label ID="LabelGUS_HouseNr" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Nr. lokalu:</div>
                                <asp:Label ID="LabelGUS_ApartmentNr" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Typ:</div>
                                <asp:Label ID="LabelGUS_Type" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Data zakończenia działalności:</div>
                                <asp:Label ID="LabelGUS_EndDate" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <h6>Ministerstwo Finansów</h6> <!-- Ministerstwo finansów -->
					<div class=" row w-100 table-bordered p-4 bg-light">
                        <div class="col">
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Firma (nazwa) lub imię i nazwisko:</div>
								<asp:Label ID="LabelMF_Name" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">NIP:</div>
								<asp:Label ID="LabelMF_NIP" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Status VAT:</div>
								<asp:Label ID="LabelMF_StatusVAT" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">REGON:</div>
								<asp:Label ID="LabelMF_REGON" runat="server" Text="Label"></asp:Label>
							</div>
                            <hr />
                            <div class="d-flex flex-row">
								<div class="pl-2 col-4">PESEL:</div>
								<asp:Label ID="LabelMF_PESEL" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">KRS:</div>
								<asp:Label ID="LabelMF_KRS" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Adres stałego miejsca prowadzenia działalności albo adres miejsca zamieszkania:</div>
								<asp:Label ID="LabelMF_ResidenceAddress" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Adres siedziby:</div>
								<asp:Label ID="LabelMF_WorkingAddress" runat="server" Text="Label"></asp:Label>
							</div>
                            <hr />
                            <div class="d-flex flex-row">
								<div class="pl-2 col-4">Data rejestracji jako podatnika VAT:</div>
								<asp:Label ID="LabelMF_RegistrationLegalDate" runat="server" Text="Label"></asp:Label>
							</div>
                            <hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Data odmowy rejestracji jako podatnika VAT:</div>
								<asp:Label ID="LabelMF_RegistrationDenialDate" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Podstawa prawna odmowy rejestracji:</div>
								<asp:Label ID="LabelMF_RegistrationDenialBasis" runat="server" ></asp:Label>
							</div>
                            <hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Data wykreślenia rejestracji jako podatnika VAT:</div>
								<asp:Label ID="LabelMF_RemovalDate" runat="server" Text="Label"></asp:Label>
							</div>
                            <hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Podstawa prawna wykreślenia:</div>
								<asp:Label ID="LabelMF_RemovalBasis" runat="server" ></asp:Label>
							</div>
                            <hr />
                            <div class="d-flex flex-row">
								<div class="pl-2 col-4">Data przywrócenia rejestracji jako podatnika VAT:</div>
								<asp:Label ID="LabelMF_RestorationDate" runat="server" Text="Label"></asp:Label>
							</div>
                            <hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Podstawa prawna przywrócenia:</div>
								<asp:Label ID="LabelMF_RestorationBasis" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Numery rachunków rozliczeniowych lub imiennych rachunków w SKOK:</div>
								<asp:Label ID="LabelMF_AccountNumbers" runat="server" Text="Label"></asp:Label>
							</div>
						</div>

                    </div>
				</div>                
				<div class="modal-footer">
                    <div class="p-2">
                        <button 
                            type="button" 
                            class="btn btn-danger" 

                            data-dismiss="modal">Zamknij</button>
                    </div>
				</div>
			</div>
		</div>
	</div>
    <asp:SqlDataSource 
        ID="SelectHistory" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionStringLocalDb %>" 
        SelectCommand="SELECT [Id], Convert(date, [AddedDate]) AS AddedDate FROM [GusDataDtoes] WHERE ([Nip] = @Nip)">
        <SelectParameters>
            <asp:ControlParameter ControlID="LabelNIP" Name="Nip" PropertyName="Text" Type="String" />
        </SelectParameters>
      </asp:SqlDataSource>
	<script type="text/javascript">
		function showInfo()
		{
			$('#modalInfo').modal('show');
        };

        function showDetails()
        {
            $('#modalDetails').modal('show');
        };
	</script>
</asp:Content>

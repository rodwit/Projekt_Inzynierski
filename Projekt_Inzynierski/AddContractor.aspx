<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="AddContractor.aspx.cs" Inherits="Projekt_Inzynierski.AddContractor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Validate/NIP.js"></script>
    <script src="Scripts/Validate/PostCode.js"></script>
    <script src="Scripts/Validate/PESEL.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	

	<div class="col h-100">
		<div class="d-flex flex-row">
			<div class="p-2"><asp:TextBox CssClass="p-2" ID="TextBoxSearchByNIP" runat="server" placeholder="Podaj NIP"></asp:TextBox></div>
            <div class="p-2"><a id="search" runat="server" class="btn btn-primary" onserverclick="ButtonSearch_Click" >Szukaj w bazie danych</a></div>
			<!--<div class="p-2"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBoxSearchByNIP" runat="server" ErrorMessage="Nie poprawny nip" ValidationExpression="([0-9]{3}-?){2}([0-9]{2}-?){2}" CssClass="text-danger"></asp:RegularExpressionValidator></div>-->
		</div>
		<hr />
		<div class="d-flex flex-row">
			<h3>Dane</h3>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">Nazwa</div>
			<asp:TextBox ID="TextBoxName" runat="server" required="required"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">Ulica</div>
			<asp:TextBox ID="TextBoxStreet" runat="server" required="required"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">Miasto</div>
			<asp:TextBox ID="TextBoxCity" runat="server" required="required"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">Kod pocztowy</div>
			<asp:TextBox ID="TextBoxPostCode" runat="server" required="required"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">Poczta</div>
			<asp:TextBox ID="TextBoxPostTown" runat="server" required="required"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">NIP</div>
			<asp:TextBox ID="TextBoxNIP" class="test" runat="server"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">REGON</div>
			<asp:TextBox ID="TextBoxREGON" runat="server"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2 col-2">PESEL</div>
			<asp:TextBox ID="TextBoxPESEL" runat="server"></asp:TextBox>
		</div>
		<br />
		<div class="d-flex flex-row">
			<div class="p-2"><asp:Button ID="ButtonSave" runat="server" Text="Zapisz" CssClass="btn btn-primary" disabled="true" OnClick="ButtonSave_Click"/></div>
		</div>
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
                    <h6>Ministerstwo Finansów</h6> <!-- Ministerstwo finansów -->
					<div class=" row w-100 table-bordered p-4 bg-light">
                        <div class="col-6">
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Nazwa:</div>
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
								<div class="pl-2 col-4">Adres zamieszkania:</div>
								<asp:Label ID="LabelMF_ResidenceAddress" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Adres firmy:</div>
								<asp:Label ID="LabelMF_WorkingAddress" runat="server" Text="Label"></asp:Label>
							</div>
						</div>

						<div class="col-6">
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">Data rejestracji:</div>
								<asp:Label ID="LabelMF_RegistrationLegalDate" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">registrationDenialBasis:</div>
								<asp:Label ID="LabelMF_RegistrationDenialBasis" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">registrationDenialDate:</div>
								<asp:Label ID="LabelMF_RegistrationDenialDate" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">restorationBasis:</div>
								<asp:Label ID="LabelMF_RestorationBasis" runat="server" Text="Label"></asp:Label>
							</div>
                            <div class="d-flex flex-row">
								<div class="pl-2 col-4">restorationDate:</div>
								<asp:Label ID="LabelMF_RestorationDate" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">removalBasis:</div>
								<asp:Label ID="LabelMF_RemovalBasis" runat="server" ></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">removalDate:</div>
								<asp:Label ID="LabelMF_RemovalDate" runat="server" Text="Label"></asp:Label>
							</div>
							<hr />
							<div class="d-flex flex-row">
								<div class="pl-2 col-4">accountNumbers:</div>
								<asp:Label ID="LabelMF_AccountNumbers" runat="server" Text="Label"></asp:Label>
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
        function showInfo() {
            $('#modalInfo').modal('show');
        }
	</script>


	<script type="text/javascript">


        function checkData(data, type) {
            if (type == "NIP")  //NIP
            {
                if (data.length)
                    if (!validatenip(data)) {
                        showModalError("Błędne dane", "Błędny nip");
                        $('[id*=ButtonSearch]').prop('disabled', true);
                    }
                    else
                        $('[id*=ButtonSearch]').prop('disabled', false);
                else
                    $('[id*=ButtonSearch]').prop('disabled', true);

            }
            else if (type == "POST_CODE") {
                if (!validatePostCode(data))
                    showModalError("Błędne dane", "Niepoprawny format kodu pocztowego");
            }
            else if (type == "PESEL") {
                if (data.length)
                    if (!validatePesel(data))
                        showModalError("Błędne dane", "Błędny pesel");
            }

        }

		jQuery.noConflict();
		function ifSwitchButton() {
			var nip = $('[id*=TextBoxNIP]').val();
			var pesel = $('[id*=TextBoxPESEL]').val();

			if (nip.length || pesel.length)
				$('[id*=ButtonSave]').prop('disabled', false);
			else
                $('[id*=ButtonSave]').prop('disabled', true);

            if (nip.length)
                checkData(nip, "NIP");
            if (pesel.length)
                checkData(pesel, "PESEL");

        }

        
		$(function () {
			jQuery.noConflict();
			$('[id*=TextBoxNIP]').change(ifSwitchButton);
            $('[id*=TextBoxPESEL]').change(ifSwitchButton);
            $('[id*=TextBoxSearchByNIP]').change(function () {
                checkData(this.value, "NIP");
            });
            $('[id*=TextBoxPostCode]').change(function () {
                checkData(this.value, "POST_CODE");
            });

        });


	</script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="EditContractor.aspx.cs" Inherits="Projekt_Inzynierski.EditContractor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Validate/NIP.js"></script>
    <script src="Scripts/Validate/PostCode.js"></script>
    <script src="Scripts/Validate/PESEL.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="col h-100">
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
			<div class="p-2"><asp:Button ID="ButtonSave" runat="server" Text="Zapisz" CssClass="btn btn-primary" OnClick="ButtonSave_Click" /></div>
			<div class="p-2"><asp:Button ID="ButtonCancel" runat="server" Text="Anuluj" CssClass="btn btn-danger" OnClick="ButtonCancel_Click"/></div>
		</div>
	</div>

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

        };

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

        };


        $(function () {
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

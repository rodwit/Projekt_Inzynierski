<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="Projekt_Inzynierski.Invoices" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col h-100">
        <div class="row p-3">
            <a class="btn btn-primary" href="AddInvoice.aspx">Wystaw fakturę</a>
        </div>

        <asp:GridView ID="GridViewInvoices" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Id" DataSourceID="SqlDataSourceInvoices" OnRowCommand="GridViewInvoices_RowCommand" OnSelectedIndexChanged="GridViewInvoices_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Pokaż" />
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="false" />
                <asp:TemplateField HeaderText="Numer faktury">
                    <ItemTemplate>
                        <asp:Label ID="LabelInvoiceNumber" runat="server" Text=' <%# SetInvoiceNumber(Eval("Issue_date"), Eval("Number")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Number" HeaderText="Numer" SortExpression="Number" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Kontrahent" SortExpression="Name" />
                <asp:BoundField DataField="Issue_date" HeaderText="Data wydania" SortExpression="Issue_date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="Due_date" HeaderText="Termin zapłaty" SortExpression="Due_date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:ButtonField Text="Usuń" CommandName="DeleteInvoice" ButtonType="Button">
                    <ControlStyle CssClass="btn btn-danger" />
                </asp:ButtonField>
                <asp:ButtonField Text="Drukuj" CommandName="PrintInvoice" ButtonType="Button">
                    <ControlStyle CssClass="btn btn-success" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSourceInvoices" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="SELECT Invoices.Id, Invoices.Number, Contractors.Name, Invoices.Issue_date, Invoices.Due_date FROM Invoices INNER JOIN Contractors ON Invoices.Id_contractor = Contractors.Id where Invoices.Id_user = @userID order by Invoices.Issue_date">
            <SelectParameters>
                <asp:SessionParameter Name="userID" SessionField="User" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>

    <!-- modal info -->
    <div class="modal fade" id="modalInfo" role="dialog">
        <div class="modal-dialog mw-100 w-50">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Dane faktury
                        <asp:Label ID="LabelTitle" runat="server" Text="Label"></asp:Label></h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <h6>Podstawowe informacje</h6>
                    <div class=" row w-100 table-bordered p-4 bg-light">
                        <div class="col-6">
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Kontrahent:</div>
                                <asp:Label ID="LabelContractor" runat="server" Text="Label"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Data wystawienia:</div>
                                <asp:Label ID="LabelIssueDate" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Metoda płatności:</div>
                                <asp:Label ID="LabelPayment" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Odbiorca:</div>
                                <asp:Label ID="LabelReciever" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="d-flex flex-row">
                                <div class="pl-2 col-4">Termin płatności:</div>
                                <asp:Label ID="LabelDueDate" runat="server"></asp:Label>
                            </div>
                            <hr />
                        </div>
                    </div>
                    <br />
                    <h6>Pozycje na dokumencie</h6>
                    <div class=" row table-bordered p-4 bg-light">
                        <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataSourceID="SqlDataSourceProducts" Width="157%" >
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Produkt" SortExpression="Name" />
                                <asp:BoundField DataField="Name1" HeaderText="Jednostka" SortExpression="Name1" />
                                <asp:BoundField DataField="Name2" HeaderText="Stawka" SortExpression="Name2" />
                                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="false" />
                                <asp:BoundField DataField="Amount" HeaderText="Ilość" SortExpression="Amount" />
                                <asp:BoundField DataField="Price" HeaderText="Cena netto" SortExpression="Price" DataFormatString="{0:n}"/>
                                <asp:TemplateField HeaderText="Cena brutto">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelProductBrutto" runat="server" Text=' <%# SetProductBrutto(Eval("Price"), Eval("Value")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:SqlDataSource ID="SqlDataSourceProducts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringSqlProject %>" SelectCommand="select Products.Name,Units.Name, Tax_rates.Name,Tax_rates.Value, Amount, Price from Invoice_Products inner join Tax_rates on Id_tax = Tax_rates.Id
inner join Products on Id_product = Products.Id inner join Units on Products.Id_unit = Units.Id"></asp:SqlDataSource>
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

</asp:Content>

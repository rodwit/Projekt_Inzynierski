function VAT()
{
	var type = 'Netto';

	if ($(this).attr('class') == 'netto')
		type = 'Brutto';

	var id = '[id*=' + type+ this.id[this.id.length - 1] + ']';
	var idTax = '[id*=TaxRate' + this.id[this.id.length - 1] + '] option:selected';
	var taxRate = parseFloat($(idTax).val().replace(',', '.'));
	taxRate += 1;

	if ($(this).attr('class') == 'tax')
	{
		var idNetto = '[id*=Brutto' + this.id[this.id.length - 1] + ']';
		var number = parseFloat($(idNetto).val().replace(',', '.'));
	}
	else
		var number = parseFloat($(this).val().replace(',', '.'));

	var result = 0;
	if (type == 'Brutto')
		result = number * taxRate;
	else
		result = number / taxRate;
	$(id).val(Round(result, 2));
}
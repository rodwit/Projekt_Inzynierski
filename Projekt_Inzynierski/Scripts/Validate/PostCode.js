function validatePostCode(code)
{
	if (code.length != 6)
		return false;
	if (!$.isNumeric(code[0]))
		return false;
	if (!$.isNumeric(code[1]))
		return false;
	if (code[2] != '-')
		return false;
	if (!$.isNumeric(code[3]))
		return false;
	if (!$.isNumeric(code[4]))
		return false;
	if (!$.isNumeric(code[5]))
		return false;
	return true;
}
function showModalError(title, body)
{
	$("#modalError .modal-header").html(title);
	$("#modalError .modal-body").html(body);
	$("#modalError").modal("show");
}
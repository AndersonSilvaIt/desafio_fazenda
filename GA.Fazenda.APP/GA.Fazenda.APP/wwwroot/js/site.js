function AjaxModal(target, loadDiv) {

	$(document).ready(function () {
		$(function () {
			$.ajaxSetup({ cache: false });

			$("a[data-modal]").on("click",
				function (e) {

					var hasClassDelete = $(this).hasClass("dialog-delete");
					if (hasClassDelete) {

						$('div[data-modal-width]').css("max-width", "50%");

					} else {

						//dialog-width-custom
						var hasClassWithCustom = $(this).hasClass("dialog-width-custom");

						if (!hasClassWithCustom) {
							$('div[data-modal-width]').css("max-width", "80%");
						}
					}

					$('#myModalContent').load(this.href,
						function () {
							$('#myModal').modal({
								// desabilita o clique fora do modal
								backdrop: 'static',
								keyboard: true
							},
								'show');

							bindForm(this);
						});
					return false;
				});
		});

		function bindForm(dialog) {

			$(dialog).ready(function () {

				// se a função for definida nas "Index" invoca ela, isso evita erro no console.
				if (typeof AfterLoadModal != 'undefined') {
					AfterLoadModal();
				}
			});

			$('form', dialog).submit(function () {
				//let dataSerialize = '';

				//if (typeof GetDataSerialize != 'undefined') {
				//	dataSerialize = GetDataSerialize(this);
				//} else {
				//	dataSerialize = new FormData(this);
				//}

				$.ajax({
					url: this.action,
					type: this.method,
					data: $(this).serialize(),
					success: function (result) {
						if (result.success) {

							console.log(result.msgToastrResurn);

							$('#myModal').modal('hide');
							if (loadDiv) {
								//esse código, preenche uma div dentro de outra tela, exemplo, tela de cadastro que contém uma outra grid dentr dela, somente essa grid será feita o load
								//$('#' + target).load(result.url); // Carrega o resultado HTML para a div demarcada
							} else {

								if (result.msgToastrResurn != undefined && result.msgToastrResurn != null) {
									localStorage.setItem('msgToastr', result.msgToastrResurn);
								}

								window.location = result.url;
							}
							// esse código rechama a tela, por exemplo, estou inserindo alguma coisa, salvo e volto para a tela de listagem
						} else {
							$('#myModalContent').html(result);
							bindForm(dialog);
						}
					}
				});
				return false;
			});
		}
	});
}

function CollapseAndShowFilterIndex() {

	$('#divFilterList').hide();
	$('#btnClearFilter').hide();
	$('[data-filter-collapse]').hide();

	$('[data-filter-expand]').click(function (e) {
		e.preventDefault();
		$(this).hide();

		$('#divFilterList').slideDown(500);

		$('[data-filter-collapse]').show();
	});

	$('[data-filter-collapse]').click(function (e) {
		e.preventDefault();
		$(this).hide();

		$('#divFilterList').slideUp(500);

		$('[data-filter-expand]').show();
	});
}
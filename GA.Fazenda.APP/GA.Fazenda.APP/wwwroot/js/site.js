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

						var hasClassWithCustom = $(this).hasClass("dialog-width-custom");

						if (!hasClassWithCustom) {
							$('div[data-modal-width]').css("max-width", "80%");
						}
					}

					$('#myModalContent').load(this.href,
						function () {
							$('#myModal').modal({
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

				if (typeof AfterLoadModal != 'undefined') {
					AfterLoadModal();
				}
			});

			$('form', dialog).submit(function () {
				
				$.ajax({
					url: this.action,
					type: this.method,
					data: $(this).serialize(),
					success: function (result) {
						if (result.success) {

							$('#myModal').modal('hide');
							if (loadDiv) {

							} else {

								window.location = result.url;
							}
							
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
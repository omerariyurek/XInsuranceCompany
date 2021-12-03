$(document).ready(function () {
    $("#frmCreateInsuranceOffer").validate({
        errorPlacement: function (n, t) {
            n.insertAfter(t.parent());
        },
        rules: {
            TcIdentityNo: {
                required: true,
                remote: {
                    url: "/Common/VerifyTcIdentificationNumber",
                    type: "POST",
                    data: {
                        TcIdentityNo: function () {
                            return $("#tcIdentityNo").val();
                        }
                    }
                },
                minlength: 11,
                maxlength: 11
            },
            PlateNo: {
                required: true,
                minlength: 4,
                maxlength: 10
            },
            LicenseSerialCode: {
                required: true,
                minlength: 2,
                maxlength: 2
            },
            LicenseSerialNumber: {
                required: true,
                minlength: 6,
                maxlength: 6
            }
        },
        messages: {
            TcIdentityNo: {
                required: "Lütfen TC Kimlik numaranızı girin.",
                remote: "Lütfen geçerli bir TC Kimlik numarası girin.",
                minlength: "TC kimlik numarası 11 haneli olmalıdır.",
                maxlength: "TC kimlik numarası 11 haneli olmalıdır."
            },
            PlateNo: {
                required: "Lütfen araç plakanızı girin.",
                minlength: "Lütfen geçerli bir plaka girin.",
                maxlength: "Lütfen geçerli bir plaka girin."
            },
            LicenseSerialCode: {
                required: "Lütfen ruhsat seri kodunu girin.",
                minlength: "Ruhsat seri kodu 2 haneli olmalıdır.",
                maxlength: "Ruhsat seri kodu 2 haneli olmalıdır."
            },
            LicenseSerialNumber: {
                required: "Lütfen ruhsat seri numarasını girin.",
                minlength: "Ruhsat seri numarası 6 haneli olmalıdır.",
                maxlength: "Ruhsat seri numarası 6 haneli olmalıdır."
            }
        },
        submitHandler: function (form) {
            $.blockUI({
                message:
                    '<h2><img style="height: 40px;width: 40px;" src="/img/loader.gif" /> Teklif oluşturuluyor...</h1>'
            });
            form.submit();
        }
    });


    $('#plateNo').autocomplete({//girilen tc kimlik no ve plakayla eşleşen ruhsat seri no ve kodunu otomatik atar. 
        minLength: 4,
        //delay: 500,
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                method: 'POST',
                url: '/Common/GetOfferDataByPlateNoAndIdentityNo',
                dataType: 'json',
                data: {
                    plateNo: $(this.element).val(),
                    identityNo: $('#tcIdentityNo').val()
                },
                success: function (response) {
                    $("#plateNo").removeClass("ui-autocomplete-loading");
                    if (response.isSuccess) {
                        $('#licenseSerialCode').val(response.data.licenseSerialCode);
                        $('#licenseSerialNo').val(response.data.licenseSerialNumber);

                    }
                },
                error: function (xhr, status, error) {
                    $("#plateNo").removeClass("ui-autocomplete-loading");
                }
            });
        }
    });
});
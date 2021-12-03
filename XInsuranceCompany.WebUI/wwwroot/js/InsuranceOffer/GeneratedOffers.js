$(document).ready(function () {
    $("#frmViewGeneratedOffers").validate({
        errorPlacement: function (n, t) {
            n.insertAfter(t.parent());
        },
        rules: {
            tcIdentityNo: {
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
            }
        },
        messages: {
            tcIdentityNo: {
                required: "Lütfen TC Kimlik numaranızı girin.",
                remote: "Lütfen geçerli bir TC Kimlik numarası girin.",
                minlength: "TC kimlik numarası 11 haneli olmalıdır.",
                maxlength: "TC kimlik numarası 11 haneli olmalıdır."
            }
        },
        submitHandler: function (form) {
            $.blockUI({
                message:
                    '<h2><img style="height: 40px;width: 40px;" src="/img/loader.gif" /> Teklifler getiriliyor...</h1>'
            });
            form.submit();
        }
    });
});
var Register = function () {
    var handleRegister = function () {
        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                userName: {
                    required: true
                },
                password: {
                    required: true
                },
                confirmPassword: {
                    equalTo: "#password"
                },
                tnc: {
                    required: true
                }
            },
            messages: { // custom messages for radio buttons and checkboxes
                userName: '请填写手机号/会员名/邮箱',
                password: '请填写密码',
                confirmPassword: {
                    equalTo: '两次填写的密码不一致',    
                },
                tnc: {
                    required: "请先同意服务条款与隐私政策"
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit   

            },
            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
                    error.insertAfter($('#register_tnc_error'));
                } else if (element.closest('.input-icon').size() === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },

            submitHandler: function (form) {
                form.submit();

//                $.post(form.action, $(form).serialize(), function (data) {
//                    if (data.success) {
//                        location.href = data.returnUrl;
//                    } else {
//                        $('.alert-danger span', $('.login-form')).html(data.message);
//                        $('.alert-danger', $('.login-form')).show();
//                    }
//                });
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit();
                }
                return false;
            }
        });
    }

    return {
        init: function () {
            handleRegister();
        }
    };

}();

jQuery(document).ready(function () {
    Register.init();
});
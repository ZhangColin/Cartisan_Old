var Login = function () {

    var handleLogin = function () {

        $('.login-form').validate({
            errorElement: 'span',
            errorClass: 'help-block', 
            focusInvalid: true, 
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "请填写用户名/邮箱/手机"
                },
                password: {
                    required: "请填写密码"
                }
            },

            invalidHandler: function (event, validator) {
                $('.alert-danger span', $('.login-form')).html(validator.errorList[0].message);
                $('.alert-danger', $('.login-form')).show();
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
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                //form.submit(); // form validation success, call ajax form submit
                $.post(form.action, $(form).serialize(), function(data) {
                    if (data.success) {
                        location.href = data.returnUrl;
                    } else {
                        $('.alert-danger span', $('.login-form')).html(data.message);
                        $('.alert-danger', $('.login-form')).show();
                    }
                });
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });
    }

    return {
        init: function () {
            handleLogin();
        }
    };

}();

jQuery(document).ready(function () {
    Login.init();
});
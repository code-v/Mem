$(function () {
    $.validator.setDefaults({
        debug: true,
        errorElement: "span",
        errorClass: "error"
    })

    $.validator.addMethod("compareDate", function (value, element, param) {
        var d1 = jQuery(param).val();

        var d1 = d1.replace("-", "").replace("-", "");
        var d2 = value.replace("-", "").replace("-", "");

        return d1 <= d2;
    }, "结束日期要大于或等于开始日期");

    $.validator.addMethod("contact", function (value, element, param) {

        return $.trim(value) != "" || $.trim($(param).val()) != "";
    }, "请输入至少一个联系方式");

    $.validator.addMethod("contactPhone", function (value, element, param) {
        var str = $.trim($(param).val()) + "-" + $.trim(value)

        var tel = /^\d{3,4}-\d{7,8}$/;
        var tel1 = /(^1[3-8]\d{9}$)/;
        return tel.test(str) || tel1.test(str) || this.optional(element);

    }, "请输入正确的电话号码");

    //验证是否为中英文
    $.validator.addMethod("trueName", function (value, element, param) {
        var reg = /[^\d\a-zA-Z\u4E00-\u9FA5]/g;

        return !reg.test(value) || this.optional(element);
    }, "请输入中文或英文或数字");

    $.validator.addMethod("url", function (value, element) {
        var strRegex = '^((https|http|ftp|rtsp|mms)?://)'
            + '?(([0-9a-z_!~*\'().&=+$%-]+: )?[0-9a-z_!~*\'().&=+$%-]+@)?' //ftp的user@ 
            + '(([0-9]{1,3}.){3}[0-9]{1,3}' // IP形式的URL- 199.194.52.184 
            + '|' // 允许IP和DOMAIN（域名） 
            + '([0-9a-z_!~*\'()-]+.)*' // 域名- www. 
            + '([0-9a-z][0-9a-z-]{0,61})?[0-9a-z].' // 二级域名 
            + '[a-z]{2,6})' // first level domain- .com or .museum 
            + '(:[0-9]{1,4})?' // 端口- :80 
            + '((/?)|' // a slash isn't required if there is no file name 
            + '(/[0-9a-z_!~*\'().;?:@&=+$,%#-]+)+/?)$';
        var re = new RegExp(strRegex);

        return re.test(value) || this.optional(element);
    }, "请输入正确的网址");

    $.validator.addMethod("qq", function (value, element) {
        var reg = /^[1-9]*[1-9][0-9]*$/;

        return reg.test(value) || this.optional(element);
    }, "请输入正确的邮编");

    $.validator.addMethod("code", function (value, element) {
        var code = /^\d{6}$/;
        return code.test(value) || this.optional(element);
    }, "请输入正确的邮编");

    $.validator.addMethod("Phone", function (value, element) {
        var tel = /^\d{3,4}-\d{7,8}$/;
        var tel1 = /(^1[3-8]\d{9}$)/;
        return tel.test(value) || tel1.test(value) || this.optional(element);
    }, "请输入正确的联系电话");

    $.validator.addMethod("email", function (value, element) {
        var em = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        return em.test(value) || this.optional(element);
    }, "请输入正确的邮箱");

    $.validator.addMethod("telPhone", function (value, element) {
        value = $.trim(value);
        var tel = /^\d{3,4}-\d{7,8}$/;
        return tel.test(value) || this.optional(element);
    }, "请输入正确的固话格式");

    $.validator.addMethod("mobilephone", function (value, element) {
        var tel = /(^1[3-8]\d{9}$)/;
        return tel.test(value) || this.optional(element);
    }, "请输入正确的手机号码");

    $.validator.addMethod("username", function (value, element) {
        var em = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        var tel = /(^1[3-8]\d{9}$)/;
        return tel.test(value) || em.test(value) || this.optional(element);
    }, "请输入正确的邮箱或手机号码");

    $.validator.addMethod("selectarea", function (value, element) {
        var ispass = false;
        if (value != "请选择") {
            ispass = true;
        }
        return ispass || this.optional(element);
    }, "请选择地区");

    $.validator.addMethod("selectprofession", function (value, element) {
        var ispass = false;
        if (value != "请选择") {
            ispass = true;
        }
        return ispass || this.optional(element);
    }, "请选择行业");

    $.validator.addMethod("selectproduct", function (value, element) {
        var ispass = false;
        if (value != "请选择") {
            ispass = true;
        }
        return ispass || this.optional(element);
    }, "请选择产品分类");

    // 判断跟某个值不能相同的
    $.validator.addMethod("notequalTo", function (value, element, param) {
        var target = $(param);

        return value != target.val();
    });
})
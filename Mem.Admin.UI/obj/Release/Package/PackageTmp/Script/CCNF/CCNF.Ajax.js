(function ($) {
    /*【GET】方式获取HTML】*/
    $.fn.AjaxHtml = function (url, param) {
        var $list = $(this);
        $.ajax
        ({
            cache: false,
            type: "get",
            url: url,
            data: param,
            success: function (result) {
                $list.html(result);
            }
        });
    },

    $.fn.CCNFBindEnterKey = function (id) {
        $(this).keydown(function (e) {
            e = (e) ? e : ((window.event) ? window.event : "")
            keyCode = e.keyCode ? e.keyCode : (e.which ? e.which : e.charCode);
            if (keyCode == 13) {
                document.getElementById(id).click();
            }
        });
    },

    //设置文本框的关键字
    $.fn.CCNFSetTextKeyword = function (keyword) {
        if ($(this).val() == "") {
            $(this).css("color", "#999999").val(keyword);
        }

        $(this).focus(function () {
            if ($(this).val() == keyword) {
                $(this).css("color", "#333333").val("")
            }
        }).blur(function () {
            if ($(this).val() == "") $(this).css("color", "#999999").val(keyword)
        })
    },

    //复选框全选效果
     $.fn.CCNFSelectAllCheckbox = function (name) {
         $(this).click(function () {
             if ($(this).attr('checked')) {
                 $("[name='" + name + "']:checkbox").attr('checked', 'checked');
             } else {
                 $("[name='" + name + "']:checkbox").attr('checked', null);
             }
         })
     }

})(jQuery)

 jQuery.CCNF = {
     ajaxForm2: function (url, parm, success,beforeSend) {
         $.ajax
        ({
            type: "post",
            url: url,
            data: parm,
            success: success,
            beforeSend:beforeSend
        });
     },
    ajaxForm: function (url, parm, success) {
        $.ajax
        ({
            type: "post",
            url: url,
            data: parm,
            success: success
        });
    },
    //获取当前时间
    getTime: function () {
        var now = new Date(),
			h = now.getHours(),
			m = now.getMinutes(),
			s = now.getSeconds(),
			ms = now.getMilliseconds();
        return (h + ":" + m + ":" + s + " " + ms);
    },

    //请求数据编码处理
    StringUtil: new function () {

        this.Base64Encode = function (str) {
            if (!str || str == '') {
                return "";
            }
            return base64encode(utf16to8(str));
        };

        this.Base64Decode = function (str) {
            if (!str || str == '') {
                return "";
            }
            return utf8to16(base64decode(str))
        };

        var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var base64DecodeChars = new Array(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1, -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);
        var base64encode = function (str) {
            var out, i, len;
            var c1, c2, c3;
            len = str.length;
            i = 0;
            out = "";
            while (i < len) {
                c1 = str.charCodeAt(i++) & 0xff;
                if (i == len) {
                    out += base64EncodeChars.charAt(c1 >> 2);
                    out += base64EncodeChars.charAt((c1 & 0x3) << 4);
                    out += "==";
                    break;
                }
                c2 = str.charCodeAt(i++);
                if (i == len) {
                    out += base64EncodeChars.charAt(c1 >> 2);
                    out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                    out += base64EncodeChars.charAt((c2 & 0xF) << 2);
                    out += "=";
                    break;
                }
                c3 = str.charCodeAt(i++);
                out += base64EncodeChars.charAt(c1 >> 2);
                out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                out += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
                out += base64EncodeChars.charAt(c3 & 0x3F);
            }
            return out;
        };

        var base64decode = function (str) {
            var c1, c2, c3, c4;
            var i, len, out;
            len = str.length;
            i = 0;
            out = "";
            while (i < len) {
                do {
                    c1 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
                } while (i < len && c1 == -1);
                if (c1 == -1)
                    break;
                do {
                    c2 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
                } while (i < len && c2 == -1);
                if (c2 == -1)
                    break;
                out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));
                do {
                    c3 = str.charCodeAt(i++) & 0xff;
                    if (c3 == 61)
                        return out;
                    c3 = base64DecodeChars[c3];
                } while (i < len && c3 == -1);
                if (c3 == -1)
                    break;
                out += String.fromCharCode(((c2 & 0XF) << 4) | ((c3 & 0x3C) >> 2));
                do {
                    c4 = str.charCodeAt(i++) & 0xff;
                    if (c4 == 61)
                        return out;
                    c4 = base64DecodeChars[c4];
                } while (i < len && c4 == -1);
                if (c4 == -1)
                    break;
                out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
            }
            return out;
        };
        var utf16to8 = function (str) {
            var out, i, len, c;
            out = "";
            len = str.length;
            for (i = 0; i < len; i++) {
                c = str.charCodeAt(i);
                if ((c >= 0x0001) && (c <= 0x007F)) {
                    out += str.charAt(i);
                } else if (c > 0x07FF) {
                    out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
                    out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
                    out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
                } else {
                    out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
                    out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
                }
            }
            return out;
        };
        var utf8to16 = function (str) {
            var out, i, len, c;
            var char2, char3;
            out = "";
            len = str.length;
            i = 0;
            while (i < len) {
                c = str.charCodeAt(i++);
                switch (c >> 4) {
                    case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                        out += str.charAt(i - 1);
                        break;
                    case 12: case 13:
                        char2 = str.charCodeAt(i++);
                        out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                        break;
                    case 14:
                        char2 = str.charCodeAt(i++);
                        char3 = str.charCodeAt(i++);
                        out += String.fromCharCode(((c & 0x0F) << 12) | ((char2 & 0x3F) << 6) | ((char3 & 0x3F) << 0));
                        break;
                }
            }
            return out;
        };

        // json转换成字符串
        this.JsonObjectToString = function (o) {
            var arr = [];
            var fmt = function (s) {
                if (typeof s == 'object' && s != null) return $.CCNF.StringUtil.JsonObjectToString(s);
                return /^(string|number)$/.test(typeof s) ? "'" + s + "'" : s;
            };

            if (o instanceof Array) {
                for (var i in o) {
                    if (typeof o[i] != 'function') {
                        arr.push(fmt(o[i]));
                    }
                }
                return '[' + arr.join(',') + ']';

            }
            else {
                for (var i in o) {
                    arr.push("'" + i + "':" + fmt(o[i]));
                }
                return '{' + arr.join(',') + '}';
            }
        };

        //JSON字符串转换为json对象
        //    this.StringToJSON = function (obj) {
        this.StringToJSON = function (obj) {
            return eval('(' + obj + ')');
        };


        //把HTML符号转成特殊符合
        this.Filter = function (vText) {
            var sText = vText.toString();
            sText = sText.replace("&amp;", "&");
            sText = sText.replace('&quot;', '"');
            sText = sText.replace("&lt;", "<");
            sText = sText.replace("&gt;", ">");
            sText = sText.replace("&apos;", "'");
            return sText;
        };

        //把特殊符合替换成HTML符号
        this.FilterHtml = function (vText) {
            var sText = vText.toString();
            sText = sText.replace(/&/g, "&amp;");
            sText = sText.replace(/"/g, "&quot;");
            sText = sText.replace(/</g, "&lt;");
            sText = sText.replace(/>/g, "&gt;");
            sText = sText.replace(/'/g, "&apos;");
            return sText;
        };

        //返回字符数，中文算1，英文和数字算0.5。
        this.GetTextLen = function (val) {
            val = $.trim(val).split('');
            var len = 0;
            for (var i = 0; i < val.length; i++) {
                if (val[i].match(/[^\x00-\xff]/ig) != null) //全角
                    len += 1;
                else
                    len += 0.5;
            }
            return len;
        };
    },

    //获取url参数
    Request: new function () {
        //获得对应参数的的值。
        this.QueryString = function (name) {
            return this.QueryStrings()[name.toLowerCase()];
        };

        //获得所有参数的的值。
        this.QueryStrings = function () {
            var result = {};
            var url = window.location.href.toLowerCase() ;
            var parameters = url.slice(url.indexOf('?') + 1).split('&');

            for (var i = 0; i < parameters.length; i++) {
                var parameter = parameters[i].split('=');
                result[parameter[0]] = parameter[1];
            }
            return result;
        };
    },
    JsonObjectToString: function (o) {
        var arr = [];
        var fmt = function (s) {
            if (typeof s == 'object' && s != null) return $.CCNF.JsonObjectToString(s);
            return /^(string|number)$/.test(typeof s) ? "\"" + s + "\"" : s;
        };

        if (o instanceof Array) {
            for (var i in o) {
                if (typeof o[i] != 'function') {
                    arr.push(fmt(o[i]));
                }
            }
            return '[' + arr.join(',') + ']';

        }
        else {
            for (var i in o) {
                arr.push("\"" + i + "\":" + fmt(o[i]));
            }
            return '{' + arr.join(',') + '}';
        }
    },

    // 刷新页面，兼容火狐和IE。
    RefreshPage: function () {
        var t = parseInt(Math.random() * 1000000, 10);
        if (window.location.href.indexOf('?') > 0) {
            window.location.href = window.location.href + '&t=' + t;
            window.location.reload(true); //一定要有true 强制刷新 这样才能保证页面整个重新载入
        }
        else {
            window.location.href = window.location.href + '?t=' + t;
            window.location.reload(true);
        }
    }
}
(function ($) {
    $.fn.extend({
        CCNFSelect: function (e) {

            var d = { hideObject: null, type: 0, hideObjectValue: null }; //type: 0,缺省值表示选择类型为地区,hideObjectValue:对hideObject进行付值
            d = $.extend(d, e || {});

            var $inputText = this;
            var $hidId = (typeof d.hideObject == "object") ? d.hideObject : $(d.hideObject);

            //当hideObjectValue有值时,对隐藏域进行付值
            if (d.hideObjectValue != null) {
                $hidId.val(d.hideObjectValue);
            }

            //获取位置
            var left = $inputText.offset().left;
            var top = $inputText.offset().top + $inputText.height() / 5;

            var checkType = d.type; //查询类型，0表示地区，1表示行业
            var checkId = $hidId.val() == '' ? 0 : $hidId.val();
            var selectIndex = 0; //下拉框的后缀编号，用于区分下拉框
            var selectareaId; //显示框ID
            var area_tabsId; //tab（切换条ID）
            var tab_conboxId; //子项ID
            var confirmId; //确认ID

            IntSelect(checkId)//初始化

            $inputText.focus(function () {

                if ($("#" + selectareaId).length > 0) {

                    $(".select_content").hide();
                    $("#" + selectareaId).show();

                }
                ClickBind();

                $("#" + MadeName("areaUC", selectIndex)).css({ "position": "absolute", "top": top, "left": left });
            });

            var liBar; //全局变量保存tab_conbox最后li项的状态

            //选择子项
            $("#" + tab_conboxId + " .dataChildLi").live("click", function () {

                var index = $("#" + tab_conboxId).children("li").index($(this).parent().parent()); //获取所处的位置下标
                $("#" + tab_conboxId).children("li").hide();
                $("#" + area_tabsId).children("li").slice(index + 1).remove(); //重新选择，移除子项(所有下级)
                $("#" + tab_conboxId).children("li").slice(index + 1).remove(); //重新选择，移除子项(所有下级)

                var id = $(this).attr("data-value");
                var tabName = $(this).children().html();

                $.ajax({
                    type: "get",
                    url: "/DataBase/GetDataBase",
                    datatype: "json",
                    data: { id: id, target: GetAction(checkType).split(',')[0] },
                    success: function (data, textStatus) {

                        $(MadeName("#imgLoading", selectIndex)).remove();
                        data = eval(data); //再次转json

                        if (data.length > 0) {//含有子项
                            liBar = "<li><ul>";
                            var lis = "";
                            //父tab
                            $("#" + area_tabsId).children("li").eq(index).find("span").html(tabName);
                            $("#" + area_tabsId).append(" <li  class='Select_tab'><a  href='javascript:void(0)'><span>" + "请选择" + "</span><i></i></a></li>");
                            $("#" + area_tabsId).children("li:last").siblings().addClass("Select_tab").removeClass("Select_tab");
                            //子tab
                            for (var i = 0; i < data.length; i++) {

                                lis += "<li class='dataChildLi' data-value='" + data[i].Id + "'><a href='javascript:void(0)'>" + data[i].Name + "</a></li>";
                            }
                            liBar = liBar + lis + "</ul></li>";

                        }
                        else {//没有子项

                            $("#" + tab_conboxId).children("li:last").remove(); //移除对liBar重复添加
                            $("#" + area_tabsId).children("li:last").find("span").html(tabName);

                            //兼容jquery.validate.js，使用焦点失去，再次激发效验事件
                            $inputText.blur();

                            $("#" + selectareaId).hide();

                        }

                        $("#" + tab_conboxId).append(liBar);

                        ClickBind();

                        $hidId.val(id);
                        $inputText.val(GetSelectResult());

                    },
                    beforeSend: function () {
                        var loading = "<img id='" + MadeName("imgLoading", selectIndex) + "' src='http://www.ccnf.com/Include/JS/wbox/loading.gif' style='position:absolute;top:60px;left:230px; z-index:100;width:18px;height:18px' />"
                        $("#" + tab_conboxId).append(loading);

                    }

                });


            });

            //事件绑定
            function ClickBind() {

                //项部tab切换事件绑定
                $("#" + area_tabsId + " li").click(function () {

                    $(this).addClass("Select_tab").siblings().removeClass("Select_tab");
                    $("#" + tab_conboxId).children("li").eq($(this).index()).show().siblings().hide();

                });

                //确定与关闭事件绑定
                $("#" + confirmId + ",#" + MadeName("close", selectIndex)).click(function () {

                    //兼容jquery.validate.js，使用焦点失去，再次激发效验事件
                    $inputText.blur();
                    
                    $("#" + selectareaId).hide();


                });

                //$("#" + tab_conboxId + " .dataChildLi").hover(function () { $(this).css("background", "#E0F2F7"); }, function () { $(this).css("background", "#fff"); });
            }

            //根据ID初始化
            function IntSelect(id) {

                //初始化唯一编号
                selectIndex = (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);

                //初始化各项编号
                selectareaId = MadeName("selectarea", selectIndex); //显示框ID
                area_tabsId = MadeName("area_tabs", selectIndex); //tab（切换条ID）
                tab_conboxId = MadeName("tab_conbox", selectIndex); //子项ID
                confirmId = MadeName("confirm", selectIndex);

                $inputText.attr("readonly", "readonly"); //设定只读状态

                var paramId = id;

                //当没默认ID值时根据类型付予ID初始值,行业：900，地区：999 特殊值无对应值
                if (paramId == 0 || paramId == 900 || paramId == 999) {

                    switch (checkType) {
                        case 0:
                            if (paramId == 999) {
                                paramId = 1;
                            }
                            else {
                                paramId = 1;
                            }
                            break;
                        case 1:
                            if (paramId == 900) {
                                paramId = 11
                            }
                            else {
                                paramId = 11;
                            }

                            break;
                        default:
                            paramId = 11;
                            break;
                    }
                }

                //查询所有父级下的所有子项
                $.getJSON("/DataBase/GetDataBase", { id: paramId, target: GetAction(checkType).split(',')[1] }, function (data) {

                    var tab_conboxLi = "";
                    for (var i = 0; i < data.length; i++) {
                        var outsideLi = "<li><ul>";
                        var insideLi = "";
                        for (var j = 0; j < data[i].length; j++) {
                            insideLi += "<li class='dataChildLi' data-value='" + data[i][j].Id + "'><a href='javascript:void(0)'>" + data[i][j].Name + "</a></li>";

                        }
                        tab_conboxLi += outsideLi + insideLi + "</ul></li>";
                    }

                    var droplistdiv = "<div class=\"selectUC\" id='" + MadeName("areaUC", selectIndex) + "'><div class=\"select_content\" id='" + selectareaId + "' style=\"display:none\" class=\"select_show\"><div class=\"close\" id='" + MadeName("close", selectIndex) + "'></div>  <div id=\"tabbox\"><div class=\"div_select_tabs\"> <ul id='" + area_tabsId + "' class=\"select_tabs\">" + SetTopTabs(data, id) + "</ul></div><ul class=\"tab_conbox\" id='" + tab_conboxId + "'> " + tab_conboxLi + " </ul></div> <div class=\"select_input\"> <input type=\"button\" class='btn01' value=\"确定\" id='" + confirmId + "' /></div></div></div>";

                    $("body").append(droplistdiv);

                    $("#" + tab_conboxId).children("li:last").siblings().hide();
                    $inputText.val(GetSelectResult());

                });

            }

            //初始化时设定tab
            function SetTopTabs(data, id) {
                var tab = "";
                var tabname = "";

                for (var i = data.length - 1; i > -1; i--) {//从最底子项开始

                    for (var j = 0; j < data[i].length; j++) {
                        if (data[i][j].Id == id) {
                            tabname += data[i][j].Name + "-";
                            id = data[i][j].PId; //重新设定id住上找父级

                        }

                        if (checkId == 0) {

                            tabname = "请选择-";
                        }

                    }

                }

                var tabnameArr = tabname.substring(0, tabname.length - 1).split('-');
                tabnameArr.reverse()
                $inputText.val(tabnameArr.join('-'));        //以－号分割显示

                for (var j = 0; j < tabnameArr.length; j++) {

                    if (j != tabnameArr.length - 1) {
                        tab += "<li><a  href=\"javascript:void(0)\"><span>" + tabnameArr[j] + "</span><i></i></a></li>";
                    } else {
                        tab += "<li class=\"Select_tab\"><a  href=\"javascript:void(0)\"><span>" + tabnameArr[j] + "</span><i></i></a></li>";

                    }
                }

                return tab;
            }

            //获取选择结果
            function GetSelectResult() {
                var result = "";

                $("#" + area_tabsId).children("li").each(function () {

                    result += $(this).find("span").html() + "-";

                })

                result = result.substring(0, result.lastIndexOf('-')).replace("-请选择", "");
                return result;

            }

            //构造新的ID或class
            function MadeName(name, index) {
                return name + index;
            }

            //根据类型返回action（获取地区或行业）；0:地区，1：行业,2:产品
            function GetAction(type) {

                switch (type) {
                    case 0:
                        return "GetChildrenArea,GetAllParentArea";
                    case 1:
                        return "GetChildrenPro,GetAllParentPro";
                    case 2:
                        return "GetChildrenProduct,GetAllParentProduct";

                }
            }
        }
    });
})(jQuery)
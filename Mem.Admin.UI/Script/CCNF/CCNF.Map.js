/*
功能:展会地图定位
示例：
$("#dragMap").CCNFMap({  hideObj: "#hideMapXY" });      
参数说明： 
dragMap包住地图img外层div, hideObj存储结果值的隐藏对象  x|y:x是x坐标,y是y坐标
*/
(function ($) {
    $.fn.extend({

        CCNFMap: function (e) {

            var d = { hideObj: null, hideObjValue: null }; //初始时要付的值:hideObjectValue:(对hideObject进行付值)
            d = $.extend(d, e || {});
            var $dragMap = this; //地图图片容器
            var $dragMapContainer = this.parent(); //地图图片显示区域容器
            var $mapImg = $dragMap.children("img"); //地图图片
            var $hideObject = (typeof d.hideObj == "object") ? d.hideObj : $(d.hideObj); //存储结果值的隐藏对象

            var mapImgWidthMin = $dragMap.width(); //图片最小宽度
            var mapImgHeightMin = $dragMap.height(); //图片缩放最小高度
            var mapImgWidthMax = 1888; //图片放大最大宽度

            var percent = 0.8; //比例

            var posx; //标识物相对imgMap的x绝对位置
            var posy; //标识物相对imgMap的y绝对位置
            var savePosx; //要保存的x绝对位置
            var savePosy; //要保存的y绝对位置

            $dragMapContainer.css({ "overflow": "hidden", "position": "relative" }); //设定“镜头”
            //var marker = "<div  id='marker' style='display:none; width:31px; height:42px; background:red; position:absolute;'></div>"; //标识物（座标）

            var marker = "<img src='http://static.ccnf.com/images/bintouch/map_gps.png' id='marker' style='display:none; width:31px; height:42px; position:absolute;'>"; //标识物（座标）

            var zoom = "<div style=\"z-index: 100;width:40px; height:85px; position:absolute; left:23px; top:30px;\"> <a  id=\"btToBigObj\"  style=\" width:40px;height:40px; display:block;background:url(http://static.ccnf.com/images/bintouch/zoom.png) right top no-repeat;\" href=\"javascript:void;\"></a><br /><a  id=\"btToSmallObj\" style=\"width:40px;height:40px; display:block;background:url(http://static.ccnf.com/images/bintouch/zoom.png) right bottom no-repeat;\"href=\"javascript:void;\"></a> </div>"

            $dragMap.append(marker); //标识物加到地图图片容器容器中
            $dragMap.parent().append(zoom);

            //IntValue，就进行付初始值
            var IntValue = $hideObject.val();
            if (IntValue != "") {
                var IntValue = $hideObject.val().split('|');
                if (IntValue.length == 2) {  
                    setPos(parseFloat(IntValue[0]).toFixed(5), parseFloat(IntValue[1]).toFixed(5)); //初始化
                }
            } else {
                setPos($dragMapContainer.width() / 2, $dragMapContainer.height() / 2); //初始化,无数据时默认位置中间
            }


            /**放大缩小按钮 样式**/
            $("#btToSmallObj").live("mouseover", function () {
                $(this).css("background-image", "url(http://static.ccnf.com/images/bintouch/zoom.png)");
                $(this).css({ "backgroundPosition": "left bottom" });

            });
            $("#btToSmallObj").live("mouseout", function () {
                $(this).css("background-image", "url(http://static.ccnf.com/images/bintouch/zoom.png)")
                $(this).css({ "backgroundPosition": "right bottom" });
            });

            $("#btToBigObj").live("mouseover", function () {
                $(this).css("background-image", "url(http://static.ccnf.com/images/bintouch/zoom.png)");
                $(this).css({ "backgroundPosition": "left top" });
            });
            $("#btToBigObj").live("mouseout", function () {
                $(this).css("background-image", "url(http://static.ccnf.com/images/bintouch/zoom.png)")
                $(this).css({ "backgroundPosition": "right top" });
            });
            /**放大缩小按钮 样式**/

            //缩小
            $("#btToSmallObj").live("click", function () {
                if ($mapImg.is(":animated")) {//防止点击过快,动画未执行完
                    return;
                }
                var width = $mapImg.width();
                var height = $mapImg.height();

                //缩放时防止显示区域(container)出现空白地图 start
                var mx = Math.abs($dragMap.offset().left - $dragMapContainer.offset().left); //两个容器x间距
                var my = Math.abs($dragMap.offset().top - $dragMapContainer.offset().top); //两个容器y间距

                var mx2 = ($mapImg.width() * percent - $dragMapContainer.width()) - mx;
                var my2 = ($mapImg.height() * percent - $dragMapContainer.height()) - my;

                //因为是以左上角为轴点缩放所以在合理范围内的缩放是left与top都要大于0
                if (mx2 < 0 || my2 < 0) {

                    var leftMx2 = $dragMap.position().left - mx2;
                    var toptMy2 = $dragMap.position().top - my2;

                    if (leftMx2 > 0 && toptMy2 < 0) {
                        $dragMap.animate({ "left": 0, "top": toptMy2 }, 500);
                    }
                    if (leftMx2 < 0 && toptMy2 > 0) {
                        $dragMap.animate({ "left": leftMx2, "top": 0 }, 500);
                    }
                    if (leftMx2 < 0 && toptMy2 < 0) {
                        $dragMap.animate({ "left": leftMx2, "top": toptMy2 }, 500);
                    }
                }
                //缩放时防止显示区域(container)出现空白地图 end

                //缩放最小限制
                if (width * percent < mapImgWidthMin || (height * percent + 1) < mapImgHeightMin) {//最小限制
                    return;
                }

                //缩放
                $mapImg.animate({ "width": (width * percent), "height": (height * percent) }, 500);

                if (posx != null && posy != null) {

                    setPos(((posx + 31 / 2) * percent - (31 / 2)), ((posy + 39) * percent - 39)); //重新设置标识物的位置
                }
            });

            //放大
            $("#btToBigObj").live("click", function () {

                if ($mapImg.is(":animated")) {//防止点击过快,动画未执行完
                    return;
                }

                var width = $mapImg.width();
                var height = $mapImg.height();

                if (width * (1 / percent) > mapImgWidthMax) {//最大限制
                    return;
                }
                //alert(posx + "," + posy);
                $mapImg.animate({ "width": width * (1 / percent), "height": height * (1 / percent) }, 500);

                if (posx != null && posy != null) {
                    //进行转换浮点,防止未知的变量被当成字符串处理
                    var x2 = (parseFloat(posx) + parseFloat(31 / 2)) * (1 / percent) - parseFloat(31 / 2);
                    var x3 = (parseFloat(posy) + 39) * (1 / percent) - 39;

                    setPos(x2, x3); //重新设置标识物的位置
                }
            });

            //地图上双击，进行设定坐标
            $dragMap.dblclick(function (e) {

                //$.CCNFBox.confirm("是否设置座标", confirmPos);
                confirmPos(); //先这样保留
                function confirmPos() {
                    //设定标识物
                    var markerX = e.pageX - $dragMap.offset().left; //未调整的标识物位置相对位置
                    var markerY = e.pageY - $dragMap.offset().top; //未调整的标识物位置相对位置

                    savePosx = (markerX / $mapImg.width()) * mapImgWidthMin - $("#marker").width() / 2; //比例乘以mapImg 宽,最后要保存的位置x
                    savePosy = (markerY / $mapImg.height()) * mapImgHeightMin - $("#marker").height() + 3; //比例乘以mapImg 长,最后要保存的位置y

                    savePosx = parseFloat(savePosx).toFixed(4); //保留4位小数
                    savePosy = parseFloat(savePosy).toFixed(4); //保留4位小数

                    $hideObject.val(savePosx + "|" + savePosy); //存放最终结果坐标

                    //保存地图坐标
                    SubmitSaveMap();

                    setPos(markerX - $("#marker").width() / 2, markerY - $("#marker").height() + 3); //设定标识物位置
                }
            });

            //设定标识物位置方法
            function setPos(x, y) {
                posx = x;
                posy = y;
                $("#marker").css({ "display": "block" });
                $("#marker").animate({ "left": x, "top": y }, 500);
            }

            //设定拖拽标识物时设定位置方法,少了移动动画效果
            function MoveSetPos(x, y) {
                posx = x;
                posy = y;
                $("#marker").css({ "display": "block" });
                $("#marker").css({ "left": x, "top": y });
            }

            BindDragMapEvent(); //地图拖拽事件绑定

            //地图拖拽事件 start
            function BindDragMapEvent() {

                $dragMap.mousedown(function (e) {

                    var moveObj = $(this); //拖动的对象
                    moveObj.css('cursor', 'move');

                    //捕获事件。（该用法，还有个好处，就是防止移动太快导致鼠标跑出被移动元素之外）
                    if (moveObj.get(0).setCapture) {
                        moveObj.get(0).setCapture();
                    }
                    var mouseOldX = e.pageX, mouseOldY = e.pageY;
                    $dragMapContainer.mousemove(function (e2) {

                        var moveX = mouseOldX - e2.pageX;
                        var moveY = mouseOldY - e2.pageY;

                        mouseOldX = e2.pageX;
                        mouseOldY = e2.pageY;

                        moveObjX = moveObj.position().left - moveX; //移动后计算的x坐标
                        moveObjY = moveObj.position().top - moveY; //移动后计算的y坐标

                        //限制拖拽移动时范围 start
                        var limitX1 = -($mapImg.width() - $dragMapContainer.width()); //限制最小的x坐标
                        var limitX2 = 0; //限制最大的x坐标
                        var limitY1 = -($mapImg.height() - $dragMapContainer.height()); //限制最小的y坐标
                        var limitY2 = 0; //限制最大的y坐标

                        if (moveObjX < limitX1) {//最小的x坐标
                            moveObjX = limitX1;
                        }
                        if (moveObjX > limitX2) {//最大的x坐标
                            moveObjX = limitX2;
                        }
                        if (moveObjY < limitY1) {//最小的y坐标
                            moveObjY = limitY1;
                        }
                        if (moveObjY > limitY2) {//最大的y坐标
                            moveObjY = limitY2;
                        }
                        //限制拖拽移动时范围 end
                        moveObj.css({ position: "absolute", left: moveObjX, top: moveObjY });

                    });
                    $("body").mouseup(function () {
                        moveObj.css('cursor', 'default');
                        if (moveObj.get(0).releaseCapture) {
                            moveObj.get(0).releaseCapture();
                        }
                        $dragMapContainer.unbind("mousemove");
                    });
                });
            }
            //地图拖拽事件 end

            //标识物拖拽,进行设定坐标事件 start
            $("#marker").live("mousedown", function (e) {

                var moveObj = $(this); //拖动的对象
                moveObj.css('cursor', 'move');

                //捕获事件。（该用法，还有个好处，就是防止移动太快导致鼠标跑出被移动元素之外）
                if (moveObj.get(0).setCapture) {
                    moveObj.get(0).setCapture();
                }

                $dragMap.unbind("mousedown"); //解除地图mousedown事件
                $dragMapContainer.unbind("mousemove"); //解除地图mousemove事件

                var mouseOldX = e.pageX, mouseOldY = e.pageY;
                $dragMapContainer.mousemove(function (e2) {

                    var moveX = mouseOldX - e2.pageX;
                    var moveY = mouseOldY - e2.pageY;

                    mouseOldX = e2.pageX;
                    mouseOldY = e2.pageY;

                    moveObjX = moveObj.position().left - moveX; //移动后计算的x坐标
                    moveObjY = moveObj.position().top - moveY; //移动后计算的y坐标

                    //限制拖拽移动时范围 start
                    var limitX1 = $dragMapContainer.offset().left - $("#marker").width() / 2; //限制最小的x坐标 ,"不理解是整个宽度"
                    var limitX2 = $dragMapContainer.offset().left + $dragMapContainer.width() - $("#marker").width() / 2; //限制最大的x坐标
                    var limitY1 = $dragMapContainer.offset().top + $("#marker").height() / 2 + 3; //限制最小的y坐标
                    var limitY2 = $dragMapContainer.offset().top + $dragMapContainer.height() - $("#marker").height() + 3; //限制最大的y坐标      

                    if (e2.pageX < limitX1) {//最小的x坐标
                        moveObjX = Math.abs($dragMap.offset().left - $dragMapContainer.offset().left) - $("#marker").width() / 2;
                    }
                    if (e2.pageX >= limitX2) {//最大的x坐标
                        moveObjX = Math.abs($dragMap.offset().left - $dragMapContainer.offset().left) + $dragMapContainer.width() - $("#marker").width() / 2;
                    }
                    if (e2.pageY < limitY1) {//最小的y坐标
                        moveObjY = Math.abs($dragMap.offset().top - $dragMapContainer.offset().top) - $("#marker").height() / 2;
                    }
                    if (e2.pageY >= limitY2) {//最大的y坐标
                        moveObjY = -Math.abs($dragMap.offset().top - $dragMapContainer.offset().top) + $dragMapContainer.height() - $("#marker").height() / 2 - 8;
                    }

                    MoveSetPos(moveObjX, moveObjY);

                });
                $("body").mouseup(function (e3) {
                    moveObj.css('cursor', 'default');
                    if (moveObj.get(0).releaseCapture) {
                        moveObj.get(0).releaseCapture();
                    }

                    //$.CCNFBox.confirm("是否设置座标", SavePos);
                    SavePos(e3);
                    function SavePos(e3) {

                        //设定标识物
                        var markerX = $("#marker").offset().left - $dragMap.offset().left + $("#marker").width() / 2; //未调整的标识物位置相对位置
                        var markerY = $("#marker").offset().top - $dragMap.offset().top + $("#marker").height() - 3; //未调整的标识物位置相对位置

                        var savePosx = (markerX / $mapImg.width()) * mapImgWidthMin - $("#marker").width() / 2; //比例乘以mapImg 宽,最后要保存的位置x
                        var savePosy = (markerY / $mapImg.height()) * mapImgHeightMin - $("#marker").height() + 3; //比例乘以mapImg 长,最后要保存的位置y

                        savePosx = parseFloat(savePosx).toFixed(4); //保留4位小数
                        savePosy = parseFloat(savePosy).toFixed(4); //保留4位小数

                        $hideObject.val(savePosx + "|" + savePosy); //存放最终结果坐标

                        //保存地图坐标
                        SubmitSaveMap();
                    }

                    $("body").unbind("mouseup");
                    BindDragMapEvent(); //重新绑定地图拖动事件
                    $dragMapContainer.unbind("mousemove");

                });
            });
            //标识物拖拽事件,进行设定坐标 end

            //提交保存地图
            var SubmitSaveMap = function () {
                $.ajax({
                    type: "post",
                    url: "/BinTouch/UpdateBinTouchMap",
                    datatype: "json",
                    data: { MapXY: $hideObject.val(), bintouchConfigureID: $hideObject.attr("hidebcId") },
                    beforeSend: function () {

                    },
                    success: function (data, textStatus) {

                        if (data.Result) {
                            //$.CCNFBox.alert("位置设定成功！");
                            $.wBox.close();
                        } else {
                            $.CCNFBox.alert("由于网络比较繁忙，位置设定失败，请重新尝试！");
                        }
                    },
                    error: function () {
                        $.CCNFBox.alert("由于网络比较繁忙，位置设定失败，请重新尝试！");
                    }
                });
            }
        }
    });
})(jQuery)
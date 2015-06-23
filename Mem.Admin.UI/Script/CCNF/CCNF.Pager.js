/* 对象级分页插件 */

(function ($) {
    $.fn.Pager = function (o) {
        var defualts = {
            url: "", //数据请求路径
            pager: [], //分页容器选择器
            container: this, //数据容器选择器
            pageIndex: 1, //当前页
            pageSize: 10, //分页大小
            hoverClass: "hover", //当前页样式
            recordCountId: null,
            param: {}, //附加参数(json格式),如{ type:0 }
            success: function () { } //成功后执行的回调函数
        };

        var options = $.extend(defualts, o);

        LoadData(options);
    }

    //分页实际数据计算。
    function PagerCalculation(pageIndex, pageSize, recordCount) {

        var PageParameter = {
            PageCount: 0,
            StartPageIndex: 0,
            EndPageIndex: 0
        };

        var maxLongPage = 6;

        //计算总页数
        PageParameter.PageCount = (recordCount / pageSize) + (recordCount % pageSize > 0 ? 1 : 0);

        //如果总页数等于0，那么最少也要有1页。
        PageParameter.PageCount = PageParameter.PageCount == 0 ? 1 : parseInt(PageParameter.PageCount);

        //页码起始索引.默认是每6页一轮换,那么如果页码起始索引是3,就意味着终止索引是8。
        var startPagerIndex = 1;

        //页码终止索引。
        var endPagerIndex = startPagerIndex + maxLongPage - 1;

        //如果结束页大于总页数只能等于总页数。
        endPagerIndex = endPagerIndex > PageParameter.PageCount ? PageParameter.PageCount : endPagerIndex;

        if (pageIndex + (maxLongPage / 2) - 1 <= PageParameter.PageCount) {
            endPagerIndex = pageIndex + (maxLongPage / 2) - 1;
        }
        else {
            endPagerIndex = PageParameter.PageCount;
        }

        if (pageIndex - (maxLongPage / 2) + 1 >= 1) {
            startPagerIndex = pageIndex - (maxLongPage / 2) + 1;
        }
        else {
            startPagerIndex = 1;
        }

        if (pageIndex < (maxLongPage / 2)) {
            endPagerIndex = endPagerIndex + (maxLongPage / 2 - pageIndex);
        }

        if (PageParameter.PageCount - pageIndex < maxLongPage / 2) {
            startPagerIndex = startPagerIndex - (maxLongPage / 2 - (PageParameter.PageCount - pageIndex)) + 1;
        }

        if (startPagerIndex < 1) {
            startPagerIndex = 1;
        }

        if (endPagerIndex > PageParameter.PageCount) {
            endPagerIndex = PageParameter.PageCount;
        }

        PageParameter.StartPageIndex = parseInt(startPagerIndex);
        PageParameter.EndPageIndex = parseInt(endPagerIndex);

        return PageParameter;
    }

    //输出HTML。
    function PagerHtml(pageIndex, startPageIndex, endPageIndex, pageCount) {

        var html = "";

        if (pageIndex != 1) {
            html += "<li><a href='javascript:void(0)' class='previous'>上一页</a></li>";

        } else {
            html += "<li class='disabled previous'>上一页</li>";
        }

        for (var i = startPageIndex; i <= endPageIndex; i++) {
            if (i == pageIndex) {
                html += "<li class='hover'>" + i + "</li>";
            }
            else {
                html += "<li><a href='javascript:void(0)' >" + i + "</a></li>";
            }
        }

        if (pageIndex != pageCount) {
            html += "<li><a href='javascript:void(0)' class='next'>下一页</a></li>";

        } else {
            html += "<li class='disabled next'>下一页</li>";
        }

        return html;
    }

    //加载数据。
    function LoadData(options) {

        var dataParam = $.extend({}, {
            "pageIndex": options.pageIndex,
            "pageSize": options.pageSize
        }, options.param);

        $.ajax({
            url: options.url,
            data: dataParam,
            type: "get",
            cache: false,
            success: function (result) {
                //获取总记录数。
                var recordCount = parseInt($(options.container).html(result).find("input:hidden").val());

                if (options.recordCountId != null) {
                    $(options.recordCountId).html(recordCount);
                }

                if (recordCount > 0) {
                    //计算分页的实际数据。
                    var pageParameter = PagerCalculation(options.pageIndex, options.pageSize, recordCount);

                    for (var i = 0; i < options.pager.length; i++) {

                        //var countHtml = "<li><span>共有 {0} 条记录</span></li>".replace("{0}", recordCount);
                        var countHtml = "";

                        $(options.pager[i]).html(countHtml +
                            PagerHtml(options.pageIndex, pageParameter.StartPageIndex, pageParameter.EndPageIndex, pageParameter.EndPageIndex));

                        $(options.pager[i] + " A:not([class='previous']):not([class='next'])").each(function () {
                            $(this).click(
				            function () {
				                options.pageIndex = parseInt($(this).html());
				                LoadData(options);
				            }
			            );
                        });


                        $(options.pager[i] + " A.previous").each(function () {
                            $(this).click(
				            function () {
				                options.pageIndex = options.pageIndex - 1;
				                LoadData(options);
				            }
			            );
                        });

                        $(options.pager[i] + " A.next").each(function () {
                            $(this).click(
				            function () {
				                options.pageIndex = options.pageIndex + 1;
				                LoadData(options);
				            }
			            );
                        });

                    }

                    options.success();
                }
                else {

                }
            }
        });
    }

})(jQuery)
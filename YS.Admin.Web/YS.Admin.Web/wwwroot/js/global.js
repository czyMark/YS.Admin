
/* 模拟select */

$(function () {
    /* 模拟select */
    $(".js_select_show").click(function () {
        var _this = $(this);
        $(".js_option").hide();
        _this.parents(".js_select").find(".js_option").show();
    });
    
    $(".js_option li").click(function () {
        var _this = $(this);
        _this.parents(".js_option").hide();
    });

    $(document).bind('click', function () {
        $('.js_option').hide();
    });

    $('.js_select_show').bind('click', function (e) {
        stopPropagation(e);
    });

    function stopPropagation(e) {
        if (e.stopPropagation)
            e.stopPropagation();
        else
            e.cancelBubble = true;
    }
});


// 调整字体大小 - 开始

$(document).on("click",".font-small",function(event) {
    event.preventDefault();
    $(".article-main p").css({
        "font-size": "16px"
    });
    $(".article-main span").css({
        "font-size": "16px"
    });
    
});
$(document).on("click",".font-medium",function(event) {
    event.preventDefault();
    $(".article-main p").css({
        "font-size": "20px"
    });
    $(".article-main span").css({
        "font-size": "20px"
    });
});
$(document).on("click",".font-large",function(event) {
    event.preventDefault();
    $(".article-main p").css({
        "font-size": "24px"
    });
    $(".article-main span").css({
        "font-size": "24px"
    });
});
$(document).on("click",".fontSize a",function(event) {
    $(".fontSize a").removeClass("selected");
    $(this).addClass("selected");
});


// 调整字体大小 - 结束

/* 返回顶部 */

jQuery(document).ready(function($) {
    if ($(this).scrollTop() == 0) {
        $("#toTop").fadeOut();
    }
    $(window).scroll(function(event) {
        if ($(this).scrollTop() == 0) {
            $("#toTop").fadeOut();
        }
        if ($(this).scrollTop() != 0) {
            $("#toTop").fadeIn();
        }
    });
    $("#toTop").click(function(event) {
        $("html,body").animate({
            scrollTop: "0px"
        }, 666)
    });
});

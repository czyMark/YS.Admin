$(function () {
    //初始化绑定默认的属性
    $.upLoadDefaults = $.upLoadDefaults || {};
    $.upLoadDefaults.property = {
        multiple: false, //是否多文件
        water: false, //是否加水印
        thumbnail: false, //是否生成缩略图
        sendurl: null, //发送地址
        filetypes: "jpg,jpge,png,gif,rar,zip,mp4", //文件类型
        filesize: "204800", //文件大小
        btntext: "浏览...", //上传按钮的文字
        swf: null //SWF上传控件相对地址
    };
    //初始化上传控件
    $.fn.InitUploader = function (p) {
        var fun = function (parentObj) {
            p = $.extend({}, $.upLoadDefaults.property, p || {});
            var btnObj = $('<div class="upload-btn">' + p.btntext + '</div>').appendTo(parentObj);
            //初始化属性
            var GUID = WebUploader.Base.guid(); //当前页面是生成的GUID作为标示
            //console.log(GUID);
            //初始化WebUploader
            var uploader = WebUploader.create({
                auto: true, //自动上传
                swf: p.swf, //SWF路径
                chunked: true, //分片处理大文件
                chunkSize: 3 * 1024 * 1024,
                disableGlobalDnd: true,
                fileNumLimit: 300,
                threads: 1, //上传并发数  

                server: p.sendurl, //上传地址
                pick: {
                    id: btnObj,
                    multiple: p.multiple
                },
                accept: {
                    /*title: 'Images',*/
                    extensions: p.filetypes
                    /*mimeTypes: 'image/*'*/
                },
                //由于Http的无状态特征，在往服务器发送数据过程传递一个进入当前页面是生成的GUID作为标示
                formData: {
                    guid: GUID,
                    'DelFilePath': '' //定义参数
                },
                fileVal: 'Filedata', //上传域的名称
                fileSingleSizeLimit: p.filesize * 1024 //文件大小
            });

            //当validate不通过时，会以派送错误事件的形式通知
            uploader.on('error', function (type) {
                switch (type) {
                    case 'Q_EXCEED_NUM_LIMIT':
                        alert("错误：上传文件数量过多！");
                        break;
                    case 'Q_EXCEED_SIZE_LIMIT':
                        alert("错误：文件总大小超出限制！");
                        break;
                    case 'F_EXCEED_SIZE':
                        alert("错误：文件大小超出限制！");
                        break;
                    case 'Q_TYPE_DENIED':
                        alert("错误：禁止上传该类型文件！");
                        break;
                    case 'F_DUPLICATE':
                        alert("错误：请勿重复上传该文件！");
                        break;
                    default:
                        alert('错误代码：' + type);
                        break;
                }
            });

            //当有文件添加进来的时候
            uploader.on('fileQueued', function (file) {
                //如果是单文件上传，把旧的文件地址传过去
                if (!p.multiple) {
                    uploader.options.formData.DelFilePath = parentObj.prev().children(".upload-path").val();
                }
                //防止重复创建
                if (parentObj.children(".upload-progress").length == 0) {
                    //创建进度条
                    var fileProgressObj = $('<div class="upload-progress"></div>').appendTo(parentObj);
                    var progressText = $('<span class="txt">正在上传，请稍候...</span>').appendTo(fileProgressObj);
                    var progressBar = $('<span class="bar"><b></b></span>').appendTo(fileProgressObj);
                    var progressCancel = $('<a class="close" title="取消上传">关闭</a>').appendTo(fileProgressObj);
                    //绑定点击事件
                    progressCancel.click(function () {
                        uploader.cancelFile(file);
                        fileProgressObj.remove();
                    });
                }
            });
            //文件上传过程中创建进度条实时显示
            uploader.on('uploadProgress', function (file, percentage) {
                var progressObj = parentObj.children(".upload-progress");
                progressObj.children(".txt").html(file.name);
                progressObj.find(".bar b").width(percentage * 100 + "%");
            });
            //当文件上传出错时触发
            uploader.on('uploadError', function (file, reason) {
                uploader.removeFile(file); //从队列中移除
                alert(file.name + "上传失败，错误代码：" + reason);
            });
            //当文件上传成功时触发
            uploader.on('uploadSuccess', function (file, data) {
                console.log(file)
                console.log(data);
                if ((data.chunked)) {
                    var url = "/File/FileCombine";
                    var config = {
                        url: url,
                        type: "post",
                        //dataType: "json",
                        data: { guid: GUID, fileExt: data.f_ext, uploadpath: data.uploadpath, filename: file .name},
                        beforeSend: function () {
                            var progressObj = parentObj.find(".webuploader-pick");
                            progressObj.html("<font color='red'>合成中...</font>");
                          
                            //progressObj.find(".bar b").width(percentage * 100 + "%");
                        },
                        success: function (data) {

                            console.log(parentObj);
                            if (data.Tag == '1') {
                                //如果是单文件上传，则赋值相应的表单
                                var progressObj = parentObj.find(".webuploader-pick");
                                progressObj.html("浏览...");
                                    //parentObj.siblings(".upload-name").val(data.name);
                                parentObj.prev().children(".upload-path").val(data.Data);
                                //var progressObj = parentObj.children(".upload-progress");
                                //progressObj.children(".txt").html("上传成功：" + file.name);
                            }
                            if (data.Tag == '0') {
                                var progressObj = parentObj.prev().children(".upload-progress");
                                progressObj.children(".txt").html(data.Message);
                            }
                            uploader.removeFile(file); //从队列中移除
                        }
                    };
                    $.ajax(config)


                    //$.post("/File/FileCombine", { guid: GUID, fileExt: data.f_ext, uploadpath: data.uploadpath },
                    //    function (data) {
                    //        console.log(data);
                    //        //data = $.parseJSON(data);
                    //        if (data.Tag == '0') {
                    //            var progressObj = parentObj.prev().children(".upload-progress");
                    //            progressObj.children(".txt").html(data.Message);
                    //        }
                    //        if (data.Tag == '1') {
                    //            //如果是单文件上传，则赋值相应的表单
                    //            if (!p.multiple) {
                    //                //parentObj.siblings(".upload-name").val(data.name);
                    //                parentObj.prev().children(".upload-path").val(data.filepath);
                    //                //parentObj.siblings(".upload-size").val(data.size);
                    //            } else {
                    //                addImage(parentObj, data.path, data.thumb);
                    //            }
                    //            var progressObj = parentObj.children(".upload-progress");
                    //            progressObj.children(".txt").html("上传成功：" + file.name);
                    //        }
                    //        uploader.removeFile(file); //从队列中移除
                    //    });
                } else {
                    if (data.Tag == '0') {
                        var progressObj = parentObj.prev().children(".upload-progress");
                        progressObj.children(".txt").html(data.Message);
                    }
                    if (data.Tag == '1') {
                        //如果是单文件上传，则赋值相应的表单
                        if (!p.multiple) {
                            //parentObj.siblings(".upload-name").val(data.name);
                            parentObj.prev().children(".upload-path").val(data.Data);
                            //parentObj.siblings(".upload-size").val(data.size);
                        } else {
                            addImage(parentObj, data.path, data.thumb);
                        }
                        var progressObj = parentObj.children(".upload-progress");
                        progressObj.children(".txt").html("上传成功：" + file.name);
                    }
                    uploader.removeFile(file); //从队列中移除
                }
            });
            //不管成功或者失败，文件上传完成时触发
            uploader.on('uploadComplete', function (file) {
                var progressObj = parentObj.children(".upload-progress");
                progressObj.children(".txt").html("上传完成");
                //如果队列为空，则移除进度条
                if (uploader.getStats().queueNum == 0) {
                    progressObj.remove();
                }
            });
        };
        return $(this).each(function () {
            fun($(this));
        });
    }
});
/*图片相册处理事件
=====================================================*/
//添加图片相册
function addImage(targetObj, originalSrc, thumbSrc) {
    //插入到相册UL里面
    var newLi = $('<li>'
        + '<input type="hidden" name="hid_photo_name" value="0|' + originalSrc + '|' + thumbSrc + '" />'
        + '<input type="hidden" name="hid_photo_remark" value="" />'
        + '<div class="img-box" onclick="setFocusImg(this);">'
        + '<img src="' + thumbSrc + '" bigsrc="' + originalSrc + '" />'
        + '<span class="remark"><i>暂无描述...</i></span>'
        + '</div>'
        + '<a href="javascript:;" onclick="setRemark(this);">描述</a>'
        + '<a href="javascript:;" onclick="delImg(this);">删除</a>'
        + '</li>');
    newLi.appendTo(targetObj.siblings(".photo-list").children("ul"));

    //默认第一个为相册封面
    var focusPhotoObj = targetObj.siblings(".focus-photo");
    if (focusPhotoObj.val() == "") {
        focusPhotoObj.val(thumbSrc);
        newLi.children(".img-box").addClass("selected");
    }
}
//设置相册封面
function setFocusImg(obj) {
    var focusPhotoObj = $(obj).parents(".photo-list").siblings(".focus-photo");
    focusPhotoObj.val($(obj).children("img").eq(0).attr("src"));
    $(obj).parent().siblings().children(".img-box").removeClass("selected");
    $(obj).addClass("selected");
}
//设置图片描述
function setRemark(obj) {
    var parentObj = $(obj); //父对象
    var hidRemarkObj = parentObj.prevAll("input[name='hid_photo_remark']").eq(0); //取得隐藏值
    var d = parent.dialog({
        title: "图片描述",
        content: '<textarea id="ImageRemark" style="margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:300px;height:50px;">' + hidRemarkObj.val() + '</textarea>',
        button: [{
            value: '批量描述',
            callback: function () {
                var remarkObj = $('#ImageRemark', parent.document);
                if (remarkObj.val() == "") {
                    parent.dialog({
                        title: '提示',
                        content: '亲，总该写点什么吧？',
                        okValue: '确定',
                        ok: function () { },
                        onclose: function () {
                            remarkObj.focus();
                        }
                    }).showModal();
                    return false;
                }
                parentObj.parent().parent().find("li input[name='hid_photo_remark']").val(remarkObj.val());
                parentObj.parent().parent().find("li .img-box .remark i").html(remarkObj.val());
            }
        }, {
            value: '单张描述',
            callback: function () {
                var remarkObj = $('#ImageRemark', parent.document);
                if (remarkObj.val() == "") {
                    parent.dialog({
                        title: '提示',
                        content: '亲，总该写点什么吧？',
                        okValue: '确定',
                        ok: function () { },
                        onclose: function () {
                            remarkObj.focus();
                        }
                    }).showModal();
                    return false;
                }
                hidRemarkObj.val(remarkObj.val());
                parentObj.siblings(".img-box").children(".remark").children("i").html(remarkObj.val());
            },
            autofocus: true
        }]
    }).showModal();
}
//删除图片LI节点
function delImg(obj) {
    var parentObj = $(obj).parent().parent();
    var focusPhotoObj = parentObj.parent().siblings(".focus-photo");
    var smallImg = $(obj).siblings(".img-box").children("img").attr("src");
    $(obj).parent().remove(); //删除的LI节点
    //检查是否为封面
    if (focusPhotoObj.val() == smallImg) {
        focusPhotoObj.val("");
        var firtImgBox = parentObj.find("li .img-box").eq(0); //取第一张做为封面
        firtImgBox.addClass("selected");
        focusPhotoObj.val(firtImgBox.children("img").attr("src")); //重新给封面的隐藏域赋值
    }
}
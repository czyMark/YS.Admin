layui.use(['element', 'jquery', 'form', 'layedit', 'flow'], function () {
    var element = layui.element;
    var form = layui.form;
    var $ = layui.jquery;
    var flow = layui.flow;
    var layedit = layui.layedit;
    //留言的编辑器
    var editIndex = layedit.build('remarkEditor', {
        height: 150,
        tool: ['face'],
    });
    //留言的编辑器的验证
    form.verify({
        content: function (value) {
            value = $.trim(layedit.getContent(editIndex));
            if (value == "") return "请输入内容";
            layedit.sync(editIndex);
        },
        replyContent: function (value) {
            if (value == "") return "请输入内容";
        }
    }); 
});
//validatebox验证长度
$.extend($.fn.validatebox.defaults.rules, {
    stuNumLength: {
        validator: function (value, param) {
            return value.length == param[0];
        },
        message: '请输入{0}位'
    }
});

// extend the 'equals' rule
$.extend($.fn.validatebox.defaults.rules, {
    equals: {
        validator: function(value,param){
            return value == $(param[0]).val();
        },
        message: '两次输入不一致'
    },
    className: {
        validator: function (value) {
            var reg = /^[0-9]{2}/;
            return reg.test(value);
        },
        message: '前两位必须为数字'
    }
});

$.extend($.fn.validatebox.defaults.rules, {
    isAfter: {
        validator: function (value, param) {
            var dateA = $.fn.datebox.defaults.parser(value);
            var dateB = $.fn.datebox.defaults.parser($(param[0]).datebox('getValue'));
            return dateA > new Date() && dateA > dateB;
        },
        message: 'The date is not later than today and B'
    },
    isLaterToday: {
        validator: function (value, param) {
            var date = $.fn.datebox.defaults.parser(value);
            return date > new Date();
        },
        message: '日期不能小于当前日期'
    }
});



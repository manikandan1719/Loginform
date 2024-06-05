function AjaxPost(url, data) {
    $('.Apploader').show();
    
    var result = '';
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: url,
        data: data,
        dataType: "json",
        async: false,
        beforeSend: function () {
            $('.Apploader').show();
        },
        success: function (data) {
            result = data;
            setTimeout(function () {
                $('.Apploader').hide();
            }, 50);
        },
        error: function ajaxError(err) {
            result = '';
            setTimeout(function () {
                $('.Apploader').hide();
                result = "TimedOut"
            }, 50);
        }
       
    });
    return result;

}
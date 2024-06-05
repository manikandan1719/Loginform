function autoCompletedData(field,url, data) {
    $('#' + field).autocomplete({
        source: function (request, response) {
            var txtClient = $('#' + field).val();
            var result = AjaxPost(url, data);
            if (result != '') {
                response(result);
            }

        }
    });
}
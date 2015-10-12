$(function	()	{

    $("#search_movie").on("keyup", function () {
        var query = $(this).val();
        if (query.length > 3) {
            $.ajax({
                type: 'post',
                url: 'film/SearchMovie',
                dataType: 'json',
                data: { query: query },
                success: function (r) {
                    if (r.response == 'success') {
                        $(".movies-wrapper").html("<h1>WESTSIDE BITCHES</h1>");
                    }
                }
            });
        }
    });

});
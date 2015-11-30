$(function	()	{

    $("#search_movie").on("keyup", function () {
        var query = $(this).val();
        if (query.length > 3) {
            $.ajax({
                type: 'post',
                url: '/film/SearchMovie',
                dataType: 'json',
                data: { query: query },
                success: function (r) {
                    if (r != null) {
                        showSearchResult(r);
                    }
                }
            });
        }
    });

    $("#search-btn").on("click", function () {
        
            $.ajax({
                type: 'post',
                url: '/film/AdvanceSearchMovie',
                dataType: 'json',
                data: {
                    title: $('#title').val(),
                    actor: $('#actor').val(),
                    realisator: $('#realisator').val(),
                    genre: $('#genre').val(),
                    country: $('#country').val(),
                    language: $('#language').val(),
                    year: $('#year').val()
                },
                success: function (r) {
                    if (r != null) {
                        showSearchResult(r);
                    }
                }
            });
        
    });

    $("#analyze-btn").on("click", function () {
        $.ajax({
            type: 'post',
            url: '/film/AnalyzeData',
            dataType: 'json',
            data: {
                AgeGroup: $('#ageGroupe').val(),
                Province: $('#province').val(),
                DayOfWeek: $('#dayOfWeek').val(),
                Month: $('#month').val()
            },
            success: function (r) {
            }
        });

    });

    function showSearchResult(result) {
        films = $.parseJSON(result);
        container = $('#movie-list');
        container.html('');
        if (result.length > 2) {
            $.each(films, function (i, value) {
                var film = '';
                film += "<li class='col-sm-2'>";
                film += "<a href='/film/showfilm/" + value.IDFILM + "'>";
                film += "<h2 class='film_title'>" + value.TITRE + "</h2>";
                film += "<img class='posters' src='" + value.POSTER + "'>";
                film += "<div class='quick-detail text-white'>";
                film += "<p>" + value.SCENARIO + "</p>";
                film += "</div>";
                film += "</a>";
                film += "</li>";
                container.append(film);
            });
        }
        else {
            container.append("<h1 class='no-film-results'>Aucun film n'a été trouvé!!</h1>");
        }
    }

});
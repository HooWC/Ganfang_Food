(function ($) { // Begin jQuery
    $(function () { // DOM ready
        // If a link has a dropdown, add sub menu toggle.
        $('nav ul li a:not(:only-child)').click(function (e) {
            $(this).siblings('.nav-dropdown').toggle();
            // Close one dropdown when selecting another
            $('.nav-dropdown').not($(this).siblings()).hide();
            e.stopPropagation();
        });
        // Clicking away from dropdown will remove the dropdown class
        $('html').click(function () {
            $('.nav-dropdown').hide();
        });
        // Toggle open and close nav styles on click
        $('#nav-toggle').click(function () {
            $('nav ul').slideToggle();
        });
        // Hamburger to X toggle
        $('#nav-toggle').on('click', function () {
            this.classList.toggle('active');
        });
    }); // end DOM ready
})(jQuery); // end jQuery

$(function () {

    console.log($("#food_type").html())

    $.ajax({
        type: "GET",
        url: `/Ajax/GetMore?type=${$("#food_type").html()}`,
        dataType: "json",
        success: function (data) {
            GetData(data, "con");
        }
    })

    function GetData(data, name) {

        $.each(data, function (x, y) {

            var tag_checking = y.type;
            var tag = [];
            if (tag_checking.includes("|"))
                tag = y.type.split("|");
            else
                tag.push(y.type)

            var type_data = "";
            for (var i = 0; i < tag.length; i++) {
                var tag_name = null;
                if (tag[i] == "家常菜") {
                    tag_name = "homefood"
                } else if (tag[i] == "面包") {
                    tag_name = "breah"
                } else if (tag[i] == "甜点") {
                    tag_name = "sweet"
                } else if (tag[i] == "小吃") {
                    tag_name = "xiaochi"
                } else {
                    tag_name = tag[i]
                }
                type_data += `<li><a href="/Ajax/GetMore?type=${tag_name}" class="watch-btn_tag"><h3>${tag[i]}</h3></a></li>`
            }

            $("#" + name).append(`

            <div class="container">
					<div class="cellphone-container">
						<div class="movie">
							<img src="${y.foodImg}" class="movie-img"></img>
							<div class="text-movie-cont">
								<div class="mr-grid">
									<div class="col1">
										<h2>${y.productFoodName}</h2>
										<ul class="movie-gen">
											${type_data}
										</ul>
									</div>
								</div>
								<div class="mr-grid summary-row">

									<div class="col2">
										<ul class="movie-likes">
											<li><i id="like" class="fa-solid fa-heart"></i>${y.like}</li>
											<li><i id="dislike" class="fa-solid fa-heart-crack"></i>${y.dislike}</li>
										</ul>
									</div>
									<div class="price_style col2">
										<h2>RM ${y.price.toFixed(2)}</h2>
									</div>
								</div>

								<div class="mr-grid_last">
									<div>
										<a href='/CartPage/CartAdd?id=${y.productFoodId}' class="watch-btn" ><h3>订购</h3></a>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>

            `)
        })
    }

})
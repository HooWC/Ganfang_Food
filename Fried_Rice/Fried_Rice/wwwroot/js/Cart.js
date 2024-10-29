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

    $.ajax({
        type: "GET",
        url: "/Ajax/GetCart",
        dataType: "json",
        success: function (data) {
            GetData(data, "cart");
        }
    })

    $.ajax({
        type: "GET",
        url: "/Ajax/GetTotal",
        dataType: "json",
        success: function (data) {
            GetRight(data, "right_side");
        }
    })

    function GetData(data, name) {

        $.each(data, function (x, y) {

            var tag_checking = y.p_type;
            var tag = [];
            if (tag_checking.includes("|"))
                tag = y.p_type.split("|");
            else
                tag.push(y.p_type)

            var type_data = "";
            for (var i = 0; i < tag.length; i++) {
                type_data += `<li><a href="#" class="watch-btn_tag"><h3>${tag[i]}</h3></a></li>`
            }

            $("#" + name).append(`
            <article class="product">
				<header>
					<a class="remove">
						<img src="${y.p_img}" alt="">

						<h3>取消订购</h3>
					</a>
				</header>

				<div class="content">
					<h1>${y.p_name}</h1>
					<ul class="movie-gen">
						${type_data}
					</ul>
				</div>

				<footer class="content">
					<span class="qt-minus" id="${y.c_id}">-</span>
					<span class="qt">${y.c_qua}</span>
					<span class="qt-plus" id="${y.c_id}">+</span>

					<h2 class="full-price">
						RM ${(y.c_qua * y.p_price).toFixed(2)}
					</h2>

					<h2 class="price">
						RM ${y.p_price.toFixed(2)}
					</h2>
				</footer>
			</article>

            `)

        })

        $(".qt-plus").click(function () {

            var c_id = $(this).attr("id")



            $.ajax({
                type: "POST",
                url: "/Ajax/GetCart_plus",
                dataType: "json",
                data: {
                    cart_id: c_id
                },
                success: function (data) {
                    $("#" + name).empty();
                    GetData(data, "cart");
                    da()
                }
            })

            function da() {
                $.ajax({
                    type: "GET",
                    url: "/Ajax/GetTotal",
                    dataType: "json",
                    success: function (data) {
                        $("#right_side").empty();
                        GetRight(data, "right_side");
                    }
                })
            }



        })

        $(".qt-minus").click(function () {

            var c_id = $(this).attr("id")

            $.ajax({
                type: "POST",
                url: "/Ajax/GetCart_minus",
                dataType: "json",
                data: {
                    cart_id: c_id
                },
                success: function (data) {
                    $("#" + name).empty();
                    GetData(data, "cart");
                    da()
                }
            })

            function da() {
                $.ajax({
                    type: "GET",
                    url: "/Ajax/GetTotal",
                    dataType: "json",
                    success: function (data) {
                        $("#right_side").empty();
                        GetRight(data, "right_side");
                    }
                })
            }






        })


    }

    function GetRight(data, name) {
        $("#" + name).append(`
            <div class="title_h1">
				<h1>订单摘要</h1>
			</div>
			<div class="middle_p">
				<div class="date">
					<p>订购 ${data.count}</p>
					<p>${data.date}</p>
				</div>
				<div class="email_input">
					<p>邮件验证</p>
					<input id="buy_email" type="email" placeholder="请输入您的邮件..." required>
				</div>
			</div>
			<div class="toter">
				<div class="toter_money">
					<p>总成本</p>
					<p>RM <span id="total_buy">${data.total.toFixed(2)}<span></p>
				</div>
				<button id="buy_btn">确认</button>
			</div>

        `)

        $("#buy_btn").click(function () {
            console.log($("#buy_email").val())

            $.ajax({
                type: "POST",
                url: "/Ajax/Buy",
                dataType: "json",
                data: {
                    gmail: $("#buy_email").val(),
                    total: $("#total_buy").html()
                },
                success: function (data) {
                    if (data == "email") {
                        $("#buy_email").css("border-color", "red");
                    } else if (data == true) {
                        window.location.href = `/Home/Home`;
                    } else {

                    }
                }
            })
        })

    }


})








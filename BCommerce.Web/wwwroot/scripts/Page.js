function UpdateNotificationBuble() {

    $.ajax({
        url: '/Basket/GetBasketNotificationCount',
        type: 'GET',
        success: function (response) {
            if (response.isSuccess) {
                $("#basketNotificationCountSpan").html(response.data);
                $("#basketNotificationCountSpan").show();
            }
        },
        error: function (e) {


        },
        complete: function () {
        }
    });
}

function GetUserBasket() {

    showBasket();

    $("#basketCart").html('<div id="basketLoader" style="text-align:center;padding:10px;"><div uk-spinner="ratio: 1"></div></div>');


    $.ajax({
        url: '/Basket/GetUserBasket',
        type: 'GET',
        //data: {

        //}
        success: function (data) {
            //işlem basarılı olursa bu fonksiyon calısacak
            setTimeout(function () {

                $("#basketCart").html(data);

            }, 400);


            //document.getElementById("basketCart").innerHTML = data;

        },
        error: function (e) {
            //bir hata alınırsa burası calısacak
            ToasError("İşlem yapılamadı");
            hideBasket();

        },
        complete: function () {
            //hatada alsa basarılıda olsa kesın bıtım anı burasıdır.
        }
    });



}

function addToCart(productId) {

    $.ajax({
        url: '/Basket/AddItem',
        type: 'POST',
        data: {
            productId: productId
        },
        success: function (data) {

            GetUserBasket();
            UpdateNotificationBuble();
        },
        error: function (e) {


        },
        complete: function () {
        }
    });


}


function RemoveBasketItem(itemid) {
    SetBasketItemNewCount(itemid, 0);
}




function SetBasketItemNewCount(itemid, newcount) {
    $.ajax({
        url: '/Basket/SetItemCount',
        type: 'POST',
        data: {
            itemid: itemid,
            newcount: newcount
        },
        success: function (response) {

            if (response.isSuccess) {
                GetUserBasket();
                UpdateNotificationBuble();
            }
        },
        error: function (e) {


        },
        complete: function () {
        }
    });

}


function AddFavorite(element, productid, isadd) {
    $.ajax({
        url: '/Product/AddToFavorite',
        type: 'POST',
        data: {
            productid: productid,
            isadd: isadd
        },
        success: function (response) {
                $(element).replaceWith(response);        
        },
        error: function (e) {

            console.log(e);
        },
        complete: function () {
        }
    });
}



function ToasError(errorMeseage) {

    Toastify({
        text: errorMeseage,
        className: "error",
        style: {
            background: "linear-gradient(to right, #00b09b, #96c93d)",
        }
    }).showToast();
}
function ToasInfo(infoMeseage) {

    Toastify({
        text: infoMeseage,
        className: "info",
        style: {
            background: "linear-gradient(to right, #00b09b, #96c93d)",
        }
    }).showToast();
}

function showBasket() {


    UIkit.offcanvas(document.getElementById("cart-offcanvas")).show();

}

function showBasket() {


    UIkit.offcanvas(document.getElementById("cart-offcanvas")).show();

}
function hideBasket() {


    UIkit.offcanvas(document.getElementById("cart-offcanvas")).hide();

}
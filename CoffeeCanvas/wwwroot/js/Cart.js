$(document).ready(function () {

    $('#cartInfo').click(function () {
        console.log("Click");
    });
    

    
   
    $('.coffee-add-cart').click(function () {
        var menuId = $(this).data("menuid");

        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: { menuId: menuId },
            success: function (data) {
                if (data.success) {
                    
                    
                    Toastify({

                        text: "Added to cart",

                        duration: 3000,

                        close: true,
                        style: {
                            background: "linear-gradient(to right, #8B4513, #D2691E)", 
                            color: "#fff", 
                            fontWeight: "bold",
                            fontFamily: "Arial, sans-serif", 
                            boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)", 
                        }

                    }).showToast();

                    
                    updateCartItemCount();
                } else {
                    alert("Unsuccessful implementation");
                }
            },
            error: function () {
                alert("Implementation not successful.");
            }
        });
    });

    
   


   
    


    
    $('.quantity-button').click(function () {
        var menuId = $(this).data("menuid");
        var quantityElement = $(this).siblings("span");
        var newQuantity = parseInt(quantityElement.text());

        if ($(this).data("operation") === "add") {
            newQuantity++;
        } else if ($(this).data("operation") === "subtract" && newQuantity >1){
            newQuantity--;
        }

        
        quantityElement.text(newQuantity);

        
        updateCartQuantity(menuId, newQuantity);
    });

    $('#place-order-button').click(function () {

        window.location.href = "/Order/Index";
    });



});




function updateCartQuantity(menuId, newQuantity) {
    if (menuId && newQuantity >= 0) {
        $.ajax({
            type: "POST",
            url: "/Cart/UpdateCartItemQuantity",
            data: { menuId: menuId, newQuantity: newQuantity },
            success: function (data) {
                if (data.success) {
                    

                    var quantityElement = $('.cart-table').find("button[data-menuid='" + menuId + "']").siblings("span");
                    quantityElement.text(newQuantity);
                    $('#total-amount').text(data.total_price);
                    updateCartItemCount();
                    Toastify({

                        text: "Cart updated successfully",

                        duration: 3000,

                        close: true,
                        style: {
                            background: "linear-gradient(to right, #8B4513, #D2691E)",
                            color: "#fff",
                            fontWeight: "bold",
                            fontFamily: "Arial, sans-serif",
                            boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
                        }

                    }).showToast();


                } else {
                    
                    
                    alert("Unsuccessful implementation");
                }
            },
            error: function () {
                alert("Implementation not successful.");
            }
        });
    } else {
        alert("Invalid menuId or newQuantity");
    }
}




function updateCartItemCount() {

    $.ajax({
        type: "POST",
        url: "/Cart/GetCartItemCount",
        success: function (data) {
            if (data.count !== undefined) {
                $('#cartInfo').text(data.count);
            }
        },
        error: function () {
            alert("Implementation not successful!");
        }
    });
}
function clearCartAndBackToMenu() {
    $.ajax({
        type: "POST",
        url: "/Cart/ClearCart",
        success: function() {
            window.location.href = "/Menu";
        },
        error: function() {
            alert("Failed to clear the cart.");
        }
    });
}

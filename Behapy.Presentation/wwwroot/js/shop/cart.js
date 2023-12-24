function addToCart(productId) {
    const amount = $('#item-amount').val();
    
    $.ajax({
        url: `/Cart/Add?productId=${productId}&amount=${amount}`,
        type: 'POST',
        contentType: 'application/json',
        success: function () {
            bootstrap.showToast({body: 'Đã thêm sản phẩm vào giỏ hàng!'});
            updateMiniCart();
        },
    });
}

function removeFromCart(productId) {
    $.ajax({
        url: `/Cart/Delete?productId=${productId}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function () {
            bootstrap.showToast({body: 'Đã xóa sản phẩm khỏi giỏ hàng'});
            updateMiniCart(true);
        },
    });
}

function updateMiniCart(updateCartPage) {
    $.ajax({
        url: `/Cart/GetAll/`,
        type: 'GET',
        contentType: 'application/json',
        success: function (res) {
            const listWrapper = $('.minicart-list').html('');
            const cartPageWrapper = $('#cart-table-content');
            let quantity = 0;
            let total = 0;

            if (updateCartPage) {
                cartPageWrapper.html('');
            }

            for (let i = 0; i < res.length; i++) {
                const item = res[i];
                quantity += item.amount;
                total += item.amount * item.product.price;

                const itemHtml = $('<li class="minicart-product"></li>');
                const removeBtn = $(`
                  <a onclick="removeFromCart(${item.productId})" class="product-item_remove" role="button">
                    <i class="pe-7s-close"></i>
                  </a>
                `);
                const img = `
                  <a href="/Products/Details/${item.product.id}" class="product-item_img">
                    <img class="img-full" src="${item.product.imageUrl}" alt="${item.product.name}"/>
                  </a>
                `;
                const content = `
                  <div class="product-item_content">
                    <a href="/Products/Details/${item.product.id}" class="product-item_title">${item.product.name}</a>
                    <span class="product-item_quantity">${item.amount} x ${item.product.price}</span>
                  </div>
                `;

                itemHtml.append(removeBtn)
                    .append(img)
                    .append(content);

                listWrapper.append(itemHtml);

                if (!updateCartPage) continue;

                // Update cart page
                const itemHtmlPage = $('<tr></tr>');
                const idx = $('<td class="index"></td>').html(i + 1);
                const imgPage = `
                    <td class="product-thumbnail">
                      <a href="/Products/Details/${item.product.id}">
                        <img src="${item.product.imageUrl}" alt="${item.product.name}"/>
                      </a>
                    </td>
                `;
                const productName = `
                    <td class="product-name">
                      <a href="/Products/Details/${item.product.id}">${item.product.name}</a>
                    </td>
                `;
                const price = $(`
                    <td class="product-price">
                        <span class="amount">${item.product.price}</span>
                    </td>
                `);
                const amount = $(`
                    <td class="quantity">
                        <div class="cart-plus-minus">
                            <input class="cart-plus-minus-box" value="${item.amount}" type="text"/>
                        </div>
                    </td>
                `);
                const subtotal = $(`
                    <td class="subtotal">
                        <span class="amount">${item.product.price * item.product.amount}</span>
                    </td>
                `);
                const removeBtnPage = $(`
                    <td class="product_remove">
                        <a onclick="removeFromCart(${item.productId})" role="button">
                            <i class="pe-7s-close" title="Remove"></i>
                        </a>
                    </td>
                `);

                itemHtmlPage.append(idx)
                    .append(imgPage)
                    .append(productName)
                    .append(price)
                    .append(amount)
                    .append(subtotal)
                    .append(removeBtnPage);

                cartPageWrapper.append(itemHtmlPage);
            }

            $('#mini-cart-quantity').html(quantity);
            $('#mini-cart-total').html(total);
            if (updateCartPage) {
                $('#cart-subtotal').html(total);
                $('#cart-total').html(total);

                $('.cart-plus-minus').append(
                    '<div class="dec qtybutton"><i class="fa fa-minus"></i></div><div class="inc qtybutton"><i class="fa fa-plus"></i></div>'
                );
                $('.qtybutton').on('click', function () {
                    const $button = $(this);
                    const oldValue = $button.parent().find('input').val();
                    let newVal = 1;

                    if ($button.hasClass('inc')) {
                        newVal = parseFloat(oldValue) + 1;
                    } else if (oldValue > 1) {
                        newVal = parseFloat(oldValue) - 1;
                    }

                    $button.parent().find('input').val(newVal);
                });
            }
        },
    });
}
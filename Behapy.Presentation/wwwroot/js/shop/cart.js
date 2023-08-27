function addToCart(productId) {
    $.ajax({
        url: `/Cart/Add?productId=${productId}`,
        type: 'POST',
        contentType: 'application/json',
        success: function () {
            bootstrap.showToast({
                body: 'Đã thêm sản phẩm vào giỏ hàng!',
                zIndex: 999
            });
            updateMiniCart();
        },
    });
}

function updateMiniCart() {
    $.ajax({
        url: `/Cart/GetAll/`,
        type: 'GET',
        contentType: 'application/json',
        success: function (res) {
            const listWrapper = $('.minicart-list').html('');

            for (const item of res) {
                const itemHtml = $('<li class="minicart-product"></li>');
                const removeBtn = $(`
              <a class="product-item_remove" href="#">
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
            }
        },
    });
}
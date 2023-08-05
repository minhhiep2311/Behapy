// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function bindUploadEventToInputFile(selector, progressSelector, token, signature, expire, folder) {
    const imagekit = new ImageKit({
        publicKey: "public_PyR3NGCbBShSqL3S67epH+Nn+dA=",
        urlEndpoint: "https://ik.imagekit.io/snowflower",
    });
    const customXHR = new XMLHttpRequest();
    const progressBar = $(progressSelector).find('.progress-bar');

    $(selector).change(async function () {
        var input = this;
        console.log(input.files[0]);

        const fileSize = input.files[0].size;

        function customXHRCallBack(e) {
            if (e.loaded <= fileSize) {
                var percent = Math.round(e.loaded / fileSize * 100);
                progressBar.css('width', `${percent}%`);
                progressBar.attr('aria-valuenow', percent);
            }

            if (e.loaded == e.total) {
                progressBar.css('width', '100%');
                progressBar.attr('aria-valuenow', '100');
            }
        }

        customXHR.upload.addEventListener('progress', customXHRCallBack);

        $(progressSelector).attr('data-status', 1);

        try {
            const result = await imagekit.upload({
                xhr: customXHR,
                file: input.files[0],
                fileName: input.files[0].name,
                folder: `Behapy/${folder}`,
                token,
                signature,
                expire,
            });

            console.log(result);

            $(input).parent().find('#ImageUrl').val(result.url);
            $(progressSelector).find('.progress-bar').text('Uploaded file');
            $(progressSelector).attr('data-status', 2);
            customXHR.upload.removeEventListener('progress', customXHRCallBack);

        } catch (e) {
            console.log(e)
        }
    })
}
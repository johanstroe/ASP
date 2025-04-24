export function setupWysiwyg() {
    if (typeof tinymce !== "undefined") {
        tinymce.init({
            selector: 'textarea.wysiwyg',
            height: 200,
            menubar: false,
            plugins: 'lists link image table code',
            toolbar: 'bold italic underline | alignleft aligncenter alignright | bullist numlist | link',
            branding: false,

            toolbar_location: 'bottom',
            elementpath: false,
            statusbar: false,

            content_style: `
                body {
                    font-family: 'Nunito', sans-serif;
                    font-size: 14px;
                    cursor: text;
                }
            `
        });
    }
}

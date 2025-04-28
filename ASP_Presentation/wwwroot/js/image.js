//Det mesta i min image js är CHATGPT4o genererat

export function setupImage(previewSize = 150) {
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('.image-preview');

        previewer.addEventListener('click', () => fileInput.click());

        fileInput.addEventListener('change', async ({ target: { files } }) => {
            if (files[0]) await processImageFromFile(files[0], imagePreview, previewer, previewSize);
        });
    });
}

async function processImageFromFile(file, imagePreview, previewer, previewSize = 150) {
    try {
        const reader = new FileReader();
        reader.onload = async () => {
            const img = await loadImage(reader.result);
            drawImageToCanvas(img, imagePreview, previewer, previewSize);
        };
        reader.readAsDataURL(file);
    } catch (err) {
        console.error("Fel vid filhantering:", err);
    }
}

async function loadImage(src) {
    return new Promise((resolve, reject) => {
        const img = new Image();
        img.crossOrigin = "anonymous";
        img.onload = () => resolve(img);
        img.onerror = () => reject("Kunde inte ladda bilden.");
        img.src = src;
    });
}

function drawImageToCanvas(img, imagePreview, previewer, previewSize = 150) {
    const canvas = document.createElement("canvas");
    canvas.width = canvas.height = previewSize;

    const ctx = canvas.getContext("2d");
    const scale = Math.min(previewSize / img.width, previewSize / img.height);
    const scaledWidth = img.width * scale;
    const scaledHeight = img.height * scale;
    const x = (previewSize - scaledWidth) / 2;
    const y = (previewSize - scaledHeight) / 2;

    ctx.drawImage(img, x, y, scaledWidth, scaledHeight);
    imagePreview.src = canvas.toDataURL("image/jpeg");
    previewer.classList.add("selected");
}

export async function processImageFromUrl(url, imagePreview, previewer, previewSize = 150) {
    try {
        const img = await loadImage(url);
        drawImageToCanvas(img, imagePreview, previewer, previewSize);
    } catch (err) {
        console.error("Fel vid laddning av bild från URL:", err);
    }
}

export function setPreviewImageFromDataAttribute(editButton, modalSelector = "#editProjectModal") {
    const modal = document.querySelector(modalSelector);
    if (!modal || !editButton) return;

    const imagePreview = modal.querySelector('.image-preview');
    const previewer = modal.querySelector('.image-previewer');
    const imageSrc = editButton.dataset.image;

    if (imagePreview && previewer)
    {
        if (!imageSrc || imageSrc.includes("/Images/Projecticon.svg")) {
            imagePreview.src = "/Images/Projecticon.svg";
            previewer.classList.remove("selected");
        } else {
            processImageFromUrl(imageSrc, imagePreview, previewer)
        }
    }
}

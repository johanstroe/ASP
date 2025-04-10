export function setupImage(previewSize = 150) {
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', async ({ target: { files } }) => {
            const file = files[0]
            if (file) {
                await processImage(file, imagePreview, previewer, previewSize)
            }
        })
    })
}

async function loadImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()

        reader.onerror = () => reject(new Error("Failed to upload file"))
        reader.onload = (e) => {
            const img = new Image()
            img.onerror = () => reject(new Error("Failed to load image"))
            img.onload = () => resolve(img)
            img.src = e.target.result
        }

        reader.readAsDataURL(file)
    })
}

async function processImage(file, imagePreview, previewer, previewSize = 150) {
    try {
        const img = await loadImage(file)
        const canvas = document.createElement('canvas')
        canvas.width = previewSize
        canvas.height = previewSize

        const ctx = canvas.getContext('2d')
        ctx.drawImage(img, 0, 0, previewSize, previewSize)

        imagePreview.src = canvas.toDataURL('image/jpeg')
        previewer.classList.add('selected')
    } catch (error) {
        console.error('Fel vid bildförhandsvisning:', error)
    }
}

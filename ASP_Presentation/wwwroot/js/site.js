import { setupModal } from './modal.js'
import { setupForm } from './form-handler.js'
import { setupImage } from './image.js'

document.addEventListener('DOMContentLoaded', () => {
    setupModal()
    setupForm()
    setupImage()
})

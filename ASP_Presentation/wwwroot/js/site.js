import { setupModal } from './modal.js'
import { setupForm } from './form-handler.js'
import { setupImage } from './image.js'
import { status } from './status.js'

document.addEventListener('DOMContentLoaded', () => {
    setupModal()
    setupForm()
    setupImage()
    status()

   
        const dropdownButtons = document.querySelectorAll('[data-type="dropdown"]')

        dropdownButtons.forEach(button => {
            const targetSelector = button.getAttribute('data-target')
            const target = document.querySelector(targetSelector)

            button.addEventListener('click', () => {
                target?.classList.toggle('show')
            })

            // Klick utanför stänger dropdownen
            document.addEventListener('click', (e) => {
                if (!button.contains(e.target) && !target.contains(e.target)) {
                    target.classList.remove('show')
                }
            })
        })
    })





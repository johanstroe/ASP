import { setupModal } from './modal.js'
import { setupForm } from './form-handler.js'
import { setupImage } from './image.js'
import { setupFormattedDateDisplay } from './calendar.js'





document.addEventListener('DOMContentLoaded', () => {
    setupModal();
    setupForm();
    setupImage();
    setupFormattedDateDisplay('StartDate', 'StartDateFormatted');

    const dropdownButtons = document.querySelectorAll('[data-type="dropdown"]');

    dropdownButtons.forEach(button => {
        const targetSelector = button.getAttribute('data-target');
        const target = document.querySelector(targetSelector);

        if (!target) return;

        button.addEventListener('click', (e) => {
            e.stopPropagation(); // förhindra att den direkt stänger sig själv
            target.classList.toggle('show');
        });

        // Klick utanför => stäng dropdown
        document.addEventListener('click', (e) => {
            if (!button.contains(e.target) && !target.contains(e.target)) {
                target.classList.remove('show');
            }
        });
    });
});







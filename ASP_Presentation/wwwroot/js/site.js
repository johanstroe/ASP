import { setupModal } from './modal.js'
import { setupForm } from './form-handler.js'
import { setupImage, setPreviewImageFromDataAttribute, processImageFromUrl } from './image.js'
import { setupFormattedDateDisplay } from './calendar.js'
import { setupStatusFilter } from './status.js';
import { setupWysiwyg } from './setupWysiwyg.js'







document.addEventListener('DOMContentLoaded', () => {
    setupModal();
    setupForm();
    setupImage();
    setupFormattedDateDisplay('StartDate', 'StartDateFormatted');
    setupStatusFilter();
    setupWysiwyg();

    // Visa befintlig fil i editproject
    document.querySelectorAll(".options-button").forEach(editButton => {
        editButton.addEventListener("click", () => {
            setPreviewImageFromDataAttribute(editButton, "#editProjectModal");

            const modal = document.querySelector("#editProjectModal");
            const input = modal.querySelector('input[type="file"]');
            if (input) input.value = ""; // Töm eventuell gammal fil
        });
    });
    

    const dropdownButtons = document.querySelectorAll('[data-type="dropdown"]');

    dropdownButtons.forEach(button => {
        const targetSelector = button.getAttribute('data-target');
        const target = document.querySelector(targetSelector);

        if (!target) return;

        button.addEventListener('click', (e) => {
            e.stopPropagation(); 
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









import { setupModal } from './modal.js'
import { setupForm, setupProjectActions } from './form-handler.js'
import { setupImage, setPreviewImageFromDataAttribute, processImageFromUrl } from './image.js'
import { setupFormattedDateDisplay } from './calendar.js'
import { setupStatusFilter } from './status.js';
import { setupWysiwyg } from './setupWysiwyg.js'
import { setupDarkMode } from './darkmode.js'







document.addEventListener('DOMContentLoaded', () => {
    setupModal();
    setupForm();
    setupImage();
    setupFormattedDateDisplay('StartDate', 'StartDateFormatted');
    setupStatusFilter();
    setupWysiwyg();
    setupProjectActions();
    setupDarkMode();

    // Visa befintlig fil i editproject
    document.querySelectorAll(".options-button").forEach(editButton => {
        editButton.addEventListener("click", () => {
            setPreviewImageFromDataAttribute(editButton, "#editProjectModal");

            const modal = document.querySelector("#editProjectModal");
            const input = modal.querySelector('input[type="file"]');
            if (input) input.value = "";

            const existingImageInput = modal.querySelector('input[name="ExistingImage"]');
            if (existingImageInput) {
                existingImageInput.value = editButton.dataset.existingimage || "";
            }

            const previewImage = modal.querySelector(".image-preview");
            if (previewImage) {
                previewImage.src = editButton.dataset.image || "/Images/Projecticon.svg";
            }

            const description = editButton.dataset.description;
            const editorInstance = tinymce.get("EditProject_Description");

            if (editorInstance) {
                editorInstance.setContent(description || "");
            } else {
                console.warn("Error");
            }
        });
    });

    

    // Dropdown

    document.querySelectorAll('[data-type="dropdown"]').forEach(button => {
        button.addEventListener('click', (e) => {
            e.stopPropagation(); 
            const targetSelector = button.getAttribute('data-target');
            const target = document.querySelector(targetSelector);
            if (target) {
                document.querySelectorAll('.dropdown-menu.show').forEach(menu => menu.classList.remove('show'));
                target.classList.toggle('show');
            }
        });
    });

    // Klick utanför dropdown => stäng

    document.addEventListener('click', () => {
        document.querySelectorAll('.dropdown-menu.show').forEach(menu => {
            menu.classList.remove('show');
        });
    });
});









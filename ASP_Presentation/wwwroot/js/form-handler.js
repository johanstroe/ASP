﻿export function setupForm() {
    const forms = document.querySelectorAll('form.ajaxForm');

    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            clearErrorMessages(form);

            if (typeof tinymce !== "undefined") {
                tinymce.triggerSave();
            }

            const formData = new FormData(form);

            try {
                const res = await fetch(form.action, {
                    method: 'POST',
                    body: formData
                });

                const data = await res.json();

                if (res.ok) {
                    console.log("Response från servern:", data);

                    const modal = form.closest('.modal');
                    if (modal) modal.style.display = 'none';

                    if (data.success && data.redirectUrl) {
                        window.location.href = data.redirectUrl;
                    } else {
                        window.location.reload();
                    }
                } else if (res.status === 400 && data.errors) {
                    Object.keys(data.errors).forEach(key => {
                        addErrorMessage(form, key, data.errors[key].join('\n'));
                    });
                }
            } catch (error) {
                console.error('Ett fel uppstod vid formulärsubmit:', error);
            }
        });
    });
}

// Funktion för att ta bort tidigare felmeddelanden
function clearErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error');
    });

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = '';
        span.classList.remove('field-validation-error');
    });
}

// Funktion för att visa nya felmeddelanden
function addErrorMessage(form, key, errorMessage) {
    const input = form.querySelector(`[name="${key}"]`);
    if (input) {
        input.classList.add('input-validation-error');
    }

    const span = form.querySelector(`[data-valmsg-for="${key}"]`);
    if (span) {
        span.innerText = errorMessage;
        span.classList.add('field-validation-error');
    }
}

export function setupProjectActions() {
    document.addEventListener("click", function (e) {
        const deleteButton = e.target.closest(".dropdown-item.delete");
        if (!deleteButton) return;

        const id = deleteButton.dataset.id;
        if (!id) return;

        if (confirm("Vill du verkligen ta bort projektet?")) {
            fetch("/projects", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(id)
            })
                .then(response => {
                    if (response.ok) {
                        deleteButton.closest(".project-card")?.remove();
                    } else {
                        alert("Det gick inte att ta bort projektet.");
                    }
                });
        }
    });
}

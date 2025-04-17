export function setupFormattedDateDisplay(dateInputId, formattedInputId) {
    const dateInput = document.getElementById(dateInputId);
    const formattedInput = document.getElementById(formattedInputId);

    if (!dateInput || !formattedInput) return;

    function formatDateString(dateStr) {
        if (!dateStr) return '';
        const date = new Date(dateStr);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }

    const updateFormattedDate = () => {
        formattedInput.value = formatDateString(dateInput.value);
    };

    dateInput.addEventListener('change', updateFormattedDate);

    // Visa direkt om ett värde finns
    if (dateInput.value) {
        updateFormattedDate();
    }
}

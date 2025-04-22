
export const statusOptions = [
    { id: 1, name: 'Pågående', key: 'ongoing' },
    { id: 2, name: 'Ej startad', key: 'notstarted' },
    { id: 3, name: 'Avslutat', key: 'completed' }
];

export function setupStatusFilter() {
    const buttons = document.querySelectorAll(".filter-btn");
    const cards = document.querySelectorAll("[data-project-status]");

    if (buttons.length === 0 || cards.length === 0) return;

    buttons.forEach(button => {
        button.addEventListener("click", () => {
            buttons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            const selected = button.dataset.status;

            cards.forEach(card => {
                const status = card.dataset.projectStatus;
                if (selected === "all" || status === selected) {
                    card.style.display = "block";
                } else {
                    card.style.display = "none";
                }
            });
        });
    });
}

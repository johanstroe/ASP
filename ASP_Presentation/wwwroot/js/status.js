export const statusOptions = [
    { id: 1, name: 'Pågående', key: 'ongoing' },
    { id: 2, name: 'Ej startad', key: 'notstarted' },
    { id: 3, name: 'Avslutat', key: 'completed' }
];


//Genererat av chatgpt4o
export function setupStatusFilter() {
    const buttons = document.querySelectorAll(".filter-btn");
    const cards = document.querySelectorAll("[data-project-status]");

    if (buttons.length === 0 || cards.length === 0) return;

    buttons.forEach(button => {
        button.addEventListener("click", () => {
            buttons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            const selected = button.dataset.status;
            const now = new Date();

            cards.forEach(card => {
                const status = card.dataset.projectStatus;
                const endDateAttr = card.dataset.projectEnddate;
                const endDate = endDateAttr ? new Date(endDateAttr) : null;

                let show = false;

                if (selected === "all") {
                    show = true;
                }
                else if (selected === "expired") {
                    show = endDate && endDate < now && status !== "completed";
                } else {
                    show = status === selected;
                }

                card.style.display = show ? "flex" : "none";
            });
        });
    });

    function updateCounts() {
        const all = cards.length;
        const counts = {
            all: all,
            ongoing: 0,
            notstarted: 0,
            completed: 0,
            expired: 0
        };

        const now = new Date();

        cards.forEach(card => {
            const status = card.dataset.projectStatus;
            const endDateAttr = card.dataset.projectEnddate;
            const endDate = endDateAttr ? new Date(endDateAttr) : null;

            if (status === "ongoing") counts.ongoing++;
            else if (status === "notstarted") counts.notstarted++;
            else if (status === "completed") counts.completed++;

            if (endDate && endDate < now && status !== "completed") {
                counts.expired++;
            }
        });

        buttons.forEach(button => {
            const status = button.dataset.status;
            const span = button.querySelector(".count");
            if (span && counts[status] !== undefined) {
                span.textContent = `[${counts[status]}]`;
            }
        });
    }
    updateCounts(); 
}



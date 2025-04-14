
//SetupModal kod genererad av chatgpt4o
export function setupModal() {
    
    const modalButtons = document.querySelectorAll('[data-modal="true"]')

    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal) {
                const form = modal.querySelector('form')

               
                const fields = {
                    Id: button.dataset.id,
                    ProjectName: button.dataset.projectname,
                    Description: button.dataset.description,
                    StartDate: button.dataset.startdate,
                    EndDate: button.dataset.enddate,
                    Budget: button.dataset.budget,
                    ClientId: button.dataset.clientid,
                    MemberId: button.dataset.memberid,
                    StatusId: button.dataset.statusid
                }

                const statusOptions = [
                    { id: 1, name: 'Pågående' },
                    { id: 2, name: 'Ej startad' },
                    { id: 3, name: 'Avslutat' }
                ];

                if (form) {
                    // Fyll alla inputs med rätt data
                    Object.entries(fields).forEach(([key, value]) => {
                        const input = form.querySelector(`[name="${key}"]`)
                        if (input && value !== undefined) {
                            input.value = value
                        }
                    })

                    // Fyll status-dropdown
                    const statusSelect = form.querySelector('select[name="StatusId"]');
                    if (statusSelect) {
                        statusSelect.innerHTML = '';

                        statusOptions.forEach(option => {
                            const opt = document.createElement('option');
                            opt.value = option.id;
                            opt.textContent = option.name;
                            statusSelect.appendChild(opt);
                        });

                        // Sätt förvalt värde
                        statusSelect.value = button.dataset.statusid;
                    }
                }

                modal.style.display = 'flex'
            }
        })
    })

    // Hantera stängning av modaler
    const closeButtons = document.querySelectorAll('[data-close="true"]')

    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none'

                // Rensa formulärinnehåll
                modal.querySelectorAll('form').forEach(form => {
                    form.reset()

                    const imagePreview = form.querySelector('.image-preview')
                    if (imagePreview) imagePreview.src = ''

                    const imagePreviewer = form.querySelector('.image-previewer')
                    if (imagePreviewer) imagePreviewer.classList.remove('selected')
                })
            }
        })
    })
}

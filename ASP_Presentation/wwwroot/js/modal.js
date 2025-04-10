export function setupModal() {
    // Hantera öppning av modaler
    const modalButtons = document.querySelectorAll('[data-modal="true"]')

    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal) {
                const form = modal.querySelector('form')

                // Fält som ska fyllas i automatiskt
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

                // Fyll i fält om formulär hittades
                if (form) {
                    Object.entries(fields).forEach(([key, value]) => {
                        const input = form.querySelector(`[name="${key}"]`)
                        if (input && value !== undefined) {
                            input.value = value
                        }
                    })
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

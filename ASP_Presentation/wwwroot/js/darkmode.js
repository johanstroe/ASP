export function setupDarkMode() {
    const html = document.documentElement;
    const toggle = document.getElementById("darkModeToggle");

    // Sätt rätt tema när sidan laddas
    const saved = localStorage.getItem("theme");
    const isDark = saved === "dark";

    html.setAttribute("data-theme", isDark ? "dark" : "light");
    if (toggle) toggle.checked = isDark;

    if (toggle) {
        toggle.addEventListener("change", () => {
            const newTheme = toggle.checked ? "dark" : "light";
            html.setAttribute("data-theme", newTheme);
            localStorage.setItem("theme", newTheme);
        });
    }
}

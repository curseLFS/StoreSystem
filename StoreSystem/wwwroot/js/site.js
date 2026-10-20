
// Toggle Menu
function toggleMenu(menuId, element) {
    const currentMenu = document.getElementById(menuId);
    const currentArrow  = element.querySelector('.menu-arrow');

        // Close all menus
    document.querySelectorAll('.submenu-container').forEach(menu => {
        if (menu.id !== menuId) {
            menu.classList.remove('show');
        }
    });

    // Reset all arrows
    document.querySelectorAll('.menu-arrow').forEach(arrow => {
        if (arrow !== currentArrow) {
            arrow.classList.remove('rotate');
        }
    });

    // Toggle clicked menus
    currentMenu.classList.toggle('show');
    currentArrow.classList.toggle('rotate');
}



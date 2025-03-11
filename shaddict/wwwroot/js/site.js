// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    console.log("Document ready, initializing sidebar toggle...");
    
    // Toggle sidebar - simplified and more robust implementation
    $(document).on('click', '#sidebarCollapse', function(e) {
        console.log("Sidebar toggle button clicked");
        e.preventDefault();
        $('#sidebar').toggleClass('active');
        
        // Add a visual feedback for the button
        $(this).toggleClass('active');
        
        // Update button text based on sidebar state
        if ($('#sidebar').hasClass('active')) {
            $(this).find('span').text('Show Sidebar');
        } else {
            $(this).find('span').text('Hide Sidebar');
        }
        
        // Log for debugging
        console.log("Sidebar active state after click: " + $('#sidebar').hasClass('active'));
    });

    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });

    // Initialize popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    });

    // Add animation to cards
    $('.card').addClass('animate__animated animate__fadeIn');
    
    // Log sidebar state for debugging
    console.log("Initial sidebar active state: " + $('#sidebar').hasClass('active'));
    console.log("Sidebar toggle button exists: " + ($('#sidebarCollapse').length > 0));
    
    // Ensure sidebar is visible by default on larger screens
    if ($(window).width() > 768) {
        $('#sidebar').removeClass('active');
    } else {
        // On mobile, start with sidebar hidden
        $('#sidebar').addClass('active');
    }
});

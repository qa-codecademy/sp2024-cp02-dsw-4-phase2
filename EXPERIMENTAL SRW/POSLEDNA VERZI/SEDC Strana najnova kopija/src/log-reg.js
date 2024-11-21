$(document).ready(function() {
    // Hide forms by default
    $('#sign-up-form').addClass('hidden'); // Keep sign-up hidden
    $('#profile-show').hide(); // Hide profile section by default
    $('#login-form').removeClass('hidden'); // Show login form
    $('#category-menu').hide(); // Initially hide category dropdown

    // Check if the user is logged in
    if (sessionStorage.getItem('userToken')) {
        const loggedInUser = JSON.parse(sessionStorage.getItem('loggedInUser'));
        $('#login-signup-link').hide();
        $('#profile-link').removeClass('hidden');
        $('#user-name').text(loggedInUser.name);
    } else {
        $('#profile-link').addClass('hidden');
        $('#login-signup-link').show();
    }

    // Handle profile link click
    $('#profile-link a').click(function(event) {
        event.preventDefault();
        const userToken = sessionStorage.getItem('userToken');

        if (userToken) {
            window.location.href = "http://127.0.0.1:5503/index.html#/profile";
            $('.profile-show').css('display', 'block');
        } else {
            window.location.href = "http://127.0.0.1:5503/index.html#/login";
        }
    });

    // Handle login button click
    $('#login-btn').click(function(event) {
        event.preventDefault();
        const email = $('#login-email').val().trim();
        const password = $('#password').val().trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!email || !emailRegex.test(email)) {
            showAlert("Please enter a valid email address.", "red");
            return;
        }

        if (!password) {
            showAlert("Please enter your password.", "red");
            return;
        }

        const logInUserDto = { Email: email, Password: password };

        $.ajax({
            url: 'https://localhost:7244/api/User/Login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(logInUserDto),
            success: function(response) {
                console.log(response);
                const UserId = response.userId;
                console.log(UserId);
                const userName = response.username;
                sessionStorage.setItem('userToken', response.token);
                sessionStorage.setItem('loggedInUser', JSON.stringify({ email: email, name: userName }));
                sessionStorage.setItem('userId', UserId); 
                $('#login-signup-link').hide();
                $('#profile-link').removeClass('hidden');
                $('#user-name').text(userName);
                showAlert("Login successful!", "green");
            
                fetchCategories();
                
                setTimeout(function() {
                    window.location.href = "http://127.0.0.1:5503/index.html#/home";
                }, 2000);
            },
            error: function(xhr) {
                if (xhr.status === 400) {
                    showAlert("Invalid email or password.", "red");
                } else {
                    showAlert("An unexpected error occurred.", "red");
                }
            }
        });
    });

    // Handle logout button click
    $('#logout-btn').click(function() {
        sessionStorage.removeItem('userToken'); // Clear token from sessionStorage
        sessionStorage.removeItem('loggedInUser'); // Clear user info
        sessionStorage.removeItem('userId');
        $('#profile-link').addClass('hidden');
        $('#login-signup-link').show();
        $('#user-name').text('');
        showAlert("Logout successful!", "green");

        setTimeout(function() {
            window.location.href = "http://127.0.0.1:5503/index.html#/home";
        }, 2000);
    });

    // Handle sign-up form submission
    $('#sign-up-form').on('submit', async function(e) {
        e.preventDefault();
        const requestBody = {
            FirstName: $('#first-name').val(),
            LastName: $('#last-name').val(),
            Username: $('#username').val(),
            Password: $('#sign-up-password').val(),
            Email: $('#email').val(),
            PhoneNumber: $('#phone-number').val(),
            ConfirmedPassword: $('#confirmed-password').val()
        };

        try {
            const response = await fetch('https://localhost:7244/api/User/Register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody)
            });

            const result = await response.text();
            if (response.ok) {
                alert('Registration successful! ' + result);
                $('#sign-up-form').addClass('hidden');
                $('#login-form').removeClass('hidden');
            } else {
                alert(`Registration failed: ${result}`);
            }
        } catch (error) {
            console.error('Error during registration:', error);
            alert('Error during registration. Please try again.');
        }
    });

    // Toggle between login and sign-up forms
    $('#switch-to-sign-up-btn').click(() => {
        $('#login-form').addClass('hidden'); // Hide login form
        $('#sign-up-form').removeClass('hidden'); // Show sign-up form
    });

    $('#back-to-login-btn').click(() => {
        $('#sign-up-form').addClass('hidden'); // Hide sign-up form
        $('#login-form').removeClass('hidden'); // Show login form
    });

    // Helper function to show alerts
    function showAlert(message, color) {
        $('.alert').text(message).css('color', color).show();
        setTimeout(() => {
            $('.alert').hide();
        }, 3000);
    }

    // Fetch categories function
    function fetchCategories() {
        const token = sessionStorage.getItem('userToken'); // Get token from sessionStorage

        if (!token) {
            console.error("User token is missing.");
            return;
        }

        fetch('https://localhost:7244/api/Category/GetAllCategories', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token.trim()}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(categories => {
            renderCategories(categories);
        })
        .catch(error => {
            console.error('Error fetching categories:', error);
        });
    }

    // Function to render categories in dropdown menu
    function renderCategories(categories) {
        const categoryMenu = $('#category-menu');
        categoryMenu.empty(); // Clear existing categories

        if (categories.length === 0) {
            categoryMenu.append('<li class="dropdown-item">No categories available.</li>'); // Handle no categories case
        } else {
            categories.forEach(category => {
                categoryMenu.append(`<li><a class="dropdown-item" href="#/category/${category.name}">${category.name}</a></li>`); // Add category items
            });
        }

        categoryMenu.show(); // Show categories
    }

    // Handle category toggle click
    $('#category-toggle').on('click', function(e) {
        e.preventDefault(); // Prevent default action
        $('#category-menu').toggle(); // Toggle visibility of the dropdown
        fetchCategories(); // Fetch categories when dropdown is toggled
    });

    // Hash change event to manage navigation
    window.onhashchange = function () {
        const hash = window.location.hash;

        if (hash.startsWith("#/home")) {
            // Show categories when going to home
            fetchCategories(); // Fetch and display categories
            $('#categories').show(); // Ensure categories are visible
            $('#category-details').hide(); // Hide category details if visible
        } else if (hash.startsWith("#/contact")) {
            // Show categories when going to contact us
            fetchCategories(); // Fetch and display categories
            $('#categories').show(); // Ensure categories are visible
            $('#category-details').hide(); // Hide category details if visible
        } else if (hash.startsWith("#/category/")) {
            // Load specific category details
            const categoryName = hash.split('/')[2];
            // You can add your logic to fetch and display products for the selected category
        }
    };

    // Initial trigger for hash change to set up correct state on page load
    window.onhashchange();
});
// OVA TUKA GO IMA USERID AMA NEMA KOA KJE KLIKNESH NA PROFIL
// $(document).ready(function() {
//     // Hide forms by default
//     $('#sign-up-form').addClass('hidden');
//     $('#profile-show').hide();
//     $('#login-form').removeClass('hidden');
//     $('#category-menu').hide();

//     // Check if the user is logged in
//     if (sessionStorage.getItem('userToken')) {
//         const loggedInUser = JSON.parse(sessionStorage.getItem('loggedInUser'));
//         $('#login-signup-link').hide();
//         $('#profile-link').removeClass('hidden');
//         $('#user-name').text(loggedInUser.name);
//     } else {
//         $('#profile-link').addClass('hidden');
//         $('#login-signup-link').show();
//     }

//     // Handle login button click
//     $('#login-btn').click(function(event) {
//         event.preventDefault();
//         const email = $('#login-email').val().trim();
//         const password = $('#password').val().trim();
//         const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

//         if (!email || !emailRegex.test(email)) {
//             showAlert("Please enter a valid email address.", "red");
//             return;
//         }

//         if (!password) {
//             showAlert("Please enter your password.", "red");
//             return;
//         }

//         const logInUserDto = { Email: email, Password: password };

//         $.ajax({
//             url: 'https://localhost:7244/api/User/Login',
//             type: 'POST',
//             contentType: 'application/json',
//             data: JSON.stringify(logInUserDto),
//             success: function(response) {
//                 const token = response.token;
//                 const decodedToken = decodeJwt(token);
//                 const userId = decodedToken.UserId; // Extract UserId from the decoded token
//                 const userName = response.username;
                
//                 sessionStorage.setItem('userToken', token);
//                 sessionStorage.setItem('loggedInUser', JSON.stringify({ email: email, name: userName }));
//                 sessionStorage.setItem('userId', userId);

//                 $('#login-signup-link').hide();
//                 $('#profile-link').removeClass('hidden');
//                 $('#user-name').text(userName);
//                 showAlert("Login successful!", "green");

//                 fetchCategories();
                
//                 setTimeout(function() {
//                     window.location.href = "http://127.0.0.1:5503/index.html#/home";
//                 }, 2000);
//             },
//             error: function(xhr) {
//                 if (xhr.status === 400) {
//                     showAlert("Invalid email or password.", "red");
//                 } else {
//                     showAlert("An unexpected error occurred.", "red");
//                 }
//             }
//         });
//     });

//     // Helper function to decode JWT token
//     function decodeJwt(token) {
//         const base64Url = token.split('.')[1]; // The middle part of the JWT
//         const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // Decode base64Url to base64
//         const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
//             return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
//         }).join(''));

//         return JSON.parse(jsonPayload);
//     }

//     // Handle logout button click
//     $('#logout-btn').click(function() {
//         sessionStorage.removeItem('userToken');
//         sessionStorage.removeItem('loggedInUser');
//         sessionStorage.removeItem('userId');
//         $('#profile-link').addClass('hidden');
//         $('#login-signup-link').show();
//         $('#user-name').text('');
//         showAlert("Logout successful!", "green");

//         setTimeout(function() {
//             window.location.href = "http://127.0.0.1:5503/index.html#/home";
//         }, 2000);
//     });

//     // Helper function to show alerts
//     function showAlert(message, color) {
//         $('.alert').text(message).css('color', color).show();
//         setTimeout(() => {
//             $('.alert').hide();
//         }, 3000);
//     }

//     // Fetch categories function
//     function fetchCategories() {
//         const token = sessionStorage.getItem('userToken');

//         if (!token) {
//             console.error("User token is missing.");
//             return;
//         }

//         fetch('https://localhost:7244/api/Category/GetAllCategories', {
//             method: 'GET',
//             headers: {
//                 'Authorization': `Bearer ${token.trim()}`,
//                 'Content-Type': 'application/json'
//             }
//         })
//         .then(response => {
//             if (!response.ok) {
//                 throw new Error(`HTTP error! status: ${response.status}`);
//             }
//             return response.json();
//         })
//         .then(categories => {
//             renderCategories(categories);
//         })
//         .catch(error => {
//             console.error('Error fetching categories:', error);
//         });
//     }

//     // Function to render categories in dropdown menu
//     function renderCategories(categories) {
//         const categoryMenu = $('#category-menu');
//         categoryMenu.empty(); // Clear existing categories

//         if (categories.length === 0) {
//             categoryMenu.append('<li class="dropdown-item">No categories available.</li>');
//         } else {
//             categories.forEach(category => {
//                 categoryMenu.append(`<li><a class="dropdown-item" href="#/category/${category.name}">${category.name}</a></li>`);
//             });
//         }

//         categoryMenu.show(); // Show categories
//     }

//     // Handle category toggle click
//     $('#category-toggle').on('click', function(e) {
//         e.preventDefault();
//         $('#category-menu').toggle(); // Toggle visibility of the dropdown
//         fetchCategories(); // Fetch categories when dropdown is toggled
//     });

//     // Hash change event to manage navigation
//     window.onhashchange = function () {
//         const hash = window.location.hash;

//         if (hash.startsWith("#/home")) {
//             fetchCategories();
//             $('#categories').show();
//             $('#category-details').hide();
//         } else if (hash.startsWith("#/contact")) {
//             fetchCategories();
//             $('#categories').show();
//             $('#category-details').hide();
//         } else if (hash.startsWith("#/category/")) {
//             const categoryName = hash.split('/')[2];
//             // Add logic for fetching and displaying category details
//         }
//     };

//     window.onhashchange(); // Initial trigger for hash change to set up correct state on page load
// });

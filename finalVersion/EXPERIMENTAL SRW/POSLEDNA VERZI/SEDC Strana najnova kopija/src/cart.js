console.log("Script loaded. Setting up event listener.");

// document.addEventListener('DOMContentLoaded', function () {
//     const shippingFormContainer = document.querySelector('.container-shipping-info');
//     const shippingFormButton = document.querySelector('.button-shipping-info');
//     const paymentInfoButton = document.querySelector('#payment-info');
//     const completeOrderButton = document.querySelector('.truck-button');
//     const homePageContent = document.querySelector('.homepagehidden');
//     const cartContent = document.querySelector('.cart-container');
//     const cartItemsContainer = document.querySelector('.cart-items');
//     const cartCountBadge = document.querySelector('#cart-count');
//     const cartLink = document.querySelector('#cart-link');

//     shippingFormButton.disabled = true;
//     completeOrderButton.disabled = true;

//     const form = document.querySelector('.form-shipping-info');
//     const inputs = form.querySelectorAll('.field-shipping-info__input');
//     inputs.forEach(input => {
//         input.addEventListener('input', () => {
//             const allFilled = Array.from(inputs).every(input => input.value.trim() !== '');
//             shippingFormButton.disabled = !allFilled;
//         });
//     });

//     paymentInfoButton.addEventListener('click', () => {
//         shippingFormContainer.style.display = (shippingFormContainer.style.display === 'block') ? 'none' : 'block';
//     });

//     shippingFormButton.addEventListener('click', (e) => {
//         e.preventDefault();
//         completeOrderButton.disabled = false;

//         const firstName = document.querySelector('#firstname').value;
//         const lastName = document.querySelector('#lastname').value;
//         const address = document.querySelector('#address').value;
//         const country = document.querySelector('#country').value;
//         const zipCode = document.querySelector('#zipcode').value;
//         const city = document.querySelector('#city').value;
//         const state = document.querySelector('#municipality').value;

//         alert(`Successfully entered details:\n\nFirst Name: ${firstName}\nLast Name: ${lastName}\nAddress: ${address}\nCountry: ${country}\nZip Code: ${zipCode}\nCity: ${city}\nMunicipality: ${state}`);
//     });

//     function checkCart() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             completeOrderButton.disabled = true;
//             cartCountBadge.textContent = '0';
//             cartContent.style.display = 'none';  
//             emptyCartMessage.classList.remove('hidden'); 
//         } else {
//             completeOrderButton.disabled = false;
//             cartCountBadge.classList.remove('hidden');
//             cartCountBadge.textContent = cartItems.length;
//             cartLink.classList.remove('disabled');
//             cartLink.style.pointerEvents = 'auto';
//             cartContent.style.display = 'block'; 
//             emptyCartMessage.classList.add('hidden'); 
    
//             cartItemsContainer.innerHTML = '';
//             cartItems.forEach((item, index) => {
//                 const cartItem = document.createElement('div');
//                 cartItem.classList.add('cart-item');
//                 cartItem.innerHTML = `
//                     <div class="item-image">
//                         <img src="${item.image}" alt="${item.name}">
//                     </div>
//                     <div class="item-details">
//                         <h3>${item.name}</h3>
//                         <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                         <div class="quantity">
//                             <label for="quantity${index}">Quantity:</label>
//                             <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                         </div>
//                         <button class="remove-item" data-index="${index}">Remove</button>
//                     </div>
//                 `;
//                 cartItemsContainer.appendChild(cartItem);
//             });
    
//             document.querySelectorAll('.quantity input').forEach((input, index) => {
//                 input.addEventListener('change', function () {
//                     updateQuantity(index, this.value);
//                 });
//             });
    
//             document.querySelectorAll('.remove-item').forEach(button => {
//                 button.addEventListener('click', function () {
//                     const index = this.getAttribute('data-index');
//                     removeItem(index);
//                 });
//             });
    
//             updateCartSummary();
//         }
//     }

//     cartLink.addEventListener('click', (e) => {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             e.preventDefault();
//         }
//     });

//     completeOrderButton.addEventListener('click', (e) => {
//         e.preventDefault();

//         let box = completeOrderButton.querySelector(".box"),
//             truck = completeOrderButton.querySelector(".truck");

//         if (!completeOrderButton.classList.contains("done")) {
//             if (!completeOrderButton.classList.contains("animation")) {
//                 completeOrderButton.classList.add("animation");

//                 gsap.to(completeOrderButton, {
//                     "--box-s": 1,
//                     "--box-o": 1,
//                     duration: 0.3,
//                     delay: 0.5
//                 });

//                 gsap.to(box, {
//                     x: 0,
//                     duration: 0.4,
//                     delay: 0.7
//                 });

//                 gsap.to(completeOrderButton, {
//                     "--hx": -5,
//                     "--bx": 50,
//                     duration: 0.18,
//                     delay: 0.92
//                 });

//                 gsap.to(box, {
//                     y: 0,
//                     duration: 0.1,
//                     delay: 1.15
//                 });

//                 gsap.set(completeOrderButton, {
//                     "--truck-y": 0,
//                     "--truck-y-n": -26
//                 });

//                 gsap.to(completeOrderButton, {
//                     "--truck-y": 1,
//                     "--truck-y-n": -25,
//                     duration: 0.2,
//                     delay: 1.25,
//                     onComplete() {
//                         gsap.timeline({
//                             onComplete() {
//                                 completeOrderButton.classList.add("done");
//                                 localStorage.removeItem('cartItems'); 
//                                 cartItemsContainer.innerHTML = '';
//                                 checkCart();
//                                 setTimeout(() => {
//                                     cartContent.style.display = 'none';
//                                     shippingFormContainer.style.display = 'none';
//                                     homePageContent.style.display = 'block';
//                                 }, 1000); 
//                             }
//                         })
//                             .to(truck, {
//                                 x: 0,
//                                 duration: 0.4
//                             })
//                             .to(truck, {
//                                 x: 40,
//                                 duration: 1
//                             })
//                             .to(truck, {
//                                 x: 20,
//                                 duration: 0.6
//                             })
//                             .to(truck, {
//                                 x: 96,
//                                 duration: 0.4
//                             });
//                         gsap.to(completeOrderButton, {
//                             "--progress": 1,
//                             duration: 2.4,
//                             ease: "power2.in"
//                         });
//                     }
//                 });
//             }
//         } else {
//             completeOrderButton.classList.remove("animation", "done");
//             gsap.set(truck, {
//                 x: 4
//             });
//             gsap.set(completeOrderButton, {
//                 "--progress": 0,
//                 "--hx": 0,
//                 "--bx": 0,
//                 "--box-s": 0.5,
//                 "--box-o": 0,
//                 "--truck-y": 0,
//                 "--truck-y-n": -26
//             });
//             gsap.set(box, {
//                 x: -24,
//                 y: -6
//             });
//         }
//     });

//     checkCart();

//     function addItemToCart(item) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.push(item);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();
//     }

//     function removeItemFromCart(index) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.splice(index, 1);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();

//         if (cartItems.length === 0) {
//             homePageContent.style.display = 'block';
//             cartContent.style.display = 'none';
//         }
//     }

//     function updateQuantity(index, quantity) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems[index].quantity = parseInt(quantity);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         updateCartSummary();
//         checkCart();
//     }

//     function updateCartSummary() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         const subtotal = cartItems.reduce((total, item) => total + parseFloat(item.newPrice.replace(' EUR', '')) * item.quantity, 0);
//         document.getElementById('subtotal').textContent = subtotal.toFixed(2);
//         document.getElementById('total').textContent = subtotal.toFixed(2);
//     }

//     function showSectionFromHash() {
//         const hash = window.location.hash;
//         hideAllSections();
//         switch (hash) {
//             case '#/home':
//                 document.querySelector('.homepagehidden').style.display = 'block';
//                 break;
//             case '#/salespage':
//                 document.querySelector('.bcground').style.display = 'block';
//                 break;
//             case '#/login':
//                 document.querySelector('.log-reg-hidden').style.display = 'block';
//                 break;
//             case '#/contact':
//                 document.querySelector('.contact-hidden').style.display = 'block';
//                 break;
//             case '#/profile':
//                 const profileSection = document.querySelector('.profile-show');
//                 profileSection.classList.remove('hidden');
//                 profileSection.style.display = 'block';
//                 break;
//                 case '#/cart':
//                     const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//                     if (cartItems.length > 0) {
//                         document.querySelector('.cart-container').style.display = 'block';
//                     } else {
//                         history.pushState(null, '', '#/home');
//                     }
//                     break;
//             case '#/moredetails':
//                 document.querySelector('.container-moredetails').style.display = 'block';
//                 break;
//             default:
//                 document.querySelector('.homepagehidden').style.display = 'block';
//                 updateHash('#/home');
//                 break;
//         }
//     }

//     function hideAllSections() {
//         document.querySelector('.homepagehidden').style.display = 'none';
//         document.querySelector('.bcground').style.display = 'none';
//         document.querySelector('.log-reg-hidden').style.display = 'none';
//         document.querySelector('.contact-hidden').style.display = 'none';
//         document.querySelector('.cart-container').style.display = 'none'; 
//         document.querySelector('.container-moredetails').style.display = 'none'; 
//         document.querySelector('.profile-show').style.display = 'none'; 
//     }

//     function updateHash(hash) {
//         history.pushState(null, '', hash);
//     }

//     showSectionFromHash();

// //-------------------- TIMER FUNCTION ------------------//

//     function sortProducts(criteria) {
//         const productsContainer = document.getElementById('products');
//         const products = Array.from(productsContainer.getElementsByClassName('product-item'));

//         products.sort((a, b) => {
//            if (criteria === 'lower-price') {
//                 const priceA = parseFloat(a.querySelector('#new-Price').textContent.replace(/[^\d.-]/g, ''));
//                 const priceB = parseFloat(b.querySelector('#new-Price').textContent.replace(/[^\d.-]/g, ''));
//                 return priceA - priceB;
//             } else if (criteria === 'higher-price') {
//                  const priceA = parseFloat(a.querySelector('#new-Price').textContent.replace(/[^\d.-]/g, ''));
//                 const priceB = parseFloat(b.querySelector('#new-Price').textContent.replace(/[^\d.-]/g, ''));
//                 return priceB - priceA;
//             } else if (criteria === 'highest-offer') {
//               const discountA = parseFloat(a.querySelector('.badge').textContent.replace('Extra ', '').replace('% Off', ''));
//               const discountB = parseFloat(b.querySelector('.badge').textContent.replace('Extra ', '').replace('% Off', ''));
//                  return discountB - discountA;
//            } else if (criteria === 'alphabetical') {
//                 const nameA = a.querySelector('h4').textContent.toLowerCase();
//                 const nameB = b.querySelector('h4').textContent.toLowerCase();
//                 if (nameA < nameB) return -1;
//                  if (nameA > nameB) return 1;
//                  return 0;
//             }
//         });

//         productsContainer.innerHTML = '';
//          products.forEach(product => productsContainer.appendChild(product));
//     }

//     document.getElementById('sort-options').addEventListener('change', function () {
//          sortProducts(this.value);
//      });
//  });
// =================================================================================================================================================

// function showSectionFromHash() {
//     const hash = window.location.hash;
//     hideAllSections(); // Hide all sections first
//     switch (hash) {
//         case '#/home':
//             document.querySelector('.homepagehidden').style.display = 'block';
//             break;
//         case '#/salespage':
//             document.querySelector('.bcground').style.display = 'block';
//             break;
//         case '#/login':
//             document.querySelector('.log-reg-hidden').style.display = 'block';
//             break;
//         case '#/contact':
//             document.querySelector('.contact-hidden').style.display = 'block';
//             break;
//         case '#/profile':
//             const profileSection = document.querySelector('.profile-show');
//             profileSection.classList.remove('hidden');
//             profileSection.style.display = 'block';
//             break;
//         case '#/cart':
//             const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//             if (cartItems.length > 0) {
//                 // Show cart container if there are items in the cart
//                 document.querySelector('.cart-container').style.display = 'block';
//                 displayCartItems(cartItems); // Call function to display items
//             } else {
//                 // If the cart is empty, redirect to home
//                 history.pushState(null, '', '#/home');
//             }
//             break;
//         case '#/moredetails':
//             document.querySelector('.container-moredetails').style.display = 'block';
//             break;
//         default:
//             document.querySelector('.homepagehidden').style.display = 'block';
//             updateHash('#/home');
//             break;
//     }
// }

// // Function to display cart items
// function displayCartItems(cartItems) {
//     const cartItemsContainer = document.querySelector('.cart-items'); // Ensure this matches your HTML structure
//     cartItemsContainer.innerHTML = ''; // Clear existing items
//     cartItems.forEach((item, index) => {
//         const cartItem = document.createElement('div');
//         cartItem.classList.add('cart-item');
//         cartItem.innerHTML = `
//             <div class="item-image">
//                 <img src="${item.image}" alt="${item.name}">
//             </div>
//             <div class="item-details">
//                 <h3>${item.name}</h3>
//                 <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                 <div class="quantity">
//                     <label for="quantity${index}">Quantity:</label>
//                     <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                 </div>
//                 <button class="remove-item" data-index="${index}">Remove</button>
//             </div>
//         `;
//         cartItemsContainer.appendChild(cartItem);
//     });
    
//     // Add event listeners for quantity inputs and remove buttons
//     document.querySelectorAll('.quantity input').forEach((input, index) => {
//         input.addEventListener('change', function () {
//             updateQuantity(index, this.value);
//         });
//     });

//     document.querySelectorAll('.remove-item').forEach(button => {
//         button.addEventListener('click', function () {
//             const index = this.getAttribute('data-index');
//             removeItemFromCart(index);
//         });
//     });

//     updateCartSummary(); // Update cart summary after displaying items
// }
// -======================================================= WORKING =================================================================

// document.addEventListener('DOMContentLoaded', function () {
//     const shippingFormContainer = document.querySelector('.container-shipping-info');
//     const shippingFormButton = document.querySelector('.button-shipping-info');
//     const paymentInfoButton = document.querySelector('#payment-info');
//     const completeOrderButton = document.querySelector('.truck-button');
//     const homePageContent = document.querySelector('.homepagehidden');
//     const cartContent = document.querySelector('.cart-container');
//     const cartItemsContainer = document.querySelector('.cart-items');
//     const cartCountBadge = document.querySelector('#cart-count');
//     const cartLink = document.querySelector('#cart-link');
//     const emptyCartMessage = document.querySelector('.empty-cart-message'); // Assuming this is defined

//     shippingFormButton.disabled = true;
//     completeOrderButton.disabled = true;

//     const form = document.querySelector('.form-shipping-info');
//     const inputs = form.querySelectorAll('.field-shipping-info__input');

//     inputs.forEach(input => {
//         input.addEventListener('input', toggleShippingButton);
//     });

//     paymentInfoButton.addEventListener('click', toggleShippingForm);
//     shippingFormButton.addEventListener('click', handleShippingFormSubmit);
//     completeOrderButton.addEventListener('click', handleCompleteOrder);
//     cartLink.addEventListener('click', preventEmptyCartNavigation);
    
//     checkCart();

//     function toggleShippingButton() {
//         const allFilled = Array.from(inputs).every(input => input.value.trim() !== '');
//         shippingFormButton.disabled = !allFilled;
//     }

//     function toggleShippingForm() {
//         shippingFormContainer.style.display = (shippingFormContainer.style.display === 'block') ? 'none' : 'block';
//     }

//     function handleShippingFormSubmit(e) {
//         e.preventDefault();
//         completeOrderButton.disabled = false;

//         const shippingDetails = Array.from(inputs).reduce((acc, input) => {
//             acc[input.id] = input.value;
//             return acc;
//         }, {});

//         alert(`Successfully entered details:\n\n${Object.entries(shippingDetails).map(([key, value]) => `${key.charAt(0).toUpperCase() + key.slice(1)}: ${value}`).join('\n')}`);
//     }

//     function checkCart() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             completeOrderButton.disabled = true;
//             cartCountBadge.textContent = '0';
//             cartContent.style.display = 'none';  
//             emptyCartMessage.classList.remove('hidden'); 
//         } else {
//             completeOrderButton.disabled = false;
//             updateCartBadge(cartItems.length);
//             renderCartItems(cartItems);
//             updateCartSummary(cartItems);
//         }
//     }

//     function updateCartBadge(count) {
//         cartCountBadge.textContent = count;
//         cartCountBadge.classList.remove('hidden');
//         cartLink.classList.remove('disabled');
//         cartLink.style.pointerEvents = 'auto';
//         cartContent.style.display = 'block'; 
//         emptyCartMessage.classList.add('hidden'); 
//     }

//     function renderCartItems(cartItems) {
//         cartItemsContainer.innerHTML = '';
//         cartItems.forEach((item, index) => {
//             const cartItem = createCartItemElement(item, index);
//             cartItemsContainer.appendChild(cartItem);
//         });

//         attachCartItemEvents();
//     }

//     function createCartItemElement(item, index) {
//         const cartItem = document.createElement('div');
//         cartItem.classList.add('cart-item');
//         cartItem.innerHTML = `
//             <div class="item-image">
//                 <img src="${item.image}" alt="${item.name}">
//             </div>
//             <div class="item-details">
//                 <h3>${item.name}</h3>
//                 <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                 <div class="quantity">
//                     <label for="quantity${index}">Quantity:</label>
//                     <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                 </div>
//                 <button class="remove-item" data-index="${index}">Remove</button>
//             </div>
//         `;
//         return cartItem;
//     }

//     function attachCartItemEvents() {
//         document.querySelectorAll('.quantity input').forEach((input, index) => {
//             input.addEventListener('change', function () {
//                 updateQuantity(index, this.value);
//             });
//         });

//         document.querySelectorAll('.remove-item').forEach(button => {
//             button.addEventListener('click', function () {
//                 const index = this.getAttribute('data-index');
//                 removeItem(index);
//             });
//         });
//     }

//     function preventEmptyCartNavigation(e) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             e.preventDefault();
//         }
//     }

//     function handleCompleteOrder(e) {
//         e.preventDefault();

//         if (!completeOrderButton.classList.contains("done")) {
//             if (!completeOrderButton.classList.contains("animation")) {
//                 completeOrderButton.classList.add("animation");
//                 startOrderAnimation();
//             }
//         } else {
//             resetOrderButton();
//         }
//     }

//     function startOrderAnimation() {
//         let box = completeOrderButton.querySelector(".box"),
//             truck = completeOrderButton.querySelector(".truck");

//         gsap.to(completeOrderButton, {
//             "--box-s": 1,
//             "--box-o": 1,
//             duration: 0.3,
//             delay: 0.5
//         });

//         gsap.to(box, {
//             x: 0,
//             duration: 0.4,
//             delay: 0.7
//         });

//         gsap.to(completeOrderButton, {
//             "--hx": -5,
//             "--bx": 50,
//             duration: 0.18,
//             delay: 0.92
//         });

//         gsap.to(box, {
//             y: 0,
//             duration: 0.1,
//             delay: 1.15
//         });

//         gsap.set(completeOrderButton, {
//             "--truck-y": 0,
//             "--truck-y-n": -26
//         });

//         gsap.to(completeOrderButton, {
//             "--truck-y": 1,
//             "--truck-y-n": -25,
//             duration: 0.2,
//             delay: 1.25,
//             onComplete: finalizeOrder
//         });
//     }

//     function finalizeOrder() {
//         const box = completeOrderButton.querySelector(".box");
//         const truck = completeOrderButton.querySelector(".truck");
        
//         gsap.timeline({
//             onComplete() {
//                 completeOrderButton.classList.add("done");
//                 localStorage.removeItem('cartItems'); 
//                 cartItemsContainer.innerHTML = '';
//                 checkCart();
//                 setTimeout(() => {
//                     cartContent.style.display = 'none';
//                     shippingFormContainer.style.display = 'none';
//                     homePageContent.style.display = 'block';
//                 }, 1000); 
//             }
//         })
//         .to(truck, { x: 0, duration: 0.4 })
//         .to(truck, { x: 40, duration: 1 })
//         .to(truck, { x: 20, duration: 0.6 })
//         .to(truck, { x: 96, duration: 0.4 });

//         gsap.to(completeOrderButton, {
//             "--progress": 1,
//             duration: 2.4,
//             ease: "power2.in"
//         });
//     }

//     function resetOrderButton() {
//         const truck = completeOrderButton.querySelector(".truck");
//         completeOrderButton.classList.remove("animation", "done");
//         gsap.set(truck, { x: 4 });
//         gsap.set(completeOrderButton, {
//             "--progress": 0,
//             "--hx": 0,
//             "--bx": 0,
//             "--box-s": 0.5,
//             "--box-o": 0,
//             "--truck-y": 0,
//             "--truck-y-n": -26
//         });
//         gsap.set(completeOrderButton.querySelector(".box"), {
//             x: -24,
//             y: -6
//         });
//     }

//     function updateQuantity(index, quantity) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems[index].quantity = parseInt(quantity);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         updateCartSummary(cartItems);
//         checkCart();
//     }

//     function removeItem(index) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.splice(index, 1);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();

//         if (cartItems.length === 0) {
//             homePageContent.style.display = 'block';
//             cartContent.style.display = 'none';
//         }
//     }

//     function updateCartSummary(cartItems) {
//         const subtotal = cartItems.reduce((total, item) => total + parseFloat(item.newPrice.replace(' EUR', '')) * item.quantity, 0);
//         document.getElementById('subtotal').textContent = subtotal.toFixed(2);
//         document.getElementById('total').textContent = subtotal.toFixed(2);
//     }

//     function showSectionFromHash() {
//         const hash = window.location.hash;
//         hideAllSections();
//         switch (hash) {
//             case '#/home':
//                 homePageContent.style.display = 'block';
//                 break;
//             case '#/cart':
//                 cartContent.style.display = 'block';
//                 break;
//             // Add other cases as necessary
//             default:
//                 homePageContent.style.display = 'block';
//         }
//     }

//     function hideAllSections() {
//         homePageContent.style.display = 'none';
//         cartContent.style.display = 'none';
//         // Hide other sections if needed
//     }

//     window.addEventListener('hashchange', showSectionFromHash);
// });


// const cartLink = document.querySelector('#cart-link');

// // Event listener for the cart button
// cartLink.addEventListener('click', function (e) {
//     e.preventDefault(); // Prevent default behavior if necessary (like form submission)
//     // Check if the cart is empty and handle accordingly
//     const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//     if (cartItems.length === 0) {
//         // Optionally, you can alert the user or show a message
//         alert('Your cart is empty!');
//         return; // Do not navigate if the cart is empty
//     }

//     // Redirect to the cart page
//     window.location.hash = '#/cart'; // This assumes you are using hash routing
// });

// // Function to show the correct section based on the hash
// function showSectionFromHash() {
//     const hash = window.location.hash;
//     hideAllSections(); // Function to hide all sections

//     switch (hash) {
//         case '#/home':
//             homePageContent.style.display = 'block';
//             break;
//         case '#/cart':
//             cartContent.style.display = 'block';
//             break;
//         default:
//             homePageContent.style.display = 'block';
//     }
// }

// // Initial call to show the correct section on page load
// showSectionFromHash();

// // Add an event listener for hash change
// window.addEventListener('hashchange', showSectionFromHash);


// =================================================== SENDING EMAILS =============================================================

// completeOrderButton.addEventListener('click', async (e) => {
//     e.preventDefault();

//     // Check if the button has been clicked before
//     if (!completeOrderButton.classList.contains("done")) {
//         if (!completeOrderButton.classList.contains("animation")) {
//             completeOrderButton.classList.add("animation");
//             animateOrderComplete();
//         }
//     } else {
//         resetCompleteOrderButton();
//     }

//     const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//     const orderDetails = cartItems.map(item => `Product: ${item.name}, Quantity: ${item.quantity}, Price: ${item.newPrice}€`).join('\n');

//     const userEmail = localStorage.getItem('userEmail'); // Assuming email is stored in localStorage

//     // Create the request body using the OrderNotificationDto structure
//     const requestBody = {
//         Email: userEmail, // Use the fetched email
//         OrderDetails: orderDetails
//     };

//     // Wait for 3 seconds before sending the email
//     await new Promise(resolve => setTimeout(resolve, 3000));

//     // Send order notification request to the backend
//     try {
//         const response = await fetch('/api/order/sendOrderNotification', {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify(requestBody)
//         });

//         const result = await response.json();
//         if (response.ok) {
//             alert(result);
//         } else {
//             alert(`Failed to send order notification: ${result}`);
//         }
//     } catch (error) {
//         console.error('Error sending order notification:', error);
//         alert('Error sending order notification. Please try again.');
//     }
// });
// =============================================================================================
// completeOrderButton.addEventListener('click', async (e) => {
//     e.preventDefault();
//     console.log("Button clicked.");

//     // Check if the button has been clicked before
//     if (!completeOrderButton.classList.contains("done")) {
//         console.log("Button not marked as done. Starting animation...");
//         if (!completeOrderButton.classList.contains("animation")) {
//             completeOrderButton.classList.add("animation");
//             animateOrderComplete();
//         }
//     } else {
//         console.log("Button already marked as done. Resetting button...");
//         resetCompleteOrderButton();
//     }

//     const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//     const orderDetails = cartItems.map(item => `Product: ${item.name}, Quantity: ${item.quantity}, Price: ${item.newPrice}€`).join('\n');
//     console.log("Order details:", orderDetails);

//     const userEmail = localStorage.getItem('userEmail'); // Assuming email is stored in localStorage
//     console.log("User email:", userEmail);

//     if (!userEmail) {
//         alert("User email is missing.");
//         return;
//     }

//     // Create the request body using the OrderNotificationDto structure
//     const requestBody = {
//         Email: userEmail, // Use the fetched email
//         OrderDetails: orderDetails
//     };
//     console.log("Request body:", requestBody);

//     // Wait for 3 seconds before sending the email
//     await new Promise(resolve => setTimeout(resolve, 3000));
//     console.log("Delay complete. Sending order notification...");

//     // Send order notification request to the backend
//     try {
//         const response = await fetch('https://localhost:7244/api/Contact/SendOrderNotification', {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify(requestBody)
//         });
//         console.log("API response received:", response);

//         const result = await response.json();
//         if (response.ok) {
//             console.log("Order notification sent successfully:", result);
//             alert(result);
//         } else {
//             console.error("Failed to send order notification:", result);
//             alert(`Failed to send order notification: ${result}`);
//         }
//     } catch (error) {
//         console.error('Error sending order notification:', error);
//         alert('Error sending order notification. Please try again.');
//     }
// });
// ======================================================= NEW ================================================
// completeOrderButton.addEventListener(`onclick`, async (e) => {
//     e.preventDefault();
//     console.log("Hello"); // This will log "Hello" when the button is pressed.

//     // Check if the button has been clicked before
//     if (!completeOrderButton.classList.contains("done")) {
//         console.log("Button not marked as done. Starting animation...");
//         if (!completeOrderButton.classList.contains("animation")) {
//             completeOrderButton.classList.add("animation");
//             animateOrderComplete();
//         }
//     } else {
//         console.log("Button already marked as done. Resetting button...");
//         resetCompleteOrderButton();
//     }

//     const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//     const orderDetails = cartItems.map(item => `Product: ${item.name}, Quantity: ${item.quantity}, Price: ${item.newPrice}€`).join('\n');
//     console.log("Order details:", orderDetails);

//     const userEmail = localStorage.getItem('userEmail'); // Assuming email is stored in localStorage
//     console.log("User email:", userEmail);

//     if (!userEmail) {
//         alert("User email is missing.");
//         return;
//     }

//     // Create the request body using the OrderNotificationDto structure
//     const requestBody = {
//         Email: userEmail, // Use the fetched email
//         OrderDetails: orderDetails
//     };
//     console.log("Request body:", requestBody);

//     // Wait for 3 seconds before sending the request...
// });
// =============================================================================================

// document.getElementById('completeOrderButton').addEventListener('click', async (e) => {
//     e.preventDefault(); // Prevent default button behavior

//     // Optional: Change the button text to indicate the order is being processed
//     const button = e.currentTarget;
//     button.querySelector('.default').style.display = 'none';
//     button.querySelector('.success').style.display = 'block';

//     // Prepare the email request DTO
//     const emailDescription = "Your order has been successfully placed!";
    
//     try {
//         const response = await fetch('https://localhost:7244/api/Contact/SendOrderNotification', {
//             method: 'POST',
//             headers: {
//                 'Content-Type': 'application/json',
//                 'Accept': 'application/json'
//             },
//             body: JSON.stringify({ description: emailDescription })
//         });

//         if (response.ok) {
//             const result = await response.json();
//             console.log(result); // Handle success response
//             alert(result); // Show success message to the user
//         } else {
//             const error = await response.text();
//             console.error("Error:", error); // Handle error response
//             alert("Failed to send email: " + error);
//         }
//     } catch (error) {
//         console.error("Error sending request:", error);
//         alert("An error occurred while sending the request.");
//     }
// });

// =============================================== FETCHING BE ===========================================================
// document.addEventListener('DOMContentLoaded', function () {
//     const shippingFormContainer = document.querySelector('.container-shipping-info');
//     const shippingFormButton = document.querySelector('.button-shipping-info');
//     const paymentInfoButton = document.querySelector('#payment-info');
//     const completeOrderButton = document.querySelector('.truck-button');
//     const homePageContent = document.querySelector('.homepagehidden');
//     const cartContent = document.querySelector('.cart-container');
//     const cartItemsContainer = document.querySelector('.cart-items');
//     const cartCountBadge = document.querySelector('#cart-count');
//     const cartLink = document.querySelector('#cart-link');
//     const emptyCartMessage = document.querySelector('.empty-cart-message'); // Add a reference to the empty cart message

//     shippingFormButton.disabled = true;
//     completeOrderButton.disabled = true;

//     const form = document.querySelector('.form-shipping-info');
//     const inputs = form.querySelectorAll('.field-shipping-info__input');
//     inputs.forEach(input => {
//         input.addEventListener('input', () => {
//             const allFilled = Array.from(inputs).every(input => input.value.trim() !== '');
//             shippingFormButton.disabled = !allFilled;
//         });
//     });

//     paymentInfoButton.addEventListener('click', () => {
//         shippingFormContainer.style.display = (shippingFormContainer.style.display === 'block') ? 'none' : 'block';
//     });

//     shippingFormButton.addEventListener('click', (e) => {
//         e.preventDefault();
//         completeOrderButton.disabled = false;

//         const firstName = document.querySelector('#firstname').value;
//         const lastName = document.querySelector('#lastname').value;
//         const address = document.querySelector('#address').value;
//         const country = document.querySelector('#country').value;
//         const zipCode = document.querySelector('#zipcode').value;
//         const city = document.querySelector('#city').value;
//         const state = document.querySelector('#municipality').value;

//         alert(`Successfully entered details:\n\nFirst Name: ${firstName}\nLast Name: ${lastName}\nAddress: ${address}\nCountry: ${country}\nZip Code: ${zipCode}\nCity: ${city}\nMunicipality: ${state}`);
//     });

//     function checkCart() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             completeOrderButton.disabled = true;
//             cartCountBadge.textContent = '0';
//             cartContent.style.display = 'none';  
//             emptyCartMessage.classList.remove('hidden'); 
//         } else {
//             completeOrderButton.disabled = false;
//             cartCountBadge.classList.remove('hidden');
//             cartCountBadge.textContent = cartItems.length;
//             cartLink.classList.remove('disabled');
//             cartLink.style.pointerEvents = 'auto';
//             cartContent.style.display = 'block'; 
//             emptyCartMessage.classList.add('hidden'); 
    
//             cartItemsContainer.innerHTML = '';
//             cartItems.forEach((item, index) => {
//                 const cartItem = document.createElement('div');
//                 cartItem.classList.add('cart-item');
//                 cartItem.innerHTML = `
//                     <div class="item-image">
//                         <img src="${item.image}" alt="${item.name}">
//                     </div>
//                     <div class="item-details">
//                         <h3>${item.name}</h3>
//                         <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                         <div class="quantity">
//                             <label for="quantity${index}">Quantity:</label>
//                             <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                         </div>
//                         <button class="remove-item" data-index="${index}">Remove</button>
//                     </div>
//                 `;
//                 cartItemsContainer.appendChild(cartItem);
//             });
    
//             document.querySelectorAll('.quantity input').forEach((input, index) => {
//                 input.addEventListener('change', function () {
//                     updateQuantity(index, this.value);
//                 });
//             });
    
//             document.querySelectorAll('.remove-item').forEach(button => {
//                 button.addEventListener('click', function () {
//                     const index = this.getAttribute('data-index');
//                     removeItem(index);
//                 });
//             });
    
//             updateCartSummary();
//         }
//     }

//     cartLink.addEventListener('click', (e) => {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             e.preventDefault();
//         }
//     });

//     completeOrderButton.addEventListener('click', (e) => {
//         e.preventDefault();

//         let box = completeOrderButton.querySelector(".box"),
//             truck = completeOrderButton.querySelector(".truck");

//         if (!completeOrderButton.classList.contains("done")) {
//             if (!completeOrderButton.classList.contains("animation")) {
//                 completeOrderButton.classList.add("animation");

//                 gsap.to(completeOrderButton, {
//                     "--box-s": 1,
//                     "--box-o": 1,
//                     duration: 0.3,
//                     delay: 0.5
//                 });

//                 gsap.to(box, {
//                     x: 0,
//                     duration: 0.4,
//                     delay: 0.7
//                 });

//                 gsap.to(completeOrderButton, {
//                     "--hx": -5,
//                     "--bx": 50,
//                     duration: 0.18,
//                     delay: 0.92
//                 });

//                 gsap.to(box, {
//                     y: 0,
//                     duration: 0.1,
//                     delay: 1.15
//                 });

//                 gsap.set(completeOrderButton, {
//                     "--truck-y": 0,
//                     "--truck-y-n": -26
//                 });

//                 gsap.to(completeOrderButton, {
//                     "--truck-y": 1,
//                     "--truck-y-n": -25,
//                     duration: 0.2,
//                     delay: 1.25,
//                     onComplete() {
//                         gsap.timeline({
//                             onComplete() {
//                                 completeOrderButton.classList.add("done");
//                                 localStorage.removeItem('cartItems'); 
//                                 cartItemsContainer.innerHTML = '';
//                                 checkCart();
//                                 setTimeout(() => {
//                                     cartContent.style.display = 'none';
//                                     shippingFormContainer.style.display = 'none';
//                                     homePageContent.style.display = 'block';
//                                 }, 1000); 
//                             }
//                         })
//                             .to(truck, {
//                                 x: 0,
//                                 duration: 0.4
//                             })
//                             .to(truck, {
//                                 x: 40,
//                                 duration: 1
//                             })
//                             .to(truck, {
//                                 x: 20,
//                                 duration: 0.6
//                             })
//                             .to(truck, {
//                                 x: 96,
//                                 duration: 0.4
//                             });
//                         gsap.to(completeOrderButton, {
//                             "--progress": 1,
//                             duration: 2.4,
//                             ease: "power2.in"
//                         });
//                     }
//                 });
//             }
//         } else {
//             completeOrderButton.classList.remove("animation", "done");
//             gsap.set(truck, {
//                 x: 4
//             });
//             gsap.set(completeOrderButton, {
//                 "--progress": 0,
//                 "--hx": 0,
//                 "--bx": 0,
//                 "--box-s": 0.5,
//                 "--box-o": 0,
//                 "--truck-y": 0,
//                 "--truck-y-n": -26
//             });
//             gsap.set(box, {
//                 x: -24,
//                 y: -6
//             });
//         }
//     });

//     checkCart();

//     function addItemToCart(item) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.push(item);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();
//     }

//     function removeItemFromCart(index) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.splice(index, 1);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();

//         if (cartItems.length === 0) {
//             homePageContent.style.display = 'block';
//             cartContent.style.display = 'none';
//         }
//     }

//     function updateQuantity(index, quantity) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems[index].quantity = parseInt(quantity);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         updateCartSummary();
//         checkCart();
//     }

//     function updateCartSummary() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         const subtotal = cartItems.reduce((total, item) => total + parseFloat(item.newPrice.replace(' EUR', '')) * item.quantity, 0);
//         document.getElementById('subtotal').textContent = subtotal.toFixed(2);
//         document.getElementById('total').textContent = subtotal.toFixed(2);
//     }

//     function showSectionFromHash() {
//         const hash = window.location.hash;
//         hideAllSections();
//         switch (hash) {
//             case '#/home':
//                 document.querySelector('.homepagehidden').style.display = 'block';
//                 break;
//             case '#/salespage':
//                 document.querySelector('.bcground').style.display = 'block';
//                 break;
//             case '#/login':
//                 document.querySelector('.log-reg-hidden').style.display = 'block';
//                 break;
//             case '#/contact':
//                 document.querySelector('.contacthidden').style.display = 'block';
//                 break;
//             default:
//                 document.querySelector('.homepagehidden').style.display = 'block';
//                 break;
//         }
//     }

//     window.addEventListener('hashchange', showSectionFromHash);
//     showSectionFromHash(); 

//     // Fetch function to get products from the server
//     function fetchProducts() {
//         fetch('/api/products') // Adjust to your actual API endpoint
//             .then(response => {
//                 if (!response.ok) {
//                     throw new Error('Network response was not ok');
//                 }
//                 return response.json();
//             })
//             .then(products => {
//                 displayProducts(products);
//             })
//             .catch(error => {
//                 console.error('Error fetching products:', error);
//             });
//     }

//     function displayProducts(products) {
//         const productsContainer = document.querySelector('.products-container');
//         productsContainer.innerHTML = ''; // Clear existing products
//         products.forEach(product => {
//             const productElement = document.createElement('div');
//             productElement.classList.add('product');
//             productElement.innerHTML = `
//                 <img src="${product.image}" alt="${product.name}">
//                 <h3>${product.name}</h3>
//                 <p class="price">${product.newPrice}</p>
//                 <button class="add-to-cart" data-id="${product.id}">Add to Cart</button>
//             `;
//             productsContainer.appendChild(productElement);

//             // Event listener for add to cart button
//             productElement.querySelector('.add-to-cart').addEventListener('click', () => {
//                 addItemToCart({
//                     id: product.id,
//                     name: product.name,
//                     image: product.image,
//                     newPrice: product.newPrice,
//                     quantity: 1
//                 });

//                 alert(`${product.name} has been added to your cart.`);
//             });
//         });
//     }

//     // Initial fetch of products when the page loads
//     fetchProducts();
// });


// ================================================================================== EXPERIMENT
// document.addEventListener('DOMContentLoaded', function () {
//     const shippingFormContainer = document.querySelector('.container-shipping-info');
//     const shippingFormButton = document.querySelector('.button-shipping-info');
//     const paymentInfoButton = document.querySelector('#payment-info');
//     const completeOrderButton = document.querySelector('.truck-button');
//     const homePageContent = document.querySelector('.homepagehidden');
//     const cartContent = document.querySelector('.cart-container');
//     const cartItemsContainer = document.querySelector('.cart-items');
//     const cartCountBadge = document.querySelector('#cart-count');
//     const cartLink = document.querySelector('#cart-link');
//     const emptyCartMessage = document.querySelector('.empty-cart-message');

//     shippingFormButton.disabled = true;
//     completeOrderButton.disabled = true;

//     const form = document.querySelector('.form-shipping-info');
//     const inputs = form.querySelectorAll('.field-shipping-info__input');

//     // Initialize event listeners
//     inputs.forEach(input => input.addEventListener('input', toggleShippingButton));
//     paymentInfoButton.addEventListener('click', toggleShippingForm);
//     shippingFormButton.addEventListener('click', handleShippingFormSubmit);
//     completeOrderButton.addEventListener('click', handleCompleteOrder);
//     cartLink.addEventListener('click', preventEmptyCartNavigation);

//     checkCart();

//     function toggleShippingButton() {
//         shippingFormButton.disabled = !Array.from(inputs).every(input => input.value.trim() !== '');
//     }

//     function toggleShippingForm() {
//         shippingFormContainer.style.display = (shippingFormContainer.style.display === 'block') ? 'none' : 'block';
//     }

//     function handleShippingFormSubmit(e) {
//         e.preventDefault();
//         completeOrderButton.disabled = false;

//         const shippingDetails = Array.from(inputs).reduce((acc, input) => {
//             acc[input.id] = input.value;
//             return acc;
//         }, {});

//         alert(`Successfully entered details:\n\n${Object.entries(shippingDetails).map(([key, value]) => `${key.charAt(0).toUpperCase() + key.slice(1)}: ${value}`).join('\n')}`);
//     }

//     function checkCart() {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             completeOrderButton.disabled = true;
//             cartCountBadge.textContent = '0';
//             cartContent.style.display = 'none';
//             emptyCartMessage.classList.remove('hidden');
//         } else {
//             completeOrderButton.disabled = false;
//             updateCartBadge(cartItems.length);
//             renderCartItems(cartItems);
//             updateCartSummary(cartItems);
//         }
//     }

//     function updateCartBadge(count) {
//         cartCountBadge.textContent = count;
//         cartCountBadge.classList.remove('hidden');
//         cartLink.classList.remove('disabled');
//         cartLink.style.pointerEvents = 'auto';
//         cartContent.style.display = 'block';
//         emptyCartMessage.classList.add('hidden');
//     }

//     function renderCartItems(cartItems) {
//         cartItemsContainer.innerHTML = '';
//         cartItems.forEach((item, index) => {
//             const cartItem = createCartItemElement(item, index);
//             cartItemsContainer.appendChild(cartItem);
//         });
//         attachCartItemEvents();
//     }

//     function createCartItemElement(item, index) {
//         const cartItem = document.createElement('div');
//         cartItem.classList.add('cart-item');
//         cartItem.innerHTML = `
//             <div class="item-image">
//                 <img src="${item.image}" alt="${item.name}">
//             </div>
//             <div class="item-details">
//                 <h3>${item.name}</h3>
//                 <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                 <div class="quantity">
//                     <label for="quantity${index}">Quantity:</label>
//                     <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                 </div>
//                 <button class="remove-item" data-index="${index}">Remove</button>
//             </div>
//         `;
//         return cartItem;
//     }

//     function attachCartItemEvents() {
//         document.querySelectorAll('.quantity input').forEach((input, index) => {
//             input.addEventListener('change', function () {
//                 updateQuantity(index, this.value);
//             });
//         });

//         document.querySelectorAll('.remove-item').forEach(button => {
//             button.addEventListener('click', function () {
//                 removeItem(this.getAttribute('data-index'));
//             });
//         });
//     }

//     function preventEmptyCartNavigation(e) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             e.preventDefault();
//             alert('Your cart is empty!');
//         }
//     }

//     function handleCompleteOrder(e) {
//         e.preventDefault();

//         if (!completeOrderButton.classList.contains("done")) {
//             if (!completeOrderButton.classList.contains("animation")) {
//                 completeOrderButton.classList.add("animation");
//                 startOrderAnimation();
//             }
//         } else {
//             resetOrderButton();
//         }
//     }

//     function startOrderAnimation() {
//         const box = completeOrderButton.querySelector(".box");
//         const truck = completeOrderButton.querySelector(".truck");

//         gsap.to(completeOrderButton, {
//             "--box-s": 1,
//             "--box-o": 1,
//             duration: 0.3,
//             delay: 0.5
//         });

//         gsap.to(box, {
//             x: 0,
//             duration: 0.4,
//             delay: 0.7
//         });

//         gsap.to(completeOrderButton, {
//             "--hx": -5,
//             "--bx": 50,
//             duration: 0.18,
//             delay: 0.92
//         });

//         gsap.to(box, {
//             y: 0,
//             duration: 0.1,
//             delay: 1.15
//         });

//         gsap.set(completeOrderButton, {
//             "--truck-y": 0,
//             "--truck-y-n": -26
//         });

//         gsap.to(completeOrderButton, {
//             "--truck-y": 1,
//             "--truck-y-n": -25,
//             duration: 0.2,
//             delay: 1.25,
//             onComplete: finalizeOrder
//         });
//     }

//     function finalizeOrder() {
//         const box = completeOrderButton.querySelector(".box");
//         const truck = completeOrderButton.querySelector(".truck");

//         gsap.timeline({
//             onComplete() {
//                 completeOrderButton.classList.add("done");
//                 localStorage.removeItem('cartItems');
//                 cartItemsContainer.innerHTML = '';
//                 checkCart();
//                 setTimeout(() => {
//                     cartContent.style.display = 'none';
//                     shippingFormContainer.style.display = 'none';
//                     homePageContent.style.display = 'block';
//                 }, 1000);
//             }
//         })
//         .to(truck, { x: 0, duration: 0.4 })
//         .to(truck, { x: 40, duration: 1 })
//         .to(truck, { x: 20, duration: 0.6 })
//         .to(truck, { x: 96, duration: 0.4 });

//         gsap.to(completeOrderButton, {
//             "--progress": 1,
//             duration: 2.4,
//             ease: "power2.in"
//         });
//     }

//     function resetOrderButton() {
//         const truck = completeOrderButton.querySelector(".truck");
//         completeOrderButton.classList.remove("animation", "done");
//         gsap.set(truck, { x: 4 });
//         gsap.set(completeOrderButton, {
//             "--progress": 0,
//             "--hx": 0,
//             "--bx": 0,
//             "--box-s": 0.5,
//             "--box-o": 0,
//             "--truck-y": 0,
//             "--truck-y-n": -26
//         });
//         gsap.set(completeOrderButton.querySelector(".box"), {
//             x: -24,
//             y: -6
//         });
//     }

//     function updateQuantity(index, quantity) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems[index].quantity = parseInt(quantity);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         updateCartSummary(cartItems);
//         checkCart();
//     }

//     function removeItem(index) {
//         const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
//         cartItems.splice(index, 1);
//         localStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();

//         if (cartItems.length === 0) {
//             homePageContent.style.display = 'block';
//             cartContent.style.display = 'none';
//         }
//     }

//     function updateCartSummary(cartItems) {
//         const subtotal = cartItems.reduce((total, item) => total + parseFloat(item.newPrice.replace(' EUR', '')) * item.quantity, 0);
//         document.getElementById('subtotal').textContent = subtotal.toFixed(2);
//         document.getElementById('total').textContent = subtotal.toFixed(2);
//     }

//     function showSectionFromHash() {
//         const hash = window.location.hash;
//         hideAllSections();

//         switch (hash) {
//             case '#/home':
//                 homePageContent.style.display = 'block';
//                 break;
//             case '#/cart':
//                 cartContent.style.display = 'block';
//                 break;
//             default:
//                 homePageContent.style.display = 'block';
//         }
//     }

//     function hideAllSections() {
//         homePageContent.style.display = 'none';
//         cartContent.style.display = 'none';
//     }

//     window.addEventListener('hashchange', showSectionFromHash);
//     showSectionFromHash();
// });

// document.addEventListener('DOMContentLoaded', function () {
//     const cartItemsContainer = document.querySelector('.cart-items');
//     const cartCountBadge = document.querySelector('#cart-count');
//     const cartContent = document.querySelector('.cart-container');
//     const emptyCartMessage = document.querySelector('.empty-cart-message'); 

//     // Add event listener to all "Add to Cart" buttons
//     document.querySelectorAll('.add-to-cart').forEach(button => {
//         button.addEventListener('click', function () {
//             const productId = this.getAttribute('data-product-id');
//             addToCart({ id: productId, name: "Product Name", newPrice: "15 EUR", image: "image-url.jpg" });
//         });
//     });

//     // Load cart items from sessionStorage
//     checkCart();

//     // Function to add an item to the cart
//     function addToCart(item) {
//         const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
//         const existingItemIndex = cartItems.findIndex(cartItem => cartItem.id === item.id);

//         if (existingItemIndex !== -1) {
//             cartItems[existingItemIndex].quantity += 1;
//         } else {
//             item.quantity = 1;
//             cartItems.push(item);
//         }

//         sessionStorage.setItem('cartItems', JSON.stringify(cartItems));

//         console.log("item addded", item);
//         console.log("current in cart", cartItem);

//         updateCartBadge(cartItems.length);
//         checkCart();
//     }

//     // Function to check and display the cart
//     function checkCart() {
//         const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
//         if (cartItems.length === 0) {
//             cartCountBadge.textContent = '0';
//             cartContent.style.display = 'none';
//             emptyCartMessage.classList.remove('hidden'); 
//         } else {
//             updateCartBadge(cartItems.length);
//             renderCartItems(cartItems);
//             updateCartSummary(cartItems);
//         }
//     }

//     // Function to update cart badge count
//     function updateCartBadge(count) {
//         cartCountBadge.textContent = count;
//         cartCountBadge.classList.remove('hidden');
//     }

//     // Function to render cart items in the cart section
//     function renderCartItems(cartItems) {
//         cartItemsContainer.innerHTML = '';
//         cartItems.forEach((item, index) => {
//             const cartItem = document.createElement('div');
//             cartItem.classList.add('cart-item');
//             cartItem.innerHTML = `
//                 <div class="item-image">
//                     <img src="${item.image}" alt="${item.name}">
//                 </div>
//                 <div class="item-details">
//                     <h3>${item.name}</h3>
//                     <p class="item-price" data-price="${item.newPrice}">Price: ${item.newPrice}</p>
//                     <div class="quantity">
//                         <label for="quantity${index}">Quantity:</label>
//                         <input type="number" id="quantity${index}" value="${item.quantity}" min="1">
//                     </div>
//                     <button class="remove-item" data-index="${index}">Remove</button>
//                 </div>
//             `;
//             cartItemsContainer.appendChild(cartItem);
//         });
//         attachCartItemEvents();
//     }

//     // Function to attach events for quantity changes and item removal
//     function attachCartItemEvents() {
//         document.querySelectorAll('.quantity input').forEach((input, index) => {
//             input.addEventListener('change', function () {
//                 updateQuantity(index, this.value);
//             });
//         });

//         document.querySelectorAll('.remove-item').forEach(button => {
//             button.addEventListener('click', function () {
//                 const index = this.getAttribute('data-index');
//                 removeItem(index);
//             });
//         });
//     }

//     // Function to update the quantity of a cart item
//     function updateQuantity(index, quantity) {
//         const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
//         cartItems[index].quantity = parseInt(quantity);
//         sessionStorage.setItem('cartItems', JSON.stringify(cartItems));
//         updateCartSummary(cartItems);
//         checkCart();
//     }

//     // Function to remove an item from the cart
//     function removeItem(index) {
//         const cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
//         cartItems.splice(index, 1);
//         sessionStorage.setItem('cartItems', JSON.stringify(cartItems));
//         checkCart();
//     }

//     // Function to update the cart summary (total price)
//     function updateCartSummary(cartItems) {
//         const subtotal = cartItems.reduce((total, item) => total + parseFloat(item.newPrice.replace(' EUR', '')) * item.quantity, 0);
//         document.getElementById('subtotal').textContent = subtotal.toFixed(2);
//         document.getElementById('total').textContent = subtotal.toFixed(2);
//     }
// });


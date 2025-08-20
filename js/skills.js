// // Skills Progress Bar Animation
// document.addEventListener('DOMContentLoaded', function() {
//     const skills = document.querySelectorAll('.skill');
//     const progressBars = document.querySelectorAll('.progress');

//     // Set up Intersection Observer for scroll animations
//     const observerOptions = {
//         threshold: 0.3,
//         rootMargin: '0px 0px -100px 0px'
//     };

//     const observer = new IntersectionObserver((entries) => {
//         entries.forEach(entry => {
//             if (entry.isIntersecting) {
//                 const skill = entry.target;
//                 const progressBar = skill.querySelector('.progress');
//                 const percent = progressBar.getAttribute('data-percent');
                
//                 // Add animation class
//                 skill.classList.add('animate');
                
//                 // Animate progress bar
//                 setTimeout(() => {
//                     progressBar.style.width = percent + '%';
//                 }, 200);

//                 // Animate percentage counter
//                 animateCounter(skill.querySelector('.percentage'), percent);
//             }
//         });
//     }, observerOptions);

//     // Observe all skill elements
//     skills.forEach(skill => {
//         observer.observe(skill);
//     });

//     // Counter animation function
//     function animateCounter(element, target) {
//         let current = 0;
//         const increment = target / 60; // 60 frames for smooth animation
//         const timer = setInterval(() => {
//             current += increment;
//             if (current >= target) {
//                 current = target;
//                 clearInterval(timer);
//             }
//             element.textContent = Math.floor(current) + '%';
//         }, 33); // ~30fps
//     }

//     // Add stagger animation for skills on initial load
//     skills.forEach((skill, index) => {
//         skill.style.animationDelay = `${index * 0.1}s`;
//         skill.classList.add('skill-fade-in');
//     });
// });

// // Add CSS for fade-in animation
// const style = document.createElement('style');
// style.textContent = `
//     .skill-fade-in {
//         opacity: 0;
//         transform: translateY(30px);
//         animation: fadeInUp 0.6s ease forwards;
//     }

//     @keyframes fadeInUp {
//         to {
//             opacity: 1;
//             transform: translateY(0);
//         }
//     }
// `;
// document.head.appendChild(style);

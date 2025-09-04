// ===== RESPONSIVE PORTFOLIO FUNCTIONALITY =====

// Mobile Menu Toggle
const menuBtn = document.getElementById('menu-btn');
const navbar = document.querySelector('.navbar');

// Toggle mobile menu
function toggleMobileMenu() {
  navbar.classList.toggle('active');
  menuBtn.classList.toggle('active');
}

// Event listener for menu button
if (menuBtn) {
  menuBtn.addEventListener('click', toggleMobileMenu);
}

// Close mobile menu when clicking on a link
const navLinks = document.querySelectorAll('.navbar a');
navLinks.forEach(link => {
  link.addEventListener('click', () => {
    if (navbar.classList.contains('active')) {
      navbar.classList.remove('active');
      menuBtn.classList.remove('active');
    }
  });
});

// Close mobile menu when clicking outside
document.addEventListener('click', (e) => {
  if (!navbar.contains(e.target) && !menuBtn.contains(e.target)) {
    if (navbar.classList.contains('active')) {
      navbar.classList.remove('active');
      menuBtn.classList.remove('active');
    }
  }
});

// Reset menu state on window resize
window.addEventListener('resize', () => {
  if (window.innerWidth > 990) {
    navbar.classList.remove('active');
    menuBtn.classList.remove('active');
  }
});

// ===== SMOOTH SCROLLING =====
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
  anchor.addEventListener('click', function (e) {
    e.preventDefault();
    const target = document.querySelector(this.getAttribute('href'));
    if (target) {
      target.scrollIntoView({
        behavior: 'smooth',
        block: 'start'
      });
    }
  });
});

// ===== ACTIVE NAVIGATION HIGHLIGHTING =====
function highlightActiveSection() {
  const sections = document.querySelectorAll('section[id]');
  const navLinks = document.querySelectorAll('.navbar a[href^="#"]');
  
  let current = '';
  const scrollPosition = window.pageYOffset;
  
  sections.forEach(section => {
    const sectionTop = section.offsetTop;
    const sectionHeight = section.clientHeight;
    
    if (scrollPosition >= sectionTop - 200) {
      current = section.getAttribute('id');
    }
  });
  
  navLinks.forEach(link => {
    link.classList.remove('active');
    if (link.getAttribute('href') === `#${current}`) {
      link.classList.add('active');
    }
  });
}

// Add scroll event listener for active section highlighting
window.addEventListener('scroll', highlightActiveSection);

// ===== RESPONSIVE IMAGE LOADING =====
function optimizeImagesForDevice() {
  const images = document.querySelectorAll('img');
  const devicePixelRatio = window.devicePixelRatio || 1;
  const screenWidth = window.innerWidth;
  
  images.forEach(img => {
    // Add loading="lazy" for better performance
    if (!img.hasAttribute('loading')) {
      img.setAttribute('loading', 'lazy');
    }
    
    // Add error handling for images
    img.addEventListener('error', function() {
      console.warn('Failed to load image:', this.src);
      // You can add a fallback image here if needed
    });
  });
}

// ===== RESPONSIVE VIEWPORT HEIGHT FIX FOR MOBILE =====
function setViewportHeight() {
  const vh = window.innerHeight * 0.01;
  document.documentElement.style.setProperty('--vh', `${vh}px`);
}

// Set viewport height on load and resize
setViewportHeight();
window.addEventListener('resize', setViewportHeight);

// ===== ORIENTATION CHANGE HANDLING =====
function handleOrientationChange() {
  // Close mobile menu on orientation change
  if (navbar && navbar.classList.contains('active')) {
    toggleMobileMenu();
  }
  
  // Recalculate viewport height
  setTimeout(setViewportHeight, 100);
}

window.addEventListener('orientationchange', handleOrientationChange);

// ===== TOUCH SWIPE GESTURES FOR MOBILE =====
let touchStartX = 0;
let touchEndX = 0;

function handleSwipe() {
  const swipeThreshold = 100;
  const swipeDistance = touchEndX - touchStartX;
  
  // Right swipe to open menu
  if (swipeDistance > swipeThreshold && touchStartX < 50) {
    if (!navbar.classList.contains('active')) {
      toggleMobileMenu();
    }
  }
  
  // Left swipe to close menu
  if (swipeDistance < -swipeThreshold && navbar.classList.contains('active')) {
    toggleMobileMenu();
  }
}

document.addEventListener('touchstart', e => {
  touchStartX = e.changedTouches[0].screenX;
});

document.addEventListener('touchend', e => {
  touchEndX = e.changedTouches[0].screenX;
  handleSwipe();
});

// ===== PERFORMANCE OPTIMIZATION =====
// Debounce function for scroll events
function debounce(func, wait) {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      clearTimeout(timeout);
      func(...args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  };
}

// Apply debounce to scroll events
const debouncedHighlight = debounce(highlightActiveSection, 16);
window.removeEventListener('scroll', highlightActiveSection);
window.addEventListener('scroll', debouncedHighlight);

// ===== ACCESSIBILITY IMPROVEMENTS =====
// Keyboard navigation for mobile menu
document.addEventListener('keydown', (e) => {
  if (e.key === 'Escape' && navbar.classList.contains('active')) {
    toggleMobileMenu();
  }
});

// Focus management for mobile menu
menuBtn.addEventListener('keydown', (e) => {
  if (e.key === 'Enter' || e.key === ' ') {
    e.preventDefault();
    toggleMobileMenu();
  }
});

// ===== INITIALIZE ON DOM CONTENT LOADED =====
document.addEventListener('DOMContentLoaded', () => {
  optimizeImagesForDevice();
  highlightActiveSection();
  
  // Add smooth transition class after page load
  document.body.classList.add('loaded');
  
  console.log('Responsive portfolio initialized successfully!');
});

// ===== WINDOW RESIZE HANDLING =====
let resizeTimeout;
window.addEventListener('resize', () => {
  clearTimeout(resizeTimeout);
  resizeTimeout = setTimeout(() => {
    // Close mobile menu on desktop resize
    if (window.innerWidth > 768 && navbar.classList.contains('active')) {
      toggleMobileMenu();
    }
    
    // Recalculate any responsive elements if needed
    setViewportHeight();
  }, 250);
});

// ===== LAZY LOADING FOR BETTER PERFORMANCE =====
if ('IntersectionObserver' in window) {
  const lazyImages = document.querySelectorAll('img[data-src]');
  const imageObserver = new IntersectionObserver((entries, observer) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        const img = entry.target;
        img.src = img.dataset.src;
        img.classList.remove('lazy');
        imageObserver.unobserve(img);
      }
    });
  });
  
  lazyImages.forEach(img => imageObserver.observe(img));
}

// ===== ERROR HANDLING =====
window.addEventListener('error', (e) => {
  console.error('JavaScript error:', e.error);
});

// ===== PREFERS REDUCED MOTION =====
const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)');

if (prefersReducedMotion.matches) {
  // Disable animations for users who prefer reduced motion
  document.documentElement.style.setProperty('--animation-duration', '0s');
  document.documentElement.style.setProperty('--transition-duration', '0s');
}
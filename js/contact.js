// Contact form functionality

document.addEventListener('DOMContentLoaded', function() {
  const contactForm = document.getElementById('contactForm');
  const inputs = document.querySelectorAll('.input-group input, .input-group textarea');

  // Add floating label functionality
  inputs.forEach(input => {
    input.addEventListener('blur', function() {
      if (this.value) {
        this.classList.add('has-value');
      } else {
        this.classList.remove('has-value');
      }
    });
  });

  // Handle form submission
  contactForm.addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(contactForm);
    const name = formData.get('name');
    const email = formData.get('email');
    const subject = formData.get('subject');
    const message = formData.get('message');

    // Basic validation
    if (!name || !email || !subject || !message) {
      alert('Please fill in all fields');
      return;
    }

    // Email validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      alert('Please enter a valid email address');
      return;
    }
    const submitButton = contactForm.querySelector('.contact-btn');
    const originalText = submitButton.textContent;
    
    submitButton.textContent = 'Sending...';
    submitButton.disabled = true;

    
    setTimeout(() => {
      alert('Message sent successfully! I will get back to you soon.');
      contactForm.reset();
      inputs.forEach(input => input.classList.remove('has-value'));
      submitButton.textContent = originalText;
      submitButton.disabled = false;
    }, 2000);
  });
});

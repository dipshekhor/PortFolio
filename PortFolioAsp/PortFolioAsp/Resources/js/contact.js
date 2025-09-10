// Contact form functionality

document.addEventListener('DOMContentLoaded', function() {
  const inputs = document.querySelectorAll('.input-group input, .input-group textarea');

  // Add floating label functionality for all inputs
  inputs.forEach(input => {
    // Check initial state
    if (input.value && input.value.trim() !== '') {
      input.classList.add('has-value');
    }
    
    input.addEventListener('blur', function() {
      if (this.value && this.value.trim() !== '') {
        this.classList.add('has-value');
      } else {
        this.classList.remove('has-value');
      }
    });
    
    input.addEventListener('focus', function() {
      this.classList.add('has-value');
    });
  });

  // Check if this is an ASP.NET form (more comprehensive detection)
  const hasAspNetControls = document.querySelector('input[id*="txtContact"]') || 
                           document.querySelector('input[id*="ContentPlaceHolder"]') ||
                           document.querySelector('form[id="form1"]') ||
                           document.querySelector('input[id*="MainContent"]') ||
                           document.querySelector('[id*="btnSendMessage"]');
  
  // Only handle client-side form submission if this is NOT an ASP.NET form
  const contactForm = document.getElementById('contactForm');
  
  if (contactForm && !hasAspNetControls) {
    console.log('Setting up static HTML contact form handler');
    // Handle form submission for static HTML forms only
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
  } else if (hasAspNetControls) {
    console.log('ASP.NET form detected - allowing server-side submission');
  } else {
    console.log('No contact form found');
  }
  
  // Debug logging
  console.log('Contact form setup complete:', {
    contactFormFound: !!contactForm,
    aspNetControlsDetected: !!hasAspNetControls,
    txtContactControls: document.querySelectorAll('input[id*="txtContact"]').length
  });
});

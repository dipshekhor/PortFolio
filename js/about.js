// About section interactive logic

document.addEventListener('DOMContentLoaded', function() {
  const learnMoreBtn = document.getElementById('learnMoreBtn');
  const minimizeBtn = document.getElementById('minimizeBtn');
  const aboutOverview = document.getElementById('about-overview');
  const aboutDetailsPanel = document.getElementById('about-details-panel');
  const educationTab = document.getElementById('educationTab');
  const experienceTab = document.getElementById('experienceTab');
  const educationContent = document.getElementById('educationContent');
  const experienceContent = document.getElementById('experienceContent');


    const aboutImg = document.querySelector('.about-img');
      const aboutDivider = document.querySelector('.about-divider');
      const aboutContentBox = document.querySelector('.about-content-box');

  // learnMoreBtn.addEventListener('click', function() {
  //   aboutOverview.style.display = 'none';
  //   aboutDetailsPanel.classList.add('active');
  // });
   learnMoreBtn.addEventListener('click', function() {
        aboutImg.classList.add('hide');
        aboutDivider.classList.add('hide');
        aboutContentBox.classList.add('fullwidth');
        aboutOverview.style.display = 'none';
        aboutDetailsPanel.classList.add('active');
        
      })
  // minimizeBtn.addEventListener('click', function() {
  //   aboutDetailsPanel.classList.remove('active');
  //   aboutOverview.style.display = 'block';
  // });
 minimizeBtn.addEventListener('click', function() {
        aboutImg.classList.remove('hide');
        aboutDivider.classList.remove('hide');
        aboutContentBox.classList.remove('fullwidth');

        aboutDetailsPanel.classList.remove('active');
        aboutOverview.style.display = 'block';
        // learnMoreBtn.setAttribute('aria-expanded', 'false');
      });
  // educationTab.addEventListener('click', function() {
  //   educationTab.classList.add('active');
  //   experienceTab.classList.remove('active');
  //   educationContent.style.display = 'block';
  //   experienceContent.style.display = 'none';
  // });

  // experienceTab.addEventListener('click', function() {
  //   experienceTab.classList.add('active');
  //   educationTab.classList.remove('active');
  //   educationContent.style.display = 'none';
  //   experienceContent.style.display = 'block';
  // });
    educationTab.addEventListener('click', function() {
        educationTab.classList.add('active');
        experienceTab.classList.remove('active');
        educationContent.style.display = 'block';
        experienceContent.style.display = 'none';
      });

      experienceTab.addEventListener('click', function() {
        experienceTab.classList.add('active');
        educationTab.classList.remove('active');
        educationContent.style.display = 'none';
        experienceContent.style.display = 'block';
      });
});

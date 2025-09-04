// Photography Modal Functionality

// Photo data with metadata
const photoData = {
  photo1: {
    src: 'images/football.jpg',
    title: 'Football Match',
    location: 'Bangladesh',
    date: 'August 15, 2023',
    description: 'A dynamic football match capturing the passion and energy of the sport.'
  },
  photo2: {
    src: 'images/Single_Color_04_Dip Shekhor Datta_Finally,I have done it !_KUET_01772895851_dips8137@gmail.com.jpg',
    title: 'Finally, I have done it!',
    location: 'KUET, Bangladesh',
    date: 'September 22, 2023',
    description: 'A moment of triumph and achievement captured in this powerful photograph.'
  },
  photo3: {
    src: 'images/Single_Color_03_Dip Shekhor Datta_বিশ্বস্ততা_KUET_01772895851_dips8137@gmail.com.jpg',
    title: 'বিশ্বস্ততা (Loyalty)',
    location: 'KUET, Bangladesh',
    date: 'October 8, 2023',
    description: 'A beautiful portrayal of loyalty and trust.'
  },
  photo4: {
    src: 'images/Single_Color_05_Dip Shekhor Datta_মমতাময়ী মা_KUET_01772895851_dips8137@gmail.com.jpg',
    title: 'মমতাময়ী মা (Loving Mother)',
    location: 'KUET, Bangladesh',
    date: 'July 12, 2023',
    description: 'A tender moment showcasing the unconditional love of a mother.'
  },
  photo5: {
    src: 'images/Single_Color_07_Dip Shekhor Datta_Football in a beach_KUET_01772895851_dips8137@gmail.com.jpg',
    title: 'Football in a Beach',
    location: 'Beach, Bangladesh',
    date: 'November 3, 2023',
    description: 'An exciting beach football scene where the sand meets sport.'
  },
  photo6: {
    src: 'images/Single_Color_08_Dip Shekhor Datta_Holy and harmony_KUET_01772895851_dips8137@gmail.com.jpg',
    title: 'Holy and Harmony',
    location: 'KUET, Bangladesh',
    date: 'June 28, 2023',
    description: 'A peaceful scene depicting spiritual harmony and tranquility.'
  }
};

// Function to open photo modal
function openPhotoModal(photoId) {
  const modal = document.getElementById('photoModal');
  
  if (modal) {
    modal.style.display = 'block';
    modal.style.position = 'fixed';
    modal.style.top = '0';
    modal.style.left = '0';
    modal.style.width = '100%';
    modal.style.height = '100%';
    modal.style.backgroundColor = 'rgba(0,0,0,0.8)';
    modal.style.zIndex = '99999';
    
    const photo = photoData[photoId];
    if (photo) {
      const modalImage = document.getElementById('modalImage');
      const modalTitle = document.getElementById('modalTitle');
      const modalLocation = document.getElementById('modalLocation');
      const modalDate = document.getElementById('modalDate');
      const modalDescription = document.getElementById('modalDescription');
      
      if (modalImage) modalImage.src = photo.src;
      if (modalTitle) modalTitle.textContent = photo.title;
      if (modalLocation) modalLocation.textContent = photo.location;
      if (modalDate) modalDate.textContent = photo.date;
      if (modalDescription) modalDescription.textContent = photo.description;
    }
  }
}

// Function to close photo modal
function closePhotoModal() {
  const modal = document.getElementById('photoModal');
  if (modal) {
    modal.style.display = 'none';
    document.body.style.overflow = 'auto';
  }
}

// Make functions global
window.openPhotoModal = openPhotoModal;
window.closePhotoModal = closePhotoModal;

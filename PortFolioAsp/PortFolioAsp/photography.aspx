<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="photography.aspx.cs" Inherits="PortFolioAsp.photography" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="Resources/css/styles.css"> 
  <link rel="stylesheet" href="Resources/css/photography-section.css"> 
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" crossorigin="anonymous" referrerpolicy="no-referrer" />
  <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
  <link href="https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,600&display=swap" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <header class="header">
    <a href="main.aspx#home" class="logo">Dip Shekhor Datta</a>
    <nav class="navbar">
      <ul>
        <a href="main.aspx#home">Home</a>
        <a href="main.aspx#about">About</a>
        <a href="main.aspx#skills">Skills</a>
        <a href="main.aspx#projects">Projects</a>
        <a href="photography.aspx" class="active">Photography</a>
        <a href="main.aspx#contact">Contact</a>
      </ul>
    </nav>
    <div class="hamburger-menu" id="menu-btn">
      <span></span>
      <span></span>
      <span></span>
    </div>
  </header>
  <section class="photography" id="photography">
    <h2 class="heading">My<span> Photography</span></h2>
    
    <div class="photography-container">
      <div class="photo-card" onclick="openPhotoModal('photo1')" style="cursor: pointer;">
        <div class="photo-image">
          <img src="Resources/images/football.jpg" alt="Football Match">
          <div class="photo-overlay">
            <i class='fas fa-camera'></i>
            <h3>Football Match</h3>
          </div>
        </div>
      </div>
      <div class="photo-card" onclick="openPhotoModal('photo2')">
        <div class="photo-image">
          <img src="Resources/images/Single_Color_04_Dip Shekhor Datta_Finally,I have done it !_KUET_01772895851_dips8137@gmail.com.jpg" alt="Finally, I have done it!">
          <div class="photo-overlay">
            <i class='fas fa-camera'></i>
            <h3>Finally, I have done it!</h3>
          </div>
        </div>
      </div>
      <div class="photo-card" onclick="openPhotoModal('photo3')">
        <div class="photo-image">
          <img src="Resources/images/Single_Color_03_Dip Shekhor Datta_বিশ্বস্ততা_KUET_01772895851_dips8137@gmail.com.jpg" alt="বিশ্বস্ততা">
          <div class="photo-overlay">
            <i class='fas fa-camera'></i>
            <h3>বিশ্বস্ততা</h3>
          </div>
        </div>
      </div>
      <div class="photo-card" onclick="openPhotoModal('photo5')">
        <div class="photo-image">
          <img src="Resources/images/Single_Color_07_Dip Shekhor Datta_Football in a beach_KUET_01772895851_dips8137@gmail.com.jpg" alt="Football in a beach">
          <div class="photo-overlay">
            <i class='fas fa-camera'></i>
            <h3>Football in a beach</h3>
          </div>
        </div>
      </div>
      <div class="photo-card" onclick="openPhotoModal('photo6')">
        <div class="photo-image">
          <img src="Resources/images/Single_Color_08_Dip Shekhor Datta_Holy and harmony_KUET_01772895851_dips8137@gmail.com.jpg" alt="Holy and harmony">
          <div class="photo-overlay">
            <i class='fas fa-camera'></i>
            <h3>Holy and harmony</h3>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- Photo Modal -->
  <div class="photo-modal" id="photoModal">
    <div class="modal-overlay" onclick="closePhotoModal()"></div>
    <div class="modal-content">
      <button class="close-btn" onclick="closePhotoModal()">&times;</button>
      <div class="modal-image-container">
        <img id="modalImage" src="" alt="">
      </div>
      <div class="modal-details">
        <h2 id="modalTitle"></h2>
        <div class="photo-info">
          <div class="info-item">
            <i class='fas fa-map-marker-alt'></i>
            <span id="modalLocation"></span>
          </div>
          <div class="info-item">
            <i class='fas fa-calendar-alt'></i>
            <span id="modalDate"></span>
          </div>
        </div>
        <p id="modalDescription"></p>
      </div>
    </div>
  </div>
  <!-- Bootstrap bundle JS (includes Popper) -->
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
  <script src="Resources/js/main.js"></script>
  <script src="Resources/js/photography.js"></script>
  <script src="Resources/js/contact.js"></script>
</asp:Content>

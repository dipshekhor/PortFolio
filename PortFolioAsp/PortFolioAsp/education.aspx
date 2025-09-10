<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="education.aspx.cs" Inherits="PortFolioAsp.education" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="Resources/css/styles.css">
  <link rel="stylesheet" href="Resources/css/education-section.css">
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
        <a href="main.aspx#about" class="active">About</a>
        <a href="main.aspx#skills">Skills</a>
        <a href="main.aspx#projects">Projects</a>
        <a href="photography.aspx">Photography</a>
        <a href="main.aspx#contact">Contact</a>
      </ul>
    </nav>
    <div class="hamburger-menu" id="menu-btn">
      <span></span>
      <span></span>
      <span></span>
    </div>
  </header>
  
  <section class="education" id="education">
    <h2 class="heading">My <span>Education</span></h2>
    <div class="education-timeline">
      <div class="timeline-main-branch"></div>
      <div class="education-cards">
        <asp:Repeater ID="rptEducation" runat="server">
          <ItemTemplate>
            <div class="education-card">
              <h3><i class="fas fa-graduation-cap"></i> <%# Eval("Degree") %></h3>
              <h4><%# Eval("Institution") %></h4>
              <p class="institution"><%# Eval("Field") %></p>
              <div class="year"><%# Eval("StartYear") %> - <%# Eval("EndYear") %></div>
              <div class="grade">Grade: <%# Eval("Grade") %></div>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
    <a href="main.aspx#about" class="btn" style="margin-top:2rem;">&larr; Back to About</a>
  </section>

  <!-- Bootstrap bundle JS -->
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/js/bootstrap.bundle.min.js"></script>
  <!-- Custom JS -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
  <script src="Resources/js/main.js"></script>
</asp:Content>

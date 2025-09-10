<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="PortFolioAsp.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Ds Datta</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/css/bootstrap.min.css" rel="stylesheet">
   <!-- Custom & Icon Fonts -->
  <link rel="stylesheet" href="Resources/css/styles.css"> 
  <link rel="stylesheet" href="Resources/css/skills-section.css"> 
  <link rel="stylesheet" href="Resources/css/projects-section.css"> 
  <link rel="stylesheet" href="Resources/css/contact-section.css"> 
  <link rel="stylesheet" href="Resources/css/photography-section.css">
  <link rel="stylessheet" href="Resources/css/education-section.css">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" crossorigin="anonymous" referrerpolicy="no-referrer" />
  <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
  <link href="https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,600&display=swap" rel="stylesheet">
  
  <style>
    /* Contact Form Status Messages */
    .contact-status {
      display: block;
      margin-top: 1rem;
      padding: 1rem;
      border-radius: 0.5rem;
      font-size: 1rem;
      font-weight: 500;
      text-align: center;
    }
    
    .contact-status.success {
      background-color: rgba(40, 167, 69, 0.1);
      color: #28a745;
      border: 1px solid rgba(40, 167, 69, 0.3);
    }
    
    .contact-status.error {
      background-color: rgba(220, 53, 69, 0.1);
      color: #dc3545;
      border: 1px solid rgba(220, 53, 69, 0.3);
    }
    
    /* Ensure ASP.NET controls inherit form styling */
    .form-input {
      width: 100%;
      padding: 1rem;
      border: none;
      border-bottom: 2px solid #ddd;
      background: transparent;
      font-size: 1rem;
      color: #333;
      transition: border-color 0.3s ease;
    }
    
    .form-input:focus {
      outline: none;
      border-bottom-color: #007bff;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <header class="header">
    <a href="#home" class="logo">Dip Shekhor Datta</a>
    <nav class="navbar">
      <ul>
        <a href="#home">Home</a>
        <a href="#about">About</a>
        <a href="#skills">Skills</a>
        <a href="#projects">Projects</a>
        <a href="photography.aspx">Photography</a>
        <a href="#contact">Contact</a>
        <a href="#experience" style="display: none;">Experience</a>
      </ul>
    </nav>
    <div class="hamburger-menu" id="menu-btn">
      <span></span>
      <span></span>
      <span></span>
    </div>
  </header>
  <!-- Home Section -->
  <section class="home" id="home">
    <div class="content">
      <h3>Hi, I am</h3>
      <h1>Dip Shekhor Datta</h1>
      <h3><span> A 3rd Year CS Student with an Exploring Mentality</span></h3>
      <p>Welcome to my portfolio! I am a passionate Computer Science student with an insatiable curiosity for technology and innovation. My journey in the world of programming is driven by exploration, continuous learning, and the desire to capture and create meaningful digital experiences.</p>
      <p>Explore my learning journey, projects, and the technologies I'm mastering as I grow in the field of Computer Science.</p>

      <div class="social-platforms">
        <a href="#"><i class="fa-brands fa-facebook"></i></a>
        <a href="#"><i class="fa-brands fa-linkedin"></i></a>
        <a href="#"><i class="fa-brands fa-github"></i></a>
        <a href="#"><i class="fa-brands fa-instagram"></i></a>
      </div>
      
      <div class="btn-group">
        <a href="#contact" class="btn">View My Resume</a>
      </div>

    </div>
    <div class="homepage-img">
        <img src="Resources/images/myself2.jpg" />
      
    </div>
    
  </section>
  <!-- About Section -->
  <section class="about" id="about">
    <div class="about-section-container">
      <div class="about-img">
        <img src="Resources/images/dip.jpg" alt="About Image">
      </div>
      <div class="about-divider"></div>
      <div class="about-content-box">
        <div class="about-overview" id="about-overview">
          <h2 class="heading">ABOUT<span> Me</span></h2>
          <h3>Computer Science Student & Tech Explorer</h3>
          <p>I am a dedicated Computer Science student in my 3rd year with a passion for exploring emerging technologies and innovative solutions. My academic journey has ignited a deep fascination for programming, algorithms, and the endless possibilities that technology offers to shape our world.</p>
              <a class="btn" href="education.aspx">Learn More</a>
        </div>
      </div>
    </div>
  </section>
  <!-- Skills Section -->
  <section class="skills" id="skills">
    <h2 class="heading">My<span> Skills</span></h2>
    <div class="skills-subsection">
      <h3>Languages</h3>
      <div class="skills-container">
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-cuttlefish'></i></div>
          <h3>C++</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-java'></i></div>
          <h3>Java</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-php'></i></div>
          <h3>PHP</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-python'></i></div>
          <h3>Python</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-js-square'></i></div>
          <h3>JavaScript</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-html5'></i></div>
          <h3>HTML</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-css3-alt'></i></div>
          <h3>CSS</h3>
        </div>
      </div>
    </div>
    <div class="skills-subsection">
      <h3>Frameworks</h3>
      <div class="skills-container">
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-react'></i></div>
          <h3>React</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-node-js'></i></div>
          <h3>Node.js</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-laravel'></i></div>
          <h3>Laravel</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-python'></i></div>
          <h3>Django</h3>
        </div>
      </div>
    </div>
    <div class="skills-subsection">
      <h3>Tools</h3>
      <div class="skills-container">
        <div class="skill">
          <div class="skill-icon"><i class='fab fa-android'></i></div>
          <h3>Android Studio</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fas fa-camera'></i></div>
          <h3>Adobe Lightroom</h3>
        </div>
        <div class="skill">
          <div class="skill-icon"><i class='fas fa-video'></i></div>
          <h3>Adobe Premiere Pro</h3>
        </div>
      </div>
    </div>
  </section>
  
  <!-- Projects Section -->
  <section class="projects" id="projects">
    <h2 class="heading">My<span> Projects</span></h2>
    <div class="projects-container">
      <asp:Repeater ID="rptProjects" runat="server">
        <ItemTemplate>
          <div class="project-card">
            <div class="project-image">
              <img src='<%# Eval("ImagePath") %>' alt='<%# Eval("Title") %>'>
              <div class="project-overlay">
                <a href='<%# Eval("URL") %>' target="_blank" class="project-overlay-link"><i class='bx bx-link-external'></i></a>
              </div>
            </div>
            <div class="project-content">
              <h3><%# Eval("Title") %></h3>
              <p><%# Eval("Description") %></p>
              <div class="project-tech">
                <%# GetTechTags(Eval("Technologies").ToString()) %>
              </div>
              <a href='<%# Eval("URL") %>' target="_blank" class="btn project-btn">Read More</a>
            </div>
          </div>
        </ItemTemplate>
      </asp:Repeater>
    </div>
  </section>
  
  <!-- Contact Section -->
  <section class="contact" id="contact">
    <h2 class="heading">Contact<span> Me</span></h2>
    <div class="contact-container">
      <div class="contact-info">
        <div class="contact-card">
          <div class="contact-icon">
            <i class='bx bxs-phone'></i>
          </div>
          <h3>Phone</h3>
          <p>+880 1772895851</p>
        </div>
        <div class="contact-card">
          <div class="contact-icon">
            <i class='bx bxs-envelope'></i>
          </div>
          <h3>Email</h3>
          <p>dipshekhordatta@gmail.com</p>
        </div>
        <div class="contact-card">
          <div class="contact-icon">
            <i class='bx bxs-location-plus'></i>
          </div>
          <h3>Location</h3>
          <p>Khulna, Bangladesh</p>
        </div>
      </div>
      <div class="contact-form">
        <div class="form-container">
          <div class="input-group">
            <asp:TextBox ID="txtContactName" runat="server" CssClass="form-input" required="true"></asp:TextBox>
            <label for="<%= txtContactName.ClientID %>">Your Name</label>
          </div>
          <div class="input-group">
            <asp:TextBox ID="txtContactEmail" runat="server" TextMode="Email" CssClass="form-input" required="true"></asp:TextBox>
            <label for="<%= txtContactEmail.ClientID %>">Your Email</label>
          </div>
          <div class="input-group">
            <asp:TextBox ID="txtContactSubject" runat="server" CssClass="form-input" required="true"></asp:TextBox>
            <label for="<%= txtContactSubject.ClientID %>">Subject</label>
          </div>
          <div class="input-group">
            <asp:TextBox ID="txtContactMessage" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-input" required="true"></asp:TextBox>
            <label for="<%= txtContactMessage.ClientID %>">Your Message</label>
          </div>
          <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" CssClass="btn contact-btn" OnClick="btnSendMessage_Click" />
          <asp:Label ID="lblContactMessage" runat="server" CssClass="contact-status" Visible="false"></asp:Label>
        </div>
      </div>
    </div>
  </section>
  
  <!-- Bootstrap bundle JS (includes Popper) -->
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.6/dist/js/bootstrap.bundle.min.js" integrity="sha384-4Q6Gf2aSP4eDXB8Miphtr37CMZZQ5oXLH2yaXMJ2w8e2ZtHTl7GptT4jmndRuHDT" crossorigin="anonymous"></script>
  <!-- Custom JS -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
  <script src="Resources/js/main.js"></script>
  <script src="Resources/js/skills.js"></script>
  <script src="Resources/js/about.js"></script>
  <script src="Resources/js/contact.js"></script>
  <script src="Resources/js/photography.js"></script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="PortFolioAsp.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,400;14..32,500;14..32,600;14..32,700&display=swap" rel="stylesheet">
    
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Inter', sans-serif;
            background: linear-gradient(135deg, #1a1e29 0%, #2a2f3a 100%);
            min-height: 100vh;
            color: #ffffff;
        }

        .admin-container {
            min-height: 100vh;
            display: flex;
            background: linear-gradient(135deg, #1a1e29 0%, #2a2f3a 100%);
        }

        /* Sidebar Styles */
        .sidebar {
            width: 280px;
            background: linear-gradient(145deg, #1a1f35, #252b40);
            border-right: 2px solid rgba(89, 178, 244, 0.2);
            display: flex;
            flex-direction: column;
            position: fixed;
            height: 100vh;
            overflow-y: auto;
        }

        .sidebar-header {
            padding: 2rem;
            border-bottom: 1px solid rgba(89, 178, 244, 0.2);
            text-align: center;
        }

        .sidebar-header h2 {
            font-size: 1.8rem;
            color: #59b2f4;
            margin-bottom: 0.5rem;
        }

        .sidebar-header p {
            font-size: 1rem;
            color: #858383;
        }

        .sidebar-nav {
            flex: 1;
            padding: 1rem 0;
        }

        .nav-item {
            display: block;
            padding: 1rem 2rem;
            color: #858383;
            text-decoration: none;
            font-size: 1.1rem;
            transition: all 0.3s ease;
            border-left: 3px solid transparent;
            cursor: pointer;
        }

        .nav-item:hover, .nav-item.active {
            background: rgba(89, 178, 244, 0.1);
            color: #59b2f4;
            border-left-color: #59b2f4;
        }

        .nav-item i {
            margin-right: 0.8rem;
            width: 20px;
        }

        .sidebar-footer {
            padding: 2rem;
            border-top: 1px solid rgba(89, 178, 244, 0.2);
        }

        .logout-btn {
            width: 100%;
            padding: 0.8rem;
            background: linear-gradient(135deg, #dc3545, #c82333);
            border: none;
            border-radius: 0.5rem;
            color: white;
            font-size: 1rem;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .logout-btn:hover {
            background: linear-gradient(135deg, #c82333, #dc3545);
            transform: translateY(-2px);
        }

        /* Main Content Styles */
        .main-content {
            flex: 1;
            margin-left: 280px;
            padding: 2rem;
        }

        .content-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 2rem;
            padding-bottom: 1rem;
            border-bottom: 2px solid rgba(89, 178, 244, 0.2);
        }

        .content-header h1 {
            font-size: 2.2rem;
            color: #ffffff;
        }

        .user-info {
            display: flex;
            align-items: center;
            gap: 1rem;
            color: #858383;
        }

        .user-info i {
            color: #59b2f4;
        }

        /* Dashboard Cards */
        .dashboard-cards {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 1.5rem;
            margin-bottom: 2rem;
        }

        .dashboard-card {
            background: linear-gradient(145deg, #1a1f35, #252b40);
            border-radius: 1rem;
            padding: 1.5rem;
            border: 2px solid transparent;
            transition: all 0.3s ease;
        }

        .dashboard-card:hover {
            border-color: #59b2f4;
            box-shadow: 0 10px 30px rgba(89, 178, 244, 0.3);
        }

        .card-header {
            display: flex;
            justify-content: between;
            align-items: center;
            margin-bottom: 1rem;
        }

        .card-icon {
            width: 50px;
            height: 50px;
            background: linear-gradient(135deg, #59b2f4, #4facfe);
            border-radius: 0.5rem;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 1rem;
        }

        .card-icon i {
            font-size: 1.5rem;
            color: white;
        }

        .card-title {
            font-size: 1.1rem;
            color: #858383;
            margin-bottom: 0.5rem;
        }

        .card-value {
            font-size: 2rem;
            font-weight: 700;
            color: #ffffff;
        }

        /* Content Sections */
        .content-section {
            background: linear-gradient(145deg, #1a1f35, #252b40);
            border-radius: 1rem;
            padding: 2rem;
            margin-bottom: 2rem;
            border: 2px solid rgba(89, 178, 244, 0.2);
            display: none;
        }

        .content-section.active {
            display: block;
        }

        .section-title {
            font-size: 1.8rem;
            color: #ffffff;
            margin-bottom: 1.5rem;
            display: flex;
            align-items: center;
            gap: 1rem;
        }

        .section-title i {
            color: #59b2f4;
        }

        /* Form Styles */
        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 1.5rem;
            margin-bottom: 2rem;
        }

        .form-group {
            display: flex;
            flex-direction: column;
        }

        .form-group label {
            color: #59b2f4;
            font-size: 1rem;
            margin-bottom: 0.5rem;
            font-weight: 500;
        }

        .form-group input,
        .form-group textarea,
        .form-group select {
            padding: 0.8rem;
            background: rgba(89, 178, 244, 0.1);
            border: 2px solid rgba(89, 178, 244, 0.3);
            border-radius: 0.5rem;
            color: #ffffff;
            font-size: 1rem;
            transition: all 0.3s ease;
        }

        .form-group input:focus,
        .form-group textarea:focus,
        .form-group select:focus {
            outline: none;
            border-color: #59b2f4;
            box-shadow: 0 0 10px rgba(89, 178, 244, 0.3);
        }

        .form-group textarea {
            resize: vertical;
            min-height: 100px;
        }

        /* DropDownList Specific Styles */
        select,
        .form-group select,
        [id*="ddl"],
        .form-control {
            padding: 0.8rem !important;
            background: rgba(89, 178, 244, 0.1) !important;
            border: 2px solid rgba(89, 178, 244, 0.3) !important;
            border-radius: 0.5rem !important;
            color: #ffffff !important;
            font-size: 1rem !important;
            transition: all 0.3s ease !important;
            min-height: 45px !important;
        }

        select:focus,
        .form-group select:focus,
        [id*="ddl"]:focus,
        .form-control:focus {
            outline: none !important;
            border-color: #59b2f4 !important;
            box-shadow: 0 0 10px rgba(89, 178, 244, 0.3) !important;
        }

        /* DropDownList option styling */
        select option,
        [id*="ddl"] option,
        .form-control option {
            background: #1a1a2e !important;
            color: #ffffff !important;
            padding: 0.5rem !important;
        }

        select option:hover,
        [id*="ddl"] option:hover,
        .form-control option:hover {
            background: #59b2f4 !important;
            color: #ffffff !important;
        }

        /* Ensure dropdown arrows are visible */
        select::-ms-expand,
        .form-control::-ms-expand {
            background-color: #59b2f4;
            border: none;
            color: #ffffff;
        }

        /* Additional styling for better visibility */
        .form-control option:checked {
            background: #59b2f4 !important;
            color: #ffffff !important;
        }

        /* Button Styles */
        .btn {
            padding: 0.8rem 1.5rem;
            border: none;
            border-radius: 0.5rem;
            font-size: 1rem;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            text-decoration: none;
        }

        .btn-primary {
            background: linear-gradient(135deg, #59b2f4, #4facfe);
            color: white;
        }

        .btn-primary:hover {
            background: linear-gradient(135deg, #4facfe, #59b2f4);
            transform: translateY(-2px);
        }

        .btn-success {
            background: linear-gradient(135deg, #28a745, #20c997);
            color: white;
        }

        .btn-success:hover {
            background: linear-gradient(135deg, #20c997, #28a745);
            transform: translateY(-2px);
        }

        .btn-danger {
            background: linear-gradient(135deg, #dc3545, #c82333);
            color: white;
        }

        .btn-danger:hover {
            background: linear-gradient(135deg, #c82333, #dc3545);
            transform: translateY(-2px);
        }

        .btn-group {
            display: flex;
            gap: 1rem;
            margin-top: 1.5rem;
        }

        /* Table Styles */
        .data-table {
            width: 100%;
            background: rgba(89, 178, 244, 0.1);
            border-radius: 0.5rem;
            overflow: hidden;
            margin-top: 1.5rem;
        }

        .data-table table {
            width: 100%;
            border-collapse: collapse;
        }

        .data-table th,
        .data-table td {
            padding: 1rem;
            text-align: left;
            border-bottom: 1px solid rgba(89, 178, 244, 0.2);
        }

        .data-table th {
            background: rgba(89, 178, 244, 0.2);
            color: #59b2f4;
            font-weight: 600;
        }

        .data-table td {
            color: #ffffff;
        }

        .data-table tr:hover {
            background: rgba(89, 178, 244, 0.1);
        }

        /* Enhanced GridView Styling */
        .table {
            width: 100%;
            border-collapse: collapse;
            background: rgba(89, 178, 244, 0.05);
            border-radius: 0.5rem;
            overflow: hidden;
        }

        .table th {
            background: rgba(89, 178, 244, 0.2) !important;
            color: #59b2f4 !important;
            font-weight: 600 !important;
            padding: 1rem !important;
            text-align: left !important;
            border-bottom: 2px solid rgba(89, 178, 244, 0.3) !important;
        }

        .table td {
            color: #ffffff !important;
            padding: 1rem !important;
            text-align: left !important;
            border-bottom: 1px solid rgba(89, 178, 244, 0.2) !important;
            background: rgba(26, 31, 53, 0.3) !important;
        }

        .table tr:hover td {
            background: rgba(89, 178, 244, 0.1) !important;
        }

        .table tr:nth-child(even) td {
            background: rgba(26, 31, 53, 0.5) !important;
        }

        .table tr:nth-child(even):hover td {
            background: rgba(89, 178, 244, 0.15) !important;
        }

        /* Specific ContactMessages GridView Styling */
        #gvContacts {
            width: 100% !important;
            border-collapse: collapse !important;
            background: transparent !important;
        }

        #gvContacts th {
            background: rgba(89, 178, 244, 0.3) !important;
            color: #ffffff !important;
            font-weight: bold !important;
            padding: 12px !important;
            text-align: left !important;
            border-bottom: 2px solid #59b2f4 !important;
            font-size: 1rem !important;
        }

        #gvContacts td {
            color: #ffffff !important;
            padding: 12px !important;
            text-align: left !important;
            border-bottom: 1px solid rgba(89, 178, 244, 0.3) !important;
            background: rgba(26, 31, 53, 0.4) !important;
            font-size: 0.95rem !important;
        }

        #gvContacts tr:hover td {
            background: rgba(89, 178, 244, 0.2) !important;
            color: #ffffff !important;
        }

        #gvContacts tr:nth-child(even) td {
            background: rgba(37, 43, 64, 0.6) !important;
        }

        #gvContacts tr:nth-child(even):hover td {
            background: rgba(89, 178, 244, 0.25) !important;
        }

        /* Row Selection Styling */
        table tr {
            transition: all 0.3s ease !important;
        }

        /* Green highlighting for selected rows */
        table tr.selected td {
            background-color: #28a745 !important;
            color: white !important;
            font-weight: bold !important;
        }

        /* More specific selector for GridView */
        .table tr.selected td {
            background-color: #28a745 !important;
            color: white !important;
            font-weight: bold !important;
        }

        /* Checkbox styling */
        .rowCheckbox {
            transform: scale(1.2);
            cursor: pointer;
        }

        /* Center align checkboxes */
        .table th:first-child,
        .table td:first-child {
            text-align: center !important;
            width: 60px !important;
        }

        /* Override any Bootstrap or external CSS */
        .data-table .table,
        .data-table .table th,
        .data-table .table td {
            background-color: transparent !important;
        }

        /* Checkbox styling for better visibility */
        .data-table input[type="checkbox"] {
            width: 18px !important;
            height: 18px !important;
            accent-color: #59b2f4 !important;
            cursor: pointer !important;
        }

        .data-table .row-select {
            transform: scale(1.2) !important;
            margin: 0 auto !important;
        }

        /* Center align checkboxes */
        .data-table th:first-child,
        .data-table td:first-child {
            text-align: center !important;
            width: 60px !important;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .sidebar {
                width: 100%;
                position: relative;
                height: auto;
            }

            .main-content {
                margin-left: 0;
                padding: 1rem;
            }

            .content-header {
                flex-direction: column;
                gap: 1rem;
                text-align: center;
            }

            .dashboard-cards {
                grid-template-columns: 1fr;
            }

            .form-grid {
                grid-template-columns: 1fr;
            }

            .btn-group {
                flex-direction: column;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-container">
        <!-- Sidebar -->
        <div class="sidebar">
            <div class="sidebar-header">
                <h2><i class="fas fa-user-shield"></i> Admin Panel</h2>
                <p>Portfolio Management System</p>
            </div>
            
            <nav class="sidebar-nav">
                <a href="#" class="nav-item active" onclick="showSection('dashboard')">
                    <i class="fas fa-tachometer-alt"></i> Dashboard
                </a>
                <a href="#" class="nav-item" onclick="showSection('projects')">
                    <i class="fas fa-project-diagram"></i> Projects
                </a>
                <a href="#" class="nav-item" onclick="showSection('skills')">
                    <i class="fas fa-cogs"></i> Skills
                </a>
                <a href="#" class="nav-item" onclick="showSection('education')">
                    <i class="fas fa-graduation-cap"></i> Education
                </a>
                <a href="#" class="nav-item" onclick="showSection('photography')">
                    <i class="fas fa-camera"></i> Photography
                </a>
                <a href="#" class="nav-item" onclick="showSection('contact')">
                    <i class="fas fa-envelope"></i> Contact Messages
                </a>
                <a href="#" class="nav-item" onclick="showSection('settings')">
                    <i class="fas fa-cog"></i> Settings
                </a>
            </nav>
            
            <div class="sidebar-footer">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="logout-btn" OnClick="btnLogout_Click" />
            </div>
        </div>

        <!-- Main Content -->
        <div class="main-content">
            <!-- Header -->
            <div class="content-header">
                <h1 id="pageTitle">Dashboard</h1>
                <div class="user-info">
                    <i class="fas fa-user"></i>
                    <span>Welcome, <asp:Label ID="lblUsername" runat="server"></asp:Label></span>
                    <span>|</span>
                    <i class="fas fa-clock"></i>
                    <span><asp:Label ID="lblLoginTime" runat="server"></asp:Label></span>
                </div>
            </div>

            <!-- Dashboard Section -->
            <div id="dashboard" class="content-section active">
                <div class="dashboard-cards">
                    <div class="dashboard-card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-project-diagram"></i>
                            </div>
                            <div>
                                <div class="card-title">Total Projects</div>
                                <div class="card-value"><asp:Label ID="lblProjectCount" runat="server" Text="0"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="dashboard-card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-cogs"></i>
                            </div>
                            <div>
                                <div class="card-title">Skills</div>
                                <div class="card-value"><asp:Label ID="lblSkillCount" runat="server" Text="0"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="dashboard-card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-camera"></i>
                            </div>
                            <div>
                                <div class="card-title">Photos</div>
                                <div class="card-value">25</div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="dashboard-card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-envelope"></i>
                            </div>
                            <div>
                                <div class="card-title">Messages</div>
                                <div class="card-value"><asp:Label ID="lblMessageCount" runat="server">0</asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="section-title">
                    <i class="fas fa-chart-line"></i>
                    Recent Activities
                </div>
                <p style="color: #858383; font-size: 1.1rem;">Welcome to your portfolio admin panel. Use the sidebar to navigate between different sections and manage your portfolio content.</p>
            </div>

            <!-- Projects Section -->
            <div id="projects" class="content-section">
                <div class="section-title">
                    <i class="fas fa-project-diagram"></i>
                    Project Management
                </div>
                
                <div class="form-grid">
                    <div class="form-group">
                        <label>Project Title</label>
                        <asp:TextBox ID="txtProjectTitle" runat="server" placeholder="Enter project title"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Project Type</label>
                        <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                            <asp:ListItem Value="Web Development">Web Development</asp:ListItem>
                            <asp:ListItem Value="Mobile App">Mobile App</asp:ListItem>
                            <asp:ListItem Value="Desktop App">Desktop App</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Technologies Used</label>
                        <asp:TextBox ID="txtTechnologies" runat="server" placeholder="e.g., HTML, CSS, JavaScript"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Project URL</label>
                        <asp:TextBox ID="txtProjectUrl" runat="server" placeholder="https://example.com"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Image Path (Optional)</label>
                        <asp:TextBox ID="txtImagePath" runat="server" placeholder="Resources/images/project-image.png (leave empty for default)"></asp:TextBox>
                    </div>
                    <div class="form-group" style="grid-column: span 2;">
                        <label>Project Description</label>
                        <asp:TextBox ID="txtProjectDescription" runat="server" TextMode="MultiLine" placeholder="Describe your project..."></asp:TextBox>
                    </div>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnAddProject" runat="server" Text="Add Project" CssClass="btn btn-success" OnClick="btnAddProject_Click" />
                    <asp:Button ID="btnUpdateProject" runat="server" Text="Update Selected" CssClass="btn btn-primary" OnClick="btnUpdateProject_Click" OnClientClick="return validateProjectSelection('update');" />
                    <asp:Button ID="btnDeleteProject" runat="server" Text="Delete Selected" CssClass="btn btn-danger" OnClick="btnDeleteProject_Click" OnClientClick="return validateProjectSelection('delete');" />
                </div>

                <div class="data-table">
                    <asp:GridView ID="gvProjects" runat="server" AutoGenerateColumns="false" CssClass="table" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="selectAllProjectsCheckbox" runat="server" onclick="toggleSelectAllProjects(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectProject" runat="server" CssClass="rowCheckbox" onclick="highlightProjectRow(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Technologies" HeaderText="Technologies" />
                            <asp:BoundField DataField="URL" HeaderText="URL" />
                            <asp:BoundField DataField="ImagePath" HeaderText="Image Path" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Skills Section -->
            <div id="skills" class="content-section">
                <div class="section-title">
                    <i class="fas fa-cogs"></i>
                    Skills Management
                </div>
                
                <div class="form-grid">
                    <div class="form-group">
                        <label>Skill Name</label>
                        <asp:TextBox ID="txtSkillName" runat="server" placeholder="e.g., JavaScript"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Skill Category</label>
                        <asp:DropDownList ID="ddlSkillCategory" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                            <asp:ListItem Value="Programming Languages">Programming Languages</asp:ListItem>
                            <asp:ListItem Value="Frameworks">Frameworks</asp:ListItem>
                            <asp:ListItem Value="Databases">Databases</asp:ListItem>
                            <asp:ListItem Value="Tools">Tools</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Proficiency Level (%)</label>
                        <asp:TextBox ID="txtSkillLevel" runat="server" placeholder="85" type="number" min="0" max="100"></asp:TextBox>
                    </div>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" CssClass="btn btn-success" OnClick="btnAddSkill_Click" />
                    <asp:Button ID="btnUpdateSkill" runat="server" Text="Update Skill" CssClass="btn btn-primary" OnClick="btnUpdateSkill_Click" />
                    <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete Skill" CssClass="btn btn-danger" OnClick="btnDeleteSkill_Click" />
                </div>

                <div class="data-table">
                    <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Name" HeaderText="Skill Name" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Level" HeaderText="Level %" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Education Section -->
            <div id="education" class="content-section">
                <div class="section-title">
                    <i class="fas fa-graduation-cap"></i>
                    Education Management
                </div>
                
                <div class="form-grid">
                    <div class="form-group">
                        <label>Institution Name</label>
                        <asp:TextBox ID="txtInstitution" runat="server" placeholder="University/School Name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Degree/Course</label>
                        <asp:TextBox ID="txtDegree" runat="server" placeholder="e.g., Bachelor of Science"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Field of Study</label>
                        <asp:TextBox ID="txtFieldOfStudy" runat="server" placeholder="e.g., Computer Science"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Start Year</label>
                        <asp:TextBox ID="txtStartYear" runat="server" placeholder="2020" type="number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>End Year</label>
                        <asp:TextBox ID="txtEndYear" runat="server" placeholder="2024" type="number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Grade/GPA</label>
                        <asp:TextBox ID="txtGrade" runat="server" placeholder="3.8 GPA"></asp:TextBox>
                    </div>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" CssClass="btn btn-success" OnClick="btnAddEducation_Click" />
                    <asp:Button ID="btnUpdateEducation" runat="server" Text="Update Selected" CssClass="btn btn-primary" OnClick="btnUpdateEducation_Click" OnClientClick="return validateEducationSelection('update');" />
                    <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete Selected" CssClass="btn btn-danger" OnClick="btnDeleteEducation_Click" OnClientClick="return validateEducationSelection('delete');" />
                </div>

                <div class="data-table">
                    <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="false" CssClass="table" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="selectAllEducationCheckbox" runat="server" onclick="toggleSelectAllEducation(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEducationSelect" runat="server" CssClass="rowCheckbox" onclick="highlightEducationRow(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Institution" HeaderText="Institution" />
                            <asp:BoundField DataField="Degree" HeaderText="Degree" />
                            <asp:BoundField DataField="Field" HeaderText="Field" />
                            <asp:BoundField DataField="StartYear" HeaderText="Start" />
                            <asp:BoundField DataField="EndYear" HeaderText="End" />
                            <asp:BoundField DataField="Grade" HeaderText="Grade" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Photography Section -->
            <div id="photography" class="content-section">
                <div class="section-title">
                    <i class="fas fa-camera"></i>
                    Photography Management
                </div>
                
                <div class="form-grid">
                    <div class="form-group">
                        <label>Photo Title</label>
                        <asp:TextBox ID="txtPhotoTitle" runat="server" placeholder="Enter photo title"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Photo Category</label>
                        <asp:DropDownList ID="ddlPhotoCategory" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                            <asp:ListItem Value="Portrait">Portrait</asp:ListItem>
                            <asp:ListItem Value="Landscape">Landscape</asp:ListItem>
                            <asp:ListItem Value="Street">Street</asp:ListItem>
                            <asp:ListItem Value="Event">Event</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Photo Description</label>
                        <asp:TextBox ID="txtPhotoDescription" runat="server" TextMode="MultiLine" placeholder="Describe the photo..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Image Path/URL</label>
                        <asp:TextBox ID="txtPhotoPath" runat="server" placeholder="Resources/images/photo.jpg"></asp:TextBox>
                    </div>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnAddPhoto" runat="server" Text="Add Photo" CssClass="btn btn-success" OnClick="btnAddPhoto_Click" />
                    <asp:Button ID="btnUpdatePhoto" runat="server" Text="Update Photo" CssClass="btn btn-primary" OnClick="btnUpdatePhoto_Click" />
                    <asp:Button ID="btnDeletePhoto" runat="server" Text="Delete Photo" CssClass="btn btn-danger" OnClick="btnDeletePhoto_Click" />
                </div>

                <div class="data-table">
                    <asp:GridView ID="gvPhotography" runat="server" AutoGenerateColumns="false" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Path" HeaderText="Image Path" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Contact Messages Section -->
            <div id="contact" class="content-section">
                <div class="section-title">
                    <i class="fas fa-envelope"></i>
                    Contact Messages
                </div>
                
                <div class="data-table">
                    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="false" CssClass="table" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="selectAllCheckbox" runat="server" onclick="toggleSelectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="rowCheckbox" onclick="highlightRow(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Subject" HeaderText="Subject" />
                            <asp:BoundField DataField="Message" HeaderText="Message" />
                            <asp:BoundField DataField="DateReceived" HeaderText="Date Received" DataFormatString="{0:MMM dd, yyyy hh:mm tt}" />
                        </Columns>
                    </asp:GridView>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnDeleteMessage" runat="server" Text="Delete Selected" CssClass="btn btn-danger" OnClick="btnDeleteMessage_Click" OnClientClick="return confirmDelete();" />
                    <asp:Button ID="btnRefreshMessages" runat="server" Text="Refresh" CssClass="btn btn-primary" OnClick="btnRefreshMessages_Click" />
                </div>
            </div>

            <!-- Settings Section -->
            <div id="settings" class="content-section">
                <div class="section-title">
                    <i class="fas fa-cog"></i>
                    Account Settings
                </div>
                
                <div class="form-grid">
                    <div class="form-group">
                        <label>Current Password</label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" placeholder="Enter current password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>New Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="Enter new password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm new password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="admin@example.com"></asp:TextBox>
                    </div>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btnUpdatePassword" runat="server" Text="Update Password" CssClass="btn btn-primary" OnClick="btnUpdatePassword_Click" />
                    <asp:Button ID="btnUpdateEmail" runat="server" Text="Update Email" CssClass="btn btn-success" OnClick="btnUpdateEmail_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function showSection(sectionName) {
            // Hide all sections
            var sections = document.querySelectorAll('.content-section');
            sections.forEach(function(section) {
                section.classList.remove('active');
            });
            
            // Remove active class from all nav items
            var navItems = document.querySelectorAll('.nav-item');
            navItems.forEach(function(item) {
                item.classList.remove('active');
            });
            
            // Show selected section
            document.getElementById(sectionName).classList.add('active');
            
            // Add active class to the correct nav item
            var targetNavItem = document.querySelector(`a[onclick="showSection('${sectionName}')"]`);
            if (targetNavItem) {
                targetNavItem.classList.add('active');
            } else if (event && event.target) {
                // Fallback for click events
                event.target.classList.add('active');
            }
            
            // Update page title
            var titles = {
                'dashboard': 'Dashboard',
                'projects': 'Project Management',
                'skills': 'Skills Management',
                'education': 'Education Management',
                'photography': 'Photography Management',
                'contact': 'Contact Messages',
                'settings': 'Account Settings'
            };
            
            document.getElementById('pageTitle').textContent = titles[sectionName];
        }

        // Toggle all checkboxes
        function toggleSelectAll(selectAllCheckbox) {
            var table = document.getElementById('<%= gvContacts.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkSelect"]');
            
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = selectAllCheckbox.checked;
                highlightRow(checkboxes[i]);
            }
        }

        // Highlight row when checkbox is clicked
        function highlightRow(checkbox) {
            var row = checkbox.closest('tr');
            if (checkbox.checked) {
                row.classList.add('selected');
            } else {
                row.classList.remove('selected');
            }

            // Update select all checkbox state
            updateSelectAllCheckbox();
        }

        // Update the "Select All" checkbox based on individual selections
        function updateSelectAllCheckbox() {
            var table = document.getElementById('<%= gvContacts.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkSelect"]');
            var selectAllCheckbox = table.querySelector('input[type="checkbox"][id*="selectAllCheckbox"]');
            
            if (!selectAllCheckbox) return;

            var checkedCount = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    checkedCount++;
                }
            }

            if (checkedCount === 0) {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = false;
            } else if (checkedCount === checkboxes.length) {
                selectAllCheckbox.checked = true;
                selectAllCheckbox.indeterminate = false;
            } else {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = true;
            }
        }

        // Initialize on page load
        document.addEventListener('DOMContentLoaded', function() {
            updateSelectAllCheckbox();
        });

        // Confirm delete function
        function confirmDelete() {
            return confirm('Are you sure you want to delete the selected messages?');
        }

        // Projects-specific functions
        function toggleSelectAllProjects(selectAllCheckbox) {
            var table = document.getElementById('<%= gvProjects.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkSelectProject"]');
            
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = selectAllCheckbox.checked;
                highlightProjectRow(checkboxes[i]);
            }
        }

        function highlightProjectRow(checkbox) {
            var row = checkbox.closest('tr');
            if (checkbox.checked) {
                row.classList.add('selected');
            } else {
                row.classList.remove('selected');
            }

            updateSelectAllProjectsCheckbox();
        }

        function updateSelectAllProjectsCheckbox() {
            var table = document.getElementById('<%= gvProjects.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkSelectProject"]');
            var selectAllCheckbox = table.querySelector('input[type="checkbox"][id*="selectAllProjectsCheckbox"]');
            
            if (!selectAllCheckbox) return;

            var checkedCount = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    checkedCount++;
                }
            }

            if (checkedCount === 0) {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = false;
            } else if (checkedCount === checkboxes.length) {
                selectAllCheckbox.checked = true;
                selectAllCheckbox.indeterminate = false;
            } else {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = true;
            }
        }

        function confirmDeleteProjects() {
            return confirm('Are you sure you want to delete the selected projects?');
        }

        function validateProjectSelection(action) {
            var table = document.getElementById('<%= gvProjects.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkSelectProject"]');
            var selectedCount = 0;
            
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    selectedCount++;
                }
            }
            
            console.log('Selected count: ' + selectedCount);
            
            if (selectedCount === 0) {
                alert('Please select at least one project by checking the checkbox.');
                return false;
            }
            
            if (action === 'update' && selectedCount > 1) {
                alert('Please select only one project for updating.');
                return false;
            }
            
            if (action === 'delete') {
                return confirm('Are you sure you want to delete the selected project(s)?');
            }
            
            return true;
        }

        // Education-specific functions
        function toggleSelectAllEducation(selectAllCheckbox) {
            var table = document.getElementById('<%= gvEducation.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkEducationSelect"]');
            
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = selectAllCheckbox.checked;
                highlightEducationRow(checkboxes[i]);
            }
        }

        function highlightEducationRow(checkbox) {
            var row = checkbox.closest('tr');
            if (checkbox.checked) {
                row.classList.add('selected');
            } else {
                row.classList.remove('selected');
            }

            updateSelectAllEducationCheckbox();
        }

        function updateSelectAllEducationCheckbox() {
            var table = document.getElementById('<%= gvEducation.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkEducationSelect"]');
            var selectAllCheckbox = table.querySelector('input[type="checkbox"][id*="selectAllEducationCheckbox"]');
            
            if (!selectAllCheckbox) return;

            var checkedCount = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    checkedCount++;
                }
            }

            if (checkedCount === 0) {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = false;
            } else if (checkedCount === checkboxes.length) {
                selectAllCheckbox.checked = true;
                selectAllCheckbox.indeterminate = false;
            } else {
                selectAllCheckbox.checked = false;
                selectAllCheckbox.indeterminate = true;
            }
        }

        function validateEducationSelection(action) {
            var table = document.getElementById('<%= gvEducation.ClientID %>');
            var checkboxes = table.querySelectorAll('input[type="checkbox"][id*="chkEducationSelect"]');
            var selectedCount = 0;
            
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    selectedCount++;
                }
            }
            
            console.log('Selected education count: ' + selectedCount);
            
            if (selectedCount === 0) {
                alert('Please select at least one education record by checking the checkbox.');
                return false;
            }
            
            if (action === 'update' && selectedCount > 1) {
                alert('Please select only one education record for updating.');
                return false;
            }
            
            if (action === 'delete') {
                return confirm('Are you sure you want to delete the selected education record(s)?');
            }
            
            return true;
        }
    </script>
</asp:Content>

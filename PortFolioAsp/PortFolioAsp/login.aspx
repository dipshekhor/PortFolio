<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PortFolioAsp.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link href="https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,600&display=swap" rel="stylesheet">

    <style>
        /* Login Page Styles */
        .login-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(135deg, #1a1e29 0%, #2a2f3a 100%);
            padding: 2rem;
            font-family: 'Inter', sans-serif;
        }

        .login-card {
            background: linear-gradient(145deg, #1a1f35, #252b40);
            border-radius: 2rem;
            padding: 4rem 3rem;
            width: 100%;
            max-width: 450px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
            border: 2px solid transparent;
            transition: all 0.4s ease;
        }

        .login-card:hover {
            border-color: #59b2f4;
            box-shadow: 0 25px 70px rgba(89, 178, 244, 0.3);
        }

        .login-header {
            text-align: center;
            margin-bottom: 3rem;
        }

        .login-header h1 {
            font-size: 2.8rem;
            color: #ffffff;
            margin-bottom: 0.5rem;
            font-weight: 600;
        }

        .login-header .highlight {
            color: #59b2f4;
        }

        .login-header p {
            color: #858383;
            font-size: 1.4rem;
            margin: 0;
        }

        .login-form {
            display: flex;
            flex-direction: column;
            gap: 2.5rem;
        }

        .form-group {
            position: relative;
        }

        .form-group label {
            position: absolute;
            top: 1.5rem;
            left: 5rem;
            color: #858383;
            font-size: 1.6rem;
            transition: all 0.3s ease;
            pointer-events: none;
            background: linear-gradient(145deg, #1a1f35, #252b40);
            padding: 0 0.5rem;
        }

        .form-group .input-container {
            position: relative;
            display: flex;
            align-items: center;
        }

        .form-group i.fa-user,
        .form-group i.fa-lock {
            position: absolute;
            left: 2rem;
            top: 50%;
            transform: translateY(-50%);
            color: #59b2f4;
            font-size: 1.8rem;
            z-index: 2;
        }

        .form-group .toggle-password {
            position: absolute;
            right: 1.5rem;
            top: 50%;
            transform: translateY(-50%);
            color: #858383;
            font-size: 1.6rem;
            cursor: pointer;
            z-index: 3;
            transition: color 0.3s ease;
        }

        .form-group .toggle-password:hover {
            color: #59b2f4;
        }

        .form-group input {
            width: 100%;
            padding: 1.5rem 5rem 1.5rem 5rem;
            background: transparent !important;
            border: 2px solid rgba(89, 178, 244, 0.3);
            border-radius: 1rem;
            color: #ffffff !important;
            font-size: 1.6rem;
            transition: all 0.3s ease;
            box-sizing: border-box;
        }

        .form-group input:focus {
            outline: none;
            border-color: #59b2f4;
            box-shadow: 0 0 15px rgba(89, 178, 244, 0.3);
            background: transparent !important;
            color: #ffffff !important;
        }

        .form-group input:valid {
            background: transparent !important;
            color: #ffffff !important;
        }

        .form-group input:focus ~ label,
        .form-group input:valid ~ label,
        .form-group input:not(:placeholder-shown) ~ label {
            top: -0.8rem;
            left: 4.5rem;
            color: #59b2f4;
            font-size: 1.3rem;
        }

        .login-btn {
            width: 100%;
            padding: 1.5rem 2rem;
            background: linear-gradient(135deg, #59b2f4, #4facfe);
            border: none;
            border-radius: 1rem;
            color: #ffffff;
            font-size: 1.6rem;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.1rem;
            margin-top: 1rem;
        }

        .login-btn:hover {
            background: linear-gradient(135deg, #4facfe, #59b2f4);
            transform: translateY(-3px);
            box-shadow: 0 10px 30px rgba(89, 178, 244, 0.4);
        }

        .login-btn:active {
            transform: translateY(-1px);
        }

        .forgot-password {
            text-align: center;
            margin-top: 2rem;
        }

        .forgot-password a {
            color: #59b2f4;
            text-decoration: none;
            font-size: 1.4rem;
            transition: color 0.3s ease;
        }

        .forgot-password a:hover {
            color: #4facfe;
            text-decoration: underline;
        }

        .back-to-portfolio {
            text-align: center;
            margin-top: 3rem;
            padding-top: 2rem;
            border-top: 1px solid rgba(89, 178, 244, 0.2);
        }

        .back-to-portfolio a {
            color: #858383;
            text-decoration: none;
            font-size: 1.4rem;
            transition: color 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
        }

        .back-to-portfolio a:hover {
            color: #59b2f4;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .login-container {
                padding: 1rem;
            }

            .login-card {
                padding: 3rem 2rem;
            }

            .login-header h1 {
                font-size: 2.4rem;
            }

            .form-group input {
                padding: 1.3rem 4.5rem 1.3rem 4.5rem;
                font-size: 1.4rem;
            }

            .form-group label {
                font-size: 1.4rem;
                left: 4.5rem;
            }

            .form-group i.fa-user,
            .form-group i.fa-lock {
                left: 1.5rem;
                font-size: 1.6rem;
            }

            .form-group .toggle-password {
                right: 1.2rem;
                font-size: 1.4rem;
            }
        }

        @media (max-width: 480px) {
            .login-card {
                padding: 2.5rem 1.5rem;
            }

            .login-header h1 {
                font-size: 2.2rem;
            }

            .form-group input {
                padding: 1.2rem 4rem 1.2rem 4rem;
                font-size: 1.3rem;
            }

            .form-group label {
                font-size: 1.3rem;
                left: 4rem;
            }

            .form-group i.fa-user,
            .form-group i.fa-lock {
                left: 1.3rem;
                font-size: 1.5rem;
            }

            .form-group .toggle-password {
                right: 1.1rem;
                font-size: 1.3rem;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-card">
            <div class="login-header">
                <h1>Welcome <span class="highlight">Back</span></h1>
                <p>Sign in to access your portfolio admin panel</p>
            </div>
            
            <div class="login-form">
                <div class="form-group">
                    <div class="input-container">
                        <i class='fas fa-user'></i>
                        <asp:TextBox ID="txtUsername" runat="server" placeholder=" " required="true"></asp:TextBox>
                        <label>Username</label>
                    </div>
                </div>
                
                <div class="form-group">
                    <div class="input-container">
                        <i class='fas fa-lock'></i>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder=" " required="true"></asp:TextBox>
                        <label>Password</label>
                        <i class='fas fa-eye toggle-password' id="togglePassword" onclick="togglePasswordVisibility()"></i>
                    </div>
                </div>
                
                <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="login-btn" OnClick="btnLogin_Click" />
                
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false" style="text-align: center; margin-top: 1rem;"></asp:Label>
            </div>
            
            <div class="forgot-password">
                <a href="#" onclick="alert('Please contact administrator for password reset.')">Forgot Password?</a>
            </div>
            
            <div class="back-to-portfolio">
                <a href="main.aspx">
                    <i class='fas fa-arrow-left'></i>
                    Back to Portfolio
                </a>
            </div>
        </div>
    </div>

    <script>
        function togglePasswordVisibility() {
            const passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            const toggleIcon = document.getElementById('togglePassword');
            
            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                toggleIcon.classList.remove('fa-eye');
                toggleIcon.classList.add('fa-eye-slash');
            } else {
                passwordField.type = 'password';
                toggleIcon.classList.remove('fa-eye-slash');
                toggleIcon.classList.add('fa-eye');
            }
        }
    </script>
</asp:Content>

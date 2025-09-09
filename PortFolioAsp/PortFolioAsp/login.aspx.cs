using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace PortFolioAsp
{
    public partial class login : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PortfolioConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear any previous messages
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Basic validation
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ShowMessage("Please enter both username and password.", "error");
                    return;
                }

                // Authenticate user against database
                if (ValidateUser(username, password))
                {
                    // Successful login
                    Session["IsLoggedIn"] = true;
                    Session["Username"] = username;
                    Session["LoginTime"] = DateTime.Now;

                    // Redirect to admin panel or main page
                    Response.Redirect("admin.aspx");
                }
                else
                {
                    // Failed login
                    ShowMessage("Invalid username or password. Please try again.", "error");
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred during login: " + ex.Message, "error");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM AdminUsers WHERE Username = @Username AND Password = @Password";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (you can add logging here)
                ShowMessage("Database connection error: " + ex.Message, "error");
                return false;
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            
            // Set color based on message type
            if (type == "error")
            {
                lblMessage.ForeColor = System.Drawing.Color.FromArgb(255, 99, 71); // Tomato red
            }
            else if (type == "success")
            {
                lblMessage.ForeColor = System.Drawing.Color.FromArgb(50, 205, 50); // Lime green
            }
        }

        private void ClearInputs()
        {
            txtPassword.Text = string.Empty;
            txtUsername.Focus();
        }
    }
}
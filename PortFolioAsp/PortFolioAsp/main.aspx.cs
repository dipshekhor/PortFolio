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
    public partial class main : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PortfolioConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblContactMessage.Visible = false;
                LoadProjects();
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form data
                string name = txtContactName.Text.Trim();
                string email = txtContactEmail.Text.Trim();
                string subject = txtContactSubject.Text.Trim();
                string message = txtContactMessage.Text.Trim();

                // Validate required fields
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
                {
                    ShowContactMessage("Please fill in all required fields.", "error");
                    return;
                }

                // Basic email validation
                if (!IsValidEmail(email))
                {
                    ShowContactMessage("Please enter a valid email address.", "error");
                    return;
                }

                // Save to database
                if (SaveContactMessage(name, email, subject, message))
                {
                    ShowContactMessage("Thank you! Your message has been sent successfully.", "success");
                    ClearContactForm();
                }
                else
                {
                    ShowContactMessage("Sorry, there was an error sending your message. Please try again.", "error");
                }
            }
            catch (Exception ex)
            {
                ShowContactMessage("An error occurred: " + ex.Message, "error");
            }
            
            // Ensure we stay on the contact section
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scrollToContact", 
                "setTimeout(function() { document.getElementById('contact').scrollIntoView({ behavior: 'smooth' }); }, 100);", true);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool SaveContactMessage(string name, string email, string subject, string message)
        {
            try
            {
                // Validate connection string first
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Database connection string is not configured.");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Test the connection first
                    conn.Open();
                    
                    // First ensure the table exists
                    CreateContactMessagesTableIfNotExists(conn);

                    // Insert the contact message
                    string query = @"INSERT INTO ContactMessages (Name, Email, Subject, Message, DateReceived) 
                                   VALUES (@Name, @Email, @Subject, @Message, @DateReceived)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Subject", subject ?? "");
                        cmd.Parameters.AddWithValue("@Message", message);
                        cmd.Parameters.AddWithValue("@DateReceived", DateTime.Now);

                        int result = cmd.ExecuteNonQuery();
                        
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("No rows were inserted into the database.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Database-specific error
                ShowContactMessage($"Database Error: {sqlEx.Message} (Error Number: {sqlEx.Number})", "error");
                return false;
            }
            catch (Exception ex)
            {
                // General error
                ShowContactMessage($"Error: {ex.Message}", "error");
                return false;
            }
        }

        private void CreateContactMessagesTableIfNotExists(SqlConnection conn)
        {
            try
            {
                string checkTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContactMessages' AND xtype='U')
                    CREATE TABLE ContactMessages (
                        ID int IDENTITY(1,1) PRIMARY KEY,
                        Name nvarchar(100) NOT NULL,
                        Email nvarchar(200) NOT NULL,
                        Subject nvarchar(200),
                        Message nvarchar(2000) NOT NULL,
                        DateReceived datetime DEFAULT GETDATE()
                    )";

                using (SqlCommand cmd = new SqlCommand(checkTableQuery, conn))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Failed to create ContactMessages table: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Table creation error: {ex.Message}");
            }
        }

        private void ShowContactMessage(string message, string type)
        {
            lblContactMessage.Text = message;
            lblContactMessage.Visible = true;

            if (type == "success")
            {
                lblContactMessage.CssClass = "contact-status success";
            }
            else
            {
                lblContactMessage.CssClass = "contact-status error";
            }
        }

        private void ClearContactForm()
        {
            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactSubject.Text = "";
            txtContactMessage.Text = "";
        }

        #region Projects Management
        private void LoadProjects()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // First check if table exists and has data
                    conn.Open();
                    
                    // Check if table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_NAME = 'Projects'";
                    using (SqlCommand checkCmd = new SqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = (int)checkCmd.ExecuteScalar();
                        if (tableExists == 0)
                        {
                            // Table doesn't exist, create it and insert sample data
                            CreateProjectsTableAndData();
                            return;
                        }
                    }
                    
                    // Check if table has data
                    string checkDataQuery = "SELECT COUNT(*) FROM Projects";
                    using (SqlCommand checkCmd = new SqlCommand(checkDataQuery, conn))
                    {
                        int recordCount = (int)checkCmd.ExecuteScalar();
                        if (recordCount == 0)
                        {
                            // Table exists but no data, insert sample data
                            InsertSampleProjects();
                        }
                    }
                    
                    // Now load projects
                    string query = @"SELECT Title, Type, Technologies, URL, Description, 
                                    ISNULL(ImagePath, 'Resources/images/default-project.png') as ImagePath 
                                    FROM Projects ORDER BY DateCreated DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        rptProjects.DataSource = dt;
                        rptProjects.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // If any error, create table and insert sample data
                CreateProjectsTableAndData();
            }
        }

        private void CreateProjectsTableAndData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Create table
                    string createTableQuery = @"CREATE TABLE Projects (
                                              ID int IDENTITY(1,1) PRIMARY KEY,
                                              Title nvarchar(200) NOT NULL,
                                              Type nvarchar(100),
                                              Technologies nvarchar(500),
                                              URL nvarchar(500),
                                              Description nvarchar(1000),
                                              ImagePath nvarchar(500),
                                              DateCreated datetime DEFAULT GETDATE()
                                          )";
                    using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Insert sample projects
                    InsertSampleProjects();
                }
            }
            catch { }
        }

        private void InsertSampleProjects()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string insertQuery = @"INSERT INTO Projects (Title, Type, Technologies, URL, Description, ImagePath, DateCreated) 
                                         VALUES (@Title, @Type, @Technologies, @URL, @Description, @ImagePath, @DateCreated)";

                    // Project 1: Web Portfolio
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Title", "Web Portfolio");
                        cmd.Parameters.AddWithValue("@Type", "Web Development");
                        cmd.Parameters.AddWithValue("@Technologies", "HTML, CSS, JavaScript");
                        cmd.Parameters.AddWithValue("@URL", "https://github.com/dipshekhor/portfolio");
                        cmd.Parameters.AddWithValue("@Description", "A responsive personal portfolio website built with HTML, CSS, and JavaScript featuring modern design and smooth animations.");
                        cmd.Parameters.AddWithValue("@ImagePath", "Resources/images/screencapture-127-0-0-1-5500-PortFolio-main-html-2025-08-01-21_47_24.png");
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.AddDays(-30));
                        cmd.ExecuteNonQuery();
                    }

                    // Project 2: Employee Management System
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Title", "Employee Management System App For Desktop");
                        cmd.Parameters.AddWithValue("@Type", "Desktop App");
                        cmd.Parameters.AddWithValue("@Technologies", "Java, JavaFX, MySQL");
                        cmd.Parameters.AddWithValue("@URL", "https://github.com/dipshekhor/EmployeeManagementSystem");
                        cmd.Parameters.AddWithValue("@Description", "An employee management application with user authentication, real-time updates, and intuitive drag-and-drop functionality.");
                        cmd.Parameters.AddWithValue("@ImagePath", "Resources/images/Screenshot 2025-08-09 124843.png");
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.AddDays(-20));
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Reload projects after insertion
                    LoadProjects();
                }
            }
            catch { }
        }

        public string GetTechTags(string technologies)
        {
            if (string.IsNullOrEmpty(technologies))
                return "";

            string[] techArray = technologies.Split(',');
            string result = "";
            
            foreach (string tech in techArray)
            {
                string cleanTech = tech.Trim();
                if (!string.IsNullOrEmpty(cleanTech))
                {
                    result += $"<span class='tech-tag'>{cleanTech}</span>";
                }
            }
            
            return result;
        }
        #endregion
    }
}
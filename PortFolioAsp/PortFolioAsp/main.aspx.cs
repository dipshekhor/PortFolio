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
    }
}
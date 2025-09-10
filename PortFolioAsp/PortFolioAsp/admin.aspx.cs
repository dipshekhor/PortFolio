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
    public partial class admin : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PortfolioConnectionString"].ConnectionString;
        
        // Control declarations (if not auto-generated)
        protected System.Web.UI.WebControls.HiddenField hiddenSelectedIDs;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUserInfo();
                LoadAllData();
            }
        }

        private void LoadUserInfo()
        {
            // Display user information in header
            if (Session["Username"] != null)
            {
                lblUsername.Text = Session["Username"].ToString();
            }

            if (Session["LoginTime"] != null)
            {
                DateTime loginTime = (DateTime)Session["LoginTime"];
                lblLoginTime.Text = loginTime.ToString("MMM dd, yyyy hh:mm tt");
            }

            // Load dynamic counts for dashboard
            LoadDashboardCounts();
        }

        private void LoadDashboardCounts()
        {
            // Get contact messages count
            int messageCount = GetContactMessagesCount();
            lblMessageCount.Text = messageCount.ToString();
        }

        private int GetContactMessagesCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM ContactMessages";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                // Table might not exist yet
                return 0;
            }
        }

        private void LoadAllData()
        {
            LoadProjects();
            LoadSkills();
            LoadEducation();
            LoadPhotography();
            LoadContactMessages();
        }

        #region Projects Management
        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Projects (Title, Type, Technologies, URL, Description, DateCreated) 
                                   VALUES (@Title, @Type, @Technologies, @URL, @Description, @DateCreated)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtProjectTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Type", ddlProjectType.SelectedValue);
                        cmd.Parameters.AddWithValue("@Technologies", txtTechnologies.Text.Trim());
                        cmd.Parameters.AddWithValue("@URL", txtProjectUrl.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtProjectDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                
                ClearProjectFields();
                LoadProjects();
                ShowMessage("Project added successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding project: " + ex.Message, "error");
            }
        }

        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
            // Implementation for updating projects
            ShowMessage("Update functionality will be implemented with project selection.", "info");
        }

        protected void btnDeleteProject_Click(object sender, EventArgs e)
        {
            // Implementation for deleting projects
            ShowMessage("Delete functionality will be implemented with project selection.", "info");
        }

        private void LoadProjects()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Title, Type, Technologies, URL FROM Projects ORDER BY DateCreated DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvProjects.DataSource = dt;
                        gvProjects.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Create table if it doesn't exist
                CreateProjectsTable();
            }
        }

        private void CreateProjectsTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"CREATE TABLE Projects (
                                    ID int IDENTITY(1,1) PRIMARY KEY,
                                    Title nvarchar(200) NOT NULL,
                                    Type nvarchar(100),
                                    Technologies nvarchar(500),
                                    URL nvarchar(500),
                                    Description nvarchar(1000),
                                    DateCreated datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void ClearProjectFields()
        {
            txtProjectTitle.Text = "";
            ddlProjectType.SelectedIndex = 0;
            txtTechnologies.Text = "";
            txtProjectUrl.Text = "";
            txtProjectDescription.Text = "";
        }
        #endregion

        #region Skills Management
        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Skills (Name, Category, Level, DateCreated) 
                                   VALUES (@Name, @Category, @Level, @DateCreated)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtSkillName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", ddlSkillCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@Level", Convert.ToInt32(txtSkillLevel.Text.Trim()));
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                
                ClearSkillFields();
                LoadSkills();
                ShowMessage("Skill added successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding skill: " + ex.Message, "error");
            }
        }

        protected void btnUpdateSkill_Click(object sender, EventArgs e)
        {
            ShowMessage("Update functionality will be implemented with skill selection.", "info");
        }

        protected void btnDeleteSkill_Click(object sender, EventArgs e)
        {
            ShowMessage("Delete functionality will be implemented with skill selection.", "info");
        }

        private void LoadSkills()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Name, Category, Level FROM Skills ORDER BY Category, Name";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvSkills.DataSource = dt;
                        gvSkills.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CreateSkillsTable();
            }
        }

        private void CreateSkillsTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"CREATE TABLE Skills (
                                    ID int IDENTITY(1,1) PRIMARY KEY,
                                    Name nvarchar(100) NOT NULL,
                                    Category nvarchar(100),
                                    Level int CHECK (Level >= 0 AND Level <= 100),
                                    DateCreated datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void ClearSkillFields()
        {
            txtSkillName.Text = "";
            ddlSkillCategory.SelectedIndex = 0;
            txtSkillLevel.Text = "";
        }
        #endregion

        #region Education Management
        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Education (Institution, Degree, Field, StartYear, EndYear, Grade, DateCreated) 
                                   VALUES (@Institution, @Degree, @Field, @StartYear, @EndYear, @Grade, @DateCreated)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text.Trim());
                        cmd.Parameters.AddWithValue("@Degree", txtDegree.Text.Trim());
                        cmd.Parameters.AddWithValue("@Field", txtFieldOfStudy.Text.Trim());
                        cmd.Parameters.AddWithValue("@StartYear", Convert.ToInt32(txtStartYear.Text.Trim()));
                        cmd.Parameters.AddWithValue("@EndYear", Convert.ToInt32(txtEndYear.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Grade", txtGrade.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                
                ClearEducationFields();
                LoadEducation();
                ShowMessage("Education record added successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding education: " + ex.Message, "error");
            }
        }

        protected void btnUpdateEducation_Click(object sender, EventArgs e)
        {
            ShowMessage("Update functionality will be implemented with education selection.", "info");
        }

        protected void btnDeleteEducation_Click(object sender, EventArgs e)
        {
            ShowMessage("Delete functionality will be implemented with education selection.", "info");
        }

        private void LoadEducation()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Institution, Degree, Field, StartYear, EndYear, Grade FROM Education ORDER BY StartYear DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvEducation.DataSource = dt;
                        gvEducation.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CreateEducationTable();
            }
        }

        private void CreateEducationTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"CREATE TABLE Education (
                                    ID int IDENTITY(1,1) PRIMARY KEY,
                                    Institution nvarchar(200) NOT NULL,
                                    Degree nvarchar(200),
                                    Field nvarchar(200),
                                    StartYear int,
                                    EndYear int,
                                    Grade nvarchar(50),
                                    DateCreated datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void ClearEducationFields()
        {
            txtInstitution.Text = "";
            txtDegree.Text = "";
            txtFieldOfStudy.Text = "";
            txtStartYear.Text = "";
            txtEndYear.Text = "";
            txtGrade.Text = "";
        }
        #endregion

        #region Photography Management
        protected void btnAddPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Photography (Title, Category, Description, ImagePath, DateCreated) 
                                   VALUES (@Title, @Category, @Description, @ImagePath, @DateCreated)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtPhotoTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", ddlPhotoCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@Description", txtPhotoDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@ImagePath", txtPhotoPath.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                
                ClearPhotoFields();
                LoadPhotography();
                ShowMessage("Photo added successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding photo: " + ex.Message, "error");
            }
        }

        protected void btnUpdatePhoto_Click(object sender, EventArgs e)
        {
            ShowMessage("Update functionality will be implemented with photo selection.", "info");
        }

        protected void btnDeletePhoto_Click(object sender, EventArgs e)
        {
            ShowMessage("Delete functionality will be implemented with photo selection.", "info");
        }

        private void LoadPhotography()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Title, Category, ImagePath as Path FROM Photography ORDER BY DateCreated DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvPhotography.DataSource = dt;
                        gvPhotography.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CreatePhotographyTable();
            }
        }

        private void CreatePhotographyTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"CREATE TABLE Photography (
                                    ID int IDENTITY(1,1) PRIMARY KEY,
                                    Title nvarchar(200) NOT NULL,
                                    Category nvarchar(100),
                                    Description nvarchar(1000),
                                    ImagePath nvarchar(500),
                                    DateCreated datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void ClearPhotoFields()
        {
            txtPhotoTitle.Text = "";
            ddlPhotoCategory.SelectedIndex = 0;
            txtPhotoDescription.Text = "";
            txtPhotoPath.Text = "";
        }
        #endregion

        #region Contact Messages
        protected void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selectedIds = new List<int>();
                
                // Check through GridView rows for checked checkboxes
                foreach (GridViewRow row in gvContacts.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            // Get the ID from DataKeys
                            int id = Convert.ToInt32(gvContacts.DataKeys[row.RowIndex].Value);
                            selectedIds.Add(id);
                        }
                    }
                }

                if (selectedIds.Count == 0)
                {
                    ShowMessage("Please select at least one message to delete by checking the checkboxes.", "error");
                    return;
                }

                // Delete selected messages from database
                int deletedCount = DeleteContactMessages(selectedIds);
                
                if (deletedCount > 0)
                {
                    // Refresh the GridView and dashboard counts
                    LoadContactMessages();
                    LoadDashboardCounts();
                    
                    string message = deletedCount == 1 ? 
                        "1 message deleted successfully!" : 
                        $"{deletedCount} messages deleted successfully!";
                    ShowMessage(message, "success");
                }
                else
                {
                    ShowMessage("No messages were deleted. Please try again.", "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error deleting messages: {ex.Message}", "error");
            }
        }

        protected void btnRefreshMessages_Click(object sender, EventArgs e)
        {
            ShowMessage("Starting to load contact messages...", "info");
            LoadContactMessages();
            LoadDashboardCounts(); // Update the count
            ShowMessage("Contact messages loading completed!", "info");
        }

        private void LoadContactMessages()
        {
            ShowMessage("LoadContactMessages method started", "info");
            
            try
            {
                ShowMessage("About to connect to database...", "info");
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    ShowMessage("Connection object created, opening connection...", "info");
                    
                    string query = "SELECT ID, Name, Email, Subject, Message, DateReceived FROM ContactMessages ORDER BY DateReceived DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        ShowMessage("Database connection opened successfully!", "info");
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // Debug: Show how many records were found
                        ShowMessage($"Found {dt.Rows.Count} contact messages in database.", "info");
                        
                        gvContacts.DataSource = dt;
                        gvContacts.DataBind();
                        
                        // Debug: Confirm grid was bound
                        ShowMessage($"GridView bound with {gvContacts.Rows.Count} rows.", "info");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading contact messages: {ex.Message}. Creating table...", "error");
                
                try 
                {
                    CreateContactMessagesTable();
                    ShowMessage("ContactMessages table created successfully", "info");
                }
                catch (Exception ex2)
                {
                    ShowMessage($"Error creating table: {ex2.Message}", "error");
                }
                
                // Try to load again after creating table
                try
                {
                    ShowMessage("Trying to load again after creating table...", "info");
                    
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT ID, Name, Email, Subject, Message, DateReceived FROM ContactMessages ORDER BY DateReceived DESC";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            gvContacts.DataSource = dt;
                            gvContacts.DataBind();
                            ShowMessage($"Table created and loaded {dt.Rows.Count} messages.", "success");
                        }
                    }
                }
                catch (Exception ex3)
                {
                    ShowMessage($"Still failed to load messages: {ex3.Message}. Loading dummy data...", "error");
                    
                    // Create a dummy row for testing
                    DataTable dummyDt = new DataTable();
                    dummyDt.Columns.Add("ID", typeof(int));
                    dummyDt.Columns.Add("Name", typeof(string));
                    dummyDt.Columns.Add("Email", typeof(string));
                    dummyDt.Columns.Add("Subject", typeof(string));
                    dummyDt.Columns.Add("Message", typeof(string));
                    dummyDt.Columns.Add("DateReceived", typeof(DateTime));
                    
                    // Add a test row
                    DataRow row = dummyDt.NewRow();
                    row["ID"] = 1;
                    row["Name"] = "Test User";
                    row["Email"] = "test@example.com";
                    row["Subject"] = "Test Subject";
                    row["Message"] = "Test message for debugging";
                    row["DateReceived"] = DateTime.Now;
                    dummyDt.Rows.Add(row);
                    
                    gvContacts.DataSource = dummyDt;
                    gvContacts.DataBind();
                    ShowMessage("Loaded dummy test data for debugging.", "info");
                }
            }
            
            ShowMessage("LoadContactMessages method completed", "info");
        }

        private int DeleteContactMessages(List<int> messageIds)
        {
            try
            {
                if (messageIds == null || messageIds.Count == 0)
                    return 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Use batch delete for better performance
                    string idList = string.Join(",", messageIds);
                    string query = $"DELETE FROM ContactMessages WHERE ID IN ({idList})";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting contact messages: {ex.Message}");
            }
        }

        private void CreateContactMessagesTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"CREATE TABLE ContactMessages (
                                    ID int IDENTITY(1,1) PRIMARY KEY,
                                    Name nvarchar(100) NOT NULL,
                                    Email nvarchar(200) NOT NULL,
                                    Subject nvarchar(200),
                                    Message nvarchar(2000) NOT NULL,
                                    DateReceived datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Settings Management
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string currentPassword = txtCurrentPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
                {
                    ShowMessage("Please fill in all password fields.", "error");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    ShowMessage("New password and confirmation do not match.", "error");
                    return;
                }

                // Verify current password and update
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // First verify current password
                    string verifyQuery = "SELECT COUNT(*) FROM AdminUsers WHERE Username = @Username AND Password = @CurrentPassword";
                    using (SqlCommand verifyCmd = new SqlCommand(verifyQuery, conn))
                    {
                        verifyCmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());
                        verifyCmd.Parameters.AddWithValue("@CurrentPassword", currentPassword);

                        conn.Open();
                        int count = (int)verifyCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            ShowMessage("Current password is incorrect.", "error");
                            return;
                        }
                    }

                    // Update password
                    string updateQuery = "UPDATE AdminUsers SET Password = @NewPassword WHERE Username = @Username";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateCmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());
                        updateCmd.ExecuteNonQuery();
                    }
                }

                txtCurrentPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                ShowMessage("Password updated successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating password: " + ex.Message, "error");
            }
        }

        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string newEmail = txtEmail.Text.Trim();

                if (string.IsNullOrEmpty(newEmail))
                {
                    ShowMessage("Please enter a valid email address.", "error");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE AdminUsers SET Email = @Email WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Email updated successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating email: " + ex.Message, "error");
            }
        }
        #endregion

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private void ShowMessage(string message, string type)
        {
            string script = $"alert('{message}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}
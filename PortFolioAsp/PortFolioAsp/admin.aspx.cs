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
            
            // Always load dashboard counts to ensure they're current
            LoadDashboardCounts();
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
        }

        private void LoadDashboardCounts()
        {
            // Get contact messages count
            int messageCount = GetContactMessagesCount();
            lblMessageCount.Text = messageCount.ToString();
            
            // Get projects count
            int projectCount = GetProjectsCount();
            lblProjectCount.Text = projectCount.ToString();
            
            // Get skills count
            int skillCount = GetSkillsCount();
            lblSkillCount.Text = skillCount.ToString();
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

        private int GetProjectsCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Projects";
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

        private int GetSkillsCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Skills";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        return result != null ? (int)result : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                System.Diagnostics.Debug.WriteLine($"Error getting skills count: {ex.Message}");
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
                // Check if we're updating an existing project
                if (ViewState["EditingProjectId"] != null)
                {
                    // Update existing project
                    int projectId = (int)ViewState["EditingProjectId"];
                    UpdateExistingProject(projectId);
                    ViewState["EditingProjectId"] = null; // Clear the editing state
                }
                else
                {
                    // Add new project
                    AddNewProject();
                }
                
                // Keep projects section active after add/update operation
                string addScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsAdd", addScript, true);
            }
            catch (Exception ex)
            {
                ShowMessage("Error saving project: " + ex.Message, "error");
                // Keep projects section active even on error
                string errorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsAddError", errorScript, true);
            }
        }

        private void AddNewProject()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Projects (Title, Type, Technologies, URL, Description, ImagePath, DateCreated) 
                               VALUES (@Title, @Type, @Technologies, @URL, @Description, @ImagePath, @DateCreated)";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", txtProjectTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Type", ddlProjectType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Technologies", txtTechnologies.Text.Trim());
                    cmd.Parameters.AddWithValue("@URL", txtProjectUrl.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtProjectDescription.Text.Trim());
                    
                    // Handle ImagePath - use default if empty
                    string imagePath = txtImagePath.Text.Trim();
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        imagePath = "Resources/images/default-project.png"; // Default image
                    }
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
            ClearProjectFields();
            LoadProjects();
            LoadDashboardCounts(); // Update dashboard counts
            ShowMessage("Project added successfully!", "success");
        }

        private void UpdateExistingProject(int projectId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Projects SET 
                               Title = @Title, 
                               Type = @Type, 
                               Technologies = @Technologies, 
                               URL = @URL, 
                               Description = @Description, 
                               ImagePath = @ImagePath 
                               WHERE ID = @ID";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", projectId);
                    cmd.Parameters.AddWithValue("@Title", txtProjectTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Type", ddlProjectType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Technologies", txtTechnologies.Text.Trim());
                    cmd.Parameters.AddWithValue("@URL", txtProjectUrl.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtProjectDescription.Text.Trim());
                    
                    // Handle ImagePath - use default if empty
                    string imagePath = txtImagePath.Text.Trim();
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        imagePath = "Resources/images/default-project.png"; // Default image
                    }
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
            ClearProjectFields();
            LoadProjects();
            LoadDashboardCounts(); // Update dashboard counts
            ShowMessage("Project updated successfully!", "success");
        }

        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selectedIds = GetSelectedProjectIds();

                if (selectedIds.Count == 0)
                {
                    ShowMessage("Please select a project to update by checking the checkbox.", "error");
                    // Keep projects section active
                    string errorScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('projects');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showProjectsError", errorScript, true);
                    return;
                }

                if (selectedIds.Count > 1)
                {
                    ShowMessage("Please select only one project to update.", "error");
                    // Keep projects section active
                    string multiScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('projects');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showProjectsMulti", multiScript, true);
                    return;
                }

                // Get the selected project data and populate the form
                int projectId = selectedIds[0];
                PopulateProjectForm(projectId);
                ShowMessage("Project data loaded for editing. Make your changes and click 'Update Project' to save.", "success");
                
                // Keep projects section active after successful operation
                string script = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsDelayed", script, true);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading project data: {ex.Message}", "error");
                // Keep projects section active even on error
                string errorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsError", errorScript, true);
            }
        }

        protected void btnDeleteProject_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selectedIds = GetSelectedProjectIds();

                if (selectedIds.Count == 0)
                {
                    ShowMessage("Please select at least one project to delete by checking the checkboxes.", "error");
                    // Keep projects section active
                    string noSelectionScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('projects');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showProjectsNoSelection", noSelectionScript, true);
                    return;
                }

                // Delete selected projects from database
                int deletedCount = DeleteProjects(selectedIds);
                
                if (deletedCount > 0)
                {
                    // Refresh the GridView and dashboard counts
                    LoadProjects();
                    LoadDashboardCounts();
                    
                    string message = deletedCount == 1 ? 
                        "1 project deleted successfully!" : 
                        $"{deletedCount} projects deleted successfully!";
                    ShowMessage(message, "success");
                }
                else
                {
                    ShowMessage("No projects were deleted. Please try again.", "error");
                }
                
                // Keep projects section active after deletion operation
                string deleteScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsDelete", deleteScript, true);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error deleting projects: {ex.Message}", "error");
                // Keep projects section active even on error
                string deleteErrorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('projects');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showProjectsDeleteError", deleteErrorScript, true);
            }
        }

        private List<int> GetSelectedProjectIds()
        {
            List<int> selectedIds = new List<int>();
            
            foreach (GridViewRow row in gvProjects.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelectProject = (CheckBox)row.FindControl("chkSelectProject");
                    if (chkSelectProject != null && chkSelectProject.Checked)
                    {
                        int id = Convert.ToInt32(gvProjects.DataKeys[row.RowIndex].Value);
                        selectedIds.Add(id);
                    }
                }
            }
            
            return selectedIds;
        }

        private void PopulateProjectForm(int projectId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Title, Type, Technologies, URL, Description, ImagePath FROM Projects WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", projectId);
                        conn.Open();
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtProjectTitle.Text = reader["Title"].ToString();
                                ddlProjectType.SelectedValue = reader["Type"].ToString();
                                txtTechnologies.Text = reader["Technologies"].ToString();
                                txtProjectUrl.Text = reader["URL"].ToString();
                                txtProjectDescription.Text = reader["Description"].ToString();
                                txtImagePath.Text = reader["ImagePath"].ToString();
                                
                                // Store the project ID for update
                                ViewState["EditingProjectId"] = projectId;
                                
                                // Change button text to indicate we're updating
                                btnAddProject.Text = "Update Project";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading project data: {ex.Message}", "error");
            }
        }

        private int DeleteProjects(List<int> projectIds)
        {
            int deletedCount = 0;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (int id in projectIds)
                    {
                        string query = "DELETE FROM Projects WHERE ID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0) deletedCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error deleting projects: {ex.Message}", "error");
            }
            
            return deletedCount;
        }

        private void LoadProjects()
        {
            try
            {
                // First ensure the table has the correct structure
                UpdateProjectsTableStructure();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Title, Type, Technologies, URL, ISNULL(ImagePath, '') as ImagePath FROM Projects ORDER BY DateCreated DESC";
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
                                    ImagePath nvarchar(500),
                                    DateCreated datetime DEFAULT GETDATE()
                                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                
                // Insert existing projects from the main.aspx
                InsertExistingProjects();
            }
            catch { }
        }

        private void UpdateProjectsTableStructure()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Check if ImagePath column exists
                    string checkColumnQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                                              WHERE TABLE_NAME = 'Projects' AND COLUMN_NAME = 'ImagePath'";
                    
                    using (SqlCommand checkCmd = new SqlCommand(checkColumnQuery, conn))
                    {
                        int columnExists = (int)checkCmd.ExecuteScalar();
                        
                        if (columnExists == 0)
                        {
                            // Add ImagePath column
                            string addColumnQuery = "ALTER TABLE Projects ADD ImagePath nvarchar(500)";
                            using (SqlCommand addCmd = new SqlCommand(addColumnQuery, conn))
                            {
                                addCmd.ExecuteNonQuery();
                            }
                            
                            // Update existing records with default image paths
                            UpdateExistingProjectImages();
                        }
                    }
                }
            }
            catch 
            {
                // If table doesn't exist or other issues, create it
                CreateProjectsTable();
            }
        }

        private void UpdateExistingProjectImages()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Update Web Portfolio project
                    string updateQuery1 = @"UPDATE Projects 
                                           SET ImagePath = 'Resources/images/screencapture-127-0-0-1-5500-PortFolio-main-html-2025-08-01-21_47_24.png' 
                                           WHERE Title LIKE '%Web Portfolio%' AND ImagePath IS NULL";
                    using (SqlCommand cmd = new SqlCommand(updateQuery1, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Update Employee Management System project
                    string updateQuery2 = @"UPDATE Projects 
                                           SET ImagePath = 'Resources/images/Screenshot 2025-08-09 124843.png' 
                                           WHERE Title LIKE '%Employee Management%' AND ImagePath IS NULL";
                    using (SqlCommand cmd = new SqlCommand(updateQuery2, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void InsertExistingProjects()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Check if projects already exist
                    string checkQuery = "SELECT COUNT(*) FROM Projects";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0) return; // Projects already exist
                    }

                    // Insert the existing projects from main.aspx
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
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Note: Existing projects added to database", "info");
            }
        }

        private void ClearProjectFields()
        {
            txtProjectTitle.Text = "";
            ddlProjectType.SelectedIndex = 0;
            txtTechnologies.Text = "";
            txtProjectUrl.Text = "";
            txtImagePath.Text = "";
            txtProjectDescription.Text = "";
            
            // Clear ViewState and reset button text
            ViewState["EditingProjectId"] = null;
            btnAddProject.Text = "Add Project";
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
                LoadDashboardCounts(); // Update dashboard counts
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
                // Check if we're updating an existing education record
                if (ViewState["EditingEducationId"] != null)
                {
                    // Update existing education record
                    int educationId = (int)ViewState["EditingEducationId"];
                    UpdateExistingEducation(educationId);
                    ViewState["EditingEducationId"] = null; // Clear the editing state
                    btnAddEducation.Text = "Add Education"; // Reset button text
                }
                else
                {
                    // Add new education record
                    AddNewEducation();
                }
                
                // Keep education section active after add/update operation
                string addScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationAdd", addScript, true);
            }
            catch (Exception ex)
            {
                ShowMessage("Error saving education: " + ex.Message, "error");
                // Keep education section active even on error
                string errorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationAddError", errorScript, true);
            }
        }

        private void AddNewEducation()
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

        private void UpdateExistingEducation(int educationId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Education SET Institution = @Institution, Degree = @Degree, Field = @Field, 
                               StartYear = @StartYear, EndYear = @EndYear, Grade = @Grade 
                               WHERE ID = @ID";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text.Trim());
                    cmd.Parameters.AddWithValue("@Degree", txtDegree.Text.Trim());
                    cmd.Parameters.AddWithValue("@Field", txtFieldOfStudy.Text.Trim());
                    cmd.Parameters.AddWithValue("@StartYear", Convert.ToInt32(txtStartYear.Text.Trim()));
                    cmd.Parameters.AddWithValue("@EndYear", Convert.ToInt32(txtEndYear.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Grade", txtGrade.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID", educationId);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
            ClearEducationFields();
            LoadEducation();
            ShowMessage("Education record updated successfully!", "success");
        }

        protected void btnUpdateEducation_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selectedIds = GetSelectedEducationIds();

                if (selectedIds.Count == 0)
                {
                    ShowMessage("Please select an education record to update by checking the checkbox.", "error");
                    // Keep education section active
                    string errorScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('education');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showEducationError", errorScript, true);
                    return;
                }

                if (selectedIds.Count > 1)
                {
                    ShowMessage("Please select only one education record to update.", "error");
                    // Keep education section active
                    string multiScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('education');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showEducationMulti", multiScript, true);
                    return;
                }

                // Get the selected education data and populate the form
                int educationId = selectedIds[0];
                PopulateEducationForm(educationId);
                ShowMessage("Education record loaded for editing. The 'Add Education' button should now show 'Update Education'. Make your changes and click it to save.", "success");
                
                // Keep education section active after successful operation
                string script = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationDelayed", script, true);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading education data: {ex.Message}", "error");
                // Keep education section active even on error
                string errorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationError", errorScript, true);
            }
        }

        protected void btnDeleteEducation_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selectedIds = GetSelectedEducationIds();

                if (selectedIds.Count == 0)
                {
                    ShowMessage("Please select at least one education record to delete.", "error");
                    // Keep education section active
                    string errorScript = @"
                        window.addEventListener('load', function() {
                            setTimeout(function() {
                                showSection('education');
                            }, 100);
                        });";
                    ClientScript.RegisterStartupScript(this.GetType(), "showEducationDeleteError", errorScript, true);
                    return;
                }

                int deletedCount = DeleteEducationRecords(selectedIds);
                LoadEducation();
                ClearEducationFields();
                ShowMessage($"{deletedCount} education record(s) deleted successfully!", "success");
                
                // Keep education section active after delete
                string script = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationDelete", script, true);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error deleting education records: {ex.Message}", "error");
                // Keep education section active even on error
                string errorScript = @"
                    window.addEventListener('load', function() {
                        setTimeout(function() {
                            showSection('education');
                        }, 100);
                    });";
                ClientScript.RegisterStartupScript(this.GetType(), "showEducationDeleteError", errorScript, true);
            }
        }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting education records: " + ex.Message, "error");
            }
        }

        private void PopulateEducationForm(int educationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Institution, Degree, Field, StartYear, EndYear, Grade FROM Education WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", educationId);
                        conn.Open();
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtInstitution.Text = reader["Institution"].ToString();
                                txtDegree.Text = reader["Degree"].ToString();
                                txtFieldOfStudy.Text = reader["Field"].ToString();
                                txtStartYear.Text = reader["StartYear"].ToString();
                                txtEndYear.Text = reader["EndYear"].ToString();
                                txtGrade.Text = reader["Grade"].ToString();
                                
                                // Store the education ID for update
                                ViewState["EditingEducationId"] = educationId;
                                
                                // Change button text to indicate we're updating
                                btnAddEducation.Text = "Update Education";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading education data: {ex.Message}", "error");
            }
        }
            }
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
                        
                        if (dt.Rows.Count > 0)
                        {
                            gvEducation.DataSource = dt;
                            gvEducation.DataBind();
                        }
                        else
                        {
                            gvEducation.DataSource = null;
                            gvEducation.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CreateEducationTable();
                LoadEducation(); // Retry after creating table
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
                                    Description nvarchar(MAX),
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
            btnAddEducation.Text = "Add Education"; // Reset button text
            ViewState["EditingEducationId"] = null; // Clear editing state
        }

        private List<int> GetSelectedEducationIds()
        {
            List<int> selectedIds = new List<int>();
            
            foreach (GridViewRow row in gvEducation.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkEducationSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    int educationId = Convert.ToInt32(gvEducation.DataKeys[row.RowIndex].Value);
                    selectedIds.Add(educationId);
                }
            }
            
            return selectedIds;
        }

        private int DeleteEducationRecords(List<int> educationIds)
        {
            int deletedCount = 0;
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (int educationId in educationIds)
                {
                    string query = "DELETE FROM Education WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", educationId);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            deletedCount++;
                        }
                    }
                }
            }
            
            return deletedCount;
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
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
    public partial class education : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PortfolioConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEducation();
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
                            rptEducation.DataSource = dt;
                            rptEducation.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Try to create the table if it doesn't exist
                try
                {
                    CreateEducationTableIfNotExists();
                    // Try loading again after ensuring table structure is correct
                    LoadEducationRetry();
                }
                catch (Exception createEx)
                {
                    Response.Write($"<script>console.log('Error: {createEx.Message}');</script>");
                }
            }
        }

        private void LoadEducationRetry()
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
                            rptEducation.DataSource = dt;
                            rptEducation.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>console.log('Error loading education: {ex.Message}');</script>");
            }
        }
        
        private void CreateEducationTableIfNotExists()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Check if table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_NAME = 'Education'";
                    
                    using (SqlCommand checkCmd = new SqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = (int)checkCmd.ExecuteScalar();
                        
                        if (tableExists == 0)
                        {
                            // Create table without Description column
                            string createTableQuery = @"CREATE TABLE Education (
                                        ID int IDENTITY(1,1) PRIMARY KEY,
                                        Institution nvarchar(200) NOT NULL,
                                        Degree nvarchar(200),
                                        Field nvarchar(200),
                                        StartYear int,
                                        EndYear int,
                                        Grade nvarchar(50),
                                        DateCreated datetime DEFAULT GETDATE()
                                    )";
                            
                            using (SqlCommand createCmd = new SqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Table exists, check if Description column exists and drop it
                            string checkColumnQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                                                       WHERE TABLE_NAME = 'Education' AND COLUMN_NAME = 'Description'";
                            
                            using (SqlCommand columnCmd = new SqlCommand(checkColumnQuery, conn))
                            {
                                int columnExists = (int)columnCmd.ExecuteScalar();
                                
                                if (columnExists > 0)
                                {
                                    // Drop Description column
                                    string dropColumnQuery = "ALTER TABLE Education DROP COLUMN Description";
                                    using (SqlCommand dropCmd = new SqlCommand(dropColumnQuery, conn))
                                    {
                                        dropCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CreateEducationTableIfNotExists: {ex.Message}");
            }
        }
    }
}
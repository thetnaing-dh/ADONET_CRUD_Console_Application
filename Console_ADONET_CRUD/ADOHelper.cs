using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_ADONET_CRUD
{
    internal class ADOHelper
    {
        string _connectionString = "Server=.;Database=TrainingBatch5;User Id=sa;Password=23032106;TrustServerCertificate=True;";

        public void ReadData()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();            

            string sqlQuery = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] WHERE [DeleteFlag] = 0";

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            Console.WriteLine("List of Blogs : ");

            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Console.WriteLine($"BlogNo.: {sqlDataReader["BlogId"]}, Title: {sqlDataReader["BlogTitle"]}, Author: {sqlDataReader["BlogAuthor"]}, Content: {sqlDataReader["BlogContent"]}");
            }

            connection.Close();
            Console.WriteLine("");
        }

        public void InsertData()
        {
            Console.WriteLine("Enter Blog Title : ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author : ");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Content : ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string sqlQuery = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent]
                                   ,[DeleteFlag])
                                VALUES
                                   (@Title
                                   ,@Author
                                   ,@Content
                                   ,0)";

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Author", author);
            command.Parameters.AddWithValue("@Content", content);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Blog Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Error in Inserting Blog");
            }

            connection.Close();
            Console.WriteLine("");
        }

        public void UpdateData()
        {
            Console.WriteLine("Enter Blog Id to Update : ");
            int blogId;
            try
            {
                blogId = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid Blog Id");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string searchQuery = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] WHERE [BlogId] = @BlogId AND [DeleteFlag] = 0";

            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);

            searchCommand.Parameters.AddWithValue("@BlogId", blogId);

            SqlDataReader searchReader = searchCommand.ExecuteReader();

            if (!searchReader.HasRows)
            {
                Console.WriteLine("Blog Id not found or it is deleted.");
                connection.Close();
                return;
            }

            searchReader.Read();

            Console.WriteLine("Enter New Blog Title : ");
            string title = Console.ReadLine();         

            Console.WriteLine("Enter New Blog Author : ");
            string author = Console.ReadLine();

            Console.WriteLine("Enter New Blog Content : ");
            string content = Console.ReadLine();

            if (title.Length == 0) 
            {
                title = (string)searchReader["BlogTitle"];              
            }

            if (author.Length == 0)
            {
                author = (string)searchReader["BlogAuthor"];
            }

            if (content.Length == 0)
            {
                content = (string)searchReader["BlogContent"];
            }

            searchReader.Close();

            string sqlQuery = @"UPDATE [dbo].[Tbl_Blog]
                               SET [BlogTitle] = @Title
                                  ,[BlogAuthor] = @Author
                                  ,[BlogContent] = @Content      
                             WHERE [BlogId] = @BlogId";

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            
            command.Parameters.AddWithValue("@BlogId", blogId);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Author", author);
            command.Parameters.AddWithValue("@Content", content);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Blog Updated Successfully");
            }
            else
            {
                Console.WriteLine("Error in Updating Blog");
            }                   

            connection.Close();
            Console.WriteLine("");
        }

        public void DeleteData()
        {
            Console.WriteLine("Enter Blog Id to Delete : ");
            int blogId;
            try
            {
                blogId = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid Blog Id");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string searchQuery = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] WHERE [BlogId] = @BlogId AND [DeleteFlag] = 0";

            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);

            searchCommand.Parameters.AddWithValue("@BlogId", blogId);

            SqlDataReader searchReader = searchCommand.ExecuteReader();

            if (!searchReader.HasRows)
            {
                Console.WriteLine("Blog Id not found or it is deleted.");
                connection.Close();
                return;
            }

            searchReader.Close();

            string sqlQuery = @"UPDATE [dbo].[Tbl_Blog]
                               SET [DeleteFlag] = 1                                      
                               WHERE [BlogId] = @BlogId";

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@BlogId", blogId);           

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Blog Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Error in Deleting Blog");
            }

            connection.Close();
            Console.WriteLine("");
        }
    }
}

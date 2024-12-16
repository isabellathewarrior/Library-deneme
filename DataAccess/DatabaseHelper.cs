using Library_deneme.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Library_deneme.DataAccess
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // You can store the connection string in App.config
            connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        // Method to authenticate user
        public User AuthenticateUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, Username, IsAdmin FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Username = reader["Username"].ToString(),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
                    };
                }
                return null;
            }
        }

        // Method to get all books
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    });
                }
            }
            return books;
        }

        // Method to add a new book
        public void AddBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, IsAvailable) VALUES (@Title, @Author, @IsAvailable)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to update a book
        public void UpdateBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Title = @Title, Author = @Author, IsAvailable = @IsAvailable WHERE BookId = @BookId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to delete a book
        public void DeleteBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to borrow a book
        public void BorrowBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET IsAvailable = 0 WHERE BookId = @BookId AND IsAvailable = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Book is not available.");
                }
            }
        }
    }
}

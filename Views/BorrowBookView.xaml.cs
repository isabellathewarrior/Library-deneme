using Library_deneme.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_deneme.Views
{
    /// <summary>
    /// Interaction logic for BorrowBookView.xaml
    /// </summary>
    public partial class BorrowBookView : UserControl
    {
        private DatabaseHelper dbHelper;
        private Models.User currentUser;

        public BorrowBookView(DatabaseHelper dbHelper, Models.User user)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.currentUser = user;
            LoadBooks();
        }

        private void LoadBooks()
        {
            var books = dbHelper.GetAllBooks();
            BooksDataGrid.ItemsSource = books;
        }

        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Models.Book selectedBook)
            {
                if (selectedBook.IsAvailable)
                {
                    try
                    {
                        dbHelper.BorrowBook(selectedBook.BookId);
                        MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadBooks();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Book is not available.", "Unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to borrow.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

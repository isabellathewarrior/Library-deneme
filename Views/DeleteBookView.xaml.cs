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
    /// Interaction logic for DeleteBookView.xaml
    /// </summary>
    public partial class DeleteBookView : UserControl
    {
        private DatabaseHelper dbHelper;

        public DeleteBookView(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            LoadBooks();
        }

        private void LoadBooks()
        {
            var books = dbHelper.GetAllBooks();
            BooksDataGrid.ItemsSource = books;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Models.Book selectedBook)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedBook.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    dbHelper.DeleteBook(selectedBook.BookId);
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadBooks();
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

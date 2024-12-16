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
    /// Interaction logic for AddEditBookView.xaml
    /// </summary>
    public partial class AddEditBookView : UserControl
    {
        private DatabaseHelper dbHelper;
        private bool isEditMode;
        private List<Models.Book> books;

        public AddEditBookView(DatabaseHelper dbHelper, bool isEditMode = false)
        {
            InitializeComponent(); // burda bi sakatlık var
            this.dbHelper = dbHelper;
            this.isEditMode = isEditMode;

            if (isEditMode)
            {
                BooksComboBox.Visibility = Visibility.Visible;
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            books = dbHelper.GetAllBooks();
            BooksComboBox.ItemsSource = books;
            BooksComboBox.DisplayMemberPath = "Title";
            BooksComboBox.SelectedValuePath = "BookId";
        }

        private void BooksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksComboBox.SelectedItem is Models.Book selectedBook)
            {
                TitleTextBox.Text = selectedBook.Title;
                AuthorTextBox.Text = selectedBook.Author;
                IsAvailableCheckBox.IsChecked = selectedBook.IsAvailable;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            string author = AuthorTextBox.Text.Trim();
            bool isAvailable = IsAvailableCheckBox.IsChecked ?? true;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Please enter both title and author.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEditMode)
            {
                if (BooksComboBox.SelectedItem is Models.Book selectedBook)
                {
                    selectedBook.Title = title;
                    selectedBook.Author = author;
                    selectedBook.IsAvailable = isAvailable;
                    dbHelper.UpdateBook(selectedBook);
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                Models.Book newBook = new Models.Book
                {
                    Title = title,
                    Author = author,
                    IsAvailable = isAvailable
                };
                dbHelper.AddBook(newBook);
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

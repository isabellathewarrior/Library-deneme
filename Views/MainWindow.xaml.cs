using Library_deneme.DataAccess;
using Library_deneme.Models;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private DatabaseHelper dbHelper;

        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            dbHelper = new DatabaseHelper();
            WelcomeLabel.Content = $"Welcome, {currentUser.Username}";

            if (currentUser.IsAdmin)
            {
                AddBookButton.Visibility = Visibility.Visible;
                EditBookButton.Visibility = Visibility.Visible;
                DeleteBookButton.Visibility = Visibility.Visible;
            }

            // Load Books View by default
            ViewBooksButton_Click(null, null);
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void ViewBooksButton_Click(object sender, RoutedEventArgs e)
        {
            BooksView booksView = new BooksView(dbHelper, currentUser);
            ContentArea.Content = booksView;
        }

        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            BorrowBookView borrowView = new BorrowBookView(dbHelper, currentUser);
            ContentArea.Content = borrowView;
        }

        // Admin Functions
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookView addView = new AddEditBookView(dbHelper);
            ContentArea.Content = addView;
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookView editView = new AddEditBookView(dbHelper, isEditMode: true);
            ContentArea.Content = editView;
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteBookView deleteView = new DeleteBookView(dbHelper);
            ContentArea.Content = deleteView;
        }
    }
}

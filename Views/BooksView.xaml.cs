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
    /// Interaction logic for BooksView.xaml
    /// </summary>
    public partial class BooksView : UserControl
    {
        private DatabaseHelper dbHelper;
        private Models.User currentUser;

        public BooksView(DatabaseHelper dbHelper, Models.User user)
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
    }
}

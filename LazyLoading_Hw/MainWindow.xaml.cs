using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LazyLoading_Hw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDataLoaded = false;
        private LibraryContext database;

        public ObservableCollection<Book> book { get; } = new ObservableCollection<Book>();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void EnsureDataLoaded()
        {
            if (!isDataLoaded)
            {
                database = new LibraryContext();
                isDataLoaded = true;
            }
        }

        private void cmb1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBoxItem? selectedItem = cmb1.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            cmb2.Items.Clear();
            book.Clear();
            EnsureDataLoaded();

            if (selectedItem.Content.ToString() == "Authors")
            {
                var authors = database.Authors.ToList();
                authors.ForEach(a => cmb2.Items.Add($"{a.FirstName} {a.LastName}"));
            }
            else if (selectedItem.Content.ToString() == "Themes")
            {
                var themes = database.Themes.ToList();
                themes.ForEach(t => cmb2.Items.Add(t.Name));
            }
            else if (selectedItem.Content.ToString() == "Categories")
            {
                var categories = database.Categories.ToList();
                categories.ForEach(c => cmb2.Items.Add(c.Name));
            }
        }

        private void cmb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem? selectedItem = cmb1.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            book.Clear();
            EnsureDataLoaded();

            if (selectedItem.Content.ToString() == "Authors")
            {
                var selectedAuthor = cmb2.SelectedItem as string;
                if (selectedAuthor == null)
                    return;

                var author = database.Authors.FirstOrDefault(a => (a.FirstName + " " + a.LastName) == selectedAuthor);
                if (author != null)
                {
                    var authorBooks = author.Books.ToList();
                    authorBooks.ForEach(b => book.Add(b));
                }
            }
            else if (selectedItem.Content.ToString() == "Themes")
            {
                // ... diğer kodlar ...
            }
            else if (selectedItem.Content.ToString() == "Categories")
            {
                // ... diğer kodlar ...
            }
        }
    }
}

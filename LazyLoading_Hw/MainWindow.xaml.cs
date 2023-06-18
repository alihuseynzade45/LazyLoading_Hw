using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LazyLoading_Hw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> book { get; } = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void cmb1_SelectionChanged(object sender, RoutedEventArgs e)
        {

            ComboBoxItem? selectedItem = cmb1.SelectedItem as ComboBoxItem;

            book.Clear();
            using (LibraryContext database = new())
            {

                if (selectedItem!.Content.ToString() == "Authors")
                {
                    cmb2.Items.Clear();

                    var authors = database.Authors.ToList();

                    authors.ForEach(a => cmb2.Items.Add($@"{a.FirstName} {a.LastName}"));
                }
                else if (selectedItem!.Content.ToString() == "Themes")
                {
                    cmb2.Items.Clear();

                    var themes = database.Themes.ToList();

                    themes.ForEach(t => cmb2.Items.Add($@"{t.Name}"));
                }
                else if (selectedItem!.Content.ToString() == "Categories")
                {
                    cmb2.Items.Clear();

                    var categories = database.Categories.ToList();

                    categories.ForEach(c => cmb2.Items.Add($@"{c.Name}"));
                }

            }
        }

        private void cmb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem? selectedItem = cmb1.SelectedItem as ComboBoxItem;

            book.Clear();

            if (selectedItem!.Content.ToString() == "Authors")
            {

                var selectedAuthor = cmb2.SelectedItem as string;

                if (selectedAuthor != null)
                {

                    using (LibraryContext database = new())
                    {
                        var authors = database.Authors.ToList();

                        var author = authors.FirstOrDefault(a => $@"{a.FirstName} {a.LastName}" == selectedAuthor);

                        var authorBooks = author!.Books.ToList();

                        authorBooks.ForEach(b => book.Add(b));
                    }
                }
            }
            else if (selectedItem.Content.ToString() == "Themes")
            {
                var selectedTheme = cmb2.SelectedItem as string;

                if (selectedTheme != null)
                {

                    using (LibraryContext database = new())
                    {
                        var themes = database.Themes.ToList();

                        var theme = themes.FirstOrDefault(t => t.Name == selectedTheme);

                        var themeBooks = theme!.Books.ToList();

                        themeBooks.ForEach(b => book.Add(b));
                    }
                }
            }
            else if (selectedItem.Content.ToString() == "Categories")
            {
                var selectedCategory = cmb2.SelectedItem as string;

                if (selectedCategory != null)
                {

                    using (LibraryContext database = new())
                    {
                        var categories = database.Categories.ToList();

                        var category = categories.FirstOrDefault(t => t.Name == selectedCategory);

                        var categoryBooks = category!.Books.ToList();

                        categoryBooks.ForEach(b => book.Add(b));
                    }

                }

            }

        }
    }
}
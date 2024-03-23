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

namespace ResturaantBillCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - ${Price}";
        }
    }

    public partial class MainWindow : Window
        {
        private ObservableCollection<MenuItem> selectedItemsList = new ObservableCollection<MenuItem>();
        
            private decimal totalPrice = 0; // Total price variable

            public MainWindow()
            {
                InitializeComponent();
            // Bind the ObservableCollection to the ListBox
            SelectedItemsListBox.ItemsSource = selectedItemsList;
        }
        private void AddSelectedItems(ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                // Get the selected item from the ComboBox
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

                // Extract the content of the selected item
                string[] itemDetails = selectedItem.Content.ToString().Split(new string[] { " - $" }, StringSplitOptions.None);

                if (itemDetails.Length == 2)
                {
                    string itemName = itemDetails[0];
                    decimal itemPrice = decimal.Parse(itemDetails[1]);

                    // Create a new MenuItem object
                    MenuItem selectedItemObject = new MenuItem(itemName, itemPrice);

                    // Add the selected item to the list
                    selectedItemsList.Add(selectedItemObject);

                    // Refresh the ListBox
                    RefreshListBox();
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Add selected items from all ComboBoxes
            AddSelectedItems(DessertsComboBox);
            AddSelectedItems(BeveragesComboBox);
            AddSelectedItems(AppetizersComboBox);
            AddSelectedItems(MainCoursesComboBox);

            // Refresh the ListBox
            RefreshListBox();

            // Call CalculateTotalPrice method to update totalPrice
            CalculateTotalPrice();

            // Update the content of a label to display the total price
            TotalLabel.Content = $"Total: ${totalPrice:0.00}";


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the ListBox
            if (SelectedItemsListBox.SelectedItem != null)
            {
                // Cast the selected item to MenuItem
                MenuItem selectedItem = (MenuItem)SelectedItemsListBox.SelectedItem;

                // Remove the selected item from the selectedItemsList
                selectedItemsList.Remove(selectedItem);

                // Refresh ListBox to reflect the changes
                RefreshListBox();

                // Recalculate total price
                CalculateTotalPrice();

                // Update total price label
                TotalLabel.Content = $"Total: ${totalPrice:0.00}";
            }

        }

        private void RefreshListBox()
        {
            // Set the ItemsSource of the ListBox to the ObservableCollection
            SelectedItemsListBox.ItemsSource = selectedItemsList;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

            TotalLabel.Content = $"Total: ${totalPrice:0.00}"; 
            selectedItemsList.Clear();
            CalculateTotalPrice();
            TotalLabel.Content = $"Total: ${totalPrice:0.00}";
        }

   

        private void CalculateTotalPrice()
        {
            totalPrice = 0;

            foreach (MenuItem item in selectedItemsList)
            {
                totalPrice += item.Price;
            }
        }
        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.centennialcollege.ca/");
        }
    }
    }
    


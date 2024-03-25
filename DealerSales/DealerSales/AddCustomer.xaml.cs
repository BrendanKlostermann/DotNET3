using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace DealerSales
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        List<int> zipcodes = null;
        List<ZipCode> zips = null;
        public AddCustomer()
        {
            InitializeComponent();
        }

        private List<int> getZipCodes()
        {
            zipcodes = new List<int>();
            zips = new List<ZipCode>();
            ZipCodeManager zipManager = new ZipCodeManager();
            zips = zipManager.GetAllZipCodes();

            foreach (ZipCode item in zips)
            {
                zipcodes.Add(item.Zipcode);
            }
            

            return zipcodes;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            cmbZipCode.ItemsSource = getZipCodes();
        }

        private void txtFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            string firstName;
            string lastName;
            string phoneNumber;
            string email;
            int zipCode;
            if(!Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Names can only contain letters");
                return;
            }

            if(!Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Last name can only contain letters");
                return;
            }

            if (!Regex.IsMatch(txtPhone.Text, @"^\d+$"))
            {
                MessageBox.Show("Phone numbers can only contain digits");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Email is not the correct format");
                return;
            }

            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            phoneNumber = txtPhone.Text;
            email = txtEmail.Text;
            zipCode = int.Parse(cmbZipCode.Text);
            try
            {
                CustomerManager customerManager = new CustomerManager();
                var addCust = customerManager.AddCustomer(firstName, lastName, phoneNumber, email, zipCode);
                if (addCust)
                {
                    this.DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }



        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

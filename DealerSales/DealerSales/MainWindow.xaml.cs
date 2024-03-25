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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using DataObjects;
using System.Data;

namespace DealerSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user = null;
        List<string> _roles = null;

        private Vehicle _vehicle = null;
        List<Vehicle> _vehicles = null;
        private VehicleFactoryOptions _factoryOptions = null;

        private Vehicle _manVehicle = null;
        List<Vehicle> _manVehicles = null;

        private Customer _customer = null;
        List<Customer> _customers = null;

        private Customer _manCustomer = null;
        List<Customer> _manCustomers = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HideAllTabs()
        {
            foreach (var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Hidden;
            }
        }

        //Open tabset panels based on role
        private void ShowRoleTab(List<String> roles)
        {
            _roles = roles;

            foreach (string role in _roles)
            {
                switch (role)
                {
                    case "Director":
                        tabCustomers.Visibility = Visibility.Visible;
                        tabManageCustomers.Visibility = Visibility.Visible;
                        tabVehicles.Visibility = Visibility.Visible;
                        tabManageVehicles.Visibility = Visibility.Visible;
                        tabManageEmployees.Visibility = Visibility.Visible;
                        break;
                    case "Manager":
                        tabCustomers.Visibility = Visibility.Visible;
                        tabManageCustomers.Visibility = Visibility.Visible;
                        tabVehicles.Visibility = Visibility.Visible;
                        tabManageVehicles.Visibility = Visibility.Visible;
                        break;
                    case "Salesman":
                        tabCustomers.Visibility = Visibility.Visible;
                        tabVehicles.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        //for user interface on login
        private void UpdateUILogin(User user)
        {
            User _user = user;
            lblGreeting.Content = "Sign in as " + _user.FirstName + " " + _user.LastName;
            btnLogin.Content = "Sign out";
            statMessage.Content = _user.FirstName + " " + _user.LastName + " " + DateTime.Now.ToString();

            // Hide login options
            txtEmail.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            // update tabset for user
            ShowRoleTab(_user.Roles);

            if (_user.Roles.Contains("Director"))
            {
                lblRoles.Content = "Signed in as a Director";
            }
            else if (_user.Roles.Contains("Manager"))
            {
                lblRoles.Content = "Signed in as a Manager";
            }
            else
            {
                lblRoles.Content = "Signed in as a Salesman";
            }
        } // end of UpdateUILogin

        //for user interface on logout
        private void UpdateUILogout()
        {
            btnLogin.Content = "Sign in";
            //Reset visability for login credentials
            txtEmail.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            txtEmail.Text = "";
            txtPassword.Password = "";

            //Reset text
            lblGreeting.Content = "You are not signed in.";
            statMessage.Content = "Welcome. Please log in to continue.";

            //Close the tabSet
            HideAllTabs();
            btnLogin.IsDefault = true;
            txtEmail.Focus();
        } // End of updateUILogout


        private void populateVehiclesForSale()
        {
            _vehicles = new List<Vehicle>();
            try
            {
                _vehicles = new List<Vehicle>();

                VehicleManager vehicleManager = new VehicleManager();
                _vehicles = vehicleManager.GetAllAvailableVehicles();

                datVehicle.ItemsSource = _vehicles;

                datVehicle.Columns.RemoveAt(6);
                datVehicle.Columns.RemoveAt(5);
                datVehicle.Columns.RemoveAt(8);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void populateCustomerList()
        {
            _customers = new List<Customer>();
            try
            {
                CustomerManager customerManager = new CustomerManager();
                _customers = customerManager.GetAllActiveCustomers();

                datCustomer.ItemsSource = _customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void populateCustomerForMan()
        {
            _manCustomers = new List<Customer>();
            try
            {
                CustomerManager customerManager = new CustomerManager();
                _manCustomers = customerManager.GetAllActiveCustomers();

                datCustomerMan.ItemsSource = _manCustomers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void populateVehiclesForMan()
        {
            _manVehicles = new List<Vehicle>();
            try
            {

                VehicleManager vehicleManager = new VehicleManager();
                _manVehicles = vehicleManager.GetAllAvailableVehicles();

                datManVehicle.ItemsSource = _manVehicles;
                
                //datManVehicle.Columns.RemoveAt(10);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            
        }


        private void populateVehicleInfo(int vehicleID)
        {
            _factoryOptions = new VehicleFactoryOptions();
            Vehicle vehicle = new Vehicle();
            

            VehicleManager vehicleManager = new VehicleManager();
            vehicle = vehicleManager.GetVehicleByVehicleID(vehicleID);
            _factoryOptions = vehicleManager.GetVehicleFactoryOptionsByVehicleID(vehicleID);

            txtVehMake.Text = vehicle.Make;
            txtVehModel.Text = vehicle.Model;
            txtVehYear.Text = vehicle.Year;
            txtVehVin.Text = vehicle.Vin;
            txtVehMile.Text = vehicle.Mileage.ToString();
            txtVehStyle.Text = vehicle.BodyStyle;
            txtVehSubModel.Text = vehicle.SubModel;

            txtVehEngine.Text = _factoryOptions.EngineSize;
            txtVehCyl.Text = _factoryOptions.CylinderCount.ToString();
            txtVehTrans.Text = _factoryOptions.Transmission;
            txtVehDrive.Text = _factoryOptions.DriveLine;
            txtVehIntMat.Text = _factoryOptions.InteriorMaterial;
            txtVehIntCol.Text = _factoryOptions.InteriorColor;
            txtVehColor.Text = _factoryOptions.ExteriorColor;

        }

        private void populateManVehicleInfo(int vehicleID)
        {
            _factoryOptions = new VehicleFactoryOptions();
            Vehicle vehicle = new Vehicle();


            VehicleManager vehicleManager = new VehicleManager();
            vehicle = vehicleManager.GetVehicleByVehicleID(vehicleID);
            _factoryOptions = vehicleManager.GetVehicleFactoryOptionsByVehicleID(vehicleID);

            txtManVehMake.Text = vehicle.Make;
            txtManVehModel.Text = vehicle.Model;
            txtManVehYear.Text = vehicle.Year;
            txtManVehVin.Text = vehicle.Vin;
            txtManVehMile.Text = vehicle.Mileage.ToString();
            txtManVehStyle.Text = vehicle.BodyStyle;
            txtManVehSubModel.Text = vehicle.SubModel;

            txtManVehEngine.Text = _factoryOptions.EngineSize;
            txtManVehCyl.Text = _factoryOptions.CylinderCount.ToString();
            txtManVehTrans.Text = _factoryOptions.Transmission;
            txtManVehDrive.Text = _factoryOptions.DriveLine;
            txtVehManIntMat.Text = _factoryOptions.InteriorMaterial;
            txtManVehIntCol.Text = _factoryOptions.InteriorColor;
            txtManVehColor.Text = _factoryOptions.ExteriorColor;

        }



        // Events

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Check if user wants to sign out
            if (btnLogin.Content.Equals("Sign out"))
            {
                UpdateUILogout();
                return;
            }


            var email = this.txtEmail.Text;
            var password = this.txtPassword.Password;


            // check email length to see if valid
            if (email.Length < 5)
            {
                MessageBox.Show("Bad Email Address entered");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
            //check for a password
            if (password.Length == 0)
            {
                MessageBox.Show("Please enter password.");
                txtPassword.Password = "";
                txtPassword.Focus();
                return;
            }

            var userManager = new UserManager();
            try
            {
                _user = userManager.LoginUser(email, password);
                UpdateUILogin(_user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        } // End of Button Click

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUILogout();
        }

        private void tabVehicles_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_vehicles == null)
            {
                populateVehiclesForSale();
            }

        }

        private void datVehicle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datVehicle.SelectedItems.Count > 0)
            {

                var selectedItem = (Vehicle)datVehicle.SelectedItem;

                populateVehicleInfo(selectedItem.VehicleID);
            }

        }

        private void tabVehicles_LostFocus(object sender, RoutedEventArgs e)
        {
            _vehicles = null;
        }

        private void tabManageVehicles_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_manVehicles == null)
            {
                populateVehiclesForMan();
            }
        }

        private void tabManageVehicles_LostFocus(object sender, RoutedEventArgs e)
        {
            _manVehicles = null;
        }

        private void tabCustomers_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_customers == null)
            {
                populateCustomerList();
            }
        }

        private void tabCustomers_LostFocus(object sender, RoutedEventArgs e)
        {
            _customers = null;
        }

        private void tabManageCustomers_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_manCustomers == null)
            {
                populateCustomerForMan();
            }
        }

        private void btnCustAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addCustomer = new AddCustomer();
                if ((bool)addCustomer.ShowDialog())
                {
                    MessageBox.Show("Customer has been added");
                    _customers = null;
                    populateCustomerForMan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCustDel_Click(object sender, RoutedEventArgs e)
        {
            if (datCustomerMan.SelectedItems.Count > 0)
            {
                try
                {
                    Customer cust = (Customer)datCustomerMan.SelectedItem;
                    CustomerManager customerManager = new CustomerManager();
                    if (customerManager.RemoveCustomer(cust.CustomerID))
                    {
                        MessageBox.Show("Selected Customer has been removed");
                        _customers = null;
                        populateCustomerForMan();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("Select a Customer to Remove");
            }
        }

        private void btnCustMod_Click(object sender, RoutedEventArgs e)
        {
            if(datCustomerMan.SelectedItems.Count > 0)
            {
                try
                {
                    Customer customer = (Customer)datCustomerMan.SelectedItem;
                    var updateCustomer = new UpdateCustomer(customer);
                    if ((bool)updateCustomer.ShowDialog())
                    {
                        MessageBox.Show("Customer information is updated");
                        _customers = null;
                        populateCustomerForMan();
                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("Select a Customer to Update");
            }
        }

        private void btnDelVeh_Click(object sender, RoutedEventArgs e)
        {
            if(datManVehicle.SelectedItems.Count > 0)
            {
                try
                {
                    _vehicle = new Vehicle();
                    _vehicle = (Vehicle)datManVehicle.SelectedItem;
                    VehicleManager vehicleManager = new VehicleManager();
                    bool completed = vehicleManager.DeleteVehicle(_vehicle.VehicleID);
                    if (completed)
                    {
                        MessageBox.Show("Vehicle has been deleted");
                        _manVehicles = null;
                        populateVehiclesForMan();
                    }
                    else
                    {
                        MessageBox.Show("A vehicle must be selected to Delete");
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            
        }

        private void datManVehicle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datManVehicle.SelectedItems.Count > 0)
            {
                Vehicle selectedItem = (Vehicle)datVehicle.SelectedItem;
                VehicleManager vehicleManager = new VehicleManager();
                populateManVehicleInfo(selectedItem.VehicleID);
            }
        }
    }
}

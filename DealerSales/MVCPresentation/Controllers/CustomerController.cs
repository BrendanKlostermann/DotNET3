using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace MVCPresentation.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ICustomerManager _customerManager;
        // GET: Customer
        public ActionResult CustomerList()
        {
            try
            {
                _customerManager = new CustomerManager();
                List<Customer> customers = _customerManager.GetAllActiveCustomers();

                return View(customers);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Could not Load customer list.";
                return View("CustomerError");
            }            
        }

        public ActionResult AddCustomer()
        {
            List<ZipCode> zipcodes = new ZipCodeManager().GetAllZipCodes();
            List<int> zips = new List<int>();
            foreach (var item in zipcodes)
            {
                zips.Add(item.Zipcode);
            }
            ViewBag.ZipCodes = zips;
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            _customerManager = new CustomerManager();
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = _customerManager.AddCustomer(customer);
                    if (success)
                    {
                        return RedirectToAction("CustomerList");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Could not add new customer at this time.";
                        return View("CustomerError");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message + "<br/><br/>" + ex.InnerException.Message;
                    return View("CustomerError");
                }
            }
            return View("Error");
        }

        public ActionResult DetailsCustomer(string id)
        {
            _customerManager = new CustomerManager();
            try
            {
                Customer customer = _customerManager.GetCustomerByID(int.Parse(id));
                return View(customer);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message + "<br/><br/>" + ex.InnerException.Message;
                return View("CustomerError");
            }           
        }

        public ActionResult EditCustomer(string id)
        {
            _customerManager = new CustomerManager();
            try
            {
                Customer customer = _customerManager.GetCustomerByID(int.Parse(id));
                List<ZipCode> zipcodes = new ZipCodeManager().GetAllZipCodes();
                List<int> zips = new List<int>();
                foreach (var item in zipcodes)
                {
                    zips.Add(item.Zipcode);
                }
                ViewBag.ZipCodes = zips;
                return View(customer);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message + "<br/><br/>" + ex.InnerException.Message;
                return View("CustomerError");
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            _customerManager = new CustomerManager();
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = _customerManager.UpdateCustomer(customer);
                    if (success)
                    {
                        return RedirectToAction("CustomerList");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Could not add new customer at this time.";
                        return View("CustomerError");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message + "<br/><br/>" + ex.InnerException.Message;
                    return View("CustomerError");
                }
            }
            return View("Error");
        }

        [Authorize(Roles = "Manager, Director, Administrator")]
        public ActionResult DeleteCustomer(int id)
        {
            _customerManager = new CustomerManager();
            try
            {
                Customer customer = _customerManager.GetCustomerByID(id);
                return View(customer);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Could not find customer";
                return View("CustomerError");
            }
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int? id)
        {
            _customerManager = new CustomerManager();
            if(id != null)
            {
                try
                {

                    _customerManager.RemoveCustomer((int)id);
                    return RedirectToAction("CustomerList");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "Could not delete customer.";
                    return View("CustomerError");
                }
            }

            return View("Error");
            
        }
    }
}
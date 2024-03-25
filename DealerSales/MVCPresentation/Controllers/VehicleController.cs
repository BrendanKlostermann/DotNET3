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
    public class VehicleController : Controller
    {
        IVehicleManager _vehicleManager;
        // GET: Vehicle

        
        public ActionResult VehicleList()
        {
            _vehicleManager = new VehicleManager();
            try
            {
                List<Vehicle> vehicles = _vehicleManager.GetAllAvailableVehicles();
                return View(vehicles);
            }catch(Exception){
                ViewBag.ErrorMessage = "Could not load Vehicle list";
                return View("VehicleError");
            }
            
        }

        [Authorize(Roles = "Manager, Director, Administrator")]
        public ActionResult AddVehicle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddVehicle(Vehicle vehicle)
        {
            _vehicleManager = new VehicleManager();
            if (ModelState.IsValid)
            {
                try
                {
                    vehicle.Available = true;
                    ViewBag.vehicle = vehicle;
                    return RedirectToAction("AddFactoryOptions",vehicle);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occured collecting vehicle data.";
                    return View("Error");
                }
            }
            return View();
        }

        [Authorize(Roles = "Manager, Director, Administrator")]
        public ActionResult AddFactoryOptions(Vehicle vehicle)
        {
            try
            {
                VehicleViewModel viewModel = new VehicleViewModel();
                viewModel.Vehicle = vehicle;
                viewModel.FactoryOptions = new VehicleFactoryOptions();
                return View(viewModel);
            }catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddFactoryOptions(VehicleViewModel viewModel)
        {
            try
            {
                _vehicleManager = new VehicleManager();
                Vehicle vehicle = viewModel.Vehicle;

                VehicleFactoryOptions vehicleOptions = viewModel.FactoryOptions;

                vehicleOptions.VehicleID = _vehicleManager.AddVehicle(vehicle);

                _vehicleManager.AddVehicleOptions(vehicleOptions);

                return RedirectToAction("VehicleList");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Could not add vehicle and options." + ex.InnerException.Message;
                return View("Error");
            }

        }

        public ActionResult DetailsVehicle(string id)
        {
            _vehicleManager = new VehicleManager();
            try
            {
                Vehicle vehicle = _vehicleManager.GetVehicleByVehicleID(int.Parse(id));
                VehicleFactoryOptions options = _vehicleManager.GetVehicleFactoryOptionsByVehicleID(int.Parse(id));

                ViewBag.Options = options;
                return View(vehicle);
            }
            catch
            {
                ViewBag.Error = "Could not find vehicle";
                return View("Error");
            }          
        }

        [Authorize(Roles = "Manager, Director, Administrator")]
        public ActionResult UpdateVehicle(string id)
        {
            _vehicleManager = new VehicleManager();
            VehicleViewModel viewModel = new VehicleViewModel();
            try
            {
                Vehicle vehicle = _vehicleManager.GetVehicleByVehicleID(int.Parse(id));
                VehicleFactoryOptions options = _vehicleManager.GetVehicleFactoryOptionsByVehicleID(int.Parse(id));
                viewModel.Vehicle = vehicle;
                viewModel.FactoryOptions = options;
            }
            catch
            {
                ViewBag.Error = "Could not find vehicle or optionns";
                return View("Error");
            }

            return View(viewModel);
        }



        [HttpPost]
        public ActionResult UpdateVehicle(VehicleViewModel viewModel)
        {
            _vehicleManager = new VehicleManager();

            if (ModelState.IsValid)
            {
                try
                {

                    Vehicle vehicle = viewModel.Vehicle;
                    VehicleFactoryOptions options = viewModel.FactoryOptions;

                    //Add Logic layer down for updating a vehicle and the factory options

                    return RedirectToAction("VehicleList");

                }
                catch
                {
                    ViewBag.Error = "Could not update vehicle information";
                    return View("Error");
                }
            }


            return View("Error");
        }





        [Authorize(Roles = "Manager, Director, Administrator")]
        public ActionResult DeleteVehicle(string id)
        {
            _vehicleManager = new VehicleManager();
            Vehicle vehicle = _vehicleManager.GetVehicleByVehicleID(int.Parse(id));

            return View(vehicle);
        }

        [HttpPost]
        public ActionResult DeleteVehicle(int? id)
        {
            _vehicleManager = new VehicleManager();
            if(id != null)
            {
                try
                {
                    _vehicleManager.DeleteVehicle((int)id);
                    return RedirectToAction("VehicleList");
                }
                catch(Exception)
                {
                    ViewBag.Error = "Could not delete vehicle";
                    return View("Error");
                }
            }
            return View("Error");            
        }


    }
}
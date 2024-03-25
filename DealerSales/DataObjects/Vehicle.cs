using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public string SubModel { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public decimal ServiceCost { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string Vin { get; set; }
        [Required]
        public string BodyStyle { get; set; }
        public bool Available { get; set; }

    }

    public class VehicleFactoryOptions
    {
        [Required]
        public int VehicleID { get; set; }
        [Required]
        public string EngineSize { get; set; }
        [Required]
        public int CylinderCount { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string DriveLine { get; set; }
        [Required]
        public string InteriorMaterial { get; set; }
        [Required]
        public string InteriorColor { get; set; }
        [Required]
        public string ExteriorColor { get; set; }

    }
}

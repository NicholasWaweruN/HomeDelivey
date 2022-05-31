using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels.HD_ViewModels
{
    public class CustomerOrderViewModel
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty; 
        public string Street { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string BuildingName { get; set; } = string.Empty;
        public string HouseNumber { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MaterialCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
    public class Components
    {
        public string MaterialCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

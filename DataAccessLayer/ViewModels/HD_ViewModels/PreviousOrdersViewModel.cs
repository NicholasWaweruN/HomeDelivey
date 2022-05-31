using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels.HD_ViewModels
{
    public class PreviousOrdersViewModel
    {
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
    }
}

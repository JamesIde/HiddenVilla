using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StripePaymentDTO
    {
        public string ProductName { get; set; }
        public long Cost { get; set; }
        public string ImageURL { get; set; }
        public string ReturnURL { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.DTOs
{
    public class ProductResponse
    {

        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime LoadDate { get; set; }

        public int IdCategory { get; set; }

    }
}

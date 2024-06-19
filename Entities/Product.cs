using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime LoadDate { get; set; }

        public int IdCategory { get; set; }

        public virtual Category? IdCategoryNavigation { get; set; }
    }
}

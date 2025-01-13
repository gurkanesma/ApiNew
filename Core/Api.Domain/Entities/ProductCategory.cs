using Api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class ProductCategory : IEntityBase
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Navigasyon özelliği
        public Product Product { get; set; } // Navigasyon özelliği

    }
}

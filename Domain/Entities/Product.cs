using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public Decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int BrandID { get; set; }
        public ProductType ProductType { get; set; }
        public int TypeID { get; set; }
    }
}

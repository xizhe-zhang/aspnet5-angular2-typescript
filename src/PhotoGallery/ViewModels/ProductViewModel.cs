using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string StoreName { get; set; }
        public string PicUrl { get; set; }
    }
}

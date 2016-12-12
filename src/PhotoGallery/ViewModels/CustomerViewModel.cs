using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<int> WechatId { get; set; }
    }
}

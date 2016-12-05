using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsLocked { get; set; }
        public string Salt { get; set; }
        public string Username { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> WechatId { get; set; }
        public String WechatWechatId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Entities
{
    public class User : IEntityBase
    {
        public User()
        {
            this.Sessions = new HashSet<Session>();
            this.UserRoles = new HashSet<UserRole>();
        }
    
        public int Id { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsLocked { get; set; }
        public string Salt { get; set; }
        public string Username { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> WechatId { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual Store Store { get; set; }
        public virtual Wechat Wechat { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

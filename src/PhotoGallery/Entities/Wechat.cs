namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public class Wechat : IEntityBase
    {
        public Wechat()
        {
            this.Customers = new HashSet<Customer>();
            this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string WechatId { get; set; }
        public string WechatName { get; set; }
        public string WechatImageUrl { get; set; }
        public string UnionId { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

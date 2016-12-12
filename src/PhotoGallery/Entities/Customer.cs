namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public class Customer : IEntityBase
    {
        public Customer()
        {
            this.Sessions = new HashSet<Session>();
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<int> WechatId { get; set; }
    
        public virtual Wechat Wechat { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}

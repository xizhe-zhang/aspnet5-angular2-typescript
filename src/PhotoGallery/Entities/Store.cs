namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public class Store : IEntityBase
    {
        public Store()
        {
            this.Orders = new HashSet<Order>();
            this.Products = new HashSet<Product>();
            this.Sessions = new HashSet<Session>();
            this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
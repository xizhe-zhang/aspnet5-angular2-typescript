namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderLine: IEntityBase
    {
        public OrderLine()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Product Product { get; set; }
    }
}
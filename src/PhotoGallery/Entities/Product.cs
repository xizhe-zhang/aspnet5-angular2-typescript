namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public class Product : IEntityBase
    {
        public Product()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string PicUrl { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual Store Store { get; set; }
    }
}
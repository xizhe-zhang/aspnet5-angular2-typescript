namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;

    public class Order : IEntityBase
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<System.DateTime> PlaceOrderDate { get; set; }
        public Nullable<int> OrderLineId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual OrderLine OrderLine { get; set; }
        public virtual Store Store { get; set; }
    }
}

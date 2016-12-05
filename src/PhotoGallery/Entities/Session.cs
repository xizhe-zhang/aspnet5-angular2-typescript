namespace PhotoGallery.Entities
{
    using System;
    using System.Collections.Generic;
    
    public class Session: IEntityBase
    {
        public int Id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string SessionKey { get; set; }
        public Nullable<bool> IsConnected { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}

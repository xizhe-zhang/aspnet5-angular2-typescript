using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(PhotoGalleryContext context)
            : base(context)
        { }

        public Customer GetSingleByCustomername(string customerName)
        {
            return this.GetSingle(x => x.Name == customerName);
        }
    }
}

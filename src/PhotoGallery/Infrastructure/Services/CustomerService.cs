using PhotoGallery.Entities;
using PhotoGallery.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepositor)
        {
            _customerRepository = customerRepositor;
        }

        public Customer CreateCustomer(string customerName, string email, int wechatId)
        {
            var existingCustomer = _customerRepository.GetSingleByCustomername(customerName);

            if (existingCustomer != null)
            {
                return existingCustomer;
            }

            var customer = new Customer()
            {
                StoreId = 1,
                Name = customerName,
                Email = email,
                WechatId = wechatId
            };

            _customerRepository.Add(customer);
            _customerRepository.Commit();

            return customer;
        }
    }
}
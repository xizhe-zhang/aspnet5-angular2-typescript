using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;
using PhotoGallery.ViewModels;
using AutoMapper;
using PhotoGallery.Infrastructure.Repositories;
using PhotoGallery.Infrastructure.Core;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        ICustomerRepository _customerRepository;
        ILoggingRepository _loggingRepository;
        public CustomersController(ICustomerRepository customerRepository, ILoggingRepository loggingRepository)
        {
            _customerRepository = customerRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet("{storeId:int=0}/{page:int=0}/{pageSize=12}")]
        public PaginationSet<CustomerViewModel> Get(int? storeId, int? page, int? pageSize)
        {
            PaginationSet<CustomerViewModel> pagedSet = null;
            try
            {
                int currentPage = page.Value;
                int currentPageSize = pageSize.Value;

                List<Customer> _customers = null;
                int _totalCustomers = new int();

                _customers = _customerRepository
                    .AllIncluding( p => p.Store)
                    .Where(p => p.StoreId == storeId)
                    .OrderBy(p => p.Id)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                _totalCustomers = _customerRepository.FindBy(p => p.StoreId == storeId).Count();

                IEnumerable<CustomerViewModel> _customersVM = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customers);

                pagedSet = new PaginationSet<CustomerViewModel>()
                {
                    Page = currentPage,
                    TotalCount = _totalCustomers,
                    TotalPages = (int)Math.Ceiling((decimal)_totalCustomers / currentPageSize),
                    Items = _customersVM
                };
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            return pagedSet;
        }
    }
}

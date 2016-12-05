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
    public class ProductsController : Controller
    {
        IProductRepository _productRepository;
        ILoggingRepository _loggingRepository;
        public ProductsController(IProductRepository productRepository, ILoggingRepository loggingRepository)
        {
            _productRepository = productRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet("{storeId:int=0}/{page:int=0}/{pageSize=12}")]
        public PaginationSet<ProductViewModel> Get(int? storeId, int? page, int? pageSize)
        {
            PaginationSet<ProductViewModel> pagedSet = null;

            try
            {
                int currentPage = page.Value;
                int currentPageSize = pageSize.Value;

                List<Product> _products = null;
                int _totalProducts = new int();

                _products = _productRepository
                    .AllIncluding( p => p.Store)
                    .Where(p => p.StoreId == storeId)
                    .OrderBy(p => p.Id)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                _totalProducts = _productRepository.FindBy(p => p.StoreId == storeId).Count();

                Console.Out.WriteLine(_totalProducts.ToString());

                IEnumerable<ProductViewModel> _productsVM = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_products);

                pagedSet = new PaginationSet<ProductViewModel>()
                {
                    Page = currentPage,
                    TotalCount = _totalProducts,
                    TotalPages = (int)Math.Ceiling((decimal)_totalProducts / currentPageSize),
                    Items = _productsVM
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

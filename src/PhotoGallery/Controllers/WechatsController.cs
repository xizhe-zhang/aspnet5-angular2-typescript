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
    public class WechatsController : Controller
    {
        IWechatRepository _wechatRepository;
        ILoggingRepository _loggingRepository;
        public WechatsController(IWechatRepository wechatRepository, ILoggingRepository loggingRepository)
        {
            _wechatRepository = wechatRepository;
            _loggingRepository = loggingRepository;
        }

        public PaginationSet<WechatViewModel> GetByWechatId(string wechatId)
        {
            PaginationSet<WechatViewModel> pagedSet = null;

            try
            {
                int currentPage = 0;
                int currentPageSize = 100;

                List<Wechat> _wechats = null;
                int _totalWechats = new int();

                _wechats = _wechatRepository
                    .FindBy(p => p.WechatId.Equals(wechatId))
                    .OrderBy(p => p.Id)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                _totalWechats = _wechatRepository.FindBy(p => p.WechatId.Equals(wechatId)).Count();

                IEnumerable<WechatViewModel> _wechatsVM = Mapper.Map<IEnumerable<Wechat>, IEnumerable<WechatViewModel>>(_wechats);

                pagedSet = new PaginationSet<WechatViewModel>()
                {
                    Page = currentPage,
                    TotalCount = _totalWechats,
                    TotalPages = (int)Math.Ceiling((decimal)_totalWechats / currentPageSize),
                    Items = _wechatsVM
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

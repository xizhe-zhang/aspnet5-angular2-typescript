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
    public class UsersController : Controller
    {
        IUserRepository _userRepository;
        ILoggingRepository _loggingRepository;
        public UsersController(IUserRepository userRepository, ILoggingRepository loggingRepository)
        {
            _userRepository = userRepository;
            _loggingRepository = loggingRepository;
        }

        public PaginationSet<UserViewModel> GetByWechatId(string wechatId)
        {
            PaginationSet<UserViewModel> pagedSet = null;

            try
            {
                int currentPage = 0;
                int currentPageSize = 100;

                List<User> _users = null;
                int _totalUsers = new int();

                _users = _userRepository
                    .AllIncluding(p => p.Wechat)
                    .Where(p => p.Wechat.WechatId.Equals(wechatId))
                    .OrderBy(p => p.Id)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                _totalUsers = _userRepository.AllIncluding(p => p.Wechat).Where(p => p.WechatId.Equals(wechatId)).Count();

                IEnumerable<UserViewModel> _usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_users);

                pagedSet = new PaginationSet<UserViewModel>()
                {
                    Page = currentPage,
                    TotalCount = _totalUsers,
                    TotalPages = (int)Math.Ceiling((decimal)_totalUsers / currentPageSize),
                    Items = _usersVM
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

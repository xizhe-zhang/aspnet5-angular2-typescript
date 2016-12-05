using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Infrastructure.Services;
using PhotoGallery.Infrastructure.Repositories;
using PhotoGallery.Entities;
using PhotoGallery.ViewModels;
using PhotoGallery.Infrastructure.Core;
using PhotoGallery.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserRepository _userRepository;
        private readonly ILoggingRepository _loggingRepository;

        public AccountController(IMembershipService membershipService,
            IUserRepository userRepository,
            ILoggingRepository _errorRepository)
        {
            _membershipService = membershipService;
            _userRepository = userRepository;
            _loggingRepository = _errorRepository;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel user)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _authenticationResult = null;

            try
            {
                MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                if (_userContext.User != null)
                {
                    IEnumerable<Role> _roles = _userRepository.GetUserRoles(user.Username);
                    List<Claim> _claims = new List<Claim>();
                    foreach (Role role in _roles)
                    {
                        Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Username);
                        _claims.Add(_claim);
                    }
                    await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                        new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties { IsPersistent = user.RememberMe });


                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = true,
                        Message = Convert.ToString(_userContext.User.Id)
                    };
                }
                else
                {
                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Authentication failed"
                    };
                }
            }
            catch (Exception ex)
            {
                _authenticationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            _result = new ObjectResult(_authenticationResult);
            return _result;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.Authentication.SignOutAsync("Cookies");
                return Ok();
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();

                return BadRequest();
            }

        }

        [HttpGet("pos/{id:int}")]
        public string Get(int id)
        {
            try
            {
                PosViewModel pos = new PosViewModel();
                pos.Id = id;
                string callbackFunctionName = Request.Query["callback"];
                string posID = Request.Query["posID"];
                string name = Request.Query["name"];
                string imageURL = Request.Query["imageURL"];
                string jsCode = callbackFunctionName + "({\"Status\":\"OK\"});";
                User _user = _membershipService.CreateUser("POS-" + posID, "POS-" + posID + "@netsdl.com", "111", new int[] { 1 });
                return jsCode;
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                return Message;
            }
        }

        [HttpGet("delete/{id:int}")]
        public string Delete(int id)
        {
            User _userToRemove = this._userRepository.GetSingle(id);
            this._userRepository.Delete(_userToRemove);
            this._userRepository.Commit();
            return "OK";
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegistrationViewModel user)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _registrationResult = null;

            try
            {
                if (ModelState.IsValid)
                {
                    User _user = _membershipService.CreateUser(user.Username, user.Email, user.Password, new int[] { 1 });

                    if (_user != null)
                    {
                        _registrationResult = new GenericResult()
                        {
                            Succeeded = true,
                            Message = "Registration succeeded"
                        };
                    }
                }
                else
                {
                    _registrationResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Invalid fields."
                    };
                }
            }
            catch (Exception ex)
            {
                _registrationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            _result = new ObjectResult(_registrationResult);
            return _result;
        }
    }
}

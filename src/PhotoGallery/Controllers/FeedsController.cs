using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using PhotoGallery.Hubs;
using PhotoGallery.ViewModels;
using AutoMapper;
using PhotoGallery.Infrastructure.Services;
using PhotoGallery.Infrastructure.Repositories;
using PhotoGallery.Infrastructure.Core;
using PhotoGallery.Entities;
using System.Threading.Tasks;
using System;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    public class FeedsController : ApiHubController<Broadcaster>
    {
        ICustomerService _customerService;
        IWechatService _wechatService;
        ISessionService _sessionService;
        ILoggingRepository _loggingRepository;
        public FeedsController(
            IConnectionManager signalRConnectionManager, ICustomerService customerService, IWechatService wechatService, ISessionService sessionService, ILoggingRepository loggingRepository)
        : base(signalRConnectionManager)
        {
            this._customerService = customerService;
            this._wechatService = wechatService;
            this._sessionService = sessionService;
            this._loggingRepository = loggingRepository;
        }

        // POST api/feeds
        [HttpGet("pos/{id:int}")]
        public async Task<string> Get(int id)
        {
            string callbackFunctionName = Request.Query["callback"];
            string type = Request.Query["type"];
            string sessionKey = Request.Query["sessionKey"];
            string jsCode = callbackFunctionName + "({\"Status\":\"OK\"});";
            string wechatID = "";
            string name = "";
            string imageURL = "";
            string barcode = "";

            try
            {
                if (type.Equals("login"))
                {
                    wechatID = Request.Query["wechatID"];
                    name = Request.Query["name"];
                    imageURL = Request.Query["imageURL"];
                    Wechat wechat = this._wechatService.CreateWechat(wechatID, name, imageURL);
                    Customer customer = this._customerService.CreateCustomer(name, name + "@netsdl.com", wechat.Id);
                    Session session = this._sessionService.CreateSession(sessionKey, customer.Id);
                };

                if (type.Equals("barcode"))
                {
                    barcode = Request.Query["barcode"];
                }

                var wechatModel = new WechatViewModel();
                wechatModel.SessionKey = sessionKey;
                wechatModel.WechatName = name;
                wechatModel.WechatImageUrl = imageURL;
                wechatModel.Barcode = barcode;

                await Clients.Group("1").AddFeed(wechatModel);
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            return jsCode;
        }

    }
}

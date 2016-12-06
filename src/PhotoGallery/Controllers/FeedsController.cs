using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using PhotoGallery.Hubs;
using PhotoGallery.ViewModels;
using AutoMapper;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    public class FeedsController : ApiHubController<Broadcaster>
    {

        public FeedsController(
            IConnectionManager signalRConnectionManager)
        : base(signalRConnectionManager)
        {
        }

        // POST api/feeds
        [HttpGet("pos/{id:int}")]
        public async void Get(int id)
        {
            string callbackFunctionName = Request.Query["callback"];
            string posID = Request.Query["posID"];
            string name = Request.Query["name"];
            string imageURL = Request.Query["imageURL"];
            string jsCode = callbackFunctionName + "({\"Status\":\"OK\"});";

            var wechat = new WechatViewModel();
            wechat.WechatName = name;
            wechat.WechatImageUrl = imageURL;
                
            await Clients.Group("1").AddFeed(wechat);
        }

    }
}

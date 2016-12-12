using PhotoGallery.Entities;
using PhotoGallery.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Services
{
    public class WechatService : IWechatService
    {
        private readonly IWechatRepository _wechatRepository;

        public WechatService(IWechatRepository wechatRepositor)
        {
            _wechatRepository = wechatRepositor;
        }

        public Wechat CreateWechat(string wechatId, string wechatName, string wechatImageUrl)
        {
            var existingWechat = _wechatRepository.GetSingleByWechatId(wechatId);

            if (existingWechat != null)
            {
                return existingWechat;
            }

            var wechat = new Wechat()
            {
                WechatId = wechatId,
                WechatName = wechatName,
                WechatImageUrl = wechatImageUrl
            };

            _wechatRepository.Add(wechat);
            _wechatRepository.Commit();

            return wechat;
        }
    }
}
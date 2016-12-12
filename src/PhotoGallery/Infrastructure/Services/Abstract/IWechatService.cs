using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Services
{
    public interface IWechatService
    {
        Wechat CreateWechat(string wechatId, string wechatName, string wechatImageUrl);
    }
}
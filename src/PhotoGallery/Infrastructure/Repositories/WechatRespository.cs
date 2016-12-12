using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Repositories
{
    public class WechatRepository : EntityBaseRepository<Wechat>, IWechatRepository
    {
        public WechatRepository(PhotoGalleryContext context)
            : base(context)
        { }

        public Wechat GetSingleByWechatId(string wechatId)
        {
            return this.GetSingle(x => x.WechatId == wechatId);
        }
    }
}
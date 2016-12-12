using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Repositories
{
    public class SessionRepository : EntityBaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(PhotoGalleryContext context)
            : base(context)
        { }

        public Session GetSingleBySessionKey(string sessionKey)
        {
            return this.GetSingle(x => x.SessionKey == sessionKey);
        }

    }
}

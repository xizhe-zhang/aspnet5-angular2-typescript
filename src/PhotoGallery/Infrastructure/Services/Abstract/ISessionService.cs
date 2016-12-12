using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Services
{
    public interface ISessionService
    {
        Session CreateSession(string sessionKey, int customerId);
    }
}

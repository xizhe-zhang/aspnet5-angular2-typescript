using PhotoGallery.Entities;
using PhotoGallery.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepositor)
        {
            _sessionRepository = sessionRepositor;
        }

        public Session CreateSession(string sessionKey, int customerId)
        {
            var existingSession = _sessionRepository.GetSingleBySessionKey(sessionKey);

            if (existingSession != null)
            {
                return existingSession;
            }

            var session = new Session()
            {
                SessionKey = sessionKey,
                StoreId = 1,
                UserId = 1,
                CustomerId = customerId
            };

            try
            {
                _sessionRepository.Add(session);
                _sessionRepository.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return session;
        }
    }
}
using PhotoGallery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Infrastructure.Repositories
{
    public interface IAlbumRepository : IEntityBaseRepository<Album> { }
    public interface ILoggingRepository : IEntityBaseRepository<Error> { }
    public interface IPhotoRepository : IEntityBaseRepository<Photo> { }
    public interface IRoleRepository : IEntityBaseRepository<Role> { }
    public interface IProductRepository : IEntityBaseRepository<Product> { }
    public interface IWechatRepository : IEntityBaseRepository<Wechat>
    {
        Wechat GetSingleByWechatId(string wechatId);
    }
    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {
        Customer GetSingleByCustomername(string customerName);
    }
    public interface ISessionRepository : IEntityBaseRepository<Session> {
        Session GetSingleBySessionKey(string sessionKey);
     }
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        User GetSingleByUsername(string username);
        IEnumerable<Role> GetUserRoles(string username);
    }
    public interface IUserRoleRepository : IEntityBaseRepository<UserRole> { }
}

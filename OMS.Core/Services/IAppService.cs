
namespace OMS.Core.Services.AppServices
{
    public interface IAppService<T>
    {
        bool Add(T entity);
        bool Update(T entity);
    }
}

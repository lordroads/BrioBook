namespace Brio.Authentication.Services;

public interface IRepository<TClass, TKey>
{
    IList<TClass> GetAll();
    TClass Get(TKey key);
    TClass GetByValue(string value);
    TKey Create(TClass data);
    bool Delete(TKey key);
    bool Update(TClass data);
}

namespace Brio.Confirm.Services;

public interface IRepository<TClass, TKey>
{
    public IList<TClass> GetAll();
    public TClass Get(TKey key);
    public TClass GetByValue(string value);
    public TKey Create(TClass data);
    public bool Delete(TKey key);
    public bool Update(TClass data);
}
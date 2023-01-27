namespace BrioBook.Account.Services;

public interface IRepository<TClass, TKey>
{
    TKey Add(TClass entity);
    TClass GetById(TKey id);
    TClass GetByValue(string value);
    ICollection<TClass> GetAll();
    bool Update(TClass entity);
    bool Delete(TKey id);
}
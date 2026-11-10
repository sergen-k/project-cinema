public interface IDataAccess<T>
{
    public void Write(T model);
    public T SelectAllById(int id);
    public void Update(T model);
    public void Delete(T model);
}

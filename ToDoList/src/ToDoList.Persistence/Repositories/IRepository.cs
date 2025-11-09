namespace ToDoList.Persistence.Repositories;

public interface IRepository<T>
    where T : class
{
    //CREATE AN ITEM
    public void Create(T item);

    //READ ALL
    public IEnumerable<T> GetAll();

    //READ BY ID
    T? GetById(int id); //otazník znamená, že proměná může být i null

    // UPDATE
    bool Update(int id, T request);

    // DELETE
    bool Delete(int id);
}

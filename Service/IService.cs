namespace ItemRazorV1.Service
{
    public interface IService<T>// where T : class
    {
        // CRUD: Create (Add), Read (Get), Update, Delete

        Task<IEnumerable<T>> GetObjectsAsync();
        Task AddObjectAsync(T obj);
        Task DeleteObjectAsync(T obj);
        Task UpdateObjectAsync(T obj);
        Task<T> GetObjectByIdAsync(int id);
        Task SaveObjects(List<T> objs);
    }
}
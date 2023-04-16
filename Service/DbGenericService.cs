using ItemRazorV1.EFDbContext;
using Microsoft.EntityFrameworkCore;

namespace ItemRazorV1.Service
{
    public class DbGenericService<T> : IService<T> where T : class
    {
        //Husk: At registrere den nye Service i Program.cs.. Som alle andre Services...



        /// <summary>
        /// BEMÆRK
        /// Set<T>() referere til DbSet<Item> eller DbSet<User>.. afhængig af T...
        /// AsNoTracking() er blot en lille optimering så der ikke trackes på den returnerede liste (f(x) fra EntityFrameworkCore)
        /// (den skal ikke opdateres og dermed heller ikke DB)
        /// </summary>

        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        /// <summary>
        /// BEMÆRK
        /// Der benyttes await og SaveChangesAsync for at sikre asynkron afvikling
        /// og at der ikke returneres fra metoden før opdateringen er gennemført.
        /// </summary>

        public async Task AddObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }

        }

        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new ItemDbContext())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }
        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }
           

        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}

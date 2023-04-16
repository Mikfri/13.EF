using ItemRazorV1.EFDbContext;
using ItemRazorV1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemRazorV1.Service
{
    public class DbService
    {

        public async Task<List<Item>> GetItems()
        {
            using (var context = new ItemDbContext())
            {
                return await context.Items.ToListAsync();
            }
        }


        public async Task<List<User>> GetUsers()
        {
            using (var context = new ItemDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task AddItem(Item item)
        {
            using (var context = new ItemDbContext())
            {
                context.Items.Add(item);
                context.SaveChanges();
            }
        }


        public async Task AddUser(User user)
        {
            using (var context = new ItemDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }


        public async Task SaveItems(List<Item> items)
        {
            using (var context = new ItemDbContext())
            {
                foreach (Item item in items)
                {
                    context.Items.Add(item);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }

        public async Task SaveUsers(List<User> users)
        {
            using (var context = new ItemDbContext())
            {
                foreach (User user in users)
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                }
                context.SaveChanges();
            }
        }

    }
}

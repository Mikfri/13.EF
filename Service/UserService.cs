using ItemRazorV1.MockData;
using ItemRazorV1.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

namespace ItemRazorV1.Service
{
    public class UserService
    {    

        public List<User> Users { get; set; }
        private JsonFileService<User> JsonFileUserService { get; set; }
        private DbService DbService { get; set; }
        private DbGenericService<User> _dbGenericService;


        public UserService(JsonFileService<User> jsonFileUserService, DbGenericService<User> dbGenericService)
        {
            JsonFileUserService = jsonFileUserService;
            _dbGenericService = dbGenericService;
            //DbService = dbService;
            //Users = MockUsers.GetMockUsers();
            //JsonFileUserService.SaveJsonObjects(Users); // Her får vi gemt Users ned i root folderen hvor vi fremover vil have vores data (en lignende kode MÅ tidligere have været i Items konstruktoren!)

            Users = JsonFileUserService.GetJsonObjects().ToList();
            //DbService.SaveUsers(Users);   // Bruges KUN første gang ved SAVE fra JSON til DB
            _dbGenericService.SaveObjects(Users);   // Bruges KUN første gang ved SAVE fra JSON til DB via. den generiske service
            //Users = _dbGenericService.GetObjectsAsync().Result.ToList();
            //Users = DbService.GetUsers().Result;
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            //JsonFileUserService.SaveJsonObjects(Users);
            _dbGenericService.AddObjectAsync(user);
            //DbService.AddUser(user);
        }

        public User GetUserByUserName(string userByUsername)
        {
            return Users.Find(aUser => string.IsNullOrEmpty(userByUsername) || aUser.UserName.ToLower() == userByUsername.ToLower());  // .Find.. Ikke .FindAll, dumbass!
            // return Users.Find(aUser => aUser.UserName == userByUsername); // Alternativt..
        }


    }
}

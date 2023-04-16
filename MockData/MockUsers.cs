using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using ItemRazorV1.Models;
using Microsoft.AspNetCore.Identity;

namespace ItemRazorV1.MockData
{
    public class MockUsers
    {

        /// <summary>
        /// Step 2 (MockUsers)
        /// Tilføj en class MockUsers til mappen MockData. Klassen skal have en statisk liste users,
        /// initialiseret med et par User objekter, samt en statisk metode 
        /// GetMockUsers(), der returnere listen users.
        /// 
        /// Step 3 (UserService)
        /// </summary>


        /// Step 11 (MockUsers)
        /// Først skal vi have krypteret vores password i MockUser det gøres ved kald af metoden: HashPassword fra klassen PaswordHasher, ala:


        public User User { get; set; }

        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        private static List<User> _usersList = new List<User>()
        {
            // Første spot i parameteren er for alghoritmetyepen.´null´er default. End alternativ kunne være "SHA256" som har en anden encryption.
            new User("Mikkel", passwordHasher.HashPassword(null, "SpongeIsGod")),
            new User("Nicolai", passwordHasher.HashPassword(null, "SleepIsOverrated")),
            new User("Phillip", passwordHasher.HashPassword(null,"WhereMaNeed4Gaming?")),
            new User("Kenni", passwordHasher.HashPassword(null,"IsSchoolNecesarry?")),
            new User("admin", passwordHasher.HashPassword(null,"secret"))
        };

        //public static List<User> _usersList = new List<User>()
        //{
        //    new User("Mikkel", "SpongeIsGod"),
        //    new User("Nicolai", "SleepIsOverrated"),
        //    new User("Phillip", "WhereMaNeed4Gaming?"),
        //    new User("Kenni", "IsSchoolNecesarry?"),
        //    new User("admin", "secret")
        //};

        public static List<User> GetMockUsers()
        {
            return _usersList;
        }

    }
}

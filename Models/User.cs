using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace ItemRazorV1.Models
{
    public class User
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Range(typeof(string), "3", "20", ErrorMessage = "Navnet skal være imellem {1} og {2} bogstaver")]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }


        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User() { }

    }
}

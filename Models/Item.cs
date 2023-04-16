using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemRazorV1.Models
{
    public class Item : IComparable<Item>
    {
        [Display(Name = "Item ID")]
        [Required(ErrorMessage = "Der skal angives et ID til Item")]
        [Range(typeof(int), "0", "10000", ErrorMessage = "ID skal være mellem {1} og {2}")] //curlybois referere rigtigt nok

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        [Display(Name = "Item Navn")]
        [Required(ErrorMessage = "Item skal have et navn"), MaxLength(20)]  //BEMÆRK MaxLength er ligeså godt som StringLength(20)

        public string Name { get; set; }

        [Display(Name = "Pris")]
        [Range(typeof(decimal), minimum: "0", maximum: "10000", ErrorMessage = "Prisen skal være imellem {1} og {2}")]
        //[Required(ErrorMessage = "Der skal angives en pris")] ////minimum og maximum behøves ikke.. Det er normalt underforstået i rækkefølgen
        public decimal Price { get; set; }
        //public double? Price { get; set; }    //Spørgsmålstegnet her gør den gerne må være null.. Price er null-able

        public Item()
        {
            Id = 0;
            Name = "";
            Price = 0;
        }

        public Item(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int CompareTo(Item other)
        {         
            return Id - other.Id;
        }
    }
}

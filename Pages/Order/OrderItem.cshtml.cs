using ItemRazorV1.Models;
using ItemRazorV1.Pages.Item;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Order
{
    public class OrderItemModel : PageModel
    {

        private IItemService _itemService;
        private UserService _userService;
        private OrderService _orderService;


        public Models.User User { get; set; }
        public Models.Item Item { get; set; }
        public Models.Order Order { get; set; } = new Models.Order(); /* Property: Order er nu initialiseret, med new Order()
                                                  dvs. sige at en nyt object order oprettes herved?*/
        [BindProperty]
        public int Count { get; set; }



        //public void OnGet(int id)
        //{
        //    Item = _itemService.GetItem(id);
        //    User = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
        //}

        public void OnGet(int id)
        {
            Item = _itemService.GetItem(id);
            if (Item == null)
            {
                // håndter null-tilfælde her, f.eks. ved at vise en fejlbesked til brugeren
                return;
            }
            User = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
        }


        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Item = _itemService.GetItem(id);
            User = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
            
            Order.UserId = User.UserId;
            Order.ItemId = Item.Id;
            Order.DateTime = DateTime.Now;
            Order.Count = Count;
            _orderService.AddOrder(Order);

            return RedirectToPage("../Item/GetAllItems");
            //return RedirectToAction("Index"); // <-- ? Kan den bruges istedet?

        }
    }
}

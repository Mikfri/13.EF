using ItemRazor.Comperators;
using ItemRazorV1.MockData;
using ItemRazorV1.Models;

namespace ItemRazorV1.Service
{
    public class ItemService : IItemService
    {
        
        private List<Item> _items;
        private JsonFileService<Item> JsonFileItemService { get; set; }
        //private DbService DbService { get; set; }
        private DbGenericService<Item> _dbGenericService;

        public ItemService(JsonFileService<Item> jsonFileItemService, DbGenericService<Item> dbGenericService)
        {
            JsonFileItemService = jsonFileItemService;
            _dbGenericService = dbGenericService;
            //DbService = dbService;
            //JsonFileUserService.SaveJsonObjects(Items);
            // _items = MockItems.GetMockItems();
            _items = JsonFileItemService.GetJsonObjects().ToList();
            //_items = _dbGenericService.GetObjectsAsync().Result.ToList();
            //_items = DbService.GetItems().Result;
            _dbGenericService.SaveObjects(_items);  // Bruges KUN første gang ved SAVE fra JSON til DB
        }

        public ItemService()
        {
            _items = MockItems.GetMockItems();
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
            //JsonFileItemService.SaveJsonObjects(_items);
            //DbService.AddItem(item);
            _dbGenericService.AddObjectAsync(item);
        }

        public Item GetItem(int id)
        {
            foreach (Item item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            return null;
        }

        public void UpdateItem(Item item)
        {
            if (item != null)
            {
                foreach (Item i in _items)
                {
                    if (i.Id == item.Id)
                    {
                        i.Name = item.Name;
                        i.Price = item.Price;
                    }
                }
                JsonFileItemService.SaveJsonObjects(_items);
            }
        }

        public Item DeleteItem(int? itemId)
        {
            Item itemToBeDeleted = null;
            foreach (Item item in _items)
            {
                if (item.Id == itemId)
                {
                    itemToBeDeleted = item;
                    break;
                }
            }

            if (itemToBeDeleted != null)
            {
                _items.Remove(itemToBeDeleted);
                JsonFileItemService.SaveJsonObjects(_items);
            }

            return itemToBeDeleted;
        }

        public List<Item> GetItems() { return _items; }

        //------------------------------------------------------------------------------

        /// Step 10 (NameSearch)
        /// Refaktorer NameSearch så den benytter LINQ i stedet for FindAll() med et Lambda-expressions som prædicat - function.   


        public IEnumerable<Item> NameSearchLinq(string str)         //LINQ SEARCH
        {
            return from item in _items
                   orderby item.Name
                   select item;
        }

        public IEnumerable<Item> NameSearch(string str)         //LINQ(.Where) & LAMBDA     <--
        {
            return _items.Where(obj => string.IsNullOrEmpty(str) || obj.Name.ToLower().Contains(str.ToLower()));
        }

        public IEnumerable<Item> NameSearchLamda(string str)     //LAMBDA SEARCH
        {
            return _items.FindAll(obj => string.IsNullOrEmpty(str) || obj.Name.ToLower().Contains(str.ToLower()));
        }

        ////----: GAMMEL NAMESEARCH() :----
        //public IEnumerable<Item> NameSearch(string str)
        //{
        //    List<Item> nameSearch = new List<Item>();
        //    foreach (Item item in _items)
        //    {
        //        if (string.IsNullOrEmpty(str) || item.Name.ToLower().Contains(str.ToLower()))
        //        {
        //            nameSearch.Add(item);
        //        }
        //    }
        //    return nameSearch;
        //}

        //------------------------------------------------------------------------------


        /// Step 11 (PriceFilter)
        /// Refaktorer PriceFilter så den benytter LINQ i stedet for FindAll() med et Lambda-expressions som prædicat - function.

        public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)    //LINQ (_items.where) && LAMDA (item => )
        {
            return _items.Where(item => (minPrice == 0 || item.Price >= minPrice) && (maxPrice == 0 || item.Price <= maxPrice));
        }

        public IEnumerable<Item> PriceFilterLAMBDA(int maxPrice, int minPrice = 0)
        {
            return _items.FindAll(obj => (minPrice == 0 && obj.Price <= maxPrice) || (maxPrice == 0 && obj.Price >= minPrice) || (obj.Price >= minPrice && obj.Price <= maxPrice));
        }

        ////----: GAMMEL PRICEFILTER :----
        //public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        //{
        //    List<Item> filterList = new List<Item>();
        //    foreach (Item item in _items)
        //    {
        //        if ((minPrice == 0 && item.Price <= maxPrice) || (maxPrice == 0 && item.Price >= minPrice) || (item.Price >= minPrice && item.Price <= maxPrice))
        //        {
        //            filterList.Add(item);
        //        }
        //    }

        //    return filterList;
        //}


        //--------------------------------------------------------------
        ///https://linqsamples.com/linq-to-objects/ordering/OrderByDescending-linq 

        public IEnumerable<Item> SortById()     //LINQ & LAMBDA
        {
            return _items.OrderBy(x => x.Id);   // bemærk: default er ascending(stigende.. fra 0 til 10)
        }

        ////----: GAMMEL SORTBYID() :----
        //public IEnumerable<Item> SortById()
        //{
        //    _items.Sort();
        //    return _items;
        //}

        public IEnumerable<Item> SortByIdDescending()   //LINQ & LAMBDA 
        {
            return _items.OrderByDescending(x => x.Id);
        }


        ////----: GAMMEL SORTBYID_DESCENDING() :----    
        //public IEnumerable<Item> SortByIdDescending()
        //{
        //    _items.Sort();
        //    return _items.Reverse<Item>();
        //}

        //---------------------------------------------------------------------


        ///Step 5 (SortByName)
        ///Lav en tilsvarende refaktorering af SortByName, så der benyttes LINQ i stedet for Comperatorer.*/

        public IEnumerable<Item> SortByName()   //LINQ (.OrderBy) & LAMBDA
        {
            return _items.OrderBy(obj => obj.Name);
        }

        public IEnumerable<Item> SortByNameLINQ() //LINQ (for SQL og C# folket)
        {
            return from item in _items
                   orderby item.Name
                   select item;
        }

        ////----: GAMMEL SORTBYNAME :---
        //public IEnumerable<Item> SortByName()
        //{
        //    _items.Sort(new NameComperator());
        //    return _items;
        //}


        public IEnumerable<Item> SortByNameDescending() //LAMBDA (for C# og Java folket)
        {
            return _items.OrderByDescending(obj => obj.Name);
        }

        public IEnumerable<Item> SortByNameDescending2() //LINQ (for SQL og C# folket)
        {
            return from item in _items
                   orderby item.Name descending
                   select item;
        }

        ////----: GAMMEL SORTBYNAME_DESCENDING() :----
        //public IEnumerable<Item> SortByNameDescending()
        //{
        //    _items.Sort(new NameComperator());
        //    return _items.Reverse<Item>();
        //}

        //------------------------------------------------------------------------
        /// Sammenligning af komparatorer og Sort() metoden:
        /// 
        /// Komparatorer og Sort() metoden er begge måder at sortere en samling på, men de adskiller sig fra hinanden på følgende måder:
        /// Komparatorer kræver, at du skriver en ekstra metode, som angiver, hvordan elementerne skal sammenlignes.
        /// Sort() metoden tager ikke en sådan metode som parameter, men bruger i stedet en standard sammenligningsmetode,
        /// der er defineret for typen af elementer i samlingen. Ved at bruge en komparator kan du definere en mere specifik måde
        /// at sortere på end den standardmetode, som Sort() metoden bruger.Dette kan være nyttigt, hvis du vil sortere efter et bestemt kriterium,
        /// som ikke er omfattet af standardmetoden. SORT() ÆNDRER DEN OPRINDELIGE SAMLING, mens en sammenligningsmetode ikke gør det.
        /// Dette betyder, at hvis du vil gemme den oprindelige rækkefølge af elementerne, skal du først kopiere samlingen og derefter sortere kopien.
        /// 
        /// Sammenligning af Sort() metoden og LINQ:
        /// 
        /// LINQ (Language Integrated Query) er en mere generel metode til at arbejde med samlinger.LINQ tillader dig at udføre forskellige operationer
        /// på en samling, såsom at filtrere, gruppere og sortere elementerne. Når du bruger Sort() metoden, vil den oprindelige samling blive ændret,
        /// mens LINQ ikke ændrer den oprindelige samling.I stedet oprettes en ny samling, som indeholder resultaterne af sorteringen.
        /// Dette betyder, at hvis du vil gemme den oprindelige rækkefølge af elementerne, skal du først kopiere samlingen og derefter sortere kopien.
        /// En anden forskel mellem Sort() metoden og LINQ er, at Sort() metoden kun kan sortere en samling efter en enkelt faktor.

        public IEnumerable<Item> SortByPrice()  //LINQ (for SQL og C# folket)
        {
            return from item in _items
                   orderby item.Price
                   select item;
        }

        ////----: GAMMEL :----
        //public IEnumerable<Item> SortByPrice()
        //{
        //    _items.Sort(new PriceComperator());
        //    return _items;
        //}

        public IEnumerable<Item> SortByPriceDescending()
        {
            _items.Sort(new PriceComperator());
            return _items.Reverse<Item>();
        }
    }
}

using ItemRazorV1.Models;
using System.Text.Json;

namespace ItemRazorV1.Service
{
    public class JsonFileService<T>    // <-- Renamed!
    {

        /// <summary>
        /// Step 1 (JsonFileService)
        /// Omdøb(rename) JsonFileItemService til JsonFileService.
        /// Omdøb metoderne i klassen så de ikke indeholder termen "Item" (benyt "objects" istedet)
        /// Gør klassen JsonFileService generisk.
        /// Hint: benyt typeof(T).Name + "s.json" som filnavn.
        /// 
        /// Step 2 (Program.cs)
        /// </summary>

        public IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", typeof(T).Name + "s.json"); }
            //get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Items.json"); }
        }

        public void SaveJsonObjects(List<T> objects)   // Renamed! List<Item> til List<T> Items til objects
        {
            using (FileStream jsonFileWriter = File.Create(JsonFileName))
            {
                Utf8JsonWriter jsonWriter = new Utf8JsonWriter(jsonFileWriter, new JsonWriterOptions()
                {
                    SkipValidation = false,
                    Indented = true
                });
                JsonSerializer.Serialize<T[]>(jsonWriter, objects.ToArray());
            }
        }

        public IEnumerable<T> GetJsonObjects()   // Renamed!
        {
            using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd());    // Renamed!
            }
        }
    }
}

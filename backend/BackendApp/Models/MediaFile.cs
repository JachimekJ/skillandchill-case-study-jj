namespace BackendApp.Models
{
    public class MediaFile
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;   // np. SKU123_main_1.jpg
        public string Type { get; set; } = string.Empty;   // "image", "pdf"
        public int Size { get; set; }                      // w KB
        public DateTime Date { get; set; }                 // data dodania pliku
        public string Url { get; set; } = string.Empty;    // link do pobrania
    }
}

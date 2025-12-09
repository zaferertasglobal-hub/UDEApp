namespace UDEApp.Shared.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Specialization { get; set; } = "Both";   // Software / Communications / Both
        public string Semester { get; set; } = "";             // WS veya SS
        public int ECTS { get; set; }

        // Kısa açıklama (tabloda gözükecek)
        public string ShortDescription { get; set; } = "";

        // Detaylı, öğretici, örnekli içerik (modalda gösterilecek)
        public string FullContent { get; set; } = "";
    }
}
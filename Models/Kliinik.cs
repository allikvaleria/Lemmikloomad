namespace Lemmikloomad.Models
{
    public class Kliinik
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Address { get; set; }
        public List<Lemmikloom> Lemmikloomad { get; set; }
    }
}

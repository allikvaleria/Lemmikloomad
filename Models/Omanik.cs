namespace Lemmikloomad.Models
{
    public class Omanik
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Sugu { get; set; }
        public List<Lemmikloom> Lemmikloomad { get; set; }
    }
}

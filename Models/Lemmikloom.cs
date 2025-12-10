namespace Lemmikloomad.Models
{
    public class Lemmikloom
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public double Kaal { get; set; }

        public int OmanikId { get; set; }
        public Omanik? Omanik { get; set; } 

        public int KliinikId { get; set; }
        public Kliinik? Kliinik { get; set; } 
    }
}

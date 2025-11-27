using Microsoft.AspNetCore.Mvc;
using Lemmikloomad.Data;
using Lemmikloomad.Models;

namespace Lemmikloomad.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KliinikController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KliinikController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Kliinik> GetKliinikud()
        {
            return _context.Kliinikud.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Kliinik> GetKliinik(int id)
        {
            var kliinik = _context.Kliinikud.Find(id);
            if (kliinik == null)
                return NotFound();

            return kliinik;
        }

        [HttpPost]
        public List<Kliinik> PostKliinik([FromBody] Kliinik kliinik)
        {
            _context.Kliinikud.Add(kliinik);
            _context.SaveChanges();
            return _context.Kliinikud.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Kliinik>> PutKliinik(int id, [FromBody] Kliinik updated)
        {
            var kliinik = _context.Kliinikud.Find(id);
            if (kliinik == null)
                return NotFound();

            kliinik.Nimi = updated.Nimi;
            kliinik.Address = updated.Address;

            _context.Kliinikud.Update(kliinik);
            _context.SaveChanges();

            return Ok(_context.Kliinikud.ToList());
        }

        [HttpDelete("{id}")]
        public List<Kliinik> DeleteKliinik(int id)
        {
            var kliinik = _context.Kliinikud.Find(id);
            if (kliinik == null)
                return _context.Kliinikud.ToList();

            _context.Kliinikud.Remove(kliinik);
            _context.SaveChanges();

            return _context.Kliinikud.ToList();
        }
    }
}

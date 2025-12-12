using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Kliinikud.Include(k => k.Lemmikloomad).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Kliinik> GetKliinik(int id)
        {
            var clinic = _context.Kliinikud
                .Include(k => k.Lemmikloomad)
                .FirstOrDefault(k => k.Id == id);

            if (clinic == null)
                return NotFound();

            return clinic;
        }


        [HttpPost]
        public List<Kliinik> PostKliinik([FromBody] Kliinik kliinik)
        {
            _context.Kliinikud.Add(kliinik);
            _context.SaveChanges();
            return _context.Kliinikud.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Kliinik>> PutKliinik(int id, [FromBody] Kliinik updatedClinic)
        {
            var clinic = _context.Kliinikud
                .Include(k => k.Lemmikloomad)
                .FirstOrDefault(k => k.Id == id);
            if (clinic == null)
                return NotFound();

            clinic.Nimi = updatedClinic.Nimi;
            clinic.Address = updatedClinic.Address;

            _context.Kliinikud.Update(clinic);
            _context.SaveChanges();

            return Ok(_context.Kliinikud.ToList());
        }

        [HttpDelete("{id}")]
        public List<Kliinik> DeleteKliinik(int id)
        {
            var clinic = _context.Kliinikud.Find(id);
            if (clinic != null)
            {
                _context.Kliinikud.Remove(clinic);
                _context.SaveChanges();
            }
            return _context.Kliinikud.ToList();
        }

        [HttpGet("search")]
        public ActionResult<List<Lemmikloom>> SearchPet(string name)
        {
            var pets = _context.Lemmikloomad
                .Where(p => p.Nimi.ToLower().Contains(name.ToLower()))
                .ToList();
            return Ok(pets);
        }

        [HttpGet("top")]
        public ActionResult<Kliinik> GetTopClinic()
        {
            var clinic = _context.Kliinikud
                .Include(k => k.Lemmikloomad)
                .OrderByDescending(k => k.Lemmikloomad.Count)
                .FirstOrDefault();

            if (clinic == null)
                return NotFound();
            return Ok(clinic);
        }
    }
}

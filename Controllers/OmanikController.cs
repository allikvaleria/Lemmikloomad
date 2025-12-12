using Lemmikloomad.Data;
using Lemmikloomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lemmikloomad.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OmanikController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OmanikController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Omanik> GetOmanikud()
        {
            return _context.Omanikud
                .Include(o => o.Lemmikloomad)
                .ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<Omanik> GetOmanik(int id)
        {
            var omanik = _context.Omanikud
                .Include(o => o.Lemmikloomad)
                .FirstOrDefault(o => o.Id == id);

            if (omanik == null)
                return NotFound();

            return omanik;
        }


        [HttpPost]
        public List<Omanik> PostOmanik([FromBody] Omanik omanik)
        {
            _context.Omanikud.Add(omanik);
            _context.SaveChanges();
            return _context.Omanikud.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Omanik>> PutOmanik(int id, [FromBody] Omanik updatedOmanik)
        {
            var omanik = _context.Omanikud.Find(id);
            if (omanik == null)
                return NotFound();

            omanik.Nimi = updatedOmanik.Nimi;
            omanik.Perekonnanimi = updatedOmanik.Perekonnanimi;
            omanik.Sugu = updatedOmanik.Sugu;

            _context.Omanikud.Update(omanik);
            _context.SaveChanges();

            return Ok(_context.Omanikud.ToList());
        }

        [HttpDelete("{id}")]
        public List<Omanik> DeleteOmanik(int id)
        {
            var omanik = _context.Omanikud.Find(id);
            if (omanik != null)
            {
                _context.Omanikud.Remove(omanik);
                _context.SaveChanges();
            }
            return _context.Omanikud.ToList();
        }

        [HttpGet("{id}/count")]
        public ActionResult<int> GetPetsCount(int id)
        {
            int count = _context.Lemmikloomad.Count(p => p.OmanikId == id);
            return Ok(count);
        }

        [HttpGet("{id}/max-weight")]
        public ActionResult<Lemmikloom> GetHeaviestPet(int id)
        {
            var pet = _context.Lemmikloomad
                .Where(p => p.OmanikId == id)
                .OrderByDescending(p => p.Kaal)
                .FirstOrDefault();

            if (pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpGet("{id}/min-weight")]
        public ActionResult<Lemmikloom> GetLightestPet(int id)
        {
            var pet = _context.Lemmikloomad
                .Where(p => p.OmanikId == id)
                .OrderBy(p => p.Kaal)
                .FirstOrDefault();

            if (pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpPost("{id}/add-pet")]
        public ActionResult<List<Lemmikloom>> AddPetToOwner(int id, [FromBody] Lemmikloom pet)
        {
            var omanik = _context.Omanikud.Find(id);
            if (omanik == null)
                return NotFound();

            pet.OmanikId = id;
            _context.Lemmikloomad.Add(pet);
            _context.SaveChanges();

            return Ok(_context.Lemmikloomad.Where(p => p.OmanikId == id).ToList());
        }
    }
}

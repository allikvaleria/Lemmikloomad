using Lemmikloomad.Data;
using Lemmikloomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lemmikloomad.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LemmikloomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LemmikloomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Lemmikloom> GetLemmikloomad()
        {
            return _context.Lemmikloomad
                .Include(p => p.Omanik)
                .Include(p => p.Kliinik)
                .ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<Lemmikloom> GetLemmikloom(int id)
        {
            var pet = _context.Lemmikloomad
                .Include(p => p.Omanik)
                .Include(p => p.Kliinik)
                .FirstOrDefault(p => p.Id == id);

            if (pet == null)
                return NotFound();

            return pet;
        }


        [HttpPost]
        public ActionResult<Lemmikloom> PostLemmikloom([FromBody] Lemmikloom pet)
        {
            if (pet == null)
                return BadRequest("Pet data is required.");

            _context.Lemmikloomad.Add(pet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLemmikloom), new { id = pet.Id }, pet);
        }


        [HttpPut("{id}")]
        public ActionResult<List<Lemmikloom>> PutLemmikloom(int id, [FromBody] Lemmikloom updatedPet)
        {
            var pet = _context.Lemmikloomad.Find(id);
            if (pet == null)
                return NotFound();

            pet.Nimi = updatedPet.Nimi;
            pet.Kaal = updatedPet.Kaal;
            pet.OmanikId = updatedPet.OmanikId;
            pet.KliinikId = updatedPet.KliinikId;

            _context.Lemmikloomad.Update(pet);
            _context.SaveChanges();

            return Ok(_context.Lemmikloomad.ToList());
        }

        [HttpDelete("{id}")]
        public List<Lemmikloom> DeleteLemmikloom(int id)
        {
            var pet = _context.Lemmikloomad.Find(id);
            if (pet != null)
            {
                _context.Lemmikloomad.Remove(pet);
                _context.SaveChanges();
            }
            return _context.Lemmikloomad.ToList();
        }

        [HttpGet("weight-range")]
        public ActionResult<List<Lemmikloom>> GetPetsByWeightRange(double min, double max)
        {
            var pets = _context.Lemmikloomad
                .Where(p => p.Kaal >= min && p.Kaal <= max)
                .ToList();
            return Ok(pets);
        }
    }
}

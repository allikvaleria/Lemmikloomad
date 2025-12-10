using Microsoft.AspNetCore.Mvc;
using Lemmikloomad.Data;
using Lemmikloomad.Models;

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
            return _context.Lemmikloomad.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Lemmikloom> GetLemmikloom(int id)
        {
            var pet = _context.Lemmikloomad.Find(id);
            if (pet == null)
                return NotFound();
            return pet;
        }

        [HttpPost]
        public List<Lemmikloom> PostLemmikloom([FromBody] Lemmikloom pet)
        {
            _context.Lemmikloomad.Add(pet);
            _context.SaveChanges();
            return _context.Lemmikloomad.ToList();
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

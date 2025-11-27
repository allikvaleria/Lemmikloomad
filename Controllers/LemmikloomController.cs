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
            var loom = _context.Lemmikloomad.Find(id);
            if (loom == null)
                return NotFound();

            return loom;
        }

        [HttpPost]
        public List<Lemmikloom> PostLemmikloom([FromBody] Lemmikloom loom)
        {
            _context.Lemmikloomad.Add(loom);
            _context.SaveChanges();
            return _context.Lemmikloomad.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Lemmikloom>> PutLemmikloom(int id, [FromBody] Lemmikloom updated)
        {
            var loom = _context.Lemmikloomad.Find(id);
            if (loom == null)
                return NotFound();

            loom.Nimi = updated.Nimi;
            loom.Kaal = updated.Kaal;
            loom.OmanikId = updated.OmanikId;
            loom.KliinikId = updated.KliinikId;

            _context.Lemmikloomad.Update(loom);
            _context.SaveChanges();

            return Ok(_context.Lemmikloomad.ToList());
        }

        [HttpDelete("{id}")]
        public List<Lemmikloom> DeleteLemmikloom(int id)
        {
            var loom = _context.Lemmikloomad.Find(id);
            if (loom == null)
                return _context.Lemmikloomad.ToList();

            _context.Lemmikloomad.Remove(loom);
            _context.SaveChanges();

            return _context.Lemmikloomad.ToList();
        }
    }
}

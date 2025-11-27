using Microsoft.AspNetCore.Mvc;
using Lemmikloomad.Data;
using Lemmikloomad.Models;

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
            return _context.Omanikud.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Omanik> GetOmanik(int id)
        {
            var omanik = _context.Omanikud.Find(id);
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
        public ActionResult<List<Omanik>> PutOmanik(int id, [FromBody] Omanik updated)
        {
            var omanik = _context.Omanikud.Find(id);
            if (omanik == null)
                return NotFound();

            omanik.Nimi = updated.Nimi;
            omanik.Perekonnanimi = updated.Perekonnanimi;
            omanik.Sugu = updated.Sugu;

            _context.Omanikud.Update(omanik);
            _context.SaveChanges();

            return Ok(_context.Omanikud.ToList());
        }

        [HttpDelete("{id}")]
        public List<Omanik> DeleteOmanik(int id)
        {
            var omanik = _context.Omanikud.Find(id);
            if (omanik == null)
                return _context.Omanikud.ToList();

            _context.Omanikud.Remove(omanik);
            _context.SaveChanges();

            return _context.Omanikud.ToList();
        }
    }
}

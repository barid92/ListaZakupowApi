using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ListaZakupowApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class ListaZakupowController : Controller
    {
        private readonly ListaZakupowContext _context;

        public ListaZakupowController(ListaZakupowContext context)
        {
            _context = context;

            if (_context.Zakupy.Count() == 0)
            {
                _context.Zakupy.Add(new Zakup { Name = "Margaryna" });
                _context.SaveChanges();
            }
        }  
        [HttpGet]
        public IEnumerable<Zakup> GetAll()
        {
            return _context.Zakupy.ToList();
        }

        [HttpGet("{id}", Name = "GetZakup")]
        public IActionResult GetById(long id)
        {
            var item = _context.Zakupy.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }  
        [HttpPost]
        public IActionResult Create([FromBody] Zakup item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Zakupy.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetZakup", new { id = item.Id }, item);
        }   
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Zakup item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var zakup = _context.Zakupy.FirstOrDefault(t => t.Id == id);
            if (zakup == null)
            {
                return NotFound();
            }

            zakup.Name = item.Name;
            zakup.Count = item.Count;
            _context.Zakupy.Update(zakup);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var zakup = _context.Zakupy.FirstOrDefault(t => t.Id == id);
            if (zakup == null)
            {
                return NotFound();
            }

            _context.Zakupy.Remove(zakup);
            _context.SaveChanges();
            return new NoContentResult();
        }
            }
}
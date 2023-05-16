using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using SeniorTest.WeatherAPI;

namespace SeniorTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TemperaturesController : Controller
    {
        private readonly AssessmentsContext _context;

        public TemperaturesController(AssessmentsContext context)
        {
            _context = context;
        }

        // GET: Temperatures
        [HttpGet(Name = "Index")]
        public async Task<IActionResult> Index()
        {
            var weathers = await _context.Temperatures.ToListAsync();
            return Ok(weathers);
        }

        // POST: Temperatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(int id, string desc)
        {
            Temperature tmp = new Temperature();
            tmp.Id = id;
            tmp.TemperatureDescr = desc;
            
            _context.Add(tmp);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: Temperatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TemperatureDescr")] Temperature temperature)
        {
            if (id != temperature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temperature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemperatureExists(temperature.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(temperature);
        }


        // POST: Temperatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Temperatures == null)
            {
                return Problem("Entity set 'AssessmentsContext.Temperatures'  is null.");
            }
            var temperature = await _context.Temperatures.FindAsync(id);
            if (temperature != null)
            {
                _context.Temperatures.Remove(temperature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemperatureExists(int id)
        {
          return (_context.Temperatures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

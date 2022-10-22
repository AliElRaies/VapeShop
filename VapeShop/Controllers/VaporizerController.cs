using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VapeShop.Data;
using VapeShop.Models;

namespace VapeShop.Controllers
{
    public class VaporizerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaporizerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VapeItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.VapeItem.ToListAsync());
        }

        // GET: Search for Vapes
        public IActionResult ShowSearch()
        {
            return View();
        }
        // Post: returning Search for Vapes
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.VapeItem.Where(j => j.VapeImg.Contains(SearchPhrase)).ToListAsync());
        }
        // GET: VapeItems/Details/5 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vapeItem = await _context.VapeItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vapeItem == null)
            {
                return NotFound();
            }

            return View(vapeItem);
        }

        // GET: VapeItems/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VapeImg,Maker,Model")] VapeItem vapeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vapeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vapeItem);
        }

        // GET: VapeItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vapeItem = await _context.VapeItem.FindAsync(id);
            if (vapeItem == null)
            {
                return NotFound();
            }
            return View(vapeItem);
        }

        // POST: VapeItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VapeImg,Maker,Model")] VapeItem vapeItem)
        {
            if (id != vapeItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vapeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VapeItemExists(vapeItem.Id))
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
            return View(vapeItem);
        }

        // GET: VapeItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vapeItem = await _context.VapeItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vapeItem == null)
            {
                return NotFound();
            }

            return View(vapeItem);
        }

        // POST: VapeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vapeItem = await _context.VapeItem.FindAsync(id);
            _context.VapeItem.Remove(vapeItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VapeItemExists(int id)
        {
            return _context.VapeItem.Any(e => e.Id == id);
        }
    }
}

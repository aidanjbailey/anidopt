﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly IAnimalService _animalService;

        public AnimalsController(AnidoptContext context, IAnimalService animalService)
        {
            _context = context;
            _animalService = animalService;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            return _context.Animal != null ?
                        View(await _context.Animal.ToListAsync()) :
                        Problem("Entity set 'AnidoptContext.Animal'  is null.");
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animal == null) return NotFound();
            var animal = await _animalService.GetAnimalById((int)id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            var animalType = await _animalService.GetAnimalTypes();
            ViewBag.AnimalTypes = animalType.Select(at => new SelectListItem {
                Text = at.Name,
                Value = at.Id.ToString()
            });
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,AnimalTypeId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                var animalType = await _animalService.GetAnimalTypes();

                ViewBag.AnimalTypes = animalType.Select(at => new SelectListItem
                {
                    Text = at.Name,
                    Value = at.Id.ToString(),
                    Selected = animal.AnimalTypeId == at.Id 
                });
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animal == null) return NotFound();

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null) return NotFound();

            var animalType = await _animalService.GetAnimalTypes();

            ViewBag.AnimalTypes = animalType.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString()
            });

            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,AnimalTypeId")] Animal animal)
        {
            if (id != animal.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _animalService.AnimalExistsById(animal.Id))) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var animalType = await _animalService.GetAnimalTypes();
            ViewBag.AnimalTypes = animalType.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString(),
                Selected = animal.AnimalTypeId == at.Id
            });

            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'AnidoptContext.Animal'  is null.");
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

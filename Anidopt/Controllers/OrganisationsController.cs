﻿using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers
{
    public class OrganisationsController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly IOrganisationService _organisationService;

        public OrganisationsController(AnidoptContext context, IOrganisationService organisationService)
        {
            _context = context;
            _organisationService = organisationService;
        }

        // GET: Organisations
        public async Task<IActionResult> Index()
        {
            if (!_organisationService.Initialised) return Problem("Entity set 'AnidoptContext.Organisation'  is null.");
            var organisations = await _organisationService.GetAllAsync();
            return View(organisations);
        }

        // GET: Organisations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_organisationService.Initialised) return NotFound();
            var organisation = await _organisationService.GetByIdAsync((int)id);
            if (organisation == null) return NotFound();
            return View(organisation);
        }

        // GET: Organisations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organisations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Animals")] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organisation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organisation);
        }

        // GET: Organisations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_organisationService.Initialised) return NotFound();
            var organisation = await _organisationService.GetByIdAsync((int)id);
            if (organisation == null) return NotFound();
            return View(organisation);
        }

        // POST: Organisations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Organisation organisation)
        {
            if (id != organisation.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organisation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_organisationService.ExistsById(organisation.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(organisation);
        }

        // GET: Organisations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_organisationService.Initialised) return NotFound();
            var organisation = await _organisationService.GetByIdAsync((int)id);
            if (organisation == null) return NotFound();
            return View(organisation);
        }

        // POST: Organisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organisation == null) return Problem("Entity set 'AnidoptContext.Organisation'  is null.");
            await _organisationService.EnsureDeletionById((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}

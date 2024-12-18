﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;
using VacationPlannerPro.Business.Interfaces;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class WorkersController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int page = 1, int pageSize = 5)
        {
            var pagedResult = await _workerService.GetWorkersAsync(page, pageSize, searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(pagedResult);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            return View(worker);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkerDTO workerDto)
        {
            if (!ModelState.IsValid) return View(workerDto);

            await _workerService.CreateAsync(workerDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            var updateDto = new UpdateWorkerDTO
            {
                Id = worker.Id,
                FullName = worker.FullName,
                Age = worker.Age,
                ProfessionId = worker.ProfessionId
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateWorkerDTO workerDto)
        {
            if (!ModelState.IsValid) return View(workerDto);

            await _workerService.UpdateAsync(workerDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var worker = await _workerService.GetByIdAsync(id);
            if (worker == null) return NotFound();

            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var workers = await _workerService.GetAllAsync();
            var result = workers.Select(w => new
            {
                id = w.Id,
                name = w.FullName
            });

            return Json(result);
        }
    }
}

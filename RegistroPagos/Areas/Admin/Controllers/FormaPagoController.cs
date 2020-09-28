using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroPagos.Data;
using RegistroPagos.Models;

namespace RegistroPagos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormaPagoController : Controller
    {
        //---Dependency Injection en BD
        private readonly ApplicationDbContext _db;

        public FormaPagoController(ApplicationDbContext db)
        {
            _db = db;
        }


        //---GET - Vista principal
        public async Task<IActionResult> Index()
        {
            return View(await _db.FormaPago.ToListAsync());
        }

        //---GET - Vista crear
        public IActionResult Create()
        {
            return View();
        }

        //---POST - Registrar forma de pago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormaPago formaPago)
        {
            //---Validación de datos
            if (ModelState.IsValid)
            {
                //---Registrar
                _db.FormaPago.Add(formaPago);
                await _db.SaveChangesAsync();

                //---Regresar a vista
                return RedirectToAction(nameof(Index));
            }
            return View(formaPago);
        }

        //---Get - Detalles de Forma de Pago
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            //---obtener info de registro
            var formaPago = await _db.FormaPago.FindAsync(id);
            if(formaPago == null)
            {
                return NotFound();
            }
            return View(formaPago);

        }

        //---POST - Editar (Vista)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var formaPago = await _db.FormaPago.FindAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }
            return View(formaPago);
        }

        //--- Post - Editar (Acción)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FormaPago formaPago)
        {
            if (ModelState.IsValid)
            {
                _db.Update(formaPago);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(formaPago);
        }

        //---Get - Eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var formaPago = await _db.FormaPago.FindAsync(id);
            if(formaPago == null)
            {
                return NotFound();
            }
            return View(formaPago);
        }

        //---Post - Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var formaPago = await _db.FormaPago.FindAsync(id);
            if(formaPago == null)
            {
                return NotFound();
            }

            _db.FormaPago.Remove(formaPago);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}

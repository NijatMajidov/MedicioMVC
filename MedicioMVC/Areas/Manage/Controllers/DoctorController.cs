using MedicioMVC.Business.Exceptions;
using MedicioMVC.Business.Services.Abstract;
using MedicioMVC.Business.Services.Concretes;
using MedicioMVC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MedicioMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            return View(_doctorService.GetAllDoctors());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                _doctorService.CreateDoctor(doctor);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var exsistDoctor = _doctorService.GetDoctor(x=>x.Id == id);
            if(exsistDoctor == null) return NotFound();
            return View(exsistDoctor);
        }
        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            var exsistDoctor = _doctorService.GetDoctor(x => x.Id == id);

            if (exsistDoctor == null) return NotFound();
            try
            {
                _doctorService.DeleteDoctor(id);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ImageFileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var exsistDoctor = _doctorService.GetDoctor(x => x.Id == id);
            if (exsistDoctor == null) return NotFound();
            return View(exsistDoctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor newDoctor)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                _doctorService.UpdateDoctor(newDoctor.Id, newDoctor);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return RedirectToAction("Index");
        }
        
    }
}

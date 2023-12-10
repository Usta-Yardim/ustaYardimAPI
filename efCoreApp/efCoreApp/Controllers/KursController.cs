using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers
{
    public class KursController:Controller
    { 
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Index(){
            var Kurslar = await _context.Kurslar.ToListAsync();
            return View(Kurslar);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(kurs == null){
                return NotFound();
            }

            return View(kurs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Bu formu senin yerine başkasıda gönderebilir onun için token kontrolü yapıyor
        public async Task<IActionResult> Edit(int id,Kurs model)
        {
            if(id != model.KursId) //root ile actiondan gelen id aynı mı
            {
                return NotFound();
            }

            if(ModelState.IsValid) // eğer modelin validi ile eşleşiyorsa
            {
                try
                {
                    _context.Update(model); // işaretledi
                    await _context.SaveChangesAsync(); // kaydetti
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kurslar.Any(ogr => ogr.KursId == model.KursId)) //eğer db de yoksa böyle biri
                    {
                        return NotFound();
                    }
                    else{
                        throw;
                    }  
                }
                return RedirectToAction("Index"); // eğer her şey doğruysa anasayfaya git
            }
            return View(model); // model ile eşleşmiyorsa tekrar aynı sayfayı gönder
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(kurs == null){
                return NotFound();
            }

            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) // FromRoute FromForm hangisinden almak istiyorsun cshtmlden
        {

            var kurs = await _context.Kurslar.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(kurs == null){
                return NotFound();
            }

            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
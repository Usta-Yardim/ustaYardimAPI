using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        
        public OgrenciController(DataContext context) // injection methodu deniyor buna üstteki context ile
        {
            _context = context;
        }
        public async Task<IActionResult> Index(){
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
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
            var ogrenci = await _context.Ogrenciler.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(ogrenci == null){
                return NotFound();
            }

            return View(ogrenci);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Bu formu senin yerine başkasıda gönderebilir onun için token kontrolü yapıyor
        public async Task<IActionResult> Edit(int id,Ogrenci model)
        {
            if(id != model.OgrenciId) //root ile actiondan gelen id aynı mı
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
                    if (!_context.Ogrenciler.Any(ogr => ogr.OgrenciId == model.OgrenciId)) //eğer db de yoksa böyle biri
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
            var ogrenci = await _context.Ogrenciler.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(ogrenci == null){
                return NotFound();
            }

            return View(ogrenci);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) // FromRoute FromForm hangisinden almak istiyorsun cshtmlden
        {

            var ogrenci = await _context.Ogrenciler.FindAsync(id); // sadece idye göre arama yapar alttaki istedğine göre
            //var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci => ogrenci.OgrenciId == id); // Eposta felanda olabilir

            if(ogrenci == null){
                return NotFound();
            }

            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HomeController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    // GET: Home
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(FileUploadViewModel model)
    {
        if (ModelState.IsValid)
        {
            var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            // Generate a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;

            // Combine the directory and file name
            var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            return View("_UploadSuccess");;
        }
        
        return View("_UploadSuccess");
    }
    public IActionResult _UploadSuccess()
    {
        return View();
    }
}
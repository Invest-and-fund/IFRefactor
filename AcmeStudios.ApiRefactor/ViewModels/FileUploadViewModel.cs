using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AcmeStudios.ApiRefactor.ViewModels;

public class FileUploadViewModel
{
    [Required(ErrorMessage = "Please select a file.")]
    [Display(Name = "File")]
    public IFormFile File { get; set; }
}
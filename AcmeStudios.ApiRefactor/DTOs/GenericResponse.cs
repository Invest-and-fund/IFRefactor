using System.Collections.Generic;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorName { get; set; }
        public List<string> Errors { get; set; }
        public string Redirect { get; set; }
    }
}

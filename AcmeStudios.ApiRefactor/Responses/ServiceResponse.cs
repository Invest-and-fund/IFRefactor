namespace AcmeStudios.ApiRefactor.Responses
{
    public class ServiceResponse<T> where T : class
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }
}

namespace AcmeStudios.ApiRefactor.Response;

public class ServiceResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public ServiceResponse<T> HandleError<T>(string errorMessage)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = errorMessage
        };
    }
}


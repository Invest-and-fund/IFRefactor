namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class ServiceResponse<T>
    {
        public T? Data { get; private init; }
        public bool Success { get; private init; }
        public string Message { get; private init; } = string.Empty;

        public static ServiceResponse<T> Succeeded(T data, string message)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }

        public static ServiceResponse<T> Failed(string message)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}

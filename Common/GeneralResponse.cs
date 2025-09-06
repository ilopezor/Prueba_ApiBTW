namespace Common
{
    public class GeneralResponse<T>
    {
        public T? ObjectResponse { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
        public bool OperationSuccess { get; set; }
    }
}
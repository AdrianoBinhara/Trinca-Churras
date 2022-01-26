using System;
namespace TrincaChurras.Models
{
    public sealed class OperationDataResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}
public class OperationResult
{
    public bool Sucess { get; set; }
    public string Message { get; set; }
}

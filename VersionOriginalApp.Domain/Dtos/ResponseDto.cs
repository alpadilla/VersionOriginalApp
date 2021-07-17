using System.Collections.Generic;

namespace VersionOriginalApp.Domain.Dtos
{
    public class ResponseDto<T> where T : class
    {
        public T Data { get; set; }

        public Result Result { get; protected set; }

        public bool IsSuccess()
        {
            return Result.Success;
        }

        public void AddError(string error)
        {
            Result.AddError(error);
        }

        public ResponseDto(T data = null)
        {
            Data = data;

            Result = new Result();
        }
    }

    public class Result
    {
        private readonly List<string> _errors = new List<string>();

        public bool Success { get; set; }

        public IEnumerable<string> Errors => _errors;

        internal void AddError(string error)
        {
            Success = false;
            _errors.Add(error);
        }

        public Result(bool status = true)
        {
            Success = status;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get;}
        public string? Error { get; }

        private Result(bool isSucess, T? value, string? error) { 
        
            IsSuccess = isSucess;
            Value = value;
            Error = error;
        }

        // Métodos de fábrica para crear resultados
        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
    }
}

﻿using System.Collections.Generic;

namespace ShopAggregator.Web.Models
{
    public class OperationResult
    {
        public OperationResult()
        {
            Errors = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {
            Success = true;
        }

        public T Result { get; set; }
    }
}
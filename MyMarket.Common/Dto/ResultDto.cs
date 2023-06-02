using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Common.Dto
{

    public class ResultDto
    {
        public String Message { get; set; }

        public bool IsSucsess { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ResultDto<T>
    {
        public String Message { get; set; }

        public bool IsSuccess { get; set; }

        public T Data { get; set; }
    }
}

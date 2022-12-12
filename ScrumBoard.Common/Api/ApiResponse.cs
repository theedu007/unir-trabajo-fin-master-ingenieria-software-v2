﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Common.Api
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessful { get; set; }
    }
}

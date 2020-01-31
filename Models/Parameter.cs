﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCRUD.Models
{
    public class Parameter
    {
        public int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}

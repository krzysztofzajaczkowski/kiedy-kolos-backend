﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Responses
{
    public class GetGroupResponse
    {
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public string GroupName { get; set; }
    }
}

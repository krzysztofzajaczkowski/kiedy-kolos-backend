﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Requests
{
    public class CreateGroupRequest
    {
        public int GroupNumber { get; set; }
        public string GroupName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Interfaces
{
    public  interface IYaDiskService
    {
        Task<string> PostImage (string pathOnSystem);
        Task<string> GetUrl (string pathOnYaDisk);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer.Server.Common
{
    public class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if(value == null)
            {
                name ??= "value";

                throw new ArgumentException($"{name} cannot be null.");
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Dtos
{
    public class RegistrarDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Imagen { get; set; }
        public string Password { get; set; }


    }
}

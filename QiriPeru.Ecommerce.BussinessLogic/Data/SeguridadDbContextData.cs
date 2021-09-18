using Microsoft.AspNetCore.Identity;
using QiriPeru.Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Data
{
    public class SeguridadDbContextData
    {
        public static async Task SeedUserAsync(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nombre = "Walther",
                    Apellido = "Vergaray",
                    UserName = "worer",
                    Direccion = new Direccion
                    {
                        Calle = "Maria Parado de Bellido Mz.O Lt.8",
                        Ciudad = "Lima",
                        CodigoPostal = "5123",
                        Departamento = "Lima"
                    }
                };

                await userManager.CreateAsync(usuario, "Su1c1d4s1!");
            }
        }
    }
}

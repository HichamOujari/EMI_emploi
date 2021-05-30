using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using emi_emploi.Models;

namespace emi_emploi.Data
{
    public static class DbInitialiser
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
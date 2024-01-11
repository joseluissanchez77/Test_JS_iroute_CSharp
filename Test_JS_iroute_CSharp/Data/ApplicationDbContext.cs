using Microsoft.EntityFrameworkCore;
using Test_JS_iroute_CSharp.Models;

namespace Test_JS_iroute_CSharp.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Clients> Clients { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Clients>().HasData(
        //        new Clients()
        //        {
        //            id = 10,
        //            primerNombre = "Laura",
        //            segundoNombre = "Karla",
        //            apellidos = "Villacis",
        //            identificacion = "09311147269",
        //            correo = "laura@gmail.com",
        //            estado = 1
        //        },
        //        new Clients()
        //        {
        //            id = 11,
        //            primerNombre = "Alejandra",
        //            segundoNombre = "Maria",
        //            apellidos = "Orosco",
        //            identificacion = "09311147321",
        //            correo = "alejandra@gmail.com",
        //            estado = 1
        //        }
        //    );
        //}

    }
}

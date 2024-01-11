using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Test_JS_iroute_CSharp.Data;
using Test_JS_iroute_CSharp.Models;
using Test_JS_iroute_CSharp.Models.Dto;

namespace Test_JS_iroute_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ILogger<ClientController> _logger;


        public ClientController(ILogger<ClientController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            _logger.LogInformation("Obtener las clientes");
            //return Ok(await _dbcontext.Clients.ToListAsync());

            string Identificacion = HttpContext.Request.Query["identificacion"].ToString();


            var identificacion = new SqlParameter("@identificacion", Identificacion);
            

            var clients = await _dbcontext.Clients.FromSqlRaw("sp_con_clientes @identificacion", identificacion).ToListAsync();


            if (clients.Count == 0 && Identificacion!=null)
            {
                // No se encontraron clientes, devolver un JSON personalizado y un código de estado 404 (NotFound)
                return NotFound(new { procesoExitoso = false, mensaje = "No existe cliente con dicha identificacion" });
            }
            return Ok(clients);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Clients>> CrearCliente([FromBody] ClientsDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (clientDto == null)
            {
                return BadRequest(clientDto);
            }

            var opcion = new SqlParameter("@opcion", "AA");
            var id = new SqlParameter("@id", 1);
            var primerNombre = new SqlParameter("@primerNombre", clientDto.primerNombre);
            var segundoNombre = new SqlParameter("@segundoNombre", clientDto.segundoNombre);
            var apellidos = new SqlParameter("@apellidos", clientDto.apellidos);
            var identificacion = new SqlParameter("@identificacion", clientDto.identificacion);
            var correo = new SqlParameter("@correo", clientDto.correo);
            var estado = new SqlParameter("@estado", 1);

            // Parámetros de salida
            var outputCtrl = new SqlParameter("@ctrl", SqlDbType.Int);
            outputCtrl.Direction = ParameterDirection.Output;

            var outputMsjCtrl = new SqlParameter("@msj_ctrl", SqlDbType.NVarChar, 500);
            outputMsjCtrl.Direction = ParameterDirection.Output;

            var outputDato1 = new SqlParameter("@dato_1", SqlDbType.NVarChar, 500);
            outputDato1.Direction = ParameterDirection.Output;

            var outputDato2 = new SqlParameter("@dato_2", SqlDbType.NVarChar, 500);
            outputDato2.Direction = ParameterDirection.Output;



            await _dbcontext.Database.ExecuteSqlRawAsync("sp_trx_clientes @opcion, @id, @primerNombre, @segundoNombre ,@apellidos,@identificacion, @correo, @estado, @ctrl OUTPUT, @msj_ctrl OUTPUT, @dato_1 OUTPUT, @dato_2 OUTPUT",
            opcion, id, primerNombre, segundoNombre, apellidos, identificacion, correo, estado,  outputCtrl, outputMsjCtrl, outputDato1, outputDato2);


            // Obtener los valores de las variables de 
            var ctrl = outputCtrl.Value?.ToString();
            var msjCtrl = outputMsjCtrl.Value?.ToString();
            var dato1 = outputDato1.Value?.ToString();
            var dato2 = outputDato2.Value?.ToString();
           

            if (ctrl != "7")
            {
                return NotFound(new { procesoExitoso = false, mensaje = "Error al guardar cliente" });
            }

            return Ok(new { procesoExitoso = true, id = dato1 });
           

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHome(int id)
        {
            var opcion = new SqlParameter("@opcion", "AB");
            var idCliente = new SqlParameter("@id", id);
            var primerNombre = new SqlParameter("@primerNombre","");
            var segundoNombre = new SqlParameter("@segundoNombre", "");
            var apellidos = new SqlParameter("@apellidos", "");
            var identificacion = new SqlParameter("@identificacion", "");
            var correo = new SqlParameter("@correo", "");
            var estado = new SqlParameter("@estado", 1);

            // Parámetros de salida
            var outputCtrl = new SqlParameter("@ctrl", SqlDbType.Int);
            outputCtrl.Direction = ParameterDirection.Output;

            var outputMsjCtrl = new SqlParameter("@msj_ctrl", SqlDbType.NVarChar, 500);
            outputMsjCtrl.Direction = ParameterDirection.Output;

            var outputDato1 = new SqlParameter("@dato_1", SqlDbType.NVarChar, 500);
            outputDato1.Direction = ParameterDirection.Output;

            var outputDato2 = new SqlParameter("@dato_2", SqlDbType.NVarChar, 500);
            outputDato2.Direction = ParameterDirection.Output;


            await _dbcontext.Database.ExecuteSqlRawAsync("sp_trx_clientes @opcion, @id, @primerNombre, @segundoNombre ,@apellidos,@identificacion, @correo, @estado, @ctrl OUTPUT, @msj_ctrl OUTPUT, @dato_1 OUTPUT, @dato_2 OUTPUT",
                        opcion, idCliente, primerNombre, segundoNombre, apellidos, identificacion, correo, estado, outputCtrl, outputMsjCtrl, outputDato1, outputDato2);



            // Obtener los valores de las variables de 
            var ctrl = outputCtrl.Value?.ToString();
            var msjCtrl = outputMsjCtrl.Value?.ToString();
            var dato1 = outputDato1.Value?.ToString();
            var dato2 = outputDato2.Value?.ToString();


            if (ctrl == "5")
            {
                return NotFound(new { procesoExitoso = false, mensaje = "Cliente no existe" });
            }

            else if (ctrl == "4")
            {
                return NotFound(new { procesoExitoso = false, mensaje = "El cliente ya se encuentra inactivo" });
            }

            return Ok(new { procesoExitoso = true, id = dato1 });


        }

    }




}

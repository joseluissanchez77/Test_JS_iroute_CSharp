using System.ComponentModel.DataAnnotations;

namespace Test_JS_iroute_CSharp.Models.Dto
{
    public class ClientsDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'primer nombre' es obligatorio.")]
        [MaxLength(50)]
        public string primerNombre { get; set; }

        public string segundoNombre { get; set; }
        [Required(ErrorMessage = "El campo 'apellidos' es obligatorio.")]
        [MaxLength(100)]
        public string apellidos { get; set; }
        [Required(ErrorMessage = "El campo 'identificacion' es obligatorio.")]
        [MaxLength(20)]
        public string identificacion { get; set; }
        [Required(ErrorMessage = "El campo 'correo' es obligatorio.")]
        [MaxLength(100)]
        public string correo { get; set; }

    }
}

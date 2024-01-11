using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test_JS_iroute_CSharp.Models
{
    [Table("cliente", Schema = "dbo")]
    public class Clients
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string primerNombre { get; set; }

        public string segundoNombre { get; set; }
        public string apellidos { get; set; }
        public string identificacion { get; set; }
        public string correo { get; set; }
        public int estado { get; set; }

        public Clients() { 
        
            primerNombre = string.Empty;
            segundoNombre = string.Empty;
            apellidos = string.Empty;
            identificacion = string.Empty;
            correo = string.Empty;

        }
    }
}

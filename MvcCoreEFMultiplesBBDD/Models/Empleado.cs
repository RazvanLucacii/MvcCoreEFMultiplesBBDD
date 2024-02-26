using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreEFMultiplesBBDD.Models
{
    [Table("V_EMPLEADOS")]
    public class Empleado
    {
        [Key]
        [Column("IDEMPLEADO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public string? Apellido { get; set; }
        [Column("OFICIO")]
        public string? Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DEPARTAMENTO")]
        public string? NombreDept { get; set; }
        [Column("LOCALIDAD")]
        public string? Localidad { get; set; }
        [Column("IDDEPARTAMENTO")]
        public int IdDepartamento { get; set; }

    }
}

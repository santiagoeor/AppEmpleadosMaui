﻿using System.ComponentModel.DataAnnotations;

namespace AppEmpleados.Models
{
    public class Empleado
    {
        [Key]
        public int idEmpleado { get; set; }

        public string NombreCompleto { get; set; }

        public string Correo {  get; set; }

        public decimal Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }
    }
}

﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace AppEmpleados.DTOs
{
    public partial class EmpleadoDTO : ObservableObject
    {
        [ObservableProperty]
        public int idEmpleado;

        [ObservableProperty]
        public string nombreCompleto;

        [ObservableProperty]
        public string correo;

        [ObservableProperty]
        public decimal sueldo;

        [ObservableProperty]
        public DateTime fechaContrato;
    }
}

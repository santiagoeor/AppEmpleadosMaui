using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppEmpleados.DataAccess;
using AppEmpleados.DTOs;
using AppEmpleados.Utilities;
using AppEmpleados.Models;

namespace AppEmpleados.ViewModels
{
    public partial class EmpleadoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly EmpleadoDbContext _dbContext;

        [ObservableProperty]
        private EmpleadoDTO empleadoDTO = new EmpleadoDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdEmpleado;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public EmpleadoViewModel(EmpleadoDbContext context)
        {
            _dbContext = context;
            EmpleadoDTO.FechaContrato = DateTime.Now;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString()); 
            IdEmpleado = id;

            if(IdEmpleado == 0)
            {
                TituloPagina = "Nuevo Empleado";
            }
            else
            {
                TituloPagina = "Editar Empleado";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Empleados.FirstAsync(e => e.idEmpleado == IdEmpleado);
                    EmpleadoDTO.IdEmpleado = encontrado.idEmpleado;
                    EmpleadoDTO.NombreCompleto = encontrado.NombreCompleto;
                    EmpleadoDTO.Correo = encontrado.Correo;
                    EmpleadoDTO.Sueldo = encontrado.Sueldo;
                    EmpleadoDTO.FechaContrato = encontrado.FechaContrato;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        LoadingEsVisible = false;
                    });

                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            EmpleadoMensaje mensaje = new EmpleadoMensaje();

            await Task.Run(async () =>
            {
                if(IdEmpleado == 0)
                {
                    var tbEmpleado = new Empleado
                    {
                        NombreCompleto = EmpleadoDTO.nombreCompleto,
                        Correo = EmpleadoDTO.Correo,
                        Sueldo = EmpleadoDTO.Sueldo,
                        FechaContrato = EmpleadoDTO.FechaContrato,
                    };

                    _dbContext.Empleados.Add(tbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    EmpleadoDTO.idEmpleado = tbEmpleado.idEmpleado;
                    mensaje = new EmpleadoMensaje()
                    {
                        EsCrear = true,
                        EmpleadoDTO = EmpleadoDTO
                    };
                }
                else
                {
                    var encontrado = await _dbContext.Empleados.FirstAsync(e => e.idEmpleado == IdEmpleado);
                    encontrado.NombreCompleto = EmpleadoDTO.NombreCompleto;
                    encontrado.Correo = EmpleadoDTO.Correo;
                    encontrado.Sueldo = EmpleadoDTO.Sueldo;
                    encontrado.FechaContrato = EmpleadoDTO.FechaContrato;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new EmpleadoMensaje()
                    {
                        EsCrear = false,
                        EmpleadoDTO = EmpleadoDTO
                    };
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EmpleadoMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }
    }
}

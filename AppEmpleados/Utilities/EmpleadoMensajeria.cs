using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppEmpleados.Utilities
{
    public class EmpleadoMensajeria: ValueChangedMessage<EmpleadoMensaje>
    {
        public EmpleadoMensajeria(EmpleadoMensaje value): base(value)
        {

        }
    }
}

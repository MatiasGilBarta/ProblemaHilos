using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    public class ConcreteStrategyEmail : IStrategyNotificar
    {
        public void Notificar(string tipoNotificacion)
        {
            if (tipoNotificacion == "email")
            {
                Console.WriteLine("Generando notificación para Email");
            }
        }
    }
}

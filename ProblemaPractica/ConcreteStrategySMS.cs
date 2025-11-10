using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    public class ConcreteStrategySMS : IStrategyNotificar
    {
        public void Notificar(string tipoNotificacion)
        {
            if (tipoNotificacion == "sms")
            {
                Console.WriteLine("Generando notificación para SMS");
            }
        }
    }
}

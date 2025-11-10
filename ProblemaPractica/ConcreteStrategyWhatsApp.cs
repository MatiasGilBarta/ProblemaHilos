using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    public class ConcreteStrategyWhatsApp : IStrategyNotificar
    {
        public void Notificar(string tipoNotificacion)
        {
            if (tipoNotificacion == "whatsapp")
            {
                Console.WriteLine("Generando notificación para WhatsApp");
            }
        }
    }

}

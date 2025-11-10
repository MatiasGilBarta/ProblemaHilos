using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    public class Trabajo
    {
        public int Paginas { get; set; }
        public IStrategyNotificar Strategy { get; set; }
        public Notificacion Notificacion { get; set; } // tu clase base con TipoNotificacion, Mensaje, etc.
        public string UsuarioNombre { get; set; } // opcional, para mostrar quien mandó el trabajo
    }
}

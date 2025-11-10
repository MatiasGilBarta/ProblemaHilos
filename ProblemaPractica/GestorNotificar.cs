using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    public class GestorNotificar
    {
        //public Notificacion Notificacion { get; set; }

        //private IStrategyNotificar _notificarStrategy;

        //public GestorNotificar(Notificacion not)
        //{
        //    try
        //    {
        //        Notificacion = not;

        //        //usando reflection con el type para instanciar la estrategia correspondiente
        //        Type tipo = Type.GetType("ProblemaPractica.ConcreteStrategy" + not.GetType().Name);
        //        if (tipo != null)
        //            this._notificarStrategy = (IStrategyNotificar)Activator.CreateInstance(tipo);
        //        else
        //            Console.WriteLine("No se encontro la estrategia solicitada");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        ////haciendolo de esta forma no hay if, o 1 solo que es el del get type, permitiendonos exteder codigo de manera sencilla
        //public void Notificar()
        //{
        //    if (_notificarStrategy == null)
        //        throw new Exception("No se ha seteado una estrategia");

        //    string tipo = Convert.ToString(Notificacion.TipoNotificacion);

        //    //return _notificarStrategy.Notificar(Notificacion);
        //}

    }
}

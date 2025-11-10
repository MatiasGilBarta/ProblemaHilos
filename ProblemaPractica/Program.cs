using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread hiloUsuario1 = new Thread(new Usuario("Martin").Producir);
            Thread hiloUsuario2 = new Thread(new Usuario("Julian").Producir);
            Thread hiloImpresora = new Thread(Impresora.Current.imprimir);
            //Thread hiloImpresoraFalso = new Thread(new Impresora().imprimir); no se puede asi pq tiene singleton


            hiloUsuario1.Start();
            hiloUsuario2.Start();
            Thread.Sleep(500);
            hiloImpresora.Start();

            hiloUsuario1.Join();
            hiloUsuario2.Join();



        }
    }
}

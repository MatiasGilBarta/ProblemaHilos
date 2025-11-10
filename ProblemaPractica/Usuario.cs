using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    internal class Usuario : Notificacion
    {
        public Notificacion Notificacion { get; set; }


        private readonly string _name;

        public Usuario(string name)
        {
            _name = name;

        }


        private static readonly Random random = new Random(Environment.TickCount);


        public void Producir()
        {
            for (int j = 1; j <= random.Next(2, 6); j++)
            {
                Impresora.tamañoDoc = random.Next(1, 8);

                IStrategyNotificar strategy = null;
                string tipo = null;

                //Elegir aleatoriamente una de las tres estrategias
                int opcion = random.Next(1, 4); // 1, 2 o 3

                switch (opcion)
                {
                    case 1:
                        strategy = new ConcreteStrategyWhatsApp();
                        tipo = "whatsapp";
                        break;
                    case 2:
                        strategy = new ConcreteStrategySMS();
                        tipo = "sms";
                        break;
                    case 3:
                        strategy = new ConcreteStrategyEmail();
                        tipo = "email";
                        break;
                }
                //----------------------------------------------------

                Monitor.Enter(Impresora.lockObjectUsuarios);
                {
                    try
                    {
                        while (Impresora.colaImpresion.Count >= 20)
                        {
                            Monitor.Enter(Impresora.lockObject);
                            Monitor.Wait(Impresora.lockObject);
                            Monitor.Exit(Impresora.lockObject);
                        }

                        // Encolamos AMBAS cosas dentro del mismo lock para mantener consistencia
                        Monitor.Enter(Impresora.lockObject);
                        try
                        {
                            Impresora.colaImpresion.Enqueue(Impresora.tamañoDoc);
                            Impresora.colaEstrategias.Enqueue(strategy);
                            Impresora.colaTiposNotificacion.Enqueue(tipo);
                            // opcional: si necesitas pasar algún dato extra, puedes ajustar aquí
                            Monitor.Pulse(Impresora.lockObject);
                        }
                        finally
                        {
                            Monitor.Exit(Impresora.lockObject);
                        }

                        Console.WriteLine($"Usuario: {_name} Manda a imprimir un doc de {Impresora.tamañoDoc} págs. (Notificación: {tipo})");

                        if (Impresora.colaImpresion.Count == 10)
                        {
                            Console.WriteLine($"---> Productor: Cola tiene {Impresora.colaImpresion.Count} elementos.");
                            Monitor.Enter(Impresora.lockObject);
                            try { Monitor.Pulse(Impresora.lockObject); }
                            finally { Monitor.Exit(Impresora.lockObject); }
                        }
                    }
                    finally
                    {
                        Monitor.Exit(Impresora.lockObjectUsuarios);
                    }
                }
                Thread.Sleep(random.Next(1000, 3000));

                ////Máximos 20 elementos en la queue
                //while (Impresora.colaImpresion.Count >= 20)
                //{
                //    Monitor.Enter(Impresora.lockObject);
                //    Monitor.Wait(Impresora.lockObject);
                //    Monitor.Exit(Impresora.lockObject);
                //}

                ////Añadimos el número a la cola
                //Impresora.colaImpresion.Enqueue(Impresora.tamañoDoc);
                //Console.WriteLine($"Usuario: {_name} Manda a imprimir un doc de {Impresora.tamañoDoc} pags.");

                //if (Impresora.colaImpresion.Count == 10)
                //{
                //    Console.WriteLine($"---> Productor: Cola tiene {Impresora.colaImpresion.Count} elementos.");
                //    //Notificar al consumidor que hay un nuevo elemento
                //    Monitor.Enter(Impresora.lockObject);
                //    Monitor.Pulse(Impresora.lockObject);
                //    Monitor.Exit(Impresora.lockObject);
                //}

                //} Monitor.Exit(Impresora.lockObjectUsuarios);

                //Pausa del productor aleatoria
                //Thread.Sleep(random.Next(1000, 3000));
            }

        }
    }
}

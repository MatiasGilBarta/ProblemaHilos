using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemaPractica
{
    //singleton en impresora para asegurar que solo haya una instacia
    public sealed class Impresora : Notificacion
    {
        Random random = new Random(Environment.TickCount);

        public Queue<int> colaDoc = new Queue<int>(); //capaz ni hace falta

        //pide que los recursos compartidos esten en impresora

        //el dato que vamos a necesitar es un diccionario
        //donde el primer dato va a ser la cantidad de docs, y el segundo la cantidad de paginas de dicho doc
        public static readonly Queue<int> colaImpresion = new Queue<int>();

        public static readonly Queue<IStrategyNotificar> colaEstrategias = new Queue<IStrategyNotificar>();
        public static readonly Queue<string> colaTiposNotificacion = new Queue<string>();

        public static int tamañoDoc { get; set; } = 0; //Recurso compartido entre Impresora e Usuario

        public static readonly object lockObject = new object(); //Objeto para sincronización Impresora con Usuario

        public static readonly object lockObjectUsuarios = new object(); //Objeto para sincronización Usuario con Usuario 
        //fin de recursos compartidos

        private static readonly Impresora _instance = new Impresora();

        public static Impresora Current
        {
            get
            {
                return _instance;
            }
        }
        private Impresora()
        {
            //implementacion del singleton
        }

        public void imprimir()
        {
            while (true)
            {
                int trabajo = 0;
                IStrategyNotificar strategy = null;
                string tipo = null;

                // --- obtener trabajo y strategy en forma atómica ---
                lock (lockObject)
                {
                    while (colaImpresion.Count == 0)
                    {
                        Monitor.Wait(lockObject);
                    }

                    trabajo = colaImpresion.Dequeue();
                    strategy = colaEstrategias.Dequeue();
                    tipo = colaTiposNotificacion.Dequeue();

                    // si querés mantener tamañoDoc actualizado
                    tamañoDoc = trabajo;

                    // después de sacar un item, avisamos a productores que hay espacio
                    if (colaImpresion.Count == 10) // si antes estaba lleno
                    {
                        Monitor.PulseAll(lockObject);
                    }
                } // <-- salimos del lock aquí

                // --- imprimir fuera del lock ---
                for (int i = 1; i <= trabajo; i++)
                {

                    Console.WriteLine($"Imprimiendo página {i} de {trabajo}");
                    Thread.Sleep(500);

                    // Al terminar la última página, ejecutamos la strategy
                    if (i == trabajo)
                    {
                        try
                        {
                            Console.WriteLine($"Procesando trabajo con notificación tipo {tipo}...");
                            strategy.Notificar(tipo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al notificar: {ex.Message}");
                        }
                    }

                }

                Console.WriteLine($" Documento de {trabajo} páginas impreso. Quedan {colaImpresion.Count} trabajos en cola.\n");
                Thread.Sleep(random.Next(50, 200));

                //int trabajo = 0;
                //IStrategyNotificar strategy = null;

                //// --- obtener trabajo y strategy en forma atómica ---
                //lock (lockObject)
                //{
                //    while (colaImpresion.Count == 0)
                //    {
                //        Monitor.Wait(lockObject);
                //    }

                //    trabajo = colaImpresion.Dequeue();
                //    strategy = colaEstrategias.Dequeue();

                //    // si querés mantener tamañoDoc actualizado
                //    tamañoDoc = trabajo;

                //    // después de sacar un item, avisamos a productores que hay espacio
                //    if (colaImpresion.Count == 10) // si antes estaba lleno
                //    {
                //        Monitor.PulseAll(lockObject);
                //    }
                //} // <-- salimos del lock aquí

                //// --- imprimir fuera del lock ---
                //for (int i = 1; i <= trabajo; i++)
                //{
                //    Console.WriteLine($"Imprimiendo página {i} de {trabajo}");
                //    Thread.Sleep(500);


                //}

                //Console.WriteLine($" Documento de {trabajo} páginas impreso. Quedan {colaImpresion.Count} trabajos en cola.\n");
                //Thread.Sleep(random.Next(50, 200));
            }
        }


        // Obtiene el siguiente trabajo de la cola. Si no hay trabajos, bloquea hasta que haya uno.
        // Usa lock para sincronizar con productores que agregan a colaImpresion.
        public int ObtenerSiguienteTrabajo()
        {
            lock (lockObject)
            {
                while (colaImpresion.Count == 0)
                {
                    // Espera a que un productor agregue un trabajo
                    Monitor.Wait(lockObject);
                }


                int tamaño = colaImpresion.Dequeue();
                // Actualizo la propiedad compartida (si la necesitas)
                tamañoDoc = tamaño;
                return tamaño;
            }
        }

        // Método de ayuda para que los usuarios (productores) encolen trabajos de impresión.
        // Notifica a la impresora que hay trabajo disponible.
        public static void EncolarTrabajo(int paginas)
        {
            lock (lockObject)
            {
                colaImpresion.Enqueue(paginas);
                // Despierto a la impresora/consumidores que estén esperando
                Monitor.Pulse(lockObject);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana07_DAEA_VI
{
    class Program
    {
        public static DataClasses2DataContext context = new DataClasses2DataContext();

        static void Main(string[] args)
        {
            //IntroToLinq();
            //DataSource();
            //Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            //Joining();
            introLambda();
            //datasourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lmabda();
            //JoiningLambda();
            Console.Read();
        }

        static void IntroToLinq()
        {
            int[] numbers = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;
            foreach (int n in numQuery)
            {
                Console.Write("{0,1}", n);
            }
        }

        //DataSource

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //Filtering

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var iten in queryLondonCustomers)
            {
                Console.WriteLine(iten.Ciudad);
            }
        }

        //Ordering

        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "London"
                                        orderby cust.NombreCompañia ascending
                                        select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //Grouping

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var a in queryCustomersByCity)
            {
                Console.WriteLine(a.Key);
                foreach (clientes c in a)
                {
                    Console.WriteLine(" {0}", c.NombreCompañia);
                }
            }
        }

        //Grouping2
        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //Joining

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos
                                 on cust.idCliente equals dist.IdCliente
                                 select new
                                 {
                                     CustomerName = cust.NombreCompañia,
                                     DistributorName = dist.PaisDestinatario
                                 };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

        }


        //funciones Lambda ---------------------------------------------------------------------

        //introLambda

        static void introLambda()
        {
            int[] numbers = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
            var pares = numbers.Where(n => n % 2 == 0).ToList();
            foreach (int num in pares)
            {
                Console.Write(num + "\n");
            }
        }

        //datasourceLambda

        static void datasourceLambda()
        {
            var Allcustomers = context.clientes.ToList();
            foreach (var i in Allcustomers)
            {
                Console.WriteLine(i.NombreCompañia);
            }
        }



        //FilteringLambda##############3

        static void FilteringLambda()
        {
            var clientes = context.clientes.Where(c => c.Ciudad == "Londres").ToList();
            foreach (var iten in clientes)
            {
                Console.WriteLine(iten.Ciudad);
            }
        }





        //OrderingLambda##################3333



        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(c => c.Ciudad == "Londres").OrderByDescending(j => j.NombreCompañia).ToList();

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //GroupingLambda###############33333333

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var a in queryCustomersByCity)
            {
                Console.WriteLine(a.Key);
                foreach (clientes c in a)
                {
                    Console.WriteLine(" {0}", c.NombreCompañia);
                }
            }
        }

        //GroupingLambda2###############33333333
        static void Grouping2Lmabda()
        {
            var customerQuery = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Key.Count() > 2)
                            .OrderBy(c => c.Key);

            foreach (var cq in customerQuery)
            {
                Console.WriteLine(cq.Key);
            }
        }

        //Joininglambda #######################333
        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                                .Join(context.Pedidos,
                                 c => c.idCliente,
                                    b => b.IdCliente,
                                (a, b) => new { a.NombreCompañia, b.PaisDestinatario }
                                     );

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine($"{ item.NombreCompañia} y destinatario : {item.PaisDestinatario}");
            }
        }
    }
}

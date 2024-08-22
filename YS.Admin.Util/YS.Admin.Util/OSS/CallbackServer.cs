using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace callbackserver
{
    class CallbackServer
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    TinyHttpServer.ipaddress = IPAddress.Parse(args[0]);
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("IPAddress.Parse Exception : {0} . ", e.ToString());
                    return;
                }
                TinyHttpServer.port = Convert.ToInt16(args[1]);
            }
            else
            {
                TinyHttpServer.PrintHelp();
            }

            TinyHttpServer.ListenAndServe(); 

            Console.ReadKey();
        }
    }
}

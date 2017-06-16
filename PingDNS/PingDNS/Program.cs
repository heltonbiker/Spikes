using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace PingDNS
{
    class Program
    {
        static void Main(string[] args)
        {
            var ping = new Ping();
            int timeout = 100;

            PingReply reply = ping.Send("8.8.8.8", timeout);

            if (reply.Status == IPStatus.Success)
                Console.WriteLine("foi");
            else
                Console.WriteLine("não foi");


            //Ping myPing = new Ping();
            //String host = "google.com";
            //byte[] buffer = new byte[32];
            //int timeout = 500;
            //PingOptions pingOptions = new PingOptions();
            //PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            //if (reply.Status == IPStatus.Success)
            //    return true;
            //else
            //    return false;


        }
    }
}

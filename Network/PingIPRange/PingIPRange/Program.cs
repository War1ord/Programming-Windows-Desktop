using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PingIPRange
{
    class Program
    {

        static void Main(string[] args)
        {
            #region 1st attempt at network ping (working for one IP)
            //Ping ping = new Ping();
            //PingReply reply = ping.Send("192.168.2.7");
            //if (reply != null && reply.Status == IPStatus.Success)
            //{
            //    Console.WriteLine("host is active");
            //}
            //else
            //{
            //    Console.WriteLine("host is inactive");
            //} 
            #endregion

            #region 2nd attempt at network ping (working for a range of ips based on a hardcoded base ip with a for loop from 0 to 255)
            countdown = new CountdownEvent(1);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string ipBase = "192.168.2.";
            for (int i = 1; i < 255; i++)
            {
                string ip = ipBase + i;
                Ping ping = new Ping();
                ping.PingCompleted += PingCompleted;
                countdown.AddCount();
                ping.SendAsync(ip, 100, ip);
            }
            countdown.Signal();
            countdown.Wait();
            stopwatch.Stop();
            TimeSpan span = new TimeSpan(stopwatch.ElapsedTicks);
            Console.WriteLine("Took {0} milliseconds. {1} hosts active.", stopwatch.ElapsedMilliseconds, upCount);
            Console.ReadLine(); 
            #endregion

        }

        #region 2nd attempt at network ping
        static CountdownEvent countdown;
        static int upCount = 0;
        static object lockObj = new object();
        const bool resolveNames = true;
        static void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            string ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                if (resolveNames)
                {
                    string name;
                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                        name = hostEntry.HostName;
                    }
                    catch (SocketException ex)
                    {
                        name = "?";
                    }
                    Console.WriteLine("{0} ({1}) is up: ({2} ms)", ip, name, e.Reply.RoundtripTime);
                }
                else
                {
                    Console.WriteLine("{0} is up: ({1} ms)", ip, e.Reply.RoundtripTime);
                }
                lock (lockObj)
                {
                    upCount++;
                }
            }
            else if (e.Reply == null)
            {
                Console.WriteLine("Pinging {0} failed. (Null Reply object?)", ip);
            }
            countdown.Signal();
        } 
        #endregion

        #region Holding code for possible later use.
        private static long ToInt(string address)
        {

            return (long)(uint)System.Net.IPAddress.NetworkToHostOrder(
                (int)System.Net.IPAddress.Parse(address).Address);
        }
        private static string ToAddr(long address)
        {
            return System.Net.IPAddress.Parse(address.ToString()).ToString();
        }
        #endregion

    }
}

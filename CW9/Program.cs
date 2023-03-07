using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CW9
{
   class FindPiThread
    {
         int darts;
         int count;
         Random ran;
    
    public FindPiThread(int dartsThrown)
        {
            darts = 0;
            darts = dartsThrown;
            count = 0;
            ran = new Random();
        }

        public void throwDarts()
        {
            for (int i = 0; i < darts; i++)
            {
                double x = ran.NextDouble();
                double y = ran.NextDouble();

                if ((x * x + y * y) < 1.0)
                {
                    count++;
                }

            }

        }

        public int getNumberInside()
        {
            return count;
        }

    }





    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Throws for each thread?");
            int throws = int.Parse(Console.ReadLine());

            Console.WriteLine("Threads to  run?");
            int numThread = int.Parse(Console.ReadLine());

            List<Thread> threads = new List<Thread>();

            List<FindPiThread> list = new List<FindPiThread>();

            for (int i = 0; i < numThread; i++)
            {
                FindPiThread piThread = new FindPiThread(throws);
                list.Add(piThread);

                Thread thread = new Thread(new ThreadStart(piThread.throwDarts));
                threads.Add(thread);

                thread.Start();
                Thread.Sleep(16);

            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            int hitInside = 0;
            foreach(FindPiThread piThread in list)
            {
                hitInside += piThread.getNumberInside();
            }
            
            int pi = ((4 * hitInside) / throws);
            Console.WriteLine("Pi evaluation: " + pi);
            Thread.Sleep(500000);
        }
    }
}

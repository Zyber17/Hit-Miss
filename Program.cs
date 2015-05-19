using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatProj2015
{
    class Program
    {
        static int len = 10000000;
        static int reps = 10;
        static int n = 0;
        static void Main(string[] args)
        {
            System.Console.Out.WriteLine("Running...");
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            s.Start();
            Test0();
            Test1();
            Test2();
            int[] data0 = new int[100000];
            int[] data1 = new int[100000];
            int[] data2 = new int[100000];
            for (int i = 0; i < 1000; i++)
            {
                System.Console.SetCursorPosition(0, 1);
                System.Console.Out.WriteLine(i);
                data0[i] = (int)Test0();
                data1[i] = (int)Test1();
                data2[i] = (int)Test2();
                n++;
                if (n % 10 == 0 && n > 0)
                {
                    System.Console.Out.WriteLine("Elapsed: " + (s.ElapsedMilliseconds / 1000) + "s");
                    calcStats(data0, data1, data2);
                }
            }
            System.Console.SetCursorPosition(0, 1);
            System.Console.Out.WriteLine(999);
            calcStats(data0, data1, data2);
            s.Stop();
            System.Console.Out.WriteLine("Terminated at t=" + s.ElapsedMilliseconds / 1000f + "s");
            System.Console.Out.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
        public static void calcStats(int[] data0, int[] data1, int[] data2)
        {
            double[] mean = new double[3];
            for (int i = 0; i < n; i++)
            {
                mean[0] += data0[i];
                mean[1] += data1[i];
                mean[2] += data2[i];
            }
            mean[0] /= n;
            mean[1] /= n;
            mean[2] /= n;
            double[] sdev = new double[3];
            for (int i = 0; i < n; i++)
            {
                sdev[0] += ((data0[i] - mean[0]) * (data0[i] - mean[0]));
                sdev[1] += ((data1[i] - mean[1]) * (data1[i] - mean[1]));
                sdev[2] += ((data2[i] - mean[2]) * (data2[i] - mean[2]));
            }
            sdev[0] /= n;
            sdev[1] /= n;
            sdev[2] /= n;
            sdev[0] = Math.Sqrt(sdev[0]);
            sdev[1] = Math.Sqrt(sdev[1]);
            sdev[2] = Math.Sqrt(sdev[2]);
            double[] kurt = new double[6];
            for (int i = 0; i < n; i++)
            {
                kurt[0] += Math.Pow(data0[i] - mean[0], 4);
                kurt[3] += (data0[i] - mean[0]) * (data0[i] - mean[0]);
                kurt[1] += Math.Pow(data1[i] - mean[1], 4);
                kurt[4] += (data1[i] - mean[1]) * (data1[i] - mean[1]);
                kurt[2] += Math.Pow(data2[i] - mean[2], 4);
                kurt[5] += (data2[i] - mean[2]) * (data2[i] - mean[2]);
            }
            kurt[0] /= (kurt[3] * kurt[3]);
            kurt[1] /= (kurt[4] * kurt[4]);
            kurt[2] /= (kurt[5] * kurt[5]);
            kurt[0] *= (n * (n + 1) * (n - 1)) / ((n - 2) * (n - 3));
            kurt[1] *= (n * (n + 1) * (n - 1)) / ((n - 2) * (n - 3));
            kurt[2] *= (n * (n + 1) * (n - 1)) / ((n - 2) * (n - 3));
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth * 3));
            Console.SetCursorPosition(0, currentLineCursor);
            System.Console.Out.WriteLine("means: " + mean[0] + ", " + mean[1] + ", " + mean[2]);
            System.Console.Out.WriteLine("sdevs: " + sdev[0] + ", " + sdev[1] + ", " + sdev[2]);
            System.Console.Out.WriteLine("kurts: " + kurt[0] + ", " + kurt[1] + ", " + kurt[2]);
        }
        public static long Test0()
        {
            int[] data = new int[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = 16;
            }
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            s.Start();
            for (int a = 0; a < reps; a++)
            {
                for (int i = 0; i < len; i++)
                {
                    data[i] += 8;
                }
            }
            s.Stop();
            return s.ElapsedMilliseconds;
        }
        public unsafe static long Test1()
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            int[] data = new int[len];
            fixed (int* p = data)
            {
                for (int i = 0; i < len; i++)
                {
                    data[i] = 16;
                }
                s.Start();
                for (int a = 0; a < reps; a++)
                {
                    for (int i = 0; i < len; i++)
                    {
                        *(p + i) += 8;
                    }
                }
                s.Stop();
            }
            return s.ElapsedMilliseconds;
        }
        public unsafe static long Test2()
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            int[] data = new int[len];
            fixed (int* p = data)
            {
                for (int i = 0; i < len; i++)
                {
                    data[i] = 16;
                }
                s.Start();
                for (int a = 0; a < reps; a++)
                {
                    for (int i = 0; i < len; i++)
                    {
                        *(p + ((i % len) / 2) + ((len / 2) * (i % 2))) += 8;
                    }
                }
                s.Stop();
            }
            return s.ElapsedMilliseconds;
        }
    }
}

using System;

namespace GCExample
{
    class Program : IDisposable
    {
        private Program p;
        public void GCExample()
        {
            Console.WriteLine("The number of generations are: " +
                                           GC.MaxGeneration);
            Program p = new Program();
            Console.WriteLine("The generation number of object obj is: "
                                               + GC.GetGeneration(p));
            Console.WriteLine("Garbage Collection Memory before collection: "
                                              + GC.GetTotalMemory(false));
            GC.Collect(0);
            Console.WriteLine("Garbage Collection in Generation 0 is: "
                                              + GC.CollectionCount(0));
            Console.WriteLine("Garbage Collection Memory after collection: "
                                              + GC.GetTotalMemory(false));

        }
        public void usingexample()
        {
            using (Program p = new Program())
            {
                p.invokemethod();
            }
           // p.invokemethod(); will throw an expection
        }
        public void invokemethod()
        {
            Console.WriteLine("Just for usage of the object");
        }
        public void disposeexample()
        {
            Program s = null;
            try
            {
                s = new Program();
                s.invokemethod();

            }
            finally
            {

                if (s != null)
                    ((IDisposable)s).Dispose();
            }
        }
        ~Program()
        {
            //This is destructor as well as finalize
            this.Dispose();
            Console.WriteLine("Finalizing object");

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (p != null) p.Dispose();
            }
        }
        public static void Main(string[] args)
        {
            Program x = new Program();
            x.Dispose();
            x.disposeexample();
            x.GCExample();
            x.usingexample();
            Program y = new Program();
        }
    }
}


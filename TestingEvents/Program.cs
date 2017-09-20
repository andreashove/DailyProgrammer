using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEvents
{
    class B
    {
        
        //A public event for listeners to subscribe to
        public event EventHandler SomethingHappened;

        private void Explosion(object o, EventArgs s)
        {
            //Fire the event - notifying all subscribers
            if (SomethingHappened != null)
                SomethingHappened(this, null);
        }

        internal void InitializeExplosion()
        {
            Explosion(this, null);
        }
    }
    class A
    {
        //Where B is used - subscribe to it's public event
        public A()
        {
            B objectToSubscribeTo = new B();
            objectToSubscribeTo.SomethingHappened += HandleSomethingHappening;
            objectToSubscribeTo.InitializeExplosion();
            
        }

        public void HandleSomethingHappening(object sender, EventArgs e)
        {
            Console.WriteLine("HandleSomething");
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
        }
    }
}

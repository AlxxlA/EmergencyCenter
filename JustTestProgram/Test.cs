using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Map;

namespace JustTestProgram
{
    class Test
    {
        public static event  KeyEventHandler KeyPressDown;
        static void Main()
        {
            //KeyPressDown += OnKeyDown;
            //while (true)
            //{
            //    if (Console.KeyAvailable)
            //    {
                  
                    

            //        KeyPressDown(null,new KeyEventArgs(Control.ModifierKeys));
            //    }

            //    Console.WriteLine("Update");
            //    Thread.Sleep(1000);
            //}

            

        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Modifiers);
            Console.WriteLine("Event Handle");
        }
    }
}

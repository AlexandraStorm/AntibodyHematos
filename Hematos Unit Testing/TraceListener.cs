using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

namespace Hematos_Unit_Testing
{
   internal class MyListener : System.Diagnostics.TraceListener
    {
       public override void Write(string o)
       {
           Console.Write(o);
       }
       public override void WriteLine(string o)
       {
           Console.Write(o);
       }
    }
}

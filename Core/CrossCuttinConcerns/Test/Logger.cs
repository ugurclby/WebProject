using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace Core.CrossCuttinConcerns.Test
{
    public class Logger :  IInterceptor
    {
          
       public void Intercept(IInvocation invocation)
       {
           var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
           var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString())); 

           var watch = System.Diagnostics.Stopwatch.StartNew();
           invocation.Proceed(); //Intercepted method is executed here.
           watch.Stop();
           var executionTime = watch.ElapsedMilliseconds;


           Debug.WriteLine($"Calling: {name},Args: {args},Done: result was {invocation.ReturnValue},Execution Time: {executionTime} ms.");

 
       }
    }
}

﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class MockInterceptor : IInterceptor
    {

        private Dictionary<MethodInfo, List<Behavior>> Recordings = new Dictionary<MethodInfo,List<Behavior>>();

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Before target call");
            try
            {
                Replay(invocation);
                //invocation.Proceed();
            }
            catch (Exception)
            {
                Console.WriteLine("Target threw an exception!");
                throw;
            }
            finally
            {
                Console.WriteLine("After target call");
            }
        }

        private void Replay(IInvocation invocation)
        {
            var recordings = Recordings[invocation.Method];
            foreach (var recording in recordings)
            {
                if (recording.IsMatch(invocation.Arguments))
                {
                    recording.CallCount++;
                    invocation.ReturnValue = recording.ReturnValue;
                    return;
                }

            }
        }


        public Behavior AddRecord(MethodInfo method, List<Matcher> matchers)
        {
            if (!Recordings.ContainsKey(method))
                Recordings.Add(method, new List<Behavior>());
            var methodRecordings = Recordings[method];
            var stub = new Behavior() { Matchers = matchers };
            methodRecordings.Insert(0, stub);
            return stub;
        }
    }
}

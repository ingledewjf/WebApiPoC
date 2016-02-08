namespace UnityApiPoc.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.Practices.Unity.InterceptionExtension;

    public class LoggingInterceptionBehaviour : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            WriteLog(string.Format("Invoking method {0} at {1}", input.MethodBase, DateTime.Now.ToLongDateString()));

            var result = getNext()(input, getNext);

            if (result.Exception != null)
            {
                WriteLog(
                    string.Format(
                        "Method {0} threw exception {1} at {2}",
                        input.MethodBase,
                        result.Exception.Message,
                        DateTime.Now));
            }
            else
            {
                WriteLog(string.Format("Method {0} returned {1} at {2}", input.MethodBase, result.ReturnValue, DateTime.Now.ToLongDateString()));
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        private void WriteLog(string log)
        {
            Debug.WriteLine(log);
        }
    }
}
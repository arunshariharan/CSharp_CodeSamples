using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeSamples
{
    public static class HandleUserInput
    {
        private const string CommonMethodNameToExecute = "ExecuteMethods";
        public static void ExecuteCodeSampleFromClass(int userInput, List<Type> ClassNames)
        {
            if (userInput >= ClassNames.Count)
            {
                throw new Exception($"Invalid user Input. Value must be from 0 to {ClassNames.Count - 1}. Exiting");
            }

            // C# Reflection to execute a method from the chosen class
            Type classType = Type.GetType(ClassNames[userInput].FullName);
            Object classInstance = Activator.CreateInstance(classType);
            MethodInfo methodToExecute = classType.GetMethod(CommonMethodNameToExecute);
            methodToExecute.Invoke(classInstance, null);
        }
    }
}

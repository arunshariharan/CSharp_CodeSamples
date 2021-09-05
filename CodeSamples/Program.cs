using System;
using System.Linq;
using System.Reflection;

namespace CodeSamples
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Use C# Reflection to get the list of all classes to display
            var ClassNames = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(ns => ns.Namespace == "CodeSamples")
                                .Where(r => r.Name.Contains("CodeSample", StringComparison.CurrentCultureIgnoreCase)).ToList();

            Console.WriteLine("Select the Code sample to execute from below:");
            for(int i = 0; i < ClassNames.Count; i++)
            {
                Console.WriteLine(i + ". " + ClassNames[i]);
            }

            try
            {
                var userInput = Int32.Parse(Console.ReadLine().Trim());          

                HandleUserInput.ExecuteCodeSampleFromClass(userInput, ClassNames);
            }
            catch(Exception e)
            {
                Console.WriteLine(e); ;
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}

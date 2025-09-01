using System;
using Python.Runtime;
namespace ActionTest
{
    public class PyEntry
    {
        public static void Run()
        {
            string dllPath = @"C:\\Python313\\python313.dll";
            string pythonHomePath=@"C:\\Python313";
            string[] py_paths = 
            {
                "DLLs",
                "Lib",
                "Lib/site-packages",
            };
            string pySearchPath = $"{pythonHomePath};";
            foreach (string p in py_paths)
            {
                pySearchPath += $"{pythonHomePath}/{p};";
            }
            pySearchPath += $"{Path.GetFullPath($"../../../../Scripts")};";
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", dllPath);
            PythonEngine.PythonHome = pythonHomePath;           
            PythonEngine.PythonPath = pySearchPath;
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                try
                {
                    dynamic process = Py.Import("numpy");
                    PyObject ar = process.array(new int[] { 1, 2, 3, 4 });
                    Console.WriteLine(ar);
                    dynamic mm = Py.Import("mymodule");
                    Console.WriteLine(mm.greet("World"));
                }
                catch (PythonException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
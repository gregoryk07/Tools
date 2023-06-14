using Microsoft.Win32;
using System;

namespace RegisterProtocol
{
    class Program
    {
        public static bool silent = false;
        static void Main(string[] args)
        {
            if (args.Length == 5)
            {
                if (args[4] == "silent")
                {
                    silent = true;
                }
            }
            if (args.Length >= 4)
            {
                if (args[3] == "add")
                {
                    RegisterMyProtocol(args[0], args[1], args[2]);
                    Console.WriteLine("Successfully registered protocol for app " + args[2] + ".");
                    if(!silent)
                    Console.ReadLine();
                }
                else if(args[3] == "remove")
                {
                    RemoveMyProtocol(args[0], args[1], args[2]);
                    Console.WriteLine("Successfully removed protocol for app " + args[2] + ".");
                    if(!silent)
                    Console.ReadLine();
                }
                
                
            }
            else if (args.Length == 0)
            {
                Console.WriteLine("Enter full app path: ");
                string path = Console.ReadLine();
                Console.WriteLine("\nEnter app name: ");
                string name = Console.ReadLine();
                Console.WriteLine("\nEnter full app name: ");
                string fullname = Console.ReadLine();
                Console.WriteLine("Write: add/remove");
                string func = Console.ReadLine();
                if (func == "add")
                {
                    RegisterMyProtocol(path, name, fullname);
                    Console.WriteLine("Successfully registered protocol for app " + name + ".");
                    if(!silent)
                    Console.ReadLine();
                }
                else if (func == "remove")
                {
                    RemoveMyProtocol(path, name, fullname);
                    Console.WriteLine("Successfully removed protocol for app " + name + ".");
                    if(!silent)
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\n\nERROR: Invalid input.");
                }

            }
            else
            {
                Console.WriteLine("Invalid arguments. Number of arguments passed: "+args.Length+".");
                if(!silent)
                Console.ReadLine();
            }
        }
        static void RegisterMyProtocol(string myAppPath, string myAppName, string fullAppName)  //myAppPath = full path to your application
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(myAppName);  //open myApp protocol's subkey

            if (key == null)  //if the protocol is not registered yet...we register it
            {
                key = Registry.ClassesRoot.CreateSubKey(myAppName);
                key.SetValue(string.Empty, fullAppName);
                key.SetValue("URL Protocol", string.Empty);

                key = key.CreateSubKey(@"shell\open\command");
                key.SetValue(string.Empty, myAppPath + " " + "%1");
                //%1 represents the argument - this tells windows to open this program with an argument / parameter
            }

            key.Close();
        }
        static void RemoveMyProtocol(string myAppPath, string myAppName, string fullAppName)  //myAppPath = full path to your application
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("", writable: true);  //open myApp protocol's subkey


            key.DeleteSubKeyTree(myAppName);
                //%1 represents the argument - this tells windows to open this program with an argument / parameter

            key.Close();
        }
    }
}

using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace RandomPasswordgenerator
{
    internal class PasswordGenerator
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the lenght of your desired password between 8 and 64:\t");
            int length;

            while   (!int.TryParse(Console.ReadLine(), out  length) || length < 8)
            {
                Console.WriteLine("\nThe lenght must be a positive number between 8 and 64");
                Console.Write("\nEnter desired password lenght:\t");
            }

            Console.Write("\nDo you want to include upercase (y/n):\t");
            bool includeUpperCase = Console.ReadLine().ToLower() == "y";

            Console.Write("\nDo you want to include lowercase (y/n):\t");
            bool includeLowerCase = Console.ReadLine().ToLower() == "y";

            Console.Write("\nDo you want to include digits (y/n):\t");
            bool includeDigits = Console.ReadLine().ToLower() == "y";

            Console.Write("\nDo you want to include symbols (y/n):\t");
            bool includeSymbol = Console.ReadLine().ToLower() == "y";


            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string symbols = "!@#$%^&*()_-+=<>?";


            string AllChars = "";
            if (includeUpperCase) AllChars += upper;
            if (includeLowerCase) AllChars += lower;
            if (includeDigits) AllChars += digits;
            if (includeSymbol) AllChars += symbols;


            char[] password = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[length];
                rng.GetBytes(data);
                for (int i = 0; i < length; i++)
                    password[i] = AllChars[data[i] % AllChars.Length];
            }
            string finalPassword = new string(password);
            Console.WriteLine($"\nYour passwrod is: {finalPassword}");

            Console.Write("\nWould you like to save your password to a txt file(y/n):\t");
            string answer = Console.ReadLine().Trim().ToLower();
            if (answer == "y")
            {
                string filepath = "Passwords/password.txt";
                Console.Write("\nHow to name the file(default name is password):\t");
                string fileName = (Console.ReadLine() ?? "").ToLower().Trim();
                filepath = Path.Combine(Path.GetDirectoryName(filepath) ?? "", fileName + Path.GetExtension(filepath));

                if(File.Exists(filepath))
                {
                    Console.WriteLine($"\nFile with name {filepath} already exists");
                    Console.Write("\nWould you want to overwrite the file (yes or no):\t");
                    string response = Console.ReadLine().ToLower().Trim();
                    if (response!="yes" && response !="y")
                    {
                        Console.WriteLine("Operation cancelled, file not saved");
                        Console.WriteLine("Shut down ...");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }
                }
                

                File.WriteAllText(filepath, $"Your password is :{finalPassword}\n");
                Console.WriteLine("Password saved Succefully");
                Console.WriteLine("Shut down ....");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else { Console.WriteLine("Shut down ....");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }
        
    }
}

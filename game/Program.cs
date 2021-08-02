using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace game
{
    class Program
    {
        public static string HMACHASH(string str, byte[] bkey)
        {
            using (var hmac = new HMACSHA256(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(str);
                var bhash = hmac.ComputeHash(bstr);
                return BitConverter.ToString(bhash).Replace("-", string.Empty).ToLower();
            }
        }

        public static string bytesToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(String.Format("{0:X2}", b));
            }
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            bool isTrue = true;

            for (int i = 0; i < args.Length; i++){
                for(int j = i + 1; j < args.Length ; j++)
                {
                    if(args[i] == args[j])
                    {
                        isTrue = false;
                    }
                }
            }
            if(args.Length % 2 == 0 || args.Length < 3)
            {
                isTrue = false;
            }


            if (isTrue)
            {
                Random random = new Random();
                int computer = random.Next(args.Length - 1);//args

                //key
                byte[] bytes = new byte[16];
                //RNGCryptoServiceProvider is an implementation of a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(bytes); // The array is now filled with cryptographically strong random bytes.

                Console.WriteLine("HMAC: " + HMACHASH(args[computer + 1], bytes));

                string value = String.Empty;
                int numb = 1;
                foreach (string arg in args)
                {
                    Console.WriteLine(numb + " " + arg);
                    numb++;
                }
                Console.WriteLine("0 - exit");
                int choice;
                do
                {
                    value = Console.ReadLine();
                    choice = int.Parse(value);
                    Console.WriteLine("Enter your move: ");

                    int middlePoint = args.Length / 2;
                    if (args.Length >= 3 && choice > 0 && choice <= args.Length)
                    {
                        Console.WriteLine("Your move: " + args[choice - 1] + "\nComp move: " + args[computer]);

                        int differ = 0;

                        if (choice >= computer + 1)
                        {
                            differ = args.Length - (choice - (computer + 1));
                        }
                        else if (choice < computer + 1)
                        {
                            differ = (computer + 1 - choice);
                        }
                        if (differ == 0 || differ == args.Length) Console.WriteLine("draw");
                        else if (differ <= middlePoint)
                        {

                            Console.WriteLine("u win");//win
                        }
                        else Console.WriteLine("u lose");//lose
                    }
                    else if (choice == 0) Console.WriteLine("exit");
                    else Console.WriteLine("not correct");
                    Console.WriteLine("HMAC KEY: " + bytesToHex(bytes));
                } while (choice != 0);
            }
            else
            {
                InputEnter();
            }
            Console.ReadLine();
        }
        static void InputEnter()
        {
            Console.WriteLine("Input Error\nExample: rock paper scissors lizard Spoke");
        }
    }
}

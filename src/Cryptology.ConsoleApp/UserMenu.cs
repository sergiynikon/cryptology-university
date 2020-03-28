using System;
using System.Threading.Channels;
using Cryptology.Domain;
using Cryptology.Domain.Algorithms;
using Microsoft.VisualBasic;

namespace Cryptology.ConsoleApp
{
    public class UserMenu
    {
        private readonly Cipher _cipher = new Cipher();

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Set encoder");
                Console.WriteLine("2. Encrypt");
                Console.WriteLine("3. Decrypt");

                string result;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D0:
                        return;
                    case ConsoleKey.D1:
                        SetEncoder();
                        break;
                    case ConsoleKey.D2:
                        CaptureInput();
                        result = _cipher.Encode();
                        Console.WriteLine($"Encoded result: {result}");
                        break;
                    case ConsoleKey.D3:
                        CaptureInput();
                        result = _cipher.Decode();
                        Console.WriteLine($"Decoded result: {result}");

                        break;
                    default:
                        Console.WriteLine("Invalid input, please try to enter another value!");
                        Console.ReadKey();
                        break;
                }

                Console.ReadKey();
            }
        }

        private void SetEncoder()
        {
            Console.Clear();
            Console.WriteLine("Choose a cipher:");
            Console.WriteLine("1. Caesar");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    _cipher.SetCipher(new CaesarCipher());
                    Console.WriteLine("Caesar cipher - set");
                    break;
                default:
                    Console.WriteLine("Invalid input, please try to enter another value!");
                    break;
            }
        }

        private void CaptureInput()
        {
            Console.Clear();
            Console.WriteLine("Enter the string you want to encode/decode and press enter");
            _cipher.Message = Console.ReadLine();
            Console.WriteLine("Enter the key to encode/decode");
            _cipher.Key = Console.ReadLine();
        }
    }
}
using Cryptology.Domain;
using Cryptology.Domain.Ciphers;
using System;

namespace Cryptology.ConsoleApp
{
    public class UserMenu
    {
        private readonly CipherProvider _cipherProvider = new CipherProvider();

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Set encryption provider");
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
                        result = _cipherProvider.Encrypt();
                        Console.WriteLine($"Encrypted result: {result}");
                        break;
                    case ConsoleKey.D3:
                        CaptureInput();
                        result = _cipherProvider.Decrypt();
                        Console.WriteLine($"Decrypted result: {result}");

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
                    _cipherProvider.SetCipher(new CaesarCipher());
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
            Console.WriteLine("Enter the message you want to encrypt/decrypt and press enter");
            _cipherProvider.Message = Console.ReadLine();
            Console.WriteLine("Enter the key and press enter");
            _cipherProvider.Key = Console.ReadLine();
        }
    }
}
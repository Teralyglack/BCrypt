using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация хэширования паролей с помощью BCrypt.Net-Next\n");

            try
            {
                // Шаг 1: Ввод пароля
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();

                // Проверка на пустой пароль
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Ошибка: Пароль не может быть пустым!");
                    return;
                }
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);

                Console.WriteLine($"\nХэш вашего пароля: {hashedPassword}");
                Console.WriteLine("(Обратите внимание: хэш выглядит как случайная строка)\n");

                // Шаг 3: Повторный ввод для проверки
                Console.Write("Повторите пароль для проверки: ");
                string passwordRepeat = Console.ReadLine();

                // Шаг 4: Верификация
                bool isValid = BCrypt.Net.BCrypt.Verify(passwordRepeat, hashedPassword);

                if (isValid)
                {
                    Console.WriteLine("\nУСПЕХ! Пароли совпадают!");
                    Console.WriteLine("BCrypt подтвердил, что пароль верный.");
                }
                else
                {
                    Console.WriteLine("\nОШИБКА! Пароли НЕ совпадают!");
                    Console.WriteLine("BCrypt обнаружил несоответствие.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nКРИТИЧЕСКАЯ ОШИБКА: {ex.Message}");
                Console.WriteLine("Перезапустите программу.");
            }
            finally
            {
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }
    }
}

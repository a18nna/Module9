using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module9
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }

    public class NameSorter
    {
        public event Action<List<string>> Sorted;

        public void SortNames(List<string> names, int sortOrder)
        {
            try
            {
                if (sortOrder != 1 && sortOrder != 2)
                {
                    throw new CustomException("Неверный номер сортировки. Введите 1 или 2.");
                }

                List<string> sortedNames;
                if (sortOrder == 1)
                {
                    sortedNames = names.OrderBy(n => n).ToList();
                }
                else
                {
                    sortedNames = names.OrderByDescending(n => n).ToList();
                }

                Sorted?.Invoke(sortedNames); 
            }
            catch (CustomException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Непредвиденная ошибка: {e.Message}");
            }
            finally
            {
                Console.WriteLine("Операция сортировки завершена.");
            }
        }

        public static void Main(string[] args)
        {
            NameSorter sorter = new NameSorter();
            List<string> names = new List<string>() { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов" };

            sorter.Sorted += (sortedNames) =>
            {
                Console.WriteLine("\nОтсортированный список фамилий:");
                foreach (string name in sortedNames)
                {
                    Console.WriteLine(name);
                }
            };

            while (true)
            {
                Console.WriteLine("\nВыберите тип сортировки:");
                Console.WriteLine("1 - A-Я");
                Console.WriteLine("2 - Я-А");
                Console.WriteLine("3 - Выход");
                Console.Write("Введите номер: ");

                string input = Console.ReadLine();

                if (input == "3") break;

                try
                {
                    int sortOrder = int.Parse(input);
                    sorter.SortNames(names, sortOrder);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный формат ввода. Пожалуйста, введите число.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }
            }
        }
    }
}

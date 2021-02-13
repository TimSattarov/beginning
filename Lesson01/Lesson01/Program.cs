using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();

            //Вывод на консоль всех книг, находящихся в библиотеке
            Console.WriteLine("Библиотека содержит следующие книги:");
            foreach (Book item in library)
            {
                Console.WriteLine($"Автор - {item.Author}, Название - {item.Title}, Инв.номер - {item.Id}");
            }

            Console.WriteLine("***********************");



            //Вывод на консоль книги с определенным инв. номером 
            Console.Write("Введите номер книги: ");
            if (Int32.TryParse(Console.ReadLine(), out int num) == false)
            {
                // проверка ввода, если не число, то вывести номер за пределы списка номеров книг
                num = -1;
            }

            Book book = library[num];
            if (book != null)
            {
                Console.WriteLine($"Автор - {book.Author}, Название - {book.Title}, Инв.номер - {book.Id}");
            }
            else Console.WriteLine("Книга не найдена");

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}

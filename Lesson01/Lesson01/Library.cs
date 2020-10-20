using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lesson01
{
    public class Library : IEnumerable
    {
        public List<Book> books = new List<Book>()
        {
            new Book() {Author = "Александр Дюма", Title = "Граф Монте-Кристо", Id = 0},
            new Book() {Author = "Михаил Булгаков", Title = "Мастер и Маргарита", Id = 5},
            new Book() {Author = "Фёдор Достоевский", Title = "Преступление и наказание", Id = 12},
            new Book() {Author = "Александр Пушкин", Title = "Руслан и Людмила", Id = 116},
            new Book() {Author = "Эрнест Хемингуэй", Title = "Старик и море", Id = 652},
            new Book() {Author = "Даниель Дефо", Title = "Робинзон Крузо", Id = 135},
            new Book() {Author = "Александр Пушкин", Title = "Дубровский", Id = 62},
            new Book() {Author = "Артур Конан Дойльн", Title = "Приключения Шерлока Холмса", Id = 1023},
            new Book() {Author = "Джордж Оруэлл", Title = "1984", Id = 33},
            new Book() {Author = "Александр Пушкин", Title = "Капитанская дочка", Id = 142},
        };

        public Book this[int number]
        {
            get
            {
                var book = books.FirstOrDefault(b => b.Id == number);
                return book;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return books.GetEnumerator();
        }
    }
}

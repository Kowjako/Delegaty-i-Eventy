using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
namespace ConsoleApplication10
{
    //PRZYKLAD 1//
    class AccountEventArgs //класс для изменений
    {
        public string Message { get; }
        public int Sum { get; }
        public AccountEventArgs(string mes, int sum)
        {
            Message = mes;
            Sum = sum;
        }
    }
    class Account
    {
        public delegate void AccountHandler(object sender, AccountEventArgs e); //Создаём делегат
        public event AccountHandler Event; //событие инициализрующее наш делегат
        public void Put(int sum) //метод при вызове которого будет событие
        {
            Event?.Invoke(this, new AccountEventArgs("Hello", sum)); //вызываем событие, this - указывает что sender'oм будет объект класса который его вызовет
        }
    }
    class Program
    {
        static void Main()
        {
            Account acc = new Account(); //создаём объект класса
            acc.Event += DisplayMessage; //обработчик события, без него не будет смысла т.к наш Event = NULL;
            acc.Put(300); //вызывая метод выполняется событие
            Console.Read();
        }
        public static void DisplayMessage(object sender, AccountEventArgs e) //метод для события, идентичен делегату
        {
            Console.WriteLine(e.Sum);
            Console.WriteLine(e.Message);
        }
    }
    //PRZYKLAD 2//
    //У нас два класса со своими методами и класс в котором идет цикл, как только цикл дойдет до 50 вызвать первый и второй класс с сообщением
    class Handler1
    {
        public void Message()
        {
            Console.WriteLine("CLASS 1 is ready!");
        }
    }
    class Handler2
    {
        public void Message()
        {
            Console.WriteLine("CLASS 2 is ready!");
        }
    }
    class CouterClass
    {
        public delegate void MethodContainer();
        public event MethodContainer Event;
        public void Count()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 50) Event();
            }
        }
    }
    class Program2
    {
        Handler1 h1 = new Handler1();
        Handler2 h2 = new Handler2();
        CouterClass counter = new CouterClass();
        //counter.Event += h1.Message; подписали на событие сообщение первого класса
        //counter.Event += h2.Message; подписали на событие сообщение второго класса 
        //counter.Count(); запускаем метод в котором сработает событие
    }
}


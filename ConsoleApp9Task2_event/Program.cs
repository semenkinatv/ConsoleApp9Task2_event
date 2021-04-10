using System;

namespace ConsoleApp9Task2_event
{
    public class MyException : Exception
    {
        public MyException()
        {
            Console.WriteLine("Сработал мой тип исключений");
        }

        public MyException(string message)
            : base(message)
        { }
    }

    class Program
    {
        static string[] listfio = new string[] { "Яковлев", "Сидоров", "Алдонин", "Железнов", "Токарев" };

        static void Main(string[] args)
        {
            Console.WriteLine("Исходный список фамилий:");

            foreach (string fio in listfio)
            { Console.WriteLine(fio); }

            NumberReader numberReader = new NumberReader();
            numberReader.NumberEnterEvent += SortList;
            while (true)
            {
                try
                {
                    numberReader.Read();
                }
                catch (MyException ex)
                {
                    Console.WriteLine( ex.Message);
                }
            }
        }

        static string[] Sort(string[] fio, int param)
        {
            string temp;
            for (int i = 0; i < fio.Length; i++)
            {
                for (int j = i + 1; j < fio.Length; j++)
                {
                    int MyInt = fio[i].CompareTo(fio[j]);

                    if (param == 1 && MyInt > 0)
                    { //по возрастанию
                        temp = fio[i];
                        fio[i] = fio[j];
                        fio[j] = temp;
                    }
                    else if (param == 2 && MyInt < 0)
                    { //по убыванию
                        temp = fio[i];
                        fio[i] = fio[j];
                        fio[j] = temp;
                    }
                }
            }
            return fio;
        }

        static void SortList(int number)
        {
            Console.WriteLine();
            switch (number)
            {
                case 1:
                    Console.WriteLine("Сортировка по возрастанию:");
                    break;
                case 2: 
                    Console.WriteLine("Сортировка по убыванию:");
                    break;
            }
            Sort(listfio, number);

            foreach (string fio in listfio)
            { Console.WriteLine(fio); }
        }
    }
   

    class NumberReader
    {
        public delegate void NumberEnterDelegat(int number);
        public event NumberEnterDelegat NumberEnterEvent;

       public void Read()
        {
            Console.WriteLine();
            Console.WriteLine("Введите 1 для сортировки А-Я, либо 2 для сортировки Я-А");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num != 1 && num != 2) throw new MyException();

            NumberEntered(num);
           
        }
        protected virtual void NumberEntered(int number)
        {
            NumberEnterEvent?.Invoke(number);
        }
    }
}

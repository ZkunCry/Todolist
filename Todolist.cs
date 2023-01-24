using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Todolist
{
    interface ITodoList
    {
        void Print();
        void DeleteTask(int pos);
        void CreateTask(in Tasks task);
        public List<Tasks> List { get; }

        public string Name { get; }

    }

    
    class Todolist : Tasks, ITodoList
    {

        private List<Tasks> todolists;
        private List<SubTask> sublist;
        private string name;
        public string Name { get; }
        public List<Tasks> List
        {
            get => todolists;
        }

        public Todolist() : base()
        {
            todolists = null;
            sublist = null;
            name = null;
        }
        public Todolist(string name, Tasks newtask)
        {
            this.name = name;
            todolists = new List<Tasks>
                {
                    newtask
                };

        }
        public void CreateTask(in Tasks task) => todolists?.Add(task);

        public override void Print()
        {
            try
            {
                if (todolists is null)
                    throw new Exception("List is null");
                else if (todolists.Count == 0)
                    Console.WriteLine("List is empty\n");
                else
                {
                    int index = 1;
                    Console.WriteLine($"============Todolist============\n\nName: { name}\nTasks:");
                    foreach (var item in todolists)
                    {

                        if (item.finish == true)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{index}.");
                        index++;
                        item.Print();
                        Console.ResetColor();

                    }
                    if (sublist?.Count > 0 && sublist != null)
                    {
                        Console.WriteLine("\n============SubTask============\n");
                        foreach (var item in sublist)
                        {
                            if (item.finish == true)
                                Console.ForegroundColor = ConsoleColor.Green;
                            else
                                Console.ForegroundColor = ConsoleColor.Red;
                            item.Print();
                            Console.ResetColor();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteTask(int pos)
        {

            try
            {
                pos--;
                todolists?.RemoveAt(pos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateSubtask(int number, string text)
        {
            if (number > todolists.Count)
                Console.WriteLine("Out of range");
            else
            {
                sublist = new List<SubTask> { new SubTask(number, text) };
            }
        }
        public void SetAccept(int number)
        {
            if (number > todolists.Count || number < 0)
                Console.WriteLine("Out of range");
            else
            {
                number--;
                todolists[number].finish = true;
            }
        }
     
        public static void Init ()
        {
            ConsoleKeyInfo key;
            Todolist list= null;
            Console.WriteLine("Please, enter your name: ");
            string Name = Console.ReadLine();
            
            do 
            {
               
                Console.WriteLine($"============Todolist============\n\nName: { Name}\n");
                Console.WriteLine("==============Menu==============\n1.Create list(alloc memory)\n2.Create task\n" +
                    "3.Delete task from list\n4.Create subtask\n5.Set task status to completed\n");
                key = Console.ReadKey(true);
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.D1: //Init list
                        if (list == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter your task: ");
                            list = new Todolist(Name, new Tasks(Console.ReadLine()));
                        }
                        else
                            Console.WriteLine("List already created");
                        break;
                    case ConsoleKey.D2: //Add task in list
                        Console.Clear();
                        Console.WriteLine("Enter your task:");
                        list?.CreateTask(new Tasks(Console.ReadLine()));
                        break;
                    case ConsoleKey.D3:
                        if (list == null)
                        {
                            Console.WriteLine("Your list is not init, please init ");
                            break;
                        }
                        else if (list?.List?.Count == 0)
                        {
                            Console.WriteLine("Your list is empty, please add task");
                            break;
                        }
                        int number;
                        Console.WriteLine("Your list:\n");
                        list.Print();
                        Console.WriteLine("Please, enter a number task:\n");
                        number =int.Parse( Console.ReadLine());
                        if (number <=list.List.Count && number >0)
                            list.DeleteTask(number);
                        break;
                    case ConsoleKey.D4:

                    case ConsoleKey.D6:
                        Console.Clear();
                        list?.Print();
                        break;

                }
            } while ( key.Key !=ConsoleKey.Escape);

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Todolist.Init();
        }
    }
}

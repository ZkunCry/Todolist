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
        void Delete(int IndexTask, byte type, int IndexSubTask = 0);
        void Add(in Tasks task);
        public List<Tasks> List { get; }

        public string Name { get; }

    }


    public class Todolist :ITodoList
    {

        private List<Tasks> todolists;

        private string name;
        private int count;


        private const byte DELETESUB = 2;
        private const byte DELETETASK = 1;

        public int Count { get => count; }
        public string Name { get; }
        public List<Tasks> List { get => todolists; }

        public Todolist() : base()
        {
            todolists = null;
            name = null;
            count = 0;
        }
        public Todolist(string name, in Tasks newtask)
        {
            this.name = name;
            todolists = new List<Tasks>
                {
                    newtask
                };
            count++;
        }
        public void Add(in Tasks task)
        {
            if (task is not null)
                todolists?.Add(task);
        }
        public void Add(params Tasks[] tasks)
        {
            if (tasks is not null)
                foreach(var value_tasks in tasks)
                    todolists?.Add(value_tasks);
        }

        private static void ChangeColorConsole(Tasks tasks)
        {
            if (tasks.Finish == true)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
        }
        public  void Print()
        {
            try
            {
                if (todolists is null)
                    Console.WriteLine("List is null");
                else if (count == 0)
                    Console.WriteLine("List is empty\n");
                else
                {
                    int index = 1;
                    Console.WriteLine($"============Todolist============\n\nName: {name}\nTasks:");
                    foreach (var item in todolists)
                    {
                        ChangeColorConsole(item);
                        Console.Write($"{index}.");
                        index++;
                        item.Print();
                        if (item.Subtasklist.Count > 0)
                        {
                            Console.ResetColor();
                            Console.WriteLine("\t\t\t\tSubTasks");
                            ChangeColorConsole(item);
                            foreach (var value in item.Subtasklist)
                                value.Print();
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Delete(int IndexTask,byte type,int IndexSubTask = 0)
        {
            if (IndexTask == 0)
            {
                Console.WriteLine("Error!");
                return;
            }
            if (type == DELETETASK && IndexSubTask == 0)
            {
                if (_checkposition(IndexTask, type))
                    DeleteTask(--IndexTask);
            }
            else if (type == DELETESUB && IndexTask > 0)
            {
                if (_checkposition(IndexSubTask, type))
                    DeleteSubTask(--IndexTask, --IndexSubTask);
            }
            else
                Console.WriteLine("Error! Incorrect data");

            
        }
        private void DeleteTask(int IndexTask) => todolists?.RemoveAt(IndexTask);
        private void DeleteSubTask(int IndexTask, int IndexSub) => todolists[IndexTask]?.Subtasklist?.RemoveAt(IndexSub);
        public void SetAccept(int number)
        {
            if (todolists == null || number > todolists?.Count || number < 0)
                Console.WriteLine("Out of range");
            else
            {
                number--;
                todolists[number].Finish = true;
                if (todolists[number].Subtasklist.Count > 0)
                    foreach (var item in todolists[number].Subtasklist)
                        item.Finish = true;
            }
        }
        private bool _checkposition(int pos, byte identify)
        {

            if (pos > 0 && pos <= Count && Count !=0 &&identify == DELETETASK )
                return true;
            else if (pos > 0 && pos <= todolists[pos].Subtasklist.Count && 
                todolists[pos].Subtasklist.Count !=0 && identify == DELETESUB)
                return true;
            else return false;
        }
        private static void Menu(string name)
        {
            Console.WriteLine($"\n\n============Todolist============\n\nName: { name}\n");
            Console.WriteLine("==============Menu==============\n1.Create list(alloc memory)\n2.Create task\n" +
                "3.Delete task from list\n4.Create subtask\n5.Set task status to completed\n" +
                "6.Display the list\n7.Change your name\n");
        }

        public static void Init()
        {
            ConsoleKeyInfo key;
            Todolist list = null;
            Console.WriteLine("Please, enter your name: ");
            string Name = Console.ReadLine();

            do
            {
                Menu(Name);
                key = Console.ReadKey(true);
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        if (list == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter your task: ");
                            list = new Todolist(Name, new Tasks(Console.ReadLine()));
                        }
                        else
                            Console.WriteLine("List already created");
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("Enter your task:");
                        list?.Add(new Tasks(Console.ReadLine()));
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
                        number = int.Parse(Console.ReadLine());
                        if (number <= list.List.Count && number > 0)
                            list.DeleteTask(number);
                        break;

                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.WriteLine("Enter number task:");
                        byte _numbertask = byte.Parse(Console.ReadLine());
                        _numbertask--;
                        Console.WriteLine("Enter your subtask:");
                        list.todolists[_numbertask].AddSubTask(Console.ReadLine());
                        Console.WriteLine("Your new list:\n");
                        list.Print();
                        break;

                    case ConsoleKey.D5:
                        Console.WriteLine("Enter number task:");
                        list?.SetAccept(byte.Parse(Console.ReadLine()));
                        break;

                    case ConsoleKey.D6:
                        Console.Clear();
                        list?.Print();
                        break;
                    case ConsoleKey.D7:
                        Console.WriteLine("Enter your new name:");
                        Name = Console.ReadLine();
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
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



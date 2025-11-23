using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
class Program
{
    public class Contractor(string name, int number, DateTime startDate)
    {
        public string Name { get; set; } = name;
        public int Number { get; set; } = number;
        public DateTime StartDate { get; set; } = startDate;
    }


    public class Subcontractor : Contractor
    {
        public int Shift { get; set; }
        public double HourlyPay { get; set; }


        public Subcontractor(int shift, double hourlyPay, string name, int number, DateTime startDate) : base(name, number, startDate)
        {
            if (shift != 1 && shift != 2)
            {
                Console.WriteLine("Enter 1 for the day shift and 2 for the night shift.");
                return;
            }
            if (hourlyPay <= 0)
            {
                Console.WriteLine("Hourly pay cannot be negative.");
                return;
            }
            Shift = shift;
            HourlyPay = hourlyPay;
        }


        public double CalculatePay(double hoursWorked)
        {
            double multiplayer = 1.00;
            if (Shift != 1)
            {
                multiplayer = 1.03;
            }
            double result = HourlyPay * hoursWorked * multiplayer;
            return result;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}\nNumber: {Number}\nStart date: {StartDate.Date}\nShift: {Shift}\nHourly pay: {HourlyPay}");
        }


    }


    public static int GetNumberInput()
    {
        int number;
        while (true)
        {
            string? numberRaw = Console.ReadLine();
            if (!int.TryParse(numberRaw, out number))
            {
                Console.WriteLine("Enter the number.");
            }
            else
            {
                break;
            }
        }
        return number;
    }


    public static double GetDoubleInput()
    {
        double numberFloat;
        while (true)
        {
            string? doubleRaw = Console.ReadLine();
            if (!double.TryParse(doubleRaw, out numberFloat))
            {
                Console.WriteLine("Enter the number with floating point.");
            }
            else
            {
                break;
            }
        }
        return numberFloat;
    }

    public static string GetNotNullInput(string hint)
    {
        string? s;
        while (true)
        {
            s = Console.ReadLine();
            if (string.IsNullOrEmpty(s))
            {
                Console.WriteLine($"Enter the {hint}.");
            }
            else
            {
                break;
            }
        }

        return s;
    }


    public static Subcontractor CreateNewSubcontractor()
    {


        Console.WriteLine("Follow the current instructions to create a subcontractor.");
        Console.WriteLine("Enter the name of the subcontractor.");
        string name = GetNotNullInput("name");
        Console.WriteLine("Enter the number of subcontractor.");
        int number = GetNumberInput();
        Console.WriteLine("Enter the start date of the subcontractor.");
        DateTime startDate = DateTime.Parse(GetNotNullInput("date in format: dd/mm/yyyy."));
        Console.WriteLine("Enter the shift: 1 for the day shift and 2 for the night shift.");
        int shift = GetNumberInput();
        Console.WriteLine("Enter the hourly pay rate.");
        double hourlyPay = GetDoubleInput();

        return new Subcontractor(shift, hourlyPay, name, number, startDate);

    }

    public static (string, int) GetSearchValues()
    {
        string toRemove = GetNotNullInput("name or number").ToLower();
        int number;
        bool isInteger = int.TryParse(toRemove, out number);

        return (toRemove, number);
    }


    public static void Info()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("Commands available");
        Console.WriteLine("a - Add a new subcontractor.");
        Console.WriteLine("l - Show all subcontractor on the list.");
        Console.WriteLine("r - Remove the subcontractor from the list.");
        Console.WriteLine("c - Calculate the value of the subcontractor pay.");
        Console.WriteLine("q - Stop programm.");
    }


    public static void NotFound(bool found, string succsses)
    {
        if (!found)
        {
            Console.WriteLine("The given subcontractor has not been found on the list.");
        }
        else if (!string.IsNullOrEmpty(succsses))
        {
            Console.WriteLine(succsses);
        }
    }

    static void Main(string[] args)
    {
        List<Subcontractor> subcontractors = new List<Subcontractor>();
        char op = '\0';
        while (op != 'q')
        {
            Info();
            op = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (op)
            {
                case 'a':
                    Subcontractor sub = CreateNewSubcontractor();
                    subcontractors.Add(sub);
                    Console.WriteLine("The subcontractor was added successfully.");
                    break;
                case 'l':
                    Console.WriteLine("------------------");
                    foreach (Subcontractor curr in subcontractors)
                    {
                        curr.PrintInfo();
                        Console.WriteLine("------------------");
                    }
                    break;

                case 'r':
                    Console.WriteLine("To remove a subcontractor, write its name or number.");
                    bool found = false;
                    var (toRemove, toRemoveNumber) = GetSearchValues();
                    foreach (Subcontractor curr in subcontractors)
                    {
                        if (curr.Name.ToLower() == toRemove || curr.Number == toRemoveNumber)
                        {
                            subcontractors.Remove(curr);
                            found = true;
                            break;
                        }
                    }

                    NotFound(found, "The given subcontractor was removed from the list.");
                    
                    break;
                case 'c':
                    Console.WriteLine("To calculate a subcontractor's pay, write their name or number.");
                    found = false;
                    var (toFound, toFindNumber) = GetSearchValues();
                    Console.WriteLine("Enter the number of hours the subcontractor worked.");
                    double hours = GetDoubleInput();

                    foreach (Subcontractor curr in subcontractors)
                    {
                        if (curr.Name.ToLower() == toFound || curr.Number == toFindNumber)
                        {
                            double pay = curr.CalculatePay(hours);
                            Console.WriteLine($"From {curr.StartDate.Date} to {DateTime.Now.Date} with {hours} hours worked, {curr.Name}'s pay is {pay}.");
                            found = true;
                            break;
                        }
                    }

                    NotFound(found, "");
                    break;

            }
        }
    }
}

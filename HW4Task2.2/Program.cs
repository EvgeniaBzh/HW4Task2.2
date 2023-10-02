using System;
using System.Xml.Linq;

abstract class Worker
{
    private string name;
    private string position;
    private int workDay;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Position
    {
        get { return position; }
        set { position = value; }
    }

    public int WorkDay
    {
        get { return workDay; }
        set { workDay = value; }
    }

    public Worker(string name)
    {
        Name = name; 
    }

    public abstract void FillWorkDay();

    public void Call()
    {
        Console.WriteLine($"{Name} is calling.");
    }

    public void WriteCode()
    {
        Console.WriteLine($"{Name} is writing a code");
    }

    public void Relax()
    {
        Console.WriteLine($"{Name} is relaxing.");
    }
}

class Developer: Worker
{
    public Developer(string name) : base(name)
    {
        Position = "Developer";
    }

    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
        WriteCode();
    }
}

class Manager: Worker
{
    private Random random;

    public Manager(string name) : base(name)
    {
        Position = "Manager";
    }

    public override void FillWorkDay()
    {
        int callCount1 = random.Next(1, 11);
        for (int i = 0; i < callCount1; i++)
        {
            Call();
        }
        Relax();
        int callCount2 = random.Next(1, 6);
        for (int i = 0; i < callCount1; i++)
        {
            Call();
        }
    }
}

class Team
{
    private string TeamName { get; set; }
    private List<Worker> Workers { get; } = new List<Worker>();

    public Team(string teamName)
    {
        TeamName = teamName;
    }

    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
    }

    public void DisplayTeamInfo()
    {
        Console.WriteLine($"Team name: {TeamName}");
        Console.WriteLine("The workers:");
        foreach (var worker in Workers)
        {
            Console.WriteLine(worker.Name);
        }
    }

    public void DisplayDetailedTeamInfo()
    {
        Console.WriteLine($"Team name: {TeamName}");
        Console.WriteLine("Information about all the workers:");

        foreach (var worker in Workers)
        {
            Console.WriteLine($"{worker.Name} - {worker.Position} - {worker.WorkDay}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Team team = new Team("Team 1");
        Developer stepan = new Developer("Stepan");
        Worker maria = new Manager("Maria");
        team.AddWorker(stepan);
        team.AddWorker(maria);

        stepan.WorkDay = 6;
        maria.WorkDay = 7;

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Show the team info");
            Console.WriteLine("2. Show the detailed team info");
            Console.WriteLine("3. Add new worker");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    team.DisplayTeamInfo();
                    break;
                case "2":
                    Console.Clear();
                    team.DisplayDetailedTeamInfo();
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Enter the name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Choose the position(developer or manager): ");
                    string position = "without position";

                    while(true)
                    {
                        position = Console.ReadLine();

                        if (position.ToLower() == "developer")
                        {
                            Developer newDeveloper = new Developer(newName);
                            newDeveloper.WorkDay = 8; // Set work hours for the new developer
                            team.AddWorker(newDeveloper);

                            break;
                        }
                        else if (position.ToLower() == "manager")
                        {
                            Manager newManager = new Manager(newName);
                            newManager.WorkDay = 8; // Set work hours for the new manager
                            team.AddWorker(newManager);

                            break;
                        }
                        else
                        {
                            Console.Write("Position not found. Try again: ");
                            continue;
                        }
                        
                    }

                    Console.WriteLine($"{position} {newName} is added.");
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Incorrect input, try again.");
                    break;
            }

            Console.WriteLine("Press Enter, to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
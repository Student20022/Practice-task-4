using System;

public class Child
{
    public int ChildNumber { get; }

    public Child(int number)
    {
        ChildNumber = number;
    }
}

public delegate void NotPlacesEventHandler(object sender, EventArgs e);

public class PlaySchool
{
    private int capacity;
    private int childrenCount;

    public NotPlacesEventHandler NotPlaces;

    public PlaySchool(int capacity)
    {
        this.capacity = capacity;
        childrenCount = 0;
    }

    public void PushChild(Child child)
    {
        if (childrenCount < capacity)
        {
            childrenCount++;
            Console.WriteLine($"Ребенок {child.ChildNumber} зачислен.");
        }
        else
        {
            NotPlaces?.Invoke(this, EventArgs.Empty);
        }
    }
}

public class Manageress
{
    public void Queue(object sender, EventArgs e)
    {
        Console.WriteLine("Мест нет! Предлагаю встать в очередь.");
    }
}


public class Department
{
    public void Place(object sender, EventArgs e)
    {
        Console.WriteLine("Остальные дети записываются в очередь.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        PlaySchool playSchool = new PlaySchool(5); 
        Manageress manageress = new Manageress();
        Department department = new Department();

        playSchool.NotPlaces += manageress.Queue;
        playSchool.NotPlaces += department.Place;

        for (int i = 1; i <= 6; i++)
        {
            Child child = new Child(i);
            playSchool.PushChild(child);
        }

        Console.ReadLine();
    }
}

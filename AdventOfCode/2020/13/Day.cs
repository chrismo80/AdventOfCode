namespace AdventOfCode2020;
using Extensions;
public static class Day13
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/13/Input.txt");

        var arrival = int.Parse(input[0]);

        var buses = input[1].Split(',').Select((Id, Lane) => (Id, Lane)).Where(bus => bus.Id != "x")
            .Select(bus => (Id: int.Parse(bus.Id), bus.Lane)).ToList();

        var (id, time) = buses.Select(bus => (bus.Id, Time: (arrival / bus.Id * bus.Id) + bus.Id))
            .OrderBy(departure => departure.Time).First();

        long timestamp = 0;
        var match = buses.Where(bus => (timestamp + bus.Lane) % bus.Id == 0);

        while (match.Count() < buses.Count)
        {   // ids are primes: stepsize can be equal to lcm: in prime case = product
            timestamp += match.Product(bus => (long)bus.Id);
            match = buses.Where(bus => (timestamp + bus.Lane) % bus.Id == 0);
        }

        Console.WriteLine($"Part 1: {(time - arrival) * id}, Part 2: {timestamp}");
    }
}
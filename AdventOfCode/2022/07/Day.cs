namespace AdventOfCode2022;
public static class Day7
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/07/Input.txt");

        var dir = new List<string>();
        var files = new List<(string Dir, string Name, int Size)>();

        foreach (var line in input.Where(l => l != "$ ls"))
        {
            if (line == "$ cd ..")
                dir.Remove(dir.Last());
            else if (line.StartsWith("$ cd"))
                dir.Add(line.Split(" ")[2]);
            else if (line.StartsWith("dir"))
                files.Add((string.Join("/", dir) + "/" + line.Split(" ")[1], "-", 0));
            else
                files.Add((string.Join("/", dir), line.Split(" ")[1], int.Parse(line.Split(" ")[0])));
        }

        var folders = files.Select(f => f.Dir).Distinct().Order()
            .Select(d => (Name: d, Size: files.Where(f => f.Dir.StartsWith(d)).Sum(f => f.Size)))
            .ToArray();

        //foreach (var f in folders.OrderByDescending(f => f.Size)) Console.WriteLine(f);

        var result1 = folders.Where(f => f.Size < 100000).Sum(f => f.Size);
        var result2 = folders.Where(f => f.Size > folders[0].Size - 40000000).OrderByDescending(f => f.Size);

        int largestFolderSize = folders[0].Size;

        foreach (var f in folders)
        {
            if (largestFolderSize - f.Size >= 30000000)
            {
                Console.WriteLine($"The smallest directory that, if deleted, would free up enough space is {f.Name} with size {f.Size}.");
                break;
            }
        }

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2.Last().Size}");
    }
}
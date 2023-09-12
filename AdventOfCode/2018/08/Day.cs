namespace AdventOfCode2018;
public static class Day8
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/08/Input.txt")[0]
            .Split(' ').Select(int.Parse).ToArray();

        int pointer = 0;
        var root = new Node(input, ref pointer);

        Console.WriteLine($"Part 1: {root.MetadataSum}, Part 2: {root.Value}, Part 3: {root.Count}");
    }

    public class Node
    {
        public List<int> Metadata = new();
        public List<Node> Children = new();
        public int Count => Children.Count + Children.Sum(c => c.Count);
        public int MetadataSum => Metadata.Sum() + Children.Sum(c => c.MetadataSum);
        public int Value => !Children.Any() ? Metadata.Sum() :
            Metadata.Where(m => m - 1 < Children.Count).Sum(m => Children[m - 1].Value);

        public Node(int[] data, ref int i)
        {
            // read first 2 numbers for children count and meta data count
            int c = data[i++];
            int m = data[i++];

            // create as many children as in c detected
            while (c-- > 0)
                Children.Add(new Node(data, ref i));

            // add meta data
            Metadata = data.Skip(i).Take(m).ToList();

            // move pointer manually
            i += m;
        }
    }
}
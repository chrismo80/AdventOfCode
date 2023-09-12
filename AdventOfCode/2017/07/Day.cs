namespace AdventOfCode2017
{
    public static class Day7
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/07/Input.txt").Select(row =>
            (
                Name: row.Split(" (")[0],
                Weight: int.Parse(row.Split('(', ')')[1]),
                Children: row.Split(" -> ").Length > 1 ? row.Split(" -> ")[1].Split(", ") : null
            ));

            var discs = input.Select(disc => new Disc { Name = disc.Name, Weight = disc.Weight })
                .ToDictionary(disc => disc.Name);

            foreach (var (Name, Weight, Children) in input.Where(disc => disc.Children != null))
            {
                foreach (var child in Children!)
                {
                    discs[child].Parent = discs[Name];
                    discs[Name].Children.Add(discs.Values.Single(d => d.Name == child));
                }
            }

            var unbalanced = discs.Values.Where(disc => !disc.Balanced)
                .OrderBy(disc => disc.Parents).Last().Children
                .OrderBy(child => child.TotalWeight);

            var diff = unbalanced.Last().TotalWeight - unbalanced.First().TotalWeight;

            Console.WriteLine(discs.Values.Single(disc => disc.Parent == null).Name);
            Console.WriteLine(unbalanced.Last().Weight - diff);
        }

        public class Disc
        {
            public string Name = "";
            public List<Disc> Children = new();
            public Disc? Parent;
            public int Weight;
            public bool Balanced => !Children.Any() || Children.Select(c => c.TotalWeight).Distinct().Count() == 1;
            public int TotalWeight => Weight + Children.Sum(c => c.TotalWeight);
            public int Parents => GetParents();

            private int GetParents()
            {
                int c = 0;
                Disc current = this;
                while (current.Parent != null)
                {
                    current = current.Parent;
                    c++;
                }
                return c;
            }
        }
    }
}
namespace AdventOfCode2019;
public static class Day8
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2019/08/Input.txt")[0];

        const int width = 25, height = 6;

        var layers = input.Chunk(width * height);

        var image = new char[width * height];

        foreach (var layer in layers.Reverse())
            for (int i = 0; i < image.Length; i++) image[i] = layer[i] != '2' ? layer[i] : image[i];

        for (int i = 0; i < image.Length; i++)
        {
            if (i % width == 0) Console.WriteLine();
            Console.Write(image[i] == '1' ? '#' : ' ');
        }
        Console.WriteLine();
    }
}
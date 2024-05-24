namespace Mocking;

using Moq;
using NSubstitute;
using NSubstitute.Exceptions;

public static class Main
{
    public static void Moq()
    {
        var parameter = new Dictionary<string, object>();

        var sut = new Mock<IDevice>();

        sut.Setup(d => d.GetParameterValue(It.IsAny<string>()))
            .Returns<string>((name) => parameter[name]);

        sut.Setup(d => d.SetParameterValue(It.IsAny<string>(), It.IsAny<object>()))
            .Callback<string, object>((name, value) => parameter[name] = value);

        sut.Object.SetParameterValue("A", 5);
        sut.Object.SetParameterValue("B", 6);
        sut.Object.SetParameterValue("C", 7);
        sut.Object.SetParameterValue("A", 3);

        foreach (var item in parameter)
            Console.WriteLine($"{item.Key}={item.Value}");

    }

    public static void NSubstitute()
    {
        var parameter = new Dictionary<string, object>();

        var sut = Substitute.For<IDevice>();

        sut.GetParameterValue(Arg.Any<string>())
            .Returns(a => parameter[a.ArgAt<string>(0)]);

        sut.When(d => d.SetParameterValue(Arg.Any<string>(), Arg.Any<object>()))
            .Do(a => parameter[a.ArgAt<string>(0)] = a.ArgAt<object>(1));

        sut.SetParameterValue("A", 5);
        sut.SetParameterValue("B", 6);
        sut.SetParameterValue("C", 7);
        sut.SetParameterValue("A", 3);

        foreach (var item in parameter)
            Console.WriteLine($"{item.Key}={item.Value}");

    }
}

public interface IDevice
{
    public object GetParameterValue(string name);
    public void SetParameterValue(string name, object value);
}
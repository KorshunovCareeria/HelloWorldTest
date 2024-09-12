namespace GenerateAutograding;

internal sealed class TestSet
{
    private readonly string _startupProject;

    public TestSet(string startupProject) => _startupProject = startupProject;

    public IReadOnlyList<Test> Tests => new Test[]
    {
        new DotnetTestGroup("Test1")
        {
            Points = 50
        },
        new DotnetTestGroup("Adder_Sum_GivesSumOfParameters")
        {
            Points = 25
        },
        new DotnetTestGroup("TestStudentPrintsSomethingToConsole")
        {
            Points = 25
        },
    };
}

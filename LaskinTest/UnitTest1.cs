using Laskin;

namespace LaskinTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2+2);

        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(5, 0, 5)]
        [InlineData(0, 5, 5)]
        [Trait("TestGroup", "Adder")]
        public void Adder_Sum_GivesSumOfParameters(int first, int second, int expected)
        {
            // Arrange
            LaskinClass adder = new LaskinClass();

            // Act
            var result = adder.Summ(first, second);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestStudentPrintsSomethingToConsole()
        {
            // Arrange
            using (var sw = new StringWriter())
            {
                // Redirect console output to the StringWriter instance
                Console.SetOut(sw);

                // Set a timeout of 5 seconds for the test execution
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

                try
                {
                    // Act
                    Task task = Task.Run(() => Laskin.Program.Main(new string[0]), cancellationTokenSource.Token);
                    task.Wait(cancellationTokenSource.Token);  // Wait for the task to complete or timeout

                    // Get the output that was written to the console
                    var result = sw.ToString().Trim();

                    // Assert
                    Assert.False(string.IsNullOrEmpty(result), "The program did not print anything to the console.");
                }
                catch (OperationCanceledException)
                {
                    Assert.True(false, "The operation was canceled due to timeout.");
                }
                catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
                {
                    Assert.True(false, "The operation was canceled due to timeout.");
                }
                finally
                {
                    cancellationTokenSource.Dispose();
                }
            }
        }
        }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the first number");
        string? input1 = Console.ReadLine();

        Console.WriteLine("Enter the second number");
        string? input2 = Console.ReadLine();

        try
        {
            int number1 = Convert.ToInt32(input1);
            int number2 = Convert.ToInt32(input2);

            int result = Divide(number1, number2);
            Console.WriteLine($"The result of {number1} divided by {number2} is: {result}");

        }
        catch (FormatException ex)
        {
            Console.WriteLine("Error: One or both of the inputs are not valid integers. (Make sure the input is a number, such as: 1, 11, 12, 13, 23, 232, 213, 999323, 33333).");
            Console.WriteLine($"Detailed error message: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Error: Dibision by zero is not allowed. (The second input should be a non-zero number).");
            Console.WriteLine($"Detailed error message: {ex.Message}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine("Error: One or both of the inputs is too large or too small for 32-bit. Make sure the number is within the range of (-2,147,483,648, 2,147,483,647).");
            Console.WriteLine($"Detailed error message: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred.");
            Console.WriteLine($"Detailed error message: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        static int Divide(int x, int y)
        {
            return x / y;
        }

    }
}
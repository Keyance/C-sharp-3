namespace ToDoList.Test;

public class CalculatorTests
{
    [Fact]
    public void Calculator_Add_ShouldReturnCorrectResult()
    {
        //arrange
        var calculator = new Calculator();

        //act, typicky by měla být jedna akce
        var result = calculator.Add(2, 3);

        //Assert - chci vyhodnotit že je to správná hodnota
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculator_Divide_ThrowsDivisionByZeroException()
    {
        //arrange
        var calculator = new Calculator();

        //act + assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(6, 0));

    }
}


public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return a / b;
    }
}

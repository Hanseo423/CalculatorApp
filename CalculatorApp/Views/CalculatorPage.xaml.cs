
using CalculatorApp;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorApp.Views;

public partial class CalculatorPage : ContentPage
{
	private static double firstNumber = 0;
	private static double secondNumber = 0;
	private static string mathOperator = "";
	private static CurrentState currentState = CurrentState.FirstNumber;
	private static string result = "";
	private static LastOperation lastOperation = LastOperation.None;
	
	
	public CalculatorPage()
	{
		InitializeComponent();
	}

	enum CurrentState
	{
		FirstNumber,
		SecondNumber
	}

	enum LastOperation
	{
		None,
		Arithmetic,
		Percentage
	}

	public void OnButtonClicked(object sender, EventArgs e)
	{
		if (sender is Button button)
		{
			string buttonText = button.Text;
			if (double.TryParse(buttonText, out _))
			{
				HandleNumberInput(buttonText);
			}
			else if (buttonText == "+" || buttonText == "-" || buttonText == "*" || buttonText == "/")
			{
				HandleOperatorInput(buttonText);
				lastOperation = LastOperation.Arithmetic;
                
            }
			else if (buttonText == "%")
			{
				equationLabel.Text = firstNumber.ToString() +" " +"%";
                numbersLabel.Text = firstNumber.ToString() + " " + "%";
                lastOperation = LastOperation.Percentage;
			}

			else if (buttonText == "=")
			{
				HandleEqualButton(buttonText);
			}
			else if(buttonText == "CE" || buttonText == "C")
			{
				RemoveNumbers(buttonText);
			}
            else if (buttonText == "+/-")
            {
                ToggleSign(buttonText);
            }
        }
	}

	public void HandleEqualButton(string buttonText)
	{
		switch (lastOperation)
		{
			case LastOperation.Arithmetic:
				CalculateNumbers(buttonText);
				break;

			case LastOperation.Percentage:
				CalculatePercent(buttonText);
				break;
		}
	}

	public void HandleNumberInput(string buttonText) 
	{
		if (currentState == CurrentState.FirstNumber)
		{
			if (firstNumber == 0)
			{
				firstNumber = double.Parse(buttonText);
			}
			else
			{
				firstNumber = firstNumber * 10 + double.Parse(buttonText);
                
            }
            numbersLabel.Text = firstNumber.ToString();
            equationLabel.Text = firstNumber.ToString();
        }
		else if (currentState == CurrentState.SecondNumber)
		{
			if (secondNumber == 0)
			{
                secondNumber = double.Parse(buttonText);
                

            }
			else
			{
				secondNumber = secondNumber * 10 + double.Parse(buttonText);
                
            }
            equationLabel.Text = firstNumber.ToString() + " " + mathOperator + secondNumber.ToString();
            numbersLabel.Text = secondNumber.ToString();
        }
	}

	public void HandleOperatorInput(string buttonText) 
	{
		if (currentState == CurrentState.FirstNumber)
		{
			mathOperator = buttonText;
			currentState = CurrentState.SecondNumber;

			equationLabel.Text = firstNumber.ToString() + " " + mathOperator;
			numbersLabel.Text = "0";
		}
		else if (currentState == CurrentState.SecondNumber)
		{
            if (buttonText == "=")
            {
                CalculateNumbers(buttonText);
            }
            currentState = CurrentState.FirstNumber;
		}
	}

	public void CalculateNumbers(string buttonText)
	{
		

		switch (mathOperator)
		{
			case "+":
				result = (firstNumber + secondNumber).ToString();
				break;

			case "-":
				result = (firstNumber - secondNumber).ToString();
				break;
			
			case "*":
				result = (firstNumber * secondNumber).ToString();
				break;

			case "/":
				result = (firstNumber / secondNumber).ToString();
				break;


        }
		
		numbersLabel.Text = result;
		equationLabel.Text = firstNumber.ToString() + " " + mathOperator + secondNumber.ToString();

    }

	public void CalculatePercent(string buttonText)
	{
		if (currentState == CurrentState.FirstNumber)
		{
			firstNumber /= 100;
            numbersLabel.Text = firstNumber.ToString();
            equationLabel.Text = firstNumber.ToString() + " " + "%";
        }
	}

	public void RemoveNumbers(string buttonText)
	{    

        if (buttonText == "CE")
		{
			if(currentState == CurrentState.FirstNumber)
			{
				firstNumber = 0;
				numbersLabel.Text = "0";
				equationLabel.Text = "0";
			}
			else if (currentState == CurrentState.SecondNumber)
			{
				secondNumber = 0;
				numbersLabel.Text = "0";
				equationLabel.Text = firstNumber.ToString() + " " + mathOperator;
			}

			
		}
        else if (buttonText == "C")
        {
			firstNumber = 0;
            secondNumber = 0;
			mathOperator = "";
            numbersLabel.Text = "0";
			equationLabel.Text = "";
			currentState = CurrentState.FirstNumber;
			
        }
    }

	public void ToggleSign(string buttonText)
	{
		if (currentState == CurrentState.FirstNumber)
		{
			firstNumber *= -1;
            numbersLabel.Text = firstNumber.ToString();
            equationLabel.Text = firstNumber.ToString();

        }
        else if (currentState == CurrentState.SecondNumber)
        {
            secondNumber *= -1;
            numbersLabel.Text = secondNumber.ToString();
            equationLabel.Text = firstNumber.ToString() + " " + mathOperator + " " + secondNumber.ToString();
        }
    }
	
}
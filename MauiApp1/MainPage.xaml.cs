

using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private List<string> _inputTokens; 
        private double _result;

        public MainPage()
        {
            InitializeComponent();
            _inputTokens = new List<string>();
        }

        
        private void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var digit = button.Text;

            
            if (ResultLabel.Text == "0" || ResultLabel.Text.EndsWith(" ")) 
                ResultLabel.Text = digit; 
            else
                ResultLabel.Text += digit; 
        }

      
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var operation = button.Text;

            if (ResultLabel.Text != "0" && !ResultLabel.Text.EndsWith(" ")) 
            {
                _inputTokens.Add(ResultLabel.Text.Trim()); 
                _inputTokens.Add(operation);
                ResultLabel.Text += " " + operation + " "; 
            }
        }

        
        private void OnEqualClicked(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrWhiteSpace(ResultLabel.Text) && !ResultLabel.Text.EndsWith(" "))
            {
                _inputTokens.Add(ResultLabel.Text.Trim()); 
             
                _result = EvaluateExpression(_inputTokens);
                ResultLabel.Text = _result.ToString();

                _inputTokens.Clear();
            }
        }

      
        private void OnClearClicked(object sender, EventArgs e)
        {
            ResultLabel.Text = "0"; 
            _inputTokens.Clear(); 
        }

       
        private double EvaluateExpression(List<string> tokens)
        {
            
            var newTokens = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "*" || tokens[i] == "/")
                {
                    double left = Convert.ToDouble(newTokens[^1]); 
                    double right = Convert.ToDouble(tokens[++i]); 
                    double intermediateResult = tokens[i - 1] == "*" ? left * right : left / right;
                    newTokens[newTokens.Count - 1] = intermediateResult.ToString();
                }
                else
                {
                    newTokens.Add(tokens[i]); 
                }
            }

            
            double result = Convert.ToDouble(newTokens[0]);
            for (int i = 1; i < newTokens.Count; i++)
            {
                if (newTokens[i] == "+")
                {
                    result += Convert.ToDouble(newTokens[++i]);
                }
                else if (newTokens[i] == "-")
                {
                    result -= Convert.ToDouble(newTokens[++i]);
                }
            }

            return result;
        }
    }
}

using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Pr4.Pages
{
    public partial class SecondPage : Page
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            xTb.Clear();
            mTb.Clear();
            shX.IsChecked = false;
            x2.IsChecked = false;
            eX.IsChecked = false;
            ansTb.Clear();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ansTb.Text = CalculateStatement();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ansTb.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Data Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ansTb.Clear();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ansTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ansTb.Clear();
            }
        }

        private enum FunctionType
        {
            Sh,
            Square,
            Exp
        }

        private string CalculateStatement()
        {
            double x, m;

            if (string.IsNullOrWhiteSpace(xTb.Text) || string.IsNullOrWhiteSpace(mTb.Text))
                throw new ArgumentOutOfRangeException("Please fill in all input fields for x and m.");

            if (!double.TryParse(xTb.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out x) ||
                !double.TryParse(mTb.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out m))
            {
                throw new ArgumentException("Please enter valid numeric values for x and m.");
            }

            FunctionType selectedFunction;
            if (shX.IsChecked == true) selectedFunction = FunctionType.Sh;
            else if (x2.IsChecked == true) selectedFunction = FunctionType.Square;
            else if (eX.IsChecked == true) selectedFunction = FunctionType.Exp;
            else throw new InvalidOperationException("Please select a function (sh(x), x^2, or e^x).");

            double fx = selectedFunction switch
            {
                FunctionType.Sh => Math.Sinh(x),
                FunctionType.Square => x * x,
                FunctionType.Exp => Math.Exp(x),
                _ => throw new ArgumentException("Unknown function type.")
            };

            double result = Calc.Formula2(x, m, fx);

            return result.ToString("F4", CultureInfo.InvariantCulture);
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FirstPage());
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ThirdPage());
        }
    }
}
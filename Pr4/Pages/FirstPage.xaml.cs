using System.Windows;
using System.Windows.Controls;

namespace Pr4.Pages
{
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            xTb.Clear();
            yTb.Clear();
            zTb.Clear();
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
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Division Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ansTb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ansTb.Clear();
            }
        }

        private string CalculateStatement()
        {
            double x, y, z;

            if (string.IsNullOrWhiteSpace(xTb.Text) ||
                string.IsNullOrWhiteSpace(yTb.Text) ||
                string.IsNullOrWhiteSpace(zTb.Text))
            {
                throw new ArgumentOutOfRangeException("Please fill in all input fields for x, y, and z.");
            }

            if (!double.TryParse(xTb.Text, out x) ||
                !double.TryParse(yTb.Text, out y) ||
                !double.TryParse(zTb.Text, out z))
            {
                throw new ArgumentException("Please enter valid numeric values for x, y, and z.");
            }

            if (Math.Abs(x - y) == 0 && (x + y) <= 0)
            {
                throw new ArgumentException("The expression is undefined for x and y values that make the base of the power zero or negative.");
            }

            if (y <= 0)
            {
                throw new ArgumentException("y must be greater than 0 for logarithm.");
            }

            double phi = Calc.Formula1(x, y, z);
            return phi.ToString("F4");
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ThirdPage());
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SecondPage());
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Pr4.Pages
{
    public partial class ThirdPage : Page
    {
        private readonly double X0 = -0.75;
        private readonly double Xk = -2.05;
        private readonly double Dx = -0.2;

        public ThirdPage()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<double> xValues = GenerateXValues();
                BuildGraph(xValues);
                string resultText = "";

                resultText += xValues[0].ToString("F2") + "\n";

                foreach (double x in xValues)
                {
                    double y = CalculateY(x);
                    resultText += y.ToString("F4").Replace('.', ',') + "\n";
                }

                ansTb.Text = resultText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ansTb.Clear();
            xTb.Clear();
        }

        private List<double> GenerateXValues()
        {
            var values = new List<double>();
            double currentX = X0;

            while (currentX >= Xk - 0.0001)
            {
                values.Add(currentX);
                currentX += Dx;
            }

            return values;
        }

        private double CalculateY(double x)
        {
            double term1 = 9 * Math.Pow(x, 4);
            double angleInDegrees = 57.2 + x;
            double angleInRadians = angleInDegrees * Math.PI / 180.0;
            double term2 = Math.Sin(angleInRadians);

            return term1 + term2;
        }

        private void BuildGraph(List<double> xValues)
        {
            GraphCanvas.Children.Clear();
            List<double> yValues = xValues.Select(x => CalculateY(x)).ToList();

            double width = GraphCanvas.ActualWidth;
            double height = GraphCanvas.ActualHeight;

            if (width == 0 || height == 0) return;

            double minX = xValues.Min();
            double maxX = xValues.Max();
            double minY = yValues.Min();
            double maxY = yValues.Max();

            Line xAxis = new Line
            {
                X1 = 40,
                Y1 = height - 20,
                X2 = width,
                Y2 = height - 20,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            GraphCanvas.Children.Add(xAxis);

            Line yAxis = new Line
            {
                X1 = 40,
                Y1 = 0,
                X2 = 40,
                Y2 = height - 20,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            GraphCanvas.Children.Add(yAxis);

            Polyline line = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            for (int i = 0; i < xValues.Count; i++)
            {
                double scaledX = 40 + (xValues[i] - minX) / (maxX - minX) * (width - 60);
                double scaledY = height - 20 - (yValues[i] - minY) / (maxY - minY) * (height - 40);

                Ellipse point = new Ellipse
                {
                    Width = 6,
                    Height = 6,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(point, scaledX - 3);
                Canvas.SetTop(point, scaledY - 3);
                GraphCanvas.Children.Add(point);

                line.Points.Add(new System.Windows.Point(scaledX, scaledY));
            }
            GraphCanvas.Children.Add(line);

            int stepX = Math.Max(1, xValues.Count / 5);
            for (int i = 0; i < xValues.Count; i += stepX)
            {
                double scaledX = 40 + (xValues[i] - minX) / (maxX - minX) * (width - 60);
                TextBlock lblX = new TextBlock
                {
                    Text = xValues[i].ToString("F2"),
                    FontSize = 12
                };
                Canvas.SetLeft(lblX, scaledX - 15);
                Canvas.SetTop(lblX, height - 18);
                GraphCanvas.Children.Add(lblX);
            }

            int stepY = Math.Max(1, yValues.Count / 5);
            for (int i = 0; i <= 5; i++)
            {
                double valY = minY + i * (maxY - minY) / 5;
                double scaledY = height - 20 - (valY - minY) / (maxY - minY) * (height - 40);

                TextBlock lblY = new TextBlock
                {
                    Text = valY.ToString("F2"),
                    FontSize = 12
                };
                Canvas.SetLeft(lblY, 0);
                Canvas.SetTop(lblY, scaledY - 10);
                GraphCanvas.Children.Add(lblY);

                Line tick = new Line
                {
                    X1 = 35,
                    Y1 = scaledY,
                    X2 = 40,
                    Y2 = scaledY,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                GraphCanvas.Children.Add(tick);
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SecondPage());
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FirstPage());
        }
    }
}
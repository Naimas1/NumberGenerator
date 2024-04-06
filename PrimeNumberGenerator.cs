namespace PrimeNumberGenerator
{
    public partial class Form1 : Form
    {
        private Thread fiboThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateFibonacci()
        {
            int a = 0;
            int b = 1;

            while (true)
            {
                // Оновлення відображення в головному потоці
                Invoke(new MethodInvoker(() =>
                {
                    return listBox2.Items.Add(a);
                }));

                int temp = a;
                a = b;
                b = temp + b;

                if (b <= upperBound || upperBound == 0)
                {
                    Thread.Sleep(100); // Затримка для візуального ефекту
                }
                else
                {
                    break;
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxLowerBound.Text, out lowerBound) && int.TryParse(textBoxUpperBound.Text, out upperBound))
            {
                primeThread = new Thread(GeneratePrimes);
                primeThread.IsBackground = true;
                primeThread.Start();

                fiboThread = new Thread(GenerateFibonacci);
                fiboThread.IsBackground = true;
                fiboThread.Start();
            }
            else
            {
                MessageBox.Show("Введіть правильні значення для нижньої та верхньої межі діапазону.");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Form1 form &&
                   EqualityComparer<Thread>.Default.Equals(this.primeThread, form.primeThread);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.upperBound);
        }
    }
}


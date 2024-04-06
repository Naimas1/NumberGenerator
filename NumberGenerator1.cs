namespace PrimeNumberGenerator
{
    public partial class Form1 : Form
    {
        private Thread primeThread;
        private int lowerBound;
        private int upperBound;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void GeneratePrimes()
        {
            int currentNumber = lowerBound <= 2 ? 2 : lowerBound;

            while (true)
            {
                bool isPrime = true;

                // Перевірка чи число є простим
                for (int i = 2; i <= Math.Sqrt(currentNumber); i++)
                {
                    if (currentNumber % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    // Оновлення відображення в головному потоці
                    Invoke(new MethodInvoker(() =>
                    {
                        return listBox1.Items.Add(currentNumber);
                    }));
                }

                if (upperBound != 0 && currentNumber >= upperBound)
                {
                    break;
                }

                currentNumber++;
            }
        }

        private void Invoke(MethodInvoker methodInvoker)
        {
            throw new NotImplementedException();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxLowerBound.Text, out lowerBound) && int.TryParse(textBoxUpperBound.Text, out upperBound))
            {
                primeThread = new Thread(GeneratePrimes);
                primeThread.IsBackground = true; // Потік буде закриватися, якщо головний потік закриється
                primeThread.Start();
            }
            else
            {
                MessageBox.Show("Введіть правильні значення для нижньої та верхньої межі діапазону.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (primeThread != null && primeThread.IsAlive)
            {
                primeThread.Abort();
            }
        }
    }
}

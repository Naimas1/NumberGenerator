using System;
using System.Threading;
using System.Windows.Forms;

namespace PrimeNumberGenerator
{
    public partial class Form1 : Form
    {
        private Thread primeThread;
        private Thread fiboThread;
        private int lowerBound;
        private int upperBound;

        public Form1()
        {
            InitializeComponent();
        }

        private void GeneratePrimes()
        {
            int currentNumber = lowerBound <= 2 ? 2 : lowerBound;

            while (true)
            {
                bool isPrime = true;

                // Check if the number is prime
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
                    // Update the display in the main thread
                    Invoke(new MethodInvoker(() => listBox1.Items.Add(currentNumber)));
                }

                if (upperBound != 0 && currentNumber >= upperBound)
                {
                    break;
                }

                currentNumber++;
            }
        }

        private void GenerateFibonacci()
        {
            int a = 0;
            int b = 1;

            while (true)
            {
                // Update the display in the main thread
                Invoke(new MethodInvoker(() => listBox2.Items.Add(a)));

                int temp = a;
                a = b;
                b = temp + b;

                if (b <= upperBound || upperBound == 0)
                {
                    Thread.Sleep(100); // Delay for visual effect
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
                MessageBox.Show("Enter valid values for lower and upper bound.");
            }
        }

        private void buttonStopPrimes_Click(object sender, EventArgs e)
        {
            if (primeThread != null && primeThread.IsAlive)
            {
                primeThread.Abort();
            }
        }

        private void buttonStopFibo_Click(object sender, EventArgs e)
        {
            if (fiboThread != null && fiboThread.IsAlive)
            {
                fiboThread.Abort();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (primeThread != null && primeThread.IsAlive)
            {
                primeThread.Abort();
            }

            if (fiboThread != null && fiboThread.IsAlive)
            {
                fiboThread.Abort();
            }
        }
    }
}

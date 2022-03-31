using System.Numerics;
using System.Timers;
using Timer = System.Timers.Timer;

Console.Write("How many threads? ");
int numThreads = Convert.ToInt32(Console.ReadLine());

Console.Write("Starting Number? ");
BigInteger startNum = BigInteger.Parse(Console.ReadLine()!);

Thread[] threads = new Thread[numThreads];
BigInteger[] currentNumbers = new BigInteger[numThreads];
int[] stepsArray = new int[numThreads];
for (int i = 0; i < threads.Length; i++)
{
	int ii = i;
	stepsArray[i] = 0;
	currentNumbers[i] = startNum + i;
	threads[i] = new Thread(() => InfinitePersistence(ref currentNumbers[ii], ref stepsArray[ii]))
	{
		Priority = ThreadPriority.Highest, //TODO: do these make a difference?
		IsBackground = false
	};
	threads[i].Start();
}

void OnTimerElapsed(object source, ElapsedEventArgs e) => Print();
Timer printTimer = new(1000);
printTimer.Elapsed += OnTimerElapsed!;
printTimer.Start();

void Print()
{
	string output = "";

	for (int i = 0; i < stepsArray.Length; i++)
		if (stepsArray[i] > 3)
			output += $"Thread {i}: {currentNumbers[i]}\tSteps: {stepsArray[i]}\t";

	if (output != "")
		Console.WriteLine(output);
}

void InfinitePersistence(ref BigInteger bigInt, ref int steps)
{
	while (true)
	{
		Persistence(bigInt, ref steps);
		steps = 0;
		bigInt++;
	}
}

void Persistence(BigInteger bigInt, ref int steps)
{
	string num = Convert.ToString(bigInt)!;

	while (num.Length > 1)
	{
		int[] digits = new int[num.Length];
		for (int i = 0; i < num.Length; i++) digits[i] = (int) Char.GetNumericValue(num[i]);

		BigInteger result = new(1);
		foreach (int j in digits) result *= j;
		num = Convert.ToString(result)!;
		steps += 1;
	}
}
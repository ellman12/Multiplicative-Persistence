using System.Numerics;

byte mode; //0 for default (sequential), 1 for randomized.
byte minRandLength, maxRandLength;

Console.Write("Enter mode. 0 is default (sequential), 1 is randomized: ");
mode = Convert.ToByte(Console.ReadLine());

Console.Write("How many threads? ");
int numThreads = Convert.ToInt32(Console.ReadLine());

Console.Write("Above what value should number of steps be printed? ");
int interestedStepSize = Convert.ToInt32(Console.ReadLine());

BigInteger[] currentNumbers = new BigInteger[numThreads];
Thread[] threads = new Thread[numThreads];
int[] stepsArray = new int[numThreads];

if (mode == 0)
{
	Console.Write("Starting Number? ");
	string startNumStr = Console.ReadLine()!;
	string dividerLine = "----------" + new string('-', startNumStr.Length);
	BigInteger startNum = BigInteger.Parse(startNumStr);

	Console.Write("How often to print current numbers of each thread (in ms)? ");
	int printDelay = Convert.ToInt32(Console.ReadLine());

	for (int i = 0; i < threads.Length; i++)
	{
		int ii = i;
		stepsArray[i] = 0;
		currentNumbers[i] = startNum + i;
		threads[i] = new Thread(() => InfinitePersistence(ref currentNumbers[ii], ref stepsArray[ii], ii))
		{
			Priority = ThreadPriority.Highest,
			IsBackground = false
		};
		threads[i].Start();
	}

	while (true)
	{
		Console.WriteLine($"{DateTime.Now:h:mm:ss tt} Current Numbers: ");
		for (int i = 0; i < currentNumbers.Length; i++)
		{
			Console.WriteLine($"Thread {i}: {currentNumbers[i]}\t");
		}

		Console.WriteLine(dividerLine);
		Thread.Sleep(printDelay);
	}
}
else if (mode == 1)
{
	Console.Write("Enter min random number length: ");
	minRandLength = Convert.ToByte(Console.ReadLine());
	Console.Write("Enter max random number length: ");
	maxRandLength = Convert.ToByte(Console.ReadLine());

	if (minRandLength >= maxRandLength) return;

	for (int i = 0; i < threads.Length; i++)
	{
		int ii = i;
		stepsArray[i] = 0;
		threads[i] = new Thread(() => InfiniteGenAndCheck(ref stepsArray[ii], ii))
		{
			Priority = ThreadPriority.Highest,
			IsBackground = false
		};
		threads[i].Start();
	}
}

//Runs Multiplicative Persistence on a single BigInteger.
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

//What each Thread runs when running in mode 0 (sequential).
void InfinitePersistence(ref BigInteger currentNumber, ref int steps, int threadNum)
{
	while (true)
	{
		Persistence(currentNumber, ref steps);
		currentNumber++;
		if (steps > interestedStepSize) Console.WriteLine($"Thread {threadNum}: {currentNumber}\tSteps: {steps}\t{DateTime.Now.ToString("hh:mm:ss tt")}");
		steps = 0;
	}
}

//What each Thread runs in mode 1. Loops forever generating BigIntegers of length defined at top of file, and runs Persistence() on it each time.
void InfiniteGenAndCheck(ref int steps, int threadNum)
{
	Random random = new();
	while (true)
	{
		BigInteger currentNumber = BigInteger.Abs(GetRandomBigInt(random.Next(minRandLength, maxRandLength)));
		Persistence(currentNumber, ref steps);
		if (steps > interestedStepSize) Console.WriteLine($"Thread {threadNum}: {currentNumber}\tSteps: {steps}\t{DateTime.Now.ToString("hh:mm:ss tt")}");
		steps = 0;
	}
}

//https://stackoverflow.com/a/17357822
//Used for mode 1.
BigInteger GetRandomBigInt(int length)
{
	Random random = new();
	byte[] data = new byte[length];
	random.NextBytes(data);
	return new BigInteger(data);
}
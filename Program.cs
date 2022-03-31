﻿using System.Numerics;

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
	threads[i] = new Thread(() => InfinitePersistence(ref currentNumbers[ii], ref stepsArray[ii], ii))
	{
		Priority = ThreadPriority.Highest, //TODO: do these make a difference?
		IsBackground = false
	};
	threads[i].Start();
}

void InfinitePersistence(ref BigInteger currentNumber, ref int steps, int threadNum)
{
	while (true)
	{
		Persistence(currentNumber, ref steps);
		currentNumber++;
		if (steps > 8) Console.WriteLine($"Thread {threadNum}: {currentNumber}\tSteps: {steps}\t{DateTime.Now.ToString("hh:mm:ss fffffff tt")}");
		steps = 0;
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
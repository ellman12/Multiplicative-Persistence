using System.Numerics;

// Console.Write("How many threads? ");
// int numThreads = Convert.ToInt32(Console.ReadLine());

Console.Write("Starting Number? ");
BigInteger startNum = BigInteger.Parse(Console.ReadLine()!);

void Persistence(BigInteger startNum, ref int steps)
{
   string num = Convert.ToString(startNum)!;

   while (num.Length > 1)
   {
      int[] digits = new int[num.Length];
      for (int i = 0; i < num.Length; i++) digits[i] = (int)Char.GetNumericValue(num[i]);

      BigInteger result = new(1);
      foreach (int j in digits) result *= j;
      num = Convert.ToString(result)!;
      steps += 1;
   }
}

int steps = 0;
Persistence(startNum, ref steps);
Console.WriteLine(steps);
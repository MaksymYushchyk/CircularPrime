using System;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

namespace CircularPrime
{
    class Program
    {
        static SortedSet<int> listOfPrime;
        static void Main(string[] args)
        {
            SearchCircularPrima();
            Console.ReadKey();
        }
        static void SearchCircularPrima()
        {
            DateTime startTime = DateTime.Now;
            listOfPrime = new SortedSet<int>(GetEratosphenesSieve(1000000));
            List<int> list = new List<int>();
            List<int> buffer;
            listOfPrime.Remove(2);
            listOfPrime.Remove(5);
            list.Add(2);
            list.Add(5);
            while (listOfPrime.Count > 0)
            {
                buffer = GetCircularPrimes(listOfPrime.Min);
                if (buffer != null)
                {
                    list.AddRange(buffer);
                }
            }
            listOfPrime = new SortedSet<int>(list);
            Console.WriteLine($"The number of circular primes below 1 million is { listOfPrime.Count}");
            TimeSpan duration = startTime - DateTime.Now;
            //Console.WriteLine($"{ duration.TotalMilliseconds} ms");
        }
        static List<int> GetEratosphenesSieve(int max)
        {
            BitArray isPrime = new BitArray(max+1, true);
            List<int> listOfPrime = new List<int>();
            int upperLimit = (int)Sqrt(max);
            listOfPrime.Add(2);
            int buf = 0;
            for (int i = 3; i <= max; i+=2)
            {
                if (isPrime.Get(i))
                {
                    if (i<=upperLimit)
                    {
                        buf = i * 2;
                        for (int j = i * i; j <= max; j += buf)
                        {
                            isPrime.Set(j, false);
                        }
                    }
                    listOfPrime.Add(i);
                }
            }
            return listOfPrime;
        }
        static List<int> GetCircularPrimes(int primeNumber)
        {
            int digit, number = primeNumber, count = 0, multiplier = 1;
            while (number>0)
            {
                digit = number % 10;
                if (digit%2==0 || digit==5)
                {
                    listOfPrime.Remove(primeNumber);
                    return null;
                }
                number /= 10;
                count++;
                multiplier *= 10;
            }
            multiplier /= 10;
            number = primeNumber;
            List<int> bufferList = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if (listOfPrime.Contains(number))
                {
                    bufferList.Add(number);
                    listOfPrime.Remove(number);
                }
                else
                {
                    if (!bufferList.Contains(number))
                    {
                        return null;
                    }
                }
                digit = number % 10;
                number = digit * multiplier + number / 10;
            }
            return bufferList;
        } 
    }
}

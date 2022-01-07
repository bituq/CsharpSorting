using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SortingAlgorithms.Testing
{
	public class Tests
	{
		const string MergeCategory = "Merge Sort";
		Byte[] _sequence = new byte[32];

		[SetUp]
		public void Setup()
		{
			var random = new Random();

			random.NextBytes(_sequence);
		}

		[Test]
		[Category(MergeCategory)]
		public void SortNumber()
		{
			IEnumerable<Byte> seq = _sequence;
			string res = String.Empty;

			var resList = new List<Byte>(MergeSort<Byte>.Sort(seq));

			bool success = false;

			for (int i = 0; i < resList.Count; i++)
			{
				Byte current = resList[i];
				Byte previous = resList[Math.Abs(i - 1)];

				res += $"{current}\t";

				if (i > 0)
				{
					success = current >= previous;
					Assert.GreaterOrEqual(current, previous);
				}
			}

			
			if (success)
				Assert.Pass(res);
			else
				Assert.Fail(res);
		}

		[Test]
		[Category(MergeCategory)]
		public async Task SortNumberAsync()
		{
			IEnumerable<Byte> seq = _sequence;
			string res = String.Empty;

			var resList = new List<Byte>(await MergeSort<Byte>.SortAsync(seq));

			bool success = false;

			for (int i = 0; i < resList.Count; i++)
			{
				Byte current = resList[i];
				Byte previous = resList[Math.Abs(i - 1)];

				res += $"{current}\t";

				if (i > 0)
				{
					success = current >= previous;
					Assert.GreaterOrEqual(current, previous);
				}
			}

			if (success)
				Assert.Pass(res);
			else
				Assert.Fail(res);
		}
	}
}
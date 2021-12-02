namespace SortingAlgorithms
{
	public static class MergeSortAsync
	{
		static T Min<T>(T first, T last) where T : IComparable => first.CompareTo(last) < 0 ? first : last;
		static byte IsOdd(int n) => Convert.ToByte(n % 2 != 0);

		public static async Task<IEnumerable<T>> SortAsync<T>(IEnumerable<T> sequence) where T : IComparable
		{
			int half = sequence.Count() / 2;

			IEnumerable<T> left = sequence.Take(half);

			if (left.Count() > 1)
				left = await SortAsync(left);

			IEnumerable<T> right = sequence.TakeLast(half + IsOdd(half * 2));

			if (right.Count() > 1)
				right = await SortAsync(right);

			T[] result = new T[sequence.Count()];
			int resultIndex;

			for (resultIndex = 0; left.Any() && right.Any(); resultIndex++)
			{
				result[resultIndex] = Min(left.First(), right.First());

				if (result[resultIndex].Equals(left.First()))
					left = left.TakeLast(left.Count() - 1);
				else
					right = right.TakeLast(right.Count() - 1);
			}

			foreach (T item in left.Count() > 0 ? left : right)
				result[resultIndex++] = item;

			return result;
		}
	}
}

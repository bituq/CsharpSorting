namespace SortingAlgorithms
{
	public static class MergeSort<T> where T : IComparable
	{
		static T Min(T first, T last) => first.CompareTo(last) < 0 ? first : last;

		static byte IsOdd(int n) => Convert.ToByte(n % 2 != 0);

		static IEnumerable<T> SortIfValid(IEnumerable<T> sequence) => sequence.Count() > 1 ? Sort(sequence) : sequence;

		static async Task<IEnumerable<T>> SortIfValidAsync(IEnumerable<T> sequence) => sequence.Count() > 1 ? await SortAsync(sequence) : sequence;

		static T[] DistributeOrdered(IEnumerable<T> left, IEnumerable<T> right)
		{
			T[] res = new T[left.Count() + right.Count()];
			int i;

			for (i = 0; left.Any() && right.Any(); i++)
			{
				res[i] = Min(left.First(), right.First());

				if (res[i].Equals(left.First()))
					left = left.TakeLast(left.Count() - 1);
				else
					right = right.TakeLast(right.Count() - 1);
			}

			foreach (T item in left.Any() ? left : right)
				res[i++] = item;

			return res;
		}

		public static async Task<IEnumerable<T>> SortAsync(IEnumerable<T> sequence)
		{
			int half = sequence.Count() / 2;

			IEnumerable<T> left = await SortIfValidAsync(sequence.Take(half));
			IEnumerable<T> right = await SortIfValidAsync(sequence.TakeLast(half + IsOdd(half * 2)));

			return DistributeOrdered(left, right);
		}

		public static IEnumerable<T> Sort(IEnumerable<T> sequence)
		{
			int half = sequence.Count() / 2;

			IEnumerable<T> left = SortIfValid(sequence.Take(half));
			IEnumerable<T> right = SortIfValid(sequence.TakeLast(half + IsOdd(half * 2)));

			return DistributeOrdered(left, right);
		}
	}
}

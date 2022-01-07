namespace SortingAlgorithms
{
	public static class MergeSort<T> where T : IComparable
	{
		static void Shorten(ref IEnumerable<T> items) => items = items.TakeLast(items.Count() - 1);

		static T Min(T first, T last) => first.CompareTo(last) < 0 ? first : last;

		static byte IsOddAsByte(int n) => Convert.ToByte(n % 2 != 0);

		static IEnumerable<T> SortIfValid(IEnumerable<T> sequence) => sequence.Count() > 1 ? Sort(sequence) : sequence;

		/// <summary>
		/// Determines whether the sequence is still divisible.
		/// </summary>
		/// <param name="sequence"></param>
		/// <returns>The sorted sequence if it's divisible, otherwise the original sequence.</returns>

		static async Task<IEnumerable<T>> SortIfValidAsync(IEnumerable<T> sequence) => sequence.Count() > 1 ? await SortAsync(sequence) : sequence;

		static T[] DistributeOrdered(IEnumerable<T> left, IEnumerable<T> right)
		{
			// Initialize the resulting array
			T[] res = new T[left.Count() + right.Count()];
			int i;

			// Loop while there are still items to compare
			for (i = 0; left.Any() && right.Any(); i++)
			{
				res[i] = Min(left.First(), right.First());

				// Shorten the enumerable with the minimum first value
				Shorten(ref res[i].Equals(left.First()) ? ref left : ref right);
			}

			// Add any remaining items to the resulting array
			foreach (T item in left.Any() ? left : right)
				res[i++] = item;

			return res;
		}

		public static async Task<IEnumerable<T>> SortAsync(IEnumerable<T> sequence)
		{
			int half = sequence.Count() / 2;

			IEnumerable<T> left = await SortIfValidAsync(sequence.Take(half));
			IEnumerable<T> right = await SortIfValidAsync(sequence.TakeLast(half + IsOddAsByte(half * 2)));

			return DistributeOrdered(left, right);
		}

		public static IEnumerable<T> Sort(IEnumerable<T> sequence)
		{
			int half = sequence.Count() / 2;

			IEnumerable<T> left = SortIfValid(sequence.Take(half));
			IEnumerable<T> right = SortIfValid(sequence.TakeLast(half + IsOddAsByte(half * 2)));

			return DistributeOrdered(left, right);
		}
	}
}

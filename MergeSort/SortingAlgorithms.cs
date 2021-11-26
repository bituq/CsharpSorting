namespace SortingAlgorithms
{
	public static class MergeSort
	{
		static T Min<T>(T first, T last) where T : IComparable => first.CompareTo(last) < 0 ? first : last;
		static bool IsEven(int n) => n % 2 == 0;

		public static IEnumerable<T> Sort<T>(IEnumerable<T> sequence) where T : IComparable
		{
			int half = sequence.Count() / 2;
			IEnumerable<T> left = sequence.Take(half);
			IEnumerable<T> right = sequence.TakeLast(half + (IsEven(sequence.Count()) ? 0 : 1));

			if (left.Count() > 1)
				left = Sort(left);
			if (right.Count() > 1)
				right = Sort(right);

			T[] result = new T[sequence.Count()];
			int resultIndex;

			for (resultIndex = 0; left.Count() > 0 && right.Count() > 0; resultIndex++)
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

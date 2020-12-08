using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.HabboDHM.ASUtilities
{
	internal class ASArray<T> : List<T>
	{
		private void EnsureIndex(int index)
		{
			if ((index + 1) > Count)
			{
				InsertRange(Count, Enumerable.Repeat(default(T), index + 1 - Count));
			}
		}

		public new T this[int index]
		{
			get
			{
				EnsureIndex(index);
				return base[index];
			}
			set
			{
				EnsureIndex(index);
				base[index] = value;
			}
		}
	}
}

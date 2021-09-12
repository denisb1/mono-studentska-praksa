using System;
using System.Collections.Generic;
using System.Linq;

namespace day9.API.Pagination
{
	public sealed class PaginatedList<T> : List<T>
	{
		public int PageIndex { get; }
		public int  TotalPages { get; }

		private PaginatedList(IEnumerable<T> items, int count, int index, int size)
		{
			PageIndex = index;
			TotalPages = (int)Math.Ceiling(count / (double)size);
			AddRange(items);
		}

		public bool HasPreviousPage => PageIndex > 1;
		public bool HasNextPage => PageIndex < TotalPages;

		public static PaginatedList<T> Create(IQueryable<T> source, int index, int size)
		{
			var count = source.Count();
			var items = source.Skip((index - 1) * size).Take(size).ToList();
			return new PaginatedList<T>(items, count, index, size);
		}
	}
}

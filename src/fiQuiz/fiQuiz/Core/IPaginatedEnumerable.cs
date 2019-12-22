using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Core
{
    public interface IPaginatedEnumerable : IEnumerable
    {
        int PageIndex { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IPaginatedEnumerable<out T> : IPaginatedEnumerable, IEnumerable<T>
    {

    }
}

namespace Application.Core.Abstractions;

/// <summary>
/// Provides methods for paginating a collection of items.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Retrieves a specific page of items from a given collection.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="source">The collection of items to paginate.</param>
    /// <param name="pageNumber">The page number to retrieve, where 1 represents the first page.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> containing the items on the specified page. 
    /// If the page number is out of range, an empty collection is returned.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="pageNumber"/> or <paramref name="pageSize"/> is less than 1.</exception>
    /// <exception cref="Exception">Thrown when an error occurs during pagination.</exception>
    IEnumerable<T> GetPage<T>(IEnumerable<T> source, int pageNumber, int pageSize);
    /// <summary>
    /// Calculates the total number of pages required to accommodate all items in the collection, given a specific page size.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="source">The collection of items to paginate.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>
    /// The total number of pages needed to display all items in the collection.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="pageSize"/> is less than 1.</exception>
    /// <exception cref="Exception">Thrown when an error occurs during the calculation of total pages.</exception>
    int GetPageTotal<T>(IEnumerable<T> source, int pageSize);
}
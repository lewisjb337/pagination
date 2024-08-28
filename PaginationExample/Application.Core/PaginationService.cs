using Application.Core.Abstractions;

namespace Application.Core;

/// <summary>
/// Provides pagination functionality for collections of items.
/// </summary>
public class PaginationService : IPaginationService
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
    public IEnumerable<T> GetPage<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        try
        {
            // Convert to a list to avoid 'possible multiple enumeration' issue caused by using IEnumerable type
            var enumerable = source.ToList();

            if (enumerable.Count == 0)
            {
                return enumerable;
            }
            
            // Ensure that pageNumber and pageSize are greater than zero to avoid incorrect behavior
            if (pageNumber < 1 || pageSize < 1)
            {
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            // Calculate the number of items to skip
            var skip = (pageNumber - 1) * pageSize;

            // Use LINQ to skip the items and take the desired number of items for the page
            return enumerable.Skip(skip).Take(pageSize);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Failed to paginate data. Exception given: {e}");
        }
    }

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
    public int GetPageTotal<T>(IEnumerable<T> source, int pageSize)
    {
        try
        {
            // Convert to a list to avoid 'possible multiple enumeration' issue caused by using IEnumerable type
            var enumerable = source.ToList();

            // Gets the total number of values from the data we are looking to paginate
            var count = enumerable.Count;

            // This ensures that any remainder after division will cause the division result to round up to the next whole number,
            // divides the adjusted count by the page size to get the total number of pages
            return (count + pageSize - 1) / pageSize;
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Failed to calculate page total from data. Exception given: {e}");
        }
    }
}
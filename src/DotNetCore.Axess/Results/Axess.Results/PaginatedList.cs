using Microsoft.EntityFrameworkCore;

namespace Axess.Dto;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T>
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> Items { get; }
    /// <summary>
    /// 
    /// </summary>
    public int PageNumber { get; }
    /// <summary>
    /// 
    /// </summary>
    public int TotalPages { get; }
    /// <summary>
    /// 
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="count"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// 
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        int count = await source.CountAsync();
        return await CreateAsync(source, count, pageNumber, pageSize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int count, int pageNumber, int pageSize)
    {
        List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
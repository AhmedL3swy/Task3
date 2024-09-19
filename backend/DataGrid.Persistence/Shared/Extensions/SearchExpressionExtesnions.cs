using DataGrid.Application.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataGrid.Persistence.Shared.Extensions
{

    public static class SearchExpressionExtensions
    {
        /// <summary>
        /// Applies keyword search to the query based on the provided search criteria.
        /// </summary>
        /// <typeparam name="DbSet">The type of the DbSet.</typeparam>
        /// <param name="query">The query to apply the keyword search to.</param>
        /// <param name="searchByKeywords">The list of search criteria.</param>
        /// <returns>The modified query with the keyword search applied.</returns>
        public static IQueryable<DbSet> ApplyKeywordSearch<DbSet>(this IQueryable<DbSet> query, List<SearchByKeyword> searchByKeywords)
        {
            // Check if search criteria is provided
            if (searchByKeywords == null || !searchByKeywords.Any())
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(DbSet), "e");
            Expression finalExpression = null;

            foreach (var search in searchByKeywords)
            {
                Expression keywordExpression = null;

                foreach (var field in search.FieldList)
                {
                    Expression propertyExpression = parameter;

                    // Support nested properties (e.g., Category.Name)
                    foreach (var part in field.Split('.'))
                    {
                        propertyExpression = Expression.Property(propertyExpression, part);
                    }

                    // Determine property type to build appropriate expression
                    var propertyType = propertyExpression.Type;

                    if (propertyType == typeof(string))
                    {
                        var keywordConstant = Expression.Constant(search.Keyword);
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var containsExpression = Expression.Call(propertyExpression, containsMethod, keywordConstant);

                        // Combine multiple field searches with OR ||
                        keywordExpression = keywordExpression == null
                            ? containsExpression
                            : Expression.OrElse(keywordExpression, containsExpression);
                    }
                    else if (propertyType == typeof(DateTime) || propertyType == typeof(DateOnly))
                    {
                        // Parse the keyword into DateOnly/DateTime
                        DateTime parsedDate;
                        if (!DateTime.TryParse(search.Keyword, out parsedDate))
                        {
                            throw new InvalidCastException($"Cannot parse '{search.Keyword}' to a valid {propertyType.Name}.");
                        }

                        Expression dateConstant = propertyType == typeof(DateOnly)
                            ? Expression.Constant(DateOnly.FromDateTime(parsedDate))
                            : Expression.Constant(parsedDate);

                        // Use Equals method for comparison
                        var equalsExpression = Expression.Equal(propertyExpression, dateConstant);

                        // Combine multiple field searches with OR
                        keywordExpression = keywordExpression == null
                            ? equalsExpression
                            : Expression.OrElse(keywordExpression, equalsExpression);
                    }
                    else if (propertyType.IsValueType)
                    {
                        // For other value types (int, decimal, etc.), use Equals method
                        var keywordConstant = Expression.Constant(Convert.ChangeType(search.Keyword, propertyType));
                        var equalsExpression = Expression.Equal(propertyExpression, keywordConstant);

                        // Combine multiple field searches with OR
                        keywordExpression = keywordExpression == null
                            ? equalsExpression
                            : Expression.OrElse(keywordExpression, equalsExpression);
                    }
                    else
                    {
                        // Add custom logic for other types if necessary
                        throw new NotSupportedException($"Search by field '{field}' with type '{propertyType}' is not supported.");
                    }
                }

                // Combine different SearchByKeyword with AND
                finalExpression = finalExpression == null
                    ? keywordExpression
                    : Expression.AndAlso(finalExpression, keywordExpression);
            }

            if (finalExpression != null)
            {
                var lambda = Expression.Lambda<Func<DbSet, bool>>(finalExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        /// <summary>
        /// Applies range search to the query based on the provided search criteria.
        /// </summary>
        /// <typeparam name="DbSet">The type of the DbSet.</typeparam>
        /// <param name="entities">The query to apply the range search to.</param>
        /// <param name="rangeSearches">The list of range search criteria.</param>
        /// <returns>The modified query with the range search applied.</returns>
        public static IQueryable<DbSet> ApplyRangeSearch<DbSet>(this IQueryable<DbSet> entities, List<RangeSearch> rangeSearches)
        {
            // Check if range search criteria is provided
            if (rangeSearches == null || !rangeSearches.Any()) return entities;

            foreach (var rangeSearch in rangeSearches)
            {
                // Start building the expression with the base parameter
                var parameter = Expression.Parameter(typeof(DbSet), "x");
                Expression propertyAccess = parameter;

                // Support nested properties (e.g., Category.Price or OrderDetails.OrderDate)
                foreach (var part in rangeSearch.Field.Split('.'))
                {
                    propertyAccess = Expression.Property(propertyAccess, part);
                }

                var propertyType = propertyAccess.Type;

                Expression minExpression = null;
                Expression maxExpression = null;

                if (propertyType == typeof(DateTime) || propertyType == typeof(DateOnly))
                {
                    // Handle DateTime and DateOnly ranges
                    DateTime minValue;
                    DateTime maxValue;

                    if (!DateTime.TryParse(rangeSearch.Start.ToString(), out minValue) ||
                        !DateTime.TryParse(rangeSearch.End.ToString(), out maxValue))
                    {
                        throw new InvalidCastException($"Cannot parse range values for field '{rangeSearch.Field}' as {propertyType.Name}.");
                    }

                    minExpression = propertyType == typeof(DateOnly)
                        ? Expression.Constant(DateOnly.FromDateTime(minValue))
                        : Expression.Constant(minValue);

                    maxExpression = propertyType == typeof(DateOnly)
                        ? Expression.Constant(DateOnly.FromDateTime(maxValue))
                        : Expression.Constant(maxValue);
                }
                else if (propertyType.IsValueType)
                {
                    // Handle numeric and other value types
                    var minValue = Convert.ChangeType(rangeSearch.Start, propertyType);
                    var maxValue = Convert.ChangeType(rangeSearch.End, propertyType);

                    minExpression = Expression.Constant(minValue);
                    maxExpression = Expression.Constant(maxValue);
                }
                else
                {
                    throw new NotSupportedException($"Range search for field '{rangeSearch.Field}' with type '{propertyType}' is not supported.");
                }

                // Create expressions for GreaterThanOrEqual and LessThanOrEqual
                var greaterThanOrEqual = Expression.GreaterThanOrEqual(propertyAccess, minExpression);
                var lessThanOrEqual = Expression.LessThanOrEqual(propertyAccess, maxExpression);

                // Combine both conditions with AND
                var andExpression = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
                var lambda = Expression.Lambda<Func<DbSet, bool>>(andExpression, parameter);

                // Apply the range filter to the query
                entities = entities.Where(lambda);
            }

            return entities;
        }

        /// <summary>
        /// Applies sorting to the query based on the provided sort criteria.
        /// </summary>
        /// <typeparam name="DbSet">The type of the DbSet.</typeparam>
        /// <param name="entities">The query to apply the sorting to.</param>
        /// <param name="sortBy">The field to sort by.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>The modified query with the sorting applied.</returns>
        public static IQueryable<DbSet> ApplySorting<DbSet>(this IQueryable<DbSet> entities, string sortBy, SortDirection sortDirection)
        {
            // Check if sortBy field is provided
            if (string.IsNullOrEmpty(sortBy)) return entities;

            var parameter = Expression.Parameter(typeof(DbSet), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda(property, parameter);

            var orderByMethod = sortDirection == SortDirection.Ascending
                ? nameof(Queryable.OrderBy)
                : nameof(Queryable.OrderByDescending);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                orderByMethod,
                new Type[] { typeof(DbSet), property.Type },
                entities.Expression,
                lambda
            );

            return entities.Provider.CreateQuery<DbSet>(resultExpression);
        }

        /// <summary>
        /// Applies includes to the query based on the provided include fields.
        /// </summary>
        /// <typeparam name="DbSet">The type of the DbSet.</typeparam>
        /// <param name="entities">The query to apply the includes to.</param>
        /// <param name="includeFields">The comma-separated list of include fields.</param>
        /// <returns>The modified query with the includes applied.</returns>
        public static IQueryable<DbSet> ApplyIncludes<DbSet>(this IQueryable<DbSet> entities, string includeFields) where DbSet : class
        {
            // Check if include fields are provided
            if (string.IsNullOrEmpty(includeFields)) return entities;

            foreach (var include in includeFields.Split(','))
            {
                entities = entities.Include(include);
            }

            return entities;
        }

        /// <summary>
        /// Applies paging to the query based on the provided page number and page size.
        /// </summary>
        /// <typeparam name="DbSet">The type of the DbSet.</typeparam>
        /// <param name="entities">The query to apply the paging to.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The modified query with the paging applied.</returns>
        public static IQueryable<DbSet> ApplyPaging<DbSet>(this IQueryable<DbSet> entities, int pageNumber, int pageSize)
        {
            return entities.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}

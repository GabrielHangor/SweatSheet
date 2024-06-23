using AutoMapper;

namespace SweatSheet.Server.Modules.Shared.Generics.PaginatedList;

public static class PaginatedListExtensions
{
    public static PaginatedList<TDestination> Map<TSource, TDestination>(
        this PaginatedList<TSource> source,
        IMapper mapper
    )
    {
        var items = mapper.Map<List<TDestination>>(source.Items);

        return new PaginatedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}

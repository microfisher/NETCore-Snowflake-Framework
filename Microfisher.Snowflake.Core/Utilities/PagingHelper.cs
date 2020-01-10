using System;
namespace Microfisher.Snowflake.Core.Utilities
{
    public static class PagingHelper
    {
        public static long PageIndex { get; set; } = 1;

        public static long PageSize { get; set; } = 50;

        public static (long PageIndex, long PageSize, long PageCount) GetPageInfo(long pageIndex, long pageSize, long recordCount)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            if (pageSize > 100 || pageSize <= 0)
            {
                pageSize = PageSize;
            }

            var pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);

            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            return (PageIndex: pageIndex, PageSize: pageSize, PageCount: pageCount);
        }

    }
}

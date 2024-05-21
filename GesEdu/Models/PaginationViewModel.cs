namespace GesEdu.Models
{
    public class PaginationViewModel
    {
        private const int _maxNumberOfWidgetPages = 8;

        public PaginationViewModel(int pageIndex, int pageSize, int totalCount, string ajaxRequestMethod)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (decimal)PageSize);
            AjaxRequestMethod = ajaxRequestMethod;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string AjaxRequestMethod { get; set; }
        public int MaxNumberOfWidgetPages { get { return _maxNumberOfWidgetPages % 5 == 0 ? _maxNumberOfWidgetPages : _maxNumberOfWidgetPages / 5 * 5; }  }
    }
}

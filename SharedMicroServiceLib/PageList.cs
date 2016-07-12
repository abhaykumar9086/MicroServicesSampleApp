using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicroServiceLib
{
    public class PagedList<T> where T : class
    {
        private List<T> _orgList;
        private int _noPages;
        private int _pageSize;
        private int _minPage = 1;
        private int _lastPage = 1;

        public PagedList(List<T> tlist, int pageSize)
        {

            _orgList = tlist;
            _pageSize = pageSize;
            if (tlist.Count % _pageSize == 0)
            {
                _noPages = tlist.Count / _pageSize;
            }
            else
            {
                _noPages = tlist.Count / _pageSize + 1;
            }
        }

        public int CurrentPage { get { return _lastPage; } set { _lastPage = value; } }
        public int NoPages { get { return _noPages; } }
        /// <summary>
        /// Method:GetListFromPage
        /// Purpose:Returns subset based on page index.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public List<T> GetListFromPage(int pageNo)
        {
            int pageCount = _pageSize;
            if (pageNo < _minPage)
            {
                pageNo = _minPage;
            }
            if (pageNo >= _noPages)
            {
                pageNo = _noPages;
                pageCount = _orgList.Count - (_pageSize * (pageNo - 1));
            }
            _lastPage = pageNo;
            return _orgList.Skip(_pageSize * (pageNo - 1)).Take(pageCount).ToList<T>();
        }

    }
}

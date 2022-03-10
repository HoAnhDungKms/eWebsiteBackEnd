using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
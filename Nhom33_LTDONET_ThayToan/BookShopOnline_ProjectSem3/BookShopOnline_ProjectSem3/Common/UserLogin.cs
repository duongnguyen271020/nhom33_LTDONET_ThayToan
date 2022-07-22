using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopOnline_ProjectSem3.Common
{
    [Serializable]
    public class UserLogin
    {
        public long ID { set; get; }
        public string UserName { set; get; }
    }
}
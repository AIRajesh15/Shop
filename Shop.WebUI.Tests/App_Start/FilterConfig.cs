﻿using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Tests
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

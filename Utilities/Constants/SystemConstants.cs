﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "DefaultConnection";
        public const string CartSession = "CartSession";
        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedMeals = 12;
            public const int NumberOfLatestMeals = 21;
        }

        public class ProductConstants
        {
            public const string NA = "N/A";
        }
    }
}
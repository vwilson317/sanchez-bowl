﻿using System;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public static class EnumNamesHelper
    {
        public static string GetName<T>(T enumType)
        {
            return Enum.GetName(typeof(T), enumType);
        }
    }
}
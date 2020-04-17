﻿using System;
using System.Text;

internal static class StringBuilderCache
{
    [ThreadStatic]
    private static StringBuilder CachedInstance;
    private const int MAX_BUILDER_SIZE = 360;

    public static StringBuilder Acquire(int capacity = 16)
    {
        if (capacity <= 360)
        {
            StringBuilder cachedInstance = StringBuilderCache.CachedInstance;
            if (cachedInstance != null && capacity <= cachedInstance.Capacity)
            {
                StringBuilderCache.CachedInstance = null;
                cachedInstance.Clear();
                return cachedInstance;
            }
        }
        return new StringBuilder(capacity);
    }

    public static void Release(StringBuilder sb)
    {
        if (sb.Capacity <= 360)
        {
            StringBuilderCache.CachedInstance = sb;
        }
    }

    public static string GetStringAndRelease(StringBuilder sb)
    {
        string result = sb.ToString();
        StringBuilderCache.Release(sb);
        return result;
    }
}
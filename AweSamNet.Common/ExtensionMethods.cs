﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AweSamNet.Common
{
    public static class ExtensionMethods
    {
        // http://stackoverflow.com/a/6185236/1509728
        //used by EF
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            if (page > 0 && pageSize > 0)
            {
                return source.Skip((page - 1)*pageSize).Take(pageSize);
            }

            return source;
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            if (page > 0 && pageSize > 0)
            {
                return source.Skip((page - 1)*pageSize).Take(pageSize);
            }

            return source;
        }

        public static T JsonClone<T>(this T obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

    }
}

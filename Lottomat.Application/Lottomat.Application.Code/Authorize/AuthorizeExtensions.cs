﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Code.Authorize
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public static class AuthorizeExtensions
    {
        #region 带权限的数据源查询
        /// <summary>
        /// 获取授权数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToAuthorizeData<T>(this IEnumerable<T> data)
        {
            if (data != null)
            {
                if (OperatorProvider.Provider.Current().IsSystem)
                    return data;
                string dataAutor = OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(dataAutor).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return data.Where(lambda.Compile());
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}

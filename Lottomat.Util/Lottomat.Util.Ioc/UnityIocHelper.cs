﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;

namespace Lottomat.Util.Ioc
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：
    /// 日 期：2015.10.10
    /// 描 述：UnityIoc
    /// </summary>
    public class UnityIocHelper : IServiceProvider
    {
        private readonly IUnityContainer _container;
        private static readonly UnityIocHelper dbinstance = new UnityIocHelper("DBcontainer");
        private UnityIocHelper(string containerName)
        {
            try
            {
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                _container = new UnityContainer();
                section.Configure(_container, containerName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //section.Configure(_container);
        }
        public static string GetmapToByName(string containerName, string itype, string name = "")
        {
            try
            {
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                var _Containers = section.Containers;
                foreach (var _Container in _Containers)
                 {
                    if (_Container.Name == containerName)
                    {
                        var _Registrations = _Container.Registrations;
                        foreach (var _Registration in _Registrations)
                        {
                            if (name == "" && string.IsNullOrEmpty(_Registration.Name) && _Registration.TypeName == itype)
                            {
                                return _Registration.MapToName;
                            }
                        }
                        break;
                    }
                }
                return "";
            }
            catch
            {
                throw;
            }

        }

        public static UnityIocHelper DBInstance
        {
            get { return dbinstance; }
        }
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
        public T GetService<T>(params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(obj);
        }
        public T GetService<T>(string name, params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(name, obj);
        }
    }
}

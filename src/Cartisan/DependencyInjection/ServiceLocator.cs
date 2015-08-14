using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.DependencyInjection {
    public static class ServiceLocator {
        public static IResolver Resolver { get; set; }
        public static object GetService(Type serviceType) {
            return Resolver.GetService(serviceType);
        }

        public static TService GetService<TService>() {
            return Resolver.GetService<TService>();
        }

        public static IEnumerable GetServices(Type serviceType) {
            return Resolver.GetServices(serviceType);
        }

        public static IEnumerable<TService> GetServices<TService>() {
            return Resolver.GetServices<TService>();
        }

        public static void RegisterResolver(IResolver resolver) {
            Resolver = resolver;
        }

//        public static IResolver Resolver { get; set; }
//
//
//        public static void RegisterResolver(IResolver resolver) {
//            Resolver = resolver;
//        }

//        /// <summary>
//        /// 按类型获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        public static TService Resolve<TService>() {
//            return Resolver.Resolve<TService>();
//        }
//
//        /// <summary>
//        /// 按名称获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam><param name="serviceName">组件名称</param>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        public static TService ResolveNamed<TService>(string serviceName) {
//            return Resolver.ResolveNamed<TService>(serviceName);
//        }
//
//        /// <summary>
//        /// 按key获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam><param name="serviceKey">枚举类型的Key</param>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        public static TService ResolveKeyed<TService>(object serviceKey) {
//            return Resolver.ResolveKeyed<TService>(serviceKey);
//        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.DependencyInjection {
    public interface IResolver {
        object GetService(Type serviceType);
        TService GetService<TService>();

        IEnumerable GetServices(Type serviceType);
        IEnumerable<TService> GetServices<TService>();

//        object Resolve(Type serviceType);
//
//        /// <summary>
//        /// 按类型获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        TService Resolve<TService>();
//
//        /// <summary>
//        /// 按名称获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam><param name="serviceName">组件名称</param>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        TService ResolveNamed<TService>(string serviceName);
//
//        /// <summary>
//        /// 按参数获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam><param name="parameters"><see cref="T:Autofac.Core.Parameter"/></param>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        //TService Resolve<TService>(params Parameter[] parameters);
//
//        /// <summary>
//        /// 按key获取组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam><param name="serviceKey">枚举类型的Key</param>
//        /// <returns>
//        /// 返回获取的组件
//        /// </returns>
//        TService ResolveKeyed<TService>(object serviceKey);
//
//        /// <summary>
//        /// 获取InstancePerHttpRequest的组件
//        /// </summary>
//        /// <typeparam name="TService">组件类型</typeparam>
//        //TService ResolvePerHttpRequest<TService>(); 
    }
}
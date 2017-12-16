using System;
using System.Threading;
using System.Threading.Tasks;
using Argolis.Models;
using Nancy;
using Nancy.Routing.UriTemplates;

namespace Argolis.Nancy
{
    /// <summary>
    /// Base module for serving resources whose paths are URI Templates managed by
    /// <see cref="IModelTemplateProvider"/>
    /// </summary>
    public abstract class ArgolisModule : UriTemplateModule
    {
        private readonly IModelTemplateProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgolisModule"/> class.
        /// </summary>
        protected ArgolisModule(IModelTemplateProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgolisModule"/> class.
        /// </summary>
        protected ArgolisModule(IModelTemplateProvider provider, string modulePath)
            : base(modulePath)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Declares GET handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Get<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Get, action, condition, name);
        }

        /// <summary>
        /// Declares GET handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Get<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Get, action, condition, name);
        }

        /// <summary>
        /// Declares GET handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Get<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Get, action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Put, action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Put, action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Put, action, condition, name);
        }

        /// <summary>
        /// Declares POST handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Post<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Post, action, condition, name);
        }

        /// <summary>
        /// Declares POST handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Post<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Post, action, condition, name);
        }

        /// <summary>
        /// Declares POST handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Post<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Post, action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Delete, action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Delete, action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Delete, action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Patch, action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Patch, action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Patch, action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Options, action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Options, action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Options, action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, T> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterSync(this.Head, action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterAsync(this.Head, action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, CancellationToken, Task<T>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.RegisterCancellable(this.Head, action, condition, name);
        }

        private void RegisterCancellable<T>(
            Action<string, Func<dynamic, CancellationToken, Task<object>>, Func<NancyContext, bool>, string> addRoute,
            Func<dynamic, CancellationToken, Task<T>> action,
            Func<NancyContext, bool> condition,
            string name)
        {
            async Task<object> Func(dynamic p, CancellationToken c) => await action(p, c);
            addRoute(this.provider.GetTemplate(typeof(T)), Func, condition, name);
        }

        private void RegisterAsync<T>(
            Action<string, Func<dynamic, Task<object>>, Func<NancyContext, bool>, string> addRoute,
            Func<dynamic, Task<T>> action,
            Func<NancyContext, bool> condition,
            string name)
        {
            async Task<object> Func(dynamic p) => await action(p);
            addRoute(this.provider.GetTemplate(typeof(T)), Func, condition, name);
        }

        private void RegisterSync<T>(
            Action<string, Func<dynamic, object>, Func<NancyContext, bool>, string> addRoute,
            Func<dynamic, T> action,
            Func<NancyContext, bool> condition,
            string name)
        {
            addRoute(this.provider.GetTemplate(typeof(T)), p => action(p), condition, name);
        }
    }
}

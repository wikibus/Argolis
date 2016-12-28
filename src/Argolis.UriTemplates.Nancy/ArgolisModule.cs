using System;
using System.Threading;
using System.Threading.Tasks;
using Argolis.Models;
using Nancy;
using Nancy.Routing.UriTemplates;

namespace Argolis.UriTemplates.Nancy
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
        /// <param name="provider">Model template provider.</param>
        protected ArgolisModule(IModelTemplateProvider provider)
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
        public virtual void Get<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares GET handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Get<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares GET handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Get<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PUT handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Put<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares POST handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Post<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Post(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares POST handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Post<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Post(this.provider.GetTemplate(typeof(T)), action, condition, name);
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
            this.Post(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares DELETE handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Delete<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares PATCH handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Patch<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares OPTIONS handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Options<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, object> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }

        /// <summary>
        /// Declares HEAD handler for a resource
        /// </summary>
        /// <typeparam name="T">the resource model type</typeparam>
        /// <param name="action">Action that will be invoked when handling the resource</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Head<T>(Func<dynamic, CancellationToken, Task<object>> action, Func<NancyContext, bool> condition = null, string name = null)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action, condition, name);
        }
    }
}

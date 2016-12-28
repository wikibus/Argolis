using System;
using System.Threading;
using System.Threading.Tasks;
using Argolis.Models;
using Nancy;

namespace Argolis.Nancy
{
    public class ArgolisModule : NancyModule
    {
        private readonly IModelTemplateProvider provider;

        public ArgolisModule(IModelTemplateProvider provider)
        {
            this.provider = provider;
        }

        public virtual void Get<T>(Func<dynamic, object> action)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Get<T>(Func<dynamic, Task<object>> action)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Get<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Get(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Put<T>(Func<dynamic, object> action)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Put<T>(Func<dynamic, Task<object>> action)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Put<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Put(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Post<T>(Func<dynamic, object> action)
        {
            this.Post(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Post<T>(Func<dynamic, Task<object>> action)
        {
            this.Post(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Post<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Post(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Delete<T>(Func<dynamic, object> action)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Delete<T>(Func<dynamic, Task<object>> action)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Delete<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Delete(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Head<T>(Func<dynamic, object> action)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Head<T>(Func<dynamic, Task<object>> action)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Head<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Head(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Options<T>(Func<dynamic, object> action)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Options<T>(Func<dynamic, Task<object>> action)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Options<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Options(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Patch<T>(Func<dynamic, object> action)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Patch<T>(Func<dynamic, Task<object>> action)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action);
        }

        public virtual void Patch<T>(Func<dynamic, CancellationToken, Task<object>> action)
        {
            this.Patch(this.provider.GetTemplate(typeof(T)), action);
        }
    }
}
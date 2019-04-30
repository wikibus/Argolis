﻿using Nancy;

 namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Wraps <see cref="NancyContext"/> so that it can be injected by the container
    /// </summary>
    /// <remarks>
    /// See https://github.com/NancyFx/Nancy/issues/2346 for more details
    /// </remarks>
    public class NancyContextWrapper
    {
        /// <summary>
        /// Gets the current context
        /// </summary>
        public NancyContext Current { get; private set; }

        /// <summary>
        /// Sets the current context. This will be called at request startup
        /// </summary>
        internal void SetContext(NancyContext context)
        {
            this.Current = context;
        }
    }
}

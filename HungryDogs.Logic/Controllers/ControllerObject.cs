using CommonBase.Extensions;
using HungryDogs.Logic.DataContext;
using System;
using System.Threading.Tasks;

namespace HungryDogs.Logic.Controllers
{
    abstract class ControllerObject : IDisposable
    {
        private readonly bool contextOwner;
        protected ProjectDbContext Context { get; private set; }

        public ControllerObject(ProjectDbContext context)
        {
            context.CheckArgument(nameof(context));

            contextOwner = true;
            Context = context;
        }
        public ControllerObject(ControllerObject controllerObject)
        {
            controllerObject.CheckArgument(nameof(controllerObject));

            contextOwner = false;
            Context = controllerObject.Context;
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (contextOwner)
            {
                Context.Dispose();
            }
            Context = null;
        }
    }
}

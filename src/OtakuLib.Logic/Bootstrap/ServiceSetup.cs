using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using GihanSoft.ApplicationFrameworkBase;

using Microsoft.Extensions.DependencyInjection;

namespace OtakuLib.Logic.Bootstrap
{
    public class ServiceSetup : IServiceSetup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AddViewModels(services);
        }

        public static void AddViewModels(IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var viewModels = executingAssembly
                .GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName))
                .SelectMany(assembly => assembly.ExportedTypes)
                .Concat(executingAssembly.DefinedTypes)
                .Where(type => type.IsSubclassOf(typeof(ViewModelBase)));

            foreach (var vm in viewModels)
            {
                var interfaces = vm.FindInterfaces(
                    (itype, _) => itype.GetInterfaces().Contains(typeof(INotifyPropertyChanged)),
                    null);

                switch (interfaces.Length)
                {
                    case 0:
                        services.AddTransient(vm);
                        break;
                    case 1:
                        services.AddTransient(interfaces[0], vm);
                        break;
                    default:
                        foreach (var definition in interfaces)
                        {
                            services.AddTransient(definition, vm);
                        }
                        break;
                }

                foreach (var def in interfaces)
                {
                    if (services.Count(t => t.ServiceType == def) > 1)
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}

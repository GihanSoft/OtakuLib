using Microsoft.Extensions.DependencyInjection;

namespace GihanSoft.AppBase.Bootstrap;

public interface IServiceSetup
{
    void ConfigureServices(IServiceCollection services);
}

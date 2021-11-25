using Microsoft.Extensions.DependencyInjection;

namespace GihanSoft.AppBase;

public interface IServiceSetup
{
    void ConfigureServices(IServiceCollection services);
}

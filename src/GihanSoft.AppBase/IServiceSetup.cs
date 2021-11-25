using Microsoft.Extensions.DependencyInjection;

namespace GihanSoft.ApplicationFrameworkBase;

public interface IServiceSetup
{
    void ConfigureServices(IServiceCollection services);
}

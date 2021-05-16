using System;
using System.Net;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProtoBuf.Grpc.Server;
using ProtoBuf.Grpc.Configuration;

namespace NodeService
{
    public sealed class Startup
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var hostConfig = _configuration.GetSection("Host").Get<HostConfig>();
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Listen(
                    IPAddress.TryParse(hostConfig.WebApiListen, out var webApiAddress) ? webApiAddress : IPAddress.Loopback,
                    hostConfig.WebApiPort != 0 ? hostConfig.WebApiPort : 1209
                );
                options.Listen(
                    IPAddress.TryParse(hostConfig.RpcListen, out var rpcAddress) ? rpcAddress : IPAddress.Loopback,
                    hostConfig.RpcPort != 0 ? hostConfig.RpcPort : 1120,
                    listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
            });
            services.AddSingleton<UnverifiedMessagePool>();
            services.AddSingleton<NodeService>();
            services.AddSingleton<BlockService>();
            services.AddSingleton<MessageService>();
            services.AddControllers();
            services.AddCodeFirstGrpc(options => options.ResponseCompressionLevel = CompressionLevel.Optimal);
            services.TryAddSingleton(BinderConfiguration.Create(binder: new RpcBinder(services)));
            services.AddCodeFirstGrpcReflection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<TestRpc>();
                endpoints.MapGrpcService<BlockRpc>();
                endpoints.MapGrpcService<MessageRpc>();
                endpoints.MapCodeFirstGrpcReflectionService();
            });
        }
    }
}
using System;
using Microsoft.Extensions.Configuration;

namespace NodeService
{
    public sealed class NodeService
    {
        private readonly NodeConfig _config;
        private readonly UnverifiedMessagePool _unverifiedMessagePool;

        public NodeService(IConfiguration configuration, UnverifiedMessagePool unverifiedMessagePool)
        {
            _config = configuration.GetSection("Node").Get<NodeConfig>();
            _unverifiedMessagePool = unverifiedMessagePool;
        }
    }
}
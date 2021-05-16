using System;
using System.Threading.Tasks;
using System.ServiceModel;
using ProtoBuf.Grpc;

namespace NodeService
{
    [ServiceContract]
    public interface IBlockRpc
    {
        [OperationContract]
        void NewBlock(CallContext context = default);
    }

    public class BlockRpc : IBlockRpc
    {
        private readonly BlockService _service;

        public BlockRpc(BlockService service)
        {
            _service = service;
        }

        public void NewBlock(CallContext context = default)
        {
        }
    }
}
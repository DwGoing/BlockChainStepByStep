using System;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Text.Json.Serialization;
using ProtoBuf;
using ProtoBuf.Grpc;

namespace NodeService
{
    [ProtoContract]
    public sealed class Blcok
    {
        [JsonIgnore]
        [ProtoMember(1)]
        public string Hash { get; }
        [ProtoMember(2)]
        public string PreviousHash { get; init; }
        [ProtoMember(3)]
        public Message[] Messages { get; init; }

        public Blcok()
        {
            Hash = HashUtil.GetHash(this);
        }
    }
}
namespace NodeService
{
    public sealed class HostConfig
    {
        public string WebApiListen { get; set; }
        public int WebApiPort { get; set; }
        public string RpcListen { get; set; }
        public int RpcPort { get; set; }
    }
}
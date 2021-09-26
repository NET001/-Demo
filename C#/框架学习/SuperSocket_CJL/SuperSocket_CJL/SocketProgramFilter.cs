using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SuperSocketServer_CJL
{
    /// <summary>
    /// 协议解析类
    /// </summary>
    public class SocketProgramFilter : IPipelineFilter<SocketMessage>
    {
        public IPackageDecoder<SocketMessage> Decoder { get; set; }

        public IPipelineFilter<SocketMessage> NextFilter => this;

        public object Context { get; set; }
        //对协议进行解析
        public SocketMessage Filter(ref SequenceReader<byte> reader)
        {
            var bts = reader.Sequence.ToArray();
            reader.TryRead(out _);
            var json = Encoding.UTF8.GetString(bts);
            SocketMessage result = JsonConvert.DeserializeObject<SocketMessage>(json);
            return result;
        }
        public void Reset()
        {

        }
    }
}

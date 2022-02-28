using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace kafka测试
{
    internal class Program
    {

        static string BootstrapServers = "192.168.110.160:9092";
        //static string BootstrapServers = "192.168.110.45:9092";
        static void Main(string[] args)
        {
            //Demo1();
            try
            {
                //Demo1();
                Demo2();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Demo2异常" + ex.ToString());
            }
            Console.Read();
        }
        /// <summary>
        /// kafka消息发送者
        /// </summary>
        static void Demo1()
        {
            IProducer<Null, string> producer = new ProducerBuilder<Null, string>(new ProducerConfig()
            {
                BootstrapServers = BootstrapServers
            }).Build();
            Task.Run( async () =>
            {
                while (true)
                {
                    var result = await producer.ProduceAsync("test", new Message<Null, string>()
                    {
                        Value = "这是测试"
                    });
                    Thread.Sleep(1000);
                }

            });
        }
        /// <summary>
        /// kafka消息订阅者
        /// </summary>
        static void Demo2()
        {
            new string[]{
              "T_IO_DEVICE_DATACHANGED",
              "T_IO_DEVICE_ALARM",
              "T_IO_DEVICE_ADDED",
              "T_IO_DEVICE_DELETED",
              "T_IO_DEVICE_COMMANDRSP",
              "T_IO_DEVICE_ONLINESTATUS",
              "test",
          }.ToList().ForEach(t =>
          {
              Task.Run(() =>
              {
                  var conf = new ConsumerConfig
                  {
                      GroupId = "test-consumer-group",
                      BootstrapServers = BootstrapServers,
                      AutoOffsetReset = AutoOffsetReset.Earliest
                  };
                  using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
                  {
                      //数据变化消息
                      c.Subscribe(t);
                      CancellationTokenSource cts = new CancellationTokenSource();
                      Console.CancelKeyPress += (_, e) =>
                      {
                          e.Cancel = true; // prevent the process from terminating.
                          cts.Cancel();
                      };
                      try
                      {
                          while (true)
                          {
                              try
                              {
                                  var cr = c.Consume(cts.Token);
                                  //Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                                  Console.WriteLine($"收到消息topic：{cr.Topic}，message{cr.Message.Value}");
                              }
                              catch (ConsumeException e)
                              {
                                  Console.WriteLine($"Error occured: {e.Error.Reason}");
                              }
                          }
                      }
                      catch (OperationCanceledException)
                      {
                          // Ensure the consumer leaves the group cleanly and final offsets are committed.
                          c.Close();
                      }
                  }
              });
          });

        }
    }
}

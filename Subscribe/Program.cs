using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Subscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建客户端实例  
            MqttClient client = new MqttClient("127.0.0.1", 61613, false, null);


            // 注册消息接收处理事件，还可以注册消息订阅成功、取消订阅成功、与服务器断开等事件处理函数  
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //生成客户端ID并连接服务器  
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId,"admin","password");

            // 订阅主题"/home/temperature" 消息质量为 2   
            client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //处理接收到的消息  
            string msg = Encoding.Default.GetString(e.Message);
            Console.WriteLine("收到消息:" + msg + "\r\n");
        }
    }
}

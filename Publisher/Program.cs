using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            // create client instance
            MqttClient client = new MqttClient("127.0.0.1",61613,false,null);
            

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, "admin", "password");

            while (true)
            {
                string strValue = Console.ReadLine();

                // publish a message on "/home/temperature" topic with QoS 2
                client.Publish("/home/temperature", 
                    Encoding.UTF8.GetBytes(strValue), 
                    MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                    true);
                client.Publish("lvrh",
                    Encoding.UTF8.GetBytes(strValue+"-lvrh"),
                    MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                    true);
            }
        }
    }
}

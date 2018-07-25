
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectSend
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var factory = new ConnectionFactory();
        //    factory.HostName = "localhost";//RabbitMQ服务在本地运行
        //    factory.UserName = "guest";//用户名
        //    factory.Password = "banu_guest";//密码
        //    using (var connection = factory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare("hello rabiitmq", true, false, false, null);//创建一个名称为hello的消息队列
        //            string message = "Hello World"; //传递的消息内容
        //            var properties = channel.CreateBasicProperties();
        //            properties.SetPersistent(true);


        //            var body = Encoding.UTF8.GetBytes(message);
        //            channel.BasicPublish("", "hello rabiitmq", properties, body); //开始传递
        //            Console.WriteLine("已发送： {0}", message);
        //            Console.ReadLine();
        //        }
        //    }
        //    Console.ReadKey();
        //}

        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "171.15.17.85";//RabbitMQ服务在本地运行
            factory.Port = 5672;
            //factory.HostName = "10.15.1.126";            
            factory.UserName = "admin";//用户名
            factory.Password = "123456";//密码
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello rabiitmq", true, false, false, null);//创建一个名称为hello的消息队列
                    //int prefetchCount = 1;

                    //每个消费者发送确认信号之前，消息队列不发送下一个消息过来，一次只处理一个消息 
                    //限制发给同一个消费者不得超过1条消息  
                    channel.BasicQos(0, 1, false);

                    // 发送的消息  
                    for (int i = 0; i < 50; i++)
                    {
                        String message = "生产者一号生产消息" + i;
                        // 往队列中发出一条消息  
                        channel.BasicPublish("", "hello rabiitmq", null, Encoding.UTF8.GetBytes(message));
                        Console.WriteLine("Rabiitmq已发送 '" + message + "次'");
                        Thread.Sleep(i * 100);
                    }
                    // 关闭频道和连接  
                    channel.Close();
                    connection.Close();


                }
            }
            Console.ReadKey();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}

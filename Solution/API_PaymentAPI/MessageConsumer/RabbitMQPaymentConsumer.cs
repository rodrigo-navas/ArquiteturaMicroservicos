﻿using API_PaymentAPI.Messages;
using API_PaymentAPI.RabbitMQSender;
using Microsoft.Extensions.Hosting;
using PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace API_PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage paymentResult = new()
            {
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(paymentResult);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
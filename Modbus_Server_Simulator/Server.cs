using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentModbus;

namespace Modbus_Server_Simulator {
  internal class Server {
    static void Main(string[] args) {
      var server = new ModbusTcpServer();

      short[] registers = new short[10];

      Random random = new Random();

      server.Start(new IPEndPoint(IPAddress.Any,50200));
      Console.WriteLine("Modbus Server Started on Port 50200...");


      while(true) {
        var buffer = server.GetHoldingRegisters();
        buffer[0] = (short) random.Next(200, 301) ;
        buffer[1]++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Temperature: {buffer[0]/10.0}°C | Count: {buffer[1]}.");
        Thread.Sleep(1000);
      }
    }
  }
}

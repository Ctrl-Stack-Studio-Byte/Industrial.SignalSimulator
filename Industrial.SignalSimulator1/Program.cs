using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentModbus;

namespace Industrial.SignalSimulator1 {
  internal class Program {
    static void Main(string[] args) {
      var server = new ModbusTcpServer();

      short[] registers = new short[10];

      Random random = new Random();

      server.Start(new IPEndPoint(IPAddress.Any,5200));
      Console.WriteLine("Modbus Server Started on Port 5200...");


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

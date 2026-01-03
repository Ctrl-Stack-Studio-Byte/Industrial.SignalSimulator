using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modbus_Client_Monitor {
  internal class Client {

    const byte PLC_UNIT_ID = 0;
    const ushort TEMP_ADDR = 0;
    const ushort COUNT_ADDR = 1;
    const ushort READ_QTY = 2;
    const ushort RESET_VALUE = 0;
    const ushort START_COMMAND = 1;


    static void Main(string[] args) {

      // Initialize the client.
      var client = new ModbusTcpClient();

      // Connect to the local server on port 5200.
      client.Connect(new IPEndPoint(IPAddress.Loopback, 5200));
      Console.WriteLine("Connect success. Monitoring started.");

      while(true) {

        var data = client.ReadHoldingRegisters<ushort>(PLC_UNIT_ID, TEMP_ADDR, READ_QTY);

        double temperature = data[0] / 10.0f;
        int counter = data[1];

        // Use a Timestamp to show when the data was received.
        string time = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:ff");

        Console.WriteLine($"[{time}] Temperature{temperature:F2}°C | Counter: {counter}.");


        if(counter >= 100) {
          Console.WriteLine("!!! Counter limit reached. Sending Reset Command... !!!");
          client.WriteSingleRegister(PLC_UNIT_ID,COUNT_ADDR,RESET_VALUE);
          Console.WriteLine("!!! Reset Successful !!!");
        }


        Thread.Sleep(1000);
      }

    }
  }
}

using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Modbus_Client_Monitor {
  internal class Client {
    private const ushort TEMP_ADDR = 0;    
    private const ushort READ_QTY = 2;
    private const ushort COUNT_ADDR = 1;
    private const ushort RESET_VALUE = 0;
    static void Main(string[] args) {

      // Initialize the client.
      var mbs = new ModbusServices("127.0.0.1",50200);
      mbs.Connect();


      while(true) {

        short[] data = mbs.Read(TEMP_ADDR, READ_QTY);
        double temperature = data[0] / 10.0f;
        int counter = data[1];


        // Use a Timestamp to show when the data was received.
        string time = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:ff");

        Console.WriteLine($"[{time}] Temperature{temperature:F2}°C | Counter: {counter}.");

        if(counter >= 20) {

          Console.WriteLine("!!! Counter limit reached. Sending Reset Command... !!!");
          mbs.Write(COUNT_ADDR, RESET_VALUE);
          Console.WriteLine("!!! Reset Successful !!!");

        }

        Thread.Sleep(1000);
      }

    }
  }
}

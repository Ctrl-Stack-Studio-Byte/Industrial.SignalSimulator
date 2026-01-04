using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modbus_Client_Monitor {
  internal class ModbusServices {
    private string _hostAddress;
    private int _port;
    private ModbusTcpClient _MTC;
    private const byte PLC_UNIT_ID = 0;
    
    public ModbusServices(string ip, ushort port) { 
      _hostAddress = ip;
      _port = port;
      _MTC = new ModbusTcpClient();
      
    }


    public void Connect() {
      _MTC.Connect(new IPEndPoint(IPAddress.Parse(_hostAddress), _port));
    }
    public short[] Read(ushort address, ushort quantity) {
      var data =  _MTC.ReadHoldingRegisters<short>(PLC_UNIT_ID, address, quantity);

      return data.ToArray();
    }

    public void Write(ushort address, ushort value) {

      _MTC.WriteSingleRegister(PLC_UNIT_ID, address,value);
    
    }
  }
}

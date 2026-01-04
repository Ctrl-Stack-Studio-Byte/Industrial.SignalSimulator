# Industrial Communication Practice: Modbus TCP Simulator & Monitor

A hands-on industrial automation project built with C# and the FluentModbus library.

## Project Components
* **Modbus_Server_Simulator**: Simulates an on-site Industrial PLC.
  - Generates random temperature data.
  - Maintains a 16-bit holding register counter.
* **Modbus_Client_Monitor**: A supervisory control application.
  - Reads data from the remote PLC via Modbus TCP.
  - **Logic**: Automatically sends a "Reset" command (Value 0) to the PLC when the counter reaches 100.

## Technical Details
- Protocol: Modbus TCP.
- Functions: FC03 (Read Holding Registers), FC06 (Write Single Register).
- Configuration: Managed with C# constants for IP, Port, and Addresses.

## How to Run
1. Open `Industrial.SignalSimulator.sln` in Visual Studio.
2. Enable "Multiple Startup Projects" (Server first).
3. Run and observe data sync in console windows.

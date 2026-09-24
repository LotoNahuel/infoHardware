using System;
using exportHardwareSensors;

class Program
{
    static void Main()
    {
        hardwareSensors hardware = new hardwareSensors();
        Dictionary<string, List<(string nameHardware, string nameSensor, string typeSensor)>> data = hardware.GetSensors();

        foreach (var key in data.Keys)
        {
            foreach (var sensor in data[key])
            {
                Console.WriteLine($"{key}");
                Console.WriteLine($"{sensor.nameHardware}: {sensor.nameSensor}");
            }
        }
    }
}
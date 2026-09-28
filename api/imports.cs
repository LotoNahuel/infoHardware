using System;
using exportHardwareSensors;
using exportValueHardwareSensors;

class Program
{
    static void Main()
    {
        hardwareSensors hardware = new hardwareSensors();
        valueSensors values = new valueSensors();
        // Dictionary<string, List<(string nameHardware, string nameSensor, string typeSensor)>> data = hardware.GetSensors();
        List<(string nameHardware, string nameSensor, string typeSensor)> data = hardware.GetSensors();

        // foreach (var key in data)
        // {
            // foreach (var sensor in data)
            foreach (var sensor in data)
            {
                // Console.Clear();
                string valor = values.GetValueSensors(sensor.nameSensor);
                // Console.WriteLine($"\t{sensor.nameHardware} \n\t\t{sensor.nameSensor} - {valor} \t{sensor.typeSensor}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\t{sensor.nameHardware}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"\t\t{sensor.nameSensor}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\t{valor}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\t\t{sensor.typeSensor}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n------------------------------------------------------------------------\n");
                // Console.WriteLine($"{key}");
                // Console.WriteLine($"{sensor.nameHardware}: {sensor.nameSensor}");
            }
        // }
        // foreach (var value in data)
        // {
        //     // Console.WriteLine($"{value.nameHardware}, {value.nameSensor}, {value.typeSensor}");
            
        // }
        
        
    }
}
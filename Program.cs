using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Interop.PowerMonitor;

Console.WriteLine("LibreHadwareLib Importado Correctamente;");



// while (true)
// {

public class Miclase()
{
    public Dictionary<string, List<(string nameHardware, string nameSensor, string typeSensor)>> get_sensors()
    {
            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true,
                IsPowerMonitorEnabled = true,
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            Dictionary<string, List<(string nameHardware, string nameSensor, string typeSensor)>> data_hardware = new();
            
            foreach (IHardware hardware in computer.Hardware)
            {
                hardware.Update();
                if (
                    hardware.HardwareType == HardwareType.GpuNvidia ||
                    hardware.HardwareType == HardwareType.GpuAmd ||
                    hardware.HardwareType == HardwareType.GpuIntel ||
                    hardware.HardwareType == HardwareType.Cpu
                )
                {
                    Console.WriteLine($"\nHardware [{hardware.Name}]");
                    Console.WriteLine($"{hardware.HardwareType}");
                    // Console.WriteLine("Hardware: {0}", hardware.Name);

                    foreach (IHardware subhardware in hardware.SubHardware)
                    {
                        Console.WriteLine("\tSubhardware: {0}", subhardware.Name);
                        
                        foreach (ISensor sensor in subhardware.Sensors)
                        {
                            Console.WriteLine("\t-------------------------------------------------------------------------");
                            Console.WriteLine("\t\tSensor: {0}, value: {1}, is: {2}", sensor.Name, sensor.Value ?? 0, sensor.SensorType);
                        }
                    }
                    
                    hardware.Update();
                    
                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        if (sensor.Value.HasValue)
                        {
                            string key = $"{hardware.HardwareType}";
                            string name_sensor = sensor.Name;
                            const int i = 1;
                            while (data_hardware.ContainsKey(name_sensor))
                            {
                                name_sensor = $"{sensor.Name} [{i}]";
                                i ++;
                            }
                            if (!data_hardware.ContainsKey(key))
                            {
                                data_hardware[key] = new List<(string, string, string)>();
                            }

                            data_hardware[key].Add(
                                (
                                    $"{hardware.Name}",
                                    $"{sensor.Name}",
                                    $"{sensor.SensorType}"
                                )
                            );
                        }
                        
                    }

                    foreach (var key in data_hardware.Keys)
                    {
                        Console.Write($"\n{key}");
                        foreach (var sensor in data_hardware[key])
                        {
                            Console.WriteLine($"\nHardware: {sensor.nameHardware}");
                            Console.WriteLine("\t-------------------------------------------------------------------------");
                            Console.WriteLine($"\tSensor: {sensor.nameSensor} \n\t\tType Sensor: {sensor.typeSensor}");
                            Console.WriteLine("\t-------------------------------------------------------------------------");
                            
                        }
                        Console.WriteLine($"LARGO: {data_hardware[key].Count}");
                    }

                    return data_hardware;
                }
            } 
        // }
        
    }
}

    // Thread.Sleep(1000);
    // computer.Close();
// }

public class UpdateVisitor : IVisitor
{
    public void VisitComputer(IComputer computer) => computer.Traverse(this);

    public void VisitHardware(IHardware hardware)
    {
        hardware.Update();
        foreach (IHardware subHardware in hardware.SubHardware)
            subHardware.Accept(this);
    }

    public void VisitSensor(ISensor sensor) { }

    public void VisitParameter(IParameter parameter) { }
}
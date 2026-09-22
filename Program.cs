using LibreHardwareMonitor.Hardware;

Console.WriteLine("LibreHadwareLib Importado Correctamente;");

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

foreach (IHardware hardware in computer.Hardware)
{
    hardware.Update();
    // const salida = {$"Hardware: {hardware.Name}"};
    if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd || hardware.HardwareType == HardwareType.GpuIntel || hardware.HardwareType == HardwareType.Cpu)
    // if (hardware.HardwareType == HardwareType.Cpu)
    {
        Console.WriteLine($"Hardware [{hardware.Name}]");
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

        foreach (ISensor sensor in hardware.Sensors)
        {
            if (sensor.SensorType == SensorType.Temperature)
            {
                if (sensor.Value.HasValue)
                {
                    Console.WriteLine("\t-------------------------------------------------------------------------");
                    Console.WriteLine($"\tSensor: {sensor.Name}\n\t\tValue: {sensor.Value.Value:F1}\n\t\tType: {sensor.SensorType}");
                }
                else
                {
                    Console.WriteLine("\t-------------------------------------------------------------------------");
                    Console.WriteLine($"\tSensor: {sensor.Name}\n\t\tValue: NO HAY VALOR\n\t\tType: {sensor.SensorType}");
                }
            }
        }
        break;
    }
}

computer.Close();

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
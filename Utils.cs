using ConsoleApp1;

namespace Configurator
{
    internal class Utils
    {
        public static List<string> GenerateConfigurations(List<Component> components, Data data)
        {
            List<string> configs = new List<string>();
            if (components.Count == 1)
            {
                foreach (Component motherboard in data.Motherboards)
                {
                    foreach (Component memory in data.Memory)
                    {
                        var cpu = components[0];
                        if (ValidateCPUmotherboardMemory(cpu, motherboard, memory))
                        {
                            configs.Add(cpu.Name + " " + motherboard.Name + " " + memory.Name + " Price: " + (cpu.Price + motherboard.Price + memory.Price));
                        }
                    }

                }

                if (configs.Count == 0)
                {
                    configs.Add("Not a valid config!");
                    return configs;
                }
            }
            else
            {
                var cpu = components[0];
                var motherboard = components[1];

                if (ValidateCPUMotherboard(cpu, motherboard))
                {
                    foreach (Component memory in data.Memory)
                    {
                        if (ValidateCPUmotherboardMemory(cpu, motherboard, memory))
                        {
                            configs.Add(cpu.Name + " " + motherboard.Name + " " + memory.Name + " Price: " + (cpu.Price + motherboard.Price + memory.Price));
                        }
                    }
                }

                if (configs.Count == 0)
                {
                    configs.Add("Not a valid config!");
                    return configs;
                }
            }

            return configs;
        }

        public static bool ValidateCPUmotherboardMemory(Component cpu, Component motherboard, Component memory)
        {
            return cpu.SupportedMemory == memory.Type && ValidateCPUMotherboard(cpu, motherboard);
        }

        public static bool ValidateCPUMotherboard(Component cpu, Component motherboard)
        {
            return cpu.Socket == motherboard.Socket;
        }

        // Assume components are entered in the following order -> CPU, Motherboard, Memory
        public static void ValidateAndGenerateConfigurations(List<Component> components, Data data)
        {
            if (components.Count == 3)
            {
                if (!ValidateCPUmotherboardMemory(components[0], components[1],components[2]))
                {
                    Console.WriteLine("Not a valid config!");
                    return;
                }
                Console.WriteLine(" CPU - " + components[0].Name + "\n" + " Motherboard - " + components[1].Name + "\n" + " Memory - " + components[2].Name + "\n Price:" + (components[0].Price + components[1].Price + components[2].Price));
            }
            else
            {
                var configs = GenerateConfigurations(components, data);
                foreach (var c in configs)
                {
                    Console.WriteLine(c);
                }
            }
        }


        public static List<Component> GetEnteredComponents(string input, Data? allData)
        {
            string[] inputs = input.Split(",");
            if (inputs.Length > 3) return new List<Component>();

            List<Component> components = new List<Component>();
            List<Component> availableComponents = new List<Component>();
            availableComponents.AddRange(allData.CPUs);
            availableComponents.AddRange(allData.Motherboards);
            availableComponents.AddRange(allData.Memory);

            string? prevType = null;
            string? currentType = null;

            foreach (var partNumber in inputs)
            {
                bool foundElement = false;
                foreach (Component c in availableComponents)
                {
                    if (c.PartNumber == partNumber.Trim())
                    {
                        currentType = c.ComponentType;
                        if (prevType == currentType) { return new List<Component>(); }

                        prevType = currentType;
                        components.Add(c);
                        foundElement = true;
                        break;
                    }
                }

                if (!foundElement) return new List<Component>();
            }
            return components;
        }
    }
}

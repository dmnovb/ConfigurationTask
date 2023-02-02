namespace ConsoleApp1
{
    internal class Component
    {
        public string? Name { get; set; } 
        public string? ComponentType { get; set; }
        public string? PartNumber { get; set; }
        public float Price { get; set; }
        public string? SupportedMemory { get; set; }
        public string? Socket { get; set; }
        
        public string? Type { get; set; }
    }

    internal class Data
    {
        public Component[]? CPUs { get; set; }
        public Component[]? Motherboards { get; set; }
        public Component[]? Memory { get; set; }
    }
}

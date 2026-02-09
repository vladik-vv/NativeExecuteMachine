using static PC.Computer;

struct CheckRam{
    
    public static void CheckRAM(){
        
        if (RAM > maxRAM){
            Console.WriteLine("RAM IS FULL");
            Environment.Exit(404);
        }
    }
}
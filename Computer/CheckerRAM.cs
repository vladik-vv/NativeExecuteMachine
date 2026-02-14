using static Computer;

static class CheckerRam{   
    public static void CheckRAM(){
        
        if (RAM > maxRAM){
            Console.WriteLine("\nRAM IS FULL");
            Console.ReadLine();
            Environment.Exit(404);
        }
    }
}
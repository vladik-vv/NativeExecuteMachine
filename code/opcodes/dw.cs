using static PC.Computer;
using static Interpreter;
#pragma warning disable CS8981
struct dw{
    public static void run(){
        
        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }
                    
        RAM += 2;
        if (RAM >= maxRAM)
            KillProcessRAM();

        if (varsNames.Contains(parts[1])){
            Console.Write(Errors.Print(0x05));
            return;
        }

        try {
            varsShort.Add(parts[1], Convert.ToInt16(parts[2]));
        } catch {
            Console.Write(Errors.Print(0x06));
            return;
        }

        varsNames.Add(parts[1]);
        num++;
    }
}
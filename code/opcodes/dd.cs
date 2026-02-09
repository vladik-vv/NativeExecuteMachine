using static PC.Computer;
using static Interpreter;
#pragma warning disable CS8981
struct dd{
    public static void run(){

        if(!CheckArgument.Check(2)){
            Console.WriteLine(Errors.Print(0x02));
            return;
        }

        RAM += 4;
        if (RAM >= maxRAM)
            KillProcessRAM();

        if (varsNames.Contains(parts[1])){
            Console.Write(Errors.Print(0x05));
            return;
        }

        try {
            varsFloat.Add(parts[1], float.Parse(parts[2]));
        } catch {
            Console.Write(Errors.Print(0x06));
            return;
        }

        varsNames.Add(parts[1]);
        num++;
    }
}
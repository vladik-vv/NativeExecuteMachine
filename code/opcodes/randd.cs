using static PC.Computer;
using static Interpreter;
#pragma warning disable CS8981
struct randd{
    public static void run(){
        
        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        registres["rnd"] = rnd.NextInt64(long.Parse(parts[1]), long.Parse(parts[2]));
        num++;
        return;
    }
}
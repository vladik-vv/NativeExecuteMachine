using static PC.Computer;
using static Interpreter;
using static Stack;
#pragma warning disable CS8981
struct call{
    public static void run(){

        if(!CheckArgument.Check(1)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        try {
            stackAddress = num + 1;
            num = blocks[parts[1] + ":"];
        } catch {
            Console.Write(Errors.Print(0x03));
            return;
        }
    }
}
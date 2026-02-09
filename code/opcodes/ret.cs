using static PC.Computer;
using static Interpreter;
using static Stack;
#pragma warning disable CS8981
struct ret{
    public static void run(){
        if (stackAddress == -1){
            num++;
            return;
        }
        num = stackAddress;
        stackAddress = -1;
    }
}
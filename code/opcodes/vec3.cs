using static PC.Computer;
using static Interpreter;
using System.Numerics;
#pragma warning disable CS8981
struct vec3{
    public static void run(){

        if(!CheckArgument.Check(1)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        if (varsNames.Contains(parts[1])){
            Console.Write(Errors.Print(0x05));
            return;
        }

        vec3s.Add(parts[1], new Vector3());
        varsNames.Add(parts[1]);
        RAM += 24;
        num++;

    }
}
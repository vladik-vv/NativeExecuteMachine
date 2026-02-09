using static PC.Computer;
using static Interpreter;
#pragma warning disable CS8981
struct dist{
    public static void run(){
        
        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        if (!varsNames.Contains(parts[1]) && !varsNames.Contains(parts[2])){
            Console.Write(Errors.Print(0x08));
            return;
        }

        try {
            registres["rvc"] = (vec2s[parts[1]] - vec2s[parts[2]]).Length();
            num++;
            return;
        } catch {
            registres["rvc"] = (vec3s[parts[1]] - vec3s[parts[2]]).Length();
            num++;
            return;
        }
    }
}
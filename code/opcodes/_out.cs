using static PC.Computer;
using static Interpreter;
using static ConvertValue;
using static CommandsC;
struct _out{
    public static void run(){
        if(!CheckArgument.Check(1)){ Console.Write(Errors.Print(0x02)); return; }

        if (parts[1] == systemKey){
            if (keyInfo != null){
                Console.Write(keyInfo.Value.Key);
            }
            num++;
            return;
        }
        if (isDoubleARR){
            printf(ArgDoubleARR);
            num++;
            return;
        } else if (isDouble){
            printf(ArgDouble);
            num++;
            return;
        } else if (isVector2){
            printf(vec2s[parts[1]]);
            num++;
            return;
        } else if (isVector3){
            printf(vec2s[parts[1]]);
            num++;
            return;
        } else if (isStringARR){
            printf(ArgStringARR ?? "");
            num++;
            return;
        } else {
            printf(ArgString ?? "");
            num++;
            return;
        }
    }
}
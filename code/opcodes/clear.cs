using static PC.Computer;
using static Interpreter;
using static ConvertValue;
#pragma warning disable CS8981
struct clear{
    public static void run(){

        if (!CheckArgument.Check(1)){ Console.Write(Errors.Print(0x02)); return; }

        if (registres.Keys.Contains(parts[1])){ // если на очистку дается регистр
            registres[parts[1]] = 0;
            num++;
            return;
        } else if (parts[1] == "screen"){ // если на очистку дается экран
            Console.Clear();
            num++;
            return;
        } else if (parts[1] == "registres"){ // если на очистку дается кэш
            foreach (string r in registres.Keys){
                registres[r] = 0;
            } 
            num++;
            return;
        } else if (CheckVarContain(parts[1])){ // если на очистку дается переменная
            switch (CheckVarName(parts[1])){
                case "byte":{
                    varsByte.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    RAM -= 1;
                    num++;
                    return;
                }
                case "short":{
                    varsShort.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    RAM -= 2;
                    num++;
                    return;
                }
                case "float":{
                    varsFloat.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    RAM -= 4;
                    num++;
                    return;
                }
                case "double":{
                    varsDouble.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    RAM -= 8;
                    num++;
                    return;
                }
                case "string":{
                    RAM -= varsString[parts[1]].Length;
                    varsString.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    num++;
                    return;
                }
                case "vec2":{
                    RAM -= 16;
                    vec2s.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    num++;
                    return;
                }
                case "vec3":{
                    RAM -= 24;
                    vec3s.Remove(parts[1]);
                    varsNames.Remove(parts[1]);
                    num++;
                    return;
                }
                case "arrsByte":{
                    RAM -= arrsByte[parts[1]].Count();
                    num++;
                    return;
                }
                case "arrsShort":{
                    RAM -= arrsShort[parts[1]].Count();
                    num++;
                    return;
                }
                case "arrsFloat":{
                    RAM -= arrsFloat[parts[1]].Count();
                    num++;
                    return;
                }
                case "arrsDouble":{
                    RAM -= arrsDouble[parts[1]].Count();
                    num++;
                    return;
                }
                case "arrsString":{
                    RAM -= arrsString[parts[1]].Count();
                    for (int i = 0; i < arrsString[parts[1]].Count(); i++){
                        RAM -= (arrsString[parts[1]][i] ?? "").Length;
                    }
                    num++;
                    return;
                }
            }
        } else { // если ничего не подошло
            num++;
            return;
        }
    }
}
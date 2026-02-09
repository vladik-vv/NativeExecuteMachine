using static PC.Computer;
using static Interpreter;
using static Stack;
#pragma warning disable CS8981
struct ifg{
    public static void run(){

        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }


        try {
            if (parts[2] == "ret"){
                if (stackAddress == -1){
                    num++;
                    return;
                }
                num = stackAddress;
                stackAddress = -1;
                return;
            }
            
            if (registres.Keys.Contains(systemArguments[0])){ // если первый аргумент регистр
                if (registres.Keys.Contains(systemArguments[1])){ // если второй аргумент тоже регистр
                    if (registres[systemArguments[0]] > registres[systemArguments[1]]){ // если они равны
                        goo();
                        return;
                    } else {
                        num++;
                        return;
                    }
                } else if (CheckVarContain(systemArguments[1])) { // если второй аргумент это переменная
                    switch (CheckVarName(systemArguments[1])){
                        case "string":{
                            Console.Write(Errors.Print(0x07));
                            return;
                        }
                        case "byte":{
                            if (registres[systemArguments[0]] > Convert.ToDouble(varsByte[systemArguments[1]])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "short":{
                            if (registres[systemArguments[0]] > Convert.ToDouble(varsShort[systemArguments[1]])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "float":{
                            if (registres[systemArguments[0]] > Convert.ToDouble(varsFloat[systemArguments[1]])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "double":{
                            if (registres[systemArguments[0]] > Convert.ToDouble(varsDouble[systemArguments[1]])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                    }
                } else { // если второй аргумент готовое число
                    if (registres[systemArguments[0]] > Convert.ToDouble(systemArguments[1])){
                        goo();
                        return;
                    } else {
                        num++;
                        return;
                    }
                }
            } else if (CheckVarContain(systemArguments[0])){ // если первый аргумент переменная
                if (registres.Keys.Contains(systemArguments[1])){ // если второй аргумент регистр
                    switch (CheckVarName(systemArguments[0])){
                        case "string":{
                            Console.Write(Errors.Print(0x07));
                            return;
                        }
                        case "byte":{
                            if (varsByte[systemArguments[0]] > registres[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "short":{
                            if (varsShort[systemArguments[0]] > registres[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "float":{
                            if (varsFloat[systemArguments[0]] > registres[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "double":{
                            if (varsDouble[systemArguments[0]] > registres[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                    }
                } else if (CheckVarContain(systemArguments[1])){ // если второй аргумент тоже переменная
                    switch (CheckVarName(systemArguments[0])){
                        case "string":{
                            if (varsString[systemArguments[0]].Length > varsString[systemArguments[1]].Length){
                                goo(); // если первая переменная длиннее второй
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "byte":{
                            if (varsByte[systemArguments[0]] > varsByte[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "short":{
                            if (varsShort[systemArguments[0]] > varsShort[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "float":{
                            if (varsFloat[systemArguments[0]] > varsFloat[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "double":{
                            if (varsDouble[systemArguments[0]] > varsDouble[systemArguments[1]]){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                    }
                } else { // если второй аргумент готовое число или текст
                    switch (CheckVarName(systemArguments[0])){
                        case "string":{
                            if (varsString[systemArguments[0]].Length > systemArguments[1].Length){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "byte":{
                            if (varsByte[systemArguments[0]] > Convert.ToByte(systemArguments[1])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "short":{
                            if (varsShort[systemArguments[0]] > Convert.ToInt16(systemArguments[1])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "float":{
                            if (varsFloat[systemArguments[0]] > Convert.ToSingle(systemArguments[1])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                        case "double":{
                            if (varsDouble[systemArguments[0]] > Convert.ToDouble(systemArguments[1])){
                                goo();
                                return;
                            } else {
                                num++;
                                return;
                            }
                        }
                    }
                }
            }
            num++;
            return;
        } catch {
            Console.Write(Errors.Print(0x04));
            return;
        }

        
    }

    static void goo(){
        if (parts[1] == "go"){
            num = blocks[parts[2] + ":"];
            return;
        } else if (parts[1] == "call"){
            stackAddress = num + 1;
            num = blocks[parts[2] + ":"];
            return;
        }

        num++;
        return;
    }
}
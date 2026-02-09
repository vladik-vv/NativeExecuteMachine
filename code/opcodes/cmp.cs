using static PC.Computer;
using static Interpreter;
#pragma warning disable CS8981
struct cmp{
    public static void run(){

        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        try {
            systemArguments[0] = parts[1];
            if (parts[1] == systemKey){
                systemArguments[1] = parts[2];
                num++;
                return;
            }

            if (parts[2][0] == '"'){ // если вторая часть строка
                txt.Clear();
                int numtemp = 0; 
                while (codeParts[num][numtemp] != '"'){
                    numtemp++;
                }
                numtemp++;
                while (codeParts[num][numtemp] != '"'){
                    txt.Append(codeParts[num][numtemp]);
                    numtemp++;
                }

                systemArguments[1] = txt.ToString();
                num++;
                txt.Clear();
                return;
            }

            systemArguments[1] = parts[2];
            num++;
            return;
        } catch{
            Console.Write(Errors.Print(0x04));
            return;
        }
    }
}
using static PC.Computer;
using static Interpreter;

#pragma warning disable CS8981
struct ds{
    public static void run(){

        if(!CheckArgument.Check(2)){
            Console.Write(Errors.Print(0x02));
            return;
        }

        if (varsNames.Contains(parts[1])){
            Console.Write(Errors.Print(0x05));
            return;
        }

        try {
            int num2 = 0;
            while (codeParts[num][num2] != '"'){
                num2++;
            }
            num2++;
            while (codeParts[num][num2] != '"'){
                txt.Append(codeParts[num][num2]);
                num2++;
            }

            RAM += txt.Length;
            if (RAM >= maxRAM)
                KillProcessRAM();

            varsString.Add(parts[1], txt.ToString());
            txt.Clear();

        } catch {
            Console.Write(Errors.Print(0x06));
            return;
        }

        varsNames.Add(parts[1]);
        num++;
    }
}
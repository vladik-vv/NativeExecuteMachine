using System.Text;
using static Parser;

namespace OpCodes;

struct Out{ 

    static int num;
    public static void Execute(){ 
        Span<char> chars = stackalloc char[128];
        
        if (!int.TryParse(parts[^1], out num)){
            Errors.Print(0x02); 
            return;
        }

        chars.Slice(0, num).Fill('\n');
        Console.Write($"{Executer.value}{chars.Slice(0, num)}");
    }
}



using System.Reflection.Metadata;
using static Interpreter;
using static Bools;
using NetCoreAudio;
struct Errors{
    static readonly Dictionary<byte, string> err = new Dictionary<byte, string>{
        {0x00, $"\n Line {num + 1}. Error 0x00: Instruction not found"},
        {0x01, $"\n Line {num + 1}. Error 0x01: Address is not exist"},
        {0x02, $"\n Line {num + 1}. Error 0x02: Incorrect number of arguments"},
        {0x03, $"\n Line {num + 1} Error 0x03: Incorrect block name"},
        {0x04, $"\n Line {num + 1} Error 0x04: Incorrect arguments"},
        {0x05, $"\n Line {num + 1} Error 0x05: Redefinition of symbol"},
        {0x06, $"\n Segmentation fault"},
        {0x07, $"\n Line {num + 1} Error 0x07: Typing error"},
        {0x08, $"\n Line {num + 1} Error 0x08: Incorrect name address!"}
    };

    public static string Print(byte code){
        Player player = new Player();  
        isWarn = true;
        return err[code];
    }
}
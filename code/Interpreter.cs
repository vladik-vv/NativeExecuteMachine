using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using PC;
using static PC.Computer;
using static Bools;
using static Stack;
using static CheckRam;

struct Bools{
    public static bool isWarn = false; // если true то инструкция завершилась с ошибкой.
    public static bool isEnd = false;
    public static bool isStop = false; // мы находимся в блоке стоп?
}

struct Stack{
    public static int stackAddress = -1; // стэк для запоминания адреса
}

public class Interpreter
{   
    public const string systemKey = "system.key";
    public static Random rnd = new Random();
    public static string[] systemArguments = ["0", "0"]; // one, two.
    public static string[] parts = []; // список в котором будут части линии инструкций
    public static int num = 0; // номер текущей строки
    public static StringBuilder txt = new StringBuilder();
    // строка строитель, в которой мы будем хранить текстовые данные на время выполнения кода.
    public static Dictionary<int, string> codeParts = new Dictionary<int, string>();
    // библиотека в которой хранится ключ: адрес и значение это линия кода.

    public static Dictionary<string, int> blocks = new Dictionary<string, int>();
    // библиотека в которой хранятся адреса блоков

    public static HashSet<string> varsNames = new HashSet<string>(); // список с названиями переменных в адресе
    public static Dictionary<string, byte> varsByte = new Dictionary<string, byte>(); // библиотека с переменными байт
    public static Dictionary<string, short> varsShort = new Dictionary<string, short>(); // библиотека с переменными 2 байт
    public static Dictionary<string, float> varsFloat = new Dictionary<string, float>(); // библиотека с переменными 4 байт
    public static Dictionary<string, double> varsDouble = new Dictionary<string, double>(); // библиотека с переменными 8 байт
    public static Dictionary<string, string> varsString = new Dictionary<string, string>(); // библиотека с переменными стринг

    public static Dictionary<string, byte[]> arrsByte = new Dictionary<string, byte[]>(); // массивы байт
    public static Dictionary<string, short[]> arrsShort = new Dictionary<string, short[]>(); // массивы 2 байт
    public static Dictionary<string, float[]> arrsFloat = new Dictionary<string, float[]>(); // массивы 4 байт
    public static Dictionary<string, double[]> arrsDouble = new Dictionary<string, double[]>(); // массивы 8 байт
    public static Dictionary<string, string[]> arrsString = new Dictionary<string, string[]>(); // массивы строк


    public static Dictionary<string, Vector2> vec2s = new Dictionary<string, Vector2>(); // Вектора 2д
    public static Dictionary<string, Vector3> vec3s = new Dictionary<string, Vector3>(); // Вектора 3д
    public static char currentVectorCord; // x, y, z

    public static Dictionary<string, Type> varsTypes = new Dictionary<string, Type>(); // Переменные с типами

    public static Dictionary<string, int> currentArr = new Dictionary<string, int>(); // имя и номер элемента


    public readonly static Dictionary<string, Action> opcodes = new Dictionary<string, Action>{
        {"out", () => {ConvertValue.GetArg(1); _out.run();}},
        {"add", () => {ConvertValue.GetArg(2); add.run();}},
        {"call", () => call.run()},
        {"clear", () => clear.run()}, 
        {"cmp", () => {ConvertValue.GetArg(2); cmp.run();}},
        {"db", () => {ConvertValue.GetArg(2); db.run();}},
        {"dd", () => {ConvertValue.GetArg(2); dd.run();}},
        {"div", () => {ConvertValue.GetArg(2); div.run();}},
        {"dq", () => {ConvertValue.GetArg(2); dq.run();}},
        {"ds", () => {ConvertValue.GetArg(2); ds.run();}},
        {"dw", () => {ConvertValue.GetArg(2); dw.run();}},
        {"hlt", () => {if (isStop) isEnd = true; hlt.run();}},
        {"go", () => go.run()},
        {"ife", () => ife.run()},
        {"ifg", () => ifg.run()},
        {"ifl", () => ifl.run()},
        {"ifn", () => ifn.run()},
        {"inp", () => {ConvertValue.GetArg(1); inp.run();}},
        {"mov", () => {ConvertValue.GetArg(2); mov.run();}},
        {"mul", () => {ConvertValue.GetArg(2); mul.run();}},
        {"next", () => next.run()},
        {"ret", () => ret.run()},
        {"__start:", () => start.run()},
        {"__stop:", () => stop.run()},
        {"sub", () => {ConvertValue.GetArg(2); sub.run();}},
        {"wait", () => {ConvertValue.GetArg(1); wait.run();}},
        {"vec2", () => vec2.run()},
        {"vec3", () => vec3.run()},
        {"dist", () =>  dist.run()},
        {"randd", () => {ConvertValue.GetArg(2); randd.run();}},
        {"arr", () => {ConvertValue.GetArg(2); arr.run();}}
    };

    public static void Run(){
        Clear(); // очищаем мусор
        FillCodeParts(); // заполняем список с кодом
        Interpetation(); // интерпретация

        Console.WriteLine();
    }

    public static void Clear(){
        codeParts.Clear();
        txt.Clear();
        blocks.Clear();
        isStop = false;
        num = 0;
        stackAddress = -1;
        parts = [];
        isWarn = false;
        isEnd = false;
        varsNames.Clear();
        varsByte.Clear();
        varsFloat.Clear();
        varsDouble.Clear();
        varsShort.Clear();
        varsString.Clear();
        arrsByte.Clear();
        arrsShort.Clear();
        arrsFloat.Clear();
        arrsDouble.Clear();
        arrsString.Clear();
        vec2s.Clear();
        vec3s.Clear();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillCodeParts(){
        string[] lines = File.ReadAllLines(Terminal.path);

        foreach (string line in lines){

            switch (line.Trim().Split()[0]){
                case ".p":{
                    blocks.Add(line.Trim().Split()[1], num + 1);
                    break;
                }
                case "__stop:":{
                    blocks.Add("__stop:", num);
                    break;
                }
                case "__start:":{
                    blocks.Add("__start:", num);
                    break;
                }
            }

            bool commaRemove = false;
            foreach (char ch in line){
                if ((ch == ',' || ch == '"') && commaRemove == false){
                    if (ch == '"'){
                        txt.Append(ch);
                        commaRemove = true; 
                        continue;
                    }
                    commaRemove = true;
                    continue;
                } else {
                    txt.Append(ch);
                }
            }

            codeParts.Add(num, txt.ToString().Trim());
            num++; 
            txt.Clear();
        }

        num = blocks["__start:"];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void Interpetation(){

        while (true){
            if (isWarn){
                num = blocks["__stop:"];
                isWarn = false;
            } 

            if (isEnd){
                return;
            }

            parts = codeParts[num].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Count() < 1){
                num++;
                continue;
            }

            if (opcodes.ContainsKey(parts[0])){
                opcodes[parts[0]](); // выполняем действие под нужным опкодом
                CheckRAM();
            }
            else{
                Console.Write(Errors.Print(0x00));
            }
                
        }
    }

    public static string CheckVarName(string part){ // Проверить к какому типу относится переменная
        if (varsString.ContainsKey(part)){
            return "string";
        } else if (varsShort.ContainsKey(part)){
            return "short";
        } else if (varsFloat.ContainsKey(part)){          
            return "float";
        } else if (varsDouble.ContainsKey(part)){
            return "double";
        } else if (varsByte.ContainsKey(part)){
            return "byte";
        } else if (vec2s.ContainsKey(part)){
            return "vec2";
        } else if (vec3s.ContainsKey(part)){
            return "vec3";
        } else if (arrsByte.ContainsKey(part)){
            return "arrsByte";
        } else if (arrsShort.ContainsKey(part)){
            return "arrsShort";
        } else if (arrsFloat.ContainsKey(part)){
            return "arrsFloat";
        } else if (arrsDouble.ContainsKey(part)){
            return "arrsDouble";
        } else if (arrsString.ContainsKey(part)){
            return "arrsString";
        } else {
            return "none";
        }
    }

    public static bool CheckVarContain(string part){ // Проверить есть ли такая переменная
        if (!varsNames.Contains(part)){
            return false;
        }
        return true;
    }

    public static bool CheckVectorCord(int n){ // Проверяем, является ли аргумент коордиантой вектора
        txt.Clear();
        for (int i = 0; i < parts[n].Length; i++){
            if (parts[n][i] != '.'){
                txt.Append(parts[n][i]);
            } else {
                currentVectorCord = parts[n][i + 1];
                return true;
            }
        }
        txt.Clear();
        return false;
    }
}
using static Types;
using static Instructions;
using static Parser;
using static System.Convert;

class Executer{
    private static string opcode;
    protected static Types? currentType;
    protected static Types? typeArg1; // тип переменной
    protected static int? elementNumArg1; // номер элемента массива  
    protected static string? nameArg1; // имя переменной
    public static string value;  // готовое значение, которым изменяем
    protected static byte byteValue; // ниже значения, которые уже конвертированы
    protected static short shortValue;
    protected static float floatValue;
    protected static double doubleValue;
    protected static bool isHigh = false;
    protected static bool isEqual = false;
    protected static string line;
    static Dictionary<string, Action> opcodes = new Dictionary<string, Action>{
        {"mov", () => OpCodes.Math.Execute(_mov)},
        {"add", () => OpCodes.Math.Execute(_add)},
        {"sub", () => OpCodes.Math.Execute(_sub)},
        {"div", () => OpCodes.Math.Execute(_div)},
        {"mul", () => OpCodes.Math.Execute(_mul)},
        {"db", () => OpCodes.CreateVar.Execute(_byte)},
        {"dw", () => OpCodes.CreateVar.Execute(_short)},
        {"dd", () => OpCodes.CreateVar.Execute(_float)},
        {"dq", () => OpCodes.CreateVar.Execute(_double)},
        {"ds", () => OpCodes.CreateVar.Execute(_string)},
        {"arrb", () => OpCodes.CreateArr.Execute(_byteARR)},
        {"arrw", () => OpCodes.CreateArr.Execute(_shortARR)},
        {"arrd", () => OpCodes.CreateArr.Execute(_floatARR)},
        {"arrq", () => OpCodes.CreateArr.Execute(_doubleARR)},
        {"arrs", () => OpCodes.CreateArr.Execute(_stringARR)},
        {"out", () => OpCodes.Out.Execute()}, // структура
        {"go", () => OpCodes.GoTo.Execute(_go, value)},
        {"call", () => OpCodes.GoTo.Execute(_call, value)},
        {"ife", () => OpCodes.GoTo.ExecuteIF(_ife)},
        {"ifn", () => OpCodes.GoTo.ExecuteIF(_ifn)},
        {"ifh", () => OpCodes.GoTo.ExecuteIF(_ifh)},
        {"ifl", () => OpCodes.GoTo.ExecuteIF(_ifl)},
        {"ret", () => OpCodes.GoTo.Execute(_ret, value)},
        {"vec2", () => OpCodes.CreateVector.Execute(_vec2)},
        {"vec3", () => OpCodes.CreateVector.Execute(_vec3)},
        {"vec4", () => OpCodes.CreateVector.Execute(_vec4)}, 
        {"clear", () => OpCodes.Clear.Execute()},
        {"cmp", () => OpCodes.Compare.Execute()},
        {"tst_cmp_", () => Console.WriteLine($"IsHigh {isHigh}. IsEqual {isEqual}.")},
        {"tst_vars_", () => {foreach (string name in nameVars) {Console.Write($"{name} ");}}},
        {"wait", () => Thread.Sleep(Convert.ToInt32(value))},
        {"hlt", () => ExecuteHalt()},
    };
    
    public static void Start(string _instruction, Types? _currentType, Types? _typeArg1, int _elementNumArg1, string? _nameArg1,  string _value, string _line){

            opcode = _instruction;
            currentType = _currentType;
            typeArg1 = _typeArg1;
            elementNumArg1 = _elementNumArg1;
            nameArg1 = _nameArg1;
            value = _value;
            line = _line;
            
            CheckTypeAndConvertValue(); 
            CheckerRam.CheckRAM();
            opcodes[opcode]();
    }

    static void CheckTypeAndConvertValue(){ // проверяем, какой тип у 1 аргумента и конвертируем в этот тип готовое значение (2 аргумент).
        if (opcode == "clear") return;
        switch (typeArg1){
            case _registres:
            case _double:
            case _doubleARR:
            case _vector2x:
            case _vector2y:
            case _vector3x:
            case _vector3y:
            case _vector3z:
            case _vector4x:
            case _vector4y:
            case _vector4z:
            case _vector4w:{
                doubleValue = ToDouble(value); return;
            }
            case _byteARR:
            case _byte: byteValue = ToByte(value); return;
            case _shortARR:
            case _short: shortValue = ToInt16(value); return;
            case _floatARR:
            case _float: floatValue = ToSingle(value); return;
        }
    }

    static void ExecuteHalt(){ // перейти к блоку СТОП
        numberLine = blocks["__stop:"];
    }
}
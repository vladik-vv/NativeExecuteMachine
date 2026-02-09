using System.Numerics;
using static Interpreter;
using static PC.Computer;
struct ConvertValue{    

    public static bool isDouble;
    public static bool isStringARR;
    public static bool isDoubleARR;
    public static double ArgDouble;
    public static string? ArgString;
    public static double ArgDoubleARR;
    public static string? ArgStringARR;
    public static bool isVector2;
    public static bool isVector3;
    
    public static bool isEnd = false;
    

    public static void GetArg(int n){
    try{

        if (parts[n] == "system.key"){return; }

        isDouble = false;
        isVector2 = false;
        isVector3 = false;
        isStringARR = false;
        isDoubleARR = false;
        isEnd = false;
        currentArr.Clear();

        if (registres.ContainsKey(parts[n])){
            ArgDouble = registres[parts[n]];
            isDouble = true;
            return;
        }

        CheckerMassive(n);
        if (isEnd){
            return;
        }

        if (CheckVarContain(parts[n])){
            switch (CheckVarName(parts[n])){
                case "string":{
                    ArgString = varsString[parts[n]];
                    isDouble = false;
                    return;
                }
                case "byte":{
                    ArgDouble = varsByte[parts[n]];
                    isDouble = true;
                    return;
                }
                case "short":{
                    ArgDouble = varsShort[parts[n]];
                    isDouble = true;
                    return;
                }
                case "float":{
                    ArgDouble = varsFloat[parts[n]];
                    isDouble = true;
                    return;
                }
                case "double":{
                    ArgDouble = varsDouble[parts[n]];
                    isDouble = true;
                    return;
                }
                case "vec2":{
                    isDouble = false;
                    isVector2 = true;
                    isVector3 = false;
                    return;
                }
                case "vec3":{
                    isDouble = false;
                    isVector2 = false;
                    isVector3 = true;
                    return;
                }
            }
        }

        txt.Clear();
        for (int i = 0; i < parts[n].Length; i++){
            if (parts[n][i] != '.'){
                txt.Append(parts[n][i]);
            } else {
                switch (CheckVarName(txt.ToString())){
                    case "vec2":{
                        if (parts[n][i + 1] == 'x'){
                            ArgDouble = vec2s[txt.ToString()].X;
                            isDouble = true;
                            return; 
                        } else if (parts[n][i + 1] == 'y'){
                            ArgDouble = vec2s[txt.ToString()].Y;
                            isDouble = true;
                            return;
                        } else {
                            Errors.Print(0x08);
                            return;
                        }
                    }
                    case "vec3":{
                        if (parts[n][i + 1] == 'x'){
                            ArgDouble = vec3s[txt.ToString()].X;
                            isDouble = true;
                            return;
                        } else if (parts[n][i + 1] == 'y'){
                            ArgDouble = vec3s[txt.ToString()].Y;
                            isDouble = true;
                            return;
                        } else if (parts[n][i + 1] == 'z'){
                            ArgDouble = vec3s[txt.ToString()].Z;
                            isDouble = true;
                            return;
                        } else {
                            Errors.Print(0x08);
                            return;
                        }
                    }
                }
            }
        }   
        txt.Clear();
        
        try {
            ArgDouble = Convert.ToDouble(parts[n]);
            isDouble = true;
            return;
        } catch {
            txt.Clear();
            int num2 = 0; 
            while (codeParts[num][num2] != '"'){
                num2++;
            }
            num2++;
            while (codeParts[num][num2] != '"'){
                txt.Append(codeParts[num][num2]);
                num2++;
            }

            ArgString = txt.ToString();
            txt.Clear();
            isDouble = false;
            return;
        }
    } catch{
        return;
    }
    }

    public static bool CheckElementArr(string part){
        foreach (char ch in part){
            if (ch == '['){
                return true;
            }
        }
        return false;
    }

    public static void GetCurrentArr(string part){
        txt.Clear();
        int tempnumpart = 0;
        string tempnamearr = "";
        foreach (char ch in part){
            if (ch != '['){
                txt.Append(ch);
                tempnumpart++;
            } else {
                tempnamearr = txt.ToString();
                currentArr.Add(tempnamearr, 0);
                txt.Clear();
                break;
            }
        }
        txt.Clear();
        tempnumpart++;
        while (tempnumpart < part.Length){
            if (part[tempnumpart] != ']'){
                txt.Append(part[tempnumpart]);
                tempnumpart++;
            } else {
                if (registres.ContainsKey(txt.ToString())){
                    currentArr[tempnamearr] = Convert.ToInt32(registres[txt.ToString()]);
                    break;
                }

                currentArr[tempnamearr] = Convert.ToInt32(txt.ToString());  //  если аргумент это просто число
                break;
            }
        }
    }

    public static void CheckerMassive(int n){
        if (CheckElementArr(parts[n])){     // Если аргумент является элементом массива
            GetCurrentArr(parts[n]);
            switch (CheckVarName(currentArr.First().Key)){
                case "arrsByte":{
                    ArgDoubleARR = arrsByte[currentArr.First().Key][currentArr.First().Value];
                    isDoubleARR = true;
                    isEnd = true;
                    break;
                }
                case "arrsShort":{
                    ArgDoubleARR = arrsShort[currentArr.First().Key][currentArr.First().Value];
                    isDoubleARR = true;
                    isEnd = true;
                    break;
                }
                case "arrsFloat":{
                    ArgDoubleARR = arrsFloat[currentArr.First().Key][currentArr.First().Value];
                    isDoubleARR = true;
                    isEnd = true;
                    break;
                }
                case "arrsDouble":{
                    ArgDoubleARR = arrsDouble[currentArr.First().Key][currentArr.First().Value];
                    isDoubleARR = true;
                    isEnd = true;
                    break;
                }
                case "arrsString":{
                    ArgStringARR = arrsString[currentArr.First().Key][currentArr.First().Value];
                    isDouble = false;
                    isEnd = true;
                    isStringARR = true;
                    break;
                }
            }
            isEnd = true;
            return;
        }
    }
}
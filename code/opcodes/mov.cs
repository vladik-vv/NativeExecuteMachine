using static PC.Computer;
using static Interpreter;
using static ConvertValue;
using System.Numerics;
#pragma warning disable CS8981
struct mov{
    public static void run(){

        if(!CheckArgument.Check(2)){ Console.Write(Errors.Print(0x02)); return; }

        try {
            CheckerMassive(1);
            switch(CheckVarName(currentArr.First().Key)){
                case "arrsByte":{
                    arrsByte[currentArr.First().Key][currentArr.First().Value] = Convert.ToByte(ArgDouble);
                    num++;
                    return;
                }
                case "arrsShort":{
                    arrsShort[currentArr.First().Key][currentArr.First().Value] = Convert.ToInt16(ArgDouble);
                    num++;
                    return;
                }
                case "arrsFloat":{
                    arrsFloat[currentArr.First().Key][currentArr.First().Value] = Convert.ToInt32(ArgDouble);
                    num++;
                    return;
                }
                case "arrsDouble":{
                    arrsDouble[currentArr.First().Key][currentArr.First().Value] = Convert.ToInt64(ArgDouble);
                    num++;
                    return;
                }
                case "arrsString":{
                    RAM -= arrsString[currentArr.First().Key][currentArr.First().Value].Length;
                    arrsString[currentArr.First().Key][currentArr.First().Value] = ArgString;
                    num++;
                    RAM += ArgString.Length;
                    return;
                }
            }
            num++;
            return;
        } catch {}

        if (CheckVectorCord(1)) {
            if (CheckVarContain(txt.ToString())){
                switch (CheckVarName(txt.ToString())){
                    case "vec2":{
                        if (currentVectorCord == 'x'){
                            vec2s[txt.ToString()] = new Vector2((float)ArgDouble, vec2s[txt.ToString()].Y);
                            num++;
                            return; 
                        } else if (currentVectorCord == 'y'){
                            vec2s[txt.ToString()] = new Vector2(vec2s[txt.ToString()].X, (float)ArgDouble);
                            num++;
                            return;
                        } else {
                            Errors.Print(0x08);
                            return;
                        }
                    }
                    case "vec3":{
                        if (currentVectorCord == 'x'){
                            vec3s[txt.ToString()] = new Vector3((float)ArgDouble, vec3s[txt.ToString()].Y, vec3s[txt.ToString()].Z);
                            num++;
                            return; 
                        } else if (currentVectorCord == 'y'){
                            vec3s[txt.ToString()] = new Vector3(vec3s[txt.ToString()].X, (float)ArgDouble, vec3s[txt.ToString()].Z);
                            num++;
                            return;
                        } else if(currentVectorCord == 'z'){
                            vec3s[txt.ToString()] = new Vector3(vec3s[txt.ToString()].X, vec3s[txt.ToString()].Y, (float)ArgDouble);
                            num++;
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

        if (registres.ContainsKey(parts[1])){
            registres[parts[1]] = ArgDouble;
            num++;
            return;
        }

        if (CheckVarContain(parts[1])){
            switch (CheckVarName(parts[1])){
                case "string":{
                    varsString[parts[1]] = ArgString ?? "";
                    num++;
                    return;
                }
                case "byte":{
                    varsByte[parts[1]] = Convert.ToByte(ArgDouble);
                    num++;
                    return;
                }
                case "short":{
                    varsShort[parts[1]] = Convert.ToInt16(ArgDouble);
                    num++;
                    return;
                }
                case "float":{
                    varsFloat[parts[1]] = Convert.ToSingle(ArgDouble);
                    num++;
                    return;
                }
                case "double":{
                    varsDouble[parts[1]] = Convert.ToDouble(ArgDouble);
                    num++;
                    return;
                }
            }
        }

        registres[parts[1]] = ArgDouble;
        num++;
        return;
    }
}
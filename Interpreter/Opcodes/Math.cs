using static Parser;
using static Types;
using static Computer;
using static Instructions;

namespace OpCodes;

class Math : Executer{ // typeArg1, nameArg1. value, byteValue, shortValue, floatValue, doubleValue, isHigh, isEqual, currentType, line

    public static void Execute(Instructions mode){   // математические инструкции

        switch (typeArg1){
            case _vector2x: vec2s[nameArg1].Set(doubleValue, mode, 'X'); break;
            case _vector2y: vec2s[nameArg1].Set(doubleValue, mode, 'Y'); break;
            case _vector3x: vec3s[nameArg1].Set(doubleValue, mode, 'X'); break;
            case _vector3y: vec3s[nameArg1].Set(doubleValue, mode, 'Y'); break;
            case _vector3z: vec3s[nameArg1].Set(doubleValue, mode, 'Z'); break;
            case _vector4x: vec4s[nameArg1].Set(doubleValue, mode, 'X'); break;
            case _vector4y: vec4s[nameArg1].Set(doubleValue, mode, 'Y'); break;
            case _vector4z: vec4s[nameArg1].Set(doubleValue, mode, 'Z'); break;
            case _vector4w: vec4s[nameArg1].Set(doubleValue, mode, 'W'); break;
        }

        switch (mode){
            case _mov:{    // переместить
                switch (typeArg1){
                    case _registres: registres[nameArg1] = doubleValue; return;
                    case _byte: byteVars[nameArg1] = byteValue; return;
                    case _short: shortVars[nameArg1] = shortValue; return;
                    case _float: floatVars[nameArg1] = floatValue; return;
                    case _double: doubleVars[nameArg1] = doubleValue; return;
                    case _string: RAM -= stringVars[nameArg1].Length; stringVars[nameArg1] = value; RAM += value.Length; return;
                    case _byteARR: byteArrs[nameArg1][elementNumArg1 ?? 0] = byteValue; return;
                    case _shortARR: shortArrs[nameArg1][elementNumArg1 ?? 0] = shortValue; return;
                    case _floatARR: floatArrs[nameArg1][elementNumArg1 ?? 0] = floatValue; return;
                    case _doubleARR: doubleArrs[nameArg1][elementNumArg1 ?? 0] = doubleValue; return;
                    case _stringARR: stringArrs[nameArg1][elementNumArg1 ?? 0] = value; return;
                } return;
            }
            case _add:{    // добавить
                switch (typeArg1){
                    case _registres: registres[nameArg1] += doubleValue; return;
                    case _byte: byteVars[nameArg1] += byteValue; return;
                    case _short: shortVars[nameArg1] += shortValue; return;
                    case _float: floatVars[nameArg1] += floatValue; return;
                    case _double: doubleVars[nameArg1] += doubleValue; return;
                    case _string: stringVars[nameArg1] += value; RAM += value.Length; return;
                    case _byteARR: byteArrs[nameArg1][elementNumArg1 ?? 0] += byteValue; return;
                    case _shortARR: shortArrs[nameArg1][elementNumArg1 ?? 0] += shortValue; return;
                    case _floatARR: floatArrs[nameArg1][elementNumArg1 ?? 0] += floatValue; return;
                    case _doubleARR: doubleArrs[nameArg1][elementNumArg1 ?? 0] += doubleValue; return;
                    case _stringARR: stringArrs[nameArg1][elementNumArg1 ?? 0] += value; return;
                } return;
            }
            case _sub:{    // вычтать
                switch (typeArg1){
                    case _registres: registres[nameArg1] -= doubleValue; return;
                    case _byte: byteVars[nameArg1] -= byteValue; return;
                    case _short: shortVars[nameArg1] -= shortValue; return;
                    case _float: floatVars[nameArg1] -= floatValue; return;
                    case _double: doubleVars[nameArg1] -= doubleValue; return;
                    case _byteARR: byteArrs[nameArg1][elementNumArg1 ?? 0] -= byteValue; return;
                    case _shortARR: shortArrs[nameArg1][elementNumArg1 ?? 0] -= shortValue; return;
                    case _floatARR: floatArrs[nameArg1][elementNumArg1 ?? 0] -= floatValue; return;
                    case _doubleARR: doubleArrs[nameArg1][elementNumArg1 ?? 0] -= doubleValue; return;
                    case _string:
                    case _stringARR: Errors.Print(0x08); return;
                } return;
            }
            case _div:{    // поделить
                switch (typeArg1){
                    case _registres: registres["rnr"] = registres[nameArg1] % doubleValue; registres[nameArg1] /= doubleValue; return;
                    case _byte: registres["rnr"] = byteVars[nameArg1] % byteValue; byteVars[nameArg1] /= byteValue; return;
                    case _short: registres["rnr"] = shortVars[nameArg1] % shortValue; shortVars[nameArg1] /= shortValue; return;
                    case _float: registres["rnr"] = floatVars[nameArg1] % floatValue; floatVars[nameArg1] /= floatValue; return;
                    case _double: registres["rnr"] = doubleVars[nameArg1] % doubleValue; doubleVars[nameArg1] /= doubleValue; return;
                    case _byteARR: registres["rnr"] = byteArrs[nameArg1][elementNumArg1 ?? 0] % byteValue; byteArrs[nameArg1][elementNumArg1 ?? 0] /= byteValue; return;
                    case _shortARR: registres["rnr"] = shortArrs[nameArg1][elementNumArg1 ?? 0] % shortValue; shortArrs[nameArg1][elementNumArg1 ?? 0] /= shortValue; return;
                    case _floatARR: registres["rnr"] = floatArrs[nameArg1][elementNumArg1 ?? 0] % floatValue; floatArrs[nameArg1][elementNumArg1 ?? 0] /= floatValue; return;
                    case _doubleARR: registres["rnr"] = doubleArrs[nameArg1][elementNumArg1 ?? 0] % doubleValue; doubleArrs[nameArg1][elementNumArg1 ?? 0] /= doubleValue; return;
                    case _string:
                    case _stringARR: Errors.Print(0x08); return;
                } return;
            }
            case _mul:{    // умножить
                switch (typeArg1){
                    case _registres: registres[nameArg1] *= doubleValue; return;
                    case _byte: byteVars[nameArg1] *= byteValue; return;
                    case _short: shortVars[nameArg1] *= shortValue; return;
                    case _float: floatVars[nameArg1] *= floatValue; return;
                    case _double: doubleVars[nameArg1] *= doubleValue; return;
                    case _byteARR: byteArrs[nameArg1][elementNumArg1 ?? 0] *= byteValue; return;
                    case _shortARR: shortArrs[nameArg1][elementNumArg1 ?? 0] *= shortValue; return;
                    case _floatARR: floatArrs[nameArg1][elementNumArg1 ?? 0] *= floatValue; return;
                    case _doubleARR: doubleArrs[nameArg1][elementNumArg1 ?? 0] *= doubleValue; return;
                    case _string:
                    case _stringARR: Errors.Print(0x08); return;
                } return;
            }
        }
    }
}
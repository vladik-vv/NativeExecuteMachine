using static Parser;
using static Computer;
using static Instructions;

namespace OpCodes;

class CreateVector : Executer{ // typeArg1, nameArg1. value, byteValue, shortValue, floatValue, doubleValue, isHigh, isEqual, currentType, line
    public static void Execute(Instructions t_vec){ // создание вектора

        nameVars.Add(value);
        switch (t_vec){
            case _vec2:{
                vec2s.Add(value, new Vector2(0, 0));
                RAM += 8; 
                return;
            }
            case _vec3:{
                vec3s.Add(value, new Vector3(0, 0, 0));
                RAM += 12;
                return;
            }
            case _vec4:{
                vec4s.Add(value, new Vector4(0, 0, 0, 0));
                RAM += 16;
                return;
            }
        }

    }
}
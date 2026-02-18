using static Parser;
using static Executer;

struct CreateMX2{ 

    public static void Execute(Types mode){ 

        int x = matrix_x ?? 0;
        int y = matrix_y ?? 0;

        if (mode == Types._stringMatrix2){
            Matrix2_s[nameArg1] = new string[y, x];
            return;
        } else {
            Matrix2_q[nameArg1] = new double[y, x];
            return;
        }
    }
}


using static PC.Computer;
using static Interpreter;
using static ConvertValue;
using System.Numerics;
using static CheckRam;

#pragma warning disable CS8981
struct arr{
    static int lnm;
    public static void run(){

        if(!CheckArgument.Check(2)){ Console.Write(Errors.Print(0x02)); return; }

        switch(parts[2][0]){
            case 'b':{
                CheckNum();
                arrsByte.Add(parts[1], new byte[lnm]);
                varsNames.Add(parts[1]);
                RAM += lnm;
                num++;
                return;
            }
            case 'w':{
                CheckNum();
                arrsShort.Add(parts[1], new short[lnm]);
                varsNames.Add(parts[1]);
                RAM += lnm * 2;
                num++;
                return;
            }
            case 'd':{
                CheckNum();
                arrsFloat.Add(parts[1], new float[lnm]);
                varsNames.Add(parts[1]);
                RAM += lnm * 4;
                num++;
                return;
            }
            case 'q':{
                CheckNum();
                arrsDouble.Add(parts[1], new double[lnm]);
                varsNames.Add(parts[1]);
                RAM += lnm * 8;
                num++;
                return;
            }
            case 's':{
                CheckNum();
                arrsString.Add(parts[1], new string[lnm]);
                varsNames.Add(parts[1]);
                RAM += lnm;
                num++;
                return;
            }
        }
    }

    static void CheckNum(){
        txt.Clear();
        for (int i = 1; i < parts[2].Length; i++){
            if (parts[2][i] != '[' && parts[2][i] != ']'){
                txt.Append(parts[2][i]);
            }
        }
        lnm = Convert.ToInt32(txt.ToString());
        txt.Clear(); 
    }
}

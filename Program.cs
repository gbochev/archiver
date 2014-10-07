using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace archiver
{
    class Program
    {
        static string Archieve(string input, char archievedSym, char startSym, char endSym)
        {
            input += "end";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == archievedSym)
                {
                    int j = 0;
                    do
                    {
                        j++;
                    } while (input[j + i] == archievedSym);
                    input = input.Remove(i, j);
                    input = input.Insert(i, ""+startSym + j + endSym);
                }

            }
            return input.Remove(input.Length - 3, 3);
        }

        static byte[] byteArchieve(byte[] byteArray, byte archievedSym, byte startSym, byte endSym)
        {
            List<byte> input = new List<byte>();
            for (int i = 0; i < byteArray.Length; i++)
                input.Add(byteArray[i]);
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == archievedSym)
                {
                    int j = 0;
                    do
                    {
                        j++;
                    } while (input[j + i] == archievedSym);
                    if (j >= 3)
                    {
                        input.RemoveRange(i, j);
                        input.Insert(i, (byte)startSym);
                        input.Insert(i + 1, (byte)j);
                        input.Insert(i + 2, (byte)endSym);
                    }
                    //input.Add((byte)startSym);
                    //input.Add((byte)j);
                    //input.Add((byte)endSym);
                }

            }
            byte[] output = new byte[input.Count];
            for (int i = 0; i < input.Count; i++)
                output[i] = input[i];
            return output;
        }
        static byte[] byteUnarchieve(byte[] byteArray, byte archievedSym, byte startSym, byte endSym)
        {
            List<byte> input = new List<byte>();
            for (int i = 0; i < byteArray.Length; i++)
                input.Add(byteArray[i]);
          //  input.Add((byte)'e');
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == startSym)
                {
                    string repTimes = "";
                    int j = 0;
                    do
                    {
                        j++;
                        repTimes += input[i + j];
                    } while (input[j + i] != endSym && input.Count<i+j);
                    input.RemoveRange(i, j + 2);
                    for (int k = 0; k < int.Parse(repTimes); k++)
                    {
                        input.Insert(i, (byte)archievedSym);
                    }
                }
            }
           // input.RemoveAt(input.Count-1);
            byte[] output = new byte[input.Count];
            for (int i = 0; i < input.Count; i++)
                output[i] = input[i];
            return output;
        }
        static byte mostCommonByte(byte[] array)
        {
            byte[] charList = new byte[256];
            for (int i = 0; i < array.Length; i++)
                for (int k = 0; k < 256; k++)
                    if (k == array[i]) charList[k]++;
            int maxElementCount = 0;
            byte maxElement = (byte)0;
            for (int i = 0; i < 256; i++)
                if (maxElementCount < charList[i])
                {
                    maxElementCount = charList[i];
                    maxElement = (byte)i;
                }
            return maxElement;
        }
        static string Unarchieve(string input, char archievedSym, char startSym, char endSym)
        {
            input += "end";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == startSym)
                {
                    string repTimes = "";
                    int j = 0;
                    do
                    {
                        j++;
                        repTimes += input[i + j];
                    } while (input[j + i] != endSym);
                    input = input.Remove(i, j + 1);
                    for (int k = 0; k < int.Parse(repTimes.Remove(repTimes.Length - 1)); k++)
                    {
                        input = input.Insert(i, "" + archievedSym);
                    }
                }
            }
            return input.Remove(input.Length - 3, 3);
        }
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
        static List<byte> findKeySymbols(byte[] array)
        {
            List<byte> controlBytes = new List<byte>();
            for (int i = 0; i < 256; i++)
                controlBytes.Add((byte)i);
            for (int i = 0; i < array.Length; i++)
                if (controlBytes.Contains(array[i]))
                    controlBytes.Remove(array[i]);
            return controlBytes;
        }
        static void Main(string[] args)
        {
            //Debug zone 
           // byte[] file = ReadFile("lena.bmp");//args[1] - input file, args[2] - output archieve
           // byte mostCommon = mostCommonByte(file);
          //  List<byte> lst = findKeySymbols(file);
           // Console.ReadKey();
            //byte[] temp = byteArchieve(file, mostCommon, (byte)'#', (byte)'$');
            
            if (args.Length == 3)
            {
                if (args[0] == "a")
                {
                    byte[] file = ReadFile(args[1]);//args[1] - input file, args[2] - output archieve
                    byte mostCommon = mostCommonByte(file);
                    List<byte> lst = findKeySymbols(file);
                    if (lst.Count < 2)
                    {
                        Console.WriteLine("Невъзможно архивиране.");
                        Environment.Exit(0);
                    }
                    byte[] temp = byteArchieve(file, mostCommon, lst[0], lst[1]);
                    byte[] output = new byte[temp.Length+3];
                    for(int i =0;i<temp.Length;i++)
                        output[i]=temp[i];
                    output[temp.Length]=mostCommon;
                    output[temp.Length + 1] = lst[0];//start key symbol
                    output[temp.Length + 2] = lst[1];//end key symbol
                    ByteArrayToFile(args[2], output);
                }
                if (args[0] == "r")
                {
                    byte[] file = ReadFile(args[1]);//args[1] - input file, args[2] - output archieve
                    byte mostCommon = file[file.Length - 3];
                    byte startSym = file[file.Length - 2];
                    Console.WriteLine("Debug: "+(int)(mostCommon));
                    byte endSym = file[file.Length - 1];
                    byte[] inputFile = new byte[file.Length-3];
                    for (int i = 0; i < inputFile.Length; i++)
                        inputFile[i] = file[i];
                    ByteArrayToFile(args[2], byteUnarchieve(inputFile, mostCommon, startSym, endSym));
                }
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Грешен синтаксис при избиране на операция. Използваите a име-на-файла.разширение име-на-архива.разширение за да архивирате. За разархивиране използвайте r име-на-архива.разширение име-на-файла-разширение. Този софтуер е с отворен код. Създател Георги Бочев (zxz)");
            }
        }
    }
}

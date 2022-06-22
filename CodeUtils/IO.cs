// Comments:
// [External Class] sBotics.__sBotics__GameSceneManager
//     ↪ Manages important UI objects present in the "Game Scene"

using UnityEngine;
using System.IO;
using System;

namespace sBotics
{
    namespace CodeUtils
    {
        public static class IO
        {
            public static string WrittenLogPath
            {
                get => Path.Combine(Application.streamingAssetsPath, "WrittenLog.txt");
            }

            public static bool Timestamp = true;

            public static void ClearWrite() => Write(string.Empty);

            public static void ClearPrint() => Print(string.Empty);

            public static void OpenConsole() => __sBotics__GameSceneManager.instance.OpenConsole();

            public static void Write(string text)
            {
                try
                {
                    string path = WrittenLogPath;
                
                    if(File.Exists(path))
                    {
                        File.Delete(path);
                        File.CreateText(path);
                    }
                }
                catch (IOException e) 
                {
                    Debug.Log($"The file could not be recreated: {e.Message}");
                }

                WriteLine(text);
            }

            public static void WriteLine(string text)
            {
                if(!string.IsNullOrEmpty(text))
                    text = StampTime(text);

                try
                {
                    string path = WrittenLogPath;

                    if(!File.Exists(path))
                    {
                        Write(text);
                        return;
                    }

                    using (StreamWriter outputFile = new StreamWriter(path, true))
                    {
                        outputFile.WriteLine(text);
                    }
                }
                catch (IOException e) 
                {
                    Debug.Log($"The file could not be saved: {e.Message}");
                }
            }

            public static void Print(string text)
            {
                if(!__sBotics__GameSceneManager.instance)
                    return;
                                
                __sBotics__GameSceneManager.Console = string.IsNullOrEmpty(text) ? text : StampTime(text);
            }

            public static void PrintLine(string text) =>
                Print(text + '\n' + __sBotics__GameSceneManager.Console);

            static string StampTime(string text) => Timestamp ? $"[{DateTime.Now.ToString("HH:mm:ss")}] {text}" : text;
        }
    }
}

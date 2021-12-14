using System;
using System.Collections;

namespace Array_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo5();
            Console.ReadKey();
        }
        /// <summary>
        /// 需要移位的插入
        /// </summary>
        static void Demo1()
        {
            string[] arr = new string[10];
            arr[0] = "a";
            arr[1] = "b";
            arr[2] = "c";
            arr[3] = "d";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
            Console.WriteLine("");
            //插入前需要移位最优时间复杂度O(1)最差O(n)
            arr[4] = arr[3];
            arr[3] = arr[2];
            arr[2] = "x";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
        }
        /// <summary>
        /// 只移位插入未元素的插入
        /// </summary>
        static void Demo2()
        {
            string[] arr = new string[10];
            arr[0] = "a";
            arr[1] = "b";
            arr[2] = "c";
            arr[3] = "d";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
            Console.WriteLine("");
            //时间复杂度是O(1)
            arr[4] = arr[2];
            arr[2] = "x";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
        }
        /// <summary>
        /// 移位删除
        /// </summary>
        static void Demo4()
        {
            string[] arr = new string[10];
            arr[0] = "a";
            arr[1] = "b";
            arr[2] = "c";
            arr[3] = "d";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
            Console.WriteLine("");
            arr[0] = arr[1];
            arr[1] = arr[2];
            arr[2] = arr[3];
            arr[3] = null;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
        }
        /// <summary>
        ///标记删除当空间存储满时在触发真正删除
        /// </summary>
        static void Demo5()
        {
            string[] arr = new string[10];
            arr[0] = "a";
            arr[1] = "b";
            arr[2] = "c";
            arr[3] = "d";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
            Console.WriteLine("");
            //只是标记删除这样不进行移位操作
            arr[0] = "delete";
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] == "delete" ? "" : arr[i] + ",");
            }
            Console.WriteLine("");
            arr[4] = "e";
            arr[5] = "f";
            //当存储空间满了的时候在执行删除操作
            string[] arrT = new string[10];
            int iT = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "delete")
                {
                    arrT[iT] = arr[i];
                    iT++;
                }
            }
            arr = arrT;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + ",");
            }
        }
    }
}

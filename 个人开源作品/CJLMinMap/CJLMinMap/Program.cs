using System;
using System.Linq;
using System.Collections.Generic;

namespace CJLMinMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
            Console.ReadLine();
        }
        /// <summary>
        /// 简单转换
        /// </summary>
        static void Demo1()
        {
            ObjeDto objeDto = new ObjeDto()
            {
                c1 = "c1",
                c2 = 22,
                c3 = 3.3f,
                c4 = 4.4,
                c5 = new ObjeDto_Sub()
                {
                    c1 = "c1",
                    c2 = "c2"
                }
            };
            Obje2 obj2 = objeDto.ToTransition<Obje2>();
        }
        static void Demo2()
        {
            var maps = new List<(string, string, Func<object, object>)>() {
                 ("d1","c1",(object source)=>{
                    return source.ToString();
                 }),
                 ("d2","c2",null),
                 ("d4","c4",null),
                 ("e5","c5",(object source)=>{
                    Obj4_sub data=source as Obj4_sub;
                    return data.c1+data.c2;
                 }),
            };
            List<Obj3> obj1s = new List<Obj4>()
            {
                new Obj4(){
                d1=11,
                d2="d2",
                c3="c3",
                d4="d4",
                e5=new Obj4_sub()
                {
                    c1="c1",
                    c2="c2"
                }
            }
            }.ToTransition<List<Obj3>>(maps);

        }
    }
    class ObjeDto
    {
        public string c1 { get; set; }
        public int c2 { get; set; }
        public float c3 { get; set; }
        public double c4 { get; set; }
        public ObjeDto_Sub c5 { get; set; }
    }
    class Obje2
    {
        public string c1 { get; set; }
        public int c2 { get; set; }
        public float c3 { get; set; }
        public double c4 { get; set; }
        public ObjeDto_Sub c5 { get; set; }
    }
    class ObjeDto_Sub
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
    }
    class Obj3
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
    }
    class Obj4
    {
        public int d1 { get; set; }
        public string d2 { get; set; }
        public string c3 { get; set; }
        public string d4 { get; set; }
        public Obj4_sub e5 { get; set; }
    }
    class Obj4_sub
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo3();
        }

        //需求:筛选出LastModificationTime发生变化的数据
        static void Demo1()
        {
            //当前这一刻的数据
            List<NorthTerminal> nowData = new List<NorthTerminal>();
            //上一时刻的数据
            List<NorthTerminal> befoData = new List<NorthTerminal>();
            //第一种通过连接后在去判断
            var changeData1 = nowData.Join(befoData, a => a.Id, b => b.Id, (a, b) =>
               new
               {
                   nowData = a,
                   befoData = b
               }).Where(t => t.nowData.LastModificationTime != t.befoData.LastModificationTime);
            //第二种每一项去查询后进行比对
            var changeData2 = nowData.Where(t => new Func<bool>(() =>
            {
                var data = befoData.Where(a => a.Id == t.Id).FirstOrDefault();
                //befoData未查询表示是新增数据
                return data == null ? false : t.LastModificationTime != data.LastModificationTime;
            })());
        }
        static void Demo2()
        {
            //当前这一刻的数据
            List<NorthTerminal> nowData = new List<NorthTerminal>() {
                new NorthTerminal(){
                    Id=1,
                    LastModificationTime="1",
                },new NorthTerminal(){
                    Id=2,
                    LastModificationTime="1",
                },new NorthTerminal(){
                    Id=3,
                    LastModificationTime="1",
                },
            };
            //上一时刻的数据
            List<NorthTerminal> befoData = new List<NorthTerminal>(){
                new NorthTerminal(){
                    Id=1,
                    LastModificationTime="1",
                },new NorthTerminal(){
                    Id=2,
                    LastModificationTime="1",
                },new NorthTerminal(){
                    Id=4,
                    LastModificationTime="1",
                },
            };
            var changeData1 = nowData.Join(befoData, a => a.Id, b => b.Id, (a, b) =>
                 new
                 {
                     nowData = a,
                     befoData = b
                 });
        }
        static void Demo3()
        {
            List<string> list = new List<string>() {
                "1","2","3"
            };
            var listWhere = list.Where(t => t == "1");
            listWhere.ToList();
            Console.ReadKey();
        }
    }


    class NorthTerminal
    {
        public int Id { get; set; }
        public string LastModificationTime { get; set; }
    }
}

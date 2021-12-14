using System;

namespace MementoPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            SnapshotHolder snapshotHolder = new SnapshotHolder();
            InputText inputText = new InputText();
            inputText.Append("输入值1");
            inputText.Append("输入值2");
            Snapshot snapshot = inputText.CreateSnapshot();
            snapshotHolder.PushSnapshot(snapshot);
            inputText.Append("输入值3");
            Console.WriteLine(inputText.GetText());
            inputText.RestoreSnapshot(snapshotHolder.PopSnapshot());
            Console.WriteLine(inputText.GetText());
            Console.Read();
        }
        /// <summary>
        /// 备忘录模式
        /// </summary>
        static void Demo1()
        {
            Originator o = new Originator();
            o.State = "On";
            o.Show();
            Caretaker c = new Caretaker();
            c.Memento = o.CreateMemento();
            o.State = "Off";
            o.Show();
            o.SetMemento(c.Memento);
            o.Show();
        }
        /// <summary>
        /// 文本使用栈存储备忘录
        /// </summary>
        static void Demo2()
        {
            SnapshotHolder snapshotHolder = new SnapshotHolder();
            InputText inputText = new InputText();
            inputText.Append("输入值1");
            inputText.Append("输入值2");
            Snapshot snapshot = inputText.CreateSnapshot();
            snapshotHolder.PushSnapshot(snapshot);
            inputText.Append("输入值3");
            Console.WriteLine(inputText.GetText());
            inputText.RestoreSnapshot(snapshotHolder.PopSnapshot());
            Console.WriteLine(inputText.GetText());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MementoPattern_CJL
{
    //待备份类
    public class InputText
    {
        private StringBuilder sb = new StringBuilder();
        public string GetText()
        {
            return sb.ToString();
        }
        public Snapshot CreateSnapshot()
        {
            return new Snapshot(GetText());
        }
        public void Append(string input)
        {
            sb.Append(input);
        }
        public void RestoreSnapshot(Snapshot snapshot)
        {
            this.sb = new StringBuilder();
            this.sb.Append(snapshot.GetText());
        }
    }
    /// <summary>
    /// 备忘录
    /// </summary>
    public class Snapshot
    {
        private string text;
        public Snapshot(string text)
        {
            this.text = text;
        }
        public string GetText()
        {
            return this.text;
        }
    }
    /// <summary>
    /// 存储栈
    /// </summary>
    public class SnapshotHolder
    {
        private Stack<Snapshot> snapshots = new Stack<Snapshot>();
        public Snapshot PopSnapshot()
        {
            return snapshots.Pop();
        }
        public void PushSnapshot(Snapshot snapshot)
        {
            snapshots.Push(snapshot);
        }
    }
}

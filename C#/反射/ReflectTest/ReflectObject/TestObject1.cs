using System;

namespace ReflectObject
{
    public class TestObject1
    {

        public TestObject1(int i)
        {

        }
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        public bool Property3 { get; set; }
        public ReflectObject2.TestObject1 obj1 { get; set; }
        public void Func1()
        {
            obj1 = new ReflectObject2.TestObject1()
            {
                Property1 = 1,
                Property2 = "1",
                Property3 = true
            };
        }
        public void Func2()
        {
            Console.WriteLine("Func2");
        }
    }
}

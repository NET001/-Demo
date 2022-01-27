using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.Globalization;
using System.Xml;

public class Program
{
    static void Main(string[] args)
    {
        Demo10();
        Console.ReadKey();
    }
    /// <summary>
    /// 以键值对的方式注册配置(配置系统就是一个键值对)
    /// </summary>
    static void Demo1()
    {
        Dictionary<string, string> source1 = new Dictionary<string, string>
        {
            ["longDatePattern1"] = "longDatePattern1",
            ["longTimePattern1"] = "longTimePattern1",
            ["shortDatePattern1"] = "shortDatePattern1",
            ["shortTimePattern1"] = "shortTimePattern1"
        };
        Dictionary<string, string> source2 = new Dictionary<string, string>
        {
            ["longDatePattern2"] = "longDatePattern2",
            ["longTimePattern2"] = "longTimePattern2",
            ["shortDatePattern2"] = "shortDatePattern2",
            ["shortTimePattern2"] = "shortTimePattern2"
        };
        //构建一个配置实例
        IConfigurationRoot config = new ConfigurationBuilder()
            //添加配置源可以添加多个(内存数据源)
            .Add(new MemoryConfigurationSource() { InitialData = source1 })
            .Add(new MemoryConfigurationSource() { InitialData = source2 })
            .Build();
        //从配置实例中获取
        Console.WriteLine($"LongDatePattern: {config["LongDatePattern1"]}");
        Console.WriteLine($"LongTimePattern: {config["LongTimePattern2"]}");
        Console.WriteLine($"ShortDatePattern: {config["ShortDatePattern1"]}");
        Console.WriteLine($"ShortTimePattern: {config["ShortTimePattern2"]}");
    }
    /// <summary>
    /// 进行具有结构化的数据配置和读取
    /// </summary>
    static void Demo2()
    {
        //结构化的数据核心还是键值对只是有用了一个具有路径的表示方法
        Dictionary<string, string> source = new Dictionary<string, string>
        {
            ["format:dateTime:longDatePattern"] = "123",
            ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
            ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
            ["format:dateTime:shortTimePattern"] = "h:mm tt",

            ["format:currencyDecimal:digits"] = "2",
            ["format:currencyDecimal:symbol"] = "$",
        };
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .Add(new MemoryConfigurationSource { InitialData = source })
            //或者
            //.AddInMemoryCollection(source)
            .Build();
        //获取结构化数据并转换为实体,只有Get可以,GetValue不行
        FormatOptions formatOptions = configuration.GetSection("Format").Get<FormatOptions>();
        DateTimeFormatOptions dateTimeFormatOptions1 = configuration.GetSection("Format").GetSection("DateTime").Get<DateTimeFormatOptions>();
        //为null
        DateTimeFormatOptions dateTimeFormatOptions2 = configuration.GetSection("Format").GetValue<DateTimeFormatOptions>("DateTime");
    }
    /// <summary>
    /// 进行集合和数组的绑定
    /// </summary>
    static void Demo3()
    {
        var source = new Dictionary<string, string>
        {
            ["foo:gender"] = "男",
            ["foo:age"] = "18",
            ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            ["foo:contactInfo:phoneNo"] = "123",

            ["foo:gender"] = "男",
            ["foo:age"] = "18",
            ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            ["foo:contactInfo:phoneNo"] = "123",

            ["bar:gender"] = "Male",
            ["bar:age"] = "25",
            ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
            ["bar:contactInfo:phoneNo"] = "456",

            ["abc:gender"] = "Male",
            ["abc:age"] = "25",
            ["abc:contactInfo:emailAddress"] = "bar@outlook.com",
            ["abc:contactInfo:phoneNo"] = "456",

            ["baz:gender"] = "Female",
            ["baz:age"] = "36",
            ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
            ["baz:contactInfo:phoneNo"] = "789"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(source)
            .Build();
        IEnumerable<Profile> collection1 = configuration.Get<IEnumerable<Profile>>();
        //和IEnumerable有些区别
        Profile[] collection2 = configuration.Get<Profile[]>();

        source = new Dictionary<string, string>
        {
            ["foo:gender"] = "Male",
            ["foo:age"] = "18",
            ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
            ["foo:contactInfo:phoneNo"] = "123",

            ["bar:gender"] = "Male",
            ["bar:age"] = "25",
            ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
            ["bar:contactInfo:phoneNo"] = "456",

            ["baz:gender"] = "Female",
            ["baz:age"] = "36",
            ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
            ["baz:contactInfo:phoneNo"] = "789"
        };

        configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(source)
           .Build();
        //字典的key不能重复
        var profiles = configuration.Get<IDictionary<string, Profile>>();
    }

    /// <summary>
    /// 获取json文件的配置源
    /// </summary>
    static void Demo4()
    {
        FormatOptions format = new ConfigurationBuilder()
            //添加json文件配置源,默认会在执行路径下进行搜索
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("format")
            .Get<FormatOptions>();
    }

    /// <summary>
    /// 监听变更（程序启动后修改启动程序exe同级目录下的appsettings.json的内容）
    /// </summary>
    static void Demo5()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        ChangeToken.OnChange(() => config.GetReloadToken(), () =>
        {
            Console.WriteLine("数据源发生了变化");
            var options = config.GetSection("format").Get<FormatOptions>();
            var dateTime = options.DateTime;
            var currencyDecimal = options.CurrencyDecimal;
            Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");
            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}\n\n");
        });
    }
    /// <summary>
    /// 基于标量
    /// </summary>
    static void Demo6()
    {
        Dictionary<string, string> source = new Dictionary<string, string>
        {
            ["format:dateTime:longDatePattern"] = "123",
            ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
            ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
            ["format:dateTime:shortTimePattern"] = "h:mm tt",

            ["format:currencyDecimal:digits"] = "2",
            ["format:currencyDecimal:symbol"] = "$",
        };
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .Add(new MemoryConfigurationSource { InitialData = source })
            .Build();
        //根据路径进行查询
        IConfigurationSection subConfiguration = configuration.GetSection("format").GetSection("dateTime");
        //进行标量查询(能够进行自动转换,转换错误会报错)
        Console.WriteLine(subConfiguration.GetValue<object>("longDatePattern"));
        Console.WriteLine(subConfiguration.GetValue<string>("longDatePattern"));
        Console.WriteLine(subConfiguration.GetValue<int>("longDatePattern"));
        //无法转换会报错
        Console.WriteLine(subConfiguration.GetValue<int>("longTimePattern"));
    }
    /// <summary>
    /// 自定义转换规则
    /// </summary>
    static void Demo7()
    {
        var source = new Dictionary<string, string>
        {
            ["point"] = "(123,456)"
        };

        var root = new ConfigurationBuilder()
            .AddInMemoryCollection(source)
            .Build();
        //只有GetValue才可以进行转换,GetValue只能基础类型,要转换成类要编写转换特性类
        var point = root.GetValue<Point>("point");
        Console.WriteLine(point.X + "" + point.Y);
    }
    /// <summary>
    /// 环境变量的
    /// </summary>
    static void Demo8()
    {
        Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
        Environment.SetEnvironmentVariable("TEST_AGE", "18");
        Environment.SetEnvironmentVariable("TEST_CONTACTINFO:EMAILADDRESS", "foobar@outlook.com");
        Environment.SetEnvironmentVariable("TEST_CONTACTINFO:PHONENO", "123456789");
        var profile = new ConfigurationBuilder()
            .AddEnvironmentVariables("TEST_")
            .Build()
            .Get<Profile>();
    }
    /// <summary>
    /// 命令行的
    /// </summary>
    /// <param name="args"></param>
    static void Demo9(string[] args)
    {
        var mapping = new Dictionary<string, string>
        {
            ["-a"] = "architecture",
            ["-arch"] = "architecture"
        };
        var configuration = new ConfigurationBuilder()
            .AddCommandLine(args, mapping)
            .Build();
    }
    /// <summary>
    /// 从Xml中获取
    /// </summary>
    static void Demo10()
    {
        var configuration = new ConfigurationBuilder()
            .AddExtendedXmlFile("appsettings.xml")
            .Build();
        var collection = configuration.Get<IEnumerable<Profile>>();
    }

}
#region 实体
public class FormatOptions
{
    public DateTimeFormatOptions DateTime { get; set; }
    public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }
}
public class DateTimeFormatOptions
{
    public string LongDatePattern { get; set; }
    public string LongTimePattern { get; set; }
    public string ShortDatePattern { get; set; }
    public string ShortTimePattern { get; set; }

}
public class CurrencyDecimalFormatOptions
{
    public int Digits { get; set; }
    public string Symbol { get; set; }
}

public class Profile
{
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public ContactInfo ContactInfo { get; set; }

}
public enum Gender
{
    Male,
    Female
}
public class ContactInfo
{
    public string EmailAddress { get; set; }
    public string PhoneNo { get; set; }
}
#endregion

#region 转换规则
public class PointTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    => sourceType == typeof(string);
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        string[] split = value.ToString().Split(',');
        double x = double.Parse(split[0].Trim().TrimStart('('));
        double y = double.Parse(split[1].Trim().TrimEnd(')'));
        return new Point { X = x, Y = y };
    }
}

[TypeConverter(typeof(PointTypeConverter))]
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
}
#endregion


#region 实现数据源读取
public class ExtendedXmlConfigurationSource : XmlConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new ExtendedXmlConfigurationProvider(this);
    }
}
public static class ExtendedXmlConfigurationExtensions
{
    public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path)
            => builder.AddExtendedXmlFile(path, false, false);
    public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path, bool optional)
            => builder.AddExtendedXmlFile(path, optional, false);
    public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        builder.Add(new ExtendedXmlConfigurationSource { Path = path, Optional = optional, ReloadOnChange = reloadOnChange });
        return builder;
    }
}
public class ExtendedXmlConfigurationProvider : XmlConfigurationProvider
{
    public ExtendedXmlConfigurationProvider(XmlConfigurationSource source) :
        base(source)
    { }

    public override void Load(Stream stream)
    {
        //加载源文件并创建一个XmlDocument        
        var sourceDoc = new XmlDocument();
        sourceDoc.Load(stream);

        //添加索引
        AddIndexes(sourceDoc.DocumentElement);

        //根据添加的索引创建一个新的XmlDocument
        var newDoc = new XmlDocument();
        var documentElement =
            newDoc.CreateElement(sourceDoc.DocumentElement.Name);
        newDoc.AppendChild(documentElement);

        foreach (XmlElement element in sourceDoc.DocumentElement.ChildNodes)
        {
            Rebuild(element, documentElement,
                name => newDoc.CreateElement(name));
        }

        //根据新的XmlDocument初始化配置字典
        using (Stream newStream = new MemoryStream())
        {
            using (XmlWriter writer = XmlWriter.Create(newStream))
            {
                newDoc.WriteTo(writer);
            }
            newStream.Position = 0;
            base.Load(newStream);
        }
    }

    private void AddIndexes(XmlElement element)
    {
        if (element.ChildNodes.OfType<XmlElement>().Count() > 1)
        {
            if (element.ChildNodes.OfType<XmlElement>()
                .GroupBy(it => it.Name).Count() == 1)
            {
                var index = 0;
                foreach (XmlElement subElement in element.ChildNodes)
                {
                    subElement.SetAttribute("append_index", (index++).ToString());
                    AddIndexes(subElement);
                }
            }
        }
    }

    private void Rebuild(XmlElement source, XmlElement destParent,
        Func<string, XmlElement> creator)
    {
        var index = source.GetAttribute("append_index");
        var elementName = string.IsNullOrEmpty(index) ? source.Name : $"{source.Name}_index_{index}";
        var element = creator(elementName);
        destParent.AppendChild(element);
        foreach (XmlAttribute attribute in source.Attributes)
        {
            if (attribute.Name != "append_index")
            {
                element.SetAttribute(attribute.Name, attribute.Value);
            }
        }

        foreach (XmlElement subElement in source.ChildNodes)
        {
            Rebuild(subElement, element, creator);
        }
    }
}



#endregion
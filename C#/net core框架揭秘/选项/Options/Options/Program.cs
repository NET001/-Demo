using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections;
using System.Text;

class Program
{
    static void Main()
    {
        Demo7();
        Console.Read();
    }
    /// <summary>
    /// 将配置绑定为Options对象
    /// </summary>
    static void Demo1()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()
            {
                ["Gender"] = "Male",
                ["Age"] = "18",
                ["ContactInfo:EmailAddress"] = "foo@outlook.com",
                ["ContactInfo:PhoneNo"] = "123",
            })
           .Build();
        var profile = new ServiceCollection()
            .AddOptions()
            //配置绑定
            .Configure<Profile>(configuration)
            //获取容器对象
            .BuildServiceProvider()
            //获取配置
            .GetRequiredService<IOptions<Profile>>()
            .Value;
        Console.WriteLine($"Gender: {profile.Gender}");
        Console.WriteLine($"Age: {profile.Age}");
        Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
        Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}");
    }
    /// <summary>
    /// 配置多个
    /// </summary>
    static void Demo2()
    {
        var configuration = new ConfigurationBuilder()
               .AddJsonFile("profile.json")
               .Build();

        var serviceProvider = new ServiceCollection()
            .AddOptions()
            //配置多个选项
            .Configure<Profile>("foo", configuration.GetSection("foo"))
            .Configure<Profile>("bar", configuration.GetSection("bar"))
            .BuildServiceProvider();
        var optionsAccessor = serviceProvider
            .GetRequiredService<IOptionsSnapshot<Profile>>();
        Print(optionsAccessor.Get("foo"));
        Print(optionsAccessor.Get("bar"));

        static void Print(Profile profile)
        {
            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
        }
    }
    /// <summary>
    /// 监事选项变更
    /// </summary>
    static void Demo3()
    {
        var configuration = new ConfigurationBuilder()
               .AddJsonFile(path: "profile1.json", optional: false, reloadOnChange: true)
               .Build();
        new ServiceCollection()
            .AddOptions()
            .Configure<Profile>(configuration)
            .BuildServiceProvider()
            //Monitor（监视器）
            .GetRequiredService<IOptionsMonitor<Profile>>()
            //文件修改后触发
            .OnChange(profile =>
            {
                Console.WriteLine($"Gender: {profile.Gender}");
                Console.WriteLine($"Age: {profile.Age}");
                Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
            });
    }
    /// <summary>
    /// 直接配置选项,不指定配置源
    /// </summary>
    static void Demo4()
    {
        var profile = new ServiceCollection()
                    .AddOptions()
                    .Configure<Profile>(it =>
                    {
                        it.Gender = Gender.Male;
                        it.Age = 18;
                        it.ContactInfo = new ContactInfo
                        {
                            PhoneNo = "123456789",
                            EmailAddress = "foobar@outlook.com"
                        };
                    })
                    .BuildServiceProvider()
                    .GetRequiredService<IOptions<Profile>>()
                    .Value;
        Console.WriteLine($"Gender: {profile.Gender}");
        Console.WriteLine($"Age: {profile.Age}");
        Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
        Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
    }
    /// <summary>
    /// 配置多个带名称,不指定配置源
    /// </summary>
    static void Demo5()
    {
        var optionsAccessor = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", it =>
                {
                    it.Gender = Gender.Male;
                    it.Age = 18;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123",
                        EmailAddress = "foo@outlook.com"
                    };
                })
                .Configure<Profile>("bar", it =>
                {
                    it.Gender = Gender.Female;
                    it.Age = 25;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "456",
                        EmailAddress = "bar@outlook.com"
                    };
                })
                .BuildServiceProvider()
                //Snapshot（快照）
                .GetRequiredService<IOptionsSnapshot<Profile>>();

        Print(optionsAccessor.Get("foo"));
        Print(optionsAccessor.Get("bar"));

        static void Print(Profile profile)
        {
            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
        };
    }
    /// <summary>
    /// 使用OptionsBuilder进行配置
    /// </summary>
    static void Demo6()
    {
        var services = new ServiceCollection();
        services
            .AddSingleton<Tuple<string>>(new Tuple<string>("输出字符串"))
            .AddOptions<Profile>()
            //从容器中获取实例
            .Configure<Tuple<string>>((option, str) =>
            {
                option.Gender = Gender.Male;
                option.Age = 18;
                option.ContactInfo = new ContactInfo
                {
                    PhoneNo = str.Item1,
                    EmailAddress = str.Item1
                };
            });
        var profile = services.BuildServiceProvider()
           .GetRequiredService<IOptions<Profile>>()
           .Value;
        Console.WriteLine($"Gender: {profile.Gender}");
        Console.WriteLine($"Age: {profile.Age}");
        Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
        Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
    }
    /// <summary>
    /// 使用OptionsBuilder进行option验证
    /// </summary>
    static void Demo7()
    {
        var services = new ServiceCollection();
        services.AddOptions<Profile>()
            .Configure(options =>
            {
                options.Gender = Gender.Male;
                options.Age = 18;
                options.ContactInfo = new ContactInfo
                {
                    PhoneNo = "PhoneNo",
                    EmailAddress = "EmailAddress",
                };
            })
            .Validate(options => false, "验证不通过消息在异常中");
        try
        {
            var profile = services
                .BuildServiceProvider()
                .GetRequiredService<IOptions<Profile>>().Value;
            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// Options的生命周期
    /// </summary>
    static void Demo8()
    {
        var random = new Random();
        var serviceProvider = new ServiceCollection()
            .AddOptions()
            .Configure<FoobarOptions>(foobar =>
            {
                foobar.Foo = random.Next(1, 100);
                foobar.Bar = random.Next(1, 100);
            })
            .BuildServiceProvider();

        Print(serviceProvider);
        Print(serviceProvider);

        static void Print(IServiceProvider provider)
        {
            var scopedProvider = provider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider;

            var options = scopedProvider
                .GetRequiredService<IOptions<FoobarOptions>>()
                .Value;
            var optionsSnapshot1 = scopedProvider
                .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                .Value;
            var optionsSnapshot2 = scopedProvider
                .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                .Value;
            Console.WriteLine($"options:{options}");
            Console.WriteLine($"optionsSnapshot1:{optionsSnapshot1}");
            Console.WriteLine($"optionsSnapshot2:{optionsSnapshot2}\n");
        }
    }
    /// <summary>
    /// 自定义IConfigureOptions实现类型设置Options
    /// </summary>
    static void Demo9()
    {
        var foobar1 = new FoobarOptions(1, 1);
        var foobar2 = new FoobarOptions(2, 2);
        var foobar3 = new FoobarOptions(3, 3);

        var options = new ServiceCollection()
            .AddOptions()
            .Configure<FakeOptions>("fakeoptions.json")
            .BuildServiceProvider()
            .GetRequiredService<IOptions<FakeOptions>>()
            .Value;

        Console.WriteLine(options.Foobar.Equals(foobar1));

        Console.WriteLine(options.Array[0].Equals(foobar1));
        Console.WriteLine(options.Array[1].Equals(foobar2));
        Console.WriteLine(options.Array[2].Equals(foobar3));

        Console.WriteLine(options.List[0].Equals(foobar1));
        Console.WriteLine(options.List[1].Equals(foobar2));
        Console.WriteLine(options.List[2].Equals(foobar3));

        Console.WriteLine(options.Dictionary["1"].Equals(foobar1));
        Console.WriteLine(options.Dictionary["2"].Equals(foobar2));
        Console.WriteLine(options.Dictionary["3"].Equals(foobar3));
    }
    /// <summary>
    /// 实现事件
    /// </summary>
    static void Demo10()
    {
        var random = new Random();
        var optionsMonitor = new ServiceCollection()
            .AddOptions()
            .Configure<FoobarOptions>(TimeSpan.FromSeconds(1))
            .Configure<FoobarOptions>(foobar =>
            {
                foobar.Foo = random.Next(10, 100);
                foobar.Bar = random.Next(10, 100);
            })
            .BuildServiceProvider()
            .GetRequiredService<IOptionsMonitor<FoobarOptions>>();
        optionsMonitor.OnChange(foobar => Console.WriteLine($"[{DateTime.Now}]{foobar}"));
    }
}

#region 实体

public class Profile : IEquatable<Profile>
{
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public ContactInfo ContactInfo { get; set; }

    public Profile() { }
    public Profile(Gender gender, int age, string emailAddress, string phoneNo)
    {
        Gender = gender;
        Age = age;
        ContactInfo = new ContactInfo
        {
            EmailAddress = emailAddress,
            PhoneNo = phoneNo
        };
    }
    public bool Equals(Profile other)
    {
        return other == null
            ? false
            : Gender == other.Gender &&
              Age == other.Age &&
              ContactInfo.Equals(other.ContactInfo);
    }
}
public enum Gender
{
    Male,
    Female
}
public class ContactInfo : IEquatable<ContactInfo>
{
    public string EmailAddress { get; set; }
    public string PhoneNo { get; set; }
    public bool Equals(ContactInfo other)
    {
        return other == null
           ? false
           : EmailAddress == other.EmailAddress && PhoneNo == other.PhoneNo;
    }
}
public class FoobarOptions
{
    public int Foo { get; set; }
    public int Bar { get; set; }
    public FoobarOptions(int foo, int bar)
    {
        Foo = foo;
        Bar = bar;
    }
    public override string ToString() => $"Foo:{Foo}, Bar:{Bar}";
    public bool Equals(FoobarOptions other) => this.Foo == other?.Foo && this.Bar == other?.Bar;

}
#endregion
#region 自定义IConfigureOptions实现类型设置Options
internal static class Extensions
{
    public static bool IsDictionary(this Type type)
        => type.IsGenericType && typeof(IDictionary).IsAssignableFrom(type) && type.GetGenericArguments().Length == 2;
    public static bool IsCollection(this Type type)
        => typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
    public static bool IsArray(this Type type)
        => typeof(Array).IsAssignableFrom(type);
}
public class FakeOptions
{
    public FoobarOptions Foobar { get; set; }
    public FoobarOptions[] Array { get; set; }
    public IList<FoobarOptions> List { get; set; }
    public IDictionary<string, FoobarOptions> Dictionary { get; set; }
}


public class JsonFileConfigureOptions<TOptions> : IConfigureNamedOptions<TOptions> where TOptions : class, new()
{
    private readonly IFileProvider _fileProvider;
    private readonly string _path;
    private readonly string _name;

    public JsonFileConfigureOptions(string name, string path, IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _path = path;
        _name = name;
    }

    public void Configure(string name, TOptions options)
    {
        if (name != null && _name != name)
        {
            return;
        }

        byte[] bytes;
        using (var stream = _fileProvider.GetFileInfo(_path).CreateReadStream())
        {
            bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
        }

        var contents = Encoding.Default.GetString(bytes);
        contents = contents.Substring(contents.IndexOf('{'));
        var newOptions = JsonConvert.DeserializeObject<TOptions>(contents);
        Bind(newOptions, options);
    }

    public void Configure(TOptions options) => Configure(Options.DefaultName, options);

    private void Bind(object from, object to)
    {
        var type = from.GetType();
        if (type.IsDictionary())
        {
            var dest = (IDictionary)to;
            var src = (IDictionary)from;
            foreach (var key in src.Keys)
            {
                dest.Add(key, src[key]);
            }
            return;
        }

        if (type.IsCollection())
        {
            var dest = (IList)to;
            var src = (IList)from;
            foreach (var item in src)
            {
                dest.Add(item);
            }
        }

        foreach (var property in type.GetProperties())
        {
            if (property.IsSpecialName || property.GetMethod == null || property.Name == "Item" || property.DeclaringType != type)
            {
                continue;
            }

            var src = property.GetValue(from);
            var propertyType = src?.GetType() ?? property.PropertyType;

            if ((propertyType.IsValueType || src is string || src == null) && property.SetMethod != null)
            {
                property.SetValue(to, src);
                continue;
            }

            var dest = property.GetValue(to);
            if (null != dest && !propertyType.IsArray())
            {
                Bind(src, dest);
                continue;
            }

            if (property.SetMethod != null)
            {
                var destType = propertyType.IsDictionary()
                    ? typeof(Dictionary<,>).MakeGenericType(propertyType.GetGenericArguments())
                    : propertyType.IsArray()
                    ? typeof(List<>).MakeGenericType(propertyType.GetElementType())
                    : propertyType.IsCollection()
                    ? typeof(List<>).MakeGenericType(propertyType.GetGenericArguments())
                    : propertyType;

                dest = Activator.CreateInstance(destType);
                Bind(src, dest);

                if (propertyType.IsArray())
                {
                    IList list = (IList)dest;
                    dest = Array.CreateInstance(propertyType.GetElementType(),
                        list.Count);
                    list.CopyTo((Array)dest, 0);
                }
                property.SetValue(to, src);
            }
        }
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string filePath, string basePath = null) where TOptions : class, new()
    => services.Configure<TOptions>(Options.DefaultName, filePath, basePath);

    public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, string filePath, string basePath = null) where TOptions : class, new()
    {
        var fileProvider = string.IsNullOrEmpty(basePath)
            ? new PhysicalFileProvider(Directory.GetCurrentDirectory())
            : new PhysicalFileProvider(basePath);

        return services.AddSingleton<IConfigureOptions<TOptions>>(new JsonFileConfigureOptions<TOptions>(name, filePath, fileProvider));
    }
}

#endregion
#region 实现事件
public static class ServiceCollectionExtensionsToken
{
    public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, TimeSpan refreshInterval)
       => services.AddSingleton<IOptionsChangeTokenSource<TOptions>>(new TimedRefreshTokenSource<TOptions>(refreshInterval, name));
    public static IServiceCollection Configure<TOptions>(this IServiceCollection services, TimeSpan refreshInterval)
       => services.Configure<TOptions>(Options.DefaultName, refreshInterval);
}


public class TimedRefreshTokenSource<TOptions> : IOptionsChangeTokenSource<TOptions>
{
    private OptionsChangeToken _changeToken;
    public string Name { get; }

    public TimedRefreshTokenSource(TimeSpan interval, string name)
    {
        Name = name ?? Options.DefaultName;
        _changeToken = new OptionsChangeToken();
        ChangeToken.OnChange(() => new CancellationChangeToken(new CancellationTokenSource(interval).Token),
            () =>
            {
                var previous = Interlocked.Exchange(ref _changeToken, new OptionsChangeToken());
                previous.OnChange();
            });
    }
    public IChangeToken GetChangeToken() => _changeToken;

    private class OptionsChangeToken : IChangeToken
    {
        private readonly CancellationTokenSource _tokenSource;

        public OptionsChangeToken() => _tokenSource = new CancellationTokenSource();
        public bool HasChanged => _tokenSource.Token.IsCancellationRequested;
        public bool ActiveChangeCallbacks => true;
        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => _tokenSource.Token.Register(callback, state);
        public void OnChange() => _tokenSource.Cancel();
    }
}
#endregion

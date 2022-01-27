using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

class Program
{
    static void Main()
    {
        Demo2();
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

#endregion

using Microsoft.Extensions.FileProviders;
using System;

class Program
{
    static void Main()
    {
        IFileProvider fileProvider = new PhysicalFileProvider(@"c:\test");
        foreach (var fileInfo in fileProvider.GetDirectoryContents(""))
        {
            if (fileInfo.IsDirectory)
            {
                Console.WriteLine(fileInfo.Name);
            }
        }
    }
}
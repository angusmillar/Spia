using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Spia.Runner.SupportExtensions
{
  public static class DirectoryExtension
  {
    public static void DeleteAllFiles(this DirectoryInfo DirInfo, string Extension = null)
    {      
      if (Extension != null)
      {
        foreach (FileInfo file in DirInfo.GetFiles($"*{Extension}"))
        {
          file.Delete();
        }
      }
      else
      {
        foreach (FileInfo file in DirInfo.GetFiles())
        {
          file.Delete();
        }
      }
      
    }

    

    public static void DeleteAllDirectories(this DirectoryInfo DirInfo)
    {
      foreach (DirectoryInfo ChildDirInfo in DirInfo.GetDirectories())
      {
        ChildDirInfo.Delete();
      }
    }

    public static void DeleteAllContents(this DirectoryInfo DirInfo)
    {
      DirInfo.DeleteAllFiles();
      DirInfo.DeleteAllDirectories();
    }

    public static void CreateDirectoryIfNoExist(this DirectoryInfo DirInfo)
    {
      if (!DirInfo.Exists)
        DirInfo.Create();      
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bootloader
{
    abstract class Storage
    {
        public abstract byte[] ReadAllBytes(string v);
    }
    class MirrorStorage : Storage
    {
        private string Path;

        public MirrorStorage(string path)
        {
            this.Path = System.IO.Path.GetFullPath(path);
        }
        public override byte[] ReadAllBytes(string v)
        {
            return System.IO.File.ReadAllBytes(this.Path + "/" + v);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Storage s = new MirrorStorage("./rootfs");
            byte[] data = s.ReadAllBytes("/sys/vos");
            Assembly asm = Assembly.Load(data);
            
            asm.GetType("Vos.VOS").GetMethod("Main").Invoke(null, new object[] { new string[] { "mirror", "./rootfs" } });
        }
    }
}

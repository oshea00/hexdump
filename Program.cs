using System;
using System.IO;

namespace hexdump
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length==0) {
                Console.WriteLine("Usage: hexdump [file]");
                return;
            } else {
                try {
                    var fin = new  FileStream(args[0],FileMode.Open);
                    var buffer = new byte[16];
                    var bytesread = 0; 
                    var totalbytes = 0;            
                    using (fin) {
                        while ((bytesread = fin.Read(buffer,0,16)) > 0) {
                            format(buffer,bytesread,totalbytes);
                            totalbytes += bytesread;
                        }
                    }

                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void format(byte[] bytes,int buflen,int offset) {
            Console.Write($"{offset:X8} ");
            for (int i=0;i < buflen; ++i) {
                Console.Write($"{bytes[i]:X2} ");
                offset++;
            }
            if (buflen<16) {
                Console.Write("".PadLeft((16-buflen)*3));
            }
            Console.Write("[");
            for (int i=0; i<buflen; ++i) {
                var g = System.Text.Encoding.ASCII.GetChars(bytes,i,1)[0];
                if (Char.IsControl(g)||Char.IsWhiteSpace(g)) {
                    Console.Write(" ");
                } else {
                    Console.Write($"{g}");
                }
            }
            if (buflen<16) {
                Console.Write("".PadLeft(16-buflen));
            }
            Console.WriteLine("]");
        }
    }
}

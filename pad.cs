// David Hartnett
// 2015-10-26
// Parameters: file_in file_out pad

using System;
using System.IO;

public class pad
{
	public static void Main(string[] args)
	{
		if (args.Length < 3 || !File.Exists(args[0]))
		{
			Console.WriteLine("pad file_in file_out pad");
			return;
		}
		
		byte[] file_in = File.ReadAllBytes(args[0]);
		FileStream file_out = new FileStream(args[1], FileMode.Create);
		byte[] pad = new byte[args[2].Length*sizeof(char)];
		System.Buffer.BlockCopy(args[2].ToCharArray(), 0, pad, 0, pad.Length);
		
		// cheap hack deals with c# unicode default
		for (int i = 0; i < pad.Length; i++) if (i%2==0) pad[i+1] = pad[i];
		
		byte b_read = 0;
		int p = 0;
		for (long i = 0; i < file_in.Length; i++)
		{
			b_read = (byte) (file_in[i] ^ pad[p++]);
			if (p >= pad.Length) p = 0;
			file_out.WriteByte(b_read);
		}
		file_out.Close();
	}
}
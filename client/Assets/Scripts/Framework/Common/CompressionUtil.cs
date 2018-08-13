using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressionUtil {

    public static byte[] DecompressZLIB(byte[] data)
    {
        System.IO.MemoryStream inputStream = new System.IO.MemoryStream(data);
        System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
        InflaterInputStream inflaterStream = new InflaterInputStream(inputStream);
        byte[] buffer = new byte[50000];
        int count = 0;
        while ((count = inflaterStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            outputStream.Write(buffer, 0, count);
        }
        return outputStream.ToArray();
    }
}

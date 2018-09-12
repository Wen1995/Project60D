package com.nkm.framework.utils;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.zip.DataFormatException;
import java.util.zip.Deflater;
import java.util.zip.Inflater;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public final class CompressionUtil {
	
	static Logger logger = LoggerFactory.getLogger(CompressionUtil.class);
	
	public static final void main(String args[])
	{
		String str = "1";
		for(int i = 0; i < 1024 * 10; i ++)
		{
			str += "1";
		}
		System.err.println("str len"+str.getBytes().length);
		
		
		System.err.println("buf len = "+str.getBytes().length);
		try 
		{
			{
				byte[] buf = null;
				for(int i = 0; i < 100; i ++)
				{
					buf = compress(str.getBytes(), CompressionUtil.Level.BEST_COMPRESSION);
				}
				long startTime = System.currentTimeMillis();
				for(int i = 0; i < 1000; i ++)
				{
					buf = compress(str.getBytes(), CompressionUtil.Level.BEST_COMPRESSION);
				}
				System.err.println("java zip time:"+(System.currentTimeMillis() - startTime));
				System.err.println("java zip len "+buf.length);
			}
			
			{
				byte[] buf = null;
				for(int i = 0; i < 100; i ++)
				{
					buf = compress(str.getBytes(), CompressionUtil.Level.BEST_SPEED);
				}
				long startTime = System.currentTimeMillis();
				for(int i = 0; i < 1000; i ++)
				{
					buf = compress(str.getBytes(), CompressionUtil.Level.BEST_SPEED);
				}
				System.err.println("java speedzip time:"+(System.currentTimeMillis() - startTime));
				System.err.println("java speedzip len "+buf.length);
			}
			
			{
//				byte[] buf = null;
//				for(int i = 0; i < 100; i ++)
//				{
//					buf = jzlib(str.getBytes());
//				}
//				long startTime = System.currentTimeMillis();
//				for(int i = 0; i < 1000; i ++)
//				{
//					buf = jzlib(str.getBytes());
//				}
//				System.err.println("jzlib time:"+(System.currentTimeMillis() - startTime));
//				System.err.println("jzlib len "+buf.length);
			}
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

    private static final int BUFFER_SIZE = 4 * 1024;

    public static byte[] compress(byte[] data, Level level) throws IOException {

    	if(data == null || level == null)
    	{
    		logger.error("compress data or level cant be null!");
    		return null;
    	}

        Deflater deflater = new Deflater();
        // set compression level
        deflater.setLevel(level.getLevel());
        deflater.setInput(data);

        ByteArrayOutputStream outputStream = new ByteArrayOutputStream(data.length);

        deflater.finish();
        byte[] buffer = new byte[BUFFER_SIZE];
        while (!deflater.finished()) {
            int count = deflater.deflate(buffer); // returns the generated
                                                  // code... index
            outputStream.write(buffer, 0, count);
        }
        byte[] output = outputStream.toByteArray();
		outputStream.close();
        return output;
    }

    public static byte[] decompress(byte[] data) throws IOException, DataFormatException {

    	if(data == null)
    	{
    		logger.error("decompress data or level cant be null!");
    		return null;
    	}

        Inflater inflater = new Inflater();
        inflater.setInput(data);

        ByteArrayOutputStream outputStream = new ByteArrayOutputStream(data.length);
        byte[] buffer = new byte[BUFFER_SIZE];
        while (!inflater.finished()) {
            int count = inflater.inflate(buffer);
            outputStream.write(buffer, 0, count);
        }
        byte[] output = outputStream.toByteArray();
		outputStream.close();
        return output;
    }

    /**
     * Compression level
     */
    public static enum Level {

        /**
         * Compression level for no compression.
         */
        NO_COMPRESSION(0),

        /**
         * Compression level for fastest compression.
         */
        BEST_SPEED(1),

        /**
         * Compression level for best compression.
         */
        BEST_COMPRESSION(9),

        /**
         * Default compression level.
         */
        DEFAULT_COMPRESSION(-1);

        private int level;

        Level(int level) 
        {
            this.level = level;
        }
        
        public int getLevel() {
            return level;
        }
    }  
    
}

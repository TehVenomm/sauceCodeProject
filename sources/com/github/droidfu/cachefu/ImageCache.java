package com.github.droidfu.cachefu;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;

public class ImageCache extends AbstractCache<String, byte[]> {
    public ImageCache(int i, long j, int i2) {
        super("ImageCache", i, j, i2);
    }

    public Bitmap getBitmap(Object obj) {
        Bitmap decodeByteArray;
        synchronized (this) {
            byte[] bArr = (byte[]) super.get(obj);
            decodeByteArray = bArr == null ? null : BitmapFactory.decodeByteArray(bArr, 0, bArr.length);
        }
        return decodeByteArray;
    }

    public String getFileNameForKey(String str) {
        return CacheHelper.getFileNameFromUrl(str);
    }

    protected byte[] readValueFromDisk(File file) throws IOException {
        BufferedInputStream bufferedInputStream = new BufferedInputStream(new FileInputStream(file));
        long length = file.length();
        if (length > 2147483647L) {
            throw new IOException("Cannot read files larger than 2147483647 bytes");
        }
        int i = (int) length;
        byte[] bArr = new byte[i];
        bufferedInputStream.read(bArr, 0, i);
        bufferedInputStream.close();
        return bArr;
    }

    protected void writeValueToDisk(File file, byte[] bArr) throws IOException {
        BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(file));
        bufferedOutputStream.write(bArr);
        bufferedOutputStream.close();
    }
}

package com.github.droidfu.cachefu;

import com.github.droidfu.http.CachedHttpResponse.ResponseData;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FilenameFilter;
import java.io.IOException;

public class HttpResponseCache extends AbstractCache<String, ResponseData> {
    public HttpResponseCache(int i, long j, int i2) {
        super("HttpCache", i, j, i2);
    }

    private void removeExpiredCache(final String str) {
        final File file = new File(this.diskCacheDirectory);
        if (file.exists()) {
            File[] listFiles = file.listFiles(new FilenameFilter() {
                public boolean accept(File file, String str) {
                    return file.equals(file) && str.startsWith(HttpResponseCache.this.getFileNameForKey(str));
                }
            });
            if (listFiles != null && listFiles.length != 0) {
                for (File delete : listFiles) {
                    delete.delete();
                }
            }
        }
    }

    public String getFileNameForKey(String str) {
        return CacheHelper.getFileNameFromUrl(str);
    }

    protected ResponseData readValueFromDisk(File file) throws IOException {
        BufferedInputStream bufferedInputStream = new BufferedInputStream(new FileInputStream(file));
        long length = file.length();
        if (length > 2147483647L) {
            throw new IOException("Cannot read files larger than 2147483647 bytes");
        }
        int read = bufferedInputStream.read();
        int i = ((int) length) - 1;
        byte[] bArr = new byte[i];
        bufferedInputStream.read(bArr, 0, i);
        bufferedInputStream.close();
        return new ResponseData(read, bArr);
    }

    public void removeAllWithPrefix(String str) {
        synchronized (this) {
            for (String str2 : keySet()) {
                if (str2.startsWith(str)) {
                    remove(str2);
                }
            }
            if (isDiskCacheEnabled()) {
                removeExpiredCache(str);
            }
        }
    }

    protected void writeValueToDisk(File file, ResponseData responseData) throws IOException {
        BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(file));
        bufferedOutputStream.write(responseData.getStatusCode());
        bufferedOutputStream.write(responseData.getResponseBody());
        bufferedOutputStream.close();
    }
}

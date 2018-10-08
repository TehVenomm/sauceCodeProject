package jp.colopl.util;

import android.os.Environment;
import com.github.droidfu.cachefu.AbstractCache;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.FilenameFilter;
import java.io.IOException;
import org.apache.commons.lang3.time.DateUtils;

public class CacheUtil {
    private static final String DIR_NAME = "/Android/data/jp.colopl/cache/menu";

    public static String get(String str) {
        File file = new File(Environment.getExternalStorageDirectory() + DIR_NAME, str);
        if (!file.exists()) {
            return null;
        }
        if (System.currentTimeMillis() - file.lastModified() > DateUtils.MILLIS_PER_HOUR) {
            file.delete();
            return null;
        }
        StringBuilder stringBuilder;
        try {
            BufferedReader bufferedReader = new BufferedReader(new FileReader(file));
            stringBuilder = new StringBuilder();
            while (true) {
                try {
                    String readLine = bufferedReader.readLine();
                    if (readLine == null) {
                        break;
                    }
                    stringBuilder.append(readLine);
                } catch (IOException e) {
                }
            }
            bufferedReader.close();
        } catch (IOException e2) {
            stringBuilder = null;
        }
        return (stringBuilder == null || stringBuilder.length() == 0) ? null : stringBuilder.toString();
    }

    public static void removeAllMenuCache() {
        File file = new File(Environment.getExternalStorageDirectory() + DIR_NAME);
        try {
            if (file.exists()) {
                for (File delete : file.listFiles()) {
                    delete.delete();
                }
            }
        } catch (SecurityException e) {
        }
    }

    public static void removeAllWithStringPrefix(AbstractCache<String, ?> abstractCache, String str) {
        for (String str2 : abstractCache.keySet()) {
            if (str2.startsWith(str)) {
                abstractCache.remove(str2);
            }
        }
        if (abstractCache.isDiskCacheEnabled()) {
            removeExpiredCache(abstractCache, str);
        }
    }

    public static void removeExpiredCache(final AbstractCache<String, ?> abstractCache, final String str) {
        final File file = new File(abstractCache.getDiskCacheDirectory());
        if (file.exists()) {
            File[] listFiles = file.listFiles(new FilenameFilter() {
                public boolean accept(File file, String str) {
                    return file.equals(file) && str.startsWith(abstractCache.getFileNameForKey(str));
                }
            });
            if (listFiles != null && listFiles.length != 0) {
                for (File delete : listFiles) {
                    delete.delete();
                }
            }
        }
    }

    public static void save(String str, String str2) {
        File file = new File(Environment.getExternalStorageDirectory() + DIR_NAME);
        if (!file.exists()) {
            file.mkdir();
        }
        try {
            BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(new File(file, str)));
            bufferedWriter.write(str2);
            bufferedWriter.close();
        } catch (IOException e) {
        }
    }
}

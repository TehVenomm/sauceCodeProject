package com.github.droidfu.cachefu;

import android.content.Context;
import android.os.Environment;
import android.util.Log;
import com.appsflyer.share.Constants;
import com.github.droidfu.support.StringSupport;
import com.google.common.collect.MapMaker;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Collection;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.TimeUnit;

public abstract class AbstractCache<KeyT, ValT> implements Map<KeyT, ValT> {
    public static final int DISK_CACHE_INTERNAL = 0;
    public static final int DISK_CACHE_SDCARD = 1;
    private static final String LOG_TAG = "Droid-Fu[CacheFu]";
    private ConcurrentMap<KeyT, ValT> cache;
    protected String diskCacheDirectory;
    private boolean isDiskCacheEnabled;
    private String name;

    public AbstractCache(String str, int i, long j, int i2) {
        this.name = str;
        MapMaker mapMaker = new MapMaker();
        mapMaker.initialCapacity(i);
        mapMaker.expiration(60 * j, TimeUnit.SECONDS);
        mapMaker.concurrencyLevel(i2);
        mapMaker.softValues();
        this.cache = mapMaker.makeMap();
    }

    private void cacheToDisk(KeyT keyt, ValT valt) {
        File file = new File(this.diskCacheDirectory + Constants.URL_PATH_DELIMITER + getFileNameForKey(keyt));
        try {
            file.createNewFile();
            file.deleteOnExit();
            writeValueToDisk(file, valt);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e2) {
            e2.printStackTrace();
        }
    }

    private File getFileForKey(KeyT keyt) {
        return new File(this.diskCacheDirectory + Constants.URL_PATH_DELIMITER + getFileNameForKey(keyt));
    }

    private void setRootDir(String str) {
        this.diskCacheDirectory = str + "/cachefu/" + StringSupport.underscore(this.name.replaceAll("\\s", ""));
    }

    public void clear() {
        synchronized (this) {
            this.cache.clear();
            if (this.isDiskCacheEnabled) {
                File[] listFiles = new File(this.diskCacheDirectory).listFiles();
                if (listFiles != null) {
                    for (File delete : listFiles) {
                        delete.delete();
                    }
                }
            }
        }
    }

    public boolean containsKey(Object obj) {
        boolean z;
        synchronized (this) {
            z = this.cache.containsKey(obj) || (this.isDiskCacheEnabled && getFileForKey(obj).exists());
        }
        return z;
    }

    public boolean containsKeyInMemory(Object obj) {
        boolean containsKey;
        synchronized (this) {
            containsKey = this.cache.containsKey(obj);
        }
        return containsKey;
    }

    public boolean containsValue(Object obj) {
        boolean containsValue;
        synchronized (this) {
            containsValue = this.cache.containsValue(obj);
        }
        return containsValue;
    }

    public boolean enableDiskCache(Context context, int i) {
        String absolutePath;
        Context applicationContext = context.getApplicationContext();
        if (i != 1 || !"mounted".equals(Environment.getExternalStorageState())) {
            File cacheDir = applicationContext.getCacheDir();
            if (cacheDir == null) {
                this.isDiskCacheEnabled = false;
                return false;
            }
            absolutePath = cacheDir.getAbsolutePath();
        } else {
            absolutePath = Environment.getExternalStorageDirectory().getAbsolutePath() + "/Android/data/" + applicationContext.getPackageName() + "/cache";
        }
        setRootDir(absolutePath);
        File file = new File(this.diskCacheDirectory);
        if (file.mkdirs()) {
            try {
                new File(this.diskCacheDirectory, ".nomedia").createNewFile();
            } catch (IOException e) {
                Log.e(LOG_TAG, "Failed creating .nomedia file");
            }
        }
        this.isDiskCacheEnabled = file.exists();
        if (!this.isDiskCacheEnabled) {
            Log.w(LOG_TAG, "Failed creating disk cache directory " + this.diskCacheDirectory);
        } else {
            Log.d(this.name, "enabled write through to " + this.diskCacheDirectory);
        }
        return this.isDiskCacheEnabled;
    }

    public Set<Entry<KeyT, ValT>> entrySet() {
        return this.cache.entrySet();
    }

    public ValT get(Object obj) {
        ValT valt;
        synchronized (this) {
            valt = this.cache.get(obj);
            if (valt != null) {
                Log.d(this.name, "MEM cache hit for " + obj.toString());
            } else {
                File fileForKey = getFileForKey(obj);
                if (fileForKey.exists()) {
                    Log.d(this.name, "DISK cache hit for " + obj.toString());
                    try {
                        valt = readValueFromDisk(fileForKey);
                        if (valt != null) {
                            this.cache.put(obj, valt);
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                        valt = null;
                    }
                }
                valt = null;
            }
        }
        return valt;
    }

    public String getDiskCacheDirectory() {
        return this.diskCacheDirectory;
    }

    public abstract String getFileNameForKey(KeyT keyt);

    public boolean isDiskCacheEnabled() {
        return this.isDiskCacheEnabled;
    }

    public boolean isEmpty() {
        boolean isEmpty;
        synchronized (this) {
            isEmpty = this.cache.isEmpty();
        }
        return isEmpty;
    }

    public Set<KeyT> keySet() {
        return this.cache.keySet();
    }

    public ValT put(KeyT keyt, ValT valt) {
        ValT put;
        synchronized (this) {
            if (this.isDiskCacheEnabled) {
                cacheToDisk(keyt, valt);
            }
            put = this.cache.put(keyt, valt);
        }
        return put;
    }

    public void putAll(Map<? extends KeyT, ? extends ValT> map) {
        synchronized (this) {
            throw new UnsupportedOperationException();
        }
    }

    /* access modifiers changed from: protected */
    public abstract ValT readValueFromDisk(File file) throws IOException;

    public ValT remove(Object obj) {
        ValT removeKey;
        synchronized (this) {
            removeKey = removeKey(obj);
            if (this.isDiskCacheEnabled) {
                File fileForKey = getFileForKey(obj);
                if (fileForKey.exists()) {
                    fileForKey.delete();
                }
            }
        }
        return removeKey;
    }

    public ValT removeKey(Object obj) {
        return this.cache.remove(obj);
    }

    public void setDiskCacheEnabled(String str) {
        if (str == null || str.length() <= 0) {
            this.isDiskCacheEnabled = false;
            return;
        }
        setRootDir(str);
        this.isDiskCacheEnabled = true;
    }

    public int size() {
        int size;
        synchronized (this) {
            size = this.cache.size();
        }
        return size;
    }

    public Collection<ValT> values() {
        return this.cache.values();
    }

    /* access modifiers changed from: protected */
    public abstract void writeValueToDisk(File file, ValT valt) throws IOException;
}

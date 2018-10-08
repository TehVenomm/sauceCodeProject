package com.appsflyer.cache;

import android.content.Context;
import android.util.Log;
import com.appsflyer.AppsFlyerLib;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.Reader;
import java.io.Writer;
import java.util.ArrayList;
import java.util.List;

public class CacheManager {
    public static final String AF_CACHE_DIR = "AFRequestCache";
    public static final int CACHE_MAX_SIZE = 40;
    /* renamed from: ॱ */
    private static CacheManager f218 = new CacheManager();

    public static CacheManager getInstance() {
        return f218;
    }

    private CacheManager() {
    }

    public void cacheRequest(RequestCacheData requestCacheData, Context context) {
        Throwable th;
        Throwable th2;
        Writer writer = null;
        Writer outputStreamWriter;
        try {
            File file = new File(context.getFilesDir(), AF_CACHE_DIR);
            if (file.exists()) {
                File[] listFiles = file.listFiles();
                if (listFiles == null || listFiles.length <= 40) {
                    Log.i(AppsFlyerLib.LOG_TAG, "caching request...");
                    File file2 = new File(new File(context.getFilesDir(), AF_CACHE_DIR), Long.toString(System.currentTimeMillis()));
                    file2.createNewFile();
                    outputStreamWriter = new OutputStreamWriter(new FileOutputStream(file2.getPath(), true));
                    try {
                        outputStreamWriter.write("version=");
                        outputStreamWriter.write(requestCacheData.getVersion());
                        outputStreamWriter.write(10);
                        outputStreamWriter.write("url=");
                        outputStreamWriter.write(requestCacheData.getRequestURL());
                        outputStreamWriter.write(10);
                        outputStreamWriter.write("data=");
                        outputStreamWriter.write(requestCacheData.getPostData());
                        outputStreamWriter.write(10);
                        outputStreamWriter.flush();
                        try {
                            outputStreamWriter.close();
                            return;
                        } catch (IOException e) {
                            return;
                        }
                    } catch (Exception e2) {
                        writer = outputStreamWriter;
                        try {
                            Log.i(AppsFlyerLib.LOG_TAG, "Could not cache request");
                            if (writer != null) {
                                try {
                                    writer.close();
                                } catch (IOException e3) {
                                    return;
                                }
                            }
                        } catch (Throwable th3) {
                            th = th3;
                            outputStreamWriter = writer;
                            th2 = th;
                            if (outputStreamWriter != null) {
                                try {
                                    outputStreamWriter.close();
                                } catch (IOException e4) {
                                }
                            }
                            throw th2;
                        }
                    } catch (Throwable th4) {
                        th2 = th4;
                        if (outputStreamWriter != null) {
                            outputStreamWriter.close();
                        }
                        throw th2;
                    }
                }
                Log.i(AppsFlyerLib.LOG_TAG, "reached cache limit, not caching request");
                return;
            }
            file.mkdir();
        } catch (Exception e5) {
            Log.i(AppsFlyerLib.LOG_TAG, "Could not cache request");
            if (writer != null) {
                writer.close();
            }
        } catch (Throwable th32) {
            th = th32;
            outputStreamWriter = null;
            th2 = th;
            if (outputStreamWriter != null) {
                outputStreamWriter.close();
            }
            throw th2;
        }
    }

    public List<RequestCacheData> getCachedRequests(Context context) {
        List<RequestCacheData> arrayList = new ArrayList();
        try {
            File file = new File(context.getFilesDir(), AF_CACHE_DIR);
            if (file.exists()) {
                for (File file2 : file.listFiles()) {
                    Log.i(AppsFlyerLib.LOG_TAG, new StringBuilder("Found cached request").append(file2.getName()).toString());
                    arrayList.add(m285(file2));
                }
            } else {
                file.mkdir();
            }
        } catch (Exception e) {
            Log.i(AppsFlyerLib.LOG_TAG, "Could not cache request");
        }
        return arrayList;
    }

    /* renamed from: ˎ */
    private static RequestCacheData m285(File file) {
        Reader fileReader;
        Reader reader;
        Throwable th;
        try {
            fileReader = new FileReader(file);
            try {
                char[] cArr = new char[((int) file.length())];
                fileReader.read(cArr);
                RequestCacheData requestCacheData = new RequestCacheData(cArr);
                requestCacheData.setCacheKey(file.getName());
                try {
                    fileReader.close();
                    return requestCacheData;
                } catch (IOException e) {
                    return requestCacheData;
                }
            } catch (Exception e2) {
                reader = fileReader;
                if (reader != null) {
                    try {
                        reader.close();
                    } catch (IOException e3) {
                    }
                }
                return null;
            } catch (Throwable th2) {
                th = th2;
                if (fileReader != null) {
                    try {
                        fileReader.close();
                    } catch (IOException e4) {
                    }
                }
                throw th;
            }
        } catch (Exception e5) {
            reader = null;
            if (reader != null) {
                reader.close();
            }
            return null;
        } catch (Throwable th3) {
            th = th3;
            fileReader = null;
            if (fileReader != null) {
                fileReader.close();
            }
            throw th;
        }
    }

    public void init(Context context) {
        try {
            if (!new File(context.getFilesDir(), AF_CACHE_DIR).exists()) {
                new File(context.getFilesDir(), AF_CACHE_DIR).mkdir();
            }
        } catch (Exception e) {
            Log.i(AppsFlyerLib.LOG_TAG, "Could not create cache directory");
        }
    }

    public void deleteRequest(String str, Context context) {
        File file = new File(new File(context.getFilesDir(), AF_CACHE_DIR), str);
        Log.i(AppsFlyerLib.LOG_TAG, new StringBuilder("Deleting ").append(str).append(" from cache").toString());
        if (file.exists()) {
            try {
                file.delete();
            } catch (Throwable e) {
                Log.i(AppsFlyerLib.LOG_TAG, new StringBuilder("Could not delete ").append(str).append(" from cache").toString(), e);
            }
        }
    }

    public void clearCache(Context context) {
        try {
            File file = new File(context.getFilesDir(), AF_CACHE_DIR);
            if (file.exists()) {
                for (File file2 : file.listFiles()) {
                    Log.i(AppsFlyerLib.LOG_TAG, new StringBuilder("Found cached request").append(file2.getName()).toString());
                    deleteRequest(m285(file2).getCacheKey(), context);
                }
                return;
            }
            file.mkdir();
        } catch (Exception e) {
            Log.i(AppsFlyerLib.LOG_TAG, "Could not cache request");
        }
    }
}

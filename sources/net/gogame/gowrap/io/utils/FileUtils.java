package net.gogame.gowrap.io.utils;

import android.content.Context;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.zip.GZIPInputStream;
import java.util.zip.GZIPOutputStream;

public final class FileUtils {
    private FileUtils() {
    }

    public static void copy(File file, File file2) throws IOException {
        Throwable th;
        InputStream inputStream = null;
        try {
            InputStream fileInputStream = new FileInputStream(file);
            OutputStream fileOutputStream;
            try {
                fileOutputStream = new FileOutputStream(file2);
                try {
                    IOUtils.copy(fileInputStream, fileOutputStream);
                    try {
                        IOUtils.closeQuietly(fileOutputStream);
                        IOUtils.closeQuietly(fileInputStream);
                    } catch (Throwable th2) {
                        th = th2;
                        inputStream = fileInputStream;
                        IOUtils.closeQuietly(inputStream);
                        throw th;
                    }
                } catch (Throwable th3) {
                    th = th3;
                    IOUtils.closeQuietly(fileOutputStream);
                    throw th;
                }
            } catch (Throwable th4) {
                th = th4;
                fileOutputStream = null;
                IOUtils.closeQuietly(fileOutputStream);
                throw th;
            }
        } catch (Throwable th5) {
            th = th5;
            IOUtils.closeQuietly(inputStream);
            throw th;
        }
    }

    public static void move(File file, File file2) throws IOException {
        copy(file, file2);
        file.delete();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void touch(java.io.File r2) throws java.io.IOException {
        /*
        r1 = 0;
        r0 = new java.io.FileOutputStream;	 Catch:{ all -> 0x000a }
        r0.<init>(r2);	 Catch:{ all -> 0x000a }
        net.gogame.gowrap.io.utils.IOUtils.closeQuietly(r0);
        return;
    L_0x000a:
        r0 = move-exception;
        net.gogame.gowrap.io.utils.IOUtils.closeQuietly(r1);
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.io.utils.FileUtils.touch(java.io.File):void");
    }

    public static void copyFromAsset(Context context, String str, File file) throws IOException {
        Throwable th;
        OutputStream fileOutputStream;
        try {
            fileOutputStream = new FileOutputStream(file);
            try {
                IOUtils.assetCopy(context, str, fileOutputStream);
                IOUtils.closeQuietly(fileOutputStream);
            } catch (Throwable th2) {
                th = th2;
                IOUtils.closeQuietly(fileOutputStream);
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            fileOutputStream = null;
            IOUtils.closeQuietly(fileOutputStream);
            throw th;
        }
    }

    public static String toString(File file, String str) throws IOException {
        InputStream fileInputStream;
        Throwable th;
        try {
            fileInputStream = new FileInputStream(file);
            try {
                String iOUtils = IOUtils.toString(fileInputStream, str);
                IOUtils.closeQuietly(fileInputStream);
                return iOUtils;
            } catch (Throwable th2) {
                th = th2;
                IOUtils.closeQuietly(fileInputStream);
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            fileInputStream = null;
            IOUtils.closeQuietly(fileInputStream);
            throw th;
        }
    }

    public static void gzipCopyFromAsset(Context context, String str, File file) throws IOException {
        InputStream open;
        Throwable th;
        Throwable th2;
        try {
            InputStream gZIPInputStream;
            OutputStream fileOutputStream;
            open = context.getAssets().open(str);
            try {
                if (str.endsWith(".gz")) {
                    gZIPInputStream = new GZIPInputStream(open);
                } else {
                    gZIPInputStream = open;
                }
                try {
                    fileOutputStream = new FileOutputStream(file);
                    try {
                        if (file.getName().endsWith(".gz")) {
                            fileOutputStream = new GZIPOutputStream(fileOutputStream);
                        }
                        IOUtils.copy(gZIPInputStream, fileOutputStream);
                    } catch (Throwable th3) {
                        th = th3;
                        IOUtils.closeQuietly(fileOutputStream);
                        throw th;
                    }
                } catch (Throwable th4) {
                    th = th4;
                    fileOutputStream = null;
                    IOUtils.closeQuietly(fileOutputStream);
                    throw th;
                }
            } catch (Throwable th5) {
                th2 = th5;
                IOUtils.closeQuietly(open);
                throw th2;
            }
            try {
                IOUtils.closeQuietly(fileOutputStream);
                IOUtils.closeQuietly(gZIPInputStream);
            } catch (Throwable th6) {
                Throwable th7 = th6;
                open = gZIPInputStream;
                th2 = th7;
                IOUtils.closeQuietly(open);
                throw th2;
            }
        } catch (Throwable th8) {
            th2 = th8;
            open = null;
            IOUtils.closeQuietly(open);
            throw th2;
        }
    }
}

package net.gogame.gowrap.p021io.utils;

import android.content.Context;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.zip.GZIPInputStream;
import java.util.zip.GZIPOutputStream;

/* renamed from: net.gogame.gowrap.io.utils.FileUtils */
public final class FileUtils {
    private FileUtils() {
    }

    public static void copy(File file, File file2) throws IOException {
        OutputStream outputStream;
        InputStream inputStream = null;
        try {
            FileInputStream fileInputStream = new FileInputStream(file);
            try {
                outputStream = new FileOutputStream(file2);
                try {
                    IOUtils.copy(fileInputStream, outputStream);
                    try {
                        IOUtils.closeQuietly(outputStream);
                        IOUtils.closeQuietly((InputStream) fileInputStream);
                    } catch (Throwable th) {
                        th = th;
                        inputStream = fileInputStream;
                        IOUtils.closeQuietly(inputStream);
                        throw th;
                    }
                } catch (Throwable th2) {
                    th = th2;
                    IOUtils.closeQuietly(outputStream);
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                outputStream = null;
                IOUtils.closeQuietly(outputStream);
                throw th;
            }
        } catch (Throwable th4) {
            th = th4;
            IOUtils.closeQuietly(inputStream);
            throw th;
        }
    }

    public static void move(File file, File file2) throws IOException {
        copy(file, file2);
        file.delete();
    }

    public static void touch(File file) throws IOException {
        FileOutputStream fileOutputStream = null;
        try {
            fileOutputStream = new FileOutputStream(file);
        } finally {
            IOUtils.closeQuietly(fileOutputStream);
        }
    }

    public static void copyFromAsset(Context context, String str, File file) throws IOException {
        OutputStream outputStream;
        try {
            outputStream = new FileOutputStream(file);
            try {
                IOUtils.assetCopy(context, str, outputStream);
                IOUtils.closeQuietly(outputStream);
            } catch (Throwable th) {
                th = th;
                IOUtils.closeQuietly(outputStream);
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            outputStream = null;
            IOUtils.closeQuietly(outputStream);
            throw th;
        }
    }

    public static String toString(File file, String str) throws IOException {
        InputStream inputStream;
        try {
            inputStream = new FileInputStream(file);
            try {
                String iOUtils = IOUtils.toString(inputStream, str);
                IOUtils.closeQuietly(inputStream);
                return iOUtils;
            } catch (Throwable th) {
                th = th;
                IOUtils.closeQuietly(inputStream);
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            inputStream = null;
            IOUtils.closeQuietly(inputStream);
            throw th;
        }
    }

    public static void gzipCopyFromAsset(Context context, String str, File file) throws IOException {
        Throwable th;
        InputStream inputStream;
        OutputStream outputStream;
        InputStream inputStream2 = null;
        try {
            InputStream open = context.getAssets().open(str);
            try {
                if (str.endsWith(".gz")) {
                    inputStream = new GZIPInputStream(open);
                } else {
                    inputStream = open;
                }
                try {
                    outputStream = new FileOutputStream(file);
                    try {
                        if (file.getName().endsWith(".gz")) {
                            outputStream = new GZIPOutputStream(outputStream);
                        }
                        IOUtils.copy(inputStream, outputStream);
                    } catch (Throwable th2) {
                        th = th2;
                        IOUtils.closeQuietly(outputStream);
                        throw th;
                    }
                } catch (Throwable th3) {
                    th = th3;
                    outputStream = null;
                    IOUtils.closeQuietly(outputStream);
                    throw th;
                }
            } catch (Throwable th4) {
                th = th4;
                inputStream2 = open;
                IOUtils.closeQuietly(inputStream2);
                throw th;
            }
            try {
                IOUtils.closeQuietly(outputStream);
                IOUtils.closeQuietly(inputStream);
            } catch (Throwable th5) {
                th = th5;
                inputStream2 = inputStream;
                IOUtils.closeQuietly(inputStream2);
                throw th;
            }
        } catch (Throwable th6) {
            th = th6;
            IOUtils.closeQuietly(inputStream2);
            throw th;
        }
    }
}

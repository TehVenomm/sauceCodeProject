package net.gogame.gowrap.io.utils;

import android.content.Context;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.Reader;
import java.io.Writer;
import java.util.zip.GZIPInputStream;

public final class IOUtils {
    private IOUtils() {
    }

    public static void closeQuietly(InputStream inputStream) {
        if (inputStream != null) {
            try {
                inputStream.close();
            } catch (IOException e) {
            }
        }
    }

    public static void closeQuietly(OutputStream outputStream) {
        if (outputStream != null) {
            try {
                outputStream.close();
            } catch (IOException e) {
            }
        }
    }

    public static void closeQuietly(Reader reader) {
        if (reader != null) {
            try {
                reader.close();
            } catch (IOException e) {
            }
        }
    }

    public static void closeQuietly(Writer writer) {
        if (writer != null) {
            try {
                writer.close();
            } catch (IOException e) {
            }
        }
    }

    public static long copy(InputStream inputStream, OutputStream outputStream) throws IOException {
        long j = 0;
        byte[] bArr = new byte[4096];
        while (true) {
            int read = inputStream.read(bArr);
            if (read == -1) {
                return j;
            }
            outputStream.write(bArr, 0, read);
            j += (long) read;
        }
    }

    public static String toString(Reader reader) throws IOException {
        char[] cArr = new char[4096];
        StringBuilder stringBuilder = new StringBuilder();
        while (true) {
            int read = reader.read(cArr);
            if (read <= 0) {
                return stringBuilder.toString();
            }
            stringBuilder.append(cArr, 0, read);
        }
    }

    public static String toString(InputStream inputStream, String str) throws IOException {
        return toString(new InputStreamReader(inputStream, str));
    }

    public static void assetCopy(Context context, String str, OutputStream outputStream) throws IOException {
        InputStream inputStream = null;
        try {
            inputStream = context.getAssets().open(str);
            copy(inputStream, outputStream);
        } finally {
            closeQuietly(inputStream);
        }
    }

    public static String assetToString(Context context, String str, String str2) throws IOException {
        InputStream inputStream = null;
        try {
            inputStream = context.getAssets().open(str);
            String iOUtils = toString(inputStream, str2);
            return iOUtils;
        } finally {
            closeQuietly(inputStream);
        }
    }

    public static InputStream newInputStream(File file) throws IOException {
        if (file.getName().endsWith(".gz")) {
            return new GZIPInputStream(new FileInputStream(file));
        }
        return new FileInputStream(file);
    }

    public static InputStream newInputStream(Context context, String str) throws IOException {
        if (str.endsWith(".gz")) {
            return new GZIPInputStream(context.getAssets().open(str));
        }
        return context.getAssets().open(str);
    }

    public static void drain(InputStream inputStream) throws IOException {
        do {
        } while (inputStream.read(new byte[4096]) >= 0);
    }
}

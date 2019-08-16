package p018jp.colopl.util;

import android.content.Context;
import android.os.Environment;
import android.text.format.DateFormat;
import android.util.Log;
import android.widget.Toast;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import p018jp.colopl.config.Config;

/* renamed from: jp.colopl.util.LogUtil */
public class LogUtil {
    private static final String TEMPORARY_LOG_FILENAME_FORMAT = "log.%s.txt";
    private static File file = null;
    private static boolean forceOutput = false;
    private static int logLevel = 2;
    private static BufferedWriter writer = null;

    /* renamed from: d */
    public static void m755d(String str, String str2) {
        logToFile(3, str, str2);
    }

    /* renamed from: e */
    public static void m756e(String str, String str2) {
        logToFile(6, str, str2);
    }

    public static void flush() {
        if (forceOutput || Config.debuggable) {
            BufferedWriter writer2 = getWriter();
            if (writer2 != null) {
                try {
                    writer2.flush();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private static String formatLogText(int i, String str, String str2) {
        String str3 = i == 7 ? "ASSERT" : i == 6 ? "ERROR" : i == 5 ? "WARN" : i == 4 ? "INFO" : i == 3 ? "DEBUG" : "VERBOSE";
        StringBuilder sb = new StringBuilder();
        sb.append("[").append(str3).append("][").append(str).append("][").append(DateFormat.format("yyyy/MM/dd kk:mm:ss", System.currentTimeMillis())).append("] ").append(str2);
        return sb.toString();
    }

    private static File getLogFile(Context context) {
        File file2 = new File(Environment.getExternalStorageDirectory(), context.getPackageName());
        if (!file2.exists()) {
            file2.mkdir();
        }
        return new File(file2, String.format(TEMPORARY_LOG_FILENAME_FORMAT, new Object[]{DateFormat.format("yyyyMMdd", System.currentTimeMillis())}));
    }

    private static BufferedWriter getWriter() {
        if (writer == null) {
            try {
                writer = new BufferedWriter(new FileWriter(file, true));
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return writer;
    }

    /* renamed from: i */
    public static void m757i(String str, String str2) {
        logToFile(4, str, str2);
    }

    public static void logToFile(int i, String str, String str2) {
        if ((forceOutput || Config.debuggable) && i >= logLevel) {
            Log.println(i, str, str2);
            try {
                BufferedWriter writer2 = getWriter();
                if (writer2 != null) {
                    writer2.write(formatLogText(i, str, str2));
                    writer2.newLine();
                    writer2.flush();
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static void setLevel(int i) {
        logLevel = i;
    }

    public static void setup(Context context) {
        if (forceOutput || Config.debuggable) {
            file = getLogFile(context);
        }
    }

    public static void showToast(Context context, String str, int i) {
        if (forceOutput || Config.debuggable) {
            Toast.makeText(context, str, i).show();
        }
    }

    /* renamed from: v */
    public static void m758v(String str, String str2) {
        logToFile(2, str, str2);
    }

    /* renamed from: w */
    public static void m759w(String str, String str2) {
        logToFile(5, str, str2);
    }
}

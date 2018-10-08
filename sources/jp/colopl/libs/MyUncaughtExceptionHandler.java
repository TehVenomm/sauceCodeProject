package jp.colopl.libs;

import android.os.Environment;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.PrintWriter;
import java.lang.Thread.UncaughtExceptionHandler;
import org.apache.commons.lang3.StringUtils;

public class MyUncaughtExceptionHandler implements UncaughtExceptionHandler {
    private static final String TAG = "MyUncaughtExceptionHandler";
    private UncaughtExceptionHandler mDefaultUEH = Thread.getDefaultUncaughtExceptionHandler();

    private File getTempFile() {
        File file = new File(Environment.getExternalStorageDirectory(), "jp.colopl.dino");
        if (!file.exists()) {
            file.mkdir();
        }
        return new File(file, "exception.log");
    }

    private void saveState(Thread thread, Throwable th) throws FileNotFoundException {
        int i = 0;
        PrintWriter printWriter = new PrintWriter(new FileOutputStream(getTempFile()));
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("----Thread-----\n");
        stringBuilder.append("Thread: ").append(thread.toString()).append(StringUtils.LF);
        stringBuilder.append("----Exception-----").append(StringUtils.LF);
        stringBuilder.append("Class: ").append(th.getClass().getCanonicalName()).append(StringUtils.LF);
        stringBuilder.append("Message: ").append(th.getMessage()).append(StringUtils.LF);
        printWriter.print(stringBuilder.toString());
        StackTraceElement[] stackTrace = th.getStackTrace();
        if (stackTrace != null) {
            stringBuilder.setLength(0);
            for (StackTraceElement stackTraceElement : stackTrace) {
                stringBuilder.append("     at ").append(stackTraceElement.toString()).append(StringUtils.LF);
            }
            printWriter.print(stringBuilder.toString());
        }
        printWriter.println("----Cause-----");
        Throwable cause = th.getCause();
        if (cause == null) {
            printWriter.println("cause is null");
        } else {
            stringBuilder.setLength(0);
            stringBuilder.append("Class: ").append(cause.getClass().getCanonicalName()).append(StringUtils.LF);
            stringBuilder.append("Message: ").append(cause.getMessage()).append(StringUtils.LF);
            printWriter.print(stringBuilder.toString());
            StackTraceElement[] stackTrace2 = cause.getStackTrace();
            if (stackTrace2 != null) {
                stringBuilder.setLength(0);
                int length = stackTrace2.length;
                while (i < length) {
                    stringBuilder.append("     at ").append(stackTrace2[i].toString()).append(StringUtils.LF);
                    i++;
                }
                printWriter.print(stringBuilder.toString());
            }
        }
        printWriter.println("-------------------------------");
        printWriter.close();
    }

    public void uncaughtException(Thread thread, Throwable th) {
        try {
            saveState(thread, th);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        this.mDefaultUEH.uncaughtException(thread, th);
    }
}

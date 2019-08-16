package p018jp.colopl.libs;

import android.os.Environment;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.PrintWriter;
import java.lang.Thread.UncaughtExceptionHandler;
import org.apache.commons.lang3.StringUtils;

/* renamed from: jp.colopl.libs.MyUncaughtExceptionHandler */
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
        PrintWriter printWriter = new PrintWriter(new FileOutputStream(getTempFile()));
        StringBuilder sb = new StringBuilder();
        sb.append("----Thread-----\n");
        sb.append("Thread: ").append(thread.toString()).append(StringUtils.f1189LF);
        sb.append("----Exception-----").append(StringUtils.f1189LF);
        sb.append("Class: ").append(th.getClass().getCanonicalName()).append(StringUtils.f1189LF);
        sb.append("Message: ").append(th.getMessage()).append(StringUtils.f1189LF);
        printWriter.print(sb.toString());
        StackTraceElement[] stackTrace = th.getStackTrace();
        if (stackTrace != null) {
            sb.setLength(0);
            for (StackTraceElement stackTraceElement : stackTrace) {
                sb.append("     at ").append(stackTraceElement.toString()).append(StringUtils.f1189LF);
            }
            printWriter.print(sb.toString());
        }
        printWriter.println("----Cause-----");
        Throwable cause = th.getCause();
        if (cause == null) {
            printWriter.println("cause is null");
        } else {
            sb.setLength(0);
            sb.append("Class: ").append(cause.getClass().getCanonicalName()).append(StringUtils.f1189LF);
            sb.append("Message: ").append(cause.getMessage()).append(StringUtils.f1189LF);
            printWriter.print(sb.toString());
            StackTraceElement[] stackTrace2 = cause.getStackTrace();
            if (stackTrace2 != null) {
                sb.setLength(0);
                for (StackTraceElement stackTraceElement2 : stackTrace2) {
                    sb.append("     at ").append(stackTraceElement2.toString()).append(StringUtils.f1189LF);
                }
                printWriter.print(sb.toString());
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

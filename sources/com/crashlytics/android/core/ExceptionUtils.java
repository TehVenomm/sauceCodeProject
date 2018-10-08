package com.crashlytics.android.core;

import android.content.Context;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.Closeable;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.Writer;
import org.apache.commons.lang3.StringUtils;

final class ExceptionUtils {
    private ExceptionUtils() {
    }

    private static String getMessage(Throwable th) {
        String localizedMessage = th.getLocalizedMessage();
        return localizedMessage == null ? null : localizedMessage.replaceAll("(\r\n|\n|\f)", " ");
    }

    public static void writeStackTrace(Context context, Throwable th, String str) {
        Closeable printWriter;
        Throwable th2;
        Throwable e;
        Throwable th3;
        Closeable closeable = null;
        try {
            printWriter = new PrintWriter(context.openFileOutput(str, 0));
            try {
                writeStackTrace(th, (Writer) printWriter);
                CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
            } catch (Throwable e2) {
                th2 = e2;
                closeable = printWriter;
                th3 = th2;
                try {
                    Fabric.getLogger().mo4292e("Fabric", "Failed to create PrintWriter", th3);
                    CommonUtils.closeOrLog(closeable, "Failed to close stack trace writer.");
                } catch (Throwable th32) {
                    th2 = th32;
                    printWriter = closeable;
                    e2 = th2;
                    CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
                    throw e2;
                }
            } catch (Throwable th4) {
                e2 = th4;
                CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
                throw e2;
            }
        } catch (Exception e3) {
            th32 = e3;
            Fabric.getLogger().mo4292e("Fabric", "Failed to create PrintWriter", th32);
            CommonUtils.closeOrLog(closeable, "Failed to close stack trace writer.");
        } catch (Throwable th322) {
            th2 = th322;
            printWriter = null;
            e2 = th2;
            CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
            throw e2;
        }
    }

    private static void writeStackTrace(Throwable th, OutputStream outputStream) {
        Closeable printWriter;
        Throwable th2;
        Throwable e;
        Throwable th3;
        Closeable closeable = null;
        try {
            printWriter = new PrintWriter(outputStream);
            try {
                writeStackTrace(th, (Writer) printWriter);
                CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
            } catch (Throwable e2) {
                th2 = e2;
                closeable = printWriter;
                th3 = th2;
                try {
                    Fabric.getLogger().mo4292e("Fabric", "Failed to create PrintWriter", th3);
                    CommonUtils.closeOrLog(closeable, "Failed to close stack trace writer.");
                } catch (Throwable th32) {
                    th2 = th32;
                    printWriter = closeable;
                    e2 = th2;
                    CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
                    throw e2;
                }
            } catch (Throwable th4) {
                e2 = th4;
                CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
                throw e2;
            }
        } catch (Exception e3) {
            th32 = e3;
            Fabric.getLogger().mo4292e("Fabric", "Failed to create PrintWriter", th32);
            CommonUtils.closeOrLog(closeable, "Failed to close stack trace writer.");
        } catch (Throwable th322) {
            th2 = th322;
            printWriter = null;
            e2 = th2;
            CommonUtils.closeOrLog(printWriter, "Failed to close stack trace writer.");
            throw e2;
        }
    }

    private static void writeStackTrace(Throwable th, Writer writer) {
        Object obj = 1;
        while (th != null) {
            String message = getMessage(th);
            writer.write((obj != null ? "" : "Caused by: ") + th.getClass().getName() + ": " + (message != null ? message : "") + StringUtils.LF);
            for (StackTraceElement stackTraceElement : th.getStackTrace()) {
                writer.write("\tat " + stackTraceElement.toString() + StringUtils.LF);
            }
            try {
                th = th.getCause();
                obj = null;
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Could not write stack trace", e);
                return;
            }
        }
    }

    public static void writeStackTraceIfNotNull(Throwable th, OutputStream outputStream) {
        if (outputStream != null) {
            writeStackTrace(th, outputStream);
        }
    }
}

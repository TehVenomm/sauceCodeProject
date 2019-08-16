package com.crashlytics.android.core;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.Locale;
import org.apache.commons.lang3.StringUtils;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.QueueFile;
import p017io.fabric.sdk.android.services.common.QueueFile.ElementReader;

class QueueFileLogStore implements FileLogStore {
    private QueueFile logFile;
    private final int maxLogSize;
    private final File workingFile;

    public class LogBytes {
        public final byte[] bytes;
        public final int offset;

        public LogBytes(byte[] bArr, int i) {
            this.bytes = bArr;
            this.offset = i;
        }
    }

    public QueueFileLogStore(File file, int i) {
        this.workingFile = file;
        this.maxLogSize = i;
    }

    private void doWriteToLog(long j, String str) {
        if (this.logFile != null) {
            String str2 = str == null ? "null" : str;
            try {
                int i = this.maxLogSize / 4;
                if (str2.length() > i) {
                    str2 = "..." + str2.substring(str2.length() - i);
                }
                this.logFile.add(String.format(Locale.US, "%d %s%n", new Object[]{Long.valueOf(j), str2.replaceAll(StringUtils.f1198CR, " ").replaceAll(StringUtils.f1199LF, " ")}).getBytes("UTF-8"));
                while (!this.logFile.isEmpty() && this.logFile.usedBytes() > this.maxLogSize) {
                    this.logFile.remove();
                }
            } catch (IOException e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "There was a problem writing to the Crashlytics log.", e);
            }
        }
    }

    private LogBytes getLogBytes() {
        if (!this.workingFile.exists()) {
            return null;
        }
        openLogFile();
        if (this.logFile == null) {
            return null;
        }
        final int[] iArr = {0};
        final byte[] bArr = new byte[this.logFile.usedBytes()];
        try {
            this.logFile.forEach(new ElementReader() {
                public void read(InputStream inputStream, int i) throws IOException {
                    try {
                        inputStream.read(bArr, iArr[0], i);
                        int[] iArr = iArr;
                        iArr[0] = iArr[0] + i;
                    } finally {
                        inputStream.close();
                    }
                }
            });
        } catch (IOException e) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "A problem occurred while reading the Crashlytics log file.", e);
        }
        return new LogBytes(bArr, iArr[0]);
    }

    private void openLogFile() {
        if (this.logFile == null) {
            try {
                this.logFile = new QueueFile(this.workingFile);
            } catch (IOException e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Could not open log file: " + this.workingFile, e);
            }
        }
    }

    public void closeLogFile() {
        CommonUtils.closeOrLog(this.logFile, "There was a problem closing the Crashlytics log file.");
        this.logFile = null;
    }

    public void deleteLogFile() {
        closeLogFile();
        this.workingFile.delete();
    }

    public ByteString getLogAsByteString() {
        LogBytes logBytes = getLogBytes();
        if (logBytes == null) {
            return null;
        }
        return ByteString.copyFrom(logBytes.bytes, 0, logBytes.offset);
    }

    public byte[] getLogAsBytes() {
        LogBytes logBytes = getLogBytes();
        if (logBytes == null) {
            return null;
        }
        return logBytes.bytes;
    }

    public void writeToLog(long j, String str) {
        openLogFile();
        doWriteToLog(j, str);
    }
}

package com.crashlytics.android.core;

import android.content.Context;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.File;

class LogFileManager {
    private static final String DIRECTORY_NAME = "log-files";
    private static final String LOGFILE_EXT = ".temp";
    private static final String LOGFILE_PREFIX = "crashlytics-userlog-";
    static final int MAX_LOG_SIZE = 65536;
    private static final NoopLogStore NOOP_LOG_STORE = new NoopLogStore();
    private final Context context;
    private FileLogStore currentLog = NOOP_LOG_STORE;
    private final File logFileDir;

    private static final class NoopLogStore implements FileLogStore {
        private NoopLogStore() {
        }

        public void closeLogFile() {
        }

        public void deleteLogFile() {
        }

        public ByteString getLogAsByteString() {
            return null;
        }

        public void writeToLog(long j, String str) {
        }
    }

    public LogFileManager(Context context, File file) {
        this.context = context;
        this.logFileDir = new File(file, DIRECTORY_NAME);
    }

    private void ensureTargetDirectoryExists() {
        if (!this.logFileDir.exists()) {
            this.logFileDir.mkdirs();
        }
    }

    private File getWorkingFileForSession(String str) {
        ensureTargetDirectoryExists();
        return new File(this.logFileDir, LOGFILE_PREFIX + str + LOGFILE_EXT);
    }

    private boolean isLoggingEnabled() {
        return CommonUtils.getBooleanResourceValue(this.context, "com.crashlytics.CollectCustomLogs", true);
    }

    public void clearLog() {
        this.currentLog.deleteLogFile();
    }

    public ByteString getByteStringForLog() {
        return this.currentLog.getLogAsByteString();
    }

    public void onSessionChange(String str) {
        clearLog();
        if (isLoggingEnabled()) {
            setLogFile(getWorkingFileForSession(str), 65536);
            return;
        }
        Fabric.getLogger().mo4753d("Fabric", "Preferences requested no custom logs. Aborting log file creation.");
        this.currentLog = NOOP_LOG_STORE;
    }

    void setLogFile(File file, int i) {
        this.currentLog.closeLogFile();
        this.currentLog = new QueueFileLogStore(file, i);
    }

    public void writeToLog(long j, String str) {
        this.currentLog.writeToLog(j, str);
    }
}

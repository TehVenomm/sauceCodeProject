package com.crashlytics.android.core;

import android.app.ActivityManager.RunningAppProcessInfo;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build;
import android.os.Environment;
import android.os.StatFs;
import com.crashlytics.android.core.internal.models.SessionEventData;
import com.facebook.appevents.AppEventsConstants;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.DeliveryMechanism;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.settings.SessionSettingsData;
import io.fabric.sdk.android.services.settings.Settings;
import java.io.Closeable;
import java.io.File;
import java.io.FileInputStream;
import java.io.FilenameFilter;
import java.io.Flushable;
import java.io.IOException;
import java.io.OutputStream;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.TreeMap;
import java.util.concurrent.Callable;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

class CrashlyticsUncaughtExceptionHandler implements UncaughtExceptionHandler {
    private static final int ANALYZER_VERSION = 1;
    static final FilenameFilter ANY_SESSION_FILENAME_FILTER = new C03244();
    static final String CLS_CRASH_MARKER_FILE_NAME = "crash_marker";
    private static final String EVENT_TYPE_CRASH = "crash";
    private static final String EVENT_TYPE_LOGGED = "error";
    private static final String GENERATOR_FORMAT = "Crashlytics Android SDK/%s";
    static final String INVALID_CLS_CACHE_DIR = "invalidClsFiles";
    static final Comparator<File> LARGEST_FILE_NAME_FIRST = new C03222();
    private static final int MAX_COMPLETE_SESSIONS_COUNT = 4;
    private static final int MAX_LOCAL_LOGGED_EXCEPTIONS = 64;
    static final int MAX_OPEN_SESSIONS = 8;
    private static final Map<String, String> SEND_AT_CRASHTIME_HEADER = Collections.singletonMap("X-CRASHLYTICS-SEND-FLAGS", AppEventsConstants.EVENT_PARAM_VALUE_YES);
    static final String SESSION_APP_TAG = "SessionApp";
    static final String SESSION_BEGIN_TAG = "BeginSession";
    static final String SESSION_DEVICE_TAG = "SessionDevice";
    static final String SESSION_FATAL_TAG = "SessionCrash";
    static final FilenameFilter SESSION_FILE_FILTER = new C03211();
    private static final Pattern SESSION_FILE_PATTERN = Pattern.compile("([\\d|A-Z|a-z]{12}\\-[\\d|A-Z|a-z]{4}\\-[\\d|A-Z|a-z]{4}\\-[\\d|A-Z|a-z]{12}).+");
    private static final int SESSION_ID_LENGTH = 35;
    static final String SESSION_NON_FATAL_TAG = "SessionEvent";
    static final String SESSION_OS_TAG = "SessionOS";
    static final String SESSION_USER_TAG = "SessionUser";
    static final Comparator<File> SMALLEST_FILE_NAME_FIRST = new C03233();
    private final CrashlyticsCore crashlyticsCore;
    private final UncaughtExceptionHandler defaultHandler;
    private final AtomicInteger eventCounter = new AtomicInteger(0);
    private final CrashlyticsExecutorServiceWrapper executorServiceWrapper;
    private final File filesDir;
    private final IdManager idManager;
    private final AtomicBoolean isHandlingException;
    private final LogFileManager logFileManager;
    private boolean powerConnected;
    private final BroadcastReceiver powerConnectedReceiver;
    private final BroadcastReceiver powerDisconnectedReceiver;
    private final AtomicBoolean receiversRegistered = new AtomicBoolean(false);
    private final SessionDataWriter sessionDataWriter;

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$1 */
    static final class C03211 implements FilenameFilter {
        C03211() {
        }

        public boolean accept(File file, String str) {
            return str.length() == ClsFileOutputStream.SESSION_FILE_EXTENSION.length() + 35 && str.endsWith(ClsFileOutputStream.SESSION_FILE_EXTENSION);
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$2 */
    static final class C03222 implements Comparator<File> {
        C03222() {
        }

        public int compare(File file, File file2) {
            return file2.getName().compareTo(file.getName());
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$3 */
    static final class C03233 implements Comparator<File> {
        C03233() {
        }

        public int compare(File file, File file2) {
            return file.getName().compareTo(file2.getName());
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$4 */
    static final class C03244 implements FilenameFilter {
        C03244() {
        }

        public boolean accept(File file, String str) {
            return CrashlyticsUncaughtExceptionHandler.SESSION_FILE_PATTERN.matcher(str).matches();
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$5 */
    class C03255 extends BroadcastReceiver {
        C03255() {
        }

        public void onReceive(Context context, Intent intent) {
            CrashlyticsUncaughtExceptionHandler.this.powerConnected = true;
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler$6 */
    class C03266 extends BroadcastReceiver {
        C03266() {
        }

        public void onReceive(Context context, Intent intent) {
            CrashlyticsUncaughtExceptionHandler.this.powerConnected = false;
        }
    }

    private static class AnySessionPartFileFilter implements FilenameFilter {
        private AnySessionPartFileFilter() {
        }

        public boolean accept(File file, String str) {
            return !CrashlyticsUncaughtExceptionHandler.SESSION_FILE_FILTER.accept(file, str) && CrashlyticsUncaughtExceptionHandler.SESSION_FILE_PATTERN.matcher(str).matches();
        }
    }

    static class FileNameContainsFilter implements FilenameFilter {
        private final String string;

        public FileNameContainsFilter(String str) {
            this.string = str;
        }

        public boolean accept(File file, String str) {
            return str.contains(this.string) && !str.endsWith(ClsFileOutputStream.IN_PROGRESS_SESSION_FILE_EXTENSION);
        }
    }

    static class SessionPartFileFilter implements FilenameFilter {
        private final String sessionId;

        public SessionPartFileFilter(String str) {
            this.sessionId = str;
        }

        public boolean accept(File file, String str) {
            return (str.equals(new StringBuilder().append(this.sessionId).append(ClsFileOutputStream.SESSION_FILE_EXTENSION).toString()) || !str.contains(this.sessionId) || str.endsWith(ClsFileOutputStream.IN_PROGRESS_SESSION_FILE_EXTENSION)) ? false : true;
        }
    }

    CrashlyticsUncaughtExceptionHandler(UncaughtExceptionHandler uncaughtExceptionHandler, CrashlyticsListener crashlyticsListener, CrashlyticsExecutorServiceWrapper crashlyticsExecutorServiceWrapper, IdManager idManager, SessionDataWriter sessionDataWriter, CrashlyticsCore crashlyticsCore) {
        this.defaultHandler = uncaughtExceptionHandler;
        this.executorServiceWrapper = crashlyticsExecutorServiceWrapper;
        this.idManager = idManager;
        this.crashlyticsCore = crashlyticsCore;
        this.sessionDataWriter = sessionDataWriter;
        this.isHandlingException = new AtomicBoolean(false);
        this.filesDir = crashlyticsCore.getSdkDirectory();
        this.logFileManager = new LogFileManager(crashlyticsCore.getContext(), this.filesDir);
        notifyCrashlyticsListenerOfPreviousCrash(crashlyticsListener);
        this.powerConnectedReceiver = new C03255();
        IntentFilter intentFilter = new IntentFilter("android.intent.action.ACTION_POWER_CONNECTED");
        this.powerDisconnectedReceiver = new C03266();
        IntentFilter intentFilter2 = new IntentFilter("android.intent.action.ACTION_POWER_DISCONNECTED");
        Context context = crashlyticsCore.getContext();
        context.registerReceiver(this.powerConnectedReceiver, intentFilter);
        context.registerReceiver(this.powerDisconnectedReceiver, intentFilter2);
        this.receiversRegistered.set(true);
    }

    private void closeWithoutRenamingOrLog(ClsFileOutputStream clsFileOutputStream) {
        if (clsFileOutputStream != null) {
            try {
                clsFileOutputStream.closeInProgressStream();
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Error closing session file stream in the presence of an exception", e);
            }
        }
    }

    private void deleteLegacyInvalidCacheDir() {
        File file = new File(this.crashlyticsCore.getSdkDirectory(), INVALID_CLS_CACHE_DIR);
        if (file.exists()) {
            if (file.isDirectory()) {
                for (File delete : file.listFiles()) {
                    delete.delete();
                }
            }
            file.delete();
        }
    }

    private void deleteSessionPartFilesFor(String str) {
        for (File delete : listSessionPartFilesFor(str)) {
            delete.delete();
        }
    }

    private void doCloseSessions() throws Exception {
        trimOpenSessions(8);
        String currentSessionId = getCurrentSessionId();
        if (currentSessionId != null) {
            writeSessionUser(currentSessionId);
            SessionSettingsData sessionSettingsData = this.crashlyticsCore.getSessionSettingsData();
            if (sessionSettingsData != null) {
                int i = sessionSettingsData.maxCustomExceptionEvents;
                Fabric.getLogger().mo4289d("Fabric", "Closing all open sessions.");
                File[] listSessionBeginFiles = listSessionBeginFiles();
                if (listSessionBeginFiles != null && listSessionBeginFiles.length > 0) {
                    for (File file : listSessionBeginFiles) {
                        String sessionIdFromSessionFile = getSessionIdFromSessionFile(file);
                        Fabric.getLogger().mo4289d("Fabric", "Closing session: " + sessionIdFromSessionFile);
                        writeSessionPartsToSessionFile(file, sessionIdFromSessionFile, i);
                    }
                    return;
                }
                return;
            }
            Fabric.getLogger().mo4289d("Fabric", "Unable to close session. Settings are not loaded.");
            return;
        }
        Fabric.getLogger().mo4289d("Fabric", "No open sessions exist.");
    }

    private void doOpenSession() throws Exception {
        Date date = new Date();
        String clsuuid = new CLSUUID(this.idManager).toString();
        Fabric.getLogger().mo4289d("Fabric", "Opening an new session with ID " + clsuuid);
        this.logFileManager.onSessionChange(clsuuid);
        writeBeginSession(clsuuid, date);
        writeSessionApp(clsuuid);
        writeSessionOS(clsuuid);
        writeSessionDevice(clsuuid);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void doWriteNonFatal(java.util.Date r11, java.lang.Thread r12, java.lang.Throwable r13) {
        /*
        r10 = this;
        r1 = 0;
        r8 = r10.getCurrentSessionId();
        if (r8 == 0) goto L_0x00b1;
    L_0x0007:
        com.crashlytics.android.core.CrashlyticsCore.recordLoggedExceptionEvent(r8);
        r0 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2.<init>();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r3 = "Fabric";
        r4 = "Crashlytics is logging non-fatal exception \"";
        r2 = r2.append(r4);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = r2.append(r13);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r4 = "\" from thread ";
        r2 = r2.append(r4);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r4 = r12.getName();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = r2.append(r4);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = r2.toString();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0.mo4289d(r3, r2);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0 = r10.eventCounter;	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0 = r0.getAndIncrement();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0 = io.fabric.sdk.android.services.common.CommonUtils.padWithZerosToMaxIntWidth(r0);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2.<init>();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = r2.append(r8);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r3 = "SessionEvent";
        r2 = r2.append(r3);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0 = r2.append(r0);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r0 = r0.toString();	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r7 = new com.crashlytics.android.core.ClsFileOutputStream;	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r2 = r10.filesDir;	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r7.<init>(r2, r0);	 Catch:{ Exception -> 0x007a, all -> 0x00c6 }
        r1 = com.crashlytics.android.core.CodedOutputStream.newInstance(r7);	 Catch:{ Exception -> 0x00bd, all -> 0x0095 }
        r5 = "error";
        r6 = 0;
        r0 = r10;
        r2 = r11;
        r3 = r12;
        r4 = r13;
        r0.writeSessionEvent(r1, r2, r3, r4, r5, r6);	 Catch:{ Exception -> 0x00bd, all -> 0x00c1 }
        r0 = "Failed to flush to non-fatal file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r0);
        r0 = "Failed to close non-fatal file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r7, r0);
    L_0x0074:
        r0 = 64;
        r10.trimSessionEventFiles(r8, r0);	 Catch:{ Exception -> 0x00a4 }
    L_0x0079:
        return;
    L_0x007a:
        r0 = move-exception;
        r2 = r1;
    L_0x007c:
        r3 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ all -> 0x00cc }
        r4 = "Fabric";
        r5 = "An error occurred in the non-fatal exception logger";
        r3.mo4292e(r4, r5, r0);	 Catch:{ all -> 0x00cc }
        com.crashlytics.android.core.ExceptionUtils.writeStackTraceIfNotNull(r0, r1);	 Catch:{ all -> 0x00cc }
        r0 = "Failed to flush to non-fatal file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r2, r0);
        r0 = "Failed to close non-fatal file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r1, r0);
        goto L_0x0074;
    L_0x0095:
        r0 = move-exception;
        r9 = r0;
        r0 = r1;
        r1 = r9;
    L_0x0099:
        r2 = "Failed to flush to non-fatal file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r2);
        r0 = "Failed to close non-fatal file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r7, r0);
        throw r1;
    L_0x00a4:
        r0 = move-exception;
        r1 = io.fabric.sdk.android.Fabric.getLogger();
        r2 = "Fabric";
        r3 = "An error occurred when trimming non-fatal files.";
        r1.mo4292e(r2, r3, r0);
        goto L_0x0079;
    L_0x00b1:
        r0 = io.fabric.sdk.android.Fabric.getLogger();
        r2 = "Fabric";
        r3 = "Tried to write a non-fatal exception while no session was open.";
        r0.mo4292e(r2, r3, r1);
        goto L_0x0079;
    L_0x00bd:
        r0 = move-exception;
        r2 = r1;
        r1 = r7;
        goto L_0x007c;
    L_0x00c1:
        r0 = move-exception;
        r9 = r0;
        r0 = r1;
        r1 = r9;
        goto L_0x0099;
    L_0x00c6:
        r0 = move-exception;
        r7 = r1;
        r9 = r0;
        r0 = r1;
        r1 = r9;
        goto L_0x0099;
    L_0x00cc:
        r0 = move-exception;
        r7 = r1;
        r1 = r0;
        r0 = r2;
        goto L_0x0099;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler.doWriteNonFatal(java.util.Date, java.lang.Thread, java.lang.Throwable):void");
    }

    private File[] ensureFileArrayNotNull(File[] fileArr) {
        return fileArr == null ? new File[0] : fileArr;
    }

    private String getCurrentSessionId() {
        File[] listFilesMatching = listFilesMatching(new FileNameContainsFilter(SESSION_BEGIN_TAG));
        Arrays.sort(listFilesMatching, LARGEST_FILE_NAME_FIRST);
        return listFilesMatching.length > 0 ? getSessionIdFromSessionFile(listFilesMatching[0]) : null;
    }

    private String getSessionIdFromSessionFile(File file) {
        return file.getName().substring(0, 35);
    }

    private UserMetaData getUserMetaData(String str) {
        return isHandlingException() ? new UserMetaData(this.crashlyticsCore.getUserIdentifier(), this.crashlyticsCore.getUserName(), this.crashlyticsCore.getUserEmail()) : new MetaDataStore(this.filesDir).readUserData(str);
    }

    private void handleUncaughtException(Date date, Thread thread, Throwable th) throws Exception {
        writeFatal(date, thread, th);
        doCloseSessions();
        doOpenSession();
        trimSessionFiles();
        if (!this.crashlyticsCore.shouldPromptUserBeforeSendingCrashReports()) {
            sendSessionReports();
        }
    }

    private File[] listCompleteSessionFiles() {
        return listFilesMatching(SESSION_FILE_FILTER);
    }

    private File[] listFilesMatching(FilenameFilter filenameFilter) {
        return ensureFileArrayNotNull(this.filesDir.listFiles(filenameFilter));
    }

    private File[] listSessionPartFilesFor(String str) {
        return listFilesMatching(new SessionPartFileFilter(str));
    }

    private void notifyCrashlyticsListenerOfPreviousCrash(CrashlyticsListener crashlyticsListener) {
        Fabric.getLogger().mo4289d("Fabric", "Checking for previous crash marker.");
        File file = new File(this.crashlyticsCore.getSdkDirectory(), CLS_CRASH_MARKER_FILE_NAME);
        if (file.exists()) {
            file.delete();
            if (crashlyticsListener != null) {
                try {
                    crashlyticsListener.crashlyticsDidDetectCrashDuringPreviousExecution();
                } catch (Throwable e) {
                    Fabric.getLogger().mo4292e("Fabric", "Exception thrown by CrashlyticsListener while notifying of previous crash.", e);
                }
            }
        }
    }

    private void sendSessionReports() {
        for (final File file : listCompleteSessionFiles()) {
            this.executorServiceWrapper.executeAsync(new Runnable() {
                public void run() {
                    if (CommonUtils.canTryConnection(CrashlyticsUncaughtExceptionHandler.this.crashlyticsCore.getContext())) {
                        Fabric.getLogger().mo4289d("Fabric", "Attempting to send crash report at time of crash...");
                        CreateReportSpiCall createReportSpiCall = CrashlyticsUncaughtExceptionHandler.this.crashlyticsCore.getCreateReportSpiCall(Settings.getInstance().awaitSettingsData());
                        if (createReportSpiCall != null) {
                            new ReportUploader(createReportSpiCall).forceUpload(new SessionReport(file, CrashlyticsUncaughtExceptionHandler.SEND_AT_CRASHTIME_HEADER));
                        }
                    }
                }
            });
        }
    }

    private void trimOpenSessions(int i) {
        int i2 = 0;
        Set hashSet = new HashSet();
        File[] listSessionBeginFiles = listSessionBeginFiles();
        Arrays.sort(listSessionBeginFiles, LARGEST_FILE_NAME_FIRST);
        int min = Math.min(i, listSessionBeginFiles.length);
        for (int i3 = 0; i3 < min; i3++) {
            hashSet.add(getSessionIdFromSessionFile(listSessionBeginFiles[i3]));
        }
        File[] listFilesMatching = listFilesMatching(new AnySessionPartFileFilter());
        int length = listFilesMatching.length;
        while (i2 < length) {
            File file = listFilesMatching[i2];
            Object name = file.getName();
            Matcher matcher = SESSION_FILE_PATTERN.matcher(name);
            matcher.matches();
            if (!hashSet.contains(matcher.group(1))) {
                Fabric.getLogger().mo4289d("Fabric", "Trimming open session file: " + name);
                file.delete();
            }
            i2++;
        }
    }

    private void trimSessionEventFiles(String str, int i) {
        Utils.capFileCount(this.filesDir, new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG), i, SMALLEST_FILE_NAME_FIRST);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void writeBeginSession(java.lang.String r12, java.util.Date r13) throws java.lang.Exception {
        /*
        r11 = this;
        r1 = 0;
        r6 = new com.crashlytics.android.core.ClsFileOutputStream;	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r0 = r11.filesDir;	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r2 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r2.<init>();	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r2 = r2.append(r12);	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r3 = "BeginSession";
        r2 = r2.append(r3);	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r2 = r2.toString();	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r6.<init>(r0, r2);	 Catch:{ Exception -> 0x004b, all -> 0x006e }
        r1 = com.crashlytics.android.core.CodedOutputStream.newInstance(r6);	 Catch:{ Exception -> 0x0060, all -> 0x0064 }
        r0 = java.util.Locale.US;	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r2 = "Crashlytics Android SDK/%s";
        r3 = 1;
        r3 = new java.lang.Object[r3];	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r4 = 0;
        r5 = r11.crashlyticsCore;	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r5 = r5.getVersion();	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r3[r4] = r5;	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r3 = java.lang.String.format(r0, r2, r3);	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r4 = r13.getTime();	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r8 = 1000; // 0x3e8 float:1.401E-42 double:4.94E-321;
        r4 = r4 / r8;
        r0 = r11.sessionDataWriter;	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r2 = r12;
        r0.writeBeginSession(r1, r2, r3, r4);	 Catch:{ Exception -> 0x0060, all -> 0x0069 }
        r0 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r0);
        r0 = "Failed to close begin session file.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r6, r0);
        return;
    L_0x004b:
        r0 = move-exception;
        r2 = r1;
    L_0x004d:
        com.crashlytics.android.core.ExceptionUtils.writeStackTraceIfNotNull(r0, r1);	 Catch:{ all -> 0x0051 }
        throw r0;	 Catch:{ all -> 0x0051 }
    L_0x0051:
        r0 = move-exception;
        r6 = r1;
        r1 = r0;
        r0 = r2;
    L_0x0055:
        r2 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r2);
        r0 = "Failed to close begin session file.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r6, r0);
        throw r1;
    L_0x0060:
        r0 = move-exception;
        r2 = r1;
        r1 = r6;
        goto L_0x004d;
    L_0x0064:
        r0 = move-exception;
        r10 = r0;
        r0 = r1;
        r1 = r10;
        goto L_0x0055;
    L_0x0069:
        r0 = move-exception;
        r10 = r0;
        r0 = r1;
        r1 = r10;
        goto L_0x0055;
    L_0x006e:
        r0 = move-exception;
        r6 = r1;
        r10 = r0;
        r0 = r1;
        r1 = r10;
        goto L_0x0055;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler.writeBeginSession(java.lang.String, java.util.Date):void");
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void writeExternalCrashEvent(com.crashlytics.android.core.internal.models.SessionEventData r8) throws java.io.IOException {
        /*
        r7 = this;
        r0 = 0;
        r2 = r7.getCurrentSessionId();	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        if (r2 == 0) goto L_0x0043;
    L_0x0007:
        com.crashlytics.android.core.CrashlyticsCore.recordFatalExceptionEvent(r2);	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r1 = new com.crashlytics.android.core.ClsFileOutputStream;	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r3 = r7.filesDir;	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r4 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r4.<init>();	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r4 = r4.append(r2);	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r5 = "SessionCrash";
        r4 = r4.append(r5);	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r4 = r4.toString();	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r1.<init>(r3, r4);	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r0 = com.crashlytics.android.core.CodedOutputStream.newInstance(r1);	 Catch:{ Exception -> 0x007b, all -> 0x006f }
        r3 = new com.crashlytics.android.core.MetaDataStore;	 Catch:{ Exception -> 0x0080, all -> 0x006f }
        r4 = r7.filesDir;	 Catch:{ Exception -> 0x0080, all -> 0x006f }
        r3.<init>(r4);	 Catch:{ Exception -> 0x0080, all -> 0x006f }
        r2 = r3.readKeyData(r2);	 Catch:{ Exception -> 0x0080, all -> 0x006f }
        r3 = r7.logFileManager;	 Catch:{ Exception -> 0x0080, all -> 0x006f }
        com.crashlytics.android.core.NativeCrashWriter.writeNativeCrash(r8, r3, r2, r0);	 Catch:{ Exception -> 0x0080, all -> 0x006f }
    L_0x0038:
        r2 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r2);
        r0 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r1, r0);
    L_0x0042:
        return;
    L_0x0043:
        r1 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r2 = "Fabric";
        r3 = "Tried to write a native crash while no session was open.";
        r4 = 0;
        r1.mo4292e(r2, r3, r4);	 Catch:{ Exception -> 0x0051, all -> 0x0085 }
        r1 = r0;
        goto L_0x0038;
    L_0x0051:
        r1 = move-exception;
        r2 = r0;
        r6 = r0;
        r0 = r1;
        r1 = r6;
    L_0x0056:
        r3 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ all -> 0x0088 }
        r4 = "Fabric";
        r5 = "An error occurred in the native crash logger";
        r3.mo4292e(r4, r5, r0);	 Catch:{ all -> 0x0088 }
        com.crashlytics.android.core.ExceptionUtils.writeStackTraceIfNotNull(r0, r1);	 Catch:{ all -> 0x0088 }
        r0 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r2, r0);
        r0 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r1, r0);
        goto L_0x0042;
    L_0x006f:
        r2 = move-exception;
    L_0x0070:
        r3 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r3);
        r0 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r1, r0);
        throw r2;
    L_0x007b:
        r2 = move-exception;
        r6 = r2;
        r2 = r0;
        r0 = r6;
        goto L_0x0056;
    L_0x0080:
        r2 = move-exception;
        r6 = r2;
        r2 = r0;
        r0 = r6;
        goto L_0x0056;
    L_0x0085:
        r2 = move-exception;
        r1 = r0;
        goto L_0x0070;
    L_0x0088:
        r0 = move-exception;
        r6 = r0;
        r0 = r2;
        r2 = r6;
        goto L_0x0070;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler.writeExternalCrashEvent(com.crashlytics.android.core.internal.models.SessionEventData):void");
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void writeFatal(java.util.Date r10, java.lang.Thread r11, java.lang.Throwable r12) {
        /*
        r9 = this;
        r1 = 0;
        r0 = new java.io.File;	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r2 = r9.filesDir;	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r3 = "crash_marker";
        r0.<init>(r2, r3);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r0.createNewFile();	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r0 = r9.getCurrentSessionId();	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        if (r0 == 0) goto L_0x004a;
    L_0x0013:
        com.crashlytics.android.core.CrashlyticsCore.recordFatalExceptionEvent(r0);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r7 = new com.crashlytics.android.core.ClsFileOutputStream;	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r2 = r9.filesDir;	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r3.<init>();	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r0 = r3.append(r0);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r3 = "SessionCrash";
        r0 = r0.append(r3);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r0 = r0.toString();	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r7.<init>(r2, r0);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r1 = com.crashlytics.android.core.CodedOutputStream.newInstance(r7);	 Catch:{ Exception -> 0x0082, all -> 0x0073 }
        r5 = "crash";
        r6 = 1;
        r0 = r9;
        r2 = r10;
        r3 = r11;
        r4 = r12;
        r0.writeSessionEvent(r1, r2, r3, r4, r5, r6);	 Catch:{ Exception -> 0x0082, all -> 0x0084 }
        r0 = r7;
    L_0x003f:
        r2 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r2);
        r1 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r0, r1);
    L_0x0049:
        return;
    L_0x004a:
        r0 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r2 = "Fabric";
        r3 = "Tried to write a fatal exception while no session was open.";
        r4 = 0;
        r0.mo4292e(r2, r3, r4);	 Catch:{ Exception -> 0x0058, all -> 0x0089 }
        r0 = r1;
        goto L_0x003f;
    L_0x0058:
        r0 = move-exception;
        r7 = r1;
    L_0x005a:
        r2 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ all -> 0x008f }
        r3 = "Fabric";
        r4 = "An error occurred in the fatal exception logger";
        r2.mo4292e(r3, r4, r0);	 Catch:{ all -> 0x008f }
        com.crashlytics.android.core.ExceptionUtils.writeStackTraceIfNotNull(r0, r7);	 Catch:{ all -> 0x008f }
        r0 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r0);
        r0 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r7, r0);
        goto L_0x0049;
    L_0x0073:
        r0 = move-exception;
        r8 = r0;
        r0 = r1;
        r1 = r8;
    L_0x0077:
        r2 = "Failed to flush to session begin file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r2);
        r0 = "Failed to close fatal exception file output stream.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r7, r0);
        throw r1;
    L_0x0082:
        r0 = move-exception;
        goto L_0x005a;
    L_0x0084:
        r0 = move-exception;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x0077;
    L_0x0089:
        r0 = move-exception;
        r7 = r1;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x0077;
    L_0x008f:
        r0 = move-exception;
        r8 = r0;
        r0 = r1;
        r1 = r8;
        goto L_0x0077;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler.writeFatal(java.util.Date, java.lang.Thread, java.lang.Throwable):void");
    }

    private void writeInitialPartsTo(CodedOutputStream codedOutputStream, String str) throws IOException {
        for (String str2 : new String[]{SESSION_USER_TAG, SESSION_APP_TAG, SESSION_OS_TAG, SESSION_DEVICE_TAG}) {
            File[] listFilesMatching = listFilesMatching(new FileNameContainsFilter(str + str2));
            if (listFilesMatching.length == 0) {
                Fabric.getLogger().mo4292e("Fabric", "Can't find " + str2 + " data for session ID " + str, null);
            } else {
                Fabric.getLogger().mo4289d("Fabric", "Collecting " + str2 + " data for session ID " + str);
                writeToCosFromFile(codedOutputStream, listFilesMatching[0]);
            }
        }
    }

    private void writeNonFatalEventsTo(CodedOutputStream codedOutputStream, File[] fileArr, String str) {
        Arrays.sort(fileArr, CommonUtils.FILE_MODIFIED_COMPARATOR);
        for (File name : fileArr) {
            try {
                Fabric.getLogger().mo4289d("Fabric", String.format(Locale.US, "Found Non Fatal for session ID %s in %s ", new Object[]{str, name.getName()}));
                writeToCosFromFile(codedOutputStream, name);
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Error writting non-fatal to session.", e);
            }
        }
    }

    private void writeSessionApp(String str) throws Exception {
        OutputStream clsFileOutputStream;
        Throwable e;
        Flushable flushable;
        Throwable th;
        Closeable closeable;
        Object obj;
        Flushable flushable2 = null;
        try {
            clsFileOutputStream = new ClsFileOutputStream(this.filesDir, str + SESSION_APP_TAG);
            try {
                flushable2 = CodedOutputStream.newInstance(clsFileOutputStream);
                this.sessionDataWriter.writeSessionApp(flushable2, this.crashlyticsCore.getPackageName(), this.crashlyticsCore.getVersionCode(), this.crashlyticsCore.getVersionName(), this.idManager.getAppInstallIdentifier(), DeliveryMechanism.determineFrom(this.crashlyticsCore.getInstallerPackageName()).getId());
                CommonUtils.flushOrLog(flushable2, "Failed to flush to session app file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session app file.");
            } catch (Exception e2) {
                e = e2;
                try {
                    ExceptionUtils.writeStackTraceIfNotNull(e, clsFileOutputStream);
                    throw e;
                } catch (Throwable e3) {
                    flushable = flushable2;
                    th = e3;
                    closeable = clsFileOutputStream;
                    CommonUtils.flushOrLog(flushable, "Failed to flush to session app file.");
                    CommonUtils.closeOrLog(closeable, "Failed to close session app file.");
                    throw th;
                }
            } catch (Throwable e32) {
                flushable = flushable2;
                th = e32;
                obj = clsFileOutputStream;
                CommonUtils.flushOrLog(flushable, "Failed to flush to session app file.");
                CommonUtils.closeOrLog(closeable, "Failed to close session app file.");
                throw th;
            }
        } catch (Exception e4) {
            e32 = e4;
            clsFileOutputStream = null;
            ExceptionUtils.writeStackTraceIfNotNull(e32, clsFileOutputStream);
            throw e32;
        } catch (Throwable e322) {
            flushable = null;
            th = e322;
            obj = null;
            CommonUtils.flushOrLog(flushable, "Failed to flush to session app file.");
            CommonUtils.closeOrLog(closeable, "Failed to close session app file.");
            throw th;
        }
    }

    private void writeSessionDevice(String str) throws Exception {
        Flushable newInstance;
        Throwable e;
        OutputStream outputStream;
        Flushable flushable;
        Throwable th;
        Object obj;
        Closeable closeable = null;
        try {
            OutputStream clsFileOutputStream = new ClsFileOutputStream(this.filesDir, str + SESSION_DEVICE_TAG);
            try {
                newInstance = CodedOutputStream.newInstance(clsFileOutputStream);
            } catch (Exception e2) {
                e = e2;
                newInstance = null;
                try {
                    ExceptionUtils.writeStackTraceIfNotNull(e, outputStream);
                    throw e;
                } catch (Throwable e3) {
                    flushable = newInstance;
                    th = e3;
                    closeable = outputStream;
                    CommonUtils.flushOrLog(flushable, "Failed to flush session device info.");
                    CommonUtils.closeOrLog(closeable, "Failed to close session device file.");
                    throw th;
                }
            } catch (Throwable th2) {
                e3 = th2;
                newInstance = null;
                flushable = newInstance;
                th = e3;
                obj = clsFileOutputStream;
                CommonUtils.flushOrLog(flushable, "Failed to flush session device info.");
                CommonUtils.closeOrLog(closeable, "Failed to close session device file.");
                throw th;
            }
            try {
                Context context = this.crashlyticsCore.getContext();
                StatFs statFs = new StatFs(Environment.getDataDirectory().getPath());
                long blockCount = (long) statFs.getBlockCount();
                long blockSize = (long) statFs.getBlockSize();
                this.sessionDataWriter.writeSessionDevice(newInstance, this.idManager.getDeviceUUID(), CommonUtils.getCpuArchitectureInt(), Build.MODEL, Runtime.getRuntime().availableProcessors(), CommonUtils.getTotalRamInBytes(), blockCount * blockSize, CommonUtils.isEmulator(context), this.idManager.getDeviceIdentifiers(), CommonUtils.getDeviceState(context), Build.MANUFACTURER, Build.PRODUCT);
                CommonUtils.flushOrLog(newInstance, "Failed to flush session device info.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session device file.");
            } catch (Exception e4) {
                e3 = e4;
                ExceptionUtils.writeStackTraceIfNotNull(e3, outputStream);
                throw e3;
            } catch (Throwable th3) {
                e3 = th3;
                flushable = newInstance;
                th = e3;
                obj = clsFileOutputStream;
                CommonUtils.flushOrLog(flushable, "Failed to flush session device info.");
                CommonUtils.closeOrLog(closeable, "Failed to close session device file.");
                throw th;
            }
        } catch (Exception e5) {
            e3 = e5;
            outputStream = null;
            newInstance = null;
            ExceptionUtils.writeStackTraceIfNotNull(e3, outputStream);
            throw e3;
        } catch (Throwable th4) {
            th = th4;
            flushable = null;
            CommonUtils.flushOrLog(flushable, "Failed to flush session device info.");
            CommonUtils.closeOrLog(closeable, "Failed to close session device file.");
            throw th;
        }
    }

    private void writeSessionEvent(CodedOutputStream codedOutputStream, Date date, Thread thread, Throwable th, String str, boolean z) throws Exception {
        Thread[] threadArr;
        Map map;
        Context context = this.crashlyticsCore.getContext();
        long time = date.getTime() / 1000;
        float batteryLevel = CommonUtils.getBatteryLevel(context);
        int batteryVelocity = CommonUtils.getBatteryVelocity(context, this.powerConnected);
        boolean proximitySensorEnabled = CommonUtils.getProximitySensorEnabled(context);
        int i = context.getResources().getConfiguration().orientation;
        long totalRamInBytes = CommonUtils.getTotalRamInBytes();
        long calculateFreeRamInBytes = CommonUtils.calculateFreeRamInBytes(context);
        long calculateUsedDiskSpaceInBytes = CommonUtils.calculateUsedDiskSpaceInBytes(Environment.getDataDirectory().getPath());
        RunningAppProcessInfo appProcessInfo = CommonUtils.getAppProcessInfo(context.getPackageName(), context);
        List linkedList = new LinkedList();
        StackTraceElement[] stackTrace = th.getStackTrace();
        if (z) {
            Map allStackTraces = Thread.getAllStackTraces();
            threadArr = new Thread[allStackTraces.size()];
            int i2 = 0;
            for (Entry entry : allStackTraces.entrySet()) {
                threadArr[i2] = (Thread) entry.getKey();
                linkedList.add(entry.getValue());
                i2++;
            }
        } else {
            threadArr = new Thread[0];
        }
        if (CommonUtils.getBooleanResourceValue(context, "com.crashlytics.CollectCustomKeys", true)) {
            Map attributes = this.crashlyticsCore.getAttributes();
            if (attributes == null || attributes.size() <= 1) {
                map = attributes;
            } else {
                Map treeMap = new TreeMap(attributes);
            }
        } else {
            map = new TreeMap();
        }
        this.sessionDataWriter.writeSessionEvent(codedOutputStream, time, thread, th, str, threadArr, batteryLevel, batteryVelocity, proximitySensorEnabled, i, totalRamInBytes - calculateFreeRamInBytes, calculateUsedDiskSpaceInBytes, appProcessInfo, linkedList, stackTrace, this.logFileManager, map);
    }

    private void writeSessionOS(String str) throws Exception {
        Closeable clsFileOutputStream;
        Flushable newInstance;
        Throwable th;
        Throwable e;
        Throwable th2;
        Flushable flushable = null;
        try {
            clsFileOutputStream = new ClsFileOutputStream(this.filesDir, str + SESSION_OS_TAG);
            try {
                newInstance = CodedOutputStream.newInstance((OutputStream) clsFileOutputStream);
                try {
                    this.sessionDataWriter.writeSessionOS(newInstance, CommonUtils.isRooted(this.crashlyticsCore.getContext()));
                    CommonUtils.flushOrLog(newInstance, "Failed to flush to session OS file.");
                    CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session OS file.");
                } catch (Throwable e2) {
                    th = e2;
                    flushable = newInstance;
                    th2 = th;
                    try {
                        ExceptionUtils.writeStackTraceIfNotNull(th2, clsFileOutputStream);
                        throw th2;
                    } catch (Throwable th22) {
                        th = th22;
                        newInstance = flushable;
                        e2 = th;
                        CommonUtils.flushOrLog(newInstance, "Failed to flush to session OS file.");
                        CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session OS file.");
                        throw e2;
                    }
                } catch (Throwable th3) {
                    e2 = th3;
                    CommonUtils.flushOrLog(newInstance, "Failed to flush to session OS file.");
                    CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session OS file.");
                    throw e2;
                }
            } catch (Exception e3) {
                th22 = e3;
                ExceptionUtils.writeStackTraceIfNotNull(th22, clsFileOutputStream);
                throw th22;
            } catch (Throwable th222) {
                th = th222;
                newInstance = null;
                e2 = th;
                CommonUtils.flushOrLog(newInstance, "Failed to flush to session OS file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session OS file.");
                throw e2;
            }
        } catch (Exception e4) {
            th222 = e4;
            clsFileOutputStream = null;
            ExceptionUtils.writeStackTraceIfNotNull(th222, clsFileOutputStream);
            throw th222;
        } catch (Throwable th2222) {
            clsFileOutputStream = null;
            th = th2222;
            newInstance = null;
            e2 = th;
            CommonUtils.flushOrLog(newInstance, "Failed to flush to session OS file.");
            CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session OS file.");
            throw e2;
        }
    }

    private void writeSessionPartsToSessionFile(File file, String str, int i) {
        OutputStream clsFileOutputStream;
        Throwable e;
        Flushable flushable;
        Throwable th;
        Closeable closeable;
        Throwable th2;
        Flushable flushable2;
        Fabric.getLogger().mo4289d("Fabric", "Collecting session parts for ID " + str);
        File[] listFilesMatching = listFilesMatching(new FileNameContainsFilter(str + SESSION_FATAL_TAG));
        boolean z = listFilesMatching != null && listFilesMatching.length > 0;
        Fabric.getLogger().mo4289d("Fabric", String.format(Locale.US, "Session %s has fatal exception: %s", new Object[]{str, Boolean.valueOf(z)}));
        File[] listFilesMatching2 = listFilesMatching(new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG));
        boolean z2 = listFilesMatching2 != null && listFilesMatching2.length > 0;
        Fabric.getLogger().mo4289d("Fabric", String.format(Locale.US, "Session %s has non-fatal exceptions: %s", new Object[]{str, Boolean.valueOf(z2)}));
        if (z || z2) {
            try {
                clsFileOutputStream = new ClsFileOutputStream(this.filesDir, str);
                try {
                    Flushable newInstance = CodedOutputStream.newInstance(clsFileOutputStream);
                    try {
                        Fabric.getLogger().mo4289d("Fabric", "Collecting SessionStart data for session ID " + str);
                        writeToCosFromFile(newInstance, file);
                        newInstance.writeUInt64(4, new Date().getTime() / 1000);
                        newInstance.writeBool(5, z);
                        writeInitialPartsTo(newInstance, str);
                        if (z2) {
                            File[] listFilesMatching3;
                            if (listFilesMatching2.length > i) {
                                Fabric.getLogger().mo4289d("Fabric", String.format(Locale.US, "Trimming down to %d logged exceptions.", new Object[]{Integer.valueOf(i)}));
                                trimSessionEventFiles(str, i);
                                listFilesMatching3 = listFilesMatching(new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG));
                            } else {
                                listFilesMatching3 = listFilesMatching2;
                            }
                            writeNonFatalEventsTo(newInstance, listFilesMatching3, str);
                        }
                        if (z) {
                            writeToCosFromFile(newInstance, listFilesMatching[0]);
                        }
                        newInstance.writeUInt32(11, 1);
                        newInstance.writeEnum(12, 3);
                        CommonUtils.flushOrLog(newInstance, "Error flushing session file stream");
                        CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close CLS file");
                    } catch (Exception e2) {
                        e = e2;
                        flushable = newInstance;
                        try {
                            Fabric.getLogger().mo4292e("Fabric", "Failed to write session file for session ID: " + str, e);
                            ExceptionUtils.writeStackTraceIfNotNull(e, clsFileOutputStream);
                            CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                            closeWithoutRenamingOrLog(clsFileOutputStream);
                            Fabric.getLogger().mo4289d("Fabric", "Removing session part files for ID " + str);
                            deleteSessionPartFilesFor(str);
                        } catch (Throwable th3) {
                            th = th3;
                            Object obj = clsFileOutputStream;
                            CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                            CommonUtils.closeOrLog(closeable, "Failed to close CLS file");
                            throw th;
                        }
                    } catch (Throwable th4) {
                        th2 = th4;
                        flushable2 = newInstance;
                        th = th2;
                        flushable = flushable2;
                        closeable = clsFileOutputStream;
                        CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                        CommonUtils.closeOrLog(closeable, "Failed to close CLS file");
                        throw th;
                    }
                } catch (Exception e3) {
                    e = e3;
                    flushable = null;
                    Fabric.getLogger().mo4292e("Fabric", "Failed to write session file for session ID: " + str, e);
                    ExceptionUtils.writeStackTraceIfNotNull(e, clsFileOutputStream);
                    CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                    closeWithoutRenamingOrLog(clsFileOutputStream);
                    Fabric.getLogger().mo4289d("Fabric", "Removing session part files for ID " + str);
                    deleteSessionPartFilesFor(str);
                } catch (Throwable th5) {
                    th2 = th5;
                    flushable2 = null;
                    th = th2;
                    flushable = flushable2;
                    closeable = clsFileOutputStream;
                    CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                    CommonUtils.closeOrLog(closeable, "Failed to close CLS file");
                    throw th;
                }
            } catch (Exception e4) {
                e = e4;
                clsFileOutputStream = null;
                flushable = null;
                Fabric.getLogger().mo4292e("Fabric", "Failed to write session file for session ID: " + str, e);
                ExceptionUtils.writeStackTraceIfNotNull(e, clsFileOutputStream);
                CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                closeWithoutRenamingOrLog(clsFileOutputStream);
                Fabric.getLogger().mo4289d("Fabric", "Removing session part files for ID " + str);
                deleteSessionPartFilesFor(str);
            } catch (Throwable th6) {
                th = th6;
                closeable = null;
                flushable = null;
                CommonUtils.flushOrLog(flushable, "Error flushing session file stream");
                CommonUtils.closeOrLog(closeable, "Failed to close CLS file");
                throw th;
            }
        }
        Fabric.getLogger().mo4289d("Fabric", "No events present for session ID " + str);
        Fabric.getLogger().mo4289d("Fabric", "Removing session part files for ID " + str);
        deleteSessionPartFilesFor(str);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void writeSessionUser(java.lang.String r8) throws java.lang.Exception {
        /*
        r7 = this;
        r1 = 0;
        r0 = new com.crashlytics.android.core.ClsFileOutputStream;	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r2 = r7.filesDir;	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r3 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r3.<init>();	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r3 = r3.append(r8);	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r4 = "SessionUser";
        r3 = r3.append(r4);	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r3 = r3.toString();	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r0.<init>(r2, r3);	 Catch:{ Exception -> 0x004a, all -> 0x006f }
        r1 = com.crashlytics.android.core.CodedOutputStream.newInstance(r0);	 Catch:{ Exception -> 0x0060, all -> 0x006d }
        r2 = r7.getUserMetaData(r8);	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r3 = r2.isEmpty();	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        if (r3 == 0) goto L_0x0034;
    L_0x0029:
        r2 = "Failed to flush session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r2);
        r1 = "Failed to close session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r0, r1);
    L_0x0033:
        return;
    L_0x0034:
        r3 = r7.sessionDataWriter;	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r4 = r2.id;	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r5 = r2.name;	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r2 = r2.email;	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r3.writeSessionUser(r1, r4, r5, r2);	 Catch:{ Exception -> 0x0068, all -> 0x006d }
        r2 = "Failed to flush session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r2);
        r1 = "Failed to close session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r0, r1);
        goto L_0x0033;
    L_0x004a:
        r0 = move-exception;
        r2 = r1;
    L_0x004c:
        com.crashlytics.android.core.ExceptionUtils.writeStackTraceIfNotNull(r0, r1);	 Catch:{ all -> 0x0050 }
        throw r0;	 Catch:{ all -> 0x0050 }
    L_0x0050:
        r0 = move-exception;
        r6 = r0;
        r0 = r1;
        r1 = r2;
        r2 = r6;
    L_0x0055:
        r3 = "Failed to flush session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r3);
        r1 = "Failed to close session user file.";
        io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r0, r1);
        throw r2;
    L_0x0060:
        r2 = move-exception;
        r6 = r2;
        r2 = r1;
        r1 = r6;
    L_0x0064:
        r6 = r1;
        r1 = r0;
        r0 = r6;
        goto L_0x004c;
    L_0x0068:
        r2 = move-exception;
        r6 = r2;
        r2 = r1;
        r1 = r6;
        goto L_0x0064;
    L_0x006d:
        r2 = move-exception;
        goto L_0x0055;
    L_0x006f:
        r2 = move-exception;
        r0 = r1;
        goto L_0x0055;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler.writeSessionUser(java.lang.String):void");
    }

    private void writeToCosFromFile(CodedOutputStream codedOutputStream, File file) throws IOException {
        Throwable th;
        if (file.exists()) {
            byte[] bArr = new byte[((int) file.length())];
            Closeable fileInputStream;
            try {
                fileInputStream = new FileInputStream(file);
                int i = 0;
                while (i < bArr.length) {
                    try {
                        int read = fileInputStream.read(bArr, i, bArr.length - i);
                        if (read < 0) {
                            break;
                        }
                        i += read;
                    } catch (Throwable th2) {
                        th = th2;
                    }
                }
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream.");
                codedOutputStream.writeRawBytes(bArr);
                return;
            } catch (Throwable th3) {
                th = th3;
                fileInputStream = null;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream.");
                throw th;
            }
        }
        Fabric.getLogger().mo4292e("Fabric", "Tried to include a file that doesn't exist: " + file.getName(), null);
    }

    void cacheKeyData(final Map<String, String> map) {
        this.executorServiceWrapper.executeAsync(new Callable<Void>() {
            public Void call() throws Exception {
                new MetaDataStore(CrashlyticsUncaughtExceptionHandler.this.filesDir).writeKeyData(CrashlyticsUncaughtExceptionHandler.this.getCurrentSessionId(), map);
                return null;
            }
        });
    }

    void cacheUserData(final String str, final String str2, final String str3) {
        this.executorServiceWrapper.executeAsync(new Callable<Void>() {
            public Void call() throws Exception {
                new MetaDataStore(CrashlyticsUncaughtExceptionHandler.this.filesDir).writeUserData(CrashlyticsUncaughtExceptionHandler.this.getCurrentSessionId(), new UserMetaData(str, str2, str3));
                return null;
            }
        });
    }

    void cleanInvalidTempFiles() {
        this.executorServiceWrapper.executeAsync(new Runnable() {
            public void run() {
                CrashlyticsUncaughtExceptionHandler.this.doCleanInvalidTempFiles(CrashlyticsUncaughtExceptionHandler.this.listFilesMatching(ClsFileOutputStream.TEMP_FILENAME_FILTER));
            }
        });
    }

    void doCleanInvalidTempFiles(File[] fileArr) {
        deleteLegacyInvalidCacheDir();
        for (File file : fileArr) {
            Fabric.getLogger().mo4289d("Fabric", "Found invalid session part file: " + file);
            final String sessionIdFromSessionFile = getSessionIdFromSessionFile(file);
            FilenameFilter anonymousClass15 = new FilenameFilter() {
                public boolean accept(File file, String str) {
                    return str.startsWith(sessionIdFromSessionFile);
                }
            };
            Fabric.getLogger().mo4289d("Fabric", "Deleting all part files for invalid session: " + sessionIdFromSessionFile);
            for (File file2 : listFilesMatching(anonymousClass15)) {
                Fabric.getLogger().mo4289d("Fabric", "Deleting session file: " + file2);
                file2.delete();
            }
        }
    }

    void ensureOpenSessionExists() {
        this.executorServiceWrapper.executeAsync(new Callable<Void>() {
            public Void call() throws Exception {
                if (CrashlyticsUncaughtExceptionHandler.this.hasOpenSession()) {
                    CrashlyticsUncaughtExceptionHandler.this.logFileManager.onSessionChange(CrashlyticsUncaughtExceptionHandler.this.getCurrentSessionId());
                } else {
                    CrashlyticsUncaughtExceptionHandler.this.doOpenSession();
                }
                return null;
            }
        });
    }

    boolean finalizeSessions() {
        return ((Boolean) this.executorServiceWrapper.executeSyncLoggingException(new Callable<Boolean>() {
            public Boolean call() throws Exception {
                if (CrashlyticsUncaughtExceptionHandler.this.isHandlingException.get()) {
                    Fabric.getLogger().mo4289d("Fabric", "Skipping session finalization because a crash has already occurred.");
                    return Boolean.valueOf(false);
                }
                SessionEventData externalCrashEventData = CrashlyticsUncaughtExceptionHandler.this.crashlyticsCore.getExternalCrashEventData();
                if (externalCrashEventData != null) {
                    CrashlyticsUncaughtExceptionHandler.this.writeExternalCrashEvent(externalCrashEventData);
                }
                CrashlyticsUncaughtExceptionHandler.this.doCloseSessions();
                CrashlyticsUncaughtExceptionHandler.this.doOpenSession();
                Fabric.getLogger().mo4289d("Fabric", "Open sessions were closed and a new session was opened.");
                return Boolean.valueOf(true);
            }
        })).booleanValue();
    }

    boolean hasOpenSession() {
        return listSessionBeginFiles().length > 0;
    }

    boolean isHandlingException() {
        return this.isHandlingException.get();
    }

    File[] listSessionBeginFiles() {
        return listFilesMatching(new FileNameContainsFilter(SESSION_BEGIN_TAG));
    }

    void trimSessionFiles() {
        Utils.capFileCount(this.filesDir, SESSION_FILE_FILTER, 4, SMALLEST_FILE_NAME_FIRST);
    }

    public void uncaughtException(final Thread thread, final Throwable th) {
        synchronized (this) {
            this.isHandlingException.set(true);
            try {
                Fabric.getLogger().mo4289d("Fabric", "Crashlytics is handling uncaught exception \"" + th + "\" from thread " + thread.getName());
                if (!this.receiversRegistered.getAndSet(true)) {
                    Fabric.getLogger().mo4289d("Fabric", "Unregistering power receivers.");
                    Context context = this.crashlyticsCore.getContext();
                    context.unregisterReceiver(this.powerConnectedReceiver);
                    context.unregisterReceiver(this.powerDisconnectedReceiver);
                }
                final Date date = new Date();
                this.executorServiceWrapper.executeSyncLoggingException(new Callable<Void>() {
                    public Void call() throws Exception {
                        CrashlyticsUncaughtExceptionHandler.this.handleUncaughtException(date, thread, th);
                        return null;
                    }
                });
                Fabric.getLogger().mo4289d("Fabric", "Crashlytics completed exception processing. Invoking default exception handler.");
                this.defaultHandler.uncaughtException(thread, th);
                this.isHandlingException.set(false);
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "An error occurred in the uncaught exception handler", e);
                Fabric.getLogger().mo4289d("Fabric", "Crashlytics completed exception processing. Invoking default exception handler.");
                this.defaultHandler.uncaughtException(thread, th);
                this.isHandlingException.set(false);
            } catch (Throwable th2) {
                Fabric.getLogger().mo4289d("Fabric", "Crashlytics completed exception processing. Invoking default exception handler.");
                this.defaultHandler.uncaughtException(thread, th);
                this.isHandlingException.set(false);
            }
        }
    }

    void writeNonFatalException(final Thread thread, final Throwable th) {
        final Date date = new Date();
        this.executorServiceWrapper.executeAsync(new Runnable() {
            public void run() {
                if (!CrashlyticsUncaughtExceptionHandler.this.isHandlingException.get()) {
                    CrashlyticsUncaughtExceptionHandler.this.doWriteNonFatal(date, thread, th);
                }
            }
        });
    }

    void writeToLog(final long j, final String str) {
        this.executorServiceWrapper.executeAsync(new Callable<Void>() {
            public Void call() throws Exception {
                if (!CrashlyticsUncaughtExceptionHandler.this.isHandlingException.get()) {
                    CrashlyticsUncaughtExceptionHandler.this.logFileManager.writeToLog(j, str);
                }
                return null;
            }
        });
    }
}

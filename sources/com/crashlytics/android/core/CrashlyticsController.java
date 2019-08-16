package com.crashlytics.android.core;

import android.app.Activity;
import android.app.ActivityManager.RunningAppProcessInfo;
import android.content.Context;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Environment;
import android.os.StatFs;
import android.text.TextUtils;
import com.crashlytics.android.answers.Answers;
import com.crashlytics.android.answers.EventLogger;
import com.crashlytics.android.core.LogFileManager.DirectoryProvider;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.ServerProtocol;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FilenameFilter;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.TreeMap;
import java.util.TreeSet;
import java.util.concurrent.Callable;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.GZIPOutputStream;
import org.json.JSONObject;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.Crash.FatalException;
import p017io.fabric.sdk.android.services.common.Crash.LoggedException;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.common.DeliveryMechanism;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;
import p017io.fabric.sdk.android.services.persistence.FileStore;
import p017io.fabric.sdk.android.services.settings.FeaturesSettingsData;
import p017io.fabric.sdk.android.services.settings.PromptSettingsData;
import p017io.fabric.sdk.android.services.settings.SessionSettingsData;
import p017io.fabric.sdk.android.services.settings.Settings;
import p017io.fabric.sdk.android.services.settings.SettingsData;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

class CrashlyticsController {
    private static final int ANALYZER_VERSION = 1;
    private static final String COLLECT_CUSTOM_KEYS = "com.crashlytics.CollectCustomKeys";
    private static final String CRASHLYTICS_API_ENDPOINT = "com.crashlytics.ApiEndpoint";
    private static final String EVENT_TYPE_CRASH = "crash";
    private static final String EVENT_TYPE_LOGGED = "error";
    static final String FATAL_SESSION_DIR = "fatal-sessions";
    static final String FIREBASE_ANALYTICS_ORIGIN_CRASHLYTICS = "clx";
    static final String FIREBASE_APPLICATION_EXCEPTION = "_ae";
    static final String FIREBASE_CRASH_TYPE = "fatal";
    static final int FIREBASE_CRASH_TYPE_FATAL = 1;
    static final String FIREBASE_REALTIME = "_r";
    static final String FIREBASE_TIMESTAMP = "timestamp";
    private static final String GENERATOR_FORMAT = "Crashlytics Android SDK/%s";
    private static final String[] INITIAL_SESSION_PART_TAGS = {SESSION_USER_TAG, SESSION_APP_TAG, SESSION_OS_TAG, SESSION_DEVICE_TAG};
    static final String INVALID_CLS_CACHE_DIR = "invalidClsFiles";
    static final Comparator<File> LARGEST_FILE_NAME_FIRST = new Comparator<File>() {
        public int compare(File file, File file2) {
            return file2.getName().compareTo(file.getName());
        }
    };
    static final int MAX_INVALID_SESSIONS = 4;
    private static final int MAX_LOCAL_LOGGED_EXCEPTIONS = 64;
    static final int MAX_OPEN_SESSIONS = 8;
    static final int MAX_STACK_SIZE = 1024;
    static final String NONFATAL_SESSION_DIR = "nonfatal-sessions";
    static final int NUM_STACK_REPETITIONS_ALLOWED = 10;
    private static final Map<String, String> SEND_AT_CRASHTIME_HEADER = Collections.singletonMap("X-CRASHLYTICS-SEND-FLAGS", AppEventsConstants.EVENT_PARAM_VALUE_YES);
    static final String SESSION_APP_TAG = "SessionApp";
    static final FilenameFilter SESSION_BEGIN_FILE_FILTER = new FileNameContainsFilter(SESSION_BEGIN_TAG) {
        public boolean accept(File file, String str) {
            return super.accept(file, str) && str.endsWith(ClsFileOutputStream.SESSION_FILE_EXTENSION);
        }
    };
    static final String SESSION_BEGIN_TAG = "BeginSession";
    static final String SESSION_DEVICE_TAG = "SessionDevice";
    static final FileFilter SESSION_DIRECTORY_FILTER = new FileFilter() {
        public boolean accept(File file) {
            return file.isDirectory() && file.getName().length() == 35;
        }
    };
    static final String SESSION_EVENT_MISSING_BINARY_IMGS_TAG = "SessionMissingBinaryImages";
    static final String SESSION_FATAL_TAG = "SessionCrash";
    static final FilenameFilter SESSION_FILE_FILTER = new FilenameFilter() {
        public boolean accept(File file, String str) {
            return str.length() == ClsFileOutputStream.SESSION_FILE_EXTENSION.length() + 35 && str.endsWith(ClsFileOutputStream.SESSION_FILE_EXTENSION);
        }
    };
    /* access modifiers changed from: private */
    public static final Pattern SESSION_FILE_PATTERN = Pattern.compile("([\\d|A-Z|a-z]{12}\\-[\\d|A-Z|a-z]{4}\\-[\\d|A-Z|a-z]{4}\\-[\\d|A-Z|a-z]{12}).+");
    private static final int SESSION_ID_LENGTH = 35;
    static final String SESSION_JSON_SUFFIX = ".json";
    static final String SESSION_NON_FATAL_TAG = "SessionEvent";
    static final String SESSION_OS_TAG = "SessionOS";
    static final String SESSION_USER_TAG = "SessionUser";
    private static final boolean SHOULD_PROMPT_BEFORE_SENDING_REPORTS_DEFAULT = false;
    static final Comparator<File> SMALLEST_FILE_NAME_FIRST = new Comparator<File>() {
        public int compare(File file, File file2) {
            return file.getName().compareTo(file2.getName());
        }
    };
    /* access modifiers changed from: private */
    public final AppData appData;
    private final AppMeasurementEventListenerRegistrar appMeasurementEventListenerRegistrar;
    private final CrashlyticsBackgroundWorker backgroundWorker;
    private CrashlyticsUncaughtExceptionHandler crashHandler;
    /* access modifiers changed from: private */
    public final CrashlyticsCore crashlyticsCore;
    private final DevicePowerStateListener devicePowerStateListener;
    private final AtomicInteger eventCounter = new AtomicInteger(0);
    private final FileStore fileStore;
    private final EventLogger firebaseAnalyticsLogger;
    private final HandlingExceptionCheck handlingExceptionCheck;
    private final HttpRequestFactory httpRequestFactory;
    private final IdManager idManager;
    private final LogFileDirectoryProvider logFileDirectoryProvider;
    /* access modifiers changed from: private */
    public final LogFileManager logFileManager;
    private final PreferenceManager preferenceManager;
    private final ReportFilesProvider reportFilesProvider;
    private final StackTraceTrimmingStrategy stackTraceTrimmingStrategy;
    /* access modifiers changed from: private */
    public final String unityVersion;

    private static class AnySessionPartFileFilter implements FilenameFilter {
        private AnySessionPartFileFilter() {
        }

        public boolean accept(File file, String str) {
            return !CrashlyticsController.SESSION_FILE_FILTER.accept(file, str) && CrashlyticsController.SESSION_FILE_PATTERN.matcher(str).matches();
        }
    }

    private interface CodedOutputStreamWriteAction {
        void writeTo(CodedOutputStream codedOutputStream) throws Exception;
    }

    private static final class DefaultSettingsDataProvider implements SettingsDataProvider {
        private DefaultSettingsDataProvider() {
        }

        public SettingsData getSettingsData() {
            return Settings.getInstance().awaitSettingsData();
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

    private interface FileOutputStreamWriteAction {
        void writeTo(FileOutputStream fileOutputStream) throws Exception;
    }

    static class InvalidPartFileFilter implements FilenameFilter {
        InvalidPartFileFilter() {
        }

        public boolean accept(File file, String str) {
            return ClsFileOutputStream.TEMP_FILENAME_FILTER.accept(file, str) || str.contains(CrashlyticsController.SESSION_EVENT_MISSING_BINARY_IMGS_TAG);
        }
    }

    private static final class LogFileDirectoryProvider implements DirectoryProvider {
        private static final String LOG_FILES_DIR = "log-files";
        private final FileStore rootFileStore;

        public LogFileDirectoryProvider(FileStore fileStore) {
            this.rootFileStore = fileStore;
        }

        public File getLogFileDir() {
            File file = new File(this.rootFileStore.getFilesDir(), LOG_FILES_DIR);
            if (!file.exists()) {
                file.mkdirs();
            }
            return file;
        }
    }

    private static final class PrivacyDialogCheck implements SendCheck {
        private final Kit kit;
        /* access modifiers changed from: private */
        public final PreferenceManager preferenceManager;
        private final PromptSettingsData promptData;

        public PrivacyDialogCheck(Kit kit2, PreferenceManager preferenceManager2, PromptSettingsData promptSettingsData) {
            this.kit = kit2;
            this.preferenceManager = preferenceManager2;
            this.promptData = promptSettingsData;
        }

        public boolean canSendReports() {
            Activity currentActivity = this.kit.getFabric().getCurrentActivity();
            if (currentActivity == null || currentActivity.isFinishing()) {
                return true;
            }
            final CrashPromptDialog create = CrashPromptDialog.create(currentActivity, this.promptData, new AlwaysSendCallback() {
                public void sendUserReportsWithoutPrompting(boolean z) {
                    PrivacyDialogCheck.this.preferenceManager.setShouldAlwaysSendReports(z);
                }
            });
            currentActivity.runOnUiThread(new Runnable() {
                public void run() {
                    create.show();
                }
            });
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Waiting for user opt-in.");
            create.await();
            return create.getOptIn();
        }
    }

    private final class ReportUploaderFilesProvider implements ReportFilesProvider {
        private ReportUploaderFilesProvider() {
        }

        public File[] getCompleteSessionFiles() {
            return CrashlyticsController.this.listCompleteSessionFiles();
        }

        public File[] getInvalidSessionFiles() {
            return CrashlyticsController.this.getInvalidFilesDir().listFiles();
        }

        public File[] getNativeReportFiles() {
            return CrashlyticsController.this.listNativeSessionFileDirectories();
        }
    }

    private final class ReportUploaderHandlingExceptionCheck implements HandlingExceptionCheck {
        private ReportUploaderHandlingExceptionCheck() {
        }

        public boolean isHandlingException() {
            return CrashlyticsController.this.isHandlingException();
        }
    }

    private static final class SendReportRunnable implements Runnable {
        private final Context context;
        private final Report report;
        private final ReportUploader reportUploader;

        public SendReportRunnable(Context context2, Report report2, ReportUploader reportUploader2) {
            this.context = context2;
            this.report = report2;
            this.reportUploader = reportUploader2;
        }

        public void run() {
            if (CommonUtils.canTryConnection(this.context)) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Attempting to send crash report at time of crash...");
                this.reportUploader.forceUpload(this.report);
            }
        }
    }

    static class SessionPartFileFilter implements FilenameFilter {
        private final String sessionId;

        public SessionPartFileFilter(String str) {
            this.sessionId = str;
        }

        public boolean accept(File file, String str) {
            return !str.equals(new StringBuilder().append(this.sessionId).append(ClsFileOutputStream.SESSION_FILE_EXTENSION).toString()) && str.contains(this.sessionId) && !str.endsWith(ClsFileOutputStream.IN_PROGRESS_SESSION_FILE_EXTENSION);
        }
    }

    CrashlyticsController(CrashlyticsCore crashlyticsCore2, CrashlyticsBackgroundWorker crashlyticsBackgroundWorker, HttpRequestFactory httpRequestFactory2, IdManager idManager2, PreferenceManager preferenceManager2, FileStore fileStore2, AppData appData2, UnityVersionProvider unityVersionProvider, AppMeasurementEventListenerRegistrar appMeasurementEventListenerRegistrar2, EventLogger eventLogger) {
        this.crashlyticsCore = crashlyticsCore2;
        this.backgroundWorker = crashlyticsBackgroundWorker;
        this.httpRequestFactory = httpRequestFactory2;
        this.idManager = idManager2;
        this.preferenceManager = preferenceManager2;
        this.fileStore = fileStore2;
        this.appData = appData2;
        this.unityVersion = unityVersionProvider.getUnityVersion();
        this.appMeasurementEventListenerRegistrar = appMeasurementEventListenerRegistrar2;
        this.firebaseAnalyticsLogger = eventLogger;
        Context context = crashlyticsCore2.getContext();
        this.logFileDirectoryProvider = new LogFileDirectoryProvider(fileStore2);
        this.logFileManager = new LogFileManager(context, this.logFileDirectoryProvider);
        this.reportFilesProvider = new ReportUploaderFilesProvider();
        this.handlingExceptionCheck = new ReportUploaderHandlingExceptionCheck();
        this.devicePowerStateListener = new DevicePowerStateListener(context);
        this.stackTraceTrimmingStrategy = new MiddleOutFallbackStrategy(1024, new RemoveRepeatsStrategy(10));
    }

    private void closeOpenSessions(File[] fileArr, int i, int i2) {
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Closing open sessions.");
        while (i < fileArr.length) {
            File file = fileArr[i];
            String sessionIdFromSessionFile = getSessionIdFromSessionFile(file);
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Closing session: " + sessionIdFromSessionFile);
            writeSessionPartsToSessionFile(file, sessionIdFromSessionFile, i2);
            i++;
        }
    }

    private void closeWithoutRenamingOrLog(ClsFileOutputStream clsFileOutputStream) {
        if (clsFileOutputStream != null) {
            try {
                clsFileOutputStream.closeInProgressStream();
            } catch (IOException e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error closing session file stream in the presence of an exception", e);
            }
        }
    }

    private static void copyToCodedOutputStream(InputStream inputStream, CodedOutputStream codedOutputStream, int i) throws IOException {
        byte[] bArr = new byte[i];
        int i2 = 0;
        while (i2 < bArr.length) {
            int read = inputStream.read(bArr, i2, bArr.length - i2);
            if (read < 0) {
                break;
            }
            i2 += read;
        }
        codedOutputStream.writeRawBytes(bArr);
    }

    private void deleteSessionPartFilesFor(String str) {
        for (File delete : listSessionPartFilesFor(str)) {
            delete.delete();
        }
    }

    /* access modifiers changed from: private */
    public void doCloseSessions(SessionSettingsData sessionSettingsData, boolean z) throws Exception {
        int i = z ? 1 : 0;
        trimOpenSessions(i + 8);
        File[] listSortedSessionBeginFiles = listSortedSessionBeginFiles();
        if (listSortedSessionBeginFiles.length <= i) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "No open sessions to be closed.");
            return;
        }
        writeSessionUser(getSessionIdFromSessionFile(listSortedSessionBeginFiles[i]));
        if (sessionSettingsData == null) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Unable to close session. Settings are not loaded.");
        } else {
            closeOpenSessions(listSortedSessionBeginFiles, i, sessionSettingsData.maxCustomExceptionEvents);
        }
    }

    /* access modifiers changed from: private */
    public void doOpenSession() throws Exception {
        Date date = new Date();
        String clsuuid = new CLSUUID(this.idManager).toString();
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Opening a new session with ID " + clsuuid);
        writeBeginSession(clsuuid, date);
        writeSessionApp(clsuuid);
        writeSessionOS(clsuuid);
        writeSessionDevice(clsuuid);
        this.logFileManager.setCurrentSession(clsuuid);
    }

    /* access modifiers changed from: private */
    public void doWriteNonFatal(Date date, Thread thread, Throwable th) {
        ClsFileOutputStream clsFileOutputStream;
        Throwable th2;
        CodedOutputStream codedOutputStream;
        CodedOutputStream codedOutputStream2;
        CodedOutputStream codedOutputStream3;
        ClsFileOutputStream clsFileOutputStream2;
        CodedOutputStream codedOutputStream4 = null;
        String currentSessionId = getCurrentSessionId();
        if (currentSessionId == null) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Tried to write a non-fatal exception while no session was open.", null);
            return;
        }
        recordLoggedExceptionAnswersEvent(currentSessionId, th.getClass().getName());
        try {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Crashlytics is logging non-fatal exception \"" + th + "\" from thread " + thread.getName());
            clsFileOutputStream = new ClsFileOutputStream(getFilesDir(), currentSessionId + SESSION_NON_FATAL_TAG + CommonUtils.padWithZerosToMaxIntWidth(this.eventCounter.getAndIncrement()));
            try {
                CodedOutputStream newInstance = CodedOutputStream.newInstance((OutputStream) clsFileOutputStream);
                try {
                    writeSessionEvent(newInstance, date, thread, th, "error", false);
                    CommonUtils.flushOrLog(newInstance, "Failed to flush to non-fatal file.");
                    CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close non-fatal file output stream.");
                } catch (Exception e) {
                    e = e;
                    codedOutputStream4 = newInstance;
                    clsFileOutputStream2 = clsFileOutputStream;
                    codedOutputStream3 = codedOutputStream4;
                    try {
                        Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the non-fatal exception logger", e);
                        CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to non-fatal file.");
                        CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close non-fatal file output stream.");
                        trimSessionEventFiles(currentSessionId, 64);
                    } catch (Throwable th3) {
                        th = th3;
                        clsFileOutputStream = clsFileOutputStream2;
                        codedOutputStream2 = codedOutputStream3;
                        th2 = th;
                        CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to non-fatal file.");
                        CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close non-fatal file output stream.");
                        throw th2;
                    }
                } catch (Throwable th4) {
                    th = th4;
                    codedOutputStream = newInstance;
                    th2 = th;
                    CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to non-fatal file.");
                    CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close non-fatal file output stream.");
                    throw th2;
                }
            } catch (Exception e2) {
                e = e2;
                clsFileOutputStream2 = clsFileOutputStream;
                codedOutputStream3 = codedOutputStream4;
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the non-fatal exception logger", e);
                CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to non-fatal file.");
                CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close non-fatal file output stream.");
                trimSessionEventFiles(currentSessionId, 64);
            } catch (Throwable th5) {
                th = th5;
                codedOutputStream = null;
                th2 = th;
                CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to non-fatal file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close non-fatal file output stream.");
                throw th2;
            }
        } catch (Exception e3) {
            e = e3;
            clsFileOutputStream2 = null;
            codedOutputStream3 = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the non-fatal exception logger", e);
            CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to non-fatal file.");
            CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close non-fatal file output stream.");
            trimSessionEventFiles(currentSessionId, 64);
        } catch (Throwable th6) {
            th = th6;
            clsFileOutputStream = null;
            codedOutputStream2 = null;
            th2 = th;
            CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to non-fatal file.");
            CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close non-fatal file output stream.");
            throw th2;
        }
        try {
            trimSessionEventFiles(currentSessionId, 64);
        } catch (Exception e4) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred when trimming non-fatal files.", e4);
        }
    }

    private File[] ensureFileArrayNotNull(File[] fileArr) {
        return fileArr == null ? new File[0] : fileArr;
    }

    /* access modifiers changed from: private */
    public void finalizeMostRecentNativeCrash(Context context, File file, String str) throws IOException {
        byte[] minidumpFromDirectory = NativeFileUtils.minidumpFromDirectory(file);
        byte[] metadataJsonFromDirectory = NativeFileUtils.metadataJsonFromDirectory(file);
        byte[] binaryImagesJsonFromDirectory = NativeFileUtils.binaryImagesJsonFromDirectory(file, context);
        if (minidumpFromDirectory == null || minidumpFromDirectory.length == 0) {
            Fabric.getLogger().mo20982w(CrashlyticsCore.TAG, "No minidump data found in directory " + file);
            return;
        }
        recordFatalExceptionAnswersEvent(str, "<native-crash: minidump>");
        byte[] readFile = readFile(str, "BeginSession.json");
        byte[] readFile2 = readFile(str, "SessionApp.json");
        byte[] readFile3 = readFile(str, "SessionDevice.json");
        byte[] readFile4 = readFile(str, "SessionOS.json");
        byte[] readFile5 = NativeFileUtils.readFile(new MetaDataStore(getFilesDir()).getUserDataFileForSession(str));
        LogFileManager logFileManager2 = new LogFileManager(this.crashlyticsCore.getContext(), this.logFileDirectoryProvider, str);
        byte[] bytesForLog = logFileManager2.getBytesForLog();
        logFileManager2.clearLog();
        byte[] readFile6 = NativeFileUtils.readFile(new MetaDataStore(getFilesDir()).getKeysFileForSession(str));
        File file2 = new File(this.fileStore.getFilesDir(), str);
        if (!file2.mkdir()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Couldn't create native sessions directory");
            return;
        }
        gzipIfNotEmpty(minidumpFromDirectory, new File(file2, "minidump"));
        gzipIfNotEmpty(metadataJsonFromDirectory, new File(file2, "metadata"));
        gzipIfNotEmpty(binaryImagesJsonFromDirectory, new File(file2, "binaryImages"));
        gzipIfNotEmpty(readFile, new File(file2, SettingsJsonConstants.SESSION_KEY));
        gzipIfNotEmpty(readFile2, new File(file2, SettingsJsonConstants.APP_KEY));
        gzipIfNotEmpty(readFile3, new File(file2, "device"));
        gzipIfNotEmpty(readFile4, new File(file2, "os"));
        gzipIfNotEmpty(readFile5, new File(file2, "user"));
        gzipIfNotEmpty(bytesForLog, new File(file2, "logs"));
        gzipIfNotEmpty(readFile6, new File(file2, "keys"));
    }

    private boolean firebaseCrashExists() {
        try {
            Class.forName("com.google.firebase.crash.FirebaseCrash");
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    private CreateReportSpiCall getCreateReportSpiCall(String str, String str2) {
        String stringsFileValue = CommonUtils.getStringsFileValue(this.crashlyticsCore.getContext(), CRASHLYTICS_API_ENDPOINT);
        return new CompositeCreateReportSpiCall(new DefaultCreateReportSpiCall(this.crashlyticsCore, stringsFileValue, str, this.httpRequestFactory), new NativeCreateReportSpiCall(this.crashlyticsCore, stringsFileValue, str2, this.httpRequestFactory));
    }

    /* access modifiers changed from: private */
    public String getCurrentSessionId() {
        File[] listSortedSessionBeginFiles = listSortedSessionBeginFiles();
        if (listSortedSessionBeginFiles.length > 0) {
            return getSessionIdFromSessionFile(listSortedSessionBeginFiles[0]);
        }
        return null;
    }

    /* access modifiers changed from: private */
    public String getPreviousSessionId() {
        File[] listSortedSessionBeginFiles = listSortedSessionBeginFiles();
        if (listSortedSessionBeginFiles.length > 1) {
            return getSessionIdFromSessionFile(listSortedSessionBeginFiles[1]);
        }
        return null;
    }

    static String getSessionIdFromSessionFile(File file) {
        return file.getName().substring(0, 35);
    }

    private File[] getTrimmedNonFatalFiles(String str, File[] fileArr, int i) {
        if (fileArr.length <= i) {
            return fileArr;
        }
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, String.format(Locale.US, "Trimming down to %d logged exceptions.", new Object[]{Integer.valueOf(i)}));
        trimSessionEventFiles(str, i);
        return listFilesMatching((FilenameFilter) new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG));
    }

    private UserMetaData getUserMetaData(String str) {
        return isHandlingException() ? new UserMetaData(this.crashlyticsCore.getUserIdentifier(), this.crashlyticsCore.getUserName(), this.crashlyticsCore.getUserEmail()) : new MetaDataStore(getFilesDir()).readUserData(str);
    }

    private void gzip(byte[] bArr, File file) throws IOException {
        GZIPOutputStream gZIPOutputStream;
        try {
            gZIPOutputStream = new GZIPOutputStream(new FileOutputStream(file));
            try {
                gZIPOutputStream.write(bArr);
                gZIPOutputStream.finish();
                CommonUtils.closeQuietly(gZIPOutputStream);
            } catch (Throwable th) {
                th = th;
                CommonUtils.closeQuietly(gZIPOutputStream);
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            gZIPOutputStream = null;
            CommonUtils.closeQuietly(gZIPOutputStream);
            throw th;
        }
    }

    private void gzipIfNotEmpty(byte[] bArr, File file) throws IOException {
        if (bArr != null && bArr.length > 0) {
            gzip(bArr, file);
        }
    }

    private File[] listFiles(File file) {
        return ensureFileArrayNotNull(file.listFiles());
    }

    private File[] listFilesMatching(File file, FilenameFilter filenameFilter) {
        return ensureFileArrayNotNull(file.listFiles(filenameFilter));
    }

    private File[] listFilesMatching(FileFilter fileFilter) {
        return ensureFileArrayNotNull(getFilesDir().listFiles(fileFilter));
    }

    /* access modifiers changed from: private */
    public File[] listFilesMatching(FilenameFilter filenameFilter) {
        return listFilesMatching(getFilesDir(), filenameFilter);
    }

    private File[] listSessionPartFilesFor(String str) {
        return listFilesMatching((FilenameFilter) new SessionPartFileFilter(str));
    }

    private File[] listSortedSessionBeginFiles() {
        File[] listSessionBeginFiles = listSessionBeginFiles();
        Arrays.sort(listSessionBeginFiles, LARGEST_FILE_NAME_FIRST);
        return listSessionBeginFiles;
    }

    private byte[] readFile(String str, String str2) {
        return NativeFileUtils.readFile(new File(getFilesDir(), str + str2));
    }

    private static void recordFatalExceptionAnswersEvent(String str, String str2) {
        Answers answers = (Answers) Fabric.getKit(Answers.class);
        if (answers == null) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Answers is not available");
        } else {
            answers.onException(new FatalException(str, str2));
        }
    }

    /* access modifiers changed from: private */
    public void recordFatalFirebaseEvent(long j) {
        if (firebaseCrashExists()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Skipping logging Crashlytics event to Firebase, FirebaseCrash exists");
        } else if (this.firebaseAnalyticsLogger != null) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Logging Crashlytics event to Firebase");
            Bundle bundle = new Bundle();
            bundle.putInt(FIREBASE_REALTIME, 1);
            bundle.putInt("fatal", 1);
            bundle.putLong("timestamp", j);
            this.firebaseAnalyticsLogger.logEvent(FIREBASE_ANALYTICS_ORIGIN_CRASHLYTICS, "_ae", bundle);
        } else {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Skipping logging Crashlytics event to Firebase, no Firebase Analytics");
        }
    }

    private static void recordLoggedExceptionAnswersEvent(String str, String str2) {
        Answers answers = (Answers) Fabric.getKit(Answers.class);
        if (answers == null) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Answers is not available");
        } else {
            answers.onException(new LoggedException(str, str2));
        }
    }

    private void recursiveDelete(File file) {
        if (file.isDirectory()) {
            for (File recursiveDelete : file.listFiles()) {
                recursiveDelete(recursiveDelete);
            }
        }
        file.delete();
    }

    /* access modifiers changed from: private */
    public void recursiveDelete(Set<File> set) {
        for (File recursiveDelete : set) {
            recursiveDelete(recursiveDelete);
        }
    }

    private void retainSessions(File[] fileArr, Set<String> set) {
        for (File file : fileArr) {
            String name = file.getName();
            Matcher matcher = SESSION_FILE_PATTERN.matcher(name);
            if (!matcher.matches()) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Deleting unknown file: " + name);
                file.delete();
            } else if (!set.contains(matcher.group(1))) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Trimming session file: " + name);
                file.delete();
            }
        }
    }

    /* access modifiers changed from: private */
    public void sendSessionReports(SettingsData settingsData) {
        if (settingsData == null) {
            Fabric.getLogger().mo20982w(CrashlyticsCore.TAG, "Cannot send reports. Settings are unavailable.");
            return;
        }
        Context context = this.crashlyticsCore.getContext();
        ReportUploader reportUploader = new ReportUploader(this.appData.apiKey, getCreateReportSpiCall(settingsData.appData.reportsUrl, settingsData.appData.ndkReportsUrl), this.reportFilesProvider, this.handlingExceptionCheck);
        for (File sessionReport : listCompleteSessionFiles()) {
            this.backgroundWorker.submit((Runnable) new SendReportRunnable(context, new SessionReport(sessionReport, SEND_AT_CRASHTIME_HEADER), reportUploader));
        }
    }

    /* access modifiers changed from: private */
    public boolean shouldPromptUserBeforeSendingCrashReports(SettingsData settingsData) {
        return settingsData != null && settingsData.featuresData.promptEnabled && !this.preferenceManager.shouldAlwaysSendReports();
    }

    /* JADX WARNING: type inference failed for: r2v0 */
    /* JADX WARNING: type inference failed for: r3v1, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r0v4, types: [java.io.Flushable] */
    /* JADX WARNING: type inference failed for: r3v2 */
    /* JADX WARNING: type inference failed for: r0v6 */
    /* JADX WARNING: type inference failed for: r3v3 */
    /* JADX WARNING: type inference failed for: r0v7 */
    /* JADX WARNING: type inference failed for: r3v4, types: [com.crashlytics.android.core.ClsFileOutputStream] */
    /* JADX WARNING: type inference failed for: r1v4, types: [java.io.Flushable] */
    /* JADX WARNING: type inference failed for: r0v9 */
    /* JADX WARNING: type inference failed for: r1v5 */
    /* JADX WARNING: type inference failed for: r3v5 */
    /* JADX WARNING: type inference failed for: r3v6, types: [java.io.OutputStream, com.crashlytics.android.core.ClsFileOutputStream, java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r0v12 */
    /* JADX WARNING: type inference failed for: r1v7 */
    /* JADX WARNING: type inference failed for: r2v5, types: [com.crashlytics.android.core.CodedOutputStream, java.io.Flushable] */
    /* JADX WARNING: type inference failed for: r0v14 */
    /* JADX WARNING: type inference failed for: r1v9 */
    /* JADX WARNING: type inference failed for: r3v7 */
    /* JADX WARNING: type inference failed for: r0v24 */
    /* JADX WARNING: type inference failed for: r3v8 */
    /* JADX WARNING: type inference failed for: r3v9 */
    /* JADX WARNING: type inference failed for: r3v10 */
    /* JADX WARNING: type inference failed for: r3v11 */
    /* JADX WARNING: type inference failed for: r3v12 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r3v2
      assigns: []
      uses: []
      mth insns count: 78
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 9 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void synthesizeSessionFile(java.io.File r11, java.lang.String r12, java.io.File[] r13, java.io.File r14) {
        /*
            r10 = this;
            r0 = 1
            r2 = 0
            if (r14 == 0) goto L_0x0072
            r1 = r0
        L_0x0005:
            if (r1 == 0) goto L_0x0075
            java.io.File r0 = r10.getFatalSessionFilesDir()
        L_0x000b:
            boolean r3 = r0.exists()
            if (r3 != 0) goto L_0x0014
            r0.mkdirs()
        L_0x0014:
            com.crashlytics.android.core.ClsFileOutputStream r3 = new com.crashlytics.android.core.ClsFileOutputStream     // Catch:{ Exception -> 0x007a, all -> 0x00a2 }
            r3.<init>(r0, r12)     // Catch:{ Exception -> 0x007a, all -> 0x00a2 }
            com.crashlytics.android.core.CodedOutputStream r2 = com.crashlytics.android.core.CodedOutputStream.newInstance(r3)     // Catch:{ Exception -> 0x00b1, all -> 0x00b7 }
            io.fabric.sdk.android.Logger r0 = p017io.fabric.sdk.android.Fabric.getLogger()     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r4.<init>()     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            java.lang.String r5 = "CrashlyticsCore"
            java.lang.String r6 = "Collecting SessionStart data for session ID "
            java.lang.StringBuilder r4 = r4.append(r6)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            java.lang.StringBuilder r4 = r4.append(r12)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            java.lang.String r4 = r4.toString()     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r0.mo20969d(r5, r4)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            writeToCosFromFile(r2, r11)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            java.util.Date r0 = new java.util.Date     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r0.<init>()     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r4 = 4
            long r6 = r0.getTime()     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r8 = 1000(0x3e8, double:4.94E-321)
            long r6 = r6 / r8
            r2.writeUInt64(r4, r6)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r0 = 5
            r2.writeBool(r0, r1)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r0 = 11
            r4 = 1
            r2.writeUInt32(r0, r4)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r0 = 12
            r4 = 3
            r2.writeEnum(r0, r4)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            r10.writeInitialPartsTo(r2, r12)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            writeNonFatalEventsTo(r2, r13, r12)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
            if (r1 == 0) goto L_0x0067
            writeToCosFromFile(r2, r14)     // Catch:{ Exception -> 0x00b4, all -> 0x00ba }
        L_0x0067:
            java.lang.String r0 = "Error flushing session file stream"
            p017io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r2, r0)
            java.lang.String r0 = "Failed to close CLS file"
            p017io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r3, r0)
        L_0x0071:
            return
        L_0x0072:
            r0 = 0
            r1 = r0
            goto L_0x0005
        L_0x0075:
            java.io.File r0 = r10.getNonFatalSessionFilesDir()
            goto L_0x000b
        L_0x007a:
            r0 = move-exception
            r1 = r2
            r3 = r2
        L_0x007d:
            io.fabric.sdk.android.Logger r2 = p017io.fabric.sdk.android.Fabric.getLogger()     // Catch:{ all -> 0x00bd }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ all -> 0x00bd }
            r4.<init>()     // Catch:{ all -> 0x00bd }
            java.lang.String r5 = "CrashlyticsCore"
            java.lang.String r6 = "Failed to write session file for session ID: "
            java.lang.StringBuilder r4 = r4.append(r6)     // Catch:{ all -> 0x00bd }
            java.lang.StringBuilder r4 = r4.append(r12)     // Catch:{ all -> 0x00bd }
            java.lang.String r4 = r4.toString()     // Catch:{ all -> 0x00bd }
            r2.mo20972e(r5, r4, r0)     // Catch:{ all -> 0x00bd }
            java.lang.String r0 = "Error flushing session file stream"
            p017io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r1, r0)
            r10.closeWithoutRenamingOrLog(r3)
            goto L_0x0071
        L_0x00a2:
            r4 = move-exception
            r3 = r2
            r0 = r2
        L_0x00a5:
            r1 = r4
        L_0x00a6:
            java.lang.String r2 = "Error flushing session file stream"
            p017io.fabric.sdk.android.services.common.CommonUtils.flushOrLog(r0, r2)
            java.lang.String r0 = "Failed to close CLS file"
            p017io.fabric.sdk.android.services.common.CommonUtils.closeOrLog(r3, r0)
            throw r1
        L_0x00b1:
            r0 = move-exception
            r1 = r2
            goto L_0x007d
        L_0x00b4:
            r0 = move-exception
            r1 = r2
            goto L_0x007d
        L_0x00b7:
            r1 = move-exception
            r0 = r2
            goto L_0x00a6
        L_0x00ba:
            r1 = move-exception
            r0 = r2
            goto L_0x00a6
        L_0x00bd:
            r4 = move-exception
            r0 = r1
            goto L_0x00a5
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsController.synthesizeSessionFile(java.io.File, java.lang.String, java.io.File[], java.io.File):void");
    }

    private void trimInvalidSessionFiles() {
        File invalidFilesDir = getInvalidFilesDir();
        if (invalidFilesDir.exists()) {
            File[] listFilesMatching = listFilesMatching(invalidFilesDir, new InvalidPartFileFilter());
            Arrays.sort(listFilesMatching, Collections.reverseOrder());
            HashSet hashSet = new HashSet();
            for (int i = 0; i < listFilesMatching.length && hashSet.size() < 4; i++) {
                hashSet.add(getSessionIdFromSessionFile(listFilesMatching[i]));
            }
            retainSessions(listFiles(invalidFilesDir), hashSet);
        }
    }

    private void trimOpenSessions(int i) {
        HashSet hashSet = new HashSet();
        File[] listSortedSessionBeginFiles = listSortedSessionBeginFiles();
        int min = Math.min(i, listSortedSessionBeginFiles.length);
        for (int i2 = 0; i2 < min; i2++) {
            hashSet.add(getSessionIdFromSessionFile(listSortedSessionBeginFiles[i2]));
        }
        this.logFileManager.discardOldLogFiles(hashSet);
        retainSessions(listFilesMatching((FilenameFilter) new AnySessionPartFileFilter()), hashSet);
    }

    private void trimSessionEventFiles(String str, int i) {
        Utils.capFileCount(getFilesDir(), new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG), i, SMALLEST_FILE_NAME_FIRST);
    }

    private void writeBeginSession(String str, Date date) throws Exception {
        final String format = String.format(Locale.US, GENERATOR_FORMAT, new Object[]{this.crashlyticsCore.getVersion()});
        final long time = date.getTime() / 1000;
        final String str2 = str;
        writeSessionPartFile(str, SESSION_BEGIN_TAG, new CodedOutputStreamWriteAction() {
            public void writeTo(CodedOutputStream codedOutputStream) throws Exception {
                SessionProtobufHelper.writeBeginSession(codedOutputStream, str2, format, time);
            }
        });
        final String str3 = str;
        writeFile(str, "BeginSession.json", new FileOutputStreamWriteAction() {
            public void writeTo(FileOutputStream fileOutputStream) throws Exception {
                fileOutputStream.write(new JSONObject(new HashMap<String, Object>() {
                    {
                        put("session_id", str3);
                        put("generator", format);
                        put("started_at_seconds", Long.valueOf(time));
                    }
                }).toString().getBytes());
            }
        });
    }

    /* access modifiers changed from: private */
    public void writeFatal(Date date, Thread thread, Throwable th) {
        ClsFileOutputStream clsFileOutputStream;
        Throwable th2;
        CodedOutputStream codedOutputStream;
        CodedOutputStream codedOutputStream2;
        ClsFileOutputStream clsFileOutputStream2;
        CodedOutputStream codedOutputStream3;
        try {
            String currentSessionId = getCurrentSessionId();
            if (currentSessionId == null) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Tried to write a fatal exception while no session was open.", null);
                CommonUtils.flushOrLog(null, "Failed to flush to session begin file.");
                CommonUtils.closeOrLog(null, "Failed to close fatal exception file output stream.");
                return;
            }
            recordFatalExceptionAnswersEvent(currentSessionId, th.getClass().getName());
            clsFileOutputStream = new ClsFileOutputStream(getFilesDir(), currentSessionId + SESSION_FATAL_TAG);
            try {
                codedOutputStream3 = CodedOutputStream.newInstance((OutputStream) clsFileOutputStream);
            } catch (Exception e) {
                e = e;
                codedOutputStream3 = null;
                clsFileOutputStream2 = clsFileOutputStream;
                try {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the fatal exception logger", e);
                    CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to session begin file.");
                    CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close fatal exception file output stream.");
                } catch (Throwable th3) {
                    th = th3;
                    clsFileOutputStream = clsFileOutputStream2;
                    codedOutputStream2 = codedOutputStream3;
                    th2 = th;
                    CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session begin file.");
                    CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close fatal exception file output stream.");
                    throw th2;
                }
            } catch (Throwable th4) {
                th = th4;
                codedOutputStream = null;
                th2 = th;
                CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session begin file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close fatal exception file output stream.");
                throw th2;
            }
            try {
                writeSessionEvent(codedOutputStream3, date, thread, th, "crash", true);
                CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to session begin file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close fatal exception file output stream.");
            } catch (Exception e2) {
                e = e2;
                clsFileOutputStream2 = clsFileOutputStream;
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the fatal exception logger", e);
                CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to session begin file.");
                CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close fatal exception file output stream.");
            } catch (Throwable th5) {
                th = th5;
                codedOutputStream = codedOutputStream3;
                th2 = th;
                CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session begin file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close fatal exception file output stream.");
                throw th2;
            }
        } catch (Exception e3) {
            e = e3;
            codedOutputStream3 = null;
            clsFileOutputStream2 = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An error occurred in the fatal exception logger", e);
            CommonUtils.flushOrLog(codedOutputStream3, "Failed to flush to session begin file.");
            CommonUtils.closeOrLog(clsFileOutputStream2, "Failed to close fatal exception file output stream.");
        } catch (Throwable th6) {
            th = th6;
            clsFileOutputStream = null;
            codedOutputStream2 = null;
            th2 = th;
            CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session begin file.");
            CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close fatal exception file output stream.");
            throw th2;
        }
    }

    private void writeFile(String str, String str2, FileOutputStreamWriteAction fileOutputStreamWriteAction) throws Exception {
        FileOutputStream fileOutputStream;
        try {
            fileOutputStream = new FileOutputStream(new File(getFilesDir(), str + str2));
            try {
                fileOutputStreamWriteAction.writeTo(fileOutputStream);
                CommonUtils.closeOrLog(fileOutputStream, "Failed to close " + str2 + " file.");
            } catch (Throwable th) {
                th = th;
                CommonUtils.closeOrLog(fileOutputStream, "Failed to close " + str2 + " file.");
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            fileOutputStream = null;
            CommonUtils.closeOrLog(fileOutputStream, "Failed to close " + str2 + " file.");
            throw th;
        }
    }

    private void writeInitialPartsTo(CodedOutputStream codedOutputStream, String str) throws IOException {
        String[] strArr;
        for (String str2 : INITIAL_SESSION_PART_TAGS) {
            File[] listFilesMatching = listFilesMatching((FilenameFilter) new FileNameContainsFilter(str + str2 + ClsFileOutputStream.SESSION_FILE_EXTENSION));
            if (listFilesMatching.length == 0) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Can't find " + str2 + " data for session ID " + str, null);
            } else {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Collecting " + str2 + " data for session ID " + str);
                writeToCosFromFile(codedOutputStream, listFilesMatching[0]);
            }
        }
    }

    private static void writeNonFatalEventsTo(CodedOutputStream codedOutputStream, File[] fileArr, String str) {
        Arrays.sort(fileArr, CommonUtils.FILE_MODIFIED_COMPARATOR);
        for (File file : fileArr) {
            try {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, String.format(Locale.US, "Found Non Fatal for session ID %s in %s ", new Object[]{str, file.getName()}));
                writeToCosFromFile(codedOutputStream, file);
            } catch (Exception e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error writting non-fatal to session.", e);
            }
        }
    }

    private void writeSessionApp(String str) throws Exception {
        final String appIdentifier = this.idManager.getAppIdentifier();
        final String str2 = this.appData.versionCode;
        final String str3 = this.appData.versionName;
        final String appInstallIdentifier = this.idManager.getAppInstallIdentifier();
        final int id = DeliveryMechanism.determineFrom(this.appData.installerPackageName).getId();
        writeSessionPartFile(str, SESSION_APP_TAG, new CodedOutputStreamWriteAction() {
            public void writeTo(CodedOutputStream codedOutputStream) throws Exception {
                SessionProtobufHelper.writeSessionApp(codedOutputStream, appIdentifier, CrashlyticsController.this.appData.apiKey, str2, str3, appInstallIdentifier, id, CrashlyticsController.this.unityVersion);
            }
        });
        writeFile(str, "SessionApp.json", new FileOutputStreamWriteAction() {
            public void writeTo(FileOutputStream fileOutputStream) throws Exception {
                fileOutputStream.write(new JSONObject(new HashMap<String, Object>() {
                    {
                        put("app_identifier", appIdentifier);
                        put("api_key", CrashlyticsController.this.appData.apiKey);
                        put("version_code", str2);
                        put("version_name", str3);
                        put("install_uuid", appInstallIdentifier);
                        put("delivery_mechanism", Integer.valueOf(id));
                        put("unity_version", TextUtils.isEmpty(CrashlyticsController.this.unityVersion) ? "" : CrashlyticsController.this.unityVersion);
                    }
                }).toString().getBytes());
            }
        });
    }

    private void writeSessionDevice(String str) throws Exception {
        Context context = this.crashlyticsCore.getContext();
        StatFs statFs = new StatFs(Environment.getDataDirectory().getPath());
        final int cpuArchitectureInt = CommonUtils.getCpuArchitectureInt();
        final int availableProcessors = Runtime.getRuntime().availableProcessors();
        final long totalRamInBytes = CommonUtils.getTotalRamInBytes();
        final long blockCount = ((long) statFs.getBlockCount()) * ((long) statFs.getBlockSize());
        final boolean isEmulator = CommonUtils.isEmulator(context);
        final Map deviceIdentifiers = this.idManager.getDeviceIdentifiers();
        final int deviceState = CommonUtils.getDeviceState(context);
        writeSessionPartFile(str, SESSION_DEVICE_TAG, new CodedOutputStreamWriteAction() {
            public void writeTo(CodedOutputStream codedOutputStream) throws Exception {
                SessionProtobufHelper.writeSessionDevice(codedOutputStream, cpuArchitectureInt, Build.MODEL, availableProcessors, totalRamInBytes, blockCount, isEmulator, deviceIdentifiers, deviceState, Build.MANUFACTURER, Build.PRODUCT);
            }
        });
        writeFile(str, "SessionDevice.json", new FileOutputStreamWriteAction() {
            public void writeTo(FileOutputStream fileOutputStream) throws Exception {
                fileOutputStream.write(new JSONObject(new HashMap<String, Object>() {
                    {
                        put("arch", Integer.valueOf(cpuArchitectureInt));
                        put("build_model", Build.MODEL);
                        put("available_processors", Integer.valueOf(availableProcessors));
                        put("total_ram", Long.valueOf(totalRamInBytes));
                        put("disk_space", Long.valueOf(blockCount));
                        put("is_emulator", Boolean.valueOf(isEmulator));
                        put("ids", deviceIdentifiers);
                        put(ServerProtocol.DIALOG_PARAM_STATE, Integer.valueOf(deviceState));
                        put("build_manufacturer", Build.MANUFACTURER);
                        put("build_product", Build.PRODUCT);
                    }
                }).toString().getBytes());
            }
        });
    }

    private void writeSessionEvent(CodedOutputStream codedOutputStream, Date date, Thread thread, Throwable th, String str, boolean z) throws Exception {
        Thread[] threadArr;
        Map treeMap;
        TrimmedThrowableData trimmedThrowableData = new TrimmedThrowableData(th, this.stackTraceTrimmingStrategy);
        Context context = this.crashlyticsCore.getContext();
        long time = date.getTime() / 1000;
        Float batteryLevel = CommonUtils.getBatteryLevel(context);
        int batteryVelocity = CommonUtils.getBatteryVelocity(context, this.devicePowerStateListener.isPowerConnected());
        boolean proximitySensorEnabled = CommonUtils.getProximitySensorEnabled(context);
        int i = context.getResources().getConfiguration().orientation;
        long totalRamInBytes = CommonUtils.getTotalRamInBytes();
        long calculateFreeRamInBytes = CommonUtils.calculateFreeRamInBytes(context);
        long calculateUsedDiskSpaceInBytes = CommonUtils.calculateUsedDiskSpaceInBytes(Environment.getDataDirectory().getPath());
        RunningAppProcessInfo appProcessInfo = CommonUtils.getAppProcessInfo(context.getPackageName(), context);
        LinkedList linkedList = new LinkedList();
        StackTraceElement[] stackTraceElementArr = trimmedThrowableData.stacktrace;
        String str2 = this.appData.buildId;
        String appIdentifier = this.idManager.getAppIdentifier();
        if (z) {
            Map allStackTraces = Thread.getAllStackTraces();
            threadArr = new Thread[allStackTraces.size()];
            int i2 = 0;
            Iterator it = allStackTraces.entrySet().iterator();
            while (true) {
                int i3 = i2;
                if (!it.hasNext()) {
                    break;
                }
                Entry entry = (Entry) it.next();
                threadArr[i3] = (Thread) entry.getKey();
                linkedList.add(this.stackTraceTrimmingStrategy.getTrimmedStackTrace((StackTraceElement[]) entry.getValue()));
                i2 = i3 + 1;
            }
        } else {
            threadArr = new Thread[0];
        }
        if (!CommonUtils.getBooleanResourceValue(context, COLLECT_CUSTOM_KEYS, true)) {
            treeMap = new TreeMap();
        } else {
            Map attributes = this.crashlyticsCore.getAttributes();
            treeMap = (attributes == null || attributes.size() <= 1) ? attributes : new TreeMap(attributes);
        }
        SessionProtobufHelper.writeSessionEvent(codedOutputStream, time, str, trimmedThrowableData, thread, stackTraceElementArr, threadArr, linkedList, treeMap, this.logFileManager, appProcessInfo, i, appIdentifier, str2, batteryLevel, batteryVelocity, proximitySensorEnabled, totalRamInBytes - calculateFreeRamInBytes, calculateUsedDiskSpaceInBytes);
    }

    private void writeSessionOS(String str) throws Exception {
        final boolean isRooted = CommonUtils.isRooted(this.crashlyticsCore.getContext());
        writeSessionPartFile(str, SESSION_OS_TAG, new CodedOutputStreamWriteAction() {
            public void writeTo(CodedOutputStream codedOutputStream) throws Exception {
                SessionProtobufHelper.writeSessionOS(codedOutputStream, VERSION.RELEASE, VERSION.CODENAME, isRooted);
            }
        });
        writeFile(str, "SessionOS.json", new FileOutputStreamWriteAction() {
            public void writeTo(FileOutputStream fileOutputStream) throws Exception {
                fileOutputStream.write(new JSONObject(new HashMap<String, Object>() {
                    {
                        put(ServerProtocol.FALLBACK_DIALOG_PARAM_VERSION, VERSION.RELEASE);
                        put("build_version", VERSION.CODENAME);
                        put("is_rooted", Boolean.valueOf(isRooted));
                    }
                }).toString().getBytes());
            }
        });
    }

    private void writeSessionPartFile(String str, String str2, CodedOutputStreamWriteAction codedOutputStreamWriteAction) throws Exception {
        CodedOutputStream codedOutputStream;
        ClsFileOutputStream clsFileOutputStream;
        CodedOutputStream codedOutputStream2 = null;
        try {
            clsFileOutputStream = new ClsFileOutputStream(getFilesDir(), str + str2);
            try {
                codedOutputStream2 = CodedOutputStream.newInstance((OutputStream) clsFileOutputStream);
                codedOutputStreamWriteAction.writeTo(codedOutputStream2);
                CommonUtils.flushOrLog(codedOutputStream2, "Failed to flush to session " + str2 + " file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session " + str2 + " file.");
            } catch (Throwable th) {
                th = th;
                codedOutputStream = codedOutputStream2;
                CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session " + str2 + " file.");
                CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session " + str2 + " file.");
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            clsFileOutputStream = null;
            codedOutputStream = null;
            CommonUtils.flushOrLog(codedOutputStream, "Failed to flush to session " + str2 + " file.");
            CommonUtils.closeOrLog(clsFileOutputStream, "Failed to close session " + str2 + " file.");
            throw th;
        }
    }

    private void writeSessionPartsToSessionFile(File file, String str, int i) {
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Collecting session parts for ID " + str);
        File[] listFilesMatching = listFilesMatching((FilenameFilter) new FileNameContainsFilter(str + SESSION_FATAL_TAG));
        boolean z = listFilesMatching != null && listFilesMatching.length > 0;
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, String.format(Locale.US, "Session %s has fatal exception: %s", new Object[]{str, Boolean.valueOf(z)}));
        File[] listFilesMatching2 = listFilesMatching((FilenameFilter) new FileNameContainsFilter(str + SESSION_NON_FATAL_TAG));
        boolean z2 = listFilesMatching2 != null && listFilesMatching2.length > 0;
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, String.format(Locale.US, "Session %s has non-fatal exceptions: %s", new Object[]{str, Boolean.valueOf(z2)}));
        if (z || z2) {
            synthesizeSessionFile(file, str, getTrimmedNonFatalFiles(str, listFilesMatching2, i), z ? listFilesMatching[0] : null);
        } else {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "No events present for session ID " + str);
        }
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Removing session part files for ID " + str);
        deleteSessionPartFilesFor(str);
    }

    private void writeSessionUser(String str) throws Exception {
        String str2 = SESSION_USER_TAG;
        final UserMetaData userMetaData = getUserMetaData(str);
        writeSessionPartFile(str, str2, new CodedOutputStreamWriteAction() {
            public void writeTo(CodedOutputStream codedOutputStream) throws Exception {
                SessionProtobufHelper.writeSessionUser(codedOutputStream, userMetaData.f385id, userMetaData.name, userMetaData.email);
            }
        });
    }

    private static void writeToCosFromFile(CodedOutputStream codedOutputStream, File file) throws IOException {
        FileInputStream fileInputStream;
        if (!file.exists()) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Tried to include a file that doesn't exist: " + file.getName(), null);
            return;
        }
        try {
            fileInputStream = new FileInputStream(file);
            try {
                copyToCodedOutputStream(fileInputStream, codedOutputStream, (int) file.length());
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream.");
            } catch (Throwable th) {
                th = th;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream.");
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream.");
            throw th;
        }
    }

    /* access modifiers changed from: 0000 */
    public void cacheKeyData(final Map<String, String> map) {
        this.backgroundWorker.submit((Callable<T>) new Callable<Void>() {
            public Void call() throws Exception {
                new MetaDataStore(CrashlyticsController.this.getFilesDir()).writeKeyData(CrashlyticsController.this.getCurrentSessionId(), map);
                return null;
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void cacheUserData(final String str, final String str2, final String str3) {
        this.backgroundWorker.submit((Callable<T>) new Callable<Void>() {
            public Void call() throws Exception {
                new MetaDataStore(CrashlyticsController.this.getFilesDir()).writeUserData(CrashlyticsController.this.getCurrentSessionId(), new UserMetaData(str, str2, str3));
                return null;
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void cleanInvalidTempFiles() {
        this.backgroundWorker.submit((Runnable) new Runnable() {
            public void run() {
                CrashlyticsController.this.doCleanInvalidTempFiles(CrashlyticsController.this.listFilesMatching((FilenameFilter) new InvalidPartFileFilter()));
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void doCleanInvalidTempFiles(File[] fileArr) {
        File[] listFilesMatching;
        final HashSet hashSet = new HashSet();
        for (File file : fileArr) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Found invalid session part file: " + file);
            hashSet.add(getSessionIdFromSessionFile(file));
        }
        if (!hashSet.isEmpty()) {
            File invalidFilesDir = getInvalidFilesDir();
            if (!invalidFilesDir.exists()) {
                invalidFilesDir.mkdir();
            }
            for (File file2 : listFilesMatching((FilenameFilter) new FilenameFilter() {
                public boolean accept(File file, String str) {
                    if (str.length() < 35) {
                        return false;
                    }
                    return hashSet.contains(str.substring(0, 35));
                }
            })) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Moving session file: " + file2);
                if (!file2.renameTo(new File(invalidFilesDir, file2.getName()))) {
                    Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Could not move session file. Deleting " + file2);
                    file2.delete();
                }
            }
            trimInvalidSessionFiles();
        }
    }

    /* access modifiers changed from: 0000 */
    public void doCloseSessions(SessionSettingsData sessionSettingsData) throws Exception {
        doCloseSessions(sessionSettingsData, false);
    }

    /* access modifiers changed from: 0000 */
    public void enableExceptionHandling(UncaughtExceptionHandler uncaughtExceptionHandler, boolean z) {
        openSession();
        this.crashHandler = new CrashlyticsUncaughtExceptionHandler(new CrashListener() {
            public void onUncaughtException(SettingsDataProvider settingsDataProvider, Thread thread, Throwable th, boolean z) {
                CrashlyticsController.this.handleUncaughtException(settingsDataProvider, thread, th, z);
            }
        }, new DefaultSettingsDataProvider(), z, uncaughtExceptionHandler);
        Thread.setDefaultUncaughtExceptionHandler(this.crashHandler);
    }

    /* access modifiers changed from: 0000 */
    public boolean finalizeNativeReport(final CrashlyticsNdkData crashlyticsNdkData) {
        if (crashlyticsNdkData == null) {
            return true;
        }
        return ((Boolean) this.backgroundWorker.submitAndWait(new Callable<Boolean>() {
            public Boolean call() throws Exception {
                TreeSet<File> treeSet = crashlyticsNdkData.timestampedDirectories;
                String access$1600 = CrashlyticsController.this.getPreviousSessionId();
                if (access$1600 != null && !treeSet.isEmpty()) {
                    File file = (File) treeSet.first();
                    if (file != null) {
                        CrashlyticsController.this.finalizeMostRecentNativeCrash(CrashlyticsController.this.crashlyticsCore.getContext(), file, access$1600);
                    }
                }
                CrashlyticsController.this.recursiveDelete((Set<File>) treeSet);
                return Boolean.TRUE;
            }
        })).booleanValue();
    }

    /* access modifiers changed from: 0000 */
    public boolean finalizeSessions(final SessionSettingsData sessionSettingsData) {
        return ((Boolean) this.backgroundWorker.submitAndWait(new Callable<Boolean>() {
            public Boolean call() throws Exception {
                if (CrashlyticsController.this.isHandlingException()) {
                    Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Skipping session finalization because a crash has already occurred.");
                    return Boolean.FALSE;
                }
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Finalizing previously open sessions.");
                CrashlyticsController.this.doCloseSessions(sessionSettingsData, true);
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Closed all previously open sessions");
                return Boolean.TRUE;
            }
        })).booleanValue();
    }

    /* access modifiers changed from: 0000 */
    public File getFatalSessionFilesDir() {
        return new File(getFilesDir(), FATAL_SESSION_DIR);
    }

    /* access modifiers changed from: 0000 */
    public File getFilesDir() {
        return this.fileStore.getFilesDir();
    }

    /* access modifiers changed from: 0000 */
    public File getInvalidFilesDir() {
        return new File(getFilesDir(), INVALID_CLS_CACHE_DIR);
    }

    /* access modifiers changed from: 0000 */
    public File getNonFatalSessionFilesDir() {
        return new File(getFilesDir(), NONFATAL_SESSION_DIR);
    }

    /* access modifiers changed from: 0000 */
    public void handleUncaughtException(SettingsDataProvider settingsDataProvider, Thread thread, Throwable th, boolean z) {
        synchronized (this) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Crashlytics is handling uncaught exception \"" + th + "\" from thread " + thread.getName());
            this.devicePowerStateListener.dispose();
            final Date date = new Date();
            final Thread thread2 = thread;
            final Throwable th2 = th;
            final SettingsDataProvider settingsDataProvider2 = settingsDataProvider;
            final boolean z2 = z;
            this.backgroundWorker.submitAndWait(new Callable<Void>() {
                public Void call() throws Exception {
                    FeaturesSettingsData featuresSettingsData;
                    SessionSettingsData sessionSettingsData;
                    boolean z = true;
                    CrashlyticsController.this.crashlyticsCore.createCrashMarker();
                    CrashlyticsController.this.writeFatal(date, thread2, th2);
                    SettingsData settingsData = settingsDataProvider2.getSettingsData();
                    if (settingsData != null) {
                        sessionSettingsData = settingsData.sessionData;
                        featuresSettingsData = settingsData.featuresData;
                    } else {
                        featuresSettingsData = null;
                        sessionSettingsData = null;
                    }
                    if ((featuresSettingsData == null || featuresSettingsData.firebaseCrashlyticsEnabled) || z2) {
                        CrashlyticsController.this.recordFatalFirebaseEvent(date.getTime());
                    }
                    CrashlyticsController.this.doCloseSessions(sessionSettingsData);
                    CrashlyticsController.this.doOpenSession();
                    if (sessionSettingsData != null) {
                        CrashlyticsController.this.trimSessionFiles(sessionSettingsData.maxCompleteSessionsCount);
                    }
                    if (!DataCollectionArbiter.getInstance(CrashlyticsController.this.crashlyticsCore.getContext()).isDataCollectionEnabled() || CrashlyticsController.this.shouldPromptUserBeforeSendingCrashReports(settingsData)) {
                        z = false;
                    }
                    if (z) {
                        CrashlyticsController.this.sendSessionReports(settingsData);
                    }
                    return null;
                }
            });
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean hasOpenSession() {
        return listSessionBeginFiles().length > 0;
    }

    /* access modifiers changed from: 0000 */
    public boolean isHandlingException() {
        return this.crashHandler != null && this.crashHandler.isHandlingException();
    }

    /* access modifiers changed from: 0000 */
    public File[] listCompleteSessionFiles() {
        LinkedList linkedList = new LinkedList();
        Collections.addAll(linkedList, listFilesMatching(getFatalSessionFilesDir(), SESSION_FILE_FILTER));
        Collections.addAll(linkedList, listFilesMatching(getNonFatalSessionFilesDir(), SESSION_FILE_FILTER));
        Collections.addAll(linkedList, listFilesMatching(getFilesDir(), SESSION_FILE_FILTER));
        return (File[]) linkedList.toArray(new File[linkedList.size()]);
    }

    /* access modifiers changed from: 0000 */
    public File[] listNativeSessionFileDirectories() {
        return listFilesMatching(SESSION_DIRECTORY_FILTER);
    }

    /* access modifiers changed from: 0000 */
    public File[] listSessionBeginFiles() {
        return listFilesMatching(SESSION_BEGIN_FILE_FILTER);
    }

    /* access modifiers changed from: 0000 */
    public void openSession() {
        this.backgroundWorker.submit((Callable<T>) new Callable<Void>() {
            public Void call() throws Exception {
                CrashlyticsController.this.doOpenSession();
                return null;
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void registerAnalyticsEventListener(SettingsData settingsData) {
        if (settingsData.featuresData.firebaseCrashlyticsEnabled) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Registered Firebase Analytics event listener for breadcrumbs: " + this.appMeasurementEventListenerRegistrar.register());
        }
    }

    /* access modifiers changed from: 0000 */
    public void registerDevicePowerStateListener() {
        this.devicePowerStateListener.initialize();
    }

    /* access modifiers changed from: 0000 */
    public void submitAllReports(float f, SettingsData settingsData) {
        if (settingsData == null) {
            Fabric.getLogger().mo20982w(CrashlyticsCore.TAG, "Could not send reports. Settings are not available.");
            return;
        }
        new ReportUploader(this.appData.apiKey, getCreateReportSpiCall(settingsData.appData.reportsUrl, settingsData.appData.ndkReportsUrl), this.reportFilesProvider, this.handlingExceptionCheck).uploadReports(f, shouldPromptUserBeforeSendingCrashReports(settingsData) ? new PrivacyDialogCheck(this.crashlyticsCore, this.preferenceManager, settingsData.promptData) : new AlwaysSendCheck());
    }

    /* access modifiers changed from: 0000 */
    public void trimSessionFiles(int i) {
        int capFileCount = i - Utils.capFileCount(getFatalSessionFilesDir(), i, SMALLEST_FILE_NAME_FIRST);
        Utils.capFileCount(getFilesDir(), SESSION_FILE_FILTER, capFileCount - Utils.capFileCount(getNonFatalSessionFilesDir(), capFileCount, SMALLEST_FILE_NAME_FIRST), SMALLEST_FILE_NAME_FIRST);
    }

    /* access modifiers changed from: 0000 */
    public void writeNonFatalException(final Thread thread, final Throwable th) {
        final Date date = new Date();
        this.backgroundWorker.submit((Runnable) new Runnable() {
            public void run() {
                if (!CrashlyticsController.this.isHandlingException()) {
                    CrashlyticsController.this.doWriteNonFatal(date, thread, th);
                }
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void writeToLog(final long j, final String str) {
        this.backgroundWorker.submit((Callable<T>) new Callable<Void>() {
            public Void call() throws Exception {
                if (!CrashlyticsController.this.isHandlingException()) {
                    CrashlyticsController.this.logFileManager.writeToLog(j, str);
                }
                return null;
            }
        });
    }
}

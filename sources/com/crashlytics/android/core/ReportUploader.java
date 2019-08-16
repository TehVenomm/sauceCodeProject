package com.crashlytics.android.core;

import com.facebook.appevents.AppEventsConstants;
import java.io.File;
import java.util.Collections;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.BackgroundPriorityRunnable;

class ReportUploader {
    static final Map<String, String> HEADER_INVALID_CLS_FILE = Collections.singletonMap("X-CRASHLYTICS-INVALID-SESSION", AppEventsConstants.EVENT_PARAM_VALUE_YES);
    /* access modifiers changed from: private */
    public static final short[] RETRY_INTERVALS = {10, 20, 30, 60, 120, 300};
    private final String apiKey;
    private final CreateReportSpiCall createReportCall;
    private final Object fileAccessLock = new Object();
    /* access modifiers changed from: private */
    public final HandlingExceptionCheck handlingExceptionCheck;
    private final ReportFilesProvider reportFilesProvider;
    /* access modifiers changed from: private */
    public Thread uploadThread;

    static final class AlwaysSendCheck implements SendCheck {
        AlwaysSendCheck() {
        }

        public boolean canSendReports() {
            return true;
        }
    }

    interface HandlingExceptionCheck {
        boolean isHandlingException();
    }

    interface ReportFilesProvider {
        File[] getCompleteSessionFiles();

        File[] getInvalidSessionFiles();

        File[] getNativeReportFiles();
    }

    interface SendCheck {
        boolean canSendReports();
    }

    private class Worker extends BackgroundPriorityRunnable {
        private final float delay;
        private final SendCheck sendCheck;

        Worker(float f, SendCheck sendCheck2) {
            this.delay = f;
            this.sendCheck = sendCheck2;
        }

        private void attemptUploadWithRetry() {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Starting report processing in " + this.delay + " second(s)...");
            if (this.delay > 0.0f) {
                try {
                    Thread.sleep((long) (this.delay * 1000.0f));
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return;
                }
            }
            List<Report> findReports = ReportUploader.this.findReports();
            if (!ReportUploader.this.handlingExceptionCheck.isHandlingException()) {
                if (findReports.isEmpty() || this.sendCheck.canSendReports()) {
                    int i = 0;
                    while (!findReports.isEmpty() && !ReportUploader.this.handlingExceptionCheck.isHandlingException()) {
                        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Attempting to send " + findReports.size() + " report(s)");
                        for (Report forceUpload : findReports) {
                            ReportUploader.this.forceUpload(forceUpload);
                        }
                        findReports = ReportUploader.this.findReports();
                        if (!findReports.isEmpty()) {
                            long j = (long) ReportUploader.RETRY_INTERVALS[Math.min(i, ReportUploader.RETRY_INTERVALS.length - 1)];
                            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Report submisson: scheduling delayed retry in " + j + " seconds");
                            try {
                                Thread.sleep(j * 1000);
                                i++;
                            } catch (InterruptedException e2) {
                                Thread.currentThread().interrupt();
                                return;
                            }
                        }
                    }
                    return;
                }
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "User declined to send. Removing " + findReports.size() + " Report(s).");
                for (Report remove : findReports) {
                    remove.remove();
                }
            }
        }

        public void onRun() {
            try {
                attemptUploadWithRetry();
            } catch (Exception e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "An unexpected error occurred while attempting to upload crash reports.", e);
            }
            ReportUploader.this.uploadThread = null;
        }
    }

    public ReportUploader(String str, CreateReportSpiCall createReportSpiCall, ReportFilesProvider reportFilesProvider2, HandlingExceptionCheck handlingExceptionCheck2) {
        if (createReportSpiCall == null) {
            throw new IllegalArgumentException("createReportCall must not be null.");
        }
        this.createReportCall = createReportSpiCall;
        this.apiKey = str;
        this.reportFilesProvider = reportFilesProvider2;
        this.handlingExceptionCheck = handlingExceptionCheck2;
    }

    /* access modifiers changed from: 0000 */
    public List<Report> findReports() {
        File[] completeSessionFiles;
        File[] invalidSessionFiles;
        File[] nativeReportFiles;
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Checking for crash reports...");
        synchronized (this.fileAccessLock) {
            completeSessionFiles = this.reportFilesProvider.getCompleteSessionFiles();
            invalidSessionFiles = this.reportFilesProvider.getInvalidSessionFiles();
            nativeReportFiles = this.reportFilesProvider.getNativeReportFiles();
        }
        LinkedList linkedList = new LinkedList();
        if (completeSessionFiles != null) {
            for (File file : completeSessionFiles) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Found crash report " + file.getPath());
                linkedList.add(new SessionReport(file));
            }
        }
        HashMap hashMap = new HashMap();
        if (invalidSessionFiles != null) {
            for (File file2 : invalidSessionFiles) {
                String sessionIdFromSessionFile = CrashlyticsController.getSessionIdFromSessionFile(file2);
                if (!hashMap.containsKey(sessionIdFromSessionFile)) {
                    hashMap.put(sessionIdFromSessionFile, new LinkedList());
                }
                ((List) hashMap.get(sessionIdFromSessionFile)).add(file2);
            }
        }
        for (String str : hashMap.keySet()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Found invalid session: " + str);
            List list = (List) hashMap.get(str);
            linkedList.add(new InvalidSessionReport(str, (File[]) list.toArray(new File[list.size()])));
        }
        if (nativeReportFiles != null) {
            for (File nativeSessionReport : nativeReportFiles) {
                linkedList.add(new NativeSessionReport(nativeSessionReport));
            }
        }
        if (linkedList.isEmpty()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "No reports found.");
        }
        return linkedList;
    }

    /* access modifiers changed from: 0000 */
    public boolean forceUpload(Report report) {
        boolean z;
        synchronized (this.fileAccessLock) {
            try {
                boolean invoke = this.createReportCall.invoke(new CreateReportRequest(this.apiKey, report));
                Fabric.getLogger().mo20974i(CrashlyticsCore.TAG, "Crashlytics report upload " + (invoke ? "complete: " : "FAILED: ") + report.getIdentifier());
                if (invoke) {
                    report.remove();
                    z = true;
                } else {
                    z = false;
                }
            } catch (Exception e) {
                Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error occurred sending report " + report, e);
                z = false;
            }
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    public boolean isUploading() {
        return this.uploadThread != null;
    }

    public void uploadReports(float f, SendCheck sendCheck) {
        synchronized (this) {
            if (this.uploadThread != null) {
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Report upload has already been started.");
            } else {
                this.uploadThread = new Thread(new Worker(f, sendCheck), "Crashlytics Report Uploader");
                this.uploadThread.start();
            }
        }
    }
}

package com.crashlytics.android.core;

import com.facebook.appevents.AppEventsConstants;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.ApiKey;
import io.fabric.sdk.android.services.common.BackgroundPriorityRunnable;
import java.io.File;
import java.io.FilenameFilter;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

class ReportUploader {
    private static final String CLS_FILE_EXT = ".cls";
    static final Map<String, String> HEADER_INVALID_CLS_FILE = Collections.singletonMap("X-CRASHLYTICS-INVALID-SESSION", AppEventsConstants.EVENT_PARAM_VALUE_YES);
    private static final short[] RETRY_INTERVALS = new short[]{(short) 10, (short) 20, (short) 30, (short) 60, (short) 120, (short) 300};
    private static final FilenameFilter crashFileFilter = new C03331();
    private final CreateReportSpiCall createReportCall;
    private final Object fileAccessLock = new Object();
    private Thread uploadThread;

    /* renamed from: com.crashlytics.android.core.ReportUploader$1 */
    static final class C03331 implements FilenameFilter {
        C03331() {
        }

        public boolean accept(File file, String str) {
            return str.endsWith(".cls") && !str.contains("Session");
        }
    }

    private class Worker extends BackgroundPriorityRunnable {
        private final float delay;

        Worker(float f) {
            this.delay = f;
        }

        private void attemptUploadWithRetry() {
            Fabric.getLogger().mo4289d("Fabric", "Starting report processing in " + this.delay + " second(s)...");
            if (this.delay > 0.0f) {
                try {
                    Thread.sleep((long) (this.delay * 1000.0f));
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return;
                }
            }
            CrashlyticsCore instance = CrashlyticsCore.getInstance();
            CrashlyticsUncaughtExceptionHandler handler = instance.getHandler();
            List<Report> findReports = ReportUploader.this.findReports();
            if (!handler.isHandlingException()) {
                if (findReports.isEmpty() || instance.canSendWithUserApproval()) {
                    int i = 0;
                    while (!findReports.isEmpty() && !CrashlyticsCore.getInstance().getHandler().isHandlingException()) {
                        Fabric.getLogger().mo4289d("Fabric", "Attempting to send " + findReports.size() + " report(s)");
                        for (Report forceUpload : findReports) {
                            ReportUploader.this.forceUpload(forceUpload);
                        }
                        List findReports2 = ReportUploader.this.findReports();
                        if (!findReports2.isEmpty()) {
                            long j = (long) ReportUploader.RETRY_INTERVALS[Math.min(i, ReportUploader.RETRY_INTERVALS.length - 1)];
                            Fabric.getLogger().mo4289d("Fabric", "Report submisson: scheduling delayed retry in " + j + " seconds");
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
                Fabric.getLogger().mo4289d("Fabric", "User declined to send. Removing " + findReports.size() + " Report(s).");
                for (Report forceUpload2 : findReports) {
                    forceUpload2.remove();
                }
            }
        }

        public void onRun() {
            try {
                attemptUploadWithRetry();
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "An unexpected error occurred while attempting to upload crash reports.", e);
            }
            ReportUploader.this.uploadThread = null;
        }
    }

    public ReportUploader(CreateReportSpiCall createReportSpiCall) {
        if (createReportSpiCall == null) {
            throw new IllegalArgumentException("createReportCall must not be null.");
        }
        this.createReportCall = createReportSpiCall;
    }

    List<Report> findReports() {
        Fabric.getLogger().mo4289d("Fabric", "Checking for crash reports...");
        synchronized (this.fileAccessLock) {
            File[] listFiles = CrashlyticsCore.getInstance().getSdkDirectory().listFiles(crashFileFilter);
        }
        List<Report> linkedList = new LinkedList();
        for (File file : listFiles) {
            Fabric.getLogger().mo4289d("Fabric", "Found crash report " + file.getPath());
            linkedList.add(new SessionReport(file));
        }
        if (linkedList.isEmpty()) {
            Fabric.getLogger().mo4289d("Fabric", "No reports found.");
        }
        return linkedList;
    }

    boolean forceUpload(Report report) {
        boolean z;
        synchronized (this.fileAccessLock) {
            try {
                boolean invoke = this.createReportCall.invoke(new CreateReportRequest(new ApiKey().getValue(CrashlyticsCore.getInstance().getContext()), report));
                Fabric.getLogger().mo4294i("Fabric", "Crashlytics report upload " + (invoke ? "complete: " : "FAILED: ") + report.getFileName());
                if (invoke) {
                    report.remove();
                    z = true;
                } else {
                    z = false;
                }
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Error occurred sending report " + report, e);
                z = false;
            }
        }
        return z;
    }

    boolean isUploading() {
        return this.uploadThread != null;
    }

    public void uploadReports() {
        uploadReports(0.0f);
    }

    public void uploadReports(float f) {
        synchronized (this) {
            if (this.uploadThread == null) {
                this.uploadThread = new Thread(new Worker(f), "Crashlytics Report Uploader");
                this.uploadThread.start();
            }
        }
    }
}

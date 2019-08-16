package p017io.fabric.sdk.android;

import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import p017io.fabric.sdk.android.services.common.TimingMetric;
import p017io.fabric.sdk.android.services.concurrency.Priority;
import p017io.fabric.sdk.android.services.concurrency.PriorityAsyncTask;
import p017io.fabric.sdk.android.services.concurrency.UnmetDependencyException;

/* renamed from: io.fabric.sdk.android.InitializationTask */
class InitializationTask<Result> extends PriorityAsyncTask<Void, Void, Result> {
    private static final String TIMING_METRIC_TAG = "KitInitialization";
    final Kit<Result> kit;

    public InitializationTask(Kit<Result> kit2) {
        this.kit = kit2;
    }

    private TimingMetric createAndStartTimingMetric(String str) {
        TimingMetric timingMetric = new TimingMetric(this.kit.getIdentifier() + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + str, TIMING_METRIC_TAG);
        timingMetric.startMeasuring();
        return timingMetric;
    }

    /* access modifiers changed from: protected */
    public Result doInBackground(Void... voidArr) {
        TimingMetric createAndStartTimingMetric = createAndStartTimingMetric("doInBackground");
        Result result = null;
        if (!isCancelled()) {
            result = this.kit.doInBackground();
        }
        createAndStartTimingMetric.stopMeasuring();
        return result;
    }

    public Priority getPriority() {
        return Priority.HIGH;
    }

    /* access modifiers changed from: protected */
    public void onCancelled(Result result) {
        this.kit.onCancelled(result);
        this.kit.initializationCallback.failure(new InitializationException(this.kit.getIdentifier() + " Initialization was cancelled"));
    }

    /* access modifiers changed from: protected */
    public void onPostExecute(Result result) {
        this.kit.onPostExecute(result);
        this.kit.initializationCallback.success(result);
    }

    /* access modifiers changed from: protected */
    public void onPreExecute() {
        super.onPreExecute();
        TimingMetric createAndStartTimingMetric = createAndStartTimingMetric("onPreExecute");
        try {
            boolean onPreExecute = this.kit.onPreExecute();
            createAndStartTimingMetric.stopMeasuring();
            if (!onPreExecute) {
                cancel(true);
            }
        } catch (UnmetDependencyException e) {
            throw e;
        } catch (Exception e2) {
            Fabric.getLogger().mo20972e(Fabric.TAG, "Failure onPreExecute()", e2);
            createAndStartTimingMetric.stopMeasuring();
            cancel(true);
        } catch (Throwable th) {
            createAndStartTimingMetric.stopMeasuring();
            cancel(true);
            throw th;
        }
    }
}

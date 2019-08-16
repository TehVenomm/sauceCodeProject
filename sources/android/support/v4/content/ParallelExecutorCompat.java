package android.support.p000v4.content;

import android.os.AsyncTask;
import java.util.concurrent.Executor;

@Deprecated
/* renamed from: android.support.v4.content.ParallelExecutorCompat */
public final class ParallelExecutorCompat {
    private ParallelExecutorCompat() {
    }

    @Deprecated
    public static Executor getParallelExecutor() {
        return AsyncTask.THREAD_POOL_EXECUTOR;
    }
}

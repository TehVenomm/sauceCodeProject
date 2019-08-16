package p017io.fabric.sdk.android.services.concurrency;

import java.util.Collection;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import p017io.fabric.sdk.android.services.concurrency.AsyncTask.Status;

/* renamed from: io.fabric.sdk.android.services.concurrency.PriorityAsyncTask */
public abstract class PriorityAsyncTask<Params, Progress, Result> extends AsyncTask<Params, Progress, Result> implements Dependency<Task>, PriorityProvider, Task, DelegateProvider {
    private final PriorityTask priorityTask = new PriorityTask();

    /* renamed from: io.fabric.sdk.android.services.concurrency.PriorityAsyncTask$ProxyExecutor */
    private static class ProxyExecutor<Result> implements Executor {
        private final Executor executor;
        /* access modifiers changed from: private */
        public final PriorityAsyncTask task;

        public ProxyExecutor(Executor executor2, PriorityAsyncTask priorityAsyncTask) {
            this.executor = executor2;
            this.task = priorityAsyncTask;
        }

        public void execute(Runnable runnable) {
            this.executor.execute(new PriorityFutureTask<Result>(runnable, null) {
                public <T extends Dependency<Task> & PriorityProvider & Task> T getDelegate() {
                    return ProxyExecutor.this.task;
                }
            });
        }
    }

    public void addDependency(Task task) {
        if (getStatus() != Status.PENDING) {
            throw new IllegalStateException("Must not add Dependency after task is running");
        }
        ((Dependency) ((PriorityProvider) getDelegate())).addDependency(task);
    }

    public boolean areDependenciesMet() {
        return ((Dependency) ((PriorityProvider) getDelegate())).areDependenciesMet();
    }

    public int compareTo(Object obj) {
        return Priority.compareTo(this, obj);
    }

    public final void executeOnExecutor(ExecutorService executorService, Params... paramsArr) {
        super.executeOnExecutor(new ProxyExecutor(executorService, this), paramsArr);
    }

    public <T extends Dependency<Task> & PriorityProvider & Task> T getDelegate() {
        return this.priorityTask;
    }

    public Collection<Task> getDependencies() {
        return ((Dependency) ((PriorityProvider) getDelegate())).getDependencies();
    }

    public Throwable getError() {
        return ((Task) ((PriorityProvider) getDelegate())).getError();
    }

    public Priority getPriority() {
        return ((PriorityProvider) getDelegate()).getPriority();
    }

    public boolean isFinished() {
        return ((Task) ((PriorityProvider) getDelegate())).isFinished();
    }

    public void setError(Throwable th) {
        ((Task) ((PriorityProvider) getDelegate())).setError(th);
    }

    public void setFinished(boolean z) {
        ((Task) ((PriorityProvider) getDelegate())).setFinished(z);
    }
}

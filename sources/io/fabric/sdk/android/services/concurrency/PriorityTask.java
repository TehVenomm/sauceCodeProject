package io.fabric.sdk.android.services.concurrency;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicReference;

public class PriorityTask implements Dependency<Task>, PriorityProvider, Task {
    private final List<Task> dependencies = new ArrayList();
    private final AtomicBoolean hasRun = new AtomicBoolean(false);
    private final AtomicReference<Throwable> throwable = new AtomicReference(null);

    public static boolean isProperDelegate(Object obj) {
        try {
            return (((Dependency) obj) == null || ((Task) obj) == null || ((PriorityProvider) obj) == null) ? false : true;
        } catch (ClassCastException e) {
            return false;
        }
    }

    public void addDependency(Task task) {
        synchronized (this) {
            this.dependencies.add(task);
        }
    }

    public boolean areDependenciesMet() {
        for (Task isFinished : getDependencies()) {
            if (!isFinished.isFinished()) {
                return false;
            }
        }
        return true;
    }

    public int compareTo(Object obj) {
        return Priority.compareTo(this, obj);
    }

    public Collection<Task> getDependencies() {
        Collection<Task> unmodifiableCollection;
        synchronized (this) {
            unmodifiableCollection = Collections.unmodifiableCollection(this.dependencies);
        }
        return unmodifiableCollection;
    }

    public Throwable getError() {
        return (Throwable) this.throwable.get();
    }

    public Priority getPriority() {
        return Priority.NORMAL;
    }

    public boolean isFinished() {
        return this.hasRun.get();
    }

    public void setError(Throwable th) {
        this.throwable.set(th);
    }

    public void setFinished(boolean z) {
        synchronized (this) {
            this.hasRun.set(z);
        }
    }
}

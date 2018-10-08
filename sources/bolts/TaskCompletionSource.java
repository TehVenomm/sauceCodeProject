package bolts;

public class TaskCompletionSource<TResult> {
    private final Task<TResult> task = new Task();

    public Task<TResult> getTask() {
        return this.task;
    }

    public void setCancelled() {
        if (!trySetCancelled()) {
            throw new IllegalStateException("Cannot cancel a completed task.");
        }
    }

    public void setError(Exception exception) {
        if (!trySetError(exception)) {
            throw new IllegalStateException("Cannot set the error on a completed task.");
        }
    }

    public void setResult(TResult tResult) {
        if (!trySetResult(tResult)) {
            throw new IllegalStateException("Cannot set the result of a completed task.");
        }
    }

    public boolean trySetCancelled() {
        return this.task.trySetCancelled();
    }

    public boolean trySetError(Exception exception) {
        return this.task.trySetError(exception);
    }

    public boolean trySetResult(TResult tResult) {
        return this.task.trySetResult(tResult);
    }
}

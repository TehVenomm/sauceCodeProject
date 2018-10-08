package com.google.common.collect;

public class AsynchronousComputationException extends ComputationException {
    private static final long serialVersionUID = 0;

    public AsynchronousComputationException(Throwable th) {
        super(th);
    }
}

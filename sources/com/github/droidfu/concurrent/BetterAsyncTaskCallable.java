package com.github.droidfu.concurrent;

public interface BetterAsyncTaskCallable<ParameterT, ProgressT, ReturnT> {
    ReturnT call(BetterAsyncTask<ParameterT, ProgressT, ReturnT> betterAsyncTask) throws Exception;
}

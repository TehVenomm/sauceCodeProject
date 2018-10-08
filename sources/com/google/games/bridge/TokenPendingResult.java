package com.google.games.bridge;

import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

public class TokenPendingResult extends PendingResult<TokenResult> {
    private static final String TAG = "TokenPendingResult";
    private CountDownLatch latch = new CountDownLatch(1);
    TokenResult result = new TokenResult();
    private ResultCallback<? super TokenResult> resultCallback;

    private ResultCallback<? super TokenResult> getCallback() {
        ResultCallback<? super TokenResult> resultCallback;
        synchronized (this) {
            resultCallback = this.resultCallback;
        }
        return resultCallback;
    }

    private TokenResult getResult() {
        TokenResult tokenResult;
        synchronized (this) {
            tokenResult = this.result;
        }
        return tokenResult;
    }

    private void setCallback(ResultCallback<? super TokenResult> resultCallback) {
        synchronized (this) {
            this.resultCallback = resultCallback;
        }
    }

    private void setResult(String str, String str2, String str3, int i) {
        synchronized (this) {
            if (this.result != null && str == null) {
                str = this.result.getAuthCode();
            }
            if (this.result != null && str3 == null) {
                str3 = this.result.getIdToken();
            }
            if (this.result != null && str2 == null) {
                str2 = this.result.getEmail();
            }
            this.result = new TokenResult(str, str2, str3, i);
        }
    }

    @NonNull
    public TokenResult await() {
        try {
            this.latch.await();
        } catch (InterruptedException e) {
            setResult(null, null, null, 14);
        }
        return getResult();
    }

    @NonNull
    public TokenResult await(long j, @NonNull TimeUnit timeUnit) {
        try {
            if (!this.latch.await(j, timeUnit)) {
                setResult(null, null, null, 15);
            }
        } catch (InterruptedException e) {
            setResult(null, null, null, 14);
        }
        return getResult();
    }

    public void cancel() {
        setResult(null, null, null, 16);
        this.latch.countDown();
    }

    public boolean isCanceled() {
        return getResult() != null && getResult().getStatus().isCanceled();
    }

    public void setAuthCode(String str) {
        this.result.setAuthCode(str);
    }

    public void setEmail(String str) {
        this.result.setEmail(str);
    }

    public void setIdToken(String str) {
        this.result.setIdToken(str);
    }

    public void setResultCallback(@NonNull ResultCallback<? super TokenResult> resultCallback) {
        if (this.latch.getCount() == 0) {
            resultCallback.onResult(getResult());
        } else {
            setCallback(resultCallback);
        }
    }

    public void setResultCallback(@NonNull ResultCallback<? super TokenResult> resultCallback, long j, @NonNull TimeUnit timeUnit) {
        try {
            if (!this.latch.await(j, timeUnit)) {
                setResult(null, null, null, 15);
            }
        } catch (InterruptedException e) {
            setResult(null, null, null, 14);
        }
        resultCallback.onResult(getResult());
    }

    public void setStatus(int i) {
        this.result.setStatus(i);
        this.latch.countDown();
        ResultCallback callback = getCallback();
        Result result = getResult();
        if (callback != null) {
            Log.d(TAG, " Calling onResult for callback: " + callback + " result: " + result);
            getCallback().onResult(result);
        }
    }
}

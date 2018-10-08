package org.onepf.oms.appstore.googleUtils;

import org.jetbrains.annotations.NotNull;

public class IabException extends Exception {
    IabResult mResult;

    public IabException(int i, String str) {
        this(new IabResult(i, str));
    }

    public IabException(int i, String str, Exception exception) {
        this(new IabResult(i, str), exception);
    }

    public IabException(@NotNull IabResult iabResult) {
        this(iabResult, null);
    }

    public IabException(@NotNull IabResult iabResult, Exception exception) {
        super(iabResult.getMessage(), exception);
        this.mResult = iabResult;
    }

    public IabResult getResult() {
        return this.mResult;
    }
}

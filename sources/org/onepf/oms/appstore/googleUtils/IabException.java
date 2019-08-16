package org.onepf.oms.appstore.googleUtils;

import org.jetbrains.annotations.NotNull;

public class IabException extends Exception {
    IabResult mResult;

    public IabException(int i, String str) {
        this(new IabResult(i, str));
    }

    public IabException(int i, String str, Exception exc) {
        this(new IabResult(i, str), exc);
    }

    public IabException(@NotNull IabResult iabResult) {
        this(iabResult, (Exception) null);
    }

    public IabException(@NotNull IabResult iabResult, Exception exc) {
        super(iabResult.getMessage(), exc);
        this.mResult = iabResult;
    }

    public IabResult getResult() {
        return this.mResult;
    }
}

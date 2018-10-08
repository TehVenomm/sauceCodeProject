package org.onepf.oms.appstore.googleUtils;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

public class IabResult {
    String mMessage;
    int mResponse;

    public IabResult(int i, @Nullable String str) {
        this.mResponse = i;
        if (str == null || str.trim().length() == 0) {
            this.mMessage = IabHelper.getResponseDesc(i);
        } else {
            this.mMessage = str + " (response: " + IabHelper.getResponseDesc(i) + ")";
        }
    }

    public String getMessage() {
        return this.mMessage;
    }

    public int getResponse() {
        return this.mResponse;
    }

    public boolean isFailure() {
        return !isSuccess();
    }

    public boolean isSuccess() {
        return this.mResponse == 0;
    }

    @NotNull
    public String toString() {
        return "IabResult: " + this.mResponse + ", " + getMessage();
    }
}

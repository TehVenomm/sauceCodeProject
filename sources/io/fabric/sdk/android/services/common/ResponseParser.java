package io.fabric.sdk.android.services.common;

import com.google.android.gms.nearby.messages.Strategy;

public class ResponseParser {
    public static final int ResponseActionDiscard = 0;
    public static final int ResponseActionRetry = 1;

    public static int parse(int i) {
        return (i < 200 || i > 299) ? (i < Strategy.TTL_SECONDS_DEFAULT || i > 399) ? (i < 400 || i > 499) ? i >= 500 ? 1 : 1 : 0 : 1 : 0;
    }
}

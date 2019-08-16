package com.google.android.apps.analytics;

class Referrer {
    private final int index;
    private final String referrer;
    private final long timeStamp;
    private final int visit;

    Referrer(String str, long j, int i, int i2) {
        this.referrer = str;
        this.timeStamp = j;
        this.visit = i;
        this.index = i2;
    }

    /* access modifiers changed from: 0000 */
    public int getIndex() {
        return this.index;
    }

    /* access modifiers changed from: 0000 */
    public String getReferrerString() {
        return this.referrer;
    }

    /* access modifiers changed from: 0000 */
    public long getTimeStamp() {
        return this.timeStamp;
    }

    /* access modifiers changed from: 0000 */
    public int getVisit() {
        return this.visit;
    }
}

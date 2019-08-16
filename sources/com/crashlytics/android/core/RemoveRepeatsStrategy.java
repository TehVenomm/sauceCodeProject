package com.crashlytics.android.core;

import java.util.HashMap;

class RemoveRepeatsStrategy implements StackTraceTrimmingStrategy {
    private final int maxRepetitions;

    public RemoveRepeatsStrategy() {
        this(1);
    }

    public RemoveRepeatsStrategy(int i) {
        this.maxRepetitions = i;
    }

    private static boolean isRepeatingSequence(StackTraceElement[] stackTraceElementArr, int i, int i2) {
        int i3 = i2 - i;
        if (i2 + i3 > stackTraceElementArr.length) {
            return false;
        }
        for (int i4 = 0; i4 < i3; i4++) {
            if (!stackTraceElementArr[i + i4].equals(stackTraceElementArr[i2 + i4])) {
                return false;
            }
        }
        return true;
    }

    private static StackTraceElement[] trimRepeats(StackTraceElement[] stackTraceElementArr, int i) {
        int i2;
        int i3;
        HashMap hashMap = new HashMap();
        StackTraceElement[] stackTraceElementArr2 = new StackTraceElement[stackTraceElementArr.length];
        int i4 = 1;
        int i5 = 0;
        int i6 = 0;
        while (i6 < stackTraceElementArr.length) {
            StackTraceElement stackTraceElement = stackTraceElementArr[i6];
            Integer num = (Integer) hashMap.get(stackTraceElement);
            if (num == null || !isRepeatingSequence(stackTraceElementArr, num.intValue(), i6)) {
                stackTraceElementArr2[i5] = stackTraceElementArr[i6];
                i5++;
                i3 = 1;
                i2 = i6;
            } else {
                int intValue = i6 - num.intValue();
                if (i4 < i) {
                    System.arraycopy(stackTraceElementArr, i6, stackTraceElementArr2, i5, intValue);
                    i5 += intValue;
                    i3 = i4 + 1;
                } else {
                    i3 = i4;
                }
                i2 = (intValue - 1) + i6;
            }
            hashMap.put(stackTraceElement, Integer.valueOf(i6));
            i6 = i2 + 1;
            i4 = i3;
        }
        StackTraceElement[] stackTraceElementArr3 = new StackTraceElement[i5];
        System.arraycopy(stackTraceElementArr2, 0, stackTraceElementArr3, 0, stackTraceElementArr3.length);
        return stackTraceElementArr3;
    }

    public StackTraceElement[] getTrimmedStackTrace(StackTraceElement[] stackTraceElementArr) {
        StackTraceElement[] trimRepeats = trimRepeats(stackTraceElementArr, this.maxRepetitions);
        return trimRepeats.length < stackTraceElementArr.length ? trimRepeats : stackTraceElementArr;
    }
}

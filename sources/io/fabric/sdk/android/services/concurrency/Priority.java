package io.fabric.sdk.android.services.concurrency;

public enum Priority {
    LOW,
    NORMAL,
    HIGH,
    IMMEDIATE;

    static <Y> int compareTo(PriorityProvider priorityProvider, Y y) {
        return (y instanceof PriorityProvider ? ((PriorityProvider) y).getPriority() : NORMAL).ordinal() - priorityProvider.getPriority().ordinal();
    }
}

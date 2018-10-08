package com.google.firebase.messaging.cpp;

public final class SerializedEventUnion {
    public static final byte NONE = (byte) 0;
    public static final byte SerializedMessage = (byte) 1;
    public static final byte SerializedTokenReceived = (byte) 2;
    public static final String[] names = new String[]{"NONE", "SerializedMessage", "SerializedTokenReceived"};

    private SerializedEventUnion() {
    }

    public static String name(int i) {
        return names[i];
    }
}

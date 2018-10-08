package com.google.common.collect;

class NullOutputException extends NullPointerException {
    private static final long serialVersionUID = 0;

    public NullOutputException(String str) {
        super(str);
    }
}

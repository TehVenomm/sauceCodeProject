package net.gogame.gowrap.support;

public class SupportServiceException extends Exception {
    private final Integer code;

    public SupportServiceException(Throwable th) {
        super(th);
        this.code = null;
    }

    public SupportServiceException(int i, String str) {
        super(str);
        this.code = Integer.valueOf(i);
    }

    public Integer getCode() {
        return this.code;
    }
}

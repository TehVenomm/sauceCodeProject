package net.gogame.gowrap.support;

public class SupportCategory {
    private final String id;
    private final int stringResourceId;

    public SupportCategory(String str, int i) {
        this.id = str;
        this.stringResourceId = i;
    }

    public String getId() {
        return this.id;
    }

    public int getStringResourceId() {
        return this.stringResourceId;
    }
}

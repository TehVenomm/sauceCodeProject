package net.gogame.gowrap.support;

public class LocaleDescriptor {
    private final String id;
    private final String name;

    public LocaleDescriptor(String str, String str2) {
        this.id = str;
        this.name = str2;
    }

    public String getId() {
        return this.id;
    }

    public String getName() {
        return this.name;
    }

    public String toString() {
        return this.name;
    }
}

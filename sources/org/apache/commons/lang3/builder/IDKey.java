package org.apache.commons.lang3.builder;

final class IDKey {

    /* renamed from: id */
    private final int f1423id;
    private final Object value;

    public IDKey(Object obj) {
        this.f1423id = System.identityHashCode(obj);
        this.value = obj;
    }

    public int hashCode() {
        return this.f1423id;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof IDKey)) {
            return false;
        }
        IDKey iDKey = (IDKey) obj;
        if (this.f1423id == iDKey.f1423id && this.value == iDKey.value) {
            return true;
        }
        return false;
    }
}

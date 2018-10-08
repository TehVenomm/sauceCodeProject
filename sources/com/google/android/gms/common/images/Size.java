package com.google.android.gms.common.images;

import com.github.droidfu.support.DisplaySupport;

public final class Size {
    private final int zzakv;
    private final int zzakw;

    public Size(int i, int i2) {
        this.zzakv = i;
        this.zzakw = i2;
    }

    public static Size parseSize(String str) throws NumberFormatException {
        if (str == null) {
            throw new IllegalArgumentException("string must not be null");
        }
        int indexOf = str.indexOf(42);
        if (indexOf < 0) {
            indexOf = str.indexOf(DisplaySupport.SCREEN_DENSITY_LOW);
        }
        if (indexOf < 0) {
            throw zzfw(str);
        }
        try {
            return new Size(Integer.parseInt(str.substring(0, indexOf)), Integer.parseInt(str.substring(indexOf + 1)));
        } catch (NumberFormatException e) {
            throw zzfw(str);
        }
    }

    private static NumberFormatException zzfw(String str) {
        throw new NumberFormatException(new StringBuilder(String.valueOf(str).length() + 16).append("Invalid Size: \"").append(str).append("\"").toString());
    }

    public final boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof Size)) {
            return false;
        }
        Size size = (Size) obj;
        return this.zzakv == size.zzakv && this.zzakw == size.zzakw;
    }

    public final int getHeight() {
        return this.zzakw;
    }

    public final int getWidth() {
        return this.zzakv;
    }

    public final int hashCode() {
        return this.zzakw ^ ((this.zzakv << 16) | (this.zzakv >>> 16));
    }

    public final String toString() {
        int i = this.zzakv;
        return i + "x" + this.zzakw;
    }
}

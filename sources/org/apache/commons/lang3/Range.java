package org.apache.commons.lang3;

import java.io.Serializable;
import java.util.Comparator;

public final class Range<T> implements Serializable {
    private static final long serialVersionUID = 1;
    private final Comparator<T> comparator;
    private transient int hashCode;
    private final T maximum;
    private final T minimum;
    private transient String toString;

    private enum ComparableComparator implements Comparator {
        INSTANCE;

        public int compare(Object obj, Object obj2) {
            return ((Comparable) obj).compareTo(obj2);
        }
    }

    /* renamed from: is */
    public static <T extends Comparable<T>> Range<T> m1013is(T t) {
        return between(t, t, null);
    }

    /* renamed from: is */
    public static <T> Range<T> m1014is(T t, Comparator<T> comparator2) {
        return between(t, t, comparator2);
    }

    public static <T extends Comparable<T>> Range<T> between(T t, T t2) {
        return between(t, t2, null);
    }

    public static <T> Range<T> between(T t, T t2, Comparator<T> comparator2) {
        return new Range<>(t, t2, comparator2);
    }

    private Range(T t, T t2, Comparator<T> comparator2) {
        if (t == null || t2 == null) {
            throw new IllegalArgumentException("Elements in a range must not be null: element1=" + t + ", element2=" + t2);
        }
        if (comparator2 == null) {
            this.comparator = ComparableComparator.INSTANCE;
        } else {
            this.comparator = comparator2;
        }
        if (this.comparator.compare(t, t2) < 1) {
            this.minimum = t;
            this.maximum = t2;
            return;
        }
        this.minimum = t2;
        this.maximum = t;
    }

    public T getMinimum() {
        return this.minimum;
    }

    public T getMaximum() {
        return this.maximum;
    }

    public Comparator<T> getComparator() {
        return this.comparator;
    }

    public boolean isNaturalOrdering() {
        return this.comparator == ComparableComparator.INSTANCE;
    }

    public boolean contains(T t) {
        boolean z = true;
        if (t == null) {
            return false;
        }
        if (this.comparator.compare(t, this.minimum) <= -1 || this.comparator.compare(t, this.maximum) >= 1) {
            z = false;
        }
        return z;
    }

    public boolean isAfter(T t) {
        if (t != null && this.comparator.compare(t, this.minimum) < 0) {
            return true;
        }
        return false;
    }

    public boolean isStartedBy(T t) {
        if (t != null && this.comparator.compare(t, this.minimum) == 0) {
            return true;
        }
        return false;
    }

    public boolean isEndedBy(T t) {
        if (t != null && this.comparator.compare(t, this.maximum) == 0) {
            return true;
        }
        return false;
    }

    public boolean isBefore(T t) {
        if (t != null && this.comparator.compare(t, this.maximum) > 0) {
            return true;
        }
        return false;
    }

    public int elementCompareTo(T t) {
        if (t == null) {
            throw new NullPointerException("Element is null");
        } else if (isAfter(t)) {
            return -1;
        } else {
            if (isBefore(t)) {
                return 1;
            }
            return 0;
        }
    }

    public boolean containsRange(Range<T> range) {
        if (range != null && contains(range.minimum) && contains(range.maximum)) {
            return true;
        }
        return false;
    }

    public boolean isAfterRange(Range<T> range) {
        if (range == null) {
            return false;
        }
        return isAfter(range.maximum);
    }

    public boolean isOverlappedBy(Range<T> range) {
        if (range == null) {
            return false;
        }
        if (range.contains(this.minimum) || range.contains(this.maximum) || contains(range.minimum)) {
            return true;
        }
        return false;
    }

    public boolean isBeforeRange(Range<T> range) {
        if (range == null) {
            return false;
        }
        return isBefore(range.minimum);
    }

    public Range<T> intersectionWith(Range<T> range) {
        if (!isOverlappedBy(range)) {
            throw new IllegalArgumentException(String.format("Cannot calculate intersection with non-overlapping range %s", new Object[]{range}));
        } else if (equals(range)) {
            return this;
        } else {
            return between(getComparator().compare(this.minimum, range.minimum) < 0 ? range.minimum : this.minimum, getComparator().compare(this.maximum, range.maximum) < 0 ? this.maximum : range.maximum, getComparator());
        }
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        Range range = (Range) obj;
        if (!this.minimum.equals(range.minimum) || !this.maximum.equals(range.maximum)) {
            return false;
        }
        return true;
    }

    public int hashCode() {
        int i = this.hashCode;
        if (this.hashCode != 0) {
            return i;
        }
        int hashCode2 = ((((getClass().hashCode() + 629) * 37) + this.minimum.hashCode()) * 37) + this.maximum.hashCode();
        this.hashCode = hashCode2;
        return hashCode2;
    }

    public String toString() {
        String str = this.toString;
        if (str != null) {
            return str;
        }
        StringBuilder sb = new StringBuilder(32);
        sb.append('[');
        sb.append(this.minimum);
        sb.append("..");
        sb.append(this.maximum);
        sb.append(']');
        String sb2 = sb.toString();
        this.toString = sb2;
        return sb2;
    }

    public String toString(String str) {
        return String.format(str, new Object[]{this.minimum, this.maximum, this.comparator});
    }
}

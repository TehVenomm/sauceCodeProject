package org.apache.commons.lang3;

import java.io.Serializable;
import java.util.Iterator;
import java.util.NoSuchElementException;

final class CharRange implements Iterable<Character>, Serializable {
    private static final long serialVersionUID = 8270183163158333422L;
    /* access modifiers changed from: private */
    public final char end;
    private transient String iToString;
    /* access modifiers changed from: private */
    public final boolean negated;
    /* access modifiers changed from: private */
    public final char start;

    private static class CharacterIterator implements Iterator<Character> {
        private char current;
        private boolean hasNext;
        private final CharRange range;

        private CharacterIterator(CharRange charRange) {
            this.range = charRange;
            this.hasNext = true;
            if (!this.range.negated) {
                this.current = this.range.start;
            } else if (this.range.start != 0) {
                this.current = 0;
            } else if (this.range.end == 65535) {
                this.hasNext = false;
            } else {
                this.current = (char) (this.range.end + 1);
            }
        }

        private void prepareNext() {
            if (this.range.negated) {
                if (this.current == 65535) {
                    this.hasNext = false;
                } else if (this.current + 1 != this.range.start) {
                    this.current = (char) (this.current + 1);
                } else if (this.range.end == 65535) {
                    this.hasNext = false;
                } else {
                    this.current = (char) (this.range.end + 1);
                }
            } else if (this.current < this.range.end) {
                this.current = (char) (this.current + 1);
            } else {
                this.hasNext = false;
            }
        }

        public boolean hasNext() {
            return this.hasNext;
        }

        public Character next() {
            if (!this.hasNext) {
                throw new NoSuchElementException();
            }
            char c = this.current;
            prepareNext();
            return Character.valueOf(c);
        }

        public void remove() {
            throw new UnsupportedOperationException();
        }
    }

    private CharRange(char c, char c2, boolean z) {
        char c3;
        char c4;
        if (c > c2) {
            c3 = c;
            c4 = c2;
        } else {
            c3 = c2;
            c4 = c;
        }
        this.start = c4;
        this.end = c3;
        this.negated = z;
    }

    /* renamed from: is */
    public static CharRange m1012is(char c) {
        return new CharRange(c, c, false);
    }

    public static CharRange isNot(char c) {
        return new CharRange(c, c, true);
    }

    public static CharRange isIn(char c, char c2) {
        return new CharRange(c, c2, false);
    }

    public static CharRange isNotIn(char c, char c2) {
        return new CharRange(c, c2, true);
    }

    public char getStart() {
        return this.start;
    }

    public char getEnd() {
        return this.end;
    }

    public boolean isNegated() {
        return this.negated;
    }

    public boolean contains(char c) {
        return (c >= this.start && c <= this.end) != this.negated;
    }

    public boolean contains(CharRange charRange) {
        boolean z = false;
        if (charRange == null) {
            throw new IllegalArgumentException("The Range must not be null");
        } else if (this.negated) {
            if (!charRange.negated) {
                if (charRange.end < this.start || charRange.start > this.end) {
                    z = true;
                }
                return z;
            } else if (this.start < charRange.start || this.end > charRange.end) {
                return false;
            } else {
                return true;
            }
        } else if (charRange.negated) {
            if (this.start == 0 && this.end == 65535) {
                return true;
            }
            return false;
        } else if (this.start > charRange.start || this.end < charRange.end) {
            return false;
        } else {
            return true;
        }
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof CharRange)) {
            return false;
        }
        CharRange charRange = (CharRange) obj;
        if (this.start == charRange.start && this.end == charRange.end && this.negated == charRange.negated) {
            return true;
        }
        return false;
    }

    public int hashCode() {
        return (this.negated ? 1 : 0) + (this.end * 7) + this.start + 'S';
    }

    public String toString() {
        if (this.iToString == null) {
            StringBuilder sb = new StringBuilder(4);
            if (isNegated()) {
                sb.append('^');
            }
            sb.append(this.start);
            if (this.start != this.end) {
                sb.append('-');
                sb.append(this.end);
            }
            this.iToString = sb.toString();
        }
        return this.iToString;
    }

    public Iterator<Character> iterator() {
        return new CharacterIterator();
    }
}

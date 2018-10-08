package org.apache.commons.lang3.time;

import android.support.v4.view.MotionEventCompat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.TimeZone;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class DurationFormatUtils {
    /* renamed from: H */
    static final Object f1351H = "H";
    public static final String ISO_EXTENDED_FORMAT_PATTERN = "'P'yyyy'Y'M'M'd'DT'H'H'm'M's.SSS'S'";
    /* renamed from: M */
    static final Object f1352M = "M";
    /* renamed from: S */
    static final Object f1353S = "S";
    /* renamed from: d */
    static final Object f1354d = "d";
    /* renamed from: m */
    static final Object f1355m = "m";
    /* renamed from: s */
    static final Object f1356s = "s";
    /* renamed from: y */
    static final Object f1357y = "y";

    static class Token {
        private int count;
        private final Object value;

        static boolean containsTokenWithValue(Token[] tokenArr, Object obj) {
            for (Token value : tokenArr) {
                if (value.getValue() == obj) {
                    return true;
                }
            }
            return false;
        }

        Token(Object obj) {
            this.value = obj;
            this.count = 1;
        }

        Token(Object obj, int i) {
            this.value = obj;
            this.count = i;
        }

        void increment() {
            this.count++;
        }

        int getCount() {
            return this.count;
        }

        Object getValue() {
            return this.value;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof Token)) {
                return false;
            }
            Token token = (Token) obj;
            if (this.value.getClass() != token.value.getClass() || this.count != token.count) {
                return false;
            }
            if (this.value instanceof StringBuilder) {
                return this.value.toString().equals(token.value.toString());
            }
            if (this.value instanceof Number) {
                return this.value.equals(token.value);
            }
            if (this.value == token.value) {
                return true;
            }
            return false;
        }

        public int hashCode() {
            return this.value.hashCode();
        }

        public String toString() {
            return StringUtils.repeat(this.value.toString(), this.count);
        }
    }

    public static String formatDurationHMS(long j) {
        return formatDuration(j, "HH:mm:ss.SSS");
    }

    public static String formatDurationISO(long j) {
        return formatDuration(j, ISO_EXTENDED_FORMAT_PATTERN, false);
    }

    public static String formatDuration(long j, String str) {
        return formatDuration(j, str, true);
    }

    public static String formatDuration(long j, String str, boolean z) {
        long j2;
        Validate.inclusiveBetween(0, Long.MAX_VALUE, j, "durationMillis must not be negative");
        Token[] lexx = lexx(str);
        long j3 = 0;
        long j4 = 0;
        long j5 = 0;
        long j6 = 0;
        if (Token.containsTokenWithValue(lexx, f1354d)) {
            j3 = j / DateUtils.MILLIS_PER_DAY;
            j -= DateUtils.MILLIS_PER_DAY * j3;
        }
        if (Token.containsTokenWithValue(lexx, f1351H)) {
            j4 = j / DateUtils.MILLIS_PER_HOUR;
            j -= DateUtils.MILLIS_PER_HOUR * j4;
        }
        if (Token.containsTokenWithValue(lexx, f1355m)) {
            j5 = j / 60000;
            j -= 60000 * j5;
        }
        if (Token.containsTokenWithValue(lexx, f1356s)) {
            j6 = j / 1000;
            j2 = j - (1000 * j6);
        } else {
            j2 = j;
        }
        return format(lexx, 0, 0, j3, j4, j5, j6, j2, z);
    }

    public static String formatDurationWords(long j, boolean z, boolean z2) {
        String str;
        String formatDuration = formatDuration(j, "d' days 'H' hours 'm' minutes 's' seconds'");
        if (z) {
            str = " " + formatDuration;
            formatDuration = StringUtils.replaceOnce(str, " 0 days", "");
            if (formatDuration.length() != str.length()) {
                str = StringUtils.replaceOnce(formatDuration, " 0 hours", "");
                if (str.length() != formatDuration.length()) {
                    formatDuration = StringUtils.replaceOnce(str, " 0 minutes", "");
                    if (formatDuration.length() != formatDuration.length()) {
                        formatDuration = StringUtils.replaceOnce(formatDuration, " 0 seconds", "");
                    }
                }
            } else {
                formatDuration = str;
            }
            if (formatDuration.length() != 0) {
                formatDuration = formatDuration.substring(1);
            }
        }
        if (z2) {
            str = StringUtils.replaceOnce(formatDuration, " 0 seconds", "");
            if (str.length() != formatDuration.length()) {
                formatDuration = StringUtils.replaceOnce(str, " 0 minutes", "");
                if (formatDuration.length() != str.length()) {
                    str = StringUtils.replaceOnce(formatDuration, " 0 hours", "");
                    if (str.length() != formatDuration.length()) {
                        formatDuration = StringUtils.replaceOnce(str, " 0 days", "");
                    }
                } else {
                    formatDuration = str;
                }
            }
        }
        return StringUtils.replaceOnce(StringUtils.replaceOnce(StringUtils.replaceOnce(StringUtils.replaceOnce(" " + formatDuration, " 1 seconds", " 1 second"), " 1 minutes", " 1 minute"), " 1 hours", " 1 hour"), " 1 days", " 1 day").trim();
    }

    public static String formatPeriodISO(long j, long j2) {
        return formatPeriod(j, j2, ISO_EXTENDED_FORMAT_PATTERN, false, TimeZone.getDefault());
    }

    public static String formatPeriod(long j, long j2, String str) {
        return formatPeriod(j, j2, str, true, TimeZone.getDefault());
    }

    public static String formatPeriod(long j, long j2, String str, boolean z, TimeZone timeZone) {
        int i;
        int i2;
        int i3;
        int i4;
        int i5;
        Validate.isTrue(j <= j2, "startMillis must not be greater than endMillis", new Object[0]);
        Token[] lexx = lexx(str);
        Calendar instance = Calendar.getInstance(timeZone);
        instance.setTime(new Date(j));
        Calendar instance2 = Calendar.getInstance(timeZone);
        instance2.setTime(new Date(j2));
        int i6 = instance2.get(14) - instance.get(14);
        int i7 = instance2.get(13) - instance.get(13);
        int i8 = instance2.get(12) - instance.get(12);
        int i9 = instance2.get(11) - instance.get(11);
        int i10 = instance2.get(5) - instance.get(5);
        int i11 = instance2.get(2) - instance.get(2);
        int i12 = instance2.get(1) - instance.get(1);
        while (i6 < 0) {
            i6 += 1000;
            i7--;
        }
        while (i7 < 0) {
            i7 += 60;
            i8--;
        }
        while (i8 < 0) {
            i8 += 60;
            i9--;
        }
        while (i9 < 0) {
            i9 += 24;
            i10--;
        }
        if (Token.containsTokenWithValue(lexx, f1352M)) {
            int i13 = i11;
            i11 = i10;
            i10 = i13;
            while (i11 < 0) {
                i11 += instance.getActualMaximum(5);
                i10--;
                instance.add(2, 1);
            }
            while (i10 < 0) {
                i10 += 12;
                i12--;
            }
            if (!(Token.containsTokenWithValue(lexx, f1357y) || i12 == 0)) {
                while (i12 != 0) {
                    i10 += i12 * 12;
                    i12 = 0;
                }
            }
        } else {
            if (!Token.containsTokenWithValue(lexx, f1357y)) {
                i12 = instance2.get(1);
                if (i11 < 0) {
                    i12--;
                }
                while (instance.get(1) != i12) {
                    i10 += instance.getActualMaximum(6) - instance.get(6);
                    if ((instance instanceof GregorianCalendar) && instance.get(2) == 1 && instance.get(5) == 29) {
                        i10++;
                    }
                    instance.add(1, 1);
                    i10 += instance.get(6);
                }
                i12 = 0;
            }
            while (instance.get(2) != instance2.get(2)) {
                i10 += instance.getActualMaximum(5);
                instance.add(2, 1);
            }
            i11 = i10;
            i10 = 0;
            while (i11 < 0) {
                i11 += instance.getActualMaximum(5);
                i10--;
                instance.add(2, 1);
            }
        }
        int i14 = i10;
        i10 = i11;
        if (Token.containsTokenWithValue(lexx, f1354d)) {
            i = i10;
            i10 = i9;
        } else {
            i = 0;
            i10 = i9 + (i10 * 24);
        }
        if (Token.containsTokenWithValue(lexx, f1351H)) {
            i2 = i10;
            i10 = i8;
        } else {
            i2 = 0;
            i10 = i8 + (i10 * 60);
        }
        if (Token.containsTokenWithValue(lexx, f1355m)) {
            i3 = i10;
            i10 = i7;
        } else {
            i3 = 0;
            i10 = i7 + (i10 * 60);
        }
        if (Token.containsTokenWithValue(lexx, f1356s)) {
            i4 = i10;
            i5 = i6;
        } else {
            i4 = 0;
            i5 = i6 + (i10 * 1000);
        }
        return format(lexx, (long) i12, (long) i14, (long) i, (long) i2, (long) i3, (long) i4, (long) i5, z);
    }

    static String format(Token[] tokenArr, long j, long j2, long j3, long j4, long j5, long j6, long j7, boolean z) {
        StringBuilder stringBuilder = new StringBuilder();
        Object obj = null;
        for (Token token : tokenArr) {
            Object value = token.getValue();
            int count = token.getCount();
            if (value instanceof StringBuilder) {
                stringBuilder.append(value.toString());
            } else if (value == f1357y) {
                stringBuilder.append(paddedValue(j, z, count));
                obj = null;
            } else if (value == f1352M) {
                stringBuilder.append(paddedValue(j2, z, count));
                obj = null;
            } else if (value == f1354d) {
                stringBuilder.append(paddedValue(j3, z, count));
                obj = null;
            } else if (value == f1351H) {
                stringBuilder.append(paddedValue(j4, z, count));
                obj = null;
            } else if (value == f1355m) {
                stringBuilder.append(paddedValue(j5, z, count));
                obj = null;
            } else if (value == f1356s) {
                stringBuilder.append(paddedValue(j6, z, count));
                obj = 1;
            } else if (value == f1353S) {
                if (obj != null) {
                    stringBuilder.append(paddedValue(j7, true, z ? Math.max(3, count) : 3));
                } else {
                    stringBuilder.append(paddedValue(j7, z, count));
                }
                obj = null;
            }
        }
        return stringBuilder.toString();
    }

    private static String paddedValue(long j, boolean z, int i) {
        String l = Long.toString(j);
        return z ? StringUtils.leftPad(l, i, '0') : l;
    }

    static Token[] lexx(String str) {
        ArrayList arrayList = new ArrayList(str.length());
        Token token = null;
        StringBuilder stringBuilder = null;
        Object obj = null;
        for (int i = 0; i < str.length(); i++) {
            char charAt = str.charAt(i);
            if (obj == null || charAt == '\'') {
                Object obj2;
                switch (charAt) {
                    case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                        if (obj == null) {
                            stringBuilder = new StringBuilder();
                            arrayList.add(new Token(stringBuilder));
                            obj = 1;
                            obj2 = null;
                            break;
                        }
                        obj2 = null;
                        stringBuilder = null;
                        obj = null;
                        break;
                    case 'H':
                        obj2 = f1351H;
                        break;
                    case 'M':
                        obj2 = f1352M;
                        break;
                    case 'S':
                        obj2 = f1353S;
                        break;
                    case 'd':
                        obj2 = f1354d;
                        break;
                    case 'm':
                        obj2 = f1355m;
                        break;
                    case 's':
                        obj2 = f1356s;
                        break;
                    case 'y':
                        obj2 = f1357y;
                        break;
                    default:
                        if (stringBuilder == null) {
                            stringBuilder = new StringBuilder();
                            arrayList.add(new Token(stringBuilder));
                        }
                        stringBuilder.append(charAt);
                        obj2 = null;
                        break;
                }
                if (obj2 != null) {
                    if (token == null || token.getValue() != obj2) {
                        token = new Token(obj2);
                        arrayList.add(token);
                    } else {
                        token.increment();
                    }
                    stringBuilder = null;
                }
            } else {
                stringBuilder.append(charAt);
            }
        }
        if (obj == null) {
            return (Token[]) arrayList.toArray(new Token[arrayList.size()]);
        }
        throw new IllegalArgumentException("Unmatched quote in format: " + str);
    }
}

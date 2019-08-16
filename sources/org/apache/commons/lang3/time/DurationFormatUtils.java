package org.apache.commons.lang3.time;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.TimeZone;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class DurationFormatUtils {

    /* renamed from: H */
    static final Object f1426H = "H";
    public static final String ISO_EXTENDED_FORMAT_PATTERN = "'P'yyyy'Y'M'M'd'DT'H'H'm'M's.SSS'S'";

    /* renamed from: M */
    static final Object f1427M = "M";

    /* renamed from: S */
    static final Object f1428S = "S";

    /* renamed from: d */
    static final Object f1429d = "d";

    /* renamed from: m */
    static final Object f1430m = "m";

    /* renamed from: s */
    static final Object f1431s = "s";

    /* renamed from: y */
    static final Object f1432y = "y";

    static class Token {
        private int count;
        private final Object value;

        static boolean containsTokenWithValue(Token[] tokenArr, Object obj) {
            for (Token value2 : tokenArr) {
                if (value2.getValue() == obj) {
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

        /* access modifiers changed from: 0000 */
        public void increment() {
            this.count++;
        }

        /* access modifiers changed from: 0000 */
        public int getCount() {
            return this.count;
        }

        /* access modifiers changed from: 0000 */
        public Object getValue() {
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
        if (Token.containsTokenWithValue(lexx, f1429d)) {
            j3 = j / DateUtils.MILLIS_PER_DAY;
            j -= DateUtils.MILLIS_PER_DAY * j3;
        }
        if (Token.containsTokenWithValue(lexx, f1426H)) {
            j4 = j / DateUtils.MILLIS_PER_HOUR;
            j -= DateUtils.MILLIS_PER_HOUR * j4;
        }
        if (Token.containsTokenWithValue(lexx, f1430m)) {
            j5 = j / 60000;
            j -= 60000 * j5;
        }
        if (Token.containsTokenWithValue(lexx, f1431s)) {
            j6 = j / 1000;
            j2 = j - (1000 * j6);
        } else {
            j2 = j;
        }
        return format(lexx, 0, 0, j3, j4, j5, j6, j2, z);
    }

    public static String formatDurationWords(long j, boolean z, boolean z2) {
        String formatDuration = formatDuration(j, "d' days 'H' hours 'm' minutes 's' seconds'");
        if (z) {
            String str = " " + formatDuration;
            formatDuration = StringUtils.replaceOnce(str, " 0 days", "");
            if (formatDuration.length() != str.length()) {
                String replaceOnce = StringUtils.replaceOnce(formatDuration, " 0 hours", "");
                if (replaceOnce.length() != formatDuration.length()) {
                    formatDuration = StringUtils.replaceOnce(replaceOnce, " 0 minutes", "");
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
            String replaceOnce2 = StringUtils.replaceOnce(formatDuration, " 0 seconds", "");
            if (replaceOnce2.length() != formatDuration.length()) {
                formatDuration = StringUtils.replaceOnce(replaceOnce2, " 0 minutes", "");
                if (formatDuration.length() != replaceOnce2.length()) {
                    String replaceOnce3 = StringUtils.replaceOnce(formatDuration, " 0 hours", "");
                    if (replaceOnce3.length() != formatDuration.length()) {
                        formatDuration = StringUtils.replaceOnce(replaceOnce3, " 0 days", "");
                    }
                } else {
                    formatDuration = replaceOnce2;
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
        int i6;
        int i7;
        int i8;
        int i9;
        int i10;
        Validate.isTrue(j <= j2, "startMillis must not be greater than endMillis", new Object[0]);
        Token[] lexx = lexx(str);
        Calendar instance = Calendar.getInstance(timeZone);
        instance.setTime(new Date(j));
        Calendar instance2 = Calendar.getInstance(timeZone);
        instance2.setTime(new Date(j2));
        int i11 = instance2.get(14) - instance.get(14);
        int i12 = instance2.get(13) - instance.get(13);
        int i13 = instance2.get(12) - instance.get(12);
        int i14 = instance2.get(11) - instance.get(11);
        int i15 = instance2.get(5) - instance.get(5);
        int i16 = instance2.get(2) - instance.get(2);
        int i17 = instance2.get(1) - instance.get(1);
        while (i11 < 0) {
            i11 += 1000;
            i12--;
        }
        while (i12 < 0) {
            i12 += 60;
            i13--;
        }
        while (i13 < 0) {
            i13 += 60;
            i14--;
        }
        while (i14 < 0) {
            i14 += 24;
            i15--;
        }
        if (Token.containsTokenWithValue(lexx, f1427M)) {
            int i18 = i15;
            while (i18 < 0) {
                i18 += instance.getActualMaximum(5);
                i16--;
                instance.add(2, 1);
            }
            int i19 = i16;
            while (i19 < 0) {
                i19 += 12;
                i17--;
            }
            if (!Token.containsTokenWithValue(lexx, f1432y) && i17 != 0) {
                while (i17 != 0) {
                    i19 += i17 * 12;
                    i17 = 0;
                }
            }
            i2 = i19;
            i = i18;
        } else {
            if (!Token.containsTokenWithValue(lexx, f1432y)) {
                int i20 = instance2.get(1);
                if (i16 < 0) {
                    i20--;
                }
                while (instance.get(1) != i20) {
                    int actualMaximum = i15 + (instance.getActualMaximum(6) - instance.get(6));
                    if ((instance instanceof GregorianCalendar) && instance.get(2) == 1 && instance.get(5) == 29) {
                        actualMaximum++;
                    }
                    instance.add(1, 1);
                    i15 = actualMaximum + instance.get(6);
                }
                i17 = 0;
            }
            while (instance.get(2) != instance2.get(2)) {
                i15 += instance.getActualMaximum(5);
                instance.add(2, 1);
            }
            int i21 = 0;
            while (i15 < 0) {
                i15 += instance.getActualMaximum(5);
                i21--;
                instance.add(2, 1);
            }
            i2 = i21;
            i = i15;
        }
        if (!Token.containsTokenWithValue(lexx, f1429d)) {
            i4 = (i * 24) + i14;
            i3 = 0;
        } else {
            i3 = i;
            i4 = i14;
        }
        if (!Token.containsTokenWithValue(lexx, f1426H)) {
            i6 = i13 + (i4 * 60);
            i5 = 0;
        } else {
            i5 = i4;
            i6 = i13;
        }
        if (!Token.containsTokenWithValue(lexx, f1430m)) {
            i8 = (i6 * 60) + i12;
            i7 = 0;
        } else {
            i7 = i6;
            i8 = i12;
        }
        if (!Token.containsTokenWithValue(lexx, f1431s)) {
            i9 = 0;
            i10 = i11 + (i8 * 1000);
        } else {
            i9 = i8;
            i10 = i11;
        }
        return format(lexx, (long) i17, (long) i2, (long) i3, (long) i5, (long) i7, (long) i9, (long) i10, z);
    }

    static String format(Token[] tokenArr, long j, long j2, long j3, long j4, long j5, long j6, long j7, boolean z) {
        StringBuilder sb = new StringBuilder();
        boolean z2 = false;
        for (Token token : tokenArr) {
            Object value = token.getValue();
            int count = token.getCount();
            if (value instanceof StringBuilder) {
                sb.append(value.toString());
            } else if (value == f1432y) {
                sb.append(paddedValue(j, z, count));
                z2 = false;
            } else if (value == f1427M) {
                sb.append(paddedValue(j2, z, count));
                z2 = false;
            } else if (value == f1429d) {
                sb.append(paddedValue(j3, z, count));
                z2 = false;
            } else if (value == f1426H) {
                sb.append(paddedValue(j4, z, count));
                z2 = false;
            } else if (value == f1430m) {
                sb.append(paddedValue(j5, z, count));
                z2 = false;
            } else if (value == f1431s) {
                sb.append(paddedValue(j6, z, count));
                z2 = true;
            } else if (value == f1428S) {
                if (z2) {
                    sb.append(paddedValue(j7, true, z ? Math.max(3, count) : 3));
                } else {
                    sb.append(paddedValue(j7, z, count));
                }
                z2 = false;
            }
        }
        return sb.toString();
    }

    private static String paddedValue(long j, boolean z, int i) {
        String l = Long.toString(j);
        return z ? StringUtils.leftPad(l, i, '0') : l;
    }

    static Token[] lexx(String str) {
        Object obj;
        ArrayList arrayList = new ArrayList(str.length());
        Token token = null;
        StringBuilder sb = null;
        boolean z = false;
        for (int i = 0; i < str.length(); i++) {
            char charAt = str.charAt(i);
            if (!z || charAt == '\'') {
                switch (charAt) {
                    case '\'':
                        if (!z) {
                            sb = new StringBuilder();
                            arrayList.add(new Token(sb));
                            z = true;
                            obj = null;
                            break;
                        } else {
                            obj = null;
                            sb = null;
                            z = false;
                            break;
                        }
                    case 'H':
                        obj = f1426H;
                        break;
                    case 'M':
                        obj = f1427M;
                        break;
                    case 'S':
                        obj = f1428S;
                        break;
                    case 'd':
                        obj = f1429d;
                        break;
                    case 'm':
                        obj = f1430m;
                        break;
                    case 's':
                        obj = f1431s;
                        break;
                    case 'y':
                        obj = f1432y;
                        break;
                    default:
                        if (sb == null) {
                            sb = new StringBuilder();
                            arrayList.add(new Token(sb));
                        }
                        sb.append(charAt);
                        obj = null;
                        break;
                }
                if (obj != null) {
                    if (token == null || token.getValue() != obj) {
                        token = new Token(obj);
                        arrayList.add(token);
                    } else {
                        token.increment();
                    }
                    sb = null;
                }
            } else {
                sb.append(charAt);
            }
        }
        if (!z) {
            return (Token[]) arrayList.toArray(new Token[arrayList.size()]);
        }
        throw new IllegalArgumentException("Unmatched quote in format: " + str);
    }
}

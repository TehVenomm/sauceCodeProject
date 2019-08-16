package com.fasterxml.jackson.databind.util;

import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Locale;
import java.util.TimeZone;
import net.gogame.gowrap.InternalConstants;
import org.apache.commons.lang3.ClassUtils;

public class ISO8601Utils {
    @Deprecated
    private static final String GMT_ID = "GMT";
    @Deprecated
    private static final TimeZone TIMEZONE_GMT = TimeZone.getTimeZone(GMT_ID);
    private static final TimeZone TIMEZONE_UTC = TimeZone.getTimeZone(UTC_ID);
    private static final TimeZone TIMEZONE_Z = TIMEZONE_UTC;
    private static final String UTC_ID = "UTC";

    @Deprecated
    public static TimeZone timeZoneGMT() {
        return TIMEZONE_GMT;
    }

    public static String format(Date date) {
        return format(date, false, TIMEZONE_UTC);
    }

    public static String format(Date date, boolean z) {
        return format(date, z, TIMEZONE_UTC);
    }

    public static String format(Date date, boolean z, TimeZone timeZone) {
        int length;
        GregorianCalendar gregorianCalendar = new GregorianCalendar(timeZone, Locale.US);
        gregorianCalendar.setTime(date);
        int length2 = "yyyy-MM-ddThh:mm:ss".length() + (z ? ".sss".length() : 0);
        if (timeZone.getRawOffset() == 0) {
            length = "Z".length();
        } else {
            length = "+hh:mm".length();
        }
        StringBuilder sb = new StringBuilder(length + length2);
        padInt(sb, gregorianCalendar.get(1), "yyyy".length());
        sb.append('-');
        padInt(sb, gregorianCalendar.get(2) + 1, "MM".length());
        sb.append('-');
        padInt(sb, gregorianCalendar.get(5), "dd".length());
        sb.append('T');
        padInt(sb, gregorianCalendar.get(11), "hh".length());
        sb.append(':');
        padInt(sb, gregorianCalendar.get(12), "mm".length());
        sb.append(':');
        padInt(sb, gregorianCalendar.get(13), "ss".length());
        if (z) {
            sb.append(ClassUtils.PACKAGE_SEPARATOR_CHAR);
            padInt(sb, gregorianCalendar.get(14), "sss".length());
        }
        int offset = timeZone.getOffset(gregorianCalendar.getTimeInMillis());
        if (offset != 0) {
            int abs = Math.abs((offset / InternalConstants.FAB_BLINKING_TIME_INTERVAL) / 60);
            int abs2 = Math.abs((offset / InternalConstants.FAB_BLINKING_TIME_INTERVAL) % 60);
            sb.append(offset < 0 ? '-' : '+');
            padInt(sb, abs, "hh".length());
            sb.append(':');
            padInt(sb, abs2, "mm".length());
        } else {
            sb.append('Z');
        }
        return sb.toString();
    }

    /* JADX WARNING: Removed duplicated region for block: B:43:0x00ca  */
    /* JADX WARNING: Removed duplicated region for block: B:77:0x0205  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.util.Date parse(java.lang.String r14, java.text.ParsePosition r15) throws java.text.ParseException {
        /*
            r11 = 43
            r13 = 34
            r10 = 45
            r0 = 0
            int r2 = r15.getIndex()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r1 = r2 + 4
            int r7 = parseInt(r14, r2, r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 45
            boolean r2 = checkOffset(r14, r1, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 == 0) goto L_0x0232
            int r1 = r1 + 1
            r2 = r1
        L_0x001c:
            int r1 = r2 + 2
            int r8 = parseInt(r14, r2, r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 45
            boolean r2 = checkOffset(r14, r1, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 == 0) goto L_0x022f
            int r1 = r1 + 1
            r2 = r1
        L_0x002d:
            int r1 = r2 + 2
            int r9 = parseInt(r14, r2, r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 84
            boolean r2 = checkOffset(r14, r1, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 != 0) goto L_0x0050
            int r3 = r14.length()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r3 > r1) goto L_0x0050
            java.util.GregorianCalendar r0 = new java.util.GregorianCalendar     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r2 = r8 + -1
            r0.<init>(r7, r2, r9)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r15.setIndex(r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.util.Date r0 = r0.getTime()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
        L_0x004f:
            return r0
        L_0x0050:
            if (r2 == 0) goto L_0x0228
            int r2 = r1 + 1
            int r1 = r2 + 2
            int r5 = parseInt(r14, r2, r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 58
            boolean r2 = checkOffset(r14, r1, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 == 0) goto L_0x0225
            int r1 = r1 + 1
            r2 = r1
        L_0x0065:
            int r1 = r2 + 2
            int r4 = parseInt(r14, r2, r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 58
            boolean r2 = checkOffset(r14, r1, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 == 0) goto L_0x0075
            int r1 = r1 + 1
        L_0x0075:
            int r2 = r14.length()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 <= r1) goto L_0x0220
            char r2 = r14.charAt(r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r3 = 90
            if (r2 == r3) goto L_0x0220
            if (r2 == r11) goto L_0x0220
            if (r2 == r10) goto L_0x0220
            int r6 = r1 + 2
            int r1 = parseInt(r14, r1, r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2 = 59
            if (r1 <= r2) goto L_0x0097
            r2 = 63
            if (r1 >= r2) goto L_0x0097
            r1 = 59
        L_0x0097:
            r2 = 46
            boolean r2 = checkOffset(r14, r6, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r2 == 0) goto L_0x021c
            int r2 = r6 + 1
            int r0 = r2 + 1
            int r6 = indexOfNonDigit(r14, r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r0 = r2 + 3
            int r3 = java.lang.Math.min(r6, r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r0 = parseInt(r14, r2, r3)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r2 = r3 - r2
            switch(r2) {
                case 1: goto L_0x0125;
                case 2: goto L_0x0122;
                default: goto L_0x00b6;
            }     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
        L_0x00b6:
            r2 = r0
            r3 = r1
        L_0x00b8:
            int r0 = r14.length()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r0 > r6) goto L_0x0128
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r1 = "No time zone indicator"
            r0.<init>(r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            throw r0     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
        L_0x00c6:
            r0 = move-exception
            r2 = r0
        L_0x00c8:
            if (r14 != 0) goto L_0x0205
            r0 = 0
        L_0x00cb:
            java.lang.String r1 = r2.getMessage()
            if (r1 == 0) goto L_0x00d7
            boolean r3 = r1.isEmpty()
            if (r3 == 0) goto L_0x00f8
        L_0x00d7:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r3 = "("
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.Class r3 = r2.getClass()
            java.lang.String r3 = r3.getName()
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.String r3 = ")"
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.String r1 = r1.toString()
        L_0x00f8:
            java.text.ParseException r3 = new java.text.ParseException
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.lang.String r5 = "Failed to parse date "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r0 = r4.append(r0)
            java.lang.String r4 = ": "
            java.lang.StringBuilder r0 = r0.append(r4)
            java.lang.StringBuilder r0 = r0.append(r1)
            java.lang.String r0 = r0.toString()
            int r1 = r15.getIndex()
            r3.<init>(r0, r1)
            r3.initCause(r2)
            throw r3
        L_0x0122:
            int r0 = r0 * 10
            goto L_0x00b6
        L_0x0125:
            int r0 = r0 * 100
            goto L_0x00b6
        L_0x0128:
            char r0 = r14.charAt(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r1 = 90
            if (r0 != r1) goto L_0x0168
            java.util.TimeZone r0 = TIMEZONE_Z     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r1 = r6 + 1
        L_0x0134:
            java.util.GregorianCalendar r6 = new java.util.GregorianCalendar     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r6.<init>(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 0
            r6.setLenient(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 1
            r6.set(r0, r7)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 2
            int r7 = r8 + -1
            r6.set(r0, r7)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 5
            r6.set(r0, r9)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 11
            r6.set(r0, r5)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 12
            r6.set(r0, r4)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 13
            r6.set(r0, r3)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r0 = 14
            r6.set(r0, r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r15.setIndex(r1)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.util.Date r0 = r6.getTime()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            goto L_0x004f
        L_0x0168:
            if (r0 == r11) goto L_0x016c
            if (r0 != r10) goto L_0x01e2
        L_0x016c:
            java.lang.String r0 = r14.substring(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r1 = r0.length()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            int r1 = r1 + r6
            java.lang.String r6 = "+0000"
            boolean r6 = r6.equals(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r6 != 0) goto L_0x0185
            java.lang.String r6 = "+00:00"
            boolean r6 = r6.equals(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r6 == 0) goto L_0x0188
        L_0x0185:
            java.util.TimeZone r0 = TIMEZONE_Z     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            goto L_0x0134
        L_0x0188:
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r6.<init>()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r10 = "GMT"
            java.lang.StringBuilder r6 = r6.append(r10)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r0 = r6.append(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r6 = r0.toString()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.util.TimeZone r0 = java.util.TimeZone.getTimeZone(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r10 = r0.getID()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            boolean r11 = r10.equals(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r11 != 0) goto L_0x0134
            java.lang.String r11 = ":"
            java.lang.String r12 = ""
            java.lang.String r10 = r10.replace(r11, r12)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            boolean r10 = r10.equals(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            if (r10 != 0) goto L_0x0134
            java.lang.IndexOutOfBoundsException r1 = new java.lang.IndexOutOfBoundsException     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2.<init>()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r3 = "Mismatching time zone indicator: "
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r2 = r2.append(r6)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r3 = " given, resolves to "
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r0 = r0.getID()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r0 = r2.append(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r0 = r0.toString()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r1.<init>(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            throw r1     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
        L_0x01de:
            r0 = move-exception
            r2 = r0
            goto L_0x00c8
        L_0x01e2:
            java.lang.IndexOutOfBoundsException r1 = new java.lang.IndexOutOfBoundsException     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r2.<init>()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r3 = "Invalid time zone indicator '"
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.StringBuilder r0 = r2.append(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r2 = "'"
            java.lang.StringBuilder r0 = r0.append(r2)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            java.lang.String r0 = r0.toString()     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            r1.<init>(r0)     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
            throw r1     // Catch:{ IndexOutOfBoundsException -> 0x00c6, NumberFormatException -> 0x01de, IllegalArgumentException -> 0x0201 }
        L_0x0201:
            r0 = move-exception
            r2 = r0
            goto L_0x00c8
        L_0x0205:
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.StringBuilder r0 = r0.append(r13)
            java.lang.StringBuilder r0 = r0.append(r14)
            java.lang.StringBuilder r0 = r0.append(r13)
            java.lang.String r0 = r0.toString()
            goto L_0x00cb
        L_0x021c:
            r2 = r0
            r3 = r1
            goto L_0x00b8
        L_0x0220:
            r2 = r0
            r3 = r0
            r6 = r1
            goto L_0x00b8
        L_0x0225:
            r2 = r1
            goto L_0x0065
        L_0x0228:
            r2 = r0
            r3 = r0
            r4 = r0
            r5 = r0
            r6 = r1
            goto L_0x00b8
        L_0x022f:
            r2 = r1
            goto L_0x002d
        L_0x0232:
            r2 = r1
            goto L_0x001c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.util.ISO8601Utils.parse(java.lang.String, java.text.ParsePosition):java.util.Date");
    }

    private static boolean checkOffset(String str, int i, char c) {
        return i < str.length() && str.charAt(i) == c;
    }

    private static int parseInt(String str, int i, int i2) throws NumberFormatException {
        int i3;
        if (i < 0 || i2 > str.length() || i > i2) {
            throw new NumberFormatException(str);
        }
        int i4 = 0;
        if (i < i2) {
            i3 = i + 1;
            int digit = Character.digit(str.charAt(i), 10);
            if (digit < 0) {
                throw new NumberFormatException("Invalid number: " + str.substring(i, i2));
            }
            i4 = -digit;
        } else {
            i3 = i;
        }
        while (i3 < i2) {
            int i5 = i3 + 1;
            int digit2 = Character.digit(str.charAt(i3), 10);
            if (digit2 < 0) {
                throw new NumberFormatException("Invalid number: " + str.substring(i, i2));
            }
            i4 = (i4 * 10) - digit2;
            i3 = i5;
        }
        return -i4;
    }

    private static void padInt(StringBuilder sb, int i, int i2) {
        String num = Integer.toString(i);
        for (int length = i2 - num.length(); length > 0; length--) {
            sb.append('0');
        }
        sb.append(num);
    }

    private static int indexOfNonDigit(String str, int i) {
        while (i < str.length()) {
            char charAt = str.charAt(i);
            if (charAt < '0' || charAt > '9') {
                return i;
            }
            i++;
        }
        return str.length();
    }

    public static void main(String[] strArr) {
        while (true) {
            long currentTimeMillis = System.currentTimeMillis();
            long currentTimeMillis2 = System.currentTimeMillis() - currentTimeMillis;
            System.out.println("Pow (" + test1(250000, 3) + ") -> " + currentTimeMillis2 + " ms");
            long currentTimeMillis3 = System.currentTimeMillis();
            long currentTimeMillis4 = System.currentTimeMillis() - currentTimeMillis3;
            System.out.println("Iter (" + test2(250000, 3) + ") -> " + currentTimeMillis4 + " ms");
        }
    }

    static int test1(int i, int i2) {
        int i3 = 3;
        while (true) {
            i--;
            if (i < 0) {
                return i3;
            }
            i3 = (int) Math.pow(10.0d, (double) i2);
        }
    }

    static int test2(int i, int i2) {
        int i3 = 3;
        while (true) {
            i--;
            if (i < 0) {
                return i3;
            }
            i3 = 10;
            int i4 = i2;
            while (true) {
                i4--;
                if (i4 > 0) {
                    i3 *= 10;
                }
            }
        }
    }
}

package org.apache.commons.lang3.time;

import java.text.ParseException;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Iterator;
import java.util.Locale;
import java.util.NoSuchElementException;
import java.util.concurrent.TimeUnit;

public class DateUtils {
    public static final long MILLIS_PER_DAY = 86400000;
    public static final long MILLIS_PER_HOUR = 3600000;
    public static final long MILLIS_PER_MINUTE = 60000;
    public static final long MILLIS_PER_SECOND = 1000;
    public static final int RANGE_MONTH_MONDAY = 6;
    public static final int RANGE_MONTH_SUNDAY = 5;
    public static final int RANGE_WEEK_CENTER = 4;
    public static final int RANGE_WEEK_MONDAY = 2;
    public static final int RANGE_WEEK_RELATIVE = 3;
    public static final int RANGE_WEEK_SUNDAY = 1;
    public static final int SEMI_MONTH = 1001;
    private static final int[][] fields = {new int[]{14}, new int[]{13}, new int[]{12}, new int[]{11, 10}, new int[]{5, 5, 9}, new int[]{2, 1001}, new int[]{1}, new int[]{0}};

    static class DateIterator implements Iterator<Calendar> {
        private final Calendar endFinal;
        private final Calendar spot;

        DateIterator(Calendar calendar, Calendar calendar2) {
            this.endFinal = calendar2;
            this.spot = calendar;
            this.spot.add(5, -1);
        }

        public boolean hasNext() {
            return this.spot.before(this.endFinal);
        }

        public Calendar next() {
            if (this.spot.equals(this.endFinal)) {
                throw new NoSuchElementException();
            }
            this.spot.add(5, 1);
            return (Calendar) this.spot.clone();
        }

        public void remove() {
            throw new UnsupportedOperationException();
        }
    }

    private enum ModifyType {
        TRUNCATE,
        ROUND,
        CEILING
    }

    public static boolean isSameDay(Date date, Date date2) {
        if (date == null || date2 == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        Calendar instance2 = Calendar.getInstance();
        instance2.setTime(date2);
        return isSameDay(instance, instance2);
    }

    public static boolean isSameDay(Calendar calendar, Calendar calendar2) {
        if (calendar == null || calendar2 == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (calendar.get(0) == calendar2.get(0) && calendar.get(1) == calendar2.get(1) && calendar.get(6) == calendar2.get(6)) {
            return true;
        } else {
            return false;
        }
    }

    public static boolean isSameInstant(Date date, Date date2) {
        if (date != null && date2 != null) {
            return date.getTime() == date2.getTime();
        }
        throw new IllegalArgumentException("The date must not be null");
    }

    public static boolean isSameInstant(Calendar calendar, Calendar calendar2) {
        if (calendar != null && calendar2 != null) {
            return calendar.getTime().getTime() == calendar2.getTime().getTime();
        }
        throw new IllegalArgumentException("The date must not be null");
    }

    public static boolean isSameLocalTime(Calendar calendar, Calendar calendar2) {
        if (calendar == null || calendar2 == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (calendar.get(14) == calendar2.get(14) && calendar.get(13) == calendar2.get(13) && calendar.get(12) == calendar2.get(12) && calendar.get(11) == calendar2.get(11) && calendar.get(6) == calendar2.get(6) && calendar.get(1) == calendar2.get(1) && calendar.get(0) == calendar2.get(0) && calendar.getClass() == calendar2.getClass()) {
            return true;
        } else {
            return false;
        }
    }

    public static Date parseDate(String str, String... strArr) throws ParseException {
        return parseDate(str, null, strArr);
    }

    public static Date parseDate(String str, Locale locale, String... strArr) throws ParseException {
        return parseDateWithLeniency(str, locale, strArr, true);
    }

    public static Date parseDateStrictly(String str, String... strArr) throws ParseException {
        return parseDateStrictly(str, null, strArr);
    }

    public static Date parseDateStrictly(String str, Locale locale, String... strArr) throws ParseException {
        return parseDateWithLeniency(str, null, strArr, false);
    }

    private static Date parseDateWithLeniency(String str, Locale locale, String[] strArr, boolean z) throws ParseException {
        SimpleDateFormat simpleDateFormat;
        String str2;
        String str3;
        if (str == null || strArr == null) {
            throw new IllegalArgumentException("Date and Patterns must not be null");
        }
        if (locale == null) {
            simpleDateFormat = new SimpleDateFormat();
        } else {
            simpleDateFormat = new SimpleDateFormat("", locale);
        }
        simpleDateFormat.setLenient(z);
        ParsePosition parsePosition = new ParsePosition(0);
        for (String str4 : strArr) {
            if (str4.endsWith("ZZ")) {
                str2 = str4.substring(0, str4.length() - 1);
            } else {
                str2 = str4;
            }
            simpleDateFormat.applyPattern(str2);
            parsePosition.setIndex(0);
            if (str4.endsWith("ZZ")) {
                str3 = str.replaceAll("([-+][0-9][0-9]):([0-9][0-9])$", "$1$2");
            } else {
                str3 = str;
            }
            Date parse = simpleDateFormat.parse(str3, parsePosition);
            if (parse != null && parsePosition.getIndex() == str3.length()) {
                return parse;
            }
        }
        throw new ParseException("Unable to parse the date: " + str, -1);
    }

    public static Date addYears(Date date, int i) {
        return add(date, 1, i);
    }

    public static Date addMonths(Date date, int i) {
        return add(date, 2, i);
    }

    public static Date addWeeks(Date date, int i) {
        return add(date, 3, i);
    }

    public static Date addDays(Date date, int i) {
        return add(date, 5, i);
    }

    public static Date addHours(Date date, int i) {
        return add(date, 11, i);
    }

    public static Date addMinutes(Date date, int i) {
        return add(date, 12, i);
    }

    public static Date addSeconds(Date date, int i) {
        return add(date, 13, i);
    }

    public static Date addMilliseconds(Date date, int i) {
        return add(date, 14, i);
    }

    private static Date add(Date date, int i, int i2) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        instance.add(i, i2);
        return instance.getTime();
    }

    public static Date setYears(Date date, int i) {
        return set(date, 1, i);
    }

    public static Date setMonths(Date date, int i) {
        return set(date, 2, i);
    }

    public static Date setDays(Date date, int i) {
        return set(date, 5, i);
    }

    public static Date setHours(Date date, int i) {
        return set(date, 11, i);
    }

    public static Date setMinutes(Date date, int i) {
        return set(date, 12, i);
    }

    public static Date setSeconds(Date date, int i) {
        return set(date, 13, i);
    }

    public static Date setMilliseconds(Date date, int i) {
        return set(date, 14, i);
    }

    private static Date set(Date date, int i, int i2) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setLenient(false);
        instance.setTime(date);
        instance.set(i, i2);
        return instance.getTime();
    }

    public static Calendar toCalendar(Date date) {
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        return instance;
    }

    public static Date round(Date date, int i) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        modify(instance, i, ModifyType.ROUND);
        return instance.getTime();
    }

    public static Calendar round(Calendar calendar, int i) {
        if (calendar == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar calendar2 = (Calendar) calendar.clone();
        modify(calendar2, i, ModifyType.ROUND);
        return calendar2;
    }

    public static Date round(Object obj, int i) {
        if (obj == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (obj instanceof Date) {
            return round((Date) obj, i);
        } else {
            if (obj instanceof Calendar) {
                return round((Calendar) obj, i).getTime();
            }
            throw new ClassCastException("Could not round " + obj);
        }
    }

    public static Date truncate(Date date, int i) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        modify(instance, i, ModifyType.TRUNCATE);
        return instance.getTime();
    }

    public static Calendar truncate(Calendar calendar, int i) {
        if (calendar == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar calendar2 = (Calendar) calendar.clone();
        modify(calendar2, i, ModifyType.TRUNCATE);
        return calendar2;
    }

    public static Date truncate(Object obj, int i) {
        if (obj == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (obj instanceof Date) {
            return truncate((Date) obj, i);
        } else {
            if (obj instanceof Calendar) {
                return truncate((Calendar) obj, i).getTime();
            }
            throw new ClassCastException("Could not truncate " + obj);
        }
    }

    public static Date ceiling(Date date, int i) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        modify(instance, i, ModifyType.CEILING);
        return instance.getTime();
    }

    public static Calendar ceiling(Calendar calendar, int i) {
        if (calendar == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar calendar2 = (Calendar) calendar.clone();
        modify(calendar2, i, ModifyType.CEILING);
        return calendar2;
    }

    public static Date ceiling(Object obj, int i) {
        if (obj == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (obj instanceof Date) {
            return ceiling((Date) obj, i);
        } else {
            if (obj instanceof Calendar) {
                return ceiling((Calendar) obj, i).getTime();
            }
            throw new ClassCastException("Could not find ceiling of for type: " + obj.getClass());
        }
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static void modify(java.util.Calendar r13, int r14, org.apache.commons.lang3.time.DateUtils.ModifyType r15) {
        /*
            r12 = 12
            r11 = 11
            r10 = 5
            r1 = 1
            r2 = 0
            int r0 = r13.get(r1)
            r3 = 280000000(0x10b07600, float:6.960157E-29)
            if (r0 <= r3) goto L_0x0018
            java.lang.ArithmeticException r0 = new java.lang.ArithmeticException
            java.lang.String r1 = "Calendar value too large for accurate calculations"
            r0.<init>(r1)
            throw r0
        L_0x0018:
            r0 = 14
            if (r14 != r0) goto L_0x001d
        L_0x001c:
            return
        L_0x001d:
            java.util.Date r3 = r13.getTime()
            long r4 = r3.getTime()
            r0 = 14
            int r0 = r13.get(r0)
            org.apache.commons.lang3.time.DateUtils$ModifyType r6 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE
            if (r6 == r15) goto L_0x0033
            r6 = 500(0x1f4, float:7.0E-43)
            if (r0 >= r6) goto L_0x0035
        L_0x0033:
            long r6 = (long) r0
            long r4 = r4 - r6
        L_0x0035:
            r0 = 13
            if (r14 != r0) goto L_0x0150
            r0 = r1
        L_0x003a:
            r6 = 13
            int r6 = r13.get(r6)
            if (r0 != 0) goto L_0x004f
            org.apache.commons.lang3.time.DateUtils$ModifyType r7 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE
            if (r7 == r15) goto L_0x004a
            r7 = 30
            if (r6 >= r7) goto L_0x004f
        L_0x004a:
            long r6 = (long) r6
            r8 = 1000(0x3e8, double:4.94E-321)
            long r6 = r6 * r8
            long r4 = r4 - r6
        L_0x004f:
            if (r14 != r12) goto L_0x0052
            r0 = r1
        L_0x0052:
            int r6 = r13.get(r12)
            if (r0 != 0) goto L_0x0066
            org.apache.commons.lang3.time.DateUtils$ModifyType r0 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE
            if (r0 == r15) goto L_0x0060
            r0 = 30
            if (r6 >= r0) goto L_0x0066
        L_0x0060:
            long r6 = (long) r6
            r8 = 60000(0xea60, double:2.9644E-319)
            long r6 = r6 * r8
            long r4 = r4 - r6
        L_0x0066:
            long r6 = r3.getTime()
            int r0 = (r6 > r4 ? 1 : (r6 == r4 ? 0 : -1))
            if (r0 == 0) goto L_0x0074
            r3.setTime(r4)
            r13.setTime(r3)
        L_0x0074:
            int[][] r6 = fields
            int r7 = r6.length
            r5 = r2
            r0 = r2
        L_0x0079:
            if (r5 >= r7) goto L_0x0131
            r8 = r6[r5]
            int r4 = r8.length
            r3 = r2
        L_0x007f:
            if (r3 >= r4) goto L_0x00ce
            r9 = r8[r3]
            if (r9 != r14) goto L_0x00cb
            org.apache.commons.lang3.time.DateUtils$ModifyType r3 = org.apache.commons.lang3.time.DateUtils.ModifyType.CEILING
            if (r15 == r3) goto L_0x008f
            org.apache.commons.lang3.time.DateUtils$ModifyType r3 = org.apache.commons.lang3.time.DateUtils.ModifyType.ROUND
            if (r15 != r3) goto L_0x001c
            if (r0 == 0) goto L_0x001c
        L_0x008f:
            r0 = 1001(0x3e9, float:1.403E-42)
            if (r14 != r0) goto L_0x00ab
            int r0 = r13.get(r10)
            if (r0 != r1) goto L_0x00a0
            r0 = 15
            r13.add(r10, r0)
            goto L_0x001c
        L_0x00a0:
            r0 = -15
            r13.add(r10, r0)
            r0 = 2
            r13.add(r0, r1)
            goto L_0x001c
        L_0x00ab:
            r0 = 9
            if (r14 != r0) goto L_0x00c4
            int r0 = r13.get(r11)
            if (r0 != 0) goto L_0x00ba
            r13.add(r11, r12)
            goto L_0x001c
        L_0x00ba:
            r0 = -12
            r13.add(r11, r0)
            r13.add(r10, r1)
            goto L_0x001c
        L_0x00c4:
            r0 = r8[r2]
            r13.add(r0, r1)
            goto L_0x001c
        L_0x00cb:
            int r3 = r3 + 1
            goto L_0x007f
        L_0x00ce:
            switch(r14) {
                case 9: goto L_0x011b;
                case 1001: goto L_0x0103;
                default: goto L_0x00d1;
            }
        L_0x00d1:
            r4 = r2
            r3 = r2
        L_0x00d3:
            if (r4 != 0) goto L_0x00ef
            r0 = r8[r2]
            int r0 = r13.getActualMinimum(r0)
            r3 = r8[r2]
            int r4 = r13.getActualMaximum(r3)
            r3 = r8[r2]
            int r3 = r13.get(r3)
            int r3 = r3 - r0
            int r0 = r4 - r0
            int r0 = r0 / 2
            if (r3 <= r0) goto L_0x012f
            r0 = r1
        L_0x00ef:
            if (r3 == 0) goto L_0x00fe
            r4 = r8[r2]
            r8 = r8[r2]
            int r8 = r13.get(r8)
            int r3 = r8 - r3
            r13.set(r4, r3)
        L_0x00fe:
            int r3 = r5 + 1
            r5 = r3
            goto L_0x0079
        L_0x0103:
            r3 = r8[r2]
            if (r3 != r10) goto L_0x00d1
            int r0 = r13.get(r10)
            int r3 = r0 + -1
            r0 = 15
            if (r3 < r0) goto L_0x0113
            int r3 = r3 + -15
        L_0x0113:
            r0 = 7
            if (r3 <= r0) goto L_0x0119
            r0 = r1
        L_0x0117:
            r4 = r1
            goto L_0x00d3
        L_0x0119:
            r0 = r2
            goto L_0x0117
        L_0x011b:
            r3 = r8[r2]
            if (r3 != r11) goto L_0x00d1
            int r3 = r13.get(r11)
            if (r3 < r12) goto L_0x0127
            int r3 = r3 + -12
        L_0x0127:
            r0 = 6
            if (r3 < r0) goto L_0x012d
            r0 = r1
        L_0x012b:
            r4 = r1
            goto L_0x00d3
        L_0x012d:
            r0 = r2
            goto L_0x012b
        L_0x012f:
            r0 = r2
            goto L_0x00ef
        L_0x0131:
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "The field "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r14)
            java.lang.String r2 = " is not supported"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0150:
            r0 = r2
            goto L_0x003a
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.time.DateUtils.modify(java.util.Calendar, int, org.apache.commons.lang3.time.DateUtils$ModifyType):void");
    }

    public static Iterator<Calendar> iterator(Date date, int i) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        return iterator(instance, i);
    }

    public static Iterator<Calendar> iterator(Calendar calendar, int i) {
        Calendar truncate;
        int i2;
        Calendar calendar2;
        int i3;
        int i4;
        int i5;
        int i6 = 2;
        if (calendar == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        switch (i) {
            case 1:
            case 2:
            case 3:
            case 4:
                truncate = truncate(calendar, 5);
                Calendar truncate2 = truncate(calendar, 5);
                switch (i) {
                    case 1:
                        i2 = 7;
                        i6 = 1;
                        calendar2 = truncate2;
                        break;
                    case 2:
                        i2 = 1;
                        calendar2 = truncate2;
                        break;
                    case 3:
                        i6 = calendar.get(7);
                        i2 = i6 - 1;
                        calendar2 = truncate2;
                        break;
                    case 4:
                        i6 = calendar.get(7) - 3;
                        i2 = calendar.get(7) + 3;
                        calendar2 = truncate2;
                        break;
                    default:
                        i2 = 7;
                        i6 = 1;
                        calendar2 = truncate2;
                        break;
                }
            case 5:
            case 6:
                truncate = truncate(calendar, 2);
                Calendar calendar3 = (Calendar) truncate.clone();
                calendar3.add(2, 1);
                calendar3.add(5, -1);
                if (i != 6) {
                    i2 = 7;
                    i6 = 1;
                    calendar2 = calendar3;
                    break;
                } else {
                    i2 = 1;
                    calendar2 = calendar3;
                    break;
                }
            default:
                throw new IllegalArgumentException("The range style " + i + " is not valid.");
        }
        if (i6 < 1) {
            i3 = i6 + 7;
        } else {
            i3 = i6;
        }
        if (i3 > 7) {
            i4 = i3 - 7;
        } else {
            i4 = i3;
        }
        if (i2 < 1) {
            i5 = i2 + 7;
        } else {
            i5 = i2;
        }
        if (i5 > 7) {
            i5 -= 7;
        }
        while (truncate.get(7) != i4) {
            truncate.add(5, -1);
        }
        while (calendar2.get(7) != i5) {
            calendar2.add(5, 1);
        }
        return new DateIterator(truncate, calendar2);
    }

    public static Iterator<?> iterator(Object obj, int i) {
        if (obj == null) {
            throw new IllegalArgumentException("The date must not be null");
        } else if (obj instanceof Date) {
            return iterator((Date) obj, i);
        } else {
            if (obj instanceof Calendar) {
                return iterator((Calendar) obj, i);
            }
            throw new ClassCastException("Could not iterate based on " + obj);
        }
    }

    public static long getFragmentInMilliseconds(Date date, int i) {
        return getFragment(date, i, TimeUnit.MILLISECONDS);
    }

    public static long getFragmentInSeconds(Date date, int i) {
        return getFragment(date, i, TimeUnit.SECONDS);
    }

    public static long getFragmentInMinutes(Date date, int i) {
        return getFragment(date, i, TimeUnit.MINUTES);
    }

    public static long getFragmentInHours(Date date, int i) {
        return getFragment(date, i, TimeUnit.HOURS);
    }

    public static long getFragmentInDays(Date date, int i) {
        return getFragment(date, i, TimeUnit.DAYS);
    }

    public static long getFragmentInMilliseconds(Calendar calendar, int i) {
        return getFragment(calendar, i, TimeUnit.MILLISECONDS);
    }

    public static long getFragmentInSeconds(Calendar calendar, int i) {
        return getFragment(calendar, i, TimeUnit.SECONDS);
    }

    public static long getFragmentInMinutes(Calendar calendar, int i) {
        return getFragment(calendar, i, TimeUnit.MINUTES);
    }

    public static long getFragmentInHours(Calendar calendar, int i) {
        return getFragment(calendar, i, TimeUnit.HOURS);
    }

    public static long getFragmentInDays(Calendar calendar, int i) {
        return getFragment(calendar, i, TimeUnit.DAYS);
    }

    private static long getFragment(Date date, int i, TimeUnit timeUnit) {
        if (date == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        return getFragment(instance, i, timeUnit);
    }

    /* JADX WARNING: Code restructure failed: missing block: B:14:0x0066, code lost:
        r0 = r0 + r7.convert((long) r5.get(12), java.util.concurrent.TimeUnit.MINUTES);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0074, code lost:
        r0 = r0 + r7.convert((long) r5.get(13), java.util.concurrent.TimeUnit.SECONDS);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:?, code lost:
        return r0 + r7.convert((long) r5.get(14), java.util.concurrent.TimeUnit.MILLISECONDS);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static long getFragment(java.util.Calendar r5, int r6, java.util.concurrent.TimeUnit r7) {
        /*
            if (r5 != 0) goto L_0x000a
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "The date must not be null"
            r0.<init>(r1)
            throw r0
        L_0x000a:
            r0 = 0
            java.util.concurrent.TimeUnit r2 = java.util.concurrent.TimeUnit.DAYS
            if (r7 != r2) goto L_0x0036
            r2 = 0
        L_0x0011:
            switch(r6) {
                case 1: goto L_0x0038;
                case 2: goto L_0x0048;
                default: goto L_0x0014;
            }
        L_0x0014:
            switch(r6) {
                case 1: goto L_0x0058;
                case 2: goto L_0x0058;
                case 3: goto L_0x0017;
                case 4: goto L_0x0017;
                case 5: goto L_0x0058;
                case 6: goto L_0x0058;
                case 7: goto L_0x0017;
                case 8: goto L_0x0017;
                case 9: goto L_0x0017;
                case 10: goto L_0x0017;
                case 11: goto L_0x0066;
                case 12: goto L_0x0074;
                case 13: goto L_0x0082;
                case 14: goto L_0x0090;
                default: goto L_0x0017;
            }
        L_0x0017:
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "The fragment "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r6)
            java.lang.String r2 = " is not supported"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0036:
            r2 = 1
            goto L_0x0011
        L_0x0038:
            r3 = 6
            int r3 = r5.get(r3)
            int r2 = r3 - r2
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.DAYS
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
            goto L_0x0014
        L_0x0048:
            r3 = 5
            int r3 = r5.get(r3)
            int r2 = r3 - r2
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.DAYS
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
            goto L_0x0014
        L_0x0058:
            r2 = 11
            int r2 = r5.get(r2)
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.HOURS
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
        L_0x0066:
            r2 = 12
            int r2 = r5.get(r2)
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.MINUTES
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
        L_0x0074:
            r2 = 13
            int r2 = r5.get(r2)
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.SECONDS
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
        L_0x0082:
            r2 = 14
            int r2 = r5.get(r2)
            long r2 = (long) r2
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.MILLISECONDS
            long r2 = r7.convert(r2, r4)
            long r0 = r0 + r2
        L_0x0090:
            return r0
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.time.DateUtils.getFragment(java.util.Calendar, int, java.util.concurrent.TimeUnit):long");
    }

    public static boolean truncatedEquals(Calendar calendar, Calendar calendar2, int i) {
        return truncatedCompareTo(calendar, calendar2, i) == 0;
    }

    public static boolean truncatedEquals(Date date, Date date2, int i) {
        return truncatedCompareTo(date, date2, i) == 0;
    }

    public static int truncatedCompareTo(Calendar calendar, Calendar calendar2, int i) {
        return truncate(calendar, i).compareTo(truncate(calendar2, i));
    }

    public static int truncatedCompareTo(Date date, Date date2, int i) {
        return truncate(date, i).compareTo(truncate(date2, i));
    }
}

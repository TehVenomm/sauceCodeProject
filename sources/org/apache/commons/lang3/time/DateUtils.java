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
    private static final int[][] fields;

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

    static {
        r0 = new int[8][];
        r0[0] = new int[]{14};
        r0[1] = new int[]{13};
        r0[2] = new int[]{12};
        r0[3] = new int[]{11, 10};
        r0[4] = new int[]{5, 5, 9};
        r0[5] = new int[]{2, 1001};
        r0[6] = new int[]{1};
        r0[7] = new int[]{0};
        fields = r0;
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
        } else {
            throw new IllegalArgumentException("The date must not be null");
        }
    }

    public static boolean isSameInstant(Calendar calendar, Calendar calendar2) {
        if (calendar != null && calendar2 != null) {
            return calendar.getTime().getTime() == calendar2.getTime().getTime();
        } else {
            throw new IllegalArgumentException("The date must not be null");
        }
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
        if (str == null || strArr == null) {
            throw new IllegalArgumentException("Date and Patterns must not be null");
        }
        SimpleDateFormat simpleDateFormat;
        if (locale == null) {
            simpleDateFormat = new SimpleDateFormat();
        } else {
            simpleDateFormat = new SimpleDateFormat("", locale);
        }
        simpleDateFormat.setLenient(z);
        ParsePosition parsePosition = new ParsePosition(0);
        for (String str2 : strArr) {
            String substring;
            if (str2.endsWith("ZZ")) {
                substring = str2.substring(0, str2.length() - 1);
            } else {
                substring = str2;
            }
            simpleDateFormat.applyPattern(substring);
            parsePosition.setIndex(0);
            if (str2.endsWith("ZZ")) {
                substring = str.replaceAll("([-+][0-9][0-9]):([0-9][0-9])$", "$1$2");
            } else {
                substring = str;
            }
            Date parse = simpleDateFormat.parse(substring, parsePosition);
            if (parse != null && parsePosition.getIndex() == substring.length()) {
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

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static void modify(java.util.Calendar r11, int r12, org.apache.commons.lang3.time.DateUtils.ModifyType r13) {
        /*
        r0 = 1;
        r0 = r11.get(r0);
        r1 = 280000000; // 0x10b07600 float:6.960157E-29 double:1.38338381E-315;
        if (r0 <= r1) goto L_0x0012;
    L_0x000a:
        r0 = new java.lang.ArithmeticException;
        r1 = "Calendar value too large for accurate calculations";
        r0.<init>(r1);
        throw r0;
    L_0x0012:
        r0 = 14;
        if (r12 != r0) goto L_0x0017;
    L_0x0016:
        return;
    L_0x0017:
        r4 = r11.getTime();
        r2 = r4.getTime();
        r0 = 0;
        r1 = 14;
        r1 = r11.get(r1);
        r5 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE;
        if (r5 == r13) goto L_0x002e;
    L_0x002a:
        r5 = 500; // 0x1f4 float:7.0E-43 double:2.47E-321;
        if (r1 >= r5) goto L_0x0030;
    L_0x002e:
        r6 = (long) r1;
        r2 = r2 - r6;
    L_0x0030:
        r1 = 13;
        if (r12 != r1) goto L_0x0035;
    L_0x0034:
        r0 = 1;
    L_0x0035:
        r1 = 13;
        r1 = r11.get(r1);
        if (r0 != 0) goto L_0x004a;
    L_0x003d:
        r5 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE;
        if (r5 == r13) goto L_0x0045;
    L_0x0041:
        r5 = 30;
        if (r1 >= r5) goto L_0x004a;
    L_0x0045:
        r6 = (long) r1;
        r8 = 1000; // 0x3e8 float:1.401E-42 double:4.94E-321;
        r6 = r6 * r8;
        r2 = r2 - r6;
    L_0x004a:
        r1 = 12;
        if (r12 != r1) goto L_0x004f;
    L_0x004e:
        r0 = 1;
    L_0x004f:
        r1 = 12;
        r1 = r11.get(r1);
        if (r0 != 0) goto L_0x0181;
    L_0x0057:
        r0 = org.apache.commons.lang3.time.DateUtils.ModifyType.TRUNCATE;
        if (r0 == r13) goto L_0x005f;
    L_0x005b:
        r0 = 30;
        if (r1 >= r0) goto L_0x0181;
    L_0x005f:
        r0 = (long) r1;
        r6 = 60000; // 0xea60 float:8.4078E-41 double:2.9644E-319;
        r0 = r0 * r6;
        r0 = r2 - r0;
    L_0x0066:
        r2 = r4.getTime();
        r2 = (r2 > r0 ? 1 : (r2 == r0 ? 0 : -1));
        if (r2 == 0) goto L_0x0074;
    L_0x006e:
        r4.setTime(r0);
        r11.setTime(r4);
    L_0x0074:
        r2 = 0;
        r4 = fields;
        r5 = r4.length;
        r0 = 0;
        r3 = r0;
    L_0x007a:
        if (r3 >= r5) goto L_0x0162;
    L_0x007c:
        r6 = r4[r3];
        r1 = r6.length;
        r0 = 0;
    L_0x0080:
        if (r0 >= r1) goto L_0x00e0;
    L_0x0082:
        r7 = r6[r0];
        if (r7 != r12) goto L_0x00dd;
    L_0x0086:
        r0 = org.apache.commons.lang3.time.DateUtils.ModifyType.CEILING;
        if (r13 == r0) goto L_0x0090;
    L_0x008a:
        r0 = org.apache.commons.lang3.time.DateUtils.ModifyType.ROUND;
        if (r13 != r0) goto L_0x0016;
    L_0x008e:
        if (r2 == 0) goto L_0x0016;
    L_0x0090:
        r0 = 1001; // 0x3e9 float:1.403E-42 double:4.946E-321;
        if (r12 != r0) goto L_0x00b1;
    L_0x0094:
        r0 = 5;
        r0 = r11.get(r0);
        r1 = 1;
        if (r0 != r1) goto L_0x00a4;
    L_0x009c:
        r0 = 5;
        r1 = 15;
        r11.add(r0, r1);
        goto L_0x0016;
    L_0x00a4:
        r0 = 5;
        r1 = -15;
        r11.add(r0, r1);
        r0 = 2;
        r1 = 1;
        r11.add(r0, r1);
        goto L_0x0016;
    L_0x00b1:
        r0 = 9;
        if (r12 != r0) goto L_0x00d4;
    L_0x00b5:
        r0 = 11;
        r0 = r11.get(r0);
        if (r0 != 0) goto L_0x00c6;
    L_0x00bd:
        r0 = 11;
        r1 = 12;
        r11.add(r0, r1);
        goto L_0x0016;
    L_0x00c6:
        r0 = 11;
        r1 = -12;
        r11.add(r0, r1);
        r0 = 5;
        r1 = 1;
        r11.add(r0, r1);
        goto L_0x0016;
    L_0x00d4:
        r0 = 0;
        r0 = r6[r0];
        r1 = 1;
        r11.add(r0, r1);
        goto L_0x0016;
    L_0x00dd:
        r0 = r0 + 1;
        goto L_0x0080;
    L_0x00e0:
        r1 = 0;
        r0 = 0;
        switch(r12) {
            case 9: goto L_0x0141;
            case 1001: goto L_0x0122;
            default: goto L_0x00e5;
        };
    L_0x00e5:
        r10 = r0;
        r0 = r1;
        r1 = r2;
        r2 = r10;
    L_0x00e9:
        if (r2 != 0) goto L_0x010b;
    L_0x00eb:
        r0 = 0;
        r0 = r6[r0];
        r0 = r11.getActualMinimum(r0);
        r1 = 0;
        r1 = r6[r1];
        r2 = r11.getActualMaximum(r1);
        r1 = 0;
        r1 = r6[r1];
        r1 = r11.get(r1);
        r1 = r1 - r0;
        r0 = r2 - r0;
        r0 = r0 / 2;
        if (r1 <= r0) goto L_0x0160;
    L_0x0107:
        r0 = 1;
    L_0x0108:
        r10 = r1;
        r1 = r0;
        r0 = r10;
    L_0x010b:
        if (r0 == 0) goto L_0x011c;
    L_0x010d:
        r2 = 0;
        r2 = r6[r2];
        r7 = 0;
        r6 = r6[r7];
        r6 = r11.get(r6);
        r0 = r6 - r0;
        r11.set(r2, r0);
    L_0x011c:
        r0 = r3 + 1;
        r3 = r0;
        r2 = r1;
        goto L_0x007a;
    L_0x0122:
        r7 = 0;
        r7 = r6[r7];
        r8 = 5;
        if (r7 != r8) goto L_0x00e5;
    L_0x0128:
        r0 = 5;
        r0 = r11.get(r0);
        r2 = r0 + -1;
        r0 = 15;
        if (r2 < r0) goto L_0x0135;
    L_0x0133:
        r2 = r2 + -15;
    L_0x0135:
        r0 = 7;
        if (r2 <= r0) goto L_0x013f;
    L_0x0138:
        r0 = 1;
    L_0x0139:
        r1 = 1;
        r10 = r1;
        r1 = r0;
        r0 = r2;
        r2 = r10;
        goto L_0x00e9;
    L_0x013f:
        r0 = 0;
        goto L_0x0139;
    L_0x0141:
        r7 = 0;
        r7 = r6[r7];
        r8 = 11;
        if (r7 != r8) goto L_0x00e5;
    L_0x0148:
        r0 = 11;
        r2 = r11.get(r0);
        r0 = 12;
        if (r2 < r0) goto L_0x0154;
    L_0x0152:
        r2 = r2 + -12;
    L_0x0154:
        r0 = 6;
        if (r2 < r0) goto L_0x015e;
    L_0x0157:
        r0 = 1;
    L_0x0158:
        r1 = 1;
        r10 = r1;
        r1 = r0;
        r0 = r2;
        r2 = r10;
        goto L_0x00e9;
    L_0x015e:
        r0 = 0;
        goto L_0x0158;
    L_0x0160:
        r0 = 0;
        goto L_0x0108;
    L_0x0162:
        r0 = new java.lang.IllegalArgumentException;
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = "The field ";
        r1 = r1.append(r2);
        r1 = r1.append(r12);
        r2 = " is not supported";
        r1 = r1.append(r2);
        r1 = r1.toString();
        r0.<init>(r1);
        throw r0;
    L_0x0181:
        r0 = r2;
        goto L_0x0066;
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
        int i2 = 2;
        if (calendar == null) {
            throw new IllegalArgumentException("The date must not be null");
        }
        Calendar truncate;
        Calendar truncate2;
        int i3;
        switch (i) {
            case 1:
            case 2:
            case 3:
            case 4:
                truncate = truncate(calendar, 5);
                truncate2 = truncate(calendar, 5);
                switch (i) {
                    case 1:
                        i3 = 7;
                        i2 = 1;
                        break;
                    case 2:
                        i3 = 1;
                        break;
                    case 3:
                        i2 = calendar.get(7);
                        i3 = i2 - 1;
                        break;
                    case 4:
                        i2 = calendar.get(7) - 3;
                        i3 = calendar.get(7) + 3;
                        break;
                    default:
                        i3 = 7;
                        i2 = 1;
                        break;
                }
            case 5:
            case 6:
                truncate2 = truncate(calendar, 2);
                Calendar calendar2 = (Calendar) truncate2.clone();
                calendar2.add(2, 1);
                calendar2.add(5, -1);
                if (i != 6) {
                    i2 = 1;
                    truncate = truncate2;
                    truncate2 = calendar2;
                    i3 = 7;
                    break;
                }
                truncate = truncate2;
                truncate2 = calendar2;
                i3 = 1;
                break;
            default:
                throw new IllegalArgumentException("The range style " + i + " is not valid.");
        }
        if (i2 < 1) {
            i2 += 7;
        }
        if (i2 > 7) {
            i2 -= 7;
        }
        if (i3 < 1) {
            i3 += 7;
        }
        if (i3 > 7) {
            i3 -= 7;
        }
        while (truncate.get(7) != i2) {
            truncate.add(5, -1);
        }
        while (truncate2.get(7) != i3) {
            truncate2.add(5, 1);
        }
        return new DateIterator(truncate, truncate2);
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

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static long getFragment(java.util.Calendar r5, int r6, java.util.concurrent.TimeUnit r7) {
        /*
        if (r5 != 0) goto L_0x000a;
    L_0x0002:
        r0 = new java.lang.IllegalArgumentException;
        r1 = "The date must not be null";
        r0.<init>(r1);
        throw r0;
    L_0x000a:
        r0 = 0;
        r2 = java.util.concurrent.TimeUnit.DAYS;
        if (r7 != r2) goto L_0x0036;
    L_0x0010:
        r2 = 0;
    L_0x0011:
        switch(r6) {
            case 1: goto L_0x0038;
            case 2: goto L_0x0048;
            default: goto L_0x0014;
        };
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
        };
    L_0x0017:
        r0 = new java.lang.IllegalArgumentException;
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = "The fragment ";
        r1 = r1.append(r2);
        r1 = r1.append(r6);
        r2 = " is not supported";
        r1 = r1.append(r2);
        r1 = r1.toString();
        r0.<init>(r1);
        throw r0;
    L_0x0036:
        r2 = 1;
        goto L_0x0011;
    L_0x0038:
        r3 = 6;
        r3 = r5.get(r3);
        r2 = r3 - r2;
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.DAYS;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
        goto L_0x0014;
    L_0x0048:
        r3 = 5;
        r3 = r5.get(r3);
        r2 = r3 - r2;
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.DAYS;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
        goto L_0x0014;
    L_0x0058:
        r2 = 11;
        r2 = r5.get(r2);
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.HOURS;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
    L_0x0066:
        r2 = 12;
        r2 = r5.get(r2);
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.MINUTES;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
    L_0x0074:
        r2 = 13;
        r2 = r5.get(r2);
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.SECONDS;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
    L_0x0082:
        r2 = 14;
        r2 = r5.get(r2);
        r2 = (long) r2;
        r4 = java.util.concurrent.TimeUnit.MILLISECONDS;
        r2 = r7.convert(r2, r4);
        r0 = r0 + r2;
    L_0x0090:
        return r0;
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

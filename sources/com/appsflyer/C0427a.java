package com.appsflyer;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import android.support.annotation.Nullable;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/* renamed from: com.appsflyer.a */
final class C0427a {

    /* renamed from: ˊ */
    private static int f231 = 0;

    /* renamed from: ˎ */
    private static char[] f232 = {'a', 50912, 36216, 21464, 6743, 57519, 42800, 28108, 13316, 64155, 49632, 34943, 20184, 5470, 56235, 41532, 26777, 12096, 62888, 48367, 33652, 18883, 4164, 54954, 40255, 25520, 10773, 61623, 47081, 32376, 17605, 2901, 53669, 38972, '1', 55369, 'y', 50935, 36197, 21459, 6677, 57483, 42777, 28111, 13332, 64154, 49619, 34898, 20192, 5467, 56233, 41505, 26771, 12084, 26621, 41328, 60129, 13392, 32198, 34620, 49403, 2632, 21462, 40197, 42534, 61366, 10582, 29387, 48226, 50686, 3915, 18583, 37463, 56176, 58547, 11868, 30623, 45421, 64171, 1144, 19906, 38659, 53285, 6585, 8970, 27804, 15838, 64351, 45255, 28263, 10216, 56592, 39567, 20595, 2464, 50994, 64541, 46567, 29558, 10493, 58895, 40840, 21805, 4776, 51214, 33108, 48841, 29816, 11756, 60184, 41117, 8855, 58390, 44942, 28974, 14497, 49753, 34246, 20282, 5870, 55401, 58120, 43656, 27689, 14241, 63808, 32961, 19000, 3531, 55151, 40466, 41373, 27455, 12976, 'a', 50912, 36216, 21464, 6743, 57519, 42800, 28108, 13343, 64141, 49570, 34907, 20187, 5455, 56234, 41521, 26804, 12047, 62863, 48353, 'a', 50912, 36216, 21464, 6743, 57519, 42800, 28108, 13342, 64155, 49656, 34868, 20221, 5444, 56237, 204, 45159, 30363, 'j', 50927, 36202, 21451, 6678, 57519, 42811, 28108, 13316, 64147, 49660, 34942, 20161, 5444, '/', 50925, 36221, 21449, 6736, 57507, 14891, 64673, 46902, 27123, 8270, 56041, 40313, 22473, 3679, 49362, 16985, 34044, 53091, 4563, 22601, 41647, 58683, 12181, 30255, 47260, 33781, 51813, 3266, 22360, 39351, 57383, 10900, 'F', 50927, 36213, 21446, 6749, 57506, 42868, 28054, 13343, 64222, 49643, 34943, 20188, 5398, 56231, 41523, 26755, 12038, 62873, 48298, 33640, 18887, 4160, 54954, 40304, 25513, 10757, 61582, 47072, 32310, 17601, 2890, 53667, 38955, 24236, 9502, 60305, 45801, 31098, 16280, 1552, 31470, 48226, 63457, 10602, 24799, 39484, 56745, 5898, 20117, 32795, 47968, 62199, 13408, 28623, 41277, 55479, 4608, 21892, 36628, 50807, 63992, 13120, 27347, 44088, 'C', 50918, 36217, 21449, 6739, 57525, 42785, 28047, 13346, 64155, 49642, 34934, 20173, 5461, 56240, 41495, 26776, 12045, 62873, 48378, 33644, 18895, 4187, 54956, 'F', 50927, 36213, 21446, 6749, 57506, 42868, 28043, 13342, 64136, 49635, 34929, 20173, 5398, 56246, 41527, 26758, 12034, 62873, 48361, 33644, 18883, 4176, 55010, 40253, 25531, 10776, 61586, 47079, 32370, 17540, 2885, 53673, 38970, 24244, 9546, 60317, 45822, 31095, 16327, 1600, 52426, 37669, 22965, 8198, 59084, 44452};

    /* renamed from: ˏ */
    private static int f233 = 1;

    /* renamed from: ॱ */
    private static long f234 = -9114525830008617330L;

    C0427a() {
    }

    @Nullable
    /* renamed from: ˋ */
    static String m268(Context context, long j) {
        String intern;
        String intern2;
        String intern3;
        String intern4;
        String intern5;
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        if (m272(m270(0, 34, 0).intern())) {
            int i = f231 + 89;
            f233 = i % 128;
            if (i % 2 == 0) {
            }
            intern = m270(34, 1, 0).intern();
        } else {
            intern = m270(35, 1, 55417).intern();
        }
        sb2.append(intern);
        StringBuilder sb4 = new StringBuilder();
        String packageName = context.getPackageName();
        String r0 = m269(packageName);
        sb2.append(m270(34, 1, 0).intern());
        sb4.append(r0);
        switch (m265(context) == null) {
            case false:
                sb2.append(m270(34, 1, 0).intern());
                sb4.append(packageName);
                break;
            default:
                sb2.append(m270(35, 1, 55417).intern());
                sb4.append(packageName);
                break;
        }
        String r02 = m267(context);
        if (r02 == null) {
            sb2.append(m270(35, 1, 55417).intern());
            sb4.append(packageName);
        } else {
            sb2.append(m270(34, 1, 0).intern());
            sb4.append(r02);
        }
        sb4.append(m266(context, packageName));
        sb.append(sb4.toString());
        try {
            long j2 = context.getPackageManager().getPackageInfo(context.getPackageName(), 0).firstInstallTime;
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat(m270(36, 18, 0).intern(), Locale.US);
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            sb.append(simpleDateFormat.format(new Date(j2)));
            int i2 = f231 + 45;
            f233 = i2 % 128;
            if (i2 % 2 == 0) {
            }
            sb.append(j);
            if (m272(m270(86, 25, 15807).intern())) {
                intern2 = m270(34, 1, 0).intern();
            } else {
                intern2 = m270(35, 1, 55417).intern();
                int i3 = f233 + 115;
                f231 = i3 % 128;
                if (i3 % 2 != 0) {
                }
            }
            sb3.append(intern2);
            switch (m272(m270(111, 23, 8950).intern())) {
                case false:
                    intern3 = m270(35, 1, 55417).intern();
                    break;
                default:
                    intern3 = m270(34, 1, 0).intern();
                    break;
            }
            sb3.append(intern3);
            if (m272(m270(134, 20, 0).intern())) {
                int i4 = f233 + 9;
                f231 = i4 % 128;
                if (i4 % 2 != 0) {
                }
                intern4 = m270(34, 1, 0).intern();
            } else {
                intern4 = m270(35, 1, 55417).intern();
                int i5 = f231 + 73;
                f233 = i5 % 128;
                if (i5 % 2 == 0) {
                }
            }
            sb3.append(intern4);
            switch (m272(m270(154, 15, 0).intern()) ? 24 : 'Z') {
                case 'Z':
                    intern5 = m270(35, 1, 55417).intern();
                    break;
                default:
                    intern5 = m270(34, 1, 0).intern();
                    break;
            }
            sb3.append(intern5);
            String r03 = C0459r.m340(C0459r.m338(sb.toString()));
            String obj = sb2.toString();
            StringBuilder sb5 = new StringBuilder(r03);
            sb5.setCharAt(17, Integer.toString(Integer.parseInt(obj, 2), 16).charAt(0));
            String obj2 = sb5.toString();
            String obj3 = sb3.toString();
            StringBuilder sb6 = new StringBuilder(obj2);
            sb6.setCharAt(27, Integer.toString(Integer.parseInt(obj3, 2), 16).charAt(0));
            return m271(sb6.toString(), Long.valueOf(j));
        } catch (NameNotFoundException e) {
            return m270(54, 32, 26527).intern();
        }
    }

    /* renamed from: ॱ */
    private static String m271(String str, Long l) {
        int i = f231 + 29;
        f233 = i % 128;
        if (i % 2 == 0) {
        }
        switch (str != null ? '!' : 'I') {
            case '!':
                if (l != null) {
                    int i2 = f231 + 67;
                    f233 = i2 % 128;
                    if (i2 % 2 != 0 ? str.length() == 32 : str.length() == 115) {
                        StringBuilder sb = new StringBuilder(str);
                        String obj = l.toString();
                        int i3 = 0;
                        int i4 = 0;
                        while (true) {
                            switch (i3 < obj.length() ? 'G' : '\\') {
                                case 'G':
                                    i4 += Character.getNumericValue(obj.charAt(i3));
                                    i3++;
                                default:
                                    String hexString = Integer.toHexString(i4);
                                    sb.replace(7, hexString.length() + 7, hexString);
                                    long j = 0;
                                    int i5 = 0;
                                    while (i5 < sb.length()) {
                                        long numericValue = ((long) Character.getNumericValue(sb.charAt(i5))) + j;
                                        int i6 = i5 + 1;
                                        int i7 = f233 + 111;
                                        f231 = i7 % 128;
                                        if (i7 % 2 != 0) {
                                        }
                                        j = numericValue;
                                        i5 = i6;
                                    }
                                    while (j > 100) {
                                        j %= 100;
                                    }
                                    sb.insert(23, (int) j);
                                    if (j < 10) {
                                        sb.insert(23, m270(35, 1, 55417).intern());
                                    }
                                    return sb.toString();
                            }
                        }
                    }
                }
                break;
        }
        return m270(54, 32, 26527).intern();
    }

    /* renamed from: ॱ */
    private static boolean m272(String str) {
        int i = f231 + 79;
        f233 = i % 128;
        switch (i % 2 == 0 ? 'G' : 9) {
            case 'G':
                Class.forName(str);
                return false;
            default:
                try {
                    Class.forName(str);
                    return true;
                } catch (ClassNotFoundException e) {
                    return false;
                }
        }
    }

    /* renamed from: ˋ */
    private static String m269(String str) {
        switch (!str.contains(m270(169, 1, 226).intern()) ? (char) 26 : 14) {
            case 14:
                String[] split = str.split(m270(170, 2, 45115).intern());
                int length = split.length;
                StringBuilder sb = new StringBuilder();
                sb.append(split[length - 1]).append(m270(169, 1, 226).intern());
                int i = 1;
                while (i < length - 1) {
                    int i2 = f231 + 115;
                    f233 = i2 % 128;
                    switch (i2 % 2 == 0 ? '6' : 'G') {
                        case 'G':
                            sb.append(split[i]).append(m270(169, 1, 226).intern());
                            i++;
                            break;
                        default:
                            sb.append(split[i]).append(m270(19127, 1, 226).intern());
                            i += 98;
                            break;
                    }
                }
                sb.append(split[0]);
                return sb.toString();
            default:
                int i3 = f233 + 25;
                f231 = i3 % 128;
                if (i3 % 2 != 0) {
                }
                return str;
        }
    }

    /* renamed from: ˊ */
    private static String m265(Context context) {
        boolean z = true;
        String str = null;
        int i = f231 + 113;
        f233 = i % 128;
        if (i % 2 != 0) {
            switch (System.getProperties().containsKey(m270(172, 14, 0).intern()) ? 'C' : 'Y') {
                case 'Y':
                    return str;
            }
        } else if (!System.getProperties().containsKey(m270(79, 122, 0).intern())) {
            return str;
        }
        try {
            Matcher matcher = Pattern.compile(m270(192, 10, 14853).intern()).matcher(context.getCacheDir().getPath().replace(m270(186, 6, 0).intern(), ""));
            if (!matcher.find()) {
                return str;
            }
            int i2 = f233 + 23;
            f231 = i2 % 128;
            if (i2 % 2 != 0) {
                z = false;
            }
            switch (z) {
                case true:
                    return matcher.group(1);
                default:
                    return matcher.group(0);
            }
            C0469y.m373().mo6642(m270(202, 17, 16922).intern(), new StringBuilder().append(m270(219, 41, 0).intern()).append(e).toString());
            return str;
        } catch (Exception e) {
            C0469y.m373().mo6642(m270(202, 17, 16922).intern(), new StringBuilder().append(m270(219, 41, 0).intern()).append(e).toString());
            return str;
        }
    }

    /* renamed from: ˋ */
    private static String m267(Context context) {
        boolean z = false;
        int i = f233 + 121;
        f231 = i % 128;
        if (i % 2 != 0) {
        }
        try {
            String str = context.getPackageManager().getPackageInfo(context.getPackageName(), 0).packageName;
            int i2 = f231 + 119;
            f233 = i2 % 128;
            if (i2 % 2 == 0) {
                z = true;
            }
            switch (z) {
            }
            return str;
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:5:0x0049 A[Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }, LOOP_START] */
    /* renamed from: ˊ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.String m266(android.content.Context r11, java.lang.String r12) {
        /*
            r10 = 284(0x11c, float:3.98E-43)
            r9 = 47
            r2 = 1
            r8 = 24
            r1 = 0
            int r0 = f231
            int r0 = r0 + 113
            int r3 = r0 % 128
            f233 = r3
            int r0 = r0 % 2
            if (r0 != 0) goto L_0x0014
        L_0x0014:
            android.content.pm.PackageManager r0 = r11.getPackageManager()
            java.lang.Class<android.content.pm.PackageManager> r3 = android.content.pm.PackageManager.class
            r4 = 260(0x104, float:3.64E-43)
            r5 = 24
            r6 = 31369(0x7a89, float:4.3957E-41)
            java.lang.String r4 = m270(r4, r5, r6)     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.String r4 = r4.intern()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r5 = 1
            java.lang.Class[] r5 = new java.lang.Class[r5]     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r6 = 0
            java.lang.Class r7 = java.lang.Integer.TYPE     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r5[r6] = r7     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.reflect.Method r3 = r3.getDeclaredMethod(r4, r5)     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r4 = 1
            java.lang.Object[] r4 = new java.lang.Object[r4]     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r5 = 0
            r6 = 0
            java.lang.Integer r6 = java.lang.Integer.valueOf(r6)     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r4[r5] = r6     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.Object r0 = r3.invoke(r0, r4)     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.util.List r0 = (java.util.List) r0     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.util.Iterator r3 = r0.iterator()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
        L_0x0049:
            boolean r0 = r3.hasNext()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            if (r0 == 0) goto L_0x0114
            r0 = r1
        L_0x0050:
            switch(r0) {
                case 0: goto L_0x005b;
                default: goto L_0x0053;
            }
        L_0x0053:
            java.lang.Boolean r0 = java.lang.Boolean.FALSE
            java.lang.String r0 = r0.toString()
        L_0x005a:
            return r0
        L_0x005b:
            java.lang.Object r0 = r3.next()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            android.content.pm.ApplicationInfo r0 = (android.content.pm.ApplicationInfo) r0     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.String r0 = r0.packageName     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            boolean r0 = r0.equals(r12)     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            if (r0 == 0) goto L_0x0117
            r0 = 15
        L_0x006b:
            switch(r0) {
                case 15: goto L_0x006f;
                default: goto L_0x006e;
            }
        L_0x006e:
            goto L_0x0049
        L_0x006f:
            int r0 = f233
            int r0 = r0 + 125
            int r2 = r0 % 128
            f231 = r2
            int r0 = r0 % 2
            if (r0 == 0) goto L_0x0087
            java.lang.Boolean r0 = java.lang.Boolean.TRUE     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.String r0 = r0.toString()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            r2 = 74
            int r1 = r2 / 0
        L_0x0085:
            goto L_0x005a
        L_0x0087:
            java.lang.Boolean r0 = java.lang.Boolean.TRUE     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            java.lang.String r0 = r0.toString()     // Catch:{ IllegalAccessException -> 0x008e, NoSuchMethodException -> 0x00ba, InvocationTargetException -> 0x00e7 }
            goto L_0x0085
        L_0x008e:
            r0 = move-exception
            com.appsflyer.y r2 = com.appsflyer.C0469y.m373()
            java.lang.String r3 = m270(r10, r8, r1)
            java.lang.String r3 = r3.intern()
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            r5 = 308(0x134, float:4.32E-43)
            java.lang.String r1 = m270(r5, r9, r1)
            java.lang.String r1 = r1.intern()
            java.lang.StringBuilder r1 = r4.append(r1)
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r0 = r0.toString()
            r2.mo6642(r3, r0)
            goto L_0x0053
        L_0x00ba:
            r0 = move-exception
            com.appsflyer.y r2 = com.appsflyer.C0469y.m373()
            java.lang.String r3 = m270(r10, r8, r1)
            java.lang.String r3 = r3.intern()
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            r5 = 308(0x134, float:4.32E-43)
            java.lang.String r1 = m270(r5, r9, r1)
            java.lang.String r1 = r1.intern()
            java.lang.StringBuilder r1 = r4.append(r1)
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r0 = r0.toString()
            r2.mo6642(r3, r0)
            goto L_0x0053
        L_0x00e7:
            r0 = move-exception
            com.appsflyer.y r2 = com.appsflyer.C0469y.m373()
            java.lang.String r3 = m270(r10, r8, r1)
            java.lang.String r3 = r3.intern()
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            r5 = 308(0x134, float:4.32E-43)
            java.lang.String r1 = m270(r5, r9, r1)
            java.lang.String r1 = r1.intern()
            java.lang.StringBuilder r1 = r4.append(r1)
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r0 = r0.toString()
            r2.mo6642(r3, r0)
            goto L_0x0053
        L_0x0114:
            r0 = r2
            goto L_0x0050
        L_0x0117:
            r0 = 93
            goto L_0x006b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0427a.m266(android.content.Context, java.lang.String):java.lang.String");
    }

    /* renamed from: ॱ */
    private static String m270(int i, int i2, char c) {
        int i3 = f233 + 57;
        f231 = i3 % 128;
        if (i3 % 2 != 0) {
        }
        char[] cArr = new char[i2];
        int i4 = 0;
        while (true) {
            switch (i4 < i2 ? 3 : '1') {
                case 3:
                    int i5 = f233 + 95;
                    f231 = i5 % 128;
                    if (i5 % 2 != 0) {
                    }
                    cArr[i4] = (char) ((int) ((((long) f232[i + i4]) ^ (((long) i4) * f234)) ^ ((long) c)));
                    i4++;
                default:
                    return new String(cArr);
            }
        }
    }
}

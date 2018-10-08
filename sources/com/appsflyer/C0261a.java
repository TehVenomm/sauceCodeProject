package com.appsflyer;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.support.annotation.Nullable;
import java.lang.reflect.InvocationTargetException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.TimeZone;
import jp.colopl.util.ImageUtil;

/* renamed from: com.appsflyer.a */
final class C0261a {
    /* renamed from: ˊ */
    private static int f210 = 0;
    /* renamed from: ˎ */
    private static char[] f211 = new char[]{'a', '웠', '赸', '变', 'ᩗ', '', 'ꜰ', '淌', '㐄', '瀞', '쇠', '衿', '付', 'ᕞ', '?', 'ꈼ', '梙', '⽀', '', '볯', '荴', '䧃', '၄', '횪', '鴿', '掰', '⨕', '', '럩', '繸', '䓅', '୕', '톥', '頼', '1', '?', 'y', '웷', '赥', '叓', 'ᨕ', '', 'ꜙ', '淏', '㐔', '漢', '쇓', '衒', '仠', 'ᕛ', '?', 'ꈡ', '梓', '⼴', '柽', 'ꅰ', '', '㑐', '緆', '蜼', '샻', 'ੈ', '取', '鴅', '꘦', '', '⥖', '狋', '뱢', '엾', 'ཋ', '䢗', '鉗', '?', '', '⹜', '瞟', '녭', '磌', 'Ѹ', '䷂', '霃', '퀥', 'ᦹ', '⌊', '沜', '㷞', 'ﭟ', '냇', '湧', '⟨', '?', '骏', '偳', 'ঠ', '윲', 'ﰝ', '뗧', '獶', '⣽', '', '龈', '唭', 'ከ', '젎', '腔', '뻉', '瑸', 'ⷬ', '', 'ꂝ', '⊗', '', '꾎', '焮', '㢡', '쉙', '藆', '伺', 'ᛮ', '?', '', 'ꪈ', '氩', '㞡', '鹿', '胁', '䨸', '෋', '흯', '鸒', 'ꆝ', '欿', '㊰', 'a', '웠', '赸', '变', 'ᩗ', '', 'ꜰ', '淌', '㐟', '揄', '솢', '衛', '仛', 'ᕏ', '?', 'ꈱ', '梴', '⼏', '', '볡', 'a', '웠', '赸', '变', 'ᩗ', '', 'ꜰ', '淌', '㐞', '瀞', '쇸', '蠴', '份', 'ᕄ', '?', 'Ì', '끧', '皛', 'j', '웯', '赪', '友', 'ᨖ', '', 'ꜻ', '淌', '㐄', '望', '쇼', '衾', '仁', 'ᕄ', '/', '웭', '赽', '叉', 'ᩐ', '', '㨫', 'ﲡ', '뜶', '槳', '⁎', '?', '鵹', '埉', '๟', '샒', '䉙', '蓼', '콣', 'ᇓ', '塉', 'ꊯ', '', '⾕', '瘯', '뢜', '菵', '쩥', 'ೂ', '坘', '馷', '', '⪔', 'F', '웯', '赵', '叆', 'ᩝ', '', 'ꝴ', '涖', '㐟', '﫞', '쇫', '衿', '仜', 'ᔖ', '?', 'ꈳ', '梃', '⼆', '', '벪', '荨', '䧇', '၀', '횪', '鵰', '掩', '⨅', '', '럠', '縶', '䓁', '୊', '톣', '頫', '庬', '┞', '', '닩', '祺', '㾘', 'ؐ', '竮', '뱢', '', '⥪', '惟', '騼', '?', 'ᜊ', '井', '耛', '뭠', '', '㑠', '濏', 'ꄽ', '?', 'ሀ', '善', '輔', '왷', '笠', '㍀', '櫓', '갸', 'C', '웦', '赹', '叉', 'ᩓ', '', '꜡', '涏', '㐢', '瀞', '쇪', '衶', '仍', 'ᕕ', '?', 'ꈗ', '梘', '⼍', '', '볺', '荬', '䧏', 'ၛ', '횬', 'F', '웯', '赵', '叆', 'ᩝ', '', 'ꝴ', '涋', '㐞', '愈', '쇣', '衱', '仍', 'ᔖ', '?', 'ꈷ', '梆', '⼂', '', '볩', '荬', '䧃', 'ၐ', '훢', '鴽', '掻', '⨘', '', '럧', '繲', '䒄', '୅', '톩', '頺', '庴', '╊', '', '닾', '祷', '㿇', 'ـ', '쳊', '錥', '妵', ' ', '', '궤'};
    /* renamed from: ˏ */
    private static int f212 = 1;
    /* renamed from: ॱ */
    private static long f213 = -9114525830008617330L;

    C0261a() {
    }

    @Nullable
    /* renamed from: ˋ */
    static String m277(Context context, long j) {
        String intern;
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        StringBuilder stringBuilder3 = new StringBuilder();
        if (C0261a.m281(C0261a.m279(0, 34, '\u0000').intern())) {
            int i = f210 + 89;
            f212 = i % 128;
            intern = i % 2 == 0 ? C0261a.m279(34, 1, '\u0000').intern() : C0261a.m279(34, 1, '\u0000').intern();
        } else {
            intern = C0261a.m279(35, 1, '?').intern();
        }
        stringBuilder2.append(intern);
        StringBuilder stringBuilder4 = new StringBuilder();
        String packageName = context.getPackageName();
        intern = C0261a.m278(packageName);
        stringBuilder2.append(C0261a.m279(34, 1, '\u0000').intern());
        stringBuilder4.append(intern);
        if (C0261a.m274(context) == null) {
            i = 1;
        } else {
            char c = '\u0000';
        }
        switch (i) {
            case 0:
                stringBuilder2.append(C0261a.m279(34, 1, '\u0000').intern());
                stringBuilder4.append(packageName);
                break;
            default:
                stringBuilder2.append(C0261a.m279(35, 1, '?').intern());
                stringBuilder4.append(packageName);
                break;
        }
        intern = C0261a.m276(context);
        if (intern == null) {
            stringBuilder2.append(C0261a.m279(35, 1, '?').intern());
            stringBuilder4.append(packageName);
        } else {
            stringBuilder2.append(C0261a.m279(34, 1, '\u0000').intern());
            stringBuilder4.append(intern);
        }
        stringBuilder4.append(C0261a.m275(context, packageName));
        stringBuilder.append(stringBuilder4.toString());
        try {
            int i2;
            long j2 = context.getPackageManager().getPackageInfo(context.getPackageName(), 0).firstInstallTime;
            DateFormat simpleDateFormat = new SimpleDateFormat(C0261a.m279(36, 18, '\u0000').intern(), Locale.US);
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            stringBuilder.append(simpleDateFormat.format(new Date(j2)));
            i = f210 + 45;
            f212 = i % 128;
            if (i % 2 == 0) {
                stringBuilder.append(j);
            } else {
                stringBuilder.append(j);
            }
            if (C0261a.m281(C0261a.m279(86, 25, '㶿').intern())) {
                intern = C0261a.m279(34, 1, '\u0000').intern();
            } else {
                intern = C0261a.m279(35, 1, '?').intern();
                i2 = f212 + 115;
                f210 = i2 % 128;
                if (i2 % 2 != 0) {
                }
            }
            stringBuilder3.append(intern);
            if (C0261a.m281(C0261a.m279(111, 23, '⋶').intern())) {
                i = 1;
            } else {
                c = '\u0000';
            }
            switch (i) {
                case 0:
                    intern = C0261a.m279(35, 1, '?').intern();
                    break;
                default:
                    intern = C0261a.m279(34, 1, '\u0000').intern();
                    break;
            }
            stringBuilder3.append(intern);
            if (C0261a.m281(C0261a.m279(134, 20, '\u0000').intern())) {
                i = f212 + 9;
                f210 = i % 128;
                intern = i % 2 != 0 ? C0261a.m279(34, 1, '\u0000').intern() : C0261a.m279(34, 1, '\u0000').intern();
            } else {
                intern = C0261a.m279(35, 1, '?').intern();
                i2 = f210 + 73;
                f212 = i2 % 128;
                if (i2 % 2 == 0) {
                }
            }
            stringBuilder3.append(intern);
            switch (C0261a.m281(C0261a.m279(154, 15, '\u0000').intern()) ? 24 : 90) {
                case ImageUtil.OUTPUT_QUALITY /*90*/:
                    intern = C0261a.m279(35, 1, '?').intern();
                    break;
                default:
                    intern = C0261a.m279(34, 1, '\u0000').intern();
                    break;
            }
            stringBuilder3.append(intern);
            intern = C0291r.m347(C0291r.m345(stringBuilder.toString()));
            String obj = stringBuilder2.toString();
            stringBuilder = new StringBuilder(intern);
            stringBuilder.setCharAt(17, Integer.toString(Integer.parseInt(obj, 2), 16).charAt(0));
            intern = stringBuilder.toString();
            obj = stringBuilder3.toString();
            stringBuilder = new StringBuilder(intern);
            stringBuilder.setCharAt(27, Integer.toString(Integer.parseInt(obj, 2), 16).charAt(0));
            return C0261a.m280(stringBuilder.toString(), Long.valueOf(j));
        } catch (NameNotFoundException e) {
            return C0261a.m279(54, 32, '枟').intern();
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: ॱ */
    private static java.lang.String m280(java.lang.String r12, java.lang.Long r13) {
        /*
        r10 = 100;
        r3 = 32;
        r8 = 23;
        r1 = 0;
        r0 = f210;
        r0 = r0 + 29;
        r2 = r0 % 128;
        f212 = r2;
        r0 = r0 % 2;
        if (r0 != 0) goto L_0x0013;
    L_0x0013:
        if (r12 == 0) goto L_0x00be;
    L_0x0015:
        r0 = 33;
    L_0x0017:
        switch(r0) {
            case 33: goto L_0x0027;
            default: goto L_0x001a;
        };
    L_0x001a:
        r0 = 54;
        r1 = 26527; // 0x679f float:3.7172E-41 double:1.3106E-319;
        r0 = com.appsflyer.C0261a.m279(r0, r3, r1);
        r0 = r0.intern();
    L_0x0026:
        return r0;
    L_0x0027:
        if (r13 == 0) goto L_0x001a;
    L_0x0029:
        r0 = f210;
        r0 = r0 + 67;
        r2 = r0 % 128;
        f212 = r2;
        r0 = r0 % 2;
        if (r0 != 0) goto L_0x0084;
    L_0x0035:
        r0 = r12.length();
        r2 = 115; // 0x73 float:1.61E-43 double:5.7E-322;
        if (r0 != r2) goto L_0x001a;
    L_0x003d:
        r6 = new java.lang.StringBuilder;
        r6.<init>(r12);
        r7 = r13.toString();
        r4 = 0;
        r0 = r1;
        r2 = r1;
    L_0x004a:
        r3 = r7.length();
        if (r0 >= r3) goto L_0x00c2;
    L_0x0050:
        r3 = 71;
    L_0x0052:
        switch(r3) {
            case 71: goto L_0x008b;
            default: goto L_0x0055;
        };
    L_0x0055:
        r0 = java.lang.Integer.toHexString(r2);
        r2 = 7;
        r3 = r0.length();
        r3 = r3 + 7;
        r6.replace(r2, r3, r0);
        r2 = r1;
        r0 = r4;
    L_0x0065:
        r3 = r6.length();
        if (r2 >= r3) goto L_0x0097;
    L_0x006b:
        r3 = r6.charAt(r2);
        r3 = java.lang.Character.getNumericValue(r3);
        r4 = (long) r3;
        r0 = r0 + r4;
        r2 = r2 + 1;
        r3 = f212;
        r3 = r3 + 111;
        r4 = r3 % 128;
        f210 = r4;
        r3 = r3 % 2;
        if (r3 == 0) goto L_0x0065;
    L_0x0083:
        goto L_0x0065;
    L_0x0084:
        r0 = r12.length();
        if (r0 != r3) goto L_0x001a;
    L_0x008a:
        goto L_0x003d;
    L_0x008b:
        r3 = r7.charAt(r0);
        r3 = java.lang.Character.getNumericValue(r3);
        r2 = r2 + r3;
        r0 = r0 + 1;
        goto L_0x004a;
    L_0x0097:
        r2 = (r0 > r10 ? 1 : (r0 == r10 ? 0 : -1));
        if (r2 <= 0) goto L_0x009d;
    L_0x009b:
        r0 = r0 % r10;
        goto L_0x0097;
    L_0x009d:
        r2 = (int) r0;
        r6.insert(r8, r2);
        r2 = 10;
        r0 = (r0 > r2 ? 1 : (r0 == r2 ? 0 : -1));
        if (r0 >= 0) goto L_0x00b8;
    L_0x00a7:
        r0 = 35;
        r1 = 1;
        r2 = 55417; // 0xd879 float:7.7656E-41 double:2.73796E-319;
        r0 = com.appsflyer.C0261a.m279(r0, r1, r2);
        r0 = r0.intern();
        r6.insert(r8, r0);
    L_0x00b8:
        r0 = r6.toString();
        goto L_0x0026;
    L_0x00be:
        r0 = 73;
        goto L_0x0017;
    L_0x00c2:
        r3 = 92;
        goto L_0x0052;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.a.ॱ(java.lang.String, java.lang.Long):java.lang.String");
    }

    /* renamed from: ॱ */
    private static boolean m281(String str) {
        boolean z = false;
        int i = f210 + 79;
        f212 = i % 128;
        switch (i % 2 == 0 ? 71 : 9) {
            case 71:
                Class.forName(str);
                break;
            default:
                try {
                    Class.forName(str);
                    z = true;
                    break;
                } catch (ClassNotFoundException e) {
                    break;
                }
        }
        return z;
    }

    /* renamed from: ˋ */
    private static String m278(String str) {
        int i;
        switch (!str.contains(C0261a.m279(169, 1, 'â').intern()) ? 26 : 14) {
            case 14:
                String[] split = str.split(C0261a.m279(170, 2, '뀻').intern());
                int length = split.length;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.append(split[length - 1]).append(C0261a.m279(169, 1, 'â').intern());
                int i2 = 1;
                while (i2 < length - 1) {
                    i = f210 + 115;
                    f212 = i % 128;
                    switch (i % 2 == 0 ? 54 : 71) {
                        case 71:
                            stringBuilder.append(split[i2]).append(C0261a.m279(169, 1, 'â').intern());
                            i2++;
                            break;
                        default:
                            stringBuilder.append(split[i2]).append(C0261a.m279(19127, 1, 'â').intern());
                            i2 += 98;
                            break;
                    }
                }
                stringBuilder.append(split[0]);
                return stringBuilder.toString();
            default:
                i = f212 + 25;
                f210 = i % 128;
                return i % 2 != 0 ? str : str;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: ˊ */
    private static java.lang.String m274(android.content.Context r8) {
        /*
        r1 = 1;
        r0 = 0;
        r2 = 0;
        r3 = f210;
        r3 = r3 + 113;
        r4 = r3 % 128;
        f212 = r4;
        r3 = r3 % 2;
        if (r3 != 0) goto L_0x0072;
    L_0x000f:
        r3 = java.lang.System.getProperties();
        r4 = 79;
        r5 = 122; // 0x7a float:1.71E-43 double:6.03E-322;
        r4 = com.appsflyer.C0261a.m279(r4, r5, r2);
        r4 = r4.intern();
        r3 = r3.containsKey(r4);
        if (r3 == 0) goto L_0x0070;
    L_0x0025:
        r3 = r8.getCacheDir();	 Catch:{ Exception -> 0x0094 }
        r3 = r3.getPath();	 Catch:{ Exception -> 0x0094 }
        r4 = 186; // 0xba float:2.6E-43 double:9.2E-322;
        r5 = 6;
        r6 = 0;
        r4 = com.appsflyer.C0261a.m279(r4, r5, r6);	 Catch:{ Exception -> 0x0094 }
        r4 = r4.intern();	 Catch:{ Exception -> 0x0094 }
        r5 = "";
        r3 = r3.replace(r4, r5);	 Catch:{ Exception -> 0x0094 }
        r4 = 192; // 0xc0 float:2.69E-43 double:9.5E-322;
        r5 = 10;
        r6 = 14853; // 0x3a05 float:2.0813E-41 double:7.3384E-320;
        r4 = com.appsflyer.C0261a.m279(r4, r5, r6);	 Catch:{ Exception -> 0x0094 }
        r4 = r4.intern();	 Catch:{ Exception -> 0x0094 }
        r4 = java.util.regex.Pattern.compile(r4);	 Catch:{ Exception -> 0x0094 }
        r3 = r4.matcher(r3);	 Catch:{ Exception -> 0x0094 }
        r4 = r3.find();	 Catch:{ Exception -> 0x0094 }
        if (r4 == 0) goto L_0x0070;
    L_0x005b:
        r4 = f212;
        r4 = r4 + 23;
        r5 = r4 % 128;
        f210 = r5;
        r4 = r4 % 2;
        if (r4 == 0) goto L_0x0068;
    L_0x0067:
        r1 = r2;
    L_0x0068:
        switch(r1) {
            case 1: goto L_0x008e;
            default: goto L_0x006b;
        };
    L_0x006b:
        r1 = 0;
        r0 = r3.group(r1);	 Catch:{ Exception -> 0x0094 }
    L_0x0071:
        return r0;
    L_0x0072:
        r3 = java.lang.System.getProperties();
        r4 = 172; // 0xac float:2.41E-43 double:8.5E-322;
        r5 = 14;
        r4 = com.appsflyer.C0261a.m279(r4, r5, r2);
        r4 = r4.intern();
        r3 = r3.containsKey(r4);
        if (r3 == 0) goto L_0x00c8;
    L_0x0088:
        r3 = 67;
    L_0x008a:
        switch(r3) {
            case 89: goto L_0x0070;
            default: goto L_0x008d;
        };
    L_0x008d:
        goto L_0x0025;
    L_0x008e:
        r1 = 1;
        r0 = r3.group(r1);	 Catch:{ Exception -> 0x0094 }
        goto L_0x0070;
    L_0x0094:
        r1 = move-exception;
        r3 = com.appsflyer.C0300y.m378();
        r4 = 202; // 0xca float:2.83E-43 double:1.0E-321;
        r5 = 17;
        r6 = 16922; // 0x421a float:2.3713E-41 double:8.3606E-320;
        r4 = com.appsflyer.C0261a.m279(r4, r5, r6);
        r4 = r4.intern();
        r5 = new java.lang.StringBuilder;
        r5.<init>();
        r6 = 219; // 0xdb float:3.07E-43 double:1.08E-321;
        r7 = 41;
        r2 = com.appsflyer.C0261a.m279(r6, r7, r2);
        r2 = r2.intern();
        r2 = r5.append(r2);
        r1 = r2.append(r1);
        r1 = r1.toString();
        r3.m388(r4, r1);
        goto L_0x0071;
    L_0x00c8:
        r3 = 89;
        goto L_0x008a;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.a.ˊ(android.content.Context):java.lang.String");
    }

    /* renamed from: ˋ */
    private static String m276(Context context) {
        Object obj = null;
        int i = f212 + 121;
        f210 = i % 128;
        if (i % 2 != 0) {
            try {
            } catch (NameNotFoundException e) {
                return null;
            }
        }
        String str = context.getPackageManager().getPackageInfo(context.getPackageName(), 0).packageName;
        int i2 = f210 + 119;
        f212 = i2 % 128;
        if (i2 % 2 == 0) {
            obj = 1;
        }
        switch (obj) {
        }
        return str;
    }

    /* renamed from: ˊ */
    private static String m275(Context context, String str) {
        int i = f210 + 113;
        f212 = i % 128;
        if (i % 2 == 0) {
        }
        try {
            Iterator it = ((List) PackageManager.class.getDeclaredMethod(C0261a.m279(260, 24, '窉').intern(), new Class[]{Integer.TYPE}).invoke(context.getPackageManager(), new Object[]{Integer.valueOf(0)})).iterator();
            while (true) {
                char c;
                if (it.hasNext()) {
                    c = '\u0000';
                } else {
                    c = '\u0001';
                }
                switch (c) {
                    case '\u0000':
                        switch (((ApplicationInfo) it.next()).packageName.equals(str) ? 15 : 93) {
                            case 15:
                                String obj;
                                i = f212 + 125;
                                f210 = i % 128;
                                if (i % 2 != 0) {
                                    obj = Boolean.TRUE.toString();
                                    int i2 = 74 / 0;
                                } else {
                                    obj = Boolean.TRUE.toString();
                                }
                                return obj;
                            default:
                        }
                    default:
                        break;
                }
                return Boolean.FALSE.toString();
            }
        } catch (IllegalAccessException e) {
            C0300y.m378().m388(C0261a.m279(284, 24, '\u0000').intern(), new StringBuilder().append(C0261a.m279(308, 47, '\u0000').intern()).append(e).toString());
        } catch (NoSuchMethodException e2) {
            C0300y.m378().m388(C0261a.m279(284, 24, '\u0000').intern(), new StringBuilder().append(C0261a.m279(308, 47, '\u0000').intern()).append(e2).toString());
        } catch (InvocationTargetException e3) {
            C0300y.m378().m388(C0261a.m279(284, 24, '\u0000').intern(), new StringBuilder().append(C0261a.m279(308, 47, '\u0000').intern()).append(e3).toString());
        }
    }

    /* renamed from: ॱ */
    private static String m279(int i, int i2, char c) {
        char[] cArr;
        int i3;
        int i4 = f212 + 57;
        f210 = i4 % 128;
        if (i4 % 2 != 0) {
            cArr = new char[i2];
            i3 = 0;
        } else {
            cArr = new char[i2];
            i3 = 0;
        }
        while (true) {
            switch (i3 < i2 ? 3 : 49) {
                case 3:
                    i4 = f212 + 95;
                    f210 = i4 % 128;
                    if (i4 % 2 != 0) {
                        cArr[i3] = (char) ((int) ((((long) f211[i + i3]) ^ (((long) i3) * f213)) ^ ((long) c)));
                        i3++;
                    } else {
                        cArr[i3] = (char) ((int) ((((long) f211[i + i3]) ^ (((long) i3) * f213)) ^ ((long) c)));
                        i3++;
                    }
                default:
                    return new String(cArr);
            }
        }
    }
}

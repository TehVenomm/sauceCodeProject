package com.google.android.gms.auth.api.credentials;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.VisibleForTesting;
import android.support.v4.media.TransportMediator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.security.SecureRandom;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.TreeSet;

public final class PasswordSpecification extends com.google.android.gms.common.internal.safeparcel.zza implements ReflectedParcelable {
    public static final Creator<PasswordSpecification> CREATOR = new zzi();
    public static final PasswordSpecification zzeay = new zza().zzi(12, 16).zzel("abcdefghijkmnopqrstxyzABCDEFGHJKLMNPQRSTXY3456789").zze("abcdefghijkmnopqrstxyz", 1).zze("ABCDEFGHJKLMNPQRSTXY", 1).zze("3456789", 1).zzzy();
    private static PasswordSpecification zzeaz = new zza().zzi(12, 16).zzel("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890").zze("abcdefghijklmnopqrstuvwxyz", 1).zze("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 1).zze("1234567890", 1).zzzy();
    private final Random zzbdm;
    @VisibleForTesting
    private String zzeba;
    @VisibleForTesting
    private List<String> zzebb;
    @VisibleForTesting
    private List<Integer> zzebc;
    @VisibleForTesting
    private int zzebd;
    @VisibleForTesting
    private int zzebe;
    private final int[] zzebf;

    public static final class zza {
        private final List<String> zzebb = new ArrayList();
        private final List<Integer> zzebc = new ArrayList();
        private int zzebd = 12;
        private int zzebe = 16;
        private final TreeSet<Character> zzebg = new TreeSet();

        private static TreeSet<Character> zzq(String str, String str2) {
            if (TextUtils.isEmpty(str)) {
                throw new zzb(String.valueOf(str2).concat(" cannot be null or empty"));
            }
            TreeSet<Character> treeSet = new TreeSet();
            for (char c : str.toCharArray()) {
                if (PasswordSpecification.zzc(c, 32, TransportMediator.KEYCODE_MEDIA_PLAY)) {
                    throw new zzb(String.valueOf(str2).concat(" must only contain ASCII printable characters"));
                }
                treeSet.add(Character.valueOf(c));
            }
            return treeSet;
        }

        public final zza zze(@NonNull String str, int i) {
            this.zzebb.add(PasswordSpecification.zzb(zzq(str, "requiredChars")));
            this.zzebc.add(Integer.valueOf(1));
            return this;
        }

        public final zza zzel(@NonNull String str) {
            this.zzebg.addAll(zzq(str, "allowedChars"));
            return this;
        }

        public final zza zzi(int i, int i2) {
            this.zzebd = 12;
            this.zzebe = 16;
            return this;
        }

        public final PasswordSpecification zzzy() {
            if (this.zzebg.isEmpty()) {
                throw new zzb("no allowed characters specified");
            }
            int i = 0;
            for (Integer intValue : this.zzebc) {
                i = intValue.intValue() + i;
            }
            if (i > this.zzebe) {
                throw new zzb("required character count cannot be greater than the max password size");
            }
            boolean[] zArr = new boolean[95];
            for (String toCharArray : this.zzebb) {
                for (char c : toCharArray.toCharArray()) {
                    if (zArr[c - 32]) {
                        throw new zzb("character " + c + " occurs in more than one required character set");
                    }
                    zArr[c - 32] = true;
                }
            }
            return new PasswordSpecification(PasswordSpecification.zzb(this.zzebg), this.zzebb, this.zzebc, this.zzebd, this.zzebe);
        }
    }

    public static final class zzb extends Error {
        public zzb(String str) {
            super(str);
        }
    }

    PasswordSpecification(String str, List<String> list, List<Integer> list2, int i, int i2) {
        this.zzeba = str;
        this.zzebb = Collections.unmodifiableList(list);
        this.zzebc = Collections.unmodifiableList(list2);
        this.zzebd = i;
        this.zzebe = i2;
        int[] iArr = new int[95];
        Arrays.fill(iArr, -1);
        int i3 = 0;
        for (String toCharArray : this.zzebb) {
            for (char c : toCharArray.toCharArray()) {
                iArr[c - 32] = i3;
            }
            i3++;
        }
        this.zzebf = iArr;
        this.zzbdm = new SecureRandom();
    }

    private static String zzb(Collection<Character> collection) {
        char[] cArr = new char[collection.size()];
        int i = 0;
        for (Character charValue : collection) {
            cArr[i] = charValue.charValue();
            i++;
        }
        return new String(cArr);
    }

    private static boolean zzc(int i, int i2, int i3) {
        return i < 32 || i > TransportMediator.KEYCODE_MEDIA_PLAY;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzeba, false);
        zzd.zzb(parcel, 2, this.zzebb, false);
        zzd.zza(parcel, 3, this.zzebc, false);
        zzd.zzc(parcel, 4, this.zzebd);
        zzd.zzc(parcel, 5, this.zzebe);
        zzd.zzai(parcel, zze);
    }
}

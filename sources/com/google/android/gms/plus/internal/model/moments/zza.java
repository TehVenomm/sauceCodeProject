package com.google.android.gms.plus.internal.model.moments;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.v4.view.MotionEventCompat;
import com.facebook.GraphRequest;
import com.google.android.gms.common.internal.safeparcel.zzb;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class zza implements Creator<ItemScopeEntity> {
    static void zza(ItemScopeEntity itemScopeEntity, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        Set set = itemScopeEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            zzb.zzc(parcel, 1, itemScopeEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzb.zza(parcel, 2, itemScopeEntity.zzazE, i, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzb.zzb(parcel, 3, itemScopeEntity.zzazF, true);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzb.zza(parcel, 4, itemScopeEntity.zzazG, i, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            zzb.zza(parcel, 5, itemScopeEntity.zzazH, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            zzb.zza(parcel, 6, itemScopeEntity.zzazI, true);
        }
        if (set.contains(Integer.valueOf(7))) {
            zzb.zza(parcel, 7, itemScopeEntity.zzazJ, true);
        }
        if (set.contains(Integer.valueOf(8))) {
            zzb.zzc(parcel, 8, itemScopeEntity.zzazK, true);
        }
        if (set.contains(Integer.valueOf(9))) {
            zzb.zzc(parcel, 9, itemScopeEntity.zzazL);
        }
        if (set.contains(Integer.valueOf(10))) {
            zzb.zzc(parcel, 10, itemScopeEntity.zzazM, true);
        }
        if (set.contains(Integer.valueOf(11))) {
            zzb.zza(parcel, 11, itemScopeEntity.zzazN, i, true);
        }
        if (set.contains(Integer.valueOf(12))) {
            zzb.zzc(parcel, 12, itemScopeEntity.zzazO, true);
        }
        if (set.contains(Integer.valueOf(13))) {
            zzb.zza(parcel, 13, itemScopeEntity.zzazP, true);
        }
        if (set.contains(Integer.valueOf(14))) {
            zzb.zza(parcel, 14, itemScopeEntity.zzazQ, true);
        }
        if (set.contains(Integer.valueOf(15))) {
            zzb.zza(parcel, 15, itemScopeEntity.zzazR, i, true);
        }
        if (set.contains(Integer.valueOf(17))) {
            zzb.zza(parcel, 17, itemScopeEntity.zzazT, true);
        }
        if (set.contains(Integer.valueOf(16))) {
            zzb.zza(parcel, 16, itemScopeEntity.zzazS, true);
        }
        if (set.contains(Integer.valueOf(19))) {
            zzb.zzc(parcel, 19, itemScopeEntity.zzazU, true);
        }
        if (set.contains(Integer.valueOf(18))) {
            zzb.zza(parcel, 18, itemScopeEntity.zzql, true);
        }
        if (set.contains(Integer.valueOf(21))) {
            zzb.zza(parcel, 21, itemScopeEntity.zzazW, true);
        }
        if (set.contains(Integer.valueOf(20))) {
            zzb.zza(parcel, 20, itemScopeEntity.zzazV, true);
        }
        if (set.contains(Integer.valueOf(23))) {
            zzb.zza(parcel, 23, itemScopeEntity.zzadH, true);
        }
        if (set.contains(Integer.valueOf(22))) {
            zzb.zza(parcel, 22, itemScopeEntity.zzazX, true);
        }
        if (set.contains(Integer.valueOf(25))) {
            zzb.zza(parcel, 25, itemScopeEntity.zzazZ, true);
        }
        if (set.contains(Integer.valueOf(24))) {
            zzb.zza(parcel, 24, itemScopeEntity.zzazY, true);
        }
        if (set.contains(Integer.valueOf(27))) {
            zzb.zza(parcel, 27, itemScopeEntity.zzaAb, true);
        }
        if (set.contains(Integer.valueOf(26))) {
            zzb.zza(parcel, 26, itemScopeEntity.zzaAa, true);
        }
        if (set.contains(Integer.valueOf(29))) {
            zzb.zza(parcel, 29, itemScopeEntity.zzaAd, i, true);
        }
        if (set.contains(Integer.valueOf(28))) {
            zzb.zza(parcel, 28, itemScopeEntity.zzaAc, true);
        }
        if (set.contains(Integer.valueOf(31))) {
            zzb.zza(parcel, 31, itemScopeEntity.zzaAf, true);
        }
        if (set.contains(Integer.valueOf(30))) {
            zzb.zza(parcel, 30, itemScopeEntity.zzaAe, true);
        }
        if (set.contains(Integer.valueOf(34))) {
            zzb.zza(parcel, 34, itemScopeEntity.zzaAh, i, true);
        }
        if (set.contains(Integer.valueOf(32))) {
            zzb.zza(parcel, 32, itemScopeEntity.zzGM, true);
        }
        if (set.contains(Integer.valueOf(33))) {
            zzb.zza(parcel, 33, itemScopeEntity.zzaAg, true);
        }
        if (set.contains(Integer.valueOf(38))) {
            zzb.zza(parcel, 38, itemScopeEntity.zzapM);
        }
        if (set.contains(Integer.valueOf(39))) {
            zzb.zza(parcel, 39, itemScopeEntity.mName, true);
        }
        if (set.contains(Integer.valueOf(36))) {
            zzb.zza(parcel, 36, itemScopeEntity.zzapL);
        }
        if (set.contains(Integer.valueOf(37))) {
            zzb.zza(parcel, 37, itemScopeEntity.zzaAi, i, true);
        }
        if (set.contains(Integer.valueOf(42))) {
            zzb.zza(parcel, 42, itemScopeEntity.zzaAl, true);
        }
        if (set.contains(Integer.valueOf(43))) {
            zzb.zza(parcel, 43, itemScopeEntity.zzaAm, true);
        }
        if (set.contains(Integer.valueOf(40))) {
            zzb.zza(parcel, 40, itemScopeEntity.zzaAj, i, true);
        }
        if (set.contains(Integer.valueOf(41))) {
            zzb.zzc(parcel, 41, itemScopeEntity.zzaAk, true);
        }
        if (set.contains(Integer.valueOf(46))) {
            zzb.zza(parcel, 46, itemScopeEntity.zzaAp, i, true);
        }
        if (set.contains(Integer.valueOf(47))) {
            zzb.zza(parcel, 47, itemScopeEntity.zzaAq, true);
        }
        if (set.contains(Integer.valueOf(44))) {
            zzb.zza(parcel, 44, itemScopeEntity.zzaAn, true);
        }
        if (set.contains(Integer.valueOf(45))) {
            zzb.zza(parcel, 45, itemScopeEntity.zzaAo, true);
        }
        if (set.contains(Integer.valueOf(51))) {
            zzb.zza(parcel, 51, itemScopeEntity.zzaAu, true);
        }
        if (set.contains(Integer.valueOf(50))) {
            zzb.zza(parcel, 50, itemScopeEntity.zzaAt, i, true);
        }
        if (set.contains(Integer.valueOf(49))) {
            zzb.zza(parcel, 49, itemScopeEntity.zzaAs, true);
        }
        if (set.contains(Integer.valueOf(48))) {
            zzb.zza(parcel, 48, itemScopeEntity.zzaAr, true);
        }
        if (set.contains(Integer.valueOf(55))) {
            zzb.zza(parcel, 55, itemScopeEntity.zzaAw, true);
        }
        if (set.contains(Integer.valueOf(54))) {
            zzb.zza(parcel, 54, itemScopeEntity.zzAX, true);
        }
        if (set.contains(Integer.valueOf(53))) {
            zzb.zza(parcel, 53, itemScopeEntity.zzAV, true);
        }
        if (set.contains(Integer.valueOf(52))) {
            zzb.zza(parcel, 52, itemScopeEntity.zzaAv, true);
        }
        if (set.contains(Integer.valueOf(56))) {
            zzb.zza(parcel, 56, itemScopeEntity.zzaAx, true);
        }
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeS(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhl(i);
    }

    public ItemScopeEntity zzeS(Parcel parcel) {
        int zzL = com.google.android.gms.common.internal.safeparcel.zza.zzL(parcel);
        Set hashSet = new HashSet();
        int i = 0;
        ItemScopeEntity itemScopeEntity = null;
        List list = null;
        ItemScopeEntity itemScopeEntity2 = null;
        String str = null;
        String str2 = null;
        String str3 = null;
        List list2 = null;
        int i2 = 0;
        List list3 = null;
        ItemScopeEntity itemScopeEntity3 = null;
        List list4 = null;
        String str4 = null;
        String str5 = null;
        ItemScopeEntity itemScopeEntity4 = null;
        String str6 = null;
        String str7 = null;
        String str8 = null;
        List list5 = null;
        String str9 = null;
        String str10 = null;
        String str11 = null;
        String str12 = null;
        String str13 = null;
        String str14 = null;
        String str15 = null;
        String str16 = null;
        String str17 = null;
        ItemScopeEntity itemScopeEntity5 = null;
        String str18 = null;
        String str19 = null;
        String str20 = null;
        String str21 = null;
        ItemScopeEntity itemScopeEntity6 = null;
        double d = 0.0d;
        ItemScopeEntity itemScopeEntity7 = null;
        double d2 = 0.0d;
        String str22 = null;
        ItemScopeEntity itemScopeEntity8 = null;
        List list6 = null;
        String str23 = null;
        String str24 = null;
        String str25 = null;
        String str26 = null;
        ItemScopeEntity itemScopeEntity9 = null;
        String str27 = null;
        String str28 = null;
        String str29 = null;
        ItemScopeEntity itemScopeEntity10 = null;
        String str30 = null;
        String str31 = null;
        String str32 = null;
        String str33 = null;
        String str34 = null;
        String str35 = null;
        while (parcel.dataPosition() < zzL) {
            int zzK = com.google.android.gms.common.internal.safeparcel.zza.zzK(parcel);
            ItemScopeEntity itemScopeEntity11;
            switch (com.google.android.gms.common.internal.safeparcel.zza.zzaV(zzK)) {
                case 1:
                    i = com.google.android.gms.common.internal.safeparcel.zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(2));
                    itemScopeEntity = itemScopeEntity11;
                    break;
                case 3:
                    list = com.google.android.gms.common.internal.safeparcel.zza.zzC(parcel, zzK);
                    hashSet.add(Integer.valueOf(3));
                    break;
                case 4:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(4));
                    itemScopeEntity2 = itemScopeEntity11;
                    break;
                case 5:
                    str = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(5));
                    break;
                case 6:
                    str2 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(6));
                    break;
                case 7:
                    str3 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(7));
                    break;
                case 8:
                    list2 = com.google.android.gms.common.internal.safeparcel.zza.zzc(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(8));
                    break;
                case 9:
                    i2 = com.google.android.gms.common.internal.safeparcel.zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(9));
                    break;
                case 10:
                    list3 = com.google.android.gms.common.internal.safeparcel.zza.zzc(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(10));
                    break;
                case 11:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(11));
                    itemScopeEntity3 = itemScopeEntity11;
                    break;
                case 12:
                    list4 = com.google.android.gms.common.internal.safeparcel.zza.zzc(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(12));
                    break;
                case 13:
                    str4 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(13));
                    break;
                case 14:
                    str5 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(14));
                    break;
                case 15:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(15));
                    itemScopeEntity4 = itemScopeEntity11;
                    break;
                case 16:
                    str6 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(16));
                    break;
                case 17:
                    str7 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(17));
                    break;
                case 18:
                    str8 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(18));
                    break;
                case 19:
                    list5 = com.google.android.gms.common.internal.safeparcel.zza.zzc(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(19));
                    break;
                case 20:
                    str9 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(20));
                    break;
                case 21:
                    str10 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(21));
                    break;
                case 22:
                    str11 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(22));
                    break;
                case 23:
                    str12 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(23));
                    break;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    str13 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(24));
                    break;
                case 25:
                    str14 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(25));
                    break;
                case 26:
                    str15 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(26));
                    break;
                case MotionEventCompat.AXIS_RELATIVE_X /*27*/:
                    str16 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(27));
                    break;
                case MotionEventCompat.AXIS_RELATIVE_Y /*28*/:
                    str17 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(28));
                    break;
                case 29:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(29));
                    itemScopeEntity5 = itemScopeEntity11;
                    break;
                case 30:
                    str18 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(30));
                    break;
                case 31:
                    str19 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(31));
                    break;
                case 32:
                    str20 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(32));
                    break;
                case 33:
                    str21 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(33));
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(34));
                    itemScopeEntity6 = itemScopeEntity11;
                    break;
                case MotionEventCompat.AXIS_GENERIC_5 /*36*/:
                    d = com.google.android.gms.common.internal.safeparcel.zza.zzm(parcel, zzK);
                    hashSet.add(Integer.valueOf(36));
                    break;
                case MotionEventCompat.AXIS_GENERIC_6 /*37*/:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(37));
                    itemScopeEntity7 = itemScopeEntity11;
                    break;
                case MotionEventCompat.AXIS_GENERIC_7 /*38*/:
                    d2 = com.google.android.gms.common.internal.safeparcel.zza.zzm(parcel, zzK);
                    hashSet.add(Integer.valueOf(38));
                    break;
                case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    str22 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(39));
                    break;
                case 40:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(40));
                    itemScopeEntity8 = itemScopeEntity11;
                    break;
                case MotionEventCompat.AXIS_GENERIC_10 /*41*/:
                    list6 = com.google.android.gms.common.internal.safeparcel.zza.zzc(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(41));
                    break;
                case 42:
                    str23 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(42));
                    break;
                case MotionEventCompat.AXIS_GENERIC_12 /*43*/:
                    str24 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(43));
                    break;
                case MotionEventCompat.AXIS_GENERIC_13 /*44*/:
                    str25 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(44));
                    break;
                case MotionEventCompat.AXIS_GENERIC_14 /*45*/:
                    str26 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(45));
                    break;
                case MotionEventCompat.AXIS_GENERIC_15 /*46*/:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(46));
                    itemScopeEntity9 = itemScopeEntity11;
                    break;
                case MotionEventCompat.AXIS_GENERIC_16 /*47*/:
                    str27 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(47));
                    break;
                case 48:
                    str28 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(48));
                    break;
                case 49:
                    str29 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(49));
                    break;
                case GraphRequest.MAXIMUM_BATCH_SIZE /*50*/:
                    itemScopeEntity11 = (ItemScopeEntity) com.google.android.gms.common.internal.safeparcel.zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(50));
                    itemScopeEntity10 = itemScopeEntity11;
                    break;
                case 51:
                    str30 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(51));
                    break;
                case 52:
                    str31 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(52));
                    break;
                case 53:
                    str32 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(53));
                    break;
                case 54:
                    str33 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(54));
                    break;
                case 55:
                    str34 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(55));
                    break;
                case 56:
                    str35 = com.google.android.gms.common.internal.safeparcel.zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(56));
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new ItemScopeEntity(hashSet, i, itemScopeEntity, list, itemScopeEntity2, str, str2, str3, list2, i2, list3, itemScopeEntity3, list4, str4, str5, itemScopeEntity4, str6, str7, str8, list5, str9, str10, str11, str12, str13, str14, str15, str16, str17, itemScopeEntity5, str18, str19, str20, str21, itemScopeEntity6, d, itemScopeEntity7, d2, str22, itemScopeEntity8, list6, str23, str24, str25, str26, itemScopeEntity9, str27, str28, str29, itemScopeEntity10, str30, str31, str32, str33, str34, str35);
        }
        throw new com.google.android.gms.common.internal.safeparcel.zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public ItemScopeEntity[] zzhl(int i) {
        return new ItemScopeEntity[i];
    }
}

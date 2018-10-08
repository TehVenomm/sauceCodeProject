package com.google.android.gms.games.event;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class EventEntity extends zzc implements Event {
    public static final Creator<EventEntity> CREATOR = new zza();
    private final String mName;
    private final boolean zzavq;
    private final long zzdmy;
    private final String zzdmz;
    private final Uri zzhbd;
    private final String zzhbo;
    private final PlayerEntity zzhdr;
    private final String zzhdw;
    private final String zzhdx;

    public EventEntity(Event event) {
        this.zzhdw = event.getEventId();
        this.mName = event.getName();
        this.zzdmz = event.getDescription();
        this.zzhbd = event.getIconImageUri();
        this.zzhbo = event.getIconImageUrl();
        this.zzhdr = (PlayerEntity) event.getPlayer().freeze();
        this.zzdmy = event.getValue();
        this.zzhdx = event.getFormattedValue();
        this.zzavq = event.isVisible();
    }

    EventEntity(String str, String str2, String str3, Uri uri, String str4, Player player, long j, String str5, boolean z) {
        this.zzhdw = str;
        this.mName = str2;
        this.zzdmz = str3;
        this.zzhbd = uri;
        this.zzhbo = str4;
        this.zzhdr = new PlayerEntity(player);
        this.zzdmy = j;
        this.zzhdx = str5;
        this.zzavq = z;
    }

    static int zza(Event event) {
        return Arrays.hashCode(new Object[]{event.getEventId(), event.getName(), event.getDescription(), event.getIconImageUri(), event.getIconImageUrl(), event.getPlayer(), Long.valueOf(event.getValue()), event.getFormattedValue(), Boolean.valueOf(event.isVisible())});
    }

    static boolean zza(Event event, Object obj) {
        if (!(obj instanceof Event)) {
            return false;
        }
        if (event == obj) {
            return true;
        }
        Event event2 = (Event) obj;
        return zzbf.equal(event2.getEventId(), event.getEventId()) && zzbf.equal(event2.getName(), event.getName()) && zzbf.equal(event2.getDescription(), event.getDescription()) && zzbf.equal(event2.getIconImageUri(), event.getIconImageUri()) && zzbf.equal(event2.getIconImageUrl(), event.getIconImageUrl()) && zzbf.equal(event2.getPlayer(), event.getPlayer()) && zzbf.equal(Long.valueOf(event2.getValue()), Long.valueOf(event.getValue())) && zzbf.equal(event2.getFormattedValue(), event.getFormattedValue()) && zzbf.equal(Boolean.valueOf(event2.isVisible()), Boolean.valueOf(event.isVisible()));
    }

    static String zzb(Event event) {
        return zzbf.zzt(event).zzg("Id", event.getEventId()).zzg("Name", event.getName()).zzg("Description", event.getDescription()).zzg("IconImageUri", event.getIconImageUri()).zzg("IconImageUrl", event.getIconImageUrl()).zzg("Player", event.getPlayer()).zzg("Value", Long.valueOf(event.getValue())).zzg("FormattedValue", event.getFormattedValue()).zzg("isVisible", Boolean.valueOf(event.isVisible())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Event freeze() {
        return this;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final String getEventId() {
        return this.zzhdw;
    }

    public final String getFormattedValue() {
        return this.zzhdx;
    }

    public final void getFormattedValue(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzhdx, charArrayBuffer);
    }

    public final Uri getIconImageUri() {
        return this.zzhbd;
    }

    public final String getIconImageUrl() {
        return this.zzhbo;
    }

    public final String getName() {
        return this.mName;
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.mName, charArrayBuffer);
    }

    public final Player getPlayer() {
        return this.zzhdr;
    }

    public final long getValue() {
        return this.zzdmy;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isVisible() {
        return this.zzavq;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getEventId(), false);
        zzd.zza(parcel, 2, getName(), false);
        zzd.zza(parcel, 3, getDescription(), false);
        zzd.zza(parcel, 4, getIconImageUri(), i, false);
        zzd.zza(parcel, 5, getIconImageUrl(), false);
        zzd.zza(parcel, 6, getPlayer(), i, false);
        zzd.zza(parcel, 7, getValue());
        zzd.zza(parcel, 8, getFormattedValue(), false);
        zzd.zza(parcel, 9, isVisible());
        zzd.zzai(parcel, zze);
    }
}

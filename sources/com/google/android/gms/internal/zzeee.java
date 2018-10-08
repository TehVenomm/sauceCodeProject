package com.google.android.gms.internal;

public class zzeee<MessageType extends zzeed<MessageType, BuilderType>, BuilderType extends zzeee<MessageType, BuilderType>> extends zzedg<MessageType, BuilderType> {
    private final MessageType zzmyt;
    protected MessageType zzmyu;
    private boolean zzmyv = false;

    protected zzeee(MessageType messageType) {
        this.zzmyt = messageType;
        this.zzmyu = (zzeed) messageType.zza(zzeel.zzmze, null, null);
    }

    private static void zza(MessageType messageType, MessageType messageType2) {
        Object obj = zzeek.zzmyz;
        messageType.zza(zzeel.zzmzb, obj, (Object) messageType2);
        messageType.zzmyr = obj.zza(messageType.zzmyr, messageType2.zzmyr);
    }

    public /* synthetic */ Object clone() throws CloneNotSupportedException {
        zzeed zzeed;
        zzeee zzeee = (zzeee) this.zzmyt.zza(zzeel.zzmzf, null, null);
        if (this.zzmyv) {
            zzeed = this.zzmyu;
        } else {
            zzeed = this.zzmyu;
            zzeed.zza(zzeel.zzmzd, null, null);
            zzeed.zzmyr.zzbhq();
            this.zzmyv = true;
            zzeed = this.zzmyu;
        }
        zzeee.zza(zzeed);
        return zzeee;
    }

    protected final /* synthetic */ zzedg zza(zzedf zzedf) {
        return zza((zzeed) zzedf);
    }

    public final BuilderType zza(MessageType messageType) {
        zzccp();
        zza(this.zzmyu, messageType);
        return this;
    }

    public final /* synthetic */ zzedg zzcbj() {
        return (zzeee) clone();
    }

    public final /* synthetic */ zzeey zzcco() {
        return this.zzmyt;
    }

    protected final void zzccp() {
        if (this.zzmyv) {
            zzeed zzeed = (zzeed) this.zzmyu.zza(zzeel.zzmze, null, null);
            zza(zzeed, this.zzmyu);
            this.zzmyu = zzeed;
            this.zzmyv = false;
        }
    }

    public final MessageType zzccq() {
        if (this.zzmyv) {
            return this.zzmyu;
        }
        zzeed zzeed = this.zzmyu;
        zzeed.zza(zzeel.zzmzd, null, null);
        zzeed.zzmyr.zzbhq();
        this.zzmyv = true;
        return this.zzmyu;
    }

    public final MessageType zzccr() {
        zzeed zzeed;
        boolean z = true;
        if (this.zzmyv) {
            zzeed = this.zzmyu;
        } else {
            zzeed = this.zzmyu;
            zzeed.zza(zzeel.zzmzd, null, null);
            zzeed.zzmyr.zzbhq();
            this.zzmyv = true;
            zzeed = this.zzmyu;
        }
        zzeed = zzeed;
        if (zzeed.zza(zzeel.zzmza, Boolean.TRUE, null) == null) {
            z = false;
        }
        if (z) {
            return zzeed;
        }
        throw new zzefp(zzeed);
    }

    public final /* synthetic */ zzeey zzccs() {
        zzeed zzeed;
        boolean z = true;
        if (this.zzmyv) {
            zzeed = this.zzmyu;
        } else {
            zzeed = this.zzmyu;
            zzeed.zza(zzeel.zzmzd, null, null);
            zzeed.zzmyr.zzbhq();
            this.zzmyv = true;
            zzeed = this.zzmyu;
        }
        zzeed = zzeed;
        if (zzeed.zza(zzeel.zzmza, Boolean.TRUE, null) == null) {
            z = false;
        }
        if (z) {
            return zzeed;
        }
        throw new zzefp(zzeed);
    }
}

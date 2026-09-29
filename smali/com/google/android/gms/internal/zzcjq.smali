.class public final Lcom/google/android/gms/internal/zzcjq;
.super Lcom/google/android/gms/internal/zzee;

# interfaces
.implements Lcom/google/android/gms/internal/zzcjo;


# direct methods
.method constructor <init>(Landroid/os/IBinder;)V
    .locals 1

    const-string v0, "com.google.android.gms.nearby.internal.connection.IStartAdvertisingResultListener"

    invoke-direct {p0, p1, v0}, Lcom/google/android/gms/internal/zzee;-><init>(Landroid/os/IBinder;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzckj;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    invoke-virtual {p0}, Lcom/google/android/gms/internal/zzee;->zzax()Landroid/os/Parcel;

    move-result-object v0

    invoke-static {v0, p1}, Lcom/google/android/gms/internal/zzeg;->zza(Landroid/os/Parcel;Landroid/os/Parcelable;)V

    const/4 p1, 0x2

    invoke-virtual {p0, p1, v0}, Lcom/google/android/gms/internal/zzee;->zzc(ILandroid/os/Parcel;)V

    return-void
.end method

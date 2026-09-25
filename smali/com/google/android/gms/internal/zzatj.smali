.class final Lcom/google/android/gms/internal/zzatj;
.super Lcom/google/android/gms/internal/zzatl;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzati;)V
    .locals 0

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzatl;-><init>(Lcom/google/android/gms/internal/zzatj;)V

    return-void
.end method


# virtual methods
.method protected final zza(Lcom/google/android/gms/internal/zzatd;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    new-instance v0, Lcom/google/android/gms/internal/zzatk;

    invoke-direct {v0, p0}, Lcom/google/android/gms/internal/zzatk;-><init>(Lcom/google/android/gms/internal/zzatj;)V

    invoke-interface {p1, v0}, Lcom/google/android/gms/internal/zzatd;->zza(Lcom/google/android/gms/internal/zzatf;)V

    return-void
.end method

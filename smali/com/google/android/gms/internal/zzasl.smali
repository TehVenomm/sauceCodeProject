.class final Lcom/google/android/gms/internal/zzasl;
.super Lcom/google/android/gms/internal/zzasn;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzasn<",
        "Lcom/google/android/gms/common/api/Status;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzasg;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzasn;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final zza(Landroid/content/Context;Lcom/google/android/gms/internal/zzast;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    new-instance p1, Lcom/google/android/gms/internal/zzasm;

    invoke-direct {p1, p0}, Lcom/google/android/gms/internal/zzasm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p2, p1}, Lcom/google/android/gms/internal/zzast;->zza(Lcom/google/android/gms/internal/zzasr;)V

    return-void
.end method

.method protected final synthetic zzb(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/common/api/Result;
    .locals 0

    return-object p1
.end method

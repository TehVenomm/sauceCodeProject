.class final Lcom/google/android/gms/internal/zzask;
.super Lcom/google/android/gms/internal/zzasn;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzasn<",
        "Lcom/google/android/gms/common/api/Status;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzebk:Lcom/google/android/gms/auth/api/credentials/Credential;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzasg;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/auth/api/credentials/Credential;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzask;->zzebk:Lcom/google/android/gms/auth/api/credentials/Credential;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzasn;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final zza(Landroid/content/Context;Lcom/google/android/gms/internal/zzast;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    new-instance p1, Lcom/google/android/gms/internal/zzasm;

    invoke-direct {p1, p0}, Lcom/google/android/gms/internal/zzasm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    new-instance v0, Lcom/google/android/gms/internal/zzasp;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzask;->zzebk:Lcom/google/android/gms/auth/api/credentials/Credential;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzasp;-><init>(Lcom/google/android/gms/auth/api/credentials/Credential;)V

    invoke-interface {p2, p1, v0}, Lcom/google/android/gms/internal/zzast;->zza(Lcom/google/android/gms/internal/zzasr;Lcom/google/android/gms/internal/zzasp;)V

    return-void
.end method

.method protected final synthetic zzb(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/common/api/Result;
    .locals 0

    return-object p1
.end method

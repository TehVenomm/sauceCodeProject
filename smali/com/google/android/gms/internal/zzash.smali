.class final Lcom/google/android/gms/internal/zzash;
.super Lcom/google/android/gms/internal/zzasn;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzasn<",
        "Lcom/google/android/gms/auth/api/credentials/CredentialRequestResult;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzebi:Lcom/google/android/gms/auth/api/credentials/CredentialRequest;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzasg;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/auth/api/credentials/CredentialRequest;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzash;->zzebi:Lcom/google/android/gms/auth/api/credentials/CredentialRequest;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzasn;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final zza(Landroid/content/Context;Lcom/google/android/gms/internal/zzast;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    new-instance p1, Lcom/google/android/gms/internal/zzasi;

    invoke-direct {p1, p0}, Lcom/google/android/gms/internal/zzasi;-><init>(Lcom/google/android/gms/internal/zzash;)V

    iget-object v0, p0, Lcom/google/android/gms/internal/zzash;->zzebi:Lcom/google/android/gms/auth/api/credentials/CredentialRequest;

    invoke-interface {p2, p1, v0}, Lcom/google/android/gms/internal/zzast;->zza(Lcom/google/android/gms/internal/zzasr;Lcom/google/android/gms/auth/api/credentials/CredentialRequest;)V

    return-void
.end method

.method protected final synthetic zzb(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/common/api/Result;
    .locals 0

    invoke-static {p1}, Lcom/google/android/gms/internal/zzasf;->zzf(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/internal/zzasf;

    move-result-object p1

    return-object p1
.end method

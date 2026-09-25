.class final Lcom/google/android/gms/internal/zzasi;
.super Lcom/google/android/gms/internal/zzase;


# instance fields
.field private synthetic zzebj:Lcom/google/android/gms/internal/zzash;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzash;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzasi;->zzebj:Lcom/google/android/gms/internal/zzash;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzase;-><init>()V

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/auth/api/credentials/Credential;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzasi;->zzebj:Lcom/google/android/gms/internal/zzash;

    new-instance v1, Lcom/google/android/gms/internal/zzasf;

    invoke-direct {v1, p1, p2}, Lcom/google/android/gms/internal/zzasf;-><init>(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/auth/api/credentials/Credential;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzs;->setResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method

.method public final zze(Lcom/google/android/gms/common/api/Status;)V
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/internal/zzasi;->zzebj:Lcom/google/android/gms/internal/zzash;

    invoke-static {p1}, Lcom/google/android/gms/internal/zzasf;->zzf(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/internal/zzasf;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/google/android/gms/common/api/internal/zzs;->setResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method

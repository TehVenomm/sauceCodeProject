.class final Lcom/google/android/gms/internal/zzarc;
.super Lcom/google/android/gms/internal/zzard;


# instance fields
.field private synthetic zzdyc:Lcom/google/android/gms/internal/zzarb;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzarb;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzarc;->zzdyc:Lcom/google/android/gms/internal/zzarb;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzard;-><init>()V

    return-void
.end method


# virtual methods
.method public final zzao(Z)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzarc;->zzdyc:Lcom/google/android/gms/internal/zzarb;

    new-instance v1, Lcom/google/android/gms/internal/zzarg;

    if-eqz p1, :cond_0

    sget-object p1, Lcom/google/android/gms/common/api/Status;->zzfhp:Lcom/google/android/gms/common/api/Status;

    goto :goto_0

    :cond_0
    invoke-static {}, Lcom/google/android/gms/internal/zzaqx;->zzzu()Lcom/google/android/gms/common/api/Status;

    move-result-object p1

    :goto_0
    invoke-direct {v1, p1}, Lcom/google/android/gms/internal/zzarg;-><init>(Lcom/google/android/gms/common/api/Status;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzs;->setResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method

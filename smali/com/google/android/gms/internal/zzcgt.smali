.class final Lcom/google/android/gms/internal/zzcgt;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/ConnectionLifecycleCallback;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbi:Lcom/google/android/gms/internal/zzcjx;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzcgr;Lcom/google/android/gms/internal/zzcjx;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcgt;->zzjbi:Lcom/google/android/gms/internal/zzcjx;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 4

    check-cast p1, Lcom/google/android/gms/nearby/connection/ConnectionLifecycleCallback;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgt;->zzjbi:Lcom/google/android/gms/internal/zzcjx;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzcjx;->zzbaj()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Lcom/google/android/gms/nearby/connection/ConnectionResolution;

    new-instance v2, Lcom/google/android/gms/common/api/Status;

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgt;->zzjbi:Lcom/google/android/gms/internal/zzcjx;

    invoke-virtual {v3}, Lcom/google/android/gms/internal/zzcjx;->getStatusCode()I

    move-result v3

    invoke-direct {v2, v3}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    invoke-direct {v1, v2}, Lcom/google/android/gms/nearby/connection/ConnectionResolution;-><init>(Lcom/google/android/gms/common/api/Status;)V

    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/nearby/connection/ConnectionLifecycleCallback;->onConnectionResult(Ljava/lang/String;Lcom/google/android/gms/nearby/connection/ConnectionResolution;)V

    return-void
.end method

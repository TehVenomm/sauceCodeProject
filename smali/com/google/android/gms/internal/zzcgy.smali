.class final Lcom/google/android/gms/internal/zzcgy;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/Connections$ConnectionResponseCallback;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbm:Lcom/google/android/gms/internal/zzcjv;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzcgx;Lcom/google/android/gms/internal/zzcjv;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcgy;->zzjbm:Lcom/google/android/gms/internal/zzcjv;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 3

    check-cast p1, Lcom/google/android/gms/nearby/connection/Connections$ConnectionResponseCallback;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgy;->zzjbm:Lcom/google/android/gms/internal/zzcjv;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzcjv;->zzbaj()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Lcom/google/android/gms/common/api/Status;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzcgy;->zzjbm:Lcom/google/android/gms/internal/zzcjv;

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzcjv;->getStatusCode()I

    move-result v2

    invoke-direct {v1, v2}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    iget-object v2, p0, Lcom/google/android/gms/internal/zzcgy;->zzjbm:Lcom/google/android/gms/internal/zzcjv;

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzcjv;->zzbam()[B

    move-result-object v2

    invoke-interface {p1, v0, v1, v2}, Lcom/google/android/gms/nearby/connection/Connections$ConnectionResponseCallback;->onConnectionResponse(Ljava/lang/String;Lcom/google/android/gms/common/api/Status;[B)V

    return-void
.end method

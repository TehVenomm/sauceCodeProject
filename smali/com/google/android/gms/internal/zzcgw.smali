.class final Lcom/google/android/gms/internal/zzcgw;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/Connections$ConnectionRequestListener;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbk:Lcom/google/android/gms/internal/zzcjt;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzcgv;Lcom/google/android/gms/internal/zzcjt;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcgw;->zzjbk:Lcom/google/android/gms/internal/zzcjt;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 3

    check-cast p1, Lcom/google/android/gms/nearby/connection/Connections$ConnectionRequestListener;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgw;->zzjbk:Lcom/google/android/gms/internal/zzcjt;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzcjt;->zzbaj()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgw;->zzjbk:Lcom/google/android/gms/internal/zzcjt;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzcjt;->zzbak()Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lcom/google/android/gms/internal/zzcgw;->zzjbk:Lcom/google/android/gms/internal/zzcjt;

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzcjt;->zzbam()[B

    move-result-object v2

    invoke-virtual {p1, v0, v1, v2}, Lcom/google/android/gms/nearby/connection/Connections$ConnectionRequestListener;->onConnectionRequest(Ljava/lang/String;Ljava/lang/String;[B)V

    return-void
.end method

.class final Lcom/google/android/gms/internal/zzchh;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/Connections$MessageListener;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbj:Lcom/google/android/gms/internal/zzcjz;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchf;Lcom/google/android/gms/internal/zzcjz;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzchh;->zzjbj:Lcom/google/android/gms/internal/zzcjz;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Lcom/google/android/gms/nearby/connection/Connections$MessageListener;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchh;->zzjbj:Lcom/google/android/gms/internal/zzcjz;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzcjz;->zzbaj()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, v0}, Lcom/google/android/gms/nearby/connection/Connections$MessageListener;->onDisconnected(Ljava/lang/String;)V

    return-void
.end method

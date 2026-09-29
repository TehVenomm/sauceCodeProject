.class final Lcom/google/android/gms/internal/zzche;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/Connections$EndpointDiscoveryListener;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbo:Lcom/google/android/gms/internal/zzckd;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchc;Lcom/google/android/gms/internal/zzckd;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzche;->zzjbo:Lcom/google/android/gms/internal/zzckd;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Lcom/google/android/gms/nearby/connection/Connections$EndpointDiscoveryListener;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzche;->zzjbo:Lcom/google/android/gms/internal/zzckd;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzckd;->zzban()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/google/android/gms/nearby/connection/Connections$EndpointDiscoveryListener;->onEndpointLost(Ljava/lang/String;)V

    return-void
.end method

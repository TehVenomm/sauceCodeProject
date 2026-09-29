.class final Lcom/google/android/gms/internal/zzcmb;
.super Lcom/google/android/gms/internal/zzclv;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzclv<",
        "Lcom/google/android/gms/nearby/messages/SubscribeCallback;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzclv;-><init>()V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Lcom/google/android/gms/nearby/messages/SubscribeCallback;

    invoke-virtual {p1}, Lcom/google/android/gms/nearby/messages/SubscribeCallback;->onExpired()V

    return-void
.end method

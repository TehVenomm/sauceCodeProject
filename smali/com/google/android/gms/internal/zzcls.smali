.class final Lcom/google/android/gms/internal/zzcls;
.super Lcom/google/android/gms/internal/zzclv;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzclv<",
        "Lcom/google/android/gms/nearby/messages/MessageListener;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjha:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzclr;Ljava/util/List;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcls;->zzjha:Ljava/util/List;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzclv;-><init>()V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Lcom/google/android/gms/nearby/messages/MessageListener;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcls;->zzjha:Ljava/util/List;

    invoke-static {v0, p1}, Lcom/google/android/gms/nearby/messages/internal/zzak;->zza(Ljava/lang/Iterable;Lcom/google/android/gms/nearby/messages/MessageListener;)V

    return-void
.end method

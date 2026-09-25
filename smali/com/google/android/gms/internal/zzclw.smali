.class public final Lcom/google/android/gms/internal/zzclw;
.super Lcom/google/android/gms/nearby/messages/internal/zzv;

# interfaces
.implements Lcom/google/android/gms/internal/zzclq;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/nearby/messages/internal/zzv;",
        "Lcom/google/android/gms/internal/zzclq<",
        "Lcom/google/android/gms/nearby/messages/PublishCallback;",
        ">;"
    }
.end annotation


# static fields
.field private static final zzjhc:Lcom/google/android/gms/internal/zzclv;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/internal/zzclv<",
            "Lcom/google/android/gms/nearby/messages/PublishCallback;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/PublishCallback;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzclx;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzclx;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzclw;->zzjhc:Lcom/google/android/gms/internal/zzclv;

    return-void
.end method

.method public constructor <init>(Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/PublishCallback;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/nearby/messages/internal/zzv;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzclw;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final onExpired()V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzclw;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    sget-object v1, Lcom/google/android/gms/internal/zzclw;->zzjhc:Lcom/google/android/gms/internal/zzclv;

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/PublishCallback;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzclw;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-object v0
.end method

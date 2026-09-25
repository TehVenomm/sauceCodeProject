.class public Lcom/google/android/gms/nearby/messages/Strategy$Builder;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/gms/nearby/messages/Strategy;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private zzjeo:I

.field private zzjep:I

.field private zzjeq:I

.field private zzjer:I

.field private zzjes:I


# direct methods
.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x3

    iput v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeo:I

    const/16 v0, 0x12c

    iput v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjep:I

    const/4 v0, 0x0

    iput v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeq:I

    const/4 v1, -0x1

    iput v1, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjer:I

    iput v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjes:I

    return-void
.end method


# virtual methods
.method public build()Lcom/google/android/gms/nearby/messages/Strategy;
    .locals 11

    iget v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjer:I

    const/4 v1, 0x2

    if-ne v0, v1, :cond_1

    iget v0, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeq:I

    const/4 v1, 0x1

    if-eq v0, v1, :cond_0

    goto :goto_0

    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot set EARSHOT with BLE only mode."

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_1
    :goto_0
    new-instance v0, Lcom/google/android/gms/nearby/messages/Strategy;

    const/4 v3, 0x2

    const/4 v4, 0x0

    iget v5, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjep:I

    iget v6, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeq:I

    const/4 v7, 0x0

    iget v8, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjer:I

    iget v9, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeo:I

    const/4 v10, 0x0

    move-object v2, v0

    invoke-direct/range {v2 .. v10}, Lcom/google/android/gms/nearby/messages/Strategy;-><init>(IIIIZIII)V

    return-object v0
.end method

.method public setDiscoveryMode(I)Lcom/google/android/gms/nearby/messages/Strategy$Builder;
    .locals 0

    iput p1, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeo:I

    return-object p0
.end method

.method public setDistanceType(I)Lcom/google/android/gms/nearby/messages/Strategy$Builder;
    .locals 0

    iput p1, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjeq:I

    return-object p0
.end method

.method public setTtlSeconds(I)Lcom/google/android/gms/nearby/messages/Strategy$Builder;
    .locals 7

    const/4 v0, 0x0

    const v1, 0x15180

    const/4 v2, 0x1

    const v3, 0x7fffffff

    if-eq p1, v3, :cond_1

    if-lez p1, :cond_0

    if-gt p1, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v3, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v3, 0x1

    :goto_1
    const-string v4, "mTtlSeconds(%d) must either be TTL_SECONDS_INFINITE, or it must be between 1 and TTL_SECONDS_MAX(%d) inclusive"

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    aput-object v6, v5, v0

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    aput-object v0, v5, v2

    invoke-static {v3, v4, v5}, Lcom/google/android/gms/common/internal/zzbp;->zzb(ZLjava/lang/String;[Ljava/lang/Object;)V

    iput p1, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjep:I

    return-object p0
.end method

.method public final zzdx(I)Lcom/google/android/gms/nearby/messages/Strategy$Builder;
    .locals 0

    const/4 p1, 0x2

    iput p1, p0, Lcom/google/android/gms/nearby/messages/Strategy$Builder;->zzjer:I

    return-object p0
.end method

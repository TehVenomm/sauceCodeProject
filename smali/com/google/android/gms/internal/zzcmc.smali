.class public final Lcom/google/android/gms/internal/zzcmc;
.super Ljava/lang/Object;


# instance fields
.field private final zzjhe:Landroidx/collection/SimpleArrayMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroidx/collection/SimpleArrayMap<",
            "Ljava/lang/Object;",
            "Lcom/google/android/gms/internal/zzclq;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Landroidx/collection/SimpleArrayMap;

    const/4 v1, 0x1

    invoke-direct {v0, v1}, Landroidx/collection/SimpleArrayMap;-><init>(I)V

    iput-object v0, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    return-void
.end method

.method private static zza(Lcom/google/android/gms/internal/zzclq;)Z
    .locals 0

    if-eqz p0, :cond_0

    invoke-interface {p0}, Lcom/google/android/gms/internal/zzclq;->zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object p0

    invoke-virtual {p0}, Lcom/google/android/gms/common/api/internal/zzcj;->zzadh()Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 p0, 0x0

    return p0
.end method

.method private final declared-synchronized zzj(Lcom/google/android/gms/common/api/internal/zzcj;)Ljava/lang/Object;
    .locals 3
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "TC;>;)TC;"
        }
    .end annotation

    monitor-enter p0

    const/4 v0, 0x0

    :goto_0
    :try_start_0
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v1}, Landroidx/collection/SimpleArrayMap;->size()I

    move-result v1

    if-ge v0, v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v1, v0}, Landroidx/collection/SimpleArrayMap;->keyAt(I)Ljava/lang/Object;

    move-result-object v1

    iget-object v2, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v2, v1}, Landroidx/collection/SimpleArrayMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/google/android/gms/internal/zzclq;

    invoke-interface {v2}, Lcom/google/android/gms/internal/zzclq;->zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v2, :cond_0

    monitor-exit p0

    return-object v1

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method


# virtual methods
.method public final declared-synchronized clear()V
    .locals 1

    monitor-enter p0

    :try_start_0
    iget-object v0, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v0}, Landroidx/collection/SimpleArrayMap;->clear()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public final declared-synchronized zzae(Ljava/lang/Object;)Lcom/google/android/gms/internal/zzclq;
    .locals 3
    .param p1    # Ljava/lang/Object;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C:",
            "Ljava/lang/Object;",
            ">(TC;)",
            "Lcom/google/android/gms/internal/zzclq<",
            "TC;>;"
        }
    .end annotation

    monitor-enter p0

    const/4 v0, 0x0

    if-nez p1, :cond_0

    monitor-exit p0

    return-object v0

    :cond_0
    :try_start_0
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v1, p1}, Landroidx/collection/SimpleArrayMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/google/android/gms/internal/zzclq;

    invoke-static {v1}, Lcom/google/android/gms/internal/zzcmc;->zza(Lcom/google/android/gms/internal/zzclq;)Z

    move-result v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v2, :cond_1

    monitor-exit p0

    return-object v1

    :cond_1
    :try_start_1
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v1, p1}, Landroidx/collection/SimpleArrayMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public final declared-synchronized zzb(Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/Object;)Lcom/google/android/gms/internal/zzclq;
    .locals 2
    .param p2    # Ljava/lang/Object;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "TC;)",
            "Lcom/google/android/gms/internal/zzclq<",
            "TC;>;"
        }
    .end annotation

    monitor-enter p0

    if-nez p2, :cond_0

    const/4 p1, 0x0

    monitor-exit p0

    return-object p1

    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v0, p2}, Landroidx/collection/SimpleArrayMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/internal/zzclq;

    invoke-static {v0}, Lcom/google/android/gms/internal/zzcmc;->zza(Lcom/google/android/gms/internal/zzclq;)Z

    move-result v1

    if-nez v1, :cond_4

    invoke-virtual {p1, p2}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzp(Ljava/lang/Object;)Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object p1

    instance-of v0, p2, Lcom/google/android/gms/nearby/messages/StatusCallback;

    if-eqz v0, :cond_1

    new-instance v0, Lcom/google/android/gms/internal/zzcly;

    invoke-direct {v0, p1}, Lcom/google/android/gms/internal/zzcly;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    goto :goto_0

    :cond_1
    instance-of v0, p2, Lcom/google/android/gms/nearby/messages/MessageListener;

    if-eqz v0, :cond_2

    new-instance v0, Lcom/google/android/gms/internal/zzclr;

    invoke-direct {v0, p1}, Lcom/google/android/gms/internal/zzclr;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    :goto_0
    iget-object p1, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {p1, p2, v0}, Landroidx/collection/SimpleArrayMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    :cond_2
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Unknown callback of type "

    invoke-virtual {p2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/String;->length()I

    move-result v1

    if-eqz v1, :cond_3

    invoke-virtual {v0, p2}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    goto :goto_1

    :cond_3
    new-instance p2, Ljava/lang/String;

    invoke-direct {p2, v0}, Ljava/lang/String;-><init>(Ljava/lang/String;)V

    :goto_1
    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :cond_4
    :goto_2
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public final declared-synchronized zzh(Lcom/google/android/gms/common/api/internal/zzcj;)Lcom/google/android/gms/internal/zzclq;
    .locals 0
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "TC;>;)",
            "Lcom/google/android/gms/internal/zzclq<",
            "TC;>;"
        }
    .end annotation

    monitor-enter p0

    if-nez p1, :cond_0

    const/4 p1, 0x0

    monitor-exit p0

    return-object p1

    :cond_0
    :try_start_0
    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzcmc;->zzj(Lcom/google/android/gms/common/api/internal/zzcj;)Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/google/android/gms/internal/zzcmc;->zzae(Ljava/lang/Object;)Lcom/google/android/gms/internal/zzclq;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public final declared-synchronized zzi(Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "TC;>;)V"
        }
    .end annotation

    monitor-enter p0

    :try_start_0
    invoke-virtual {p1}, Lcom/google/android/gms/common/api/internal/zzcj;->clear()V

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcmc;->zzjhe:Landroidx/collection/SimpleArrayMap;

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzcmc;->zzj(Lcom/google/android/gms/common/api/internal/zzcj;)Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroidx/collection/SimpleArrayMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.class public final Lcom/google/android/gms/internal/zzcgp;
.super Lcom/google/android/gms/common/internal/zzaa;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/common/internal/zzaa<",
        "Lcom/google/android/gms/internal/zzcjg;",
        ">;"
    }
.end annotation


# instance fields
.field private final zzjbe:J

.field private zzjbf:Lcom/google/android/gms/internal/zzckp;


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/os/Looper;Lcom/google/android/gms/common/internal/zzq;Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)V
    .locals 7

    const/16 v3, 0x36

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v4, p3

    move-object v5, p4

    move-object v6, p5

    invoke-direct/range {v0 .. v6}, Lcom/google/android/gms/common/internal/zzaa;-><init>(Landroid/content/Context;Landroid/os/Looper;ILcom/google/android/gms/common/internal/zzq;Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)V

    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result p1

    int-to-long p1, p1

    iput-wide p1, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbe:J

    return-void
.end method


# virtual methods
.method public final disconnect()V
    .locals 3

    invoke-virtual {p0}, Lcom/google/android/gms/common/internal/zzd;->isConnected()Z

    move-result v0

    if-eqz v0, :cond_0

    :try_start_0
    invoke-virtual {p0}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/internal/zzcjg;

    new-instance v1, Lcom/google/android/gms/internal/zzcgn;

    invoke-direct {v1}, Lcom/google/android/gms/internal/zzcgn;-><init>()V

    invoke-interface {v0, v1}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcgn;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "NearbyConnectionsClient"

    const-string v2, "Failed to notify client disconnect."

    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbf:Lcom/google/android/gms/internal/zzckp;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbf:Lcom/google/android/gms/internal/zzckp;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzckp;->shutdown()V

    const/4 v0, 0x0

    iput-object v0, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbf:Lcom/google/android/gms/internal/zzckp;

    :cond_1
    invoke-super {p0}, Lcom/google/android/gms/common/internal/zzaa;->disconnect()V

    return-void
.end method

.method protected final synthetic zza(Landroid/os/IInterface;)V
    .locals 0
    .param p1    # Landroid/os/IInterface;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    invoke-super {p0, p1}, Lcom/google/android/gms/common/internal/zzaa;->zza(Landroid/os/IInterface;)V

    new-instance p1, Lcom/google/android/gms/internal/zzckp;

    invoke-direct {p1}, Lcom/google/android/gms/internal/zzckp;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbf:Lcom/google/android/gms/internal/zzckp;

    return-void
.end method

.method public final zza(Lcom/google/android/gms/common/api/internal/zzn;[Ljava/lang/String;Lcom/google/android/gms/nearby/connection/Payload;Z)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/common/api/Status;",
            ">;[",
            "Ljava/lang/String;",
            "Lcom/google/android/gms/nearby/connection/Payload;",
            "Z)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    :try_start_0
    invoke-static {p3}, Lcom/google/android/gms/internal/zzckt;->zza(Lcom/google/android/gms/nearby/connection/Payload;)Landroid/util/Pair;

    move-result-object v0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    invoke-virtual {p0}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object v1

    check-cast v1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v2, Lcom/google/android/gms/internal/zzcky;

    new-instance v3, Lcom/google/android/gms/internal/zzchm;

    invoke-direct {v3, p1}, Lcom/google/android/gms/internal/zzchm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-virtual {v3}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    iget-object v3, v0, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v3, Lcom/google/android/gms/internal/zzckr;

    invoke-direct {v2, p1, p2, v3, p4}, Lcom/google/android/gms/internal/zzcky;-><init>(Landroid/os/IBinder;[Ljava/lang/String;Lcom/google/android/gms/internal/zzckr;Z)V

    invoke-interface {v1, v2}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcky;)V

    iget-object p1, v0, Landroid/util/Pair;->second:Ljava/lang/Object;

    if-eqz p1, :cond_0

    iget-object p1, v0, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast p1, Landroid/util/Pair;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbf:Lcom/google/android/gms/internal/zzckp;

    invoke-virtual {p3}, Lcom/google/android/gms/nearby/connection/Payload;->asStream()Lcom/google/android/gms/nearby/connection/Payload$Stream;

    move-result-object p2

    invoke-virtual {p2}, Lcom/google/android/gms/nearby/connection/Payload$Stream;->asInputStream()Ljava/io/InputStream;

    move-result-object v1

    new-instance v2, Landroid/os/ParcelFileDescriptor$AutoCloseOutputStream;

    iget-object p2, p1, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast p2, Landroid/os/ParcelFileDescriptor;

    invoke-direct {v2, p2}, Landroid/os/ParcelFileDescriptor$AutoCloseOutputStream;-><init>(Landroid/os/ParcelFileDescriptor;)V

    new-instance v3, Landroid/os/ParcelFileDescriptor$AutoCloseOutputStream;

    iget-object p1, p1, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast p1, Landroid/os/ParcelFileDescriptor;

    invoke-direct {v3, p1}, Landroid/os/ParcelFileDescriptor$AutoCloseOutputStream;-><init>(Landroid/os/ParcelFileDescriptor;)V

    invoke-virtual {p3}, Lcom/google/android/gms/nearby/connection/Payload;->getId()J

    move-result-wide v4

    invoke-virtual/range {v0 .. v5}, Lcom/google/android/gms/internal/zzckp;->zza(Ljava/io/InputStream;Ljava/io/OutputStream;Ljava/io/OutputStream;J)V

    :cond_0
    return-void

    :catch_0
    new-instance p2, Lcom/google/android/gms/common/api/Status;

    const/16 p3, 0x1f4d

    invoke-direct {p2, p3}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    invoke-interface {p1, p2}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method

.method protected final synthetic zze(Landroid/os/IBinder;)Landroid/os/IInterface;
    .locals 2

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    :cond_0
    const-string v0, "com.google.android.gms.nearby.internal.connection.INearbyConnectionService"

    invoke-interface {p1, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v1, v0, Lcom/google/android/gms/internal/zzcjg;

    if-eqz v1, :cond_1

    check-cast v0, Lcom/google/android/gms/internal/zzcjg;

    return-object v0

    :cond_1
    new-instance v0, Lcom/google/android/gms/internal/zzcjh;

    invoke-direct {v0, p1}, Lcom/google/android/gms/internal/zzcjh;-><init>(Landroid/os/IBinder;)V

    return-object v0
.end method

.method protected final zzhc()Ljava/lang/String;
    .locals 1

    const-string v0, "com.google.android.gms.nearby.connection.service.START"

    return-object v0
.end method

.method protected final zzhd()Ljava/lang/String;
    .locals 1

    const-string v0, "com.google.android.gms.nearby.internal.connection.INearbyConnectionService"

    return-object v0
.end method

.method public final zzj(Lcom/google/android/gms/common/api/internal/zzn;Ljava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/common/api/Status;",
            ">;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    invoke-virtual {p0}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/internal/zzcjg;

    new-instance v1, Lcom/google/android/gms/internal/zzcku;

    new-instance v2, Lcom/google/android/gms/internal/zzchm;

    invoke-direct {v2, p1}, Lcom/google/android/gms/internal/zzchm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    invoke-direct {v1, p1, p2}, Lcom/google/android/gms/internal/zzcku;-><init>(Landroid/os/IBinder;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcku;)V

    return-void
.end method

.method protected final zzzs()Landroid/os/Bundle;
    .locals 4

    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "clientId"

    iget-wide v2, p0, Lcom/google/android/gms/internal/zzcgp;->zzjbe:J

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Bundle;->putLong(Ljava/lang/String;J)V

    return-object v0
.end method

.class public final Lcom/google/android/gms/internal/zzbid;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/gms/drive/DriveApi;


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final cancelPendingActions(Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/util/List;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/common/api/Status;",
            ">;"
        }
    .end annotation

    sget-object v0, Lcom/google/android/gms/drive/Drive;->zzdwq:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zza(Lcom/google/android/gms/common/api/Api$zzc;)Lcom/google/android/gms/common/api/Api$zze;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/internal/zzbiw;

    const/4 v1, 0x1

    if-eqz p2, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    invoke-static {v2}, Lcom/google/android/gms/common/internal/zzbp;->zzbh(Z)V

    invoke-interface {p2}, Ljava/util/List;->isEmpty()Z

    move-result v2

    xor-int/2addr v1, v2

    invoke-static {v1}, Lcom/google/android/gms/common/internal/zzbp;->zzbh(Z)V

    invoke-virtual {v0}, Lcom/google/android/gms/common/internal/zzd;->isConnected()Z

    move-result v1

    const-string v2, "Client must be connected"

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbp;->zza(ZLjava/lang/Object;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbjb;

    invoke-direct {v1, v0, p1, p2}, Lcom/google/android/gms/internal/zzbjb;-><init>(Lcom/google/android/gms/internal/zzbiw;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/util/List;)V

    invoke-virtual {p1, v1}, Lcom/google/android/gms/common/api/GoogleApiClient;->zze(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

.method public final fetchDriveId(Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Ljava/lang/String;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveApi$DriveIdResult;",
            ">;"
        }
    .end annotation

    new-instance v0, Lcom/google/android/gms/internal/zzbig;

    invoke-direct {v0, p0, p1, p2}, Lcom/google/android/gms/internal/zzbig;-><init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzd(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

.method public final getAppFolder(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/drive/DriveFolder;
    .locals 1

    sget-object v0, Lcom/google/android/gms/drive/Drive;->zzdwq:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zza(Lcom/google/android/gms/common/api/Api$zzc;)Lcom/google/android/gms/common/api/Api$zze;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzbiw;->zzanl()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzbiw;->zzank()Lcom/google/android/gms/drive/DriveId;

    move-result-object p1

    if-eqz p1, :cond_0

    new-instance v0, Lcom/google/android/gms/internal/zzbjm;

    invoke-direct {v0, p1}, Lcom/google/android/gms/internal/zzbjm;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-object v0

    :cond_0
    const/4 p1, 0x0

    return-object p1

    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Client is not yet connected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final getFile(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/DriveId;)Lcom/google/android/gms/drive/DriveFile;
    .locals 0

    if-eqz p2, :cond_1

    invoke-virtual {p1}, Lcom/google/android/gms/common/api/GoogleApiClient;->isConnected()Z

    move-result p1

    if-eqz p1, :cond_0

    new-instance p1, Lcom/google/android/gms/internal/zzbjh;

    invoke-direct {p1, p2}, Lcom/google/android/gms/internal/zzbjh;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-object p1

    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Client must be connected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Id must be provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final getFolder(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/DriveId;)Lcom/google/android/gms/drive/DriveFolder;
    .locals 0

    if-eqz p2, :cond_1

    invoke-virtual {p1}, Lcom/google/android/gms/common/api/GoogleApiClient;->isConnected()Z

    move-result p1

    if-eqz p1, :cond_0

    new-instance p1, Lcom/google/android/gms/internal/zzbjm;

    invoke-direct {p1, p2}, Lcom/google/android/gms/internal/zzbjm;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-object p1

    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Client must be connected"

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Id must be provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final getRootFolder(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/drive/DriveFolder;
    .locals 1

    sget-object v0, Lcom/google/android/gms/drive/Drive;->zzdwq:Lcom/google/android/gms/common/api/Api$zzf;

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zza(Lcom/google/android/gms/common/api/Api$zzc;)Lcom/google/android/gms/common/api/Api$zze;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzbiw;->zzanl()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzbiw;->zzanj()Lcom/google/android/gms/drive/DriveId;

    move-result-object p1

    if-eqz p1, :cond_0

    new-instance v0, Lcom/google/android/gms/internal/zzbjm;

    invoke-direct {v0, p1}, Lcom/google/android/gms/internal/zzbjm;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-object v0

    :cond_0
    const/4 p1, 0x0

    return-object p1

    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Client is not yet connected"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final isAutobackupEnabled(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/common/api/BooleanResult;",
            ">;"
        }
    .end annotation

    new-instance v0, Lcom/google/android/gms/internal/zzbii;

    invoke-direct {v0, p0, p1}, Lcom/google/android/gms/internal/zzbii;-><init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzd(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

.method public final newCreateFileActivityBuilder()Lcom/google/android/gms/drive/CreateFileActivityBuilder;
    .locals 1

    new-instance v0, Lcom/google/android/gms/drive/CreateFileActivityBuilder;

    invoke-direct {v0}, Lcom/google/android/gms/drive/CreateFileActivityBuilder;-><init>()V

    return-object v0
.end method

.method public final newDriveContents(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveApi$DriveContentsResult;",
            ">;"
        }
    .end annotation

    new-instance v0, Lcom/google/android/gms/internal/zzbif;

    const/high16 v1, 0x20000000

    invoke-direct {v0, p0, p1, v1}, Lcom/google/android/gms/internal/zzbif;-><init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;I)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzd(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

.method public final newOpenFileActivityBuilder()Lcom/google/android/gms/drive/OpenFileActivityBuilder;
    .locals 1

    new-instance v0, Lcom/google/android/gms/drive/OpenFileActivityBuilder;

    invoke-direct {v0}, Lcom/google/android/gms/drive/OpenFileActivityBuilder;-><init>()V

    return-object v0
.end method

.method public final query(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Lcom/google/android/gms/drive/query/Query;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveApi$MetadataBufferResult;",
            ">;"
        }
    .end annotation

    if-eqz p2, :cond_0

    new-instance v0, Lcom/google/android/gms/internal/zzbie;

    invoke-direct {v0, p0, p1, p2}, Lcom/google/android/gms/internal/zzbie;-><init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzd(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Query must be provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final requestSync(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/common/api/Status;",
            ">;"
        }
    .end annotation

    new-instance v0, Lcom/google/android/gms/internal/zzbih;

    invoke-direct {v0, p0, p1}, Lcom/google/android/gms/internal/zzbih;-><init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zze(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

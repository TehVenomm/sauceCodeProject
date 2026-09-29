.class public final Lcom/google/android/gms/internal/zzbjm;
.super Lcom/google/android/gms/internal/zzbkc;

# interfaces
.implements Lcom/google/android/gms/drive/DriveFolder;


# direct methods
.method public constructor <init>(Lcom/google/android/gms/drive/DriveId;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzbkc;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-void
.end method

.method private final zza(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/DriveContents;Lcom/google/android/gms/drive/zzm;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Lcom/google/android/gms/drive/MetadataChangeSet;",
            "Lcom/google/android/gms/drive/DriveContents;",
            "Lcom/google/android/gms/drive/zzm;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveFolder$DriveFileResult;",
            ">;"
        }
    .end annotation

    if-nez p4, :cond_0

    new-instance p4, Lcom/google/android/gms/drive/zzo;

    invoke-direct {p4}, Lcom/google/android/gms/drive/zzo;-><init>()V

    invoke-virtual {p4}, Lcom/google/android/gms/drive/ExecutionOptions$Builder;->build()Lcom/google/android/gms/drive/ExecutionOptions;

    move-result-object p4

    check-cast p4, Lcom/google/android/gms/drive/zzm;

    :cond_0
    move-object v6, p4

    if-eqz p2, :cond_b

    invoke-virtual {p2}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object p4

    invoke-static {p4}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzgs(Ljava/lang/String;)Lcom/google/android/gms/drive/metadata/internal/zzk;

    move-result-object p4

    if-eqz p4, :cond_2

    invoke-virtual {p4}, Lcom/google/android/gms/drive/metadata/internal/zzk;->isFolder()Z

    move-result p4

    if-nez p4, :cond_1

    goto :goto_0

    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "May not create folders using this method. Use DriveFolder.createFolder() instead of mime type application/vnd.google-apps.folder"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_2
    :goto_0
    invoke-virtual {v6, p1}, Lcom/google/android/gms/drive/ExecutionOptions;->zze(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    if-eqz p3, :cond_6

    instance-of p4, p3, Lcom/google/android/gms/internal/zzbjc;

    if-eqz p4, :cond_5

    invoke-interface {p3}, Lcom/google/android/gms/drive/DriveContents;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object p4

    if-nez p4, :cond_4

    invoke-interface {p3}, Lcom/google/android/gms/drive/DriveContents;->zzams()Z

    move-result p4

    if-nez p4, :cond_3

    goto :goto_1

    :cond_3
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "DriveContents are already closed."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_4
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Only DriveContents obtained through DriveApi.newDriveContents are accepted for file creation."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_5
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Only DriveContents obtained from the Drive API are accepted."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_6
    :goto_1
    invoke-virtual {p2}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object p4

    invoke-static {p4}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzgs(Ljava/lang/String;)Lcom/google/android/gms/drive/metadata/internal/zzk;

    move-result-object p4

    const/4 v0, 0x0

    const/4 v1, 0x1

    if-nez p3, :cond_8

    if-eqz p4, :cond_7

    invoke-virtual {p4}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzanw()Z

    move-result p3

    if-eqz p3, :cond_7

    const/4 p3, 0x0

    goto :goto_2

    :cond_7
    const/4 p3, 0x1

    :goto_2
    move v4, p3

    goto :goto_3

    :cond_8
    invoke-interface {p3}, Lcom/google/android/gms/drive/DriveContents;->zzamq()Lcom/google/android/gms/drive/zzc;

    move-result-object p4

    invoke-virtual {p4}, Lcom/google/android/gms/drive/zzc;->getRequestId()I

    move-result p4

    invoke-interface {p3}, Lcom/google/android/gms/drive/DriveContents;->zzamr()V

    move v4, p4

    :goto_3
    invoke-virtual {v6}, Lcom/google/android/gms/drive/zzm;->zzamy()Ljava/lang/String;

    move-result-object p3

    if-eqz p3, :cond_9

    sget-object p4, Lcom/google/android/gms/internal/zzbnr;->zzgmd:Lcom/google/android/gms/drive/metadata/MetadataField;

    invoke-virtual {p2, p4, p3}, Lcom/google/android/gms/drive/MetadataChangeSet;->zza(Lcom/google/android/gms/drive/metadata/MetadataField;Ljava/lang/Object;)Lcom/google/android/gms/drive/MetadataChangeSet;

    move-result-object p2

    :cond_9
    move-object v3, p2

    invoke-virtual {v3}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzgs(Ljava/lang/String;)Lcom/google/android/gms/drive/metadata/internal/zzk;

    move-result-object p2

    if-eqz p2, :cond_a

    invoke-virtual {p2}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzanw()Z

    move-result p2

    if-eqz p2, :cond_a

    const/4 v5, 0x1

    goto :goto_4

    :cond_a
    const/4 v5, 0x0

    :goto_4
    new-instance p2, Lcom/google/android/gms/internal/zzbjn;

    move-object v0, p2

    move-object v1, p0

    move-object v2, p1

    invoke-direct/range {v0 .. v6}, Lcom/google/android/gms/internal/zzbjn;-><init>(Lcom/google/android/gms/internal/zzbjm;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;IILcom/google/android/gms/drive/zzm;)V

    invoke-virtual {p1, p2}, Lcom/google/android/gms/common/api/GoogleApiClient;->zze(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1

    :cond_b
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "MetadataChangeSet must be provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private static zzb(Lcom/google/android/gms/drive/MetadataChangeSet;)V
    .locals 1

    if-eqz p0, :cond_3

    invoke-virtual {p0}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzgs(Ljava/lang/String;)Lcom/google/android/gms/drive/metadata/internal/zzk;

    move-result-object p0

    if-eqz p0, :cond_2

    invoke-virtual {p0}, Lcom/google/android/gms/drive/metadata/internal/zzk;->zzanw()Z

    move-result v0

    if-nez v0, :cond_0

    invoke-virtual {p0}, Lcom/google/android/gms/drive/metadata/internal/zzk;->isFolder()Z

    move-result p0

    if-nez p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    if-eqz p0, :cond_1

    goto :goto_1

    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "May not create shortcut files using this method. Use DriveFolder.createShortcutFile() instead."

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0

    :cond_2
    :goto_1
    return-void

    :cond_3
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "MetadataChangeSet must be provided."

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method


# virtual methods
.method public final createFile(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/DriveContents;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Lcom/google/android/gms/drive/MetadataChangeSet;",
            "Lcom/google/android/gms/drive/DriveContents;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveFolder$DriveFileResult;",
            ">;"
        }
    .end annotation

    invoke-static {p2}, Lcom/google/android/gms/internal/zzbjm;->zzb(Lcom/google/android/gms/drive/MetadataChangeSet;)V

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Lcom/google/android/gms/internal/zzbjm;->zza(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/DriveContents;Lcom/google/android/gms/drive/zzm;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object p1

    return-object p1
.end method

.method public final createFile(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/DriveContents;Lcom/google/android/gms/drive/ExecutionOptions;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Lcom/google/android/gms/drive/MetadataChangeSet;",
            "Lcom/google/android/gms/drive/DriveContents;",
            "Lcom/google/android/gms/drive/ExecutionOptions;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveFolder$DriveFileResult;",
            ">;"
        }
    .end annotation

    invoke-static {p2}, Lcom/google/android/gms/internal/zzbjm;->zzb(Lcom/google/android/gms/drive/MetadataChangeSet;)V

    new-instance v0, Lcom/google/android/gms/drive/zzo;

    invoke-direct {v0}, Lcom/google/android/gms/drive/zzo;-><init>()V

    if-eqz p4, :cond_2

    invoke-virtual {p4}, Lcom/google/android/gms/drive/ExecutionOptions;->zzamv()I

    move-result v1

    if-nez v1, :cond_1

    invoke-virtual {p4}, Lcom/google/android/gms/drive/ExecutionOptions;->zzamt()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-virtual {v0, v1}, Lcom/google/android/gms/drive/ExecutionOptions$Builder;->setTrackingTag(Ljava/lang/String;)Lcom/google/android/gms/drive/ExecutionOptions$Builder;

    :cond_0
    invoke-virtual {p4}, Lcom/google/android/gms/drive/ExecutionOptions;->zzamu()Z

    move-result p4

    invoke-virtual {v0, p4}, Lcom/google/android/gms/drive/ExecutionOptions$Builder;->setNotifyOnCompletion(Z)Lcom/google/android/gms/drive/ExecutionOptions$Builder;

    goto :goto_0

    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "May not set a conflict strategy for new file creation."

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_2
    :goto_0
    invoke-virtual {v0}, Lcom/google/android/gms/drive/ExecutionOptions$Builder;->build()Lcom/google/android/gms/drive/ExecutionOptions;

    move-result-object p4

    check-cast p4, Lcom/google/android/gms/drive/zzm;

    invoke-direct {p0, p1, p2, p3, p4}, Lcom/google/android/gms/internal/zzbjm;->zza(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/DriveContents;Lcom/google/android/gms/drive/zzm;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object p1

    return-object p1
.end method

.method public final createFolder(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "Lcom/google/android/gms/drive/MetadataChangeSet;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveFolder$DriveFolderResult;",
            ">;"
        }
    .end annotation

    if-eqz p2, :cond_2

    invoke-virtual {p2}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_1

    invoke-virtual {p2}, Lcom/google/android/gms/drive/MetadataChangeSet;->getMimeType()Ljava/lang/String;

    move-result-object v0

    const-string v1, "application/vnd.google-apps.folder"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The mimetype must be of type application/vnd.google-apps.folder"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    :goto_0
    new-instance v0, Lcom/google/android/gms/internal/zzbjo;

    invoke-direct {v0, p0, p1, p2}, Lcom/google/android/gms/internal/zzbjo;-><init>(Lcom/google/android/gms/internal/zzbjm;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zze(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1

    :cond_2
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "MetadataChangeSet must be provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final listChildren(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveApi$MetadataBufferResult;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Lcom/google/android/gms/internal/zzbjm;->queryChildren(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object p1

    return-object p1
.end method

.method public final queryChildren(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 4
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

    new-instance v0, Lcom/google/android/gms/internal/zzbid;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzbid;-><init>()V

    new-instance v1, Lcom/google/android/gms/drive/query/Query$Builder;

    invoke-direct {v1}, Lcom/google/android/gms/drive/query/Query$Builder;-><init>()V

    sget-object v2, Lcom/google/android/gms/drive/query/SearchableField;->PARENTS:Lcom/google/android/gms/drive/metadata/SearchableCollectionMetadataField;

    invoke-virtual {p0}, Lcom/google/android/gms/internal/zzbkc;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object v3

    invoke-static {v2, v3}, Lcom/google/android/gms/drive/query/Filters;->in(Lcom/google/android/gms/drive/metadata/SearchableCollectionMetadataField;Ljava/lang/Object;)Lcom/google/android/gms/drive/query/Filter;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/google/android/gms/drive/query/Query$Builder;->addFilter(Lcom/google/android/gms/drive/query/Filter;)Lcom/google/android/gms/drive/query/Query$Builder;

    move-result-object v1

    if-eqz p2, :cond_1

    invoke-virtual {p2}, Lcom/google/android/gms/drive/query/Query;->getFilter()Lcom/google/android/gms/drive/query/Filter;

    move-result-object v2

    if-eqz v2, :cond_0

    invoke-virtual {p2}, Lcom/google/android/gms/drive/query/Query;->getFilter()Lcom/google/android/gms/drive/query/Filter;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/google/android/gms/drive/query/Query$Builder;->addFilter(Lcom/google/android/gms/drive/query/Filter;)Lcom/google/android/gms/drive/query/Query$Builder;

    :cond_0
    invoke-virtual {p2}, Lcom/google/android/gms/drive/query/Query;->getPageToken()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/google/android/gms/drive/query/Query$Builder;->setPageToken(Ljava/lang/String;)Lcom/google/android/gms/drive/query/Query$Builder;

    invoke-virtual {p2}, Lcom/google/android/gms/drive/query/Query;->getSortOrder()Lcom/google/android/gms/drive/query/SortOrder;

    move-result-object p2

    invoke-virtual {v1, p2}, Lcom/google/android/gms/drive/query/Query$Builder;->setSortOrder(Lcom/google/android/gms/drive/query/SortOrder;)Lcom/google/android/gms/drive/query/Query$Builder;

    :cond_1
    invoke-virtual {v1}, Lcom/google/android/gms/drive/query/Query$Builder;->build()Lcom/google/android/gms/drive/query/Query;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Lcom/google/android/gms/internal/zzbid;->query(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object p1

    return-object p1
.end method

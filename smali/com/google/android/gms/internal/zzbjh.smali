.class public final Lcom/google/android/gms/internal/zzbjh;
.super Lcom/google/android/gms/internal/zzbkc;

# interfaces
.implements Lcom/google/android/gms/drive/DriveFile;


# direct methods
.method public constructor <init>(Lcom/google/android/gms/drive/DriveId;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzbkc;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    return-void
.end method


# virtual methods
.method public final open(Lcom/google/android/gms/common/api/GoogleApiClient;ILcom/google/android/gms/drive/DriveFile$DownloadProgressListener;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/GoogleApiClient;",
            "I",
            "Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;",
            ")",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/android/gms/drive/DriveApi$DriveContentsResult;",
            ">;"
        }
    .end annotation

    const/high16 v0, 0x10000000

    if-eq p2, v0, :cond_1

    const/high16 v0, 0x20000000

    if-eq p2, v0, :cond_1

    const/high16 v0, 0x30000000

    if-ne p2, v0, :cond_0

    goto :goto_0

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Invalid mode provided."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    :goto_0
    if-nez p3, :cond_2

    const/4 p3, 0x0

    goto :goto_1

    :cond_2
    new-instance v0, Lcom/google/android/gms/internal/zzbjj;

    invoke-virtual {p1, p3}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzp(Ljava/lang/Object;)Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object p3

    invoke-direct {v0, p3}, Lcom/google/android/gms/internal/zzbjj;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    move-object p3, v0

    :goto_1
    new-instance v0, Lcom/google/android/gms/internal/zzbji;

    invoke-direct {v0, p0, p1, p2, p3}, Lcom/google/android/gms/internal/zzbji;-><init>(Lcom/google/android/gms/internal/zzbjh;Lcom/google/android/gms/common/api/GoogleApiClient;ILcom/google/android/gms/drive/DriveFile$DownloadProgressListener;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->zzd(Lcom/google/android/gms/common/api/internal/zzm;)Lcom/google/android/gms/common/api/internal/zzm;

    move-result-object p1

    return-object p1
.end method

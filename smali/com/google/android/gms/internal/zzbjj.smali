.class final Lcom/google/android/gms/internal/zzbjj;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;


# instance fields
.field private final zzghr:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjj;->zzghr:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final onProgress(JJ)V
    .locals 8

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbjj;->zzghr:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v7, Lcom/google/android/gms/internal/zzbjk;

    move-object v1, v7

    move-object v2, p0

    move-wide v3, p1

    move-wide v5, p3

    invoke-direct/range {v1 .. v6}, Lcom/google/android/gms/internal/zzbjk;-><init>(Lcom/google/android/gms/internal/zzbjj;JJ)V

    invoke-virtual {v0, v7}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

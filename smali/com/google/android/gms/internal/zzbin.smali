.class final Lcom/google/android/gms/internal/zzbin;
.super Lcom/google/android/gms/internal/zzbhi;


# instance fields
.field private final zzfwc:Lcom/google/android/gms/common/api/internal/zzn;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/drive/DriveApi$DriveIdResult;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/google/android/gms/common/api/internal/zzn;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/drive/DriveApi$DriveIdResult;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzbhi;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbin;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    return-void
.end method


# virtual methods
.method public final onError(Lcom/google/android/gms/common/api/Status;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbin;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzbio;

    const/4 v2, 0x0

    invoke-direct {v1, p1, v2}, Lcom/google/android/gms/internal/zzbio;-><init>(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/DriveId;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzblu;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbin;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzbio;

    sget-object v2, Lcom/google/android/gms/common/api/Status;->zzfhp:Lcom/google/android/gms/common/api/Status;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzblu;->zzgfw:Lcom/google/android/gms/drive/DriveId;

    invoke-direct {v1, v2, p1}, Lcom/google/android/gms/internal/zzbio;-><init>(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/DriveId;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzbmf;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbin;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzbio;

    sget-object v2, Lcom/google/android/gms/common/api/Status;->zzfhp:Lcom/google/android/gms/common/api/Status;

    new-instance v3, Lcom/google/android/gms/internal/zzbhy;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzbmf;->zzggg:Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    invoke-direct {v3, p1}, Lcom/google/android/gms/internal/zzbhy;-><init>(Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;)V

    invoke-virtual {v3}, Lcom/google/android/gms/drive/Metadata;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object p1

    invoke-direct {v1, v2, p1}, Lcom/google/android/gms/internal/zzbio;-><init>(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/DriveId;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method

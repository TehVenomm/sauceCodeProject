.class abstract Lcom/google/android/gms/internal/zzbkn;
.super Lcom/google/android/gms/internal/zzbiu;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzbiu<",
        "Lcom/google/android/gms/drive/DriveResource$MetadataResult;",
        ">;"
    }
.end annotation


# direct methods
.method private constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiu;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method

.method synthetic constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbkd;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Lcom/google/android/gms/internal/zzbkn;-><init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzb(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/common/api/Result;
    .locals 2

    new-instance v0, Lcom/google/android/gms/internal/zzbkm;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1}, Lcom/google/android/gms/internal/zzbkm;-><init>(Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/Metadata;)V

    return-object v0
.end method

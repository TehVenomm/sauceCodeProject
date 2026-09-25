.class abstract Lcom/google/android/gms/internal/zzbkb;
.super Lcom/google/android/gms/internal/zzbiu;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzbiu<",
        "Lcom/google/android/gms/drive/DrivePreferencesApi$FileUploadPreferencesResult;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzgic:Lcom/google/android/gms/internal/zzbjw;


# direct methods
.method public constructor <init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbkb;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiu;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zzb(Lcom/google/android/gms/common/api/Status;)Lcom/google/android/gms/common/api/Result;
    .locals 3

    new-instance v0, Lcom/google/android/gms/internal/zzbka;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbkb;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    const/4 v2, 0x0

    invoke-direct {v0, v1, p1, v2, v2}, Lcom/google/android/gms/internal/zzbka;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/FileUploadPreferences;Lcom/google/android/gms/internal/zzbjx;)V

    return-object v0
.end method

.class public final Lcom/google/android/gms/internal/zzboe;
.super Ljava/lang/Object;


# static fields
.field public static final zzgml:Lcom/google/android/gms/internal/zzbof;

.field public static final zzgmm:Lcom/google/android/gms/internal/zzbog;

.field public static final zzgmn:Lcom/google/android/gms/internal/zzboi;

.field public static final zzgmo:Lcom/google/android/gms/internal/zzboh;

.field public static final zzgmp:Lcom/google/android/gms/internal/zzbok;

.field public static final zzgmq:Lcom/google/android/gms/internal/zzboj;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    new-instance v0, Lcom/google/android/gms/internal/zzbof;

    const-string v1, "created"

    const v2, 0x3e8fa0

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbof;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgml:Lcom/google/android/gms/internal/zzbof;

    new-instance v0, Lcom/google/android/gms/internal/zzbog;

    const-string v1, "lastOpenedTime"

    const v3, 0x419ce0

    invoke-direct {v0, v1, v3}, Lcom/google/android/gms/internal/zzbog;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmm:Lcom/google/android/gms/internal/zzbog;

    new-instance v0, Lcom/google/android/gms/internal/zzboi;

    const-string v1, "modified"

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzboi;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmn:Lcom/google/android/gms/internal/zzboi;

    new-instance v0, Lcom/google/android/gms/internal/zzboh;

    const-string v1, "modifiedByMe"

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzboh;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmo:Lcom/google/android/gms/internal/zzboh;

    new-instance v0, Lcom/google/android/gms/internal/zzbok;

    const-string v1, "sharedWithMe"

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbok;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmp:Lcom/google/android/gms/internal/zzbok;

    new-instance v0, Lcom/google/android/gms/internal/zzboj;

    const-string v1, "recency"

    const v2, 0x7a1200

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzboj;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmq:Lcom/google/android/gms/internal/zzboj;

    return-void
.end method

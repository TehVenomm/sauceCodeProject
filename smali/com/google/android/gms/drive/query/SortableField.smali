.class public Lcom/google/android/gms/drive/query/SortableField;
.super Ljava/lang/Object;


# static fields
.field public static final CREATED_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field

.field public static final LAST_VIEWED_BY_ME:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field

.field public static final MODIFIED_BY_ME_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field

.field public static final MODIFIED_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field

.field public static final QUOTA_USED:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation
.end field

.field public static final SHARED_WITH_ME_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field

.field public static final TITLE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static zzgnf:Lcom/google/android/gms/drive/metadata/SortableMetadataField;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/drive/metadata/SortableMetadataField<",
            "Ljava/util/Date;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    sget-object v0, Lcom/google/android/gms/internal/zzbnr;->zzglz:Lcom/google/android/gms/internal/zzboa;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->TITLE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgml:Lcom/google/android/gms/internal/zzbof;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->CREATED_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmn:Lcom/google/android/gms/internal/zzboi;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->MODIFIED_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmo:Lcom/google/android/gms/internal/zzboh;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->MODIFIED_BY_ME_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmm:Lcom/google/android/gms/internal/zzbog;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->LAST_VIEWED_BY_ME:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmp:Lcom/google/android/gms/internal/zzbok;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->SHARED_WITH_ME_DATE:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzbnr;->zzglw:Lcom/google/android/gms/internal/zzbnx;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->QUOTA_USED:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    sget-object v0, Lcom/google/android/gms/internal/zzboe;->zzgmq:Lcom/google/android/gms/internal/zzboj;

    sput-object v0, Lcom/google/android/gms/drive/query/SortableField;->zzgnf:Lcom/google/android/gms/drive/metadata/SortableMetadataField;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

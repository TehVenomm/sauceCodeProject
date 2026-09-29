.class public final enum Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;
.super Ljava/lang/Enum;

# interfaces
.implements Lcom/google/android/gms/internal/measurement/zzfc;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/gms/internal/measurement/zzbk$zzc;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "zzb"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;",
        ">;",
        "Lcom/google/android/gms/internal/measurement/zzfc;"
    }
.end annotation


# static fields
.field public static final enum zzva:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

.field public static final enum zzvb:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

.field public static final enum zzvc:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

.field public static final enum zzvd:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

.field public static final enum zzve:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

.field private static final zzvf:Lcom/google/android/gms/internal/measurement/zzfb;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/internal/measurement/zzfb<",
            "Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;",
            ">;"
        }
    .end annotation
.end field

.field private static final synthetic zzvg:[Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;


# instance fields
.field private final value:I


# direct methods
.method static constructor <clinit>()V
    .locals 7

    .line 14
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const-string v1, "UNKNOWN_COMPARISON_TYPE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzva:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    .line 15
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const-string v1, "LESS_THAN"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvb:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    .line 16
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const-string v1, "GREATER_THAN"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v4}, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvc:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    .line 17
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const-string v1, "EQUAL"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5, v5}, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvd:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    .line 18
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const-string v1, "BETWEEN"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6, v6}, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzve:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    const/4 v0, 0x5

    .line 19
    new-array v0, v0, [Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    sget-object v1, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzva:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    aput-object v1, v0, v2

    sget-object v1, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvb:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    aput-object v1, v0, v3

    sget-object v1, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvc:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    aput-object v1, v0, v4

    sget-object v1, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvd:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    aput-object v1, v0, v5

    sget-object v1, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzve:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    aput-object v1, v0, v6

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvg:[Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    .line 20
    new-instance v0, Lcom/google/android/gms/internal/measurement/zzbl;

    invoke-direct {v0}, Lcom/google/android/gms/internal/measurement/zzbl;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvf:Lcom/google/android/gms/internal/measurement/zzfb;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 11
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 12
    iput p3, p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->value:I

    return-void
.end method

.method public static values()[Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;
    .locals 1

    .line 1
    sget-object v0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvg:[Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    invoke-virtual {v0}, [Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object v0
.end method

.method public static zzf(I)Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;
    .locals 0

    packed-switch p0, :pswitch_data_0

    const/4 p0, 0x0

    return-object p0

    .line 8
    :pswitch_0
    sget-object p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzve:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object p0

    .line 7
    :pswitch_1
    sget-object p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvd:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object p0

    .line 6
    :pswitch_2
    sget-object p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvc:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object p0

    .line 5
    :pswitch_3
    sget-object p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzvb:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object p0

    .line 4
    :pswitch_4
    sget-object p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->zzva:Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;

    return-object p0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public static zzlh()Lcom/google/android/gms/internal/measurement/zzfe;
    .locals 1

    .line 10
    sget-object v0, Lcom/google/android/gms/internal/measurement/zzbm;->zzvk:Lcom/google/android/gms/internal/measurement/zzfe;

    return-object v0
.end method


# virtual methods
.method public final zzlg()I
    .locals 1

    .line 2
    iget v0, p0, Lcom/google/android/gms/internal/measurement/zzbk$zzc$zzb;->value:I

    return v0
.end method
